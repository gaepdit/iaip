Imports System.Collections.Generic
Imports Iaip.Apb.Sspp
Imports System.Linq

Public Class SsppPermitRevocationDialog

#Region " Properties "

    ' List of active permits (sent by Application Tracking Log)
    Public Property ActivePermits() As List(Of Permit)

    ' List of checked permits (available to Application Tracking Log after closed)
    Public Property PermitsToRevoke() As List(Of Permit)

#End Region

#Region " Form Events "

    Private Sub SsppPermitRevocationDialog_Load(sender As Object, e As EventArgs) Handles Me.Load
        For Each p As Permit In ActivePermits
            ActivePermitsCheckedListBox.Items.Add(p)
        Next
    End Sub

    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
        Warning.Visible = False

        ' Something has to be selected before proceeding ("None" or at least one permit)
        If Not NoneCheckbox.Checked AndAlso ActivePermitsCheckedListBox.CheckedItems.Count = 0 Then
            Warning.Visible = True
            Return
        End If

        If NoneCheckbox.Checked Then
            PermitsToRevoke = Nothing
        Else
            PermitsToRevoke = New List(Of Permit)
            PermitsToRevoke.AddRange(ActivePermitsCheckedListBox.CheckedItems.OfType(Of Permit))
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub NoneCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles NoneCheckbox.CheckedChanged
        ActivePermitsCheckedListBox.Enabled = Not NoneCheckbox.Checked
        If NoneCheckbox.Checked Then
            While ActivePermitsCheckedListBox.CheckedIndices.Count > 0
                ActivePermitsCheckedListBox.SetItemChecked(ActivePermitsCheckedListBox.CheckedIndices(0), False)
            End While
        End If
    End Sub

#End Region

End Class
