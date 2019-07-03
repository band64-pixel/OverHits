 Imports System.Media
Imports System.Runtime.InteropServices
 
 
Public Class Form1
    'build 15
    'recommended to use with Mccree
    'toggle it on/off clicking middle mouse button - will make sound
 
    'edit values below to modify triggerbot
    'autofire delay - 510 for mccree - 1400 for widow 
    Public FireDelayRightMouseDown As UInteger = 1400 'autofire delay when right mouse is down
    Public FireDelayRightMouseUp As UInteger = 510 'autofire delay when right mouse is up
 
    'Triggerbot sensitivity settings
    Public MinQuad3 As Single = 3 'minimum amount of triggering pixels required in 3 chart quadrants (0-10)
    Public MinQuad4 As Single = 1 'minimum amount of triggering pixels required in 4 chart quadrants (0-10)
    Public CombinedLineFigureMulti As Single = 1.4 'Lower to reduce randomly shooting around map (1.00-10.00)
    Public ScanRangeMulti As Single = 0.18 'minimum amount of triggering pixels needed to be found * scanrange (0-10.00)
    Public MaxBrightness As Single = 0.85 'maximum brightness of pixel range (0-1.00)
 
    Public MinRed As Single = 110 'minimum red value of pixel - set lower for darker maps (0-255)
    Public MaxSatToBriRatio As Single = 0.6 'Max ratio of saturation to brightness allowed (0-1.00)
    Public MaxBritoSatRatio As Single = 0.35 'Max ratio of brightness to saturation allowed (0-1.00)
    Public RedOrangeRange As Single = 15 ' adjust down to decrease targeting orange (0-30)
    Public RedVioletRange As Single = 343 ' adjust up to decrease targeting violet (330-360)
    Public DistanceFromTargetBeforeFiringX As Long = 6 ' maximum distance from target in x to fire (1-100)
    Public DistanceFromTargetBeforeFiringY As Long = 6 ' maximum distance from target in y to fire (1-100)
 
    Public NonWhiteCrosshair As Long = 1 ' change this to 1 to stop widow from auto firing while in zoom.  Crosshair cannot be a pure white for it to work if unzoomed.
    Public AdjustDown As Integer = -1 'optional - move scanning up/down depending on where window is - negative values allowed
    Public AdjustRight As Integer = -1  'optional - move scanning left/right depending on where window is - negative values allowed
 
    Private Declare Function GetTickCount Lib "kernel32" () As UInt32
    Public t As New Timer With {.Interval = 1}
    Public ScanRange As Long = 56
    Public ScanRangeRightMouseDown As Long = 56  'amount of pixels to scan for sniper - right click down
    Public ScanRangeRightMouseUp As Long = 28 'amount of pixels to scan for right click up
    Public MinSaturation As Single = 0.25 'dynamically adjusted
    Public MinBrightness As Single = 0.25 'dynamically adjusted
 
    Public Pixels(ScanRange * ScanRange + ScanRange) As Integer
    Public Pixels_Hue(ScanRange * ScanRange + ScanRange) As Single
    Public Pixels_Brightness(ScanRange * ScanRange + ScanRange) As Single
    Public Pixels_Saturation(ScanRange * ScanRange + ScanRange) As Single
    Public Pixels_R(ScanRange * ScanRange + ScanRange) As Single
    Public LastFireTick As UInteger
    Public ToggleOnOff As UInteger
    Public TriggerbotActive As Integer
 
    Dim AmountOfPureWhiteFound As Long
 
    Public ChartQuad(4) As Long
 
    Public BtnEnable As New Button
    Public ChkBluebox As New CheckBox
    Public TrackBarX As New TrackBar
    Public TrackBarY As New TrackBar
    Public TrackBarSensitivity As New TrackBar
    Public LblTrackBarX As New Label
    Public LblTrackBarY As New Label
    Public TxtTrackBarX As New TextBox
    Public TxtTrackBarY As New TextBox
    Public LblSensitivity As New Label
    Public LblHowSensitive As New Label
    Public IsFormClosing As Boolean = False
 
 
    <DllImport("user32.dll")>
    Shared Function GetAsyncKeyState(ByVal vKey As System.Windows.Forms.Keys) As Short
    End Function
 
    Private Const VK_RBUTTON = &H2
    Private Const VK_LBUTTON = &H1
    Private Const VK_MButton = &H4
    Private Const VK_Q = &H51
    Private Const VK_RETURN = &HD
 
    <DllImport("user32.dll")>
    Private Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDc As IntPtr) As IntPtr
    End Function
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetActiveWindow() As IntPtr
    End Function
    <DllImport("gdi32")>
    Public Shared Function BitBlt(ByVal hDestDC As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As IntPtr, ByVal SrcX As Integer, ByVal SrcY As Integer, ByVal Rop As Integer) As Boolean
    End Function
    Dim GCH As GCHandle = GCHandle.Alloc(Pixels, GCHandleType.Pinned)
 
    Dim Bmp As New Drawing.Bitmap(ScanRange, ScanRange, 4 * ScanRange,
                                  Imaging.PixelFormat.Format32bppArgb,
                                  GCH.AddrOfPinnedObject)
 
    <DllImport("user32.dll")>
    Private Shared Function GetWindowDC(ByVal hwnd As IntPtr) As IntPtr
    End Function
 
    Declare Function GetWindowRect Lib "user32.dll" (
ByVal hwnd As Int32,
ByRef lpRect As Rectangle) As Int32
 
 
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If FireDelayRightMouseDown < 100 Then FireDelayRightMouseDown = 100 ' don't make this too low to avoid suspicion - to make it lower use KEYEVENTF_KEYDOWN and KEYEVENTF_KEYUP
        If FireDelayRightMouseUp < 100 Then FireDelayRightMouseUp = 100 ' don't make this too low to avoid suspicion - to make it lower use KEYEVENTF_KEYDOWN and KEYEVENTF_KEYUP
        CreateElements()
    End Sub
 
    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        IsFormClosing = True
    End Sub
 
    Private Sub Timer_tick(sender As Object, e As EventArgs)
        If TriggerbotActive = 1 Then
            RunAimbot()
        End If
 
        If CheckKeyMButton() = 1 Then
            If ToggleOnOff = 0 Then
                If TriggerbotActive = 0 Then
                    StartTriggerbox()
                    SystemSounds.Beep.Play()
                Else
                    StopTriggerbox()
                    SystemSounds.Hand.Play()
                End If
                ToggleOnOff = 1
            End If
        Else
            ToggleOnOff = 0
        End If
    End Sub
 
 
    Public Sub RunAimbot()
 
        Dim Amountfound As Long
        Dim AmountfoundCC1 As Long
        Dim CCAmount(0 To 1000) As Long
        Dim I As Long
 
 
 
        Dim TargetX As Long
        Dim TargetY As Long
        Dim TargetAmountFound As Long
 
 
        Dim avgHUE As Single
        Dim avgBrightness As Single
        Dim avgsaturation As Single
 
        Dim avgHueFound As Single
        Dim avgBrightnessFound As Single
        Dim avgsaturationFound As Single
 
 
        Dim AMountOfPixels As Single
        Dim FireTriggered As Single
 
        Dim YW As Single
        Dim X As Single
        Dim Y As Single
 
        Dim YW2 As Single
        Dim X2 As Single
        Dim Y2 As Single
        Dim YWY As Integer
        Dim YW2Y2 As Integer
        Dim CombinedLineFigure As Long
 
        Dim screenwidth = CInt(Screen.PrimaryScreen.Bounds.Width)
        Dim screenheight = CInt(Screen.PrimaryScreen.Bounds.Height)
 
        Dim g As Graphics
        Dim hdcDest As IntPtr = IntPtr.Zero
        Dim desktopHandleDC As IntPtr = IntPtr.Zero
        Dim desktopHandle As IntPtr
 
        'change scan size based on whether mouse in down or up
 
        SetScanRange()
 
 
 
 
        desktopHandle = GetActiveWindow()
 
 
        Dim R As Rectangle = New Rectangle(CInt((Screen.PrimaryScreen.Bounds.Width / 2) - (ScanRange / 2)) + AdjustRight, CInt((Screen.PrimaryScreen.Bounds.Height / 2) - (ScanRange / 2)) + AdjustDown, ScanRange, ScanRange)
 
 
        Bmp = New Bitmap(ScanRange, ScanRange, 4 * ScanRange,
                                      Imaging.PixelFormat.Format32bppArgb,
                                      GCH.AddrOfPinnedObject)
 
 
 
        g = Graphics.FromImage(Bmp)
        desktopHandleDC = GetWindowDC(desktopHandle)
        hdcDest = g.GetHdc
 
        Dim gf As Integer
        Dim GH As Rectangle
 
 
        gf = GetWindowRect(desktopHandle, GH)
 
 
 
        Dim NewRectWidth As Integer
        Dim NewRectheight As Integer
        NewRectWidth = GH.Width - GH.Left
        NewRectheight = GH.Height - GH.Top
 
 
        BitBlt(hdcDest, 0, 0, ScanRange, ScanRange, desktopHandleDC, CInt((screenwidth / 2) - (ScanRange / 2) + AdjustRight), CInt((screenheight / 2) - (ScanRange / 2) + AdjustDown), CopyPixelOperation.SourceCopy)
 
 
        g.ReleaseHdc(hdcDest)
        ReleaseDC(desktopHandle, desktopHandleDC)
        Bmp.Dispose()
        g.Dispose() : g = Nothing
 
 
 
        Dim myColor As Color
 
 
        AMountOfPixels = ScanRange * ScanRange
 
        For X = 3 To ScanRange - 4
            YW = X * ScanRange
            For Y = 3 To ScanRange - 4
                YWY = YW + Y
                myColor = Color.FromArgb((Pixels(YWY)))
 
                Pixels_Hue(YWY) = myColor.GetHue
                Pixels_Brightness(YWY) = myColor.GetBrightness
                Pixels_Saturation(YWY) = myColor.GetSaturation
                Pixels_R(YWY) = myColor.R
 
                avgHUE = avgHUE + Pixels_Hue(YWY)
                avgBrightness = avgBrightness + Pixels_Brightness(YWY)
                avgsaturation = avgsaturation + Pixels_Saturation(YWY)
 
                If NonWhiteCrosshair = 1 Then
                    'sniper zoom fix
                    If Pixels_Hue(YWY) = 0 And Pixels_Saturation(YWY) = 0 And Pixels_Brightness(YWY) = 1 And CheckRightMouse() = 1 And (LastFireTick + FireDelayRightMouseDown) <= GetTickCount Then
                        AmountOfPureWhiteFound = AmountOfPureWhiteFound + 1
                        If AmountOfPureWhiteFound > 20 Then
                            'fix for sniper red in zoom with large scan range =/
 
                            LastFireTick = LastFireTick + 150
                        End If
                    End If
                End If
 
                If Pixels_Hue(YWY) < RedOrangeRange Or Pixels_Hue(YWY) > RedVioletRange Then
                    If Pixels_Brightness(YWY) > MinBrightness And Pixels_Brightness(YWY) < MaxBrightness And
                        Pixels_Saturation(YWY) > MinSaturation And Pixels_R(YWY) > MinRed And
                        Pixels_Saturation(YWY) >= Pixels_Brightness(YWY) * MaxSatToBriRatio And
                        Pixels_Saturation(YWY) * MaxBritoSatRatio <= Pixels_Brightness(YWY) Then ' Target by hue to bypass 1.12
                        Amountfound = Amountfound + 1
 
                        'DEBUG
                        ' RichTextBox1.AppendText("HUE=" & (myColor.GetHue()).ToString & vbTab & "SAT=" & myColor.GetSaturation().ToString & vbTab & "BGT=" & (myColor.GetBrightness()).ToString & vbTab & myColor.R & vbNewLine)
 
 
 
                        'advanced comparison
 
                        For X2 = (X - 3) To (X + 3)
                            YW2 = X2 * ScanRange
                            For Y2 = (Y - 3) To (Y + 3)
                                YW2Y2 = YW2 + Y2
                                'x2=0 and y2=0 produces additional triggered pixel
 
                                If Pixels_Hue(YW2Y2) < RedOrangeRange Or Pixels_Hue(YW2Y2) > RedVioletRange Then
                                    If Pixels_Brightness(YW2Y2) > MinBrightness And Pixels_Brightness(YW2Y2) < MaxBrightness And
                                        Pixels_Saturation(YW2Y2) > MinSaturation And Pixels_R(YW2Y2) > MinRed And
                                        Pixels_Saturation(YW2Y2) >= Pixels_Brightness(YW2Y2) * MaxSatToBriRatio And
                                        Pixels_Saturation(YW2Y2) * MaxBritoSatRatio <= Pixels_Brightness(YW2Y2) Then ' Target by hue to bypass 1.12
                                        AmountfoundCC1 = AmountfoundCC1 + 1
                                    End If
                                End If
 
                            Next Y2
                        Next X2
 
                        ' MaxSatToBriRatio As Single = 0.6
                        ' MaxBritoSatRatio As Single = 0.35
 
 
                        'target x/y
                        If CheckRightMouse() = 0 Then
                            If AmountfoundCC1 >= 1 And AmountfoundCC1 <= 13 Then
                                TargetY = TargetY + Y  'settings for right mouse up
                                TargetX = TargetX + X
 
                                CheckXYRange(X, Y)
 
                                TargetAmountFound = TargetAmountFound + 1
 
                                ' RichTextBox1.AppendText("SAT= " & myColor.GetSaturation() & " " & "BRI= " & myColor.GetBrightness() & " " & "HUE+BRI= " & myColor.GetSaturation() + myColor.GetBrightness() & vbNewLine)
 
 
 
                                If Pixels_Hue(YWY) <= RedOrangeRange Then 'RedOrangeRange
                                    avgHueFound = avgHueFound + Pixels_Hue(YWY) + 360 'beta test
                                Else
                                    avgHueFound = avgHueFound + Pixels_Hue(YWY)
                                End If
 
 
                                avgBrightnessFound = avgBrightnessFound + Pixels_Brightness(YWY)
                                avgsaturationFound = avgsaturationFound + Pixels_Saturation(YWY)
                            End If
                            'Next I
                        Else
                            ' For I = 2 To 19
                            If AmountfoundCC1 >= 2 And AmountfoundCC1 <= 19 Then
                                TargetY = TargetY + Y  'settings for right mouse down
                                TargetX = TargetX + X
 
                                CheckXYRange(X, Y)
 
                                TargetAmountFound = TargetAmountFound + 1
 
                                If Pixels_Hue(YWY) <= RedOrangeRange Then 'RedOrangeRange
                                    avgHueFound = avgHueFound + Pixels_Hue(YWY) + 360 'testing this out
                                Else
                                    avgHueFound = avgHueFound + Pixels_Hue(YWY)
                                End If
                                'avgHueFound = avgHueFound + Pixels_Hue(YWY) 'beta test
                                avgBrightnessFound = avgBrightnessFound + Pixels_Brightness(YWY)
                                avgsaturationFound = avgsaturationFound + Pixels_Saturation(YWY)
                                '  Next I
                            End If
                        End If
 
 
                        CCAmount(AmountfoundCC1) = CCAmount(AmountfoundCC1) + 1
                        AmountfoundCC1 = 0
 
 
 
                    End If
                End If
