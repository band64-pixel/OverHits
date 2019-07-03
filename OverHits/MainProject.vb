Imports System.IO
Imports System.Xml
Imports AutoItX3Lib

Public Class MainProject '와 미친.... 내가 살다살다 오버워치 에임핵은 처음 만들어본다 ㄷㄷ

    Public IsSaved As Boolean = True
    Public IsSetting As Boolean = False

    Public MchLrnWrn As Boolean = True

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
#Region "Variable: HeadShot"
    Public Enum HeadShot
        Headshot
        Bodyshot
    End Enum
#End Region
#Region "Variable: XML Settings"
    'OverHits 1.0.0.0 Beta 10

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

    Declare Function mouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dX As Int32, ByVal dY As Int32, ByVal cButtons As Int32, ByVal dwExtraInfo As Int32) As Boolean
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short

    Public Function ColorSearch() As Boolean
        Try
            Dim a, b As New Stopwatch
            Dim au3, au3b As New AutoItX3
            a.Start()

            '저번까지만 해도 나는 Visual Basic .NET에서 직접 픽셀서치 구현을 노가다로 해야했는데 ㅠㅠ
            'AutoIt으로 픽셀서치 한방으로 하니 편-안!!
            For Each PuryColor As Color In All4Colors.Pury2_N1
                au3.PixelSearch(900, 450, 1030, 600, All4Colors.ColorToInt(PuryColor), 24)
                If au3.error = 0 Then

                    Do 'The Second of AimSearch!
                        b.Start()
                        Dim AimPixels As Object = au3b.PixelSearch(920, 460, 1010, 510, All4Colors.ColorToInt(PuryColor), 24)
                        If au3b.error = 0 Then

                            Dim AimX As Integer = AimPixels(0) - ZeroX
                            Dim AimY As Integer = AimPixels(1) - ZeroY
                            Dim DirX As Integer = -1
                            Dim DirY As Integer = -1

                            If AimX > 0 Then
                                DirX = 1
                            End If
                            If AimY > 0 Then
                                DirY = 1
                            End If

                            Dim AimOffsetX As Integer = AimX * DirX
                            Dim AimOffsetY As Integer = AimY * DirY

                            If FixingPrt = HeadShot.Headshot Then

                                'Headshot.
                                Dim ran As New Random
                                Dim MoveX As Integer = Math.Floor(Math.Sqrt(AimOffsetX))
                                Dim MoveY As Integer = Math.Floor(Math.Sqrt(AimOffsetY)) + ran.Next(40, 50)

                                mouse_event(&H1, MoveX, MoveY, 0, 0)
                                For i As Integer = ran.Next(1, 3) To ran.Next(5, 7)
                                    Dim rdu As New Random
                                    Dim pm As Boolean = Convert.ToBoolean(rdu.Next(0, 10))

                                    If pm Then
                                        For q As Integer = 0 To i - 1
                                            mouse_event(&H1, 0, -i, 0, 0)
                                        Next
                                    Else
                                        For q As Integer = 0 To i - 1
                                            mouse_event(&H1, 0, i, 0, 0)
                                        Next
                                    End If
                                Next

                            ElseIf FixingPrt = HeadShot.Bodyshot Then

                                'Bodyshot.
                                Dim ran As New Random
                                Dim MoveX As Integer = Math.Floor(Math.Sqrt(AimOffsetX))
                                Dim MoveY As Integer = Math.Floor(Math.Sqrt(AimOffsetY)) + ran.Next(70, 80)

                                mouse_event(&H1, MoveX, MoveY, 0, 0)
                                For i As Integer = ran.Next(1, 5) To ran.Next(7, 10)
                                    Dim rdu As New Random
                                    Dim pm As Boolean = Convert.ToBoolean(rdu.Next(0, 10))

                                    If pm Then
                                        For q As Integer = 0 To i - 1
                                            mouse_event(&H1, 0, -q, 0, 0)
                                        Next
                                    Else
                                        For q As Integer = 0 To i - 1
                                            mouse_event(&H1, 0, q, 0, 0)
                                        Next
                                    End If
                                Next

                            End If

                            b.Stop()
                            Debug.WriteLine("True: (" & AimPixels(0) & ", " & AimPixels(1) & "), " & b.ElapsedMilliseconds & "ms")

                        Else
                            b.Stop()
                            Debug.WriteLine("False: " & b.ElapsedMilliseconds & "ms")
                        End If
                    Loop Until 10

                    a.Stop()
                    Debug.WriteLine("True: " & a.ElapsedMilliseconds & "ms")

                    Return True

                Else
                    Continue For

                    a.Stop()
                    Debug.WriteLine("False: " & a.ElapsedMilliseconds & "ms")
                End If
            Next

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
                Dim au3, au3b As New AutoItX3

                au3.PixelSearch(900, 450, 1030, 600, All4Colors.ColorToInt(PuryColor), 24)
                If au3.error = 0 Then
                    j += 1

                    au3b.PixelSearch(920, 460, 1010, 510, All4Colors.ColorToInt(PuryColor), 24)
                    If au3b.error = 0 Then
                        q += 1
                    End If

                End If

            Next

            returnstr(quiu) = String.Format("{0}: True({1}), True({2})", PuryColor.ToString, j, q)
            quiu += 1

        Next

        Return returnstr
    End Function

    Private Sub MainProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = String.Format("OverHit {0}: Main", My.Application.Info.Version.ToString) 'OverHit Version Load.

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
        If GetAsyncKeyState(1) AndAlso LftClk Then
            ColorSearch()
        End If

        If GetAsyncKeyState(2) AndAlso RhtClk Then
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

