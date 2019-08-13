Imports System.IO
Imports System.Xml
Imports System.Drawing.Imaging
Imports OvaHats.All4Colors
Imports OvaHats.scr
Imports OvaHats.msp

Public Class MainProject 'Overwatch Aim Assist Program "OverHits v7.8.2 Beta 12"
#Region "OverHits Settings"
    Public IsSaved As Boolean = True
    Public IsSetting As Boolean = False

    Public MchLrnWrn As Boolean = True
#End Region

    ''' <summary>
    ''' Overwatch Process Name.
    ''' </summary>
    Private Const overwatch_processName As String = "Overwatch"

#Region "Variable: Pixels"
    Public AntiShakeX As Integer = Math.Floor(SystemInformation.PrimaryMonitorSize.Height / 160)
    Public AntiShakeY As Integer = Math.Floor(SystemInformation.PrimaryMonitorSize.Height / 128)
    Public ZeroX As Integer = Math.Floor(SystemInformation.PrimaryMonitorSize.Width / 2)
    Public ZeroY As Integer = Math.Floor(SystemInformation.PrimaryMonitorSize.Height / 2)
    Public CFovX As Integer = Math.Floor(SystemInformation.PrimaryMonitorSize.Width / 8)
    Public CFovY As Integer = Math.Floor(SystemInformation.PrimaryMonitorSize.Height / 64)

    Public ScanL As Integer = ZeroX - CFovX
    Public ScanT As Integer = ZeroY
    Public ScanR As Integer = ZeroX + CFovX
    Public ScanB As Integer = ZeroY + CFovY

    Public NearAimScanL As Integer = ZeroX - AntiShakeX
    Public NearAimScanT As Integer = ZeroY - AntiShakeY
    Public NearAimScanR As Integer = ZeroX + AntiShakeX
    Public NearAimScanB As Integer = ZeroY + AntiShakeY

    Public AimPointL As Integer = SystemInformation.PrimaryMonitorSize.Width - NearAimScanL
    Public AimPointT As Integer = SystemInformation.PrimaryMonitorSize.Height - NearAimScanT
    Public AimPointR As Integer = SystemInformation.PrimaryMonitorSize.Width - NearAimScanR
    Public AimPointB As Integer = SystemInformation.PrimaryMonitorSize.Height - NearAimScanB
#End Region
#Region "Variable: Headshot, Range"
    Public Enum HeadShot
        Headshot
        Bodyshot
    End Enum

    Public Enum HPRange
        r200
        PuryHP
    End Enum
#End Region
#Region "Variable: XML Settings"
    'OverHits v7.8.2 Beta 12

    'Default Settings
    Public Shared ChkUpde As Boolean 'Check Update

    'Advanced Settings
    Public Shared MchLrn As Boolean 'Machine Learning
    Public Shared FstScn As Boolean 'Fast Scan
    Public Shared GENEILPath As AdvancedMode.GENEIL
    Public Shared PuryColorPath As AdvancedMode.PuryColorHP

    'Main Settings
    Public Shared SmthAim As Boolean 'Smooth Aim
    Public Shared FixingPrt As HeadShot 'Fixing Part
    Public Shared LftClk As Boolean 'Left Click
    Public Shared RhtClk As Boolean 'Right Click

    'Variables
    Public Shared LatestVersion As Version
    Public Shared LatestVerExtraInfo As String
    Public Shared LatestVerDescInfo As String
#End Region

    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short

    Public Function ColorSearch() As Boolean
        Try
            Dim a, b As New Stopwatch
            a.Start()

            Dim c As Process() = Process.GetProcessesByName(overwatch_processName)
            If c.Count = 0 Then
                Throw New KeyNotFoundException("Overwatch not found!")
            End If

            Dim s As IntPtr = c(0).MainWindowHandle
            Dim bit As Bitmap = PrintWindow(s)

            For y As Integer = 400 To 700
                For x As Integer = 700 To 1200

                    Dim GetPix As Color = bit.GetPixel(x, y)
                    If IsRed(GetPix, HPRange.r200) Then

                        Dim AimX As Integer = x - ZeroX
                        Dim AimY As Integer = y - ZeroY

                        If FixingPrt = HeadShot.Headshot Then

                            'Headshot.
                            If SmthAim Then
                                Dim m2 As Boolean = MoveToSmooth(New Point(AimX + 30, AimY))
                            Else
                                mouse_event(&H1, AimX + 30, AimY + 50, 0, 0)
                                'MoveLikAimbot(New Point(AimX, AimY + 50))
                                'PostMessage(s, WMessages.WM_MOUSEMOVE, 0, New IntPtr(x + (y * 65536)))
                            End If

                        ElseIf FixingPrt = HeadShot.Bodyshot Then

                            'Bodyshot.
                            If SmthAim Then
                                Dim m2 As Boolean = MoveToSmooth(New Point(AimX, AimY - 50))
                            Else
                                mouse_event(&H1, AimX, AimY - 50, 0, 0)
                            End If

                        End If

                        a.Stop()
                        Debug.WriteLine("True: " & a.ElapsedMilliseconds & "ms")
                        Return True

                    Else
                        Continue For
                    End If

                Next
            Next

            a.Stop()
            Debug.WriteLine("False: " & a.ElapsedMilliseconds & "ms")
            Return False

        Catch ex As Exception
            ColorSearch_Timer.Stop()
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function MchLrn_ColorSearch(TestColors() As Color, i As Integer) As String()
        'We can't sort the Color. I'm so sorry about that.

        Dim th As Integer = TestColors.Count - 1
        Dim returnstr As String() = New String(th) {}

        Dim quiu As Integer = 0 '반복 횟수.
        For Each PuryColor As Color In TestColors

            Dim j As Integer = 0 '1 픽셀서치 성공 횟수.
            Dim q As Integer = 0 '2 픽셀서치 성공 횟수.
            For fi As Integer = 0 To i - 1

                'au3.PixelSearch(900, 450, 1030, 600, All4Colors.ColorToInt(PuryColor), 24)
                'If au3.error = 0 Then
                j += 1

                'au3b.PixelSearch(920, 460, 1010, 510, All4Colors.ColorToInt(PuryColor), 24)
                'If au3b.error = 0 Then
                q += 1
                'End If

                'End If

            Next

            returnstr(quiu) = String.Format("{0}: True({1}), True({2})", PuryColor.ToString, j, q)
            quiu += 1

        Next

        Return returnstr
    End Function

    Private Sub MainProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "NVIDIA GeForce Experience"

        IsSetting = True
#Region "Checking Files"
        If Not File.Exists(Application.StartupPath & "\settings.xml") Then
            MessageBox.Show("'settings.xml' doesn't exists!", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
#End Region

#Region "Debug: Variable"
        Debug.WriteLine("AntiShakeX: " & Math.Floor(SystemInformation.PrimaryMonitorSize.Height / 160))
        Debug.WriteLine("AntiShakeY: " & Math.Floor(SystemInformation.PrimaryMonitorSize.Height / 128))
        Debug.WriteLine("ZeroX: " & Math.Floor(SystemInformation.PrimaryMonitorSize.Width / 2))
        Debug.WriteLine("ZeroY: " & Math.Floor(SystemInformation.PrimaryMonitorSize.Height / 2))
        Debug.WriteLine("CFovX: " & Math.Floor(SystemInformation.PrimaryMonitorSize.Width / 8))
        Debug.WriteLine("CFovY: " & Math.Floor(SystemInformation.PrimaryMonitorSize.Height / 64))

        Debug.WriteLine("ScanL: " & ScanL)
        Debug.WriteLine("ScanT: " & ScanT)
        Debug.WriteLine("ScanR: " & ScanR)
        Debug.WriteLine("ScanB: " & ScanB)

        Debug.WriteLine("NearAimScanL: " & NearAimScanL)
        Debug.WriteLine("NearAimScanT: " & NearAimScanT)
        Debug.WriteLine("NearAimScanR: " & NearAimScanR)
        Debug.WriteLine("NearAimScanB: " & NearAimScanB)

        Debug.WriteLine("AimPointL: " & AimPointL)
        Debug.WriteLine("AimPointT: " & AimPointT)
        Debug.WriteLine("AimPointR: " & AimPointR)
        Debug.WriteLine("AimPointB: " & AimPointB)
#End Region

#Region "Loading XML"
        MainProject_UpdateSettings()
#End Region

#Region "Setting Values"

        'Default Settings

        'Advanced Settings

        'Main Settings
        smth_CheckBox.Checked = SmthAim

        If FixingPrt = HeadShot.Headshot Then
            GroupHead.Checked = True
            GroupBody.Checked = False
        ElseIf FixingPrt = HeadShot.Bodyshot Then
            GroupHead.Checked = False
            GroupBody.Checked = True
        End If
        LeftClickCheck.Checked = LftClk
        RightClickCheck.Checked = RhtClk

        'Variable

#End Region
        IsSaved = True
        IsSetting = False

        START_AIM.Start()
        STOP_AIM.Start()

        'ExtractPlayerAsRed(New Bitmap("hpbar1.png")).Save("hpbar_.png")
    End Sub

    Public Function MainProject_UpdateSettings() As Boolean
        Try

            Dim doc As New XmlDocument
            doc.Load(Application.StartupPath & "\settings.xml")

            'Default Settings
            ChkUpde = Convert.ToBoolean(doc.GetElementsByTagName("ChkUpde")(0).InnerText)

            'Advanced Settings
            MchLrn = Convert.ToBoolean(doc.GetElementsByTagName("MchLrn")(0).InnerText)
            If MchLrn = False Then MchLrnWrn = True
            FstScn = Convert.ToBoolean(doc.GetElementsByTagName("FstScn")(0).InnerText)
            If doc.GetElementsByTagName("GENEILPath")(0).InnerText = "GENEIL" Then
                GENEILPath = AdvancedMode.GENEIL.GENEIL
            ElseIf doc.GetElementsByTagName("GENEILPath")(0).InnerText = "AdvanGo" Then
                GENEILPath = AdvancedMode.GENEIL.AdvanGo
            ElseIf doc.GetElementsByTagName("GENEILPath")(0).InnerText = "CuSeol" Then
                GENEILPath = AdvancedMode.GENEIL.CuSeol
            End If

            'Main Settings
            SmthAim = Convert.ToBoolean(doc.GetElementsByTagName("SmthAim")(0).InnerText)
            If doc.GetElementsByTagName("FixingPrt")(0).InnerText = "Head" Then
                FixingPrt = HeadShot.Headshot
            ElseIf doc.GetElementsByTagName("FixingPrt")(0).InnerText = "Body" Then
                FixingPrt = HeadShot.Bodyshot
            End If
            LftClk = Convert.ToBoolean(doc.GetElementsByTagName("LftClk")(0).InnerText)
            RhtClk = Convert.ToBoolean(doc.GetElementsByTagName("RhtClk")(0).InnerText)

            'Variables
            LatestVersion = Version.Parse(doc.GetElementsByTagName("LatestVersion")(0).InnerText)
            LatestVerExtraInfo = doc.GetElementsByTagName("LatestVerExtraInfo")(0).InnerText
            LatestVerDescInfo = doc.GetElementsByTagName("LatestVerDescInfo")(0).InnerText

            Return True

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub Info1_Click(sender As Object, e As EventArgs) Handles Info1.Click
        Information.Show()
    End Sub

    Private Sub ColorSearch_Timer_Tick(sender As Object, e As EventArgs) Handles ColorSearch_Timer.Tick
        If GetAsyncKeyState(1) AndAlso LftClk Or GetAsyncKeyState(2) AndAlso RhtClk Then
            ColorSearch()
        End If
    End Sub

    Private Sub AdvButton_Click(sender As Object, e As EventArgs) Handles AdvButton.Click
        If MessageBox.Show("Would you like to turn on the 'Advanced Mode'?", String.Format("OverHit {0}: Warning", My.Application.Info.Version.ToString), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            AdvancedMode.Show()
        End If
    End Sub

    Private Sub START_AIM_Tick(sender As Object, e As EventArgs) Handles START_AIM.Tick
        Try
            If GetAsyncKeyState(112) Then

                If MchLrn Then
                    If MchLrnWrn Then
                        If MessageBox.Show("Would you like to enable 'Machine Learning'? If you enable it, Aimbot won't work.", Me.Text & ": Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            MchLrnWrn = False
                            srt_aim.ForeColor = Color.Blue
                            MchLrn_Timer.Start()
                        End If
                    Else
                        srt_aim.ForeColor = Color.Blue
                        MchLrn_Timer.Start()
                    End If
                    Exit Sub
                End If

                srt_aim.ForeColor = Color.Blue

                'PixelSearch.
                Dim c As Process() = Process.GetProcessesByName(overwatch_processName)
                If c.Count = 0 Then
                    Throw New KeyNotFoundException("Overwatch not found!")
                End If

                Dim s As IntPtr = c(0).MainWindowHandle
                If s.ToInt32 = 0 Then
                    Throw New KeyNotFoundException("Overwatch not found!")
                Else
                    PrintWindow(s).Save("capture.png", ImageFormat.Png)
                End If
                ColorSearch_Timer.Start()
            End If
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub STOP_AIM_Tick(sender As Object, e As EventArgs) Handles STOP_AIM.Tick
        Try
            If GetAsyncKeyState(113) AndAlso srt_aim.ForeColor = Color.Blue Then
                srt_aim.ForeColor = Color.Black

                'PixelSearch Stop.
                ColorSearch_Timer.Stop()
            End If
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MchLrn_Timer_Tick(sender As Object, e As EventArgs) Handles MchLrn_Timer.Tick
        If GetAsyncKeyState(1) Then
            For Each mchLrnstr As String In MchLrn_ColorSearch(All4Colors.Pury2_N1, 5)
                Debug.WriteLine(mchLrnstr)
            Next
        End If
    End Sub

    Private Sub GroupHeadshot_CheckedChanged(sender As Object, e As EventArgs) Handles GroupHead.CheckedChanged
        Try

            If IsSetting = False AndAlso GroupHead.Checked Then
                Dim setNode As New XmlDocument
                Dim setaNode As XmlNode
                setNode.Load(Application.StartupPath & "\settings.xml")
                setaNode = setNode.SelectSingleNode("/OverHits/Settings/Main-Settings")

                setaNode.ChildNodes(1).InnerText = "Head"
                setNode.Save(Application.StartupPath & "\settings.xml")

                MainProject_UpdateSettings()
                IsSaved = True
            End If

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GroupBodyshot_CheckedChanged(sender As Object, e As EventArgs) Handles GroupBody.CheckedChanged
        Try

            If IsSetting = False AndAlso GroupBody.Checked Then
                Dim setNode As New XmlDocument
                Dim setaNode As XmlNode
                setNode.Load(Application.StartupPath & "\settings.xml")
                setaNode = setNode.SelectSingleNode("/OverHits/Settings/Main-Settings")

                setaNode.ChildNodes(1).InnerText = "Body"
                setNode.Save(Application.StartupPath & "\settings.xml")

                MainProject_UpdateSettings()
                IsSaved = True
            End If

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Smth_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles smth_CheckBox.CheckedChanged
        Try

            If IsSetting = False Then
                Dim setNode As New XmlDocument
                Dim setaNode As XmlNode
                setNode.Load(Application.StartupPath & "\settings.xml")
                setaNode = setNode.SelectSingleNode("/OverHits/Settings/Main-Settings")

                If smth_CheckBox.Checked Then
                    setaNode.ChildNodes(0).InnerText = "True"
                Else
                    setaNode.ChildNodes(0).InnerText = "False"
                End If

                setNode.Save(Application.StartupPath & "\settings.xml")

                MainProject_UpdateSettings()
                IsSaved = True
            End If

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LeftClickCheck_CheckedChanged(sender As Object, e As EventArgs) Handles LeftClickCheck.CheckedChanged
        Try

            If IsSetting = False Then
                Dim setNode As New XmlDocument
                Dim setaNode As XmlNode
                setNode.Load(Application.StartupPath & "\settings.xml")
                setaNode = setNode.SelectSingleNode("/OverHits/Settings/Main-Settings")

                If LeftClickCheck.Checked Then
                    setaNode.ChildNodes(2).InnerText = "True"
                Else
                    setaNode.ChildNodes(2).InnerText = "False"
                End If

                setNode.Save(Application.StartupPath & "\settings.xml")

                MainProject_UpdateSettings()
                IsSaved = True
            End If

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RightClickCheck_CheckedChanged(sender As Object, e As EventArgs) Handles RightClickCheck.CheckedChanged
        Try

            If IsSetting = False Then
                Dim setNode As New XmlDocument
                Dim setaNode As XmlNode
                setNode.Load(Application.StartupPath & "\settings.xml")
                setaNode = setNode.SelectSingleNode("/OverHits/Settings/Main-Settings")

                If LeftClickCheck.Checked Then
                    setaNode.ChildNodes(3).InnerText = "True"
                Else
                    setaNode.ChildNodes(3).InnerText = "False"
                End If

                setNode.Save(Application.StartupPath & "\settings.xml")

                MainProject_UpdateSettings()
                IsSaved = True
            End If

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