NExtY:
            Next Y
        Next X
        'combine line figures together
        If CheckRightMouse() = 0 Then
            For I = 1 To 13
                CombinedLineFigure = CombinedLineFigure + CCAmount(I)  'settings for right mouse up
            Next I
        Else
            For I = 2 To 19
                CombinedLineFigure = CombinedLineFigure + CCAmount(I)  'settings for right mouse down
            Next I
        End If
 
 
        ' CombinedLineFigure = CombinedLineFigure * 2
 
 
        If CombinedLineFigure >= (ScanRange * ScanRangeMulti) And (CombinedLineFigure * CombinedLineFigureMulti) >= Amountfound Then
 
 
 
 
 
            FireTriggered = 1
            '  RichTextBox1.AppendText("CombinedLineFigure=" & CombinedLineFigure & vbTab & "Amountfound=" & Amountfound & vbNewLine)
            '  RichTextBox1.AppendText("CombinedLineFigure=" & CombinedLineFigure & vbNewLine)
            ' RichTextBox1.AppendText("Fire" & vbNewLine)
 
 
 
        End If
 
 
        avgHUE = avgHUE / AMountOfPixels
        avgBrightness = avgBrightness / AMountOfPixels
        avgsaturation = avgsaturation / AMountOfPixels
 
        avgHueFound = avgHueFound / TargetAmountFound
        avgBrightnessFound = avgBrightnessFound / TargetAmountFound
        avgsaturationFound = avgsaturationFound / TargetAmountFound
 
 
 
        If avgHUE > 2 And avgHUE < 14 And avgBrightness < 0.45 And avgsaturation < 0.45 Then
            ' FireTriggered = 0 ' don't fire 
        End If
 
 
        If CheckRightMouse() = 0 Then 'adjust for zoom
 
            If Math.Abs((TargetX / TargetAmountFound) - (ScanRange / 2)) > DistanceFromTargetBeforeFiringX Then
                FireTriggered = 0 ' don't fire
            End If
 
            If Math.Abs((TargetY / TargetAmountFound) - (ScanRange / 2)) > DistanceFromTargetBeforeFiringY Then
                FireTriggered = 0 ' don't fire
            End If
        Else 'while zoomed / hold down right click
            If Math.Abs((TargetX / TargetAmountFound) - (ScanRange / 2)) > (DistanceFromTargetBeforeFiringX * 2) Then
                FireTriggered = 0 ' don't fire
            End If
 
            If Math.Abs((TargetY / TargetAmountFound) - (ScanRange / 2)) > (DistanceFromTargetBeforeFiringY * 2) Then
                FireTriggered = 0 ' don't fire
            End If
 
 
        End If
 
        If CheckKeyQ() = 1 And CheckRightMouse() = 0 Then
            LastFireTick = GetTickCount + 4000 'pressing Q for ultimate delays firing by 5 secs when mouse is up.  With widow use ultimate while zoomed to avoid delay.
        End If
 
        If CheckKeyENTER() = 1 Then
            LastFireTick = GetTickCount + 1500 'disable aimbot while chatting
        End If
 
        If QuadrantCheck4() = 0 And QuadrantCheck3() = 0 Then
            FireTriggered = 0 ' don't fire 
        End If
 
 
        If FireTriggered = 1 Then
            Fire()
        End If
 
 
 
 
        'debug and testing
        If LastFireTick = GetTickCount Then
            '  RichTextBox1.AppendText("CombinedLineFigure=" & CombinedLineFigure & vbNewLine)
            ' RichTextBox1.AppendText("CombinedLineFigure=" & CombinedLineFigure & vbTab & "Amountfound=" & Amountfound & vbNewLine)
            ' avgHueFound = avgHueFound Mod 360
            '  RichTextBox1.Text = ("avgHUE=" & avgHUE & vbTab & "avgSAT=" & avgsaturation & vbTab & "avgBGT=" & avgBrightness & vbNewLine)
 
            '   RichTextBox1.AppendText("MinBrightness=" & MinBrightness & vbTab & "MinSaturation=" & MinSaturation & vbNewLine)
            '  RichTextBox1.AppendText("avgHUEfound=" & avgHueFound & vbTab & "avgSATfound=" & avgsaturationFound & vbTab & "avgBGTfound=" & avgBrightnessFound & vbNewLine)
            '   RichTextBox1.AppendText("avgBGT=" & avgBrightness & vbTab & "avgSAT=" & avgsaturation & vbTab & "avgSATfound=" & avgsaturationFound & vbTab & "avgBGTfound=" & avgBrightnessFound & vbNewLine)
 
 
            '  Dim i2 As Long
            '  For i2 = 0 To 30
            '  RichTextBox1.AppendText(CCAmount(i2) & vbTab)
            '  Next
            '  RichTextBox1.AppendText(vbNewLine)
            'RichTextBox1.AppendText(Math.Round((TargetX / TargetAmountFound) - ScanRange / 2) & " " & (Math.Round((TargetY / TargetAmountFound)) - ScanRange / 2) & vbNewLine)
 
 
            'RichTextBox1.AppendText(ChartQuad(1) & " " & ChartQuad(2) & " " & ChartQuad(3) & " " & ChartQuad(4) & vbNewLine)
 
 
 
            'moving mouse settings
            'aimbot target X = Math.Round((screenwidth / 2) + (TargetX / TargetAmountFound) - ScanRange / 2) + AdjustRight - not tested
            'aimbot target Y = Math.Round((screenheight / 2) + (TargetY / TargetAmountFound) - ScanRange / 2) + AdjustDown- not tested
            'Y axis might be reversed - (Y * -1) to fix
 
 
            'RichTextBox1.AppendText(Math.Round((screenheight / 2) + (TargetX / TargetAmountFound) - ScanRange / 2) + AdjustRight & vbNewLine)
 
 
        End If
        MinBrightness = avgBrightness + 0.1 - (avgBrightness / 0.7 * 0.1)
        MinSaturation = avgsaturation + 0.3 - (avgsaturation / 0.8 * 0.3)
 
        'MinBrightness = avgBrightness + 0.1
        'MinSaturation = avgsaturation + 0.15
 
        If MinBrightness < 0.25 Then MinBrightness = 0.25
        If MinSaturation < 0.25 Then MinSaturation = 0.25
 
        If MinBrightness > 0.7 Then MinBrightness = 0.7
        If MinSaturation > 0.8 Then MinSaturation = 0.8
 
        Array.Clear(ChartQuad, 0, ChartQuad.Length)
    End Sub
    Public Sub Fire()
        If (LastFireTick + FireDelayRightMouseDown) <= GetTickCount And CheckRightMouse() = 1 Then GoTo Fire
        If (LastFireTick + FireDelayRightMouseUp) <= GetTickCount And CheckRightMouse() = 0 Then