Public Class All4Colors
    '구현 방식: OverHits: MainProject에서 스크린샷 bmp / 스크린샷 픽셀 색을 주면 색 데이터들로 스캔을 해서 스캔이 True일 때는 그 색을 Return함.

#Region "Scanning Colors 1"
    Public Shared ReadOnly Pury1_N1 As Color() = New Color(28) {Color.FromArgb(216, 42, 34), Color.FromArgb(255, 0, 19),
        Color.FromArgb(238, 64, 38), Color.FromArgb(233, 68, 44), Color.FromArgb(235, 69, 42),
        Color.FromArgb(234, 68, 41), Color.FromArgb(234, 75, 49), Color.FromArgb(235, 67, 43),
        Color.FromArgb(232, 66, 43), Color.FromArgb(241, 82, 56), Color.FromArgb(231, 73, 49),
        Color.FromArgb(235, 57, 26), Color.FromArgb(230, 46, 19), Color.FromArgb(209, 58, 31),
        Color.FromArgb(238, 52, 23), Color.FromArgb(230, 44, 20), Color.FromArgb(211, 48, 26),
        Color.FromArgb(231, 44, 20), Color.FromArgb(222, 52, 30), Color.FromArgb(224, 57, 32),
        Color.FromArgb(232, 54, 27), Color.FromArgb(227, 57, 31), Color.FromArgb(244, 55, 25),
        Color.FromArgb(243, 54, 24), Color.FromArgb(230, 45, 21), Color.FromArgb(230, 44, 34),
        Color.FromArgb(241, 53, 23), Color.FromArgb(236, 51, 23), Color.FromArgb(234, 60, 48)}

    Public Shared ReadOnly Pury1_N2 As Color() = New Color(28) {Color.FromArgb(216, 42, 34), Color.FromArgb(0, 255, 19),
        Color.FromArgb(222, 71, 56), Color.FromArgb(240, 86, 60), Color.FromArgb(240, 86, 60),
        Color.FromArgb(227, 72, 56), Color.FromArgb(238, 88, 63), Color.FromArgb(235, 34, 62),
        Color.FromArgb(235, 33, 62), Color.FromArgb(238, 38, 64), Color.FromArgb(237, 38, 64),
        Color.FromArgb(240, 40, 65), Color.FromArgb(233, 31, 60), Color.FromArgb(225, 91, 75),
        Color.FromArgb(208, 82, 72), Color.FromArgb(219, 69, 55), Color.FromArgb(228, 46, 71),
        Color.FromArgb(230, 34, 64), Color.FromArgb(234, 88, 71), Color.FromArgb(225, 70, 55),
        Color.FromArgb(246, 58, 28), Color.FromArgb(203, 59, 41), Color.FromArgb(218, 75, 45),
        Color.FromArgb(221, 48, 27), Color.FromArgb(216, 44, 25), Color.FromArgb(234, 47, 22),
        Color.FromArgb(222, 48, 30), Color.FromArgb(227, 46, 26), Color.FromArgb(243, 55, 28)}
