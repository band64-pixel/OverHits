Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Public Class scr
    <DllImport("user32.dll")>
    Public Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function PrintWindow(ByVal hwnd As IntPtr, ByVal hDC As IntPtr, ByVal nFlags As UInteger) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowRgn(ByVal hWnd As IntPtr, ByVal hRgn As IntPtr) As Integer
    End Function
    <DllImport("gdi32.dll")>
    Private Shared Function CreateRectRgn(ByVal nLeftRect As Integer, ByVal nTopRect As Integer, ByVal nRightRect As Integer, ByVal nBottomRect As Integer) As IntPtr
    End Function

    Public Sub New()
    End Sub

    Public Shared Function PrintWindow(ByVal hwnd As IntPtr) As Bitmap
        Dim gfxWin As Graphics = Graphics.FromHwnd(hwnd)
        Dim rc As Rectangle = Rectangle.Round(gfxWin.VisibleClipBounds)
        Dim bmp As Bitmap = New Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb)
        Dim gfxBmp As Graphics = Graphics.FromImage(bmp)
        Dim hdcBitmap As IntPtr = gfxBmp.GetHdc()
        Dim succeeded As Boolean = PrintWindow(hwnd, hdcBitmap, 2)
        gfxBmp.ReleaseHdc(hdcBitmap)

        If Not succeeded Then
            gfxBmp.FillRectangle(New SolidBrush(Color.Gray), New Rectangle(Point.Empty, bmp.Size))
        End If

        Dim hRgn As IntPtr = CreateRectRgn(0, 0, 0, 0)
        GetWindowRgn(hwnd, hRgn)
        Dim region As Region = Region.FromHrgn(hRgn)

        If Not region.IsEmpty(gfxBmp) Then
            gfxBmp.ExcludeClip(region)
            gfxBmp.Clear(Color.Transparent)
        End If

        gfxBmp.Dispose()
        Return bmp
    End Function
End Class
