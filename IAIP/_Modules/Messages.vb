Module Messages

#Region "Document upload messages"

    Public Enum DocumentMessageType As Byte
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

    Private Function GetDocumentMessageList() As Hashtable
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
    Private DocumentMessageList As Hashtable = GetDocumentMessageList()
    Public Function GetDocumentMessage(ByVal key As DocumentMessageType) As String
        Return DocumentMessageList(key)
    End Function

#End Region

#Region "Display/Clear procedures"

    ''' <summary>
    ''' Displays a precomposed message. If message represents an error, also shows error provider.
    ''' </summary>
    ''' <param name="messageDisplay">The label to display the message</param>
    ''' <param name="message">The message to display</param>
    ''' <param name="isError">True if message represents an error. Defaults to False.</param>
    ''' <param name="errorProvider">The error provider control</param>
    ''' <param name="control">The control to attach the error provider to</param>
    Public Sub DisplayMessage(ByVal messageDisplay As Label, ByVal message As String, Optional ByVal isError As Boolean = False, Optional ByVal errorProvider As ErrorProvider = Nothing, Optional ByVal control As Control = Nothing)
        If isError Then
            messageDisplay.ForeColor = Color.DarkRed
            If errorProvider IsNot Nothing AndAlso control IsNot Nothing Then
                errorProvider.SetError(control, message)
                errorProvider.SetIconAlignment(control, System.Windows.Forms.ErrorIconAlignment.TopLeft)
            End If
        Else
            messageDisplay.ForeColor = Color.DarkGreen
        End If
        messageDisplay.Text = message
        messageDisplay.Visible = True
    End Sub

    ''' <summary>
    ''' Clears a message and error provider
    ''' </summary>
    ''' <param name="messageDisplay">The label containing the message to clear</param>
    ''' <param name="errorProvider">The error provider to clear</param>
    Public Sub ClearMessage(ByVal messageDisplay As Label, Optional ByVal errorProvider As ErrorProvider = Nothing)
        messageDisplay.Visible = False
        messageDisplay.Text = ""
        If errorProvider IsNot Nothing Then errorProvider.Clear()
    End Sub

#End Region

End Module
