Imports Iaip.Apb.Finance
Imports Iaip.DAL
Imports Iaip.DAL.Finance

Public Class FinCreateRateItem

    Public Property SelectedCategory As FeeRateCategory
    Public Property NewRateItemID As Integer = -1
    Public Property Success As Boolean
    Private updating As Boolean

    Protected Overrides Sub OnLoad(e As EventArgs)
        lblMessage.ClearMessage()

        updating = True
        cmbCategory.BindToEnum(Of FeeRateCategory)()
        updating = False

        MyBase.OnLoad(e)
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged
        If Not updating Then
            SelectedCategory = CType(cmbCategory.SelectedValue, FeeRateCategory)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtDescription.Text) Then
            DialogResult = DialogResult.None
            lblMessage.ShowMessage("Enter a description for the rate item.", ErrorLevel.Warning)
            txtDescription.Focus()
            Return
        End If

        If txtNewRate.Amount <= 0 Then
            DialogResult = DialogResult.None
            lblMessage.ShowMessage("Enter a valid rate.", ErrorLevel.Warning)
            txtNewRate.Focus()
            Return
        End If

        Dim result As DbResult = SaveNewRateItem(CType(cmbCategory.SelectedValue, FeeRateCategory),
                                                 txtDescription.Text.Trim, txtNewRate.Amount, dtpNewRateDate.Value, NewRateItemID)

        Select Case result
            Case DbResult.Success
                Success = True
                Close()
            Case Else
                lblMessage.ShowMessage("There was an error saving the new rate item. Please contact EPD-IT for assistance.", ErrorLevel.Error)
                btnSave.Enabled = False
        End Select
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

End Class