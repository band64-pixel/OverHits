<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainProject
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.FixingPrtGroup = New System.Windows.Forms.GroupBox()
        Me.GroupBody = New System.Windows.Forms.RadioButton()
        Me.GroupHead = New System.Windows.Forms.RadioButton()
        Me.srt_aim = New System.Windows.Forms.Label()
        Me.stp_aim = New System.Windows.Forms.Label()
        Me.Info1 = New System.Windows.Forms.Button()
        Me.ColorSearch_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.AdvButton = New System.Windows.Forms.Button()
        Me.START_AIM = New System.Windows.Forms.Timer(Me.components)
        Me.STOP_AIM = New System.Windows.Forms.Timer(Me.components)
        Me.smth_CheckBox = New System.Windows.Forms.CheckBox()
        Me.MchLrn_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.ClickGroup = New System.Windows.Forms.GroupBox()
        Me.RightClickCheck = New System.Windows.Forms.CheckBox()
        Me.LeftClickCheck = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FixingPrtGroup.SuspendLayout()
        Me.ClickGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'FixingPrtGroup
        '
        Me.FixingPrtGroup.Controls.Add(Me.GroupBody)
        Me.FixingPrtGroup.Controls.Add(Me.GroupHead)
        Me.FixingPrtGroup.Font = New System.Drawing.Font("나눔바른고딕OTF", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FixingPrtGroup.Location = New System.Drawing.Point(27, 41)
        Me.FixingPrtGroup.Name = "FixingPrtGroup"
        Me.FixingPrtGroup.Size = New System.Drawing.Size(151, 128)
        Me.FixingPrtGroup.TabIndex = 0
        Me.FixingPrtGroup.TabStop = False
        Me.FixingPrtGroup.Text = "고정 부분"
        '
        'GroupBody
        '
        Me.GroupBody.AutoSize = True
        Me.GroupBody.Location = New System.Drawing.Point(36, 82)
        Me.GroupBody.Name = "GroupBody"
        Me.GroupBody.Size = New System.Drawing.Size(40, 22)
        Me.GroupBody.TabIndex = 1
        Me.GroupBody.Text = "몸"
        Me.GroupBody.UseVisualStyleBackColor = True
        '
        'GroupHead
        '
        Me.GroupHead.AutoSize = True
        Me.GroupHead.Location = New System.Drawing.Point(36, 40)
        Me.GroupHead.Name = "GroupHead"
        Me.GroupHead.Size = New System.Drawing.Size(54, 22)
        Me.GroupHead.TabIndex = 0
        Me.GroupHead.Text = "헤드"
        Me.GroupHead.UseVisualStyleBackColor = True
        '
        'srt_aim
        '
        Me.srt_aim.AutoSize = True
        Me.srt_aim.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.srt_aim.ForeColor = System.Drawing.Color.Black
        Me.srt_aim.Location = New System.Drawing.Point(230, 9)
        Me.srt_aim.Name = "srt_aim"
        Me.srt_aim.Size = New System.Drawing.Size(162, 22)
        Me.srt_aim.TabIndex = 1
        Me.srt_aim.Text = "Start Aimbot [F1]"
        '
        'stp_aim
        '
        Me.stp_aim.AutoSize = True
        Me.stp_aim.Font = New System.Drawing.Font("나눔바른고딕OTF", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.stp_aim.Location = New System.Drawing.Point(258, 41)
        Me.stp_aim.Name = "stp_aim"
        Me.stp_aim.Size = New System.Drawing.Size(133, 18)
        Me.stp_aim.TabIndex = 2
        Me.stp_aim.Text = "Stop Aimbot [F2]"
        '
        'Info1
        '
        Me.Info1.Font = New System.Drawing.Font("나눔바른고딕OTF", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Info1.Location = New System.Drawing.Point(309, 246)
        Me.Info1.Name = "Info1"
        Me.Info1.Size = New System.Drawing.Size(75, 43)
        Me.Info1.TabIndex = 4
        Me.Info1.Text = "Info"
        Me.Info1.UseVisualStyleBackColor = True
        '
        'ColorSearch_Timer
        '
        Me.ColorSearch_Timer.Interval = 30
        '
        'AdvButton
        '
        Me.AdvButton.Font = New System.Drawing.Font("나눔바른고딕OTF", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.AdvButton.Location = New System.Drawing.Point(199, 246)
        Me.AdvButton.Name = "AdvButton"
        Me.AdvButton.Size = New System.Drawing.Size(104, 43)
        Me.AdvButton.TabIndex = 5
        Me.AdvButton.Text = "Advanced"
        Me.AdvButton.UseVisualStyleBackColor = True
        '
        'START_AIM
        '
        '
        'STOP_AIM
        '
        '
        'smth_CheckBox
        '
        Me.smth_CheckBox.AutoSize = True
        Me.smth_CheckBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.smth_CheckBox.Location = New System.Drawing.Point(17, 196)
        Me.smth_CheckBox.Name = "smth_CheckBox"
        Me.smth_CheckBox.Size = New System.Drawing.Size(115, 22)
        Me.smth_CheckBox.TabIndex = 6
        Me.smth_CheckBox.Text = "Smooth Aim"
        Me.smth_CheckBox.UseVisualStyleBackColor = True
        '
        'MchLrn_Timer
        '
        Me.MchLrn_Timer.Interval = 30
        '
        'ClickGroup
        '
        Me.ClickGroup.Controls.Add(Me.RightClickCheck)
        Me.ClickGroup.Controls.Add(Me.LeftClickCheck)
        Me.ClickGroup.Font = New System.Drawing.Font("나눔바른고딕OTF", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ClickGroup.Location = New System.Drawing.Point(12, 224)
        Me.ClickGroup.Name = "ClickGroup"
        Me.ClickGroup.Size = New System.Drawing.Size(166, 65)
        Me.ClickGroup.TabIndex = 7
        Me.ClickGroup.TabStop = False
        Me.ClickGroup.Text = "Click Buttons"
        '
        'RightClickCheck
        '
        Me.RightClickCheck.AutoSize = True
        Me.RightClickCheck.Location = New System.Drawing.Point(5, 42)
        Me.RightClickCheck.Name = "RightClickCheck"
        Me.RightClickCheck.Size = New System.Drawing.Size(156, 22)
        Me.RightClickCheck.TabIndex = 9
        Me.RightClickCheck.Text = "Right Click Button"
        Me.RightClickCheck.UseVisualStyleBackColor = True
        '
        'LeftClickCheck
        '
        Me.LeftClickCheck.AutoSize = True
        Me.LeftClickCheck.Checked = True
        Me.LeftClickCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LeftClickCheck.Location = New System.Drawing.Point(6, 20)
        Me.LeftClickCheck.Name = "LeftClickCheck"
        Me.LeftClickCheck.Size = New System.Drawing.Size(146, 22)
        Me.LeftClickCheck.TabIndex = 8
        Me.LeftClickCheck.Text = "Left Click Button"
        Me.LeftClickCheck.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("나눔바른고딕OTF", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "[Not OvaHats, It's OverHits]"
        '
        'MainProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(397, 301)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ClickGroup)
        Me.Controls.Add(Me.smth_CheckBox)
        Me.Controls.Add(Me.AdvButton)
        Me.Controls.Add(Me.Info1)
        Me.Controls.Add(Me.stp_aim)
        Me.Controls.Add(Me.srt_aim)
        Me.Controls.Add(Me.FixingPrtGroup)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "MainProject"
        Me.Text = "NVIDIA GeForce Experience"
        Me.FixingPrtGroup.ResumeLayout(False)
        Me.FixingPrtGroup.PerformLayout()
        Me.ClickGroup.ResumeLayout(False)
        Me.ClickGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FixingPrtGroup As GroupBox
    Friend WithEvents GroupBody As RadioButton
    Friend WithEvents GroupHead As RadioButton
    Friend WithEvents srt_aim As Label
    Friend WithEvents stp_aim As Label
    Friend WithEvents Info1 As Button
    Friend WithEvents ColorSearch_Timer As Timer
    Friend WithEvents AdvButton As Button
    Friend WithEvents START_AIM As Timer
    Friend WithEvents STOP_AIM As Timer
    Friend WithEvents smth_CheckBox As CheckBox
    Friend WithEvents MchLrn_Timer As Timer
    Friend WithEvents ClickGroup As GroupBox
    Friend WithEvents RightClickCheck As CheckBox
    Friend WithEvents LeftClickCheck As CheckBox
    Friend WithEvents Label1 As Label
End Class
