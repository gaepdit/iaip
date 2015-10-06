Imports System.Windows.Forms
Imports System.Collections.Generic
Imports Iaip.Apb.Sspp
Imports System.Linq

Public Class SsppPermitRevocationDialog

#Region " Properties "

    ' List of active permits (sent by Application Tracking Log)
    Private _ActivePermits As List(Of Permit)
    Public Property ActivePermits() As List(Of Permit)
        Private Get
            Return _ActivePermits
        End Get
        Set(ByVal value As List(Of Permit))
            _ActivePermits = value
        End Set
    End Property

    ' List of checked permits (available to Application Tracking Log after closed)
    Private _SelectedPermits As List(Of Permit)
    Public Property PermitsToRevoke() As List(Of Permit)
        Get
            Return _SelectedPermits
        End Get
        Private Set(ByVal value As List(Of Permit))
            _SelectedPermits = value
        End Set
    End Property

#End Region

#Region " Form Events "

    Private Sub SsppPermitRevocationDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        For Each p As Permit In ActivePermits
            ActivePermitsCheckedListBox.Items.Add(p)
        Next
    End Sub

    Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
        Warning.Visible = False

        ' Something has to be selected before proceeding ("None" or at least one permit)
        If Not NoneCheckbox.Checked AndAlso ActivePermitsCheckedListBox.CheckedItems.Count = 0 Then
            Warning.Visible = True
            Exit Sub
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

    Private Sub NoneCheckbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoneCheckbox.CheckedChanged
        ActivePermitsCheckedListBox.Enabled = Not NoneCheckbox.Checked
        If NoneCheckbox.Checked Then
            While ActivePermitsCheckedListBox.CheckedIndices.Count > 0
                ActivePermitsCheckedListBox.SetItemChecked(ActivePermitsCheckedListBox.CheckedIndices(0), False)
            End While
        End If
    End Sub

#End Region

End Class
