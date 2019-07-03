<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainProject
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.FolderDir = New System.Windows.Forms.TextBox()
        Me.NameText = New System.Windows.Forms.TextBox()
        Me.DirButton = New System.Windows.Forms.Button()
        Me.PauseButton = New System.Windows.Forms.Button()
        Me.ProcBar = New System.Windows.Forms.Label()
        Me.CaptureTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FBD = New System.Windows.Forms.FolderBrowserDialog()
        Me.CheckButton1 = New System.Windows.Forms.CheckBox()
        Me.X1Text = New System.Windows.Forms.TextBox()
        Me.Y1Text = New System.Windows.Forms.TextBox()
        Me.X2Text = New System.Windows.Forms.TextBox()
        Me.Y2Text = New System.Windows.Forms.TextBox()
        Me.X1Label = New System.Windows.Forms.Label()
        Me.CapRes_Group = New System.Windows.Forms.GroupBox()
        Me.TERMButton = New System.Windows.Forms.CheckBox()
        Me.Y2Label = New System.Windows.Forms.Label()
        Me.X2Label = New System.Windows.Forms.Label()
        Me.Y1Label = New System.Windows.Forms.Label()
        Me.TermTimer = New System.Windows.Forms.Timer(Me.components)
        Me.CapRes_Group.SuspendLayout()
        Me.SuspendLayout()
        '
        'FolderDir
        '
        Me.FolderDir.Location = New System.Drawing.Point(12, 12)
        Me.FolderDir.Name = "FolderDir"
        Me.FolderDir.Size = New System.Drawing.Size(358, 21)
        Me.FolderDir.TabIndex = 0
        Me.FolderDir.Text = "C:\"
        '
        'NameText
        '
        Me.NameText.Location = New System.Drawing.Point(12, 54)
        Me.NameText.Name = "NameText"
        Me.NameText.Size = New System.Drawing.Size(396, 21)
        Me.NameText.TabIndex = 1
        Me.NameText.Text = "Overwatch_yyyy-MM-dd-hh-mm-ss"
        '
        'DirButton
        '
        Me.DirButton.Location = New System.Drawing.Point(376, 10)
        Me.DirButton.Name = "DirButton"
        Me.DirButton.Size = New System.Drawing.Size(32, 23)
        Me.DirButton.TabIndex = 2
        Me.DirButton.Text = "..."
        Me.DirButton.UseVisualStyleBackColor = True
        '
        'PauseButton
        '
        Me.PauseButton.Font = New System.Drawing.Font("NewJumja", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PauseButton.Location = New System.Drawing.Point(119, 170)
        Me.PauseButton.Name = "PauseButton"
        Me.PauseButton.Size = New System.Drawing.Size(177, 96)
        Me.PauseButton.TabIndex = 3
        Me.PauseButton.Text = "START!"
        Me.PauseButton.UseVisualStyleBackColor = True
        '
        'ProcBar
        '
        Me.ProcBar.AutoSize = True
        Me.ProcBar.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ProcBar.Location = New System.Drawing.Point(352, 271)
        Me.ProcBar.Name = "ProcBar"
        Me.ProcBar.Size = New System.Drawing.Size(56, 22)
        Me.ProcBar.TabIndex = 4
        Me.ProcBar.Text = "None"
        '
        'CaptureTimer
        '
        '
        'FBD
        '
        Me.FBD.Description = "Please Select Folder To Capture"
        '
        'CheckButton1
        '
        Me.CheckButton1.AutoSize = True
        Me.CheckButton1.Location = New System.Drawing.Point(6, 19)
        Me.CheckButton1.Name = "CheckButton1"
        Me.CheckButton1.Size = New System.Drawing.Size(68, 16)
        Me.CheckButton1.TabIndex = 5
        Me.CheckButton1.Text = "Check !"
        Me.CheckButton1.UseVisualStyleBackColor = True
        '
        'X1Text
        '
        Me.X1Text.Location = New System.Drawing.Point(56, 41)
        Me.X1Text.Name = "X1Text"
        Me.X1Text.Size = New System.Drawing.Size(33, 21)
        Me.X1Text.TabIndex = 6
        '
        'Y1Text
        '
        Me.Y1Text.Location = New System.Drawing.Point(148, 41)
        Me.Y1Text.Name = "Y1Text"
        Me.Y1Text.Size = New System.Drawing.Size(33, 21)
        Me.Y1Text.TabIndex = 7
        '
        'X2Text
        '
        Me.X2Text.Location = New System.Drawing.Point(247, 41)
        Me.X2Text.Name = "X2Text"
        Me.X2Text.Size = New System.Drawing.Size(33, 21)
        Me.X2Text.TabIndex = 8
        '
        'Y2Text
        '
        Me.Y2Text.Location = New System.Drawing.Point(344, 41)
        Me.Y2Text.Name = "Y2Text"
        Me.Y2Text.Size = New System.Drawing.Size(33, 21)
        Me.Y2Text.TabIndex = 9
        '
        'X1Label
        '
        Me.X1Label.AutoSize = True
        Me.X1Label.Location = New System.Drawing.Point(13, 44)
        Me.X1Label.Name = "X1Label"
        Me.X1Label.Size = New System.Drawing.Size(37, 12)
        Me.X1Label.TabIndex = 10
        Me.X1Label.Text = "X - 1:"
        '
        'CapRes_Group
        '
        Me.CapRes_Group.Controls.Add(Me.TERMButton)
        Me.CapRes_Group.Controls.Add(Me.Y2Label)
        Me.CapRes_Group.Controls.Add(Me.X2Label)
        Me.CapRes_Group.Controls.Add(Me.Y1Label)
        Me.CapRes_Group.Controls.Add(Me.CheckButton1)
        Me.CapRes_Group.Controls.Add(Me.X1Label)
        Me.CapRes_Group.Controls.Add(Me.X1Text)
        Me.CapRes_Group.Controls.Add(Me.Y2Text)
        Me.CapRes_Group.Controls.Add(Me.Y1Text)
        Me.CapRes_Group.Controls.Add(Me.X2Text)
        Me.CapRes_Group.Location = New System.Drawing.Point(12, 81)
        Me.CapRes_Group.Name = "CapRes_Group"
        Me.CapRes_Group.Size = New System.Drawing.Size(396, 74)
        Me.CapRes_Group.TabIndex = 11
        Me.CapRes_Group.TabStop = False
        Me.CapRes_Group.Text = "Capture Resolution"
        '
        'TERMButton
        '
        Me.TERMButton.AutoSize = True
        Me.TERMButton.Location = New System.Drawing.Point(80, 19)
        Me.TERMButton.Name = "TERMButton"
        Me.TERMButton.Size = New System.Drawing.Size(75, 16)
        Me.TERMButton.TabIndex = 14
        Me.TERMButton.Text = "TERM :D"
        Me.TERMButton.UseVisualStyleBackColor = True
        '
        'Y2Label
        '
        Me.Y2Label.AutoSize = True
        Me.Y2Label.Location = New System.Drawing.Point(305, 44)
        Me.Y2Label.Name = "Y2Label"
        Me.Y2Label.Size = New System.Drawing.Size(33, 12)
        Me.Y2Label.TabIndex = 13
        Me.Y2Label.Text = "Y -2:"
        '
        'X2Label
        '
        Me.X2Label.AutoSize = True
        Me.X2Label.Location = New System.Drawing.Point(204, 44)
        Me.X2Label.Name = "X2Label"
        Me.X2Label.Size = New System.Drawing.Size(37, 12)
        Me.X2Label.TabIndex = 12
        Me.X2Label.Text = "X - 2:"
        '
        'Y1Label
        '
        Me.Y1Label.AutoSize = True
        Me.Y1Label.Location = New System.Drawing.Point(105, 44)
        Me.Y1Label.Name = "Y1Label"
        Me.Y1Label.Size = New System.Drawing.Size(37, 12)
        Me.Y1Label.TabIndex = 11
        Me.Y1Label.Text = "Y - 1:"
        '
        'TermTimer
        '
        '
        'MainProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 302)
        Me.Controls.Add(Me.CapRes_Group)
        Me.Controls.Add(Me.ProcBar)
        Me.Controls.Add(Me.PauseButton)
        Me.Controls.Add(Me.DirButton)
        Me.Controls.Add(Me.NameText)
        Me.Controls.Add(Me.FolderDir)
        Me.Name = "MainProject"
        Me.Text = "ScreenCapture"
        Me.CapRes_Group.ResumeLayout(False)
        Me.CapRes_Group.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FolderDir As TextBox
    Friend WithEvents NameText As TextBox
    Friend WithEvents DirButton As Button
    Friend WithEvents PauseButton As Button
    Friend WithEvents ProcBar As Label
    Friend WithEvents CaptureTimer As Timer
    Friend WithEvents FBD As FolderBrowserDialog
    Friend WithEvents CheckButton1 As CheckBox
    Friend WithEvents X1Text As TextBox
    Friend WithEvents Y1Text As TextBox
    Friend WithEvents X2Text As TextBox
    Friend WithEvents Y2Text As TextBox
    Friend WithEvents X1Label As Label
    Friend WithEvents CapRes_Group As GroupBox
    Friend WithEvents Y2Label As Label
    Friend WithEvents X2Label As Label
    Friend WithEvents Y1Label As Label
    Friend WithEvents TERMButton As CheckBox
    Friend WithEvents TermTimer As Timer
End Class