Fire:
            If CheckLeftMouse() = 0 Then 'don't fire while the mouse is being manually clicked
                SendKeys.Send("k") 'Using the mouse to click might result in screenlock
 
                '  RichTextBox1.AppendText("avgHUE=" & avgHUE & vbTab & "avgSAT=" & avgsaturation & vbTab & "avgBGT=" & avgBrightness & vbNewLine)
            End If
            LastFireTick = GetTickCount
        End If
    End Sub
    Public Function CheckRightMouse() As Long
        If GetAsyncKeyState(VK_RBUTTON) <> 0 Then
            Return 1
        End If
        Return 0
    End Function
    Public Function CheckLeftMouse() As Long
        If GetAsyncKeyState(VK_LBUTTON) <> 0 Then
            Return 1
        End If
        Return 0
    End Function
    Public Function CheckKeyMButton() As Long
        If GetAsyncKeyState(VK_MButton) <> 0 Then
            Return 1
        End If
        Return 0
    End Function
    Public Function CheckKeyQ() As Long
        If GetAsyncKeyState(VK_Q) <> 0 Then
            Return 1
        End If
        Return 0
    End Function
    Public Function CheckKeyENTER() As Long
        If GetAsyncKeyState(VK_RETURN) <> 0 Then
            Return 1
        End If
        Return 0
    End Function
    Public Sub SetScanRange()
        If CheckRightMouse() = 0 Then
            ScanRange = ScanRangeRightMouseUp 'settings for right mouse up
        Else
            ScanRange = ScanRangeRightMouseDown 'settings for right mouse down
        End If
 
    End Sub
 
 
    Public Sub CheckXYRange(x As Long, y As Long)
        x = x - (ScanRange / 2)
        y = y - (ScanRange / 2)
 
        If x < 0 And y < 0 Then
            ChartQuad(3) = ChartQuad(3) + 1
        End If
 
        If x < 0 And y > 0 Then
            ChartQuad(2) = ChartQuad(2) + 1
        End If
 
        If x > 0 And y < 0 Then
            ChartQuad(4) = ChartQuad(4) + 1
        End If
 
        If x > 0 And y > 0 Then
            ChartQuad(1) = ChartQuad(1) + 1
        End If
 
 
 
    End Sub
    Public Function QuadrantCheck3() As Long
        Dim i As Long
        Dim QuadNumber As Long
 
        For i = 1 To 4
            If ChartQuad(i) >= MinQuad3 Then
                QuadNumber = QuadNumber + 1
            End If
        Next i
 
 
        If QuadNumber < 3 Then
            Return 0
        End If
        Return 1
    End Function
    Public Function QuadrantCheck4() As Long
        Dim i As Long
        Dim QuadNumber As Long
 
        For i = 1 To 4
            If ChartQuad(i) >= MinQuad4 Then
                QuadNumber = QuadNumber + 1
            End If
        Next i
 
        ' If QuadNumber > 2 Then
        ' RichTextBox1.AppendText(ChartQuad(1) & " " & ChartQuad(2) & " " & ChartQuad(3) & " " & ChartQuad(4) & vbNewLine)
        ' End If
 
 
        If QuadNumber < 4 Then
            Return 0
        End If
        Return 1
    End Function
    Public Sub CreateElements()
 
        'create the start/stop button
        With BtnEnable
            .Size = New Point(100, 40)
            .Location = New Point(20, 20)
            .Text = "START"
        End With
        AddHandler BtnEnable.Click, AddressOf BtnEnable_Click
        Controls.Add(BtnEnable)
 
        'create the bluebox checkbox
        With ChkBluebox
            .Size = New Point(150, 40)
            .Location = New Point(20, 60)
            .Text = "Show Detection Area"
        End With
        AddHandler ChkBluebox.Click, AddressOf Chkbluebox_Click
        Controls.Add(ChkBluebox)
 
        'create the trackbar for x-axis
        With TrackBarX
            .Size = New Point(150, 40)
            .Location = New Point(80, 120)
            .Maximum = 100
            .Value = 49
        End With
        AddHandler TrackBarX.Scroll, AddressOf TrackBarX_Scroll
        Controls.Add(TrackBarX)
 
        'create the trackbar for y-axis
        With TrackBarY
            .Size = New Point(150, 40)
            .Location = New Point(80, 160)
            .Maximum = 100
            .Value = 24
        End With
        AddHandler TrackBarY.Scroll, AddressOf TrackBarY_Scroll
        Controls.Add(TrackBarY)
 
        'create the trackbar for Sensitivity
        With TrackBarSensitivity
            .Size = New Point(150, 40)
            .Location = New Point(60, 250)
            .Maximum = 5
            .Minimum = 1
            .Value = 3
        End With
        AddHandler TrackBarSensitivity.Scroll, AddressOf TrackBarSensitivity_Scroll
        Controls.Add(TrackBarSensitivity)
 
        'create the X-Axis Label
        With LblTrackBarX
            .Size = New Point(20, 40)
            .Location = New Point(20, 120)
            .Text = "X="
        End With
        Controls.Add(LblTrackBarX)
 
        'create the Y-Axis Label
        With LblTrackBarY
            .Size = New Point(20, 40)
            .Location = New Point(20, 160)
            .Text = "Y="
        End With
        Controls.Add(LblTrackBarY)
 
 
        'create the Y-Axis Textbox
        With TxtTrackBarY
            .Size = New Point(40, 40)
            .Location = New Point(40, 160)
            .Text = "-1"
        End With
        Controls.Add(TxtTrackBarY)
        AddHandler TxtTrackBarY.TextChanged, AddressOf txttrackbary_textchanged
 
 
        'create the Y-Axis Textbox
        With TxtTrackBarX
            .Size = New Point(40, 40)
            .Location = New Point(40, 120)
            .Text = "-1"
        End With
        Controls.Add(TxtTrackBarX)
        AddHandler TxtTrackBarX.TextChanged, AddressOf txttrackbarx_textchanged
 
        'create the Sensitivity Label
        With LblSensitivity
            .Size = New Point(100, 40)
            .Location = New Point(20, 250)
            .Text = "3"
        End With
        Controls.Add(LblSensitivity)
 
        'create the Sensitivity Label
        With LblHowSensitive
            .Size = New Point(200, 40)
            .Location = New Point(50, 220)
            .Text = "Moderately Sensitive"
        End With
        Controls.Add(LblHowSensitive)
 
        'add timer
        AddHandler t.Tick, AddressOf Timer_tick
        t.Enabled = 1
 
        Me.Text = "Triggerbot"
 
        'mandate minimum form size
        If Me.Width < 260 Then Me.Width = 260
        If Me.Height < 350 Then Me.Height = 350
 
    End Sub
 
    Public Sub AdjustSensitivity(ByVal Sensitivity As Long)
        Select Case TrackBarSensitivity.Value
            Case 1
                LblHowSensitive.Text = "Less Sensitive - More Accurate"
                MinQuad3 = 3
                MinQuad4 = 2
                CombinedLineFigureMulti = 1.25
                ScanRangeMulti = 0.25
                DistanceFromTargetBeforeFiringX = 5
                DistanceFromTargetBeforeFiringY = 5
            Case 2
                LblHowSensitive.Text = "Less Sensitive - More Accurate"
                MinQuad3 = 3
                MinQuad4 = 2
                CombinedLineFigureMulti = 1.33
                ScanRangeMulti = 0.22
                DistanceFromTargetBeforeFiringX = 5
                DistanceFromTargetBeforeFiringY = 5
            Case 3
                LblHowSensitive.Text = "Moderately Sensitive"
                MinQuad3 = 3
                MinQuad4 = 1
                CombinedLineFigureMulti = 1.4
                ScanRangeMulti = 0.18
                DistanceFromTargetBeforeFiringX = 6
                DistanceFromTargetBeforeFiringY = 6
            Case 4
                LblHowSensitive.Text = "More Sensitive - Less Accurate"
                MinQuad3 = 2
                MinQuad4 = 1
                CombinedLineFigureMulti = 1.6
                ScanRangeMulti = 0.15
                DistanceFromTargetBeforeFiringX = 7
                DistanceFromTargetBeforeFiringY = 7
            Case 5
                LblHowSensitive.Text = "Very Sensitive - Less Accurate"
                MinQuad3 = 1
                MinQuad4 = 1
                CombinedLineFigureMulti = 1.8
                ScanRangeMulti = 0.1
                DistanceFromTargetBeforeFiringX = 10
                DistanceFromTargetBeforeFiringY = 10
        End Select
 
    End Sub
    Private Sub TrackBarSensitivity_Scroll(sender As Object, e As EventArgs)
        LblSensitivity.Text = TrackBarSensitivity.Value
        AdjustSensitivity(TrackBarSensitivity.Value)
    End Sub
    Private Sub txttrackbarx_textchanged(sender As Object, e As EventArgs)
        If Not IsNumeric(TxtTrackBarX.Text) Then Exit Sub
        AdjustRight = TxtTrackBarX.Text
        If ChkBluebox.CheckState = CheckState.Unchecked Then
            ChkBluebox.CheckState = CheckState.Checked
            Call Chkbluebox_Click(Nothing, EventArgs.Empty)
        End If
    End Sub
    Private Sub txttrackbary_textchanged(sender As Object, e As EventArgs)
        If Not IsNumeric(TxtTrackBarY.Text) Then Exit Sub
        AdjustDown = TxtTrackBarY.Text
        If ChkBluebox.CheckState = CheckState.Unchecked Then
            ChkBluebox.CheckState = CheckState.Checked
            Call Chkbluebox_Click(Nothing, EventArgs.Empty)
        End If
    End Sub
 
    Private Sub TrackBarY_Scroll(sender As Object, e As EventArgs)
        If ChkBluebox.CheckState = CheckState.Unchecked Then
            ChkBluebox.CheckState = CheckState.Checked
            Call Chkbluebox_Click(Nothing, EventArgs.Empty)
        End If
 
        AdjustDown = TrackBarY.Value - 25
        TxtTrackBarY.Text = AdjustDown
    End Sub
    Private Sub TrackBarX_Scroll(sender As Object, e As EventArgs)
        If ChkBluebox.CheckState = CheckState.Unchecked Then
            ChkBluebox.CheckState = CheckState.Checked
            Call Chkbluebox_Click(Nothing, EventArgs.Empty)
        End If
 
        AdjustRight = TrackBarX.Value - 50
        TxtTrackBarX.Text = AdjustRight
    End Sub
 
    Private Sub BtnEnable_Click(sender As Object, e As EventArgs)
        If TriggerbotActive = 0 Then
            StartTriggerbox()
        Else
            StopTriggerbox()
        End If
    End Sub
    Public Sub StopTriggerbox()
        BtnEnable.Text = "START"
        TriggerbotActive = 0
    End Sub
    Public Sub StartTriggerbox()
        BtnEnable.Text = "STOP"
        TriggerbotActive = 1
    End Sub
 
    Private Sub Chkbluebox_Click(sender As Object, e As EventArgs)
        If ChkBluebox.CheckState = CheckState.Checked Then
            EnableBlueBox()
        End If
 
    End Sub
 
    Private Sub EnableBlueBox()
        Dim i As Long
        StopTriggerbox()
 
        Application.DoEvents()
        Using g As Graphics = Graphics.FromHwnd(IntPtr.Zero)
            For i = 0 To 2000
                SetScanRange()
                Dim rect As Rectangle = New Rectangle((Screen.PrimaryScreen.Bounds.Width / 2) - (ScanRange / 2) + AdjustRight, (Screen.PrimaryScreen.Bounds.Height / 2) - (ScanRange / 2) + AdjustDown, ScanRange, ScanRange)
                Dim rectalpha As Rectangle = New Rectangle((Screen.PrimaryScreen.Bounds.Width / 2) - (1) + AdjustRight, (Screen.PrimaryScreen.Bounds.Height / 2) - (1) + AdjustDown, 1, 1)
                If i Mod 2 = 1 Then
                    Using lgb As New Drawing2D.LinearGradientBrush(rect, Color.Blue, Color.Blue, 90, True)
                        g.FillRectangle(lgb, rect)
                    End Using
                Else
                    Using lgba As New Drawing2D.LinearGradientBrush(rectalpha, Color.White, Color.White, 90, True)
                        g.FillRectangle(lgba, rectalpha)
                    End Using
                End If
                Application.DoEvents()
                If IsFormClosing = True Then Exit Sub
                If ChkBluebox.CheckState = CheckState.Unchecked Then
                    Me.Refresh()
                    Exit Sub
                End If
            Next i
        End Using
 
        ChkBluebox.CheckState = CheckState.Unchecked
        Me.Refresh()
    End Sub
 
 
End Class