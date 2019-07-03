Public Class Form1
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim bmp As New Bitmap(1, 1)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.CopyFromScreen(Cursor.Position, New Point(0, 0), New Size(1, 1))
        End Using
        Dim pixel As Color = bmp.GetPixel(0, 0)
        Label1.Text$ = bmp.GetPixel(0, 0).ToString
        Dim p As New Point
        p.X = (Me.Width / 2) - (Label1.Width / 2)
        p.Y = Label1.Top
        Label1.Location = p
        PictureBox1.BackColor = pixel
        Me.Invalidate()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            Me.TopMost = True
        ElseIf CheckBox1.Checked = False Then
            Me.TopMost = False
        End If
    End Sub
End Class
