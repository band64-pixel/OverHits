Imports System.Xml

Public Class AdvancedMode
    Public IsSaved As Boolean = True
    Public IsSetting As Boolean = False

    Public TipLabel_DefaultValue As String = "GeNEIL is BERICVERIC's RECOMMEND SETTING." & vbNewLine & "LMAO"

#Region "Enum: GENEIL Items"
    Public Enum GENEIL
        GENEIL
        AdvanGo
        CuSeol
    End Enum

    Public Enum PuryColorHP
        PuryColorHP1
        PuryColorHP2
        PuryColorHP3
        Custom
    End Enum
#End Region

    Private Sub AdvancedMode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            IsSetting = True

            Me.Text = String.Format("OverHit {0}: Advanced Mode", My.Application.Info.Version.ToString)

#Region "Text's Variables Load"
            'GENEIL 콤보박스 아이템 추가
            For Each GENEIL_Item As String In [Enum].GetNames(GetType(GENEIL))
                GeNEIL_ComboBox.Items.Add(GENEIL_Item)
            Next

            'PuryColor 콤보박스 아이템 추가
            For Each PuryColor_Item As String In [Enum].GetNames(GetType(PuryColorHP))
                TILComboBox.Items.Add(PuryColor_Item)
            Next
#End Region

#Region "XML Settings Load"
            If MainProject.MchLrn Then
                MchLrnCheck.Checked = True
            End If

            If MainProject.FstScn Then
                FstScnCheck.Checked = True
            End If

            If MainProject.GENEILPath = GENEIL.GENEIL Then
                GeNEIL_ComboBox.Text = "GENEIL"
            ElseIf MainProject.GENEILPath = GENEIL.AdvanGo Then
                GeNEIL_ComboBox.Text = "AdvanGo"
            ElseIf MainProject.GENEILPath = GENEIL.CuSeol Then
                GeNEIL_ComboBox.Text = "CuSeol"
            End If

            If MainProject.PuryColorPath = PuryColorHP.PuryColorHP1 Then
                TILComboBox.Text = "PuryColorHP1"
            ElseIf MainProject.PuryColorPath = PuryColorHP.PuryColorHP2 Then
                TILComboBox.Text = "PuryColorHP2"
            ElseIf MainProject.PuryColorPath = PuryColorHP.PuryColorHP3 Then
                TILComboBox.Text = "PuryColorHP3"
            ElseIf MainProject.PuryColorPath = PuryColorHP.Custom Then
                TILComboBox.Text = "Custom"
            End If

#End Region

            IsSetting = False
            IsSaved = True

            '(서버가 없어도 되는 사람의 노가다) 머신 러닝을 구현하즈아ㅏ!!!
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GeNEIL_ComboBox_MouseHover(sender As Object, e As EventArgs) Handles GeNEIL_ComboBox.MouseHover
        Select Case GeNEIL_ComboBox.SelectedText
            Case "GeNEIL"
                TipLabel.Text = "GeNEIL is BERICVERIC's Recommend Setting." & vbNewLine & "You can hack the overwatch easily with OverHit."
                TipLabel.Visible = True
        End Select
    End Sub

    Private Sub GeNEIL_ComboBox_MouseLeave(sender As Object, e As EventArgs) Handles GeNEIL_ComboBox.MouseLeave
        TipLabel.Visible = False
        TipLabel.Text = TipLabel_DefaultValue
    End Sub

    Private Sub GeNEIL_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GeNEIL_ComboBox.SelectedIndexChanged
        IsSaved = False
        If GeNEIL_ComboBox.SelectedItem.ToString = "CuSeol" Then
            TILComboBox.Enabled = True
        Else
            TILComboBox.Enabled = False
        End If
    End Sub

    Private Sub MchLrn_CheckedChanged(sender As Object, e As EventArgs) Handles MchLrnCheck.CheckedChanged
        If IsSetting = False Then
            If MchLrnCheck.Checked Then
                If MessageBox.Show("Would you like to turn on the 'Machine Learning'?" & vbNewLine & "It may affects about Computer spec :(" & vbNewLine & "Also, We doesn't recommend it to enable it. Becuz It's Beta and it may error." & vbNewLine & "In a nutshell, It may not working! Becareful.", Me.Text & ": Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    IsSaved = False
                Else
                    MchLrnCheck.Checked = False
                End If
            End If
        End If
    End Sub

    Public Function AdvancedMode_SaveSettings() As Boolean
        Try

            Dim setNode As New XmlDocument
            Dim setaNode As XmlNode
            setNode.Load(Application.StartupPath & "\settings.xml")
            setaNode = setNode.SelectSingleNode("/OverHits/Settings/Advanced-Settings")

            If MchLrnCheck.Checked Then
                setaNode.ChildNodes(0).InnerText = "True"
            Else
                setaNode.ChildNodes(0).InnerText = "False"
            End If

            If FstScnCheck.Checked Then
                setaNode.ChildNodes(1).InnerText = "True"
            Else
                setaNode.ChildNodes(1).InnerText = "False"
            End If

            If GeNEIL_ComboBox.Text = "GENEIL" Then
                setaNode.ChildNodes(2).InnerText = "GENEIL"
            ElseIf GeNEIL_ComboBox.Text = "AdvanGo" Then
                setaNode.ChildNodes(2).InnerText = "AdvanGo"
            ElseIf GeNEIL_ComboBox.Text = "CuSeol" Then
                setaNode.ChildNodes(2).InnerText = "CuSeol"
            End If

            If TILComboBox.Text = "PuryColorHP1" Then
                setaNode.ChildNodes(3).InnerText = "PuryColorHP1"
            ElseIf TILComboBox.Text = "PuryColorHP2" Then
                setaNode.ChildNodes(3).InnerText = "PuryColorHP2"
            ElseIf TILComboBox.Text = "PuryColorHP3" Then
                setaNode.ChildNodes(3).InnerText = "PuryColorHP3"
            ElseIf TILComboBox.Text = "Custom" Then
                setaNode.ChildNodes(3).InnerText = "Custom"
            End If

            setNode.Save(Application.StartupPath & "\settings.xml")
            IsSaved = True
            Return True

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub AdvancedMode_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If IsSaved = False Then
            Dim clResult As DialogResult = MessageBox.Show("You didn't save your settings. Would you like to save and exit?", Me.Text & ": Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If clResult = DialogResult.Yes Then
                AdvancedMode_SaveSettings()
            ElseIf clResult = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        If AdvancedMode_SaveSettings() AndAlso MainProject.MainProject_UpdateSettings() Then
            MessageBox.Show("Saved!", Me.Text & ": Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class