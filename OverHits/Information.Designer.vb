<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Information
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
        Me.Name1 = New System.Windows.Forms.Label()
        Me.Ver1 = New System.Windows.Forms.Label()
        Me.chkbtn = New System.Windows.Forms.Button()
        Me.helpbtn = New System.Windows.Forms.Button()
        Me.madeby = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Name1
        '
        Me.Name1.AutoSize = True
        Me.Name1.Font = New System.Drawing.Font("나눔바른고딕OTF", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name1.Location = New System.Drawing.Point(112, 21)
        Me.Name1.Name = "Name1"
        Me.Name1.Size = New System.Drawing.Size(193, 55)
        Me.Name1.TabIndex = 0
        Me.Name1.Text = "OverHit"
        '
        'Ver1
        '
        Me.Ver1.AutoSize = True
        Me.Ver1.Font = New System.Drawing.Font("나눔바른고딕", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Ver1.Location = New System.Drawing.Point(128, 76)
        Me.Ver1.Name = "Ver1"
        Me.Ver1.Size = New System.Drawing.Size(125, 22)
        Me.Ver1.TabIndex = 1
        Me.Ver1.Text = "Version None"
        '
        'chkbtn
        '
        Me.chkbtn.Enabled = False
        Me.chkbtn.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkbtn.Location = New System.Drawing.Point(308, 124)
        Me.chkbtn.Name = "chkbtn"
        Me.chkbtn.Size = New System.Drawing.Size(98, 62)
        Me.chkbtn.TabIndex = 3
        Me.chkbtn.Text = "Check Update"
        Me.chkbtn.UseVisualStyleBackColor = True
        '
        'helpbtn
        '
        Me.helpbtn.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.helpbtn.Location = New System.Drawing.Point(217, 124)
        Me.helpbtn.Name = "helpbtn"
        Me.helpbtn.Size = New System.Drawing.Size(75, 62)
        Me.helpbtn.TabIndex = 3
        Me.helpbtn.Text = "Help?"
        Me.helpbtn.UseVisualStyleBackColor = True
        '
        'madeby
        '
        Me.madeby.AutoSize = True
        Me.madeby.Font = New System.Drawing.Font("나눔고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.madeby.Location = New System.Drawing.Point(12, 168)
        Me.madeby.Name = "madeby"
        Me.madeby.Size = New System.Drawing.Size(167, 19)
        Me.madeby.TabIndex = 4
        Me.madeby.Text = "Made By BERICVERIC"
        '
        'Information
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(418, 198)
        Me.Controls.Add(Me.madeby)
        Me.Controls.Add(Me.helpbtn)
        Me.Controls.Add(Me.chkbtn)
        Me.Controls.Add(Me.Ver1)
        Me.Controls.Add(Me.Name1)
        Me.MaximizeBox = False
        Me.Name = "Information"
        Me.Text = "Information"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Name1 As Label
    Friend WithEvents Ver1 As Label
    Friend WithEvents chkbtn As Button
    Friend WithEvents helpbtn As Button
    Friend WithEvents madeby As Label
End Class
