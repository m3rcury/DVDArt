Public Class Housekeeping

    Public Sub HouseKeeping()

        ' delete System.Data.SQLite.dll when execution terminates
        Dim file As String = IO.Directory.GetCurrentDirectory() & "\System.Data.SQLite.dll"

        batchscript(file)

    End Sub

    Private Sub batchscript(file As String)

        Dim batchbuild As String = ""
        Dim nl As String = ControlChars.NewLine
        Dim temp As String = Environ("temp")

        batchbuild &= "echo off" & nl
        batchbuild &= "timeout /T 5 /NOBREAK" & nl
        batchbuild &= "del """ & file & """" & nl
        batchbuild &= "del """ & temp & "\dvdart_hk.bat""" & nl

        IO.File.WriteAllText(temp & "\dvdart_hk.bat", batchbuild)

        Dim ProcessProperties As New ProcessStartInfo
        ProcessProperties.WindowStyle = ProcessWindowStyle.Hidden

        ProcessProperties.FileName = temp & "\dvdart_hk.bat"

        Process.Start(ProcessProperties)

    End Sub

End Class