#End Region
#Region "MchLrn 1"
    Public Shared ReadOnly Pury2_N1 As Color() = New Color(180) {Color.FromArgb(216, 42, 34), Color.FromArgb(227, 72, 56),
        Color.FromArgb(225, 70, 55), Color.FromArgb(246, 58, 28), Color.FromArgb(203, 59, 41), Color.FromArgb(218, 75, 45),
        Color.FromArgb(221, 48, 27), Color.FromArgb(216, 44, 25), Color.FromArgb(234, 47, 22), Color.FromArgb(222, 48, 30),
Color.FromArgb(227, 46, 26), Color.FromArgb(243, 55, 28), Color.FromArgb(216, 42, 34), Color.FromArgb(222, 71, 56),
Color.FromArgb(208, 82, 72), Color.FromArgb(219, 69, 55), Color.FromArgb(228, 46, 71), Color.FromArgb(230, 34, 64),
Color.FromArgb(234, 88, 71), Color.FromArgb(225, 70, 55), Color.FromArgb(203, 59, 41), Color.FromArgb(218, 75, 45),
Color.FromArgb(234, 47, 22), Color.FromArgb(222, 48, 30), Color.FromArgb(227, 46, 26), Color.FromArgb(243, 55, 28),
Color.FromArgb(216, 42, 34), Color.FromArgb(222, 71, 56), Color.FromArgb(240, 86, 60), Color.FromArgb(227, 72, 56),
Color.FromArgb(238, 88, 63), Color.FromArgb(235, 34, 62), Color.FromArgb(235, 33, 62), Color.FromArgb(238, 38, 64),
Color.FromArgb(237, 38, 64), Color.FromArgb(240, 40, 65), Color.FromArgb(225, 91, 75), Color.FromArgb(208, 82, 72),
Color.FromArgb(219, 69, 55), Color.FromArgb(228, 46, 71), Color.FromArgb(230, 34, 64), Color.FromArgb(234, 88, 71),
Color.FromArgb(225, 70, 55), Color.FromArgb(246, 58, 28), Color.FromArgb(203, 59, 41), Color.FromArgb(218, 75, 45),
Color.FromArgb(221, 48, 27), Color.FromArgb(216, 44, 25), Color.FromArgb(234, 47, 22), Color.FromArgb(222, 48, 30),
Color.FromArgb(227, 46, 26), Color.FromArgb(243, 55, 28), Color.FromArgb(216, 42, 34), Color.FromArgb(222, 71, 56),
Color.FromArgb(240, 86, 60), Color.FromArgb(227, 72, 56), Color.FromArgb(238, 88, 63), Color.FromArgb(235, 34, 62),
Color.FromArgb(235, 33, 62), Color.FromArgb(238, 38, 64), Color.FromArgb(237, 38, 64), Color.FromArgb(240, 40, 65),
Color.FromArgb(233, 31, 60), Color.FromArgb(225, 91, 75), Color.FromArgb(208, 82, 72), Color.FromArgb(219, 69, 55),
Color.FromArgb(228, 46, 71), Color.FromArgb(230, 34, 64), Color.FromArgb(234, 88, 71), Color.FromArgb(225, 70, 55),
Color.FromArgb(246, 58, 28), Color.FromArgb(203, 59, 41), Color.FromArgb(218, 75, 45), Color.FromArgb(221, 48, 27),
Color.FromArgb(216, 44, 25), Color.FromArgb(234, 47, 22), Color.FromArgb(222, 48, 30), Color.FromArgb(227, 46, 26),
Color.FromArgb(243, 55, 28), Color.FromArgb(216, 42, 34), Color.FromArgb(222, 71, 56), Color.FromArgb(240, 86, 60),
Color.FromArgb(227, 72, 56), Color.FromArgb(238, 88, 63), Color.FromArgb(235, 34, 62), Color.FromArgb(235, 33, 62),
Color.FromArgb(238, 38, 64), Color.FromArgb(237, 38, 64), Color.FromArgb(240, 40, 65), Color.FromArgb(233, 31, 60),
Color.FromArgb(225, 91, 75), Color.FromArgb(208, 82, 72), Color.FromArgb(219, 69, 55), Color.FromArgb(228, 46, 71),
Color.FromArgb(230, 34, 64), Color.FromArgb(234, 88, 71), Color.FromArgb(225, 70, 55), Color.FromArgb(203, 59, 41),
Color.FromArgb(218, 75, 45), Color.FromArgb(221, 48, 27), Color.FromArgb(216, 44, 25), Color.FromArgb(234, 47, 22),
Color.FromArgb(222, 48, 30), Color.FromArgb(227, 46, 26), Color.FromArgb(243, 55, 28), Color.FromArgb(216, 42, 34),
Color.FromArgb(222, 71, 56), Color.FromArgb(240, 86, 60), Color.FromArgb(240, 86, 60), Color.FromArgb(227, 72, 56),
Color.FromArgb(238, 88, 63), Color.FromArgb(235, 34, 62), Color.FromArgb(235, 33, 62), Color.FromArgb(238, 38, 64),
Color.FromArgb(237, 38, 64), Color.FromArgb(240, 40, 65), Color.FromArgb(233, 31, 60), Color.FromArgb(225, 91, 75),
Color.FromArgb(208, 82, 72), Color.FromArgb(219, 69, 55), Color.FromArgb(228, 46, 71), Color.FromArgb(230, 34, 64),
Color.FromArgb(234, 88, 71), Color.FromArgb(225, 70, 55), Color.FromArgb(246, 58, 28), Color.FromArgb(203, 59, 41),
Color.FromArgb(218, 75, 45), Color.FromArgb(221, 48, 27), Color.FromArgb(216, 44, 25), Color.FromArgb(234, 47, 22),
Color.FromArgb(222, 48, 30), Color.FromArgb(227, 46, 26), Color.FromArgb(243, 55, 28), Color.FromArgb(216, 42, 34),
Color.FromArgb(222, 71, 56), Color.FromArgb(240, 86, 60), Color.FromArgb(240, 86, 60), Color.FromArgb(227, 72, 56),
Color.FromArgb(238, 38, 64), Color.FromArgb(237, 38, 64), Color.FromArgb(240, 40, 65), Color.FromArgb(225, 91, 75),
Color.FromArgb(208, 82, 72), Color.FromArgb(219, 69, 55), Color.FromArgb(228, 46, 71), Color.FromArgb(230, 34, 64),
Color.FromArgb(234, 88, 71), Color.FromArgb(225, 70, 55), Color.FromArgb(246, 58, 28), Color.FromArgb(203, 59, 41),
Color.FromArgb(218, 75, 45), Color.FromArgb(221, 48, 27), Color.FromArgb(216, 44, 25), Color.FromArgb(234, 47, 22),
Color.FromArgb(222, 48, 30), Color.FromArgb(227, 46, 26), Color.FromArgb(243, 55, 28), Color.FromArgb(216, 42, 34),
Color.FromArgb(222, 71, 56), Color.FromArgb(225, 91, 75), Color.FromArgb(208, 82, 72), Color.FromArgb(219, 69, 55),
Color.FromArgb(246, 58, 28), Color.FromArgb(203, 59, 41), Color.FromArgb(218, 75, 45), Color.FromArgb(221, 48, 27),
Color.FromArgb(222, 48, 30), Color.FromArgb(227, 46, 26), Color.FromArgb(243, 55, 28), Color.FromArgb(216, 42, 34),
Color.FromArgb(222, 71, 56), Color.FromArgb(227, 72, 56), Color.FromArgb(225, 91, 75), Color.FromArgb(208, 82, 72),
Color.FromArgb(218, 75, 45), Color.FromArgb(221, 48, 27), Color.FromArgb(216, 44, 25), Color.FromArgb(234, 47, 22),
Color.FromArgb(222, 48, 30), Color.FromArgb(227, 46, 26), Color.FromArgb(243, 55, 28)}
#End Region
    Public Sub New()
    End Sub
    Public Shared Function ColorToInt(Color As Color) As Integer
        Return 65536 * Color.R + 256 * Color.G + Color.B
    End Function

    Public Shared Function IntToColor(ColorInt As Integer) As Color
        Dim A As Byte = (ColorInt >> 24) And 255
        Dim R As Byte = (ColorInt >> 16) And 255
        Dim G As Byte = (ColorInt >> 8) And 255
        Dim B As Byte = ColorInt And 255

        Return Color.FromArgb(A, R, G, B)
    End Function
End Class
