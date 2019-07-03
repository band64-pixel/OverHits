Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO

Public Class MainProject
    Public userRoot As String = Environment.GetEnvironmentVariable("USERPROFILE")
    Public screenshotsText As String

    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short

    Private Sub MainProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("ScreenCapture {0}: {1} - {2}", My.Application.Info.Version.ToString, "YOLO Editon", "Xeon64")

        FolderDir.Text = String.Format("{0}\Desktop", userRoot)

        X1Text.Text = SystemInformation.PrimaryMonitorSize.Width
        Y1Text.Text = SystemInformation.PrimaryMonitorSize.Height
        X2Text.Text = 0
        Y2Text.Text = 0
        CaptureResolution_Check(False)
    End Sub
    Public Function ScreenCapture() As String
        Dim screenshots As Bitmap
        Dim graph As Graphics
        If CheckButton1.Checked Then 'CheckButton 활성화시

            Dim num As Integer 'Screen's Resolution Check !
            If Not Integer.TryParse(X1Text.Text, num) OrElse Convert.ToInt32(X1Text.Text) < 0 OrElse Not Integer.TryParse(Y1Text.Text, num) OrElse Convert.ToInt32(Y1Text.Text) < 0 OrElse Not Integer.TryParse(X2Text.Text, num) OrElse Convert.ToInt32(X2Text.Text) < 0 OrElse Not Integer.TryParse(Y2Text.Text, num) OrElse Convert.ToInt32(Y2Text.Text) < 0 Then
                Throw New Exception("We can't capture the screen becuz of The Screen's Resolution.")
            End If

            Dim sz As New Point(Convert.ToInt32(X1Text.Text), Convert.ToInt32(Y1Text.Text))
            screenshots = New Bitmap(Convert.ToInt32(X2Text.Text) - Convert.ToInt32(X1Text.Text), Convert.ToInt32(Y2Text.Text) - Convert.ToInt32(Y1Text.Text), PixelFormat.Format32bppArgb)
            graph = Graphics.FromImage(screenshots)
            graph.CompositingQuality = CompositingQuality.HighQuality
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic
            graph.SmoothingMode = SmoothingMode.HighQuality
            graph.CopyFromScreen(Convert.ToInt32(X1Text.Text), Convert.ToInt32(Y1Text.Text), 0, 0, sz, CopyPixelOperation.SourceCopy)

        Else 'NO!

            screenshots = New Bitmap(SystemInformation.PrimaryMonitorSize.Width, SystemInformation.PrimaryMonitorSize.Height, PixelFormat.Format32bppArgb)
            graph = Graphics.FromImage(screenshots)
            graph.CompositingQuality = CompositingQuality.HighQuality
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic
            graph.SmoothingMode = SmoothingMode.HighQuality
            graph.CopyFromScreen(0, 0, 0, 0, SystemInformation.PrimaryMonitorSize, CopyPixelOperation.SourceCopy)

        End If

        screenshotsText = NameText.Text
        If screenshotsText.Contains("yyyy-MM-dd-hh-mm-ss") Then
            screenshotsText = screenshotsText.Replace("yyyy-MM-dd-hh-mm-ss", Date.Now.ToString("yyyy-MM-dd-hh-mm-ss"))
        End If
        Debug.WriteLine(screenshotsText)

        screenshots.Save(String.Format("{0}\{1}.png", FolderDir.Text, screenshotsText), ImageFormat.Png)

        Return screenshotsText
    End Function

    Public Sub CaptureResolution_Check(Enabled As Boolean)
        If Enabled Then
            X1Text.Enabled = True
            Y1Text.Enabled = True
            X2Text.Enabled = True
            Y2Text.Enabled = True
        ElseIf Enabled = False Then
            X1Text.Enabled = False
            Y1Text.Enabled = False
            X2Text.Enabled = False
            Y2Text.Enabled = False
        End If
    End Sub
    Private Sub PauseButton_Click(sender As Object, e As EventArgs) Handles PauseButton.Click
        If ProcBar.Text = "None" OrElse ProcBar.Text = "Paused" Then
            ProcBar.Text = "Capturing"
            ProcBar.Left = 320
            ProcBar.ForeColor = Color.Blue

            If TERMButton.Checked Then
                TermTimer.Start()
            Else
                CaptureTimer.Start()
            End If

            Exit Sub
        End If

        If ProcBar.Text = "Capturing" Then
            ProcBar.Text = "Paused"
            ProcBar.Left = 345
            ProcBar.ForeColor = Color.Red

            If TermTimer.Enabled Then
                TermTimer.Stop()
            Else
                CaptureTimer.Stop()
            End If

            Exit Sub
        End If
    End Sub

    Private Sub CaptureTimer_Tick(sender As Object, e As EventArgs) Handles CaptureTimer.Tick
        Try
            If GetAsyncKeyState(44) Then
                ScreenCapture()
            End If
        Catch ex As Exception
            CaptureTimer.Stop()
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DirButton_Click(sender As Object, e As EventArgs) Handles DirButton.Click
        If Not String.IsNullOrWhiteSpace(FolderDir.Text) OrElse Not Directory.Exists(FolderDir.Text) Then
            FBD.SelectedPath = FolderDir.Text
        Else
            MessageBox.Show("Folder Directory doesn't exists! Please Try Again.", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If FBD.ShowDialog() = DialogResult.OK Then
            FolderDir.Text = FBD.SelectedPath
        End If
    End Sub

    Private Sub CheckButton1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckButton1.CheckedChanged
        CaptureResolution_Check(CheckButton1.Checked)
    End Sub

    Private Sub TermTimer_Tick(sender As Object, e As EventArgs) Handles TermTimer.Tick
        '조건 자유!

        Try

            '조건 1: 마우스 왼쪽 클릭시 캡쳐
            If GetAsyncKeyState(1) OrElse GetAsyncKeyState(44) Then
                ScreenCapture()
            End If

        Catch ex As Exception
            CaptureTimer.Stop()
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
