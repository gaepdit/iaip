Imports System.Collections.Generic

Public Class DmuEdtErrorDetail

#Region " Properties and variables "

    Private _edtErrorID As Integer
    Public Property EdtErrorID() As Integer
        Get
            Return _edtErrorID
        End Get
        Set(ByVal value As Integer)
            If value = _edtErrorID Then Return
            _edtErrorID = value
            Init()
        End Set
    End Property

    Private _activeUsersList As List(Of KeyValuePair(Of Integer, String))
    Public Property ActiveUsersList() As List(Of KeyValuePair(Of Integer, String))
        Get
            Return _activeUsersList
        End Get
        Set(ByVal value As List(Of KeyValuePair(Of Integer, String)))
            If value.Equals(_activeUsersList) Then Return
            _activeUsersList = value
            PrepUserComboBox()
        End Set
    End Property

    Private edtErrorDetails As Dmu.EdtError

#End Region

#Region " Init "

    Private Sub Init()
        ErrorIDDisplay.Text = "Error #" & EdtErrorID.ToString
        Me.Text = "EDT Error #" & EdtErrorID.ToString & " Detail"
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
            DisplayDetails()
        End If
    End Sub

    Private Sub DisplayDetails()
        With edtErrorDetails
            If .Resolved Then
                If .ResolvedByUserID = 0 Then
                    If .ResolvedDate.HasValue Then
                        CurrentStatus.Text = "Resolved on " & .ResolvedDate.GetValueOrDefault.ToString(DateFormat)
                    Else
                        CurrentStatus.Text = "Resolved"
                    End If
                Else
                    If .ResolvedDate.HasValue Then
                        CurrentStatus.Text = "Resolved by " & .ResolvedByUserName & vbNewLine & " on " & .ResolvedDate.GetValueOrDefault.ToString(DateFormat)
                    Else
                        CurrentStatus.Text = "Resolved by " & .ResolvedByUserName
                    End If
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

                If .IaipIDCategory = Dmu.EdtIdCategory.AIRFACILITY Then
                    IaipId.Text = New Apb.ApbFacilityId(.IaipID).FormattedString
                Else
                    IaipId.Text = .IaipID
                End If
                If Not .IaipIDCategory = Dmu.EdtIdCategory.None Then
                    IaipId.Enabled = True
                End If

                If .IaipForeignIDCategory = Dmu.EdtIdCategory.AIRFACILITY Then
                    IaipForeignId.Text = New Apb.ApbFacilityId(.IaipForeignID).FormattedString
                Else
                    IaipForeignId.Text = .IaipForeignID
                End If
                If Not .IaipForeignIDCategory = Dmu.EdtIdCategory.None Then
                    IaipForeignId.Enabled = True
                End If
            End With
        End With

    End Sub

#End Region

#Region " Form resize "

    Private Sub DmuEdtErrorDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
    End Sub

    Private Sub DmuEdtErrorDetail_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        GenericErrorMessageDisplay.MaximumSize = New Size(GenericErrorMessageDisplayContainer.Size.Width - 30, 0)
        BusinessRuleDisplay.MaximumSize = New Size(BusinessRuleDisplayContainer.Size.Width - 30, 0)
        ErrorMessageDisplay.MaximumSize = New Size(ErrorMessageDisplayContainer.Size.Width - 30, 0)
    End Sub

#End Region

#Region " Actions "

    Private Sub AssignSelectedToUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssignSelectedToUser.Click

        If DAL.DMU.AssignErrorToUser(UserAssigned.SelectedValue, Me.EdtErrorID) Then
            MessageBox.Show("User assigned.", "Success", MessageBoxButtons.OK)
        Else
            MessageBox.Show("There was an error assigning the user.", "Error", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub ChangeStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeStatus.Click
        Dim result As Boolean = False
        Dim resolving As Boolean = Not edtErrorDetails.Resolved

        result = DAL.DMU.SetResolvedStatus(resolving, Me.EdtErrorID)

        If result = True Then
            edtErrorDetails.Resolved = resolving
            If resolving Then
                With edtErrorDetails
                    .ResolvedByUserID = CurrentUser.UserID
                    .ResolvedByUserName = CurrentUser.Staff.AlphaName
                    .ResolvedDate = Now
                End With
            End If
            DisplayDetails()
        Else
            MessageBox.Show("There was an error changing the status for the selected item.", "Error", MessageBoxButtons.OK)
        End If
    End Sub

#End Region

#Region " Links "

    Private Sub IaipId_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles IaipId.LinkClicked
        Select Case edtErrorDetails.EdtSubmission.IaipIDCategory
            Case Dmu.EdtIdCategory.AIRFACILITY
                OpenFormFacilitySummary(IaipId.Text)
            Case Dmu.EdtIdCategory.COMPLIANCEMONITORING
                OpenFormSscpWorkItem(IaipId.Text)
            Case Dmu.EdtIdCategory.COMPLIANCEMONITORINGFCE
                OpenFormFceByID(IaipId.Text)
            Case Dmu.EdtIdCategory.CASEFILE, Dmu.EdtIdCategory.ENFORCEMENTACTION
                OpenFormEnforcement(IaipId.Text)
        End Select
    End Sub
    Private Sub IaipForeignId_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles IaipForeignId.LinkClicked
        Select Case edtErrorDetails.EdtSubmission.IaipForeignIDCategory
            Case Dmu.EdtIdCategory.AIRFACILITY
                OpenFormFacilitySummary(IaipForeignId.Text)
            Case Dmu.EdtIdCategory.COMPLIANCEMONITORING
                OpenFormSscpWorkItem(IaipForeignId.Text)
            Case Dmu.EdtIdCategory.COMPLIANCEMONITORINGFCE
                OpenFormFceByID(IaipForeignId.Text)
            Case Dmu.EdtIdCategory.CASEFILE, Dmu.EdtIdCategory.ENFORCEMENTACTION
                OpenFormEnforcement(IaipForeignId.Text)
        End Select
    End Sub

#End Region

End Class
