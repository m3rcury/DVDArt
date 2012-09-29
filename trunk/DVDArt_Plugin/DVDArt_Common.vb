Imports Microsoft.VisualBasic.FileIO
Imports System.Data.SQLite
Imports MediaPortal.Configuration

Public Class DVDArt_Common

    Private Sub wait(ByVal seconds As Long)
        System.Threading.Thread.Sleep(seconds * 1000)
    End Sub

    Public Shared Function parse(jsonresponse As String) As Array

        Dim moviediscurl(1, 20) As String
        Dim startp, endp, len, x As Integer

        x = 0
        startp = 1

        Do Until startp = 0

            startp = InStr(startp, jsonresponse, "id"":")

            If startp > 0 Then

                startp = InStr(startp, jsonresponse, "url"":")

                If startp > 0 Then

                    startp += 6
                    endp = InStr(startp, jsonresponse, """,")
                    len = endp - startp

                    ' movie url
                    moviediscurl(0, x) = Mid(jsonresponse, startp, len).Replace("\", "")

                    startp = InStr(endp, jsonresponse, "lang"":")

                    If startp > 0 Then

                        endp = InStr(startp, jsonresponse, """,")
                        len = endp - startp

                        ' cover art language
                        moviediscurl(1, x) = UCase(Mid(jsonresponse, startp, len).Replace("""", ""))

                        startp = InStr(endp, jsonresponse, "disc_type"":")

                        If startp > 0 Then

                            endp = InStr(startp, jsonresponse, """}")
                            len = endp - startp

                            ' cover art disk type
                            moviediscurl(1, x) = Trim(moviediscurl(1, x)) & " - " & UCase(Mid(jsonresponse, startp, len).Replace("""", ""))
                            moviediscurl(1, x) = moviediscurl(1, x).Replace("_", " ")

                        End If

                    End If

                    x += 1

                    If x > 20 Then Exit Do

                    startp = endp

                Else
                    Exit Do
                End If
            Else
                Exit Do
            End If

        Loop

        Return moviediscurl

    End Function

    Public Shared Function JSON_request(ByVal imdb_id As String, ByVal nbrimages As String) As String

        Dim WebClient As New System.Net.WebClient
        Dim apikey As String = "bfd6e4e0d4e71237f784b70fc43f8269"

        Dim url As String = "http://fanart.tv/webservice/movie/" & apikey & "/" & imdb_id & "/json/moviedisc/1/" & nbrimages

        Return WebClient.DownloadString(url)

    End Function

    Public Shared Function download(ByVal thumbs As String, ByVal imdb_id As String, ByVal overwrite As Boolean) As Boolean

        Dim WebClient As New System.Net.WebClient
        Dim moviediscurl(0, 0) As String
        Dim found As Boolean = False

        Dim fullpath, thumbpath, jsonresponse As String

        jsonresponse = JSON_request(imdb_id, "1")

        If jsonresponse <> "null" Then

            moviediscurl = parse(jsonresponse)

            fullpath = thumbs & "\MovingPictures\DVDArt\FullSize\" & imdb_id & ".png"
            thumbpath = thumbs & "\MovingPictures\DVDArt\Thumbs\" & imdb_id & ".png"

            If Not FileSystem.FileExists(thumbpath) Or overwrite Then
                WebClient.DownloadFile(moviediscurl(0, 0) & "/preview", thumbpath)
            End If

            If Not FileSystem.FileExists(fullpath) Or overwrite Then
                WebClient.DownloadFile(moviediscurl(0, 0), fullpath)
            End If

            found = FileSystem.FileExists(thumbpath)

        End If

        Return found

    End Function

    Public Shared Function Get_Paths(ByRef database As String, ByRef thumbs As String) As Boolean

        database = Nothing
        thumbs = Nothing

        Using XMLreader As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Base, "MediaPortalDirs.xml"))

            database = XMLreader.GetValueAsString("Path", "Database", Config.GetFolder(Config.Dir.Database))
            thumbs = XMLreader.GetValueAsString("Path", "Thumbs", Config.GetFolder(Config.Dir.Thumbs))

        End Using

        Return database <> Nothing And thumbs <> Nothing

    End Function

End Class
