Dim screenshots As Bitmap
        Dim graph As Graphics
        Dim sz As Size = New Point(954, 532)
        screenshots = New Bitmap(12, 16, PixelFormat.Format32bppRgb)
        graph = Graphics.FromImage(screenshots) '스크린샷.
        graph.CopyFromScreen(954, 532, 0, 0, sz, CopyPixelOperation.SourceCopy)
        'screenshots.Save(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\TFirst-AimSearch.png", ImageFormat.Png)

'픽셀 반복 함수.
        For x As Integer = 0 To 11
            For y As Integer = 0 To 15
                '픽셀 값
                Dim p As Color = screenshots.GetPixel(x, y)

                '수정 해야할 사항: Unik 코드 본 다음 코드 짜기.
                If All4Colors.Scanning(p) Then
                    '각각 AimPointL과 AimPointT를 더하는 이유는 에임을 조준할 사각형을 더해줘야 함.
                    Cursor.Position = New Point(x + AimPointL, y + AimPointT)

                    Do 'The Second of AimSearch!
                        Dim screenshots2 As Bitmap
                        Dim graph2 As Graphics
                        Dim sz2 As Size = New Point(720, 540)
                        screenshots2 = New Bitmap(480, 16, PixelFormat.Format32bppArgb)
                        graph2 = Graphics.FromImage(screenshots2) '스크린샷.
                        graph2.CopyFromScreen(720, 540, 0, 0, sz2, CopyPixelOperation.SourceCopy)

                        For x2 As Integer = 0 To 479
                            For y2 As Integer = 0 To 15
                                Dim p2 As Color = screenshots2.GetPixel(x2, y2)

                            Next
                        Next
                    Loop Until 10

                    a.Stop()
                    Debug.WriteLine("True: " & a.ElapsedMilliseconds & "ms")
                    Return True
                    Exit For

                Else
                    Continue For
                End If
            Next
        Next