Public Class Information
    Private Sub Information_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("OverHit {0}: Information", My.Application.Info.Version.ToString)
        Ver1.Text = String.Format("Version {0}", My.Application.Info.Version.ToString) 'OverHit Version Load.
    End Sub

    Private Sub helpbtn_Click(sender As Object, e As EventArgs) Handles helpbtn.Click
        MessageBox.Show("Not Yet :(", String.Format("Overhit {0}", My.Application.Info.Version.ToString), MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class