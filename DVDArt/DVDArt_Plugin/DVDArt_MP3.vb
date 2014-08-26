Imports System.IO

Imports tagnetlib

Public Class DVDArt_MP3

    Public Shared Sub EmbedID3(ByVal mp3 As String, ByVal image As String, Optional ByVal mimetype As String = Nothing)

        Dim file As ID3File = New ID3File()

        file.Open(mp3, FileAccess.ReadWrite)

        Dim tag As ITag = file.V2Tag
        Dim frames As IFrame() = tag.GetFrameArray()

        Dim pic As ID3Pic = New ID3Pic()
        Dim framepic As ID3FramePic = New ID3FramePic()
        Dim picture As Bitmap = New Bitmap(image)

        pic.SetImage(picture)

        If mimetype Is Nothing Then
            pic.MimeType = "image/jpeg"
        Else
            pic.MimeType = mimetype
        End If

        pic.PictureType = ID3C.PicTypeEnum.CoverFront
        framepic.SetField(ID3C.FieldID.Picture, pic)
        framepic.ID = ID3C.FrameID.AttachedPicture
        framepic.TextID = "APIC"

        tag.AddFrame(framepic)

        file.Update(ID3C.UpdateTagEnum.Both)
        file.Close()

    End Sub

    Public Shared Function ExtractMP3Image(ByVal mp3 As String) As Bitmap

        Dim file As ID3File = New ID3File()

        file.Open(mp3, FileAccess.Read)

        Dim tag As ITag = file.V2Tag
        Dim pic As ID3Pic = New ID3Pic()

        Return pic.GetImage()

    End Function

End Class
