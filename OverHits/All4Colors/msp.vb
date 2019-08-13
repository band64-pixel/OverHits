Imports System.Runtime.InteropServices
Imports System.Threading

Public Class msp

    Declare Function mouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dX As Int32, ByVal dY As Int32, ByVal cButtons As Int32, ByVal dwExtraInfo As Int32) As Boolean
    <DllImport("user32.dll", EntryPoint:="PostMessageA")>
    Public Shared Function PostMessage(hWnd As IntPtr, msg As UInteger, wParam As Integer, IParam As IntPtr) As Boolean
    End Function
    <DllImport("user32.dll")>
    Public Shared Function SendMessage(hWnd As IntPtr, msg As UInteger, wParam As Integer, IParam As IntPtr) As Integer
    End Function
#Region "WMessages : int"
    Public Enum WMessages
        ''' <summary>
        ''' Left mousebutton down
        ''' </summary>
        WM_LBUTTONDOWN = 513

        ''' <summary>
        ''' Left mousebutton up
        ''' </summary>
        WM_LBUTTONUP = 514

        ''' <summary>
        ''' Left mousebutton doubleclick
        ''' </summary>
        WM_LBUTTONDBLCLK = 515

        ''' <summary>
        ''' Right mousebutton down
        ''' </summary>
        WM_RBUTTONDOWN = 516

        ''' <summary>
        ''' Right mousebutton up
        ''' </summary>
        WM_RBUTTONUP = 517

        ''' <summary>
        ''' Right mousebutton doubleclick
        ''' </summary>
        WM_RBUTTONDBLCLK = 518

        ''' <summary>
        ''' Key down
        ''' </summary>
        WM_KEYDOWN = 256

        ''' <summary>
        ''' Key up
        ''' </summary>
        WM_KEYUP = 257

        WM_SYSKEYDOWN = 260
        WM_SYSKEYUP = 261
        WM_CHAR = 258
        WM_COMMAND = 273

        WM_MOUSEMOVE = &H200
    End Enum
#End Region

    ''' <summary>
    ''' Move the Mouse smoothly. using 'mouse_event' Function
    ''' </summary>
    ''' <param name="int">not cursor_position.</param>
    Public Shared Function MoveToSmooth(ByVal int As Point) As Boolean
        Dim xi As Integer = 2
        Dim yi As Integer = 1
        Dim dx As Integer = int.X
        Dim dy As Integer = int.Y

        If int.X < 0 Then
            xi = -2
            dx *= -1
        End If

        If int.Y < 0 Then
            yi = -1
            dy *= -1
        End If

        Dim i As Integer = 1
        Dim rnd As New Random
        Dim pm As Boolean

        While i <= dx
            If Not i <= dy Then
                mouse_event(&H1, xi, yi, 0, 0)
            Else
                mouse_event(&H1, xi, 0, 0, 0)
            End If

            pm = rnd.Next(0, 30)
            If pm Then
                Thread.Sleep(1)
            End If

            i += 2
        End While

        Return True
    End Function

    Public Shared Function MoveLikAimbot(ByVal int As Point) As Boolean
        Dim xi As Integer = 1
        Dim yi As Integer = 1

        If int.X < 0 Then
            xi = -1
        End If

        If int.Y < 0 Then
            yi = -1
        End If

        Dim i As Integer = 2
        Dim rnd As New Random
        Dim pm As Boolean
        While i <= int.X
            pm = rnd.Next(0, 2)
            If Not i <= int.Y Then
                mouse_event(&H1, xi, yi, 0, 0)
                If pm Then Thread.Sleep(1)
            Else
                mouse_event(&H1, xi, 0, 0, 0)
                If pm Then Thread.Sleep(1)
            End If
            i += 1
        End While

        Return True
    End Function

    Public Shared Function MakeLParam(ByVal LoWord As Integer, ByVal HiWord As Integer) As IntPtr
        Return New IntPtr((HiWord << 16) Or (LoWord And &HFFFF))
    End Function
End Class
