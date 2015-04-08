Imports System.Collections.Generic

Public Class DmuEdtErrorDetail

#Region " Properties and variables "

    Private _edtErrorID As Integer
    Public Property EdtErrorID() As Integer
        Get
            Return _edtErrorID
        End Get
        Set(ByVal value As Integer)
            _edtErrorID = value
        End Set
    End Property

    Private _activeUsersList As List(Of KeyValuePair(Of Integer, String))
    Public Property ActiveUsersList() As List(Of KeyValuePair(Of Integer, String))
        Get
            Return _activeUsersList
        End Get
        Set(ByVal value As List(Of KeyValuePair(Of Integer, String)))
            _activeUsersList = value
        End Set
    End Property

    Private edtErrorDetails As DMU.EdtError

#End Region

#Region " Init "

    Public Sub Init()
        ErrorIDDisplay.Text = "Error #" & EdtErrorID.ToString
        Me.Text = "EDT Error #" & EdtErrorID.ToString & " Detail"
        PrepUserComboBox()
        GetData()
    End Sub

    Private Sub PrepUserComboBox()
        UserAssigned.BindToKeyValuePairs(ActiveUsersList)
    End Sub

#End Region

#Region " Data "

    Private Sub GetData()
        edtErrorDetails = DAL.DMU.GetErrorDetail(EdtErrorID)

        If edtErrorDetails Is Nothing Then
            CurrentStatus.Text = "No data"
            AssignSelectedToUser.Enabled = False
            UserAssigned.Enabled = False
            ChangeStatus.Enabled = False
        Else
            With edtErrorDetails
                If .Resolved Then
                    If .ResolvedByUserID = 0 Then
                        CurrentStatus.Text = "Resolved on " & .ResolvedDate.ToString(DateFormat)
                    Else
                        CurrentStatus.Text = "Resolved by " & .ResolvedByUserName & vbNewLine & " on " & .ResolvedDate.ToString(DateFormat)
                    End If
                    ChangeStatus.Text = "Reopen"
                Else
                    CurrentStatus.Text = "Open"
                    ChangeStatus.Text = "Resolve"
                End If

                UserAssigned.SelectedValue = .AssignedToUserID
                UserAssigned.Enabled = Not .Resolved
                AssignSelectedToUser.Enabled = Not .Resolved

                With .ErrorMessage
                    EdtErrorCode.Text = .ErrorCode
                    GenericErrorMessageDisplay.Text = .ErrorCategory & vbNewLine & vbNewLine & .ErrorMessage
                    BusinessRuleDisplay.Text = .BusinessRuleMessage
                End With

                ErrorMessageDisplay.Text = .ErrorMessage.ErrorCategory & vbNewLine & vbNewLine & .EdtErrorMessageDetail

                With .EdtSubmission
                    TableName.Text = .EdtTableName
                    EdtId.Text = .EdtID
                    EdtForeignKey.Text = .EdtForeignKeyID
                    EdtOperation.Text = .EdtOperation
                    EdtStatus.Text = .EdtStatus
                    EdtDateSubmitted.Text = .EdtSubmitDate.ToString(DateFormat)
                End With
            End With
        End If
    End Sub

#End Region

#Region " Form resize "

    Private Sub DmuEdtErrorDetail_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        GenericErrorMessageDisplay.MaximumSize = New Size(GenericErrorMessageDisplayContainer.Size.Width - 30, 0)
        BusinessRuleDisplay.MaximumSize = New Size(BusinessRuleDisplayContainer.Size.Width - 30, 0)
        ErrorMessageDisplay.MaximumSize = New Size(ErrorMessageDisplayContainer.Size.Width - 30, 0)
    End Sub

#End Region

#Region " Actions "

    Private Sub AssignSelectedToUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssignSelectedToUser.Click
        DAL.DMU.AssignErrorToUser(UserAssigned.SelectedValue, Me.EdtErrorID)
    End Sub

    Private Sub ChangeStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeStatus.Click
        If edtErrorDetails.Resolved Then
            DAL.DMU.SetResolvedStatus(False, Me.EdtErrorID)
        Else
            DAL.DMU.SetResolvedStatus(True, Me.EdtErrorID)
        End If
    End Sub

#End Region

End Class
