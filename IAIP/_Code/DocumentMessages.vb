Module DocumentMessages

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
        Dim messageList As New Hashtable From {
            {DocumentMessageType.InvalidApplicationNumber, "Error: That application number does not exist."},
            {DocumentMessageType.InvalidEnforcementNumber, "Error: That enforcement number does not exist."},
            {DocumentMessageType.FileNotFound, "Error: The file cannot be found."},
            {DocumentMessageType.DocumentTypeAlreadyExists, "A ""{0}"" has already been uploaded for this application."},
            {DocumentMessageType.UploadSuccess, "Success: The file ""{0}"" has been uploaded."},
            {DocumentMessageType.UploadFailure, "Error: There was an error uploading the file. Please try again."},
            {DocumentMessageType.FileTooLarge, "The selected file is too large. Maximum file size is " & Math.Round(Document.MaxFileSize / (1024 ^ 3), 2) & "GB."},
            {DocumentMessageType.FileEmpty, "The selected file is empty."},
            {DocumentMessageType.DeleteFailure, "Error: The selected file was not deleted. Please try again."},
            {DocumentMessageType.DeleteSuccess, "Success: The file ""{0}"" was deleted."},
            {DocumentMessageType.ConfirmDelete, "Are you sure you want to delete the file ""{0}""?"},
            {DocumentMessageType.DownloadFailure, "Error: There was an error saving the file. Please try again."},
            {DocumentMessageType.UpdateFailure, "Error: The selected file was not updated. Please try again."},
            {DocumentMessageType.UpdateSuccess, "Success: The file ""{0}"" was updated."},
            {DocumentMessageType.DownloadingFile, "Downloading {0}. Please wait."},
            {DocumentMessageType.UploadingFile, "Uploading {0}. Please stand by."}
        }

        Return messageList
    End Function

    Private ReadOnly DocumentMessages As Hashtable = GetDocumentMessages()

    Public Function GetDocumentMessage(key As DocumentMessageType) As String
        Return DocumentMessages(key).ToString
    End Function

End Module
