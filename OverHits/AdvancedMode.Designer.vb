<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdvancedMode
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
        Me.GeNEIL_ComboBox = New System.Windows.Forms.ComboBox()
        Me.TipLabel = New System.Windows.Forms.Label()
        Me.TiL1 = New System.Windows.Forms.Label()
        Me.TILComboBox = New System.Windows.Forms.ComboBox()
        Me.MchLrnCheck = New System.Windows.Forms.CheckBox()
        Me.FstScnCheck = New System.Windows.Forms.CheckBox()
        Me.MchLrnLabel = New System.Windows.Forms.Label()
        Me.FstScnLabel = New System.Windows.Forms.Label()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'GeNEIL_ComboBox
        '
        Me.GeNEIL_ComboBox.Font = New System.Drawing.Font("나눔바른고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GeNEIL_ComboBox.FormattingEnabled = True
        Me.GeNEIL_ComboBox.Location = New System.Drawing.Point(12, 23)
        Me.GeNEIL_ComboBox.Name = "GeNEIL_ComboBox"
        Me.GeNEIL_ComboBox.Size = New System.Drawing.Size(121, 27)
        Me.GeNEIL_ComboBox.TabIndex = 0
        Me.GeNEIL_ComboBox.Text = "GeNEIL"
        '
        'TipLabel
        '
        Me.TipLabel.AutoSize = True
        Me.TipLabel.Font = New System.Drawing.Font("나눔바른고딕", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TipLabel.Location = New System.Drawing.Point(139, 22)
        Me.TipLabel.Name = "TipLabel"
        Me.TipLabel.Size = New System.Drawing.Size(276, 28)
        Me.TipLabel.TabIndex = 1
        Me.TipLabel.Text = "GeNEIL is BERICVERIC's RECOMMEND SETTING." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "LMAO"
        Me.TipLabel.Visible = False
        '
        'TiL1
        '
        Me.TiL1.AutoSize = True
        Me.TiL1.Font = New System.Drawing.Font("나눔바른고딕", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TiL1.Location = New System.Drawing.Point(13, 115)
        Me.TiL1.Name = "TiL1"
        Me.TiL1.Size = New System.Drawing.Size(198, 14)
        Me.TiL1.TabIndex = 3
        Me.TiL1.Text = "Path of the overwatch's hp to aim!"
        '
        'TILComboBox
        '
        Me.TILComboBox.Font = New System.Drawing.Font("나눔바른고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TILComboBox.FormattingEnabled = True
        Me.TILComboBox.Location = New System.Drawing.Point(16, 132)
        Me.TILComboBox.Name = "TILComboBox"
        Me.TILComboBox.Size = New System.Drawing.Size(188, 27)
        Me.TILComboBox.TabIndex = 5
        Me.TILComboBox.Text = "PuryColorHP1"
        '
        'MchLrnCheck
        '
        Me.MchLrnCheck.AutoSize = True
        Me.MchLrnCheck.Font = New System.Drawing.Font("나눔바른고딕", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.MchLrnCheck.Location = New System.Drawing.Point(12, 223)
        Me.MchLrnCheck.Name = "MchLrnCheck"
        Me.MchLrnCheck.Size = New System.Drawing.Size(126, 18)
        Me.MchLrnCheck.TabIndex = 7
        Me.MchLrnCheck.Text = "Machine Learning"
        Me.MchLrnCheck.UseVisualStyleBackColor = True
        '
        'FstScnCheck
        '
        Me.FstScnCheck.AutoSize = True
        Me.FstScnCheck.Font = New System.Drawing.Font("나눔바른고딕", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FstScnCheck.Location = New System.Drawing.Point(12, 199)
        Me.FstScnCheck.Name = "FstScnCheck"
        Me.FstScnCheck.Size = New System.Drawing.Size(79, 18)
        Me.FstScnCheck.TabIndex = 8
        Me.FstScnCheck.Text = "Fast Scan"
        Me.FstScnCheck.UseVisualStyleBackColor = True
        '
        'MchLrnLabel
        '
        Me.MchLrnLabel.AutoSize = True
        Me.MchLrnLabel.Font = New System.Drawing.Font("나눔바른고딕", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.MchLrnLabel.Location = New System.Drawing.Point(131, 225)
        Me.MchLrnLabel.Name = "MchLrnLabel"
        Me.MchLrnLabel.Size = New System.Drawing.Size(157, 14)
        Me.MchLrnLabel.TabIndex = 9
        Me.MchLrnLabel.Text = "(Beta, Not Recommended)"
        '
        'FstScnLabel
        '
        Me.FstScnLabel.AutoSize = True
        Me.FstScnLabel.Font = New System.Drawing.Font("나눔바른고딕", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FstScnLabel.Location = New System.Drawing.Point(83, 201)
        Me.FstScnLabel.Name = "FstScnLabel"
        Me.FstScnLabel.Size = New System.Drawing.Size(43, 14)
        Me.FstScnLabel.TabIndex = 10
        Me.FstScnLabel.Text = "(Beta)"
        '
        'SaveButton
        '
        Me.SaveButton.Font = New System.Drawing.Font("나눔바른고딕OTF", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.SaveButton.Location = New System.Drawing.Point(305, 181)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(110, 60)
        Me.SaveButton.TabIndex = 11
        Me.SaveButton.Text = "Save!"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'AdvancedMode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(427, 253)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.FstScnLabel)
        Me.Controls.Add(Me.MchLrnLabel)
        Me.Controls.Add(Me.FstScnCheck)
        Me.Controls.Add(Me.MchLrnCheck)
        Me.Controls.Add(Me.TILComboBox)
        Me.Controls.Add(Me.TiL1)
        Me.Controls.Add(Me.TipLabel)
        Me.Controls.Add(Me.GeNEIL_ComboBox)
        Me.MaximizeBox = False
        Me.Name = "AdvancedMode"
        Me.Text = "Advanced Mode"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GeNEIL_ComboBox As ComboBox
    Friend WithEvents TipLabel As Label
    Friend WithEvents TiL1 As Label
    Friend WithEvents TILComboBox As ComboBox
    Friend WithEvents MchLrnCheck As CheckBox
    Friend WithEvents FstScnCheck As CheckBox
    Friend WithEvents MchLrnLabel As Label
    Friend WithEvents FstScnLabel As Label
    Friend WithEvents SaveButton As Button
End Class
