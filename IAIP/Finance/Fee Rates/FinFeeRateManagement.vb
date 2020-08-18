Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports Iaip.Apb.Finance
Imports Iaip.DAL
Imports Iaip.DAL.Finance

Public Class FinFeeRateManagement

    Private feeRatesSchedule As Dictionary(Of Integer, FeeRateItem)
    Private selectedRateItem As FeeRateItem
    Private updating As Boolean

    Private Class FeeRateItemDisplay
        Public Property Key As Integer
        Public Property Description As String
        <DisplayName("Effective Date")>
        Public Property EffectiveDate As Date
        <DisplayName("Final Date")>
        Public Property FinalDate As Date?
    End Class

    Private Class FeeRateDisplay
        <DisplayName("Effective Date")>
        Public Property EffectiveDate As Date
        <DisplayName("Fee Rate")>
        Public Property FeeRate As Decimal
    End Class

    Protected Overrides Sub OnLoad(e As EventArgs)
        lblMessage.ClearMessage()

        updating = True
        cmbCategory.BindToEnum(Of FeeRateCategory)()
        updating = False

        LoadFeeRateItems()

        MyBase.OnLoad(e)
    End Sub

    ' Fee Rate Items

    Private Sub LoadFeeRateItems()
        If feeRatesSchedule Is Nothing Then
            feeRatesSchedule = ReloadSharedObject(Of Dictionary(Of Integer, FeeRateItem))(SharedObject.FeeRatesSchedule)
        End If

        Dim selectedCategory As FeeRateCategory = CType(cmbCategory.SelectedValue, FeeRateCategory)

        Dim items As IEnumerable(Of KeyValuePair(Of Integer, FeeRateItem))

        items = feeRatesSchedule.Where(Function(m) m.Value.RateCategory = selectedCategory)

        If Not chkShowInactive.Checked Then
            items = items.Where(Function(m) Not m.Value.EndDate.HasValue OrElse m.Value.EndDate.Value >= Today)
        End If

        updating = True

        dgvRateItems.DataSource = items.
            Select(Function(i) New FeeRateItemDisplay With {
                .Key = i.Key,
                .Description = i.Value.Description,
                .EffectiveDate = i.Value.BeginDate,
                .FinalDate = i.Value.EndDate
                }).
            OrderBy(Function(i) i.Description).
            ToArray()

        dgvRateItems.Columns("Key").Visible = False

        SelectNoRateItem()

        updating = False
    End Sub

    Private Sub dgvRateItems_CellLinkSelected(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvRateItems.CellLinkSelected
        If Not updating Then
            SelectRateItem(CInt(e.LinkValue))
        End If
    End Sub

    Private Sub SelectRateItem(rateItemID As Integer)
        lblMessage.ClearMessage()

        selectedRateItem = feeRatesSchedule(rateItemID)

        If selectedRateItem Is Nothing Then
            lblMessage.ShowMessage("Error: Rate Item could not be selected.", ErrorLevel.Error)
            Return
        End If

        txtRateItemName.Text = selectedRateItem.Description

        btnUpdateRateItemName.Enabled = True

        grpNewRate.Visible = False
        grpEndRateItem.Visible = False
        btnAddNewEffectiveRate.Visible = True
        btnEndRateItem.Visible = True

        If selectedRateItem.Rates.Count > 0 Then
            dgvRateItemHistory.DataSource = selectedRateItem.Rates.Contents.
                Select(Function(i) New FeeRateDisplay With {.EffectiveDate = i.Key, .FeeRate = i.Value}).
                ToArray()

            dgvRateItemHistory.SelectNone()

            Dim maxUsedDate As Date? = GetRateItemMaxUsedDate(selectedRateItem.FeeRateItemID)

            dtpEndRateItemDate.MinDate = CDate({selectedRateItem.Rates.LatestDate, maxUsedDate}.Max())
            dtpNewEffectiveRateDate.MinDate = CDate({selectedRateItem.Rates.LatestDate, maxUsedDate}.Max()).AddDays(1)
        End If

        txtRateItemName.Enabled = Not selectedRateItem.EndDate.HasValue
        btnUpdateRateItemName.Enabled = Not selectedRateItem.EndDate.HasValue
        btnAddNewEffectiveRate.Enabled = Not selectedRateItem.EndDate.HasValue
        btnEndRateItem.Enabled = Not selectedRateItem.EndDate.HasValue AndAlso (selectedRateItem.Rates.Count > 0)
    End Sub

    Private Sub SelectNoRateItem()
        dgvRateItems.SelectNone()
        selectedRateItem = Nothing
        txtRateItemName.Text = String.Empty
        btnUpdateRateItemName.Enabled = False

        dgvRateItemHistory.DataSource = Nothing

        grpNewRate.Visible = False
        grpEndRateItem.Visible = False
        btnAddNewEffectiveRate.Enabled = False
        btnEndRateItem.Enabled = False
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged
        If Not updating Then
            LoadFeeRateItems()
        End If
    End Sub

    Private Sub chkShowInactive_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowInactive.CheckedChanged
        If Not updating Then
            LoadFeeRateItems()
        End If
    End Sub

    Private Sub btnUpdateRateItemName_Click(sender As Object, e As EventArgs) Handles btnUpdateRateItemName.Click
        If String.IsNullOrWhiteSpace(txtRateItemName.Text) Then
            lblMessage.ShowMessage("Enter a description for the rate item.", ErrorLevel.Warning)
            Return
        End If

        Dim id As Integer = selectedRateItem.FeeRateItemID

        Dim result As DbResult = UpdateRateItemDescription(id, txtRateItemName.Text.Trim())

        feeRatesSchedule = Nothing
        LoadFeeRateItems()

        Dim index As Integer? = dgvRateItems.Columns("Key").RowIndexForValue(id)

        If index.HasValue Then
            dgvRateItems.SelectRow(index.Value)
            SelectRateItem(id)
        End If

        Select Case result
            Case DbResult.Success
                lblMessage.ShowMessage("The rate item description was updated.", ErrorLevel.Success)

            Case DbResult.DbError
                lblMessage.ShowMessage("There was an error updating the rate item. Please contact EPD-IT for assistance.", ErrorLevel.Error)
        End Select
    End Sub

    ' New Rate Item

    Private Sub btnAddNewRateItem_Click(sender As Object, e As EventArgs) Handles btnAddNewRateItem.Click
        Using frm As New FinCreateRateItem
            If frm.ShowDialog() = DialogResult.OK Then
                If frm.Success Then
                    feeRatesSchedule = Nothing

                    updating = True
                    cmbCategory.SelectedValue = frm.SelectedCategory
                    updating = False

                    LoadFeeRateItems()

                    Dim index As Integer? = dgvRateItems.Columns("Key").RowIndexForValue(frm.NewRateItemID)

                    If index.HasValue Then
                        dgvRateItems.SelectRow(index.Value)
                        SelectRateItem(frm.NewRateItemID)
                    End If

                    lblMessage.ShowMessage("The new rate item was added.", ErrorLevel.Success)
                Else
                    lblMessage.ShowMessage("There was an error adding the rate item. Please contact EPD-IT for assistance.", ErrorLevel.Error)
                End If
            End If
        End Using
    End Sub

    ' New Effective Rate

    Private Sub btnNewEffectiveRate_Click(sender As Object, e As EventArgs) Handles btnAddNewEffectiveRate.Click
        grpNewRate.Visible = True
        grpEndRateItem.Visible = False
        btnAddNewEffectiveRate.Visible = False
        btnEndRateItem.Visible = False
    End Sub

    Private Sub btnSaveNewEffectiveRate_Click(sender As Object, e As EventArgs) Handles btnSaveNewEffectiveRate.Click
        If txtNewEffectiveRate.Amount <= 0 Then
            lblMessage.ShowMessage("Enter a valid rate.", ErrorLevel.Warning)
            txtNewEffectiveRate.Focus()
            Return
        End If

        Dim id As Integer = selectedRateItem.FeeRateItemID

        Dim result As DbResult = AddNewEffectiveRate(id, txtNewEffectiveRate.Amount, dtpNewEffectiveRateDate.Value)

        feeRatesSchedule = Nothing
        LoadFeeRateItems()

        Dim index As Integer? = dgvRateItems.Columns("Key").RowIndexForValue(id)

        If index.HasValue Then
            dgvRateItems.SelectRow(index.Value)
            SelectRateItem(id)
        End If

        dgvRateItemHistory.FirstDisplayedScrollingRowIndex = dgvRateItemHistory.RowCount - 1

        Select Case result
            Case DbResult.Success
                lblMessage.ShowMessage("The new effective rate was added.", ErrorLevel.Success)

            Case DbResult.DbError
                lblMessage.ShowMessage("There was an error adding the new effective rate. Please contact EPD-IT for assistance.", ErrorLevel.Error)
        End Select
    End Sub

    Private Sub btnCancelNewRate_Click(sender As Object, e As EventArgs) Handles btnCancelNewRate.Click
        grpNewRate.Visible = False
        btnAddNewEffectiveRate.Visible = True
        btnEndRateItem.Visible = True
    End Sub

    ' End Effective Rate

    Private Sub btnEndRateItem_Click(sender As Object, e As EventArgs) Handles btnEndRateItem.Click
        grpNewRate.Visible = False
        grpEndRateItem.Visible = True
        btnAddNewEffectiveRate.Visible = False
        btnEndRateItem.Visible = False
    End Sub

    Private Sub btnSaveEndRateItem_Click(sender As Object, e As EventArgs) Handles btnSaveEndRateItem.Click
        If DialogResult.No = MessageBox.Show(
            "Are you sure you want to discontinue use of this rate item? This cannot be undone.",
            "Confirm",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2) Then
            Return
        End If

        Dim id As Integer = selectedRateItem.FeeRateItemID

        Dim result As DbResult = EndUseOfRateItem(id, dtpEndRateItemDate.Value)

        feeRatesSchedule = Nothing

        updating = True
        chkShowInactive.Checked = True
        updating = False

        LoadFeeRateItems()

        Dim index As Integer? = dgvRateItems.Columns("Key").RowIndexForValue(id)

        If index.HasValue Then
            dgvRateItems.SelectRow(index.Value)
            SelectRateItem(id)
        End If

        dgvRateItemHistory.FirstDisplayedScrollingRowIndex = 0

        Select Case result
            Case DbResult.Success
                lblMessage.ShowMessage("The rate item has been discontinued.", ErrorLevel.Success)

            Case DbResult.DbError
                lblMessage.ShowMessage("There was an error updating the rate item. Please contact EPD-IT for assistance.", ErrorLevel.Error)
        End Select
    End Sub

    Private Sub btnCancelEndRateItem_Click(sender As Object, e As EventArgs) Handles btnCancelEndRateItem.Click
        grpEndRateItem.Visible = False
        btnAddNewEffectiveRate.Visible = True
        btnEndRateItem.Visible = True
    End Sub

    ' Accept button 

    Private Sub grpNewRate_Enter(sender As Object, e As EventArgs) Handles grpNewRate.Enter
        AcceptButton = btnSaveNewEffectiveRate
    End Sub

    Private Sub grpEndRateItem_Enter(sender As Object, e As EventArgs) Handles grpEndRateItem.Enter
        AcceptButton = btnSaveEndRateItem
    End Sub


    Private Sub txtRateItemName_Enter(sender As Object, e As EventArgs) Handles txtRateItemName.Enter
        AcceptButton = btnUpdateRateItemName
    End Sub

    Private Sub NoAcceptButton(sender As Object, e As EventArgs) _
        Handles txtRateItemName.Leave, grpNewRate.Leave, grpEndRateItem.Leave
        AcceptButton = Nothing
    End Sub

End Class