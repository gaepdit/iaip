Module DocumentMessages

#Region "Document upload messages"

    Public Enum DocumentMessageType
        InvalidApplicationNumber
        InvalidEnforcementNumber
        FileNotFound
        DocumentTypeAlreadyExists
        UploadSuccess
        UploadFailure
        FileTooLarge
        FileEmpty
        DeleteSuccess
        DeleteFailure
        ConfirmDelete
        DownloadFailure
        UpdateSuccess
        UpdateFailure
        DownloadingFile
        UploadingFile
    End Enum

    Private Function GetDocumentMessages() As Hashtable
        Dim messageList As New Hashtable
        messageList.Add(DocumentMessageType.InvalidApplicationNumber, "Error: That application number does not exist.")
        messageList.Add(DocumentMessageType.InvalidEnforcementNumber, "Error: That enforcement number does not exist.")
        messageList.Add(DocumentMessageType.FileNotFound, "Error: The file cannot be found.")
        messageList.Add(DocumentMessageType.DocumentTypeAlreadyExists, "A ""{0}"" has already been uploaded for this application.")
        messageList.Add(DocumentMessageType.UploadSuccess, "Success: The file ""{0}"" has been uploaded.")
        messageList.Add(DocumentMessageType.UploadFailure, "Error: There was an error uploading the file. Please try again.")
        messageList.Add(DocumentMessageType.FileTooLarge, "The selected file is too large. Maximum file size is " & Math.Round(Document.MaxFileSize / (1024 ^ 3), 2) & "GB.")
        messageList.Add(DocumentMessageType.FileEmpty, "The selected file is empty.")
        messageList.Add(DocumentMessageType.DeleteFailure, "Error: The selected file was not deleted. Please try again.")
        messageList.Add(DocumentMessageType.DeleteSuccess, "Success: The file ""{0}"" was deleted.")
        messageList.Add(DocumentMessageType.ConfirmDelete, "Are you sure you want to delete the file ""{0}""?")
        messageList.Add(DocumentMessageType.DownloadFailure, "Error: There was an error saving the file. Please try again.")
        messageList.Add(DocumentMessageType.UpdateFailure, "Error: The selected file was not updated. Please try again.")
        messageList.Add(DocumentMessageType.UpdateSuccess, "Success: The file ""{0}"" was updated.")
        messageList.Add(DocumentMessageType.DownloadingFile, "Downloading {0}. Please wait.")
        messageList.Add(DocumentMessageType.UploadingFile, "Uploading {0}. Please stand by.")

        Return messageList
    End Function
    Private DocumentMessages As Hashtable = GetDocumentMessages()
    Public Function GetDocumentMessage(ByVal key As DocumentMessageType) As String
        Return DocumentMessages(key)
    End Function

#End Region

End Module
