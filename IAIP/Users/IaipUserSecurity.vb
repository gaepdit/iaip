Imports System.Collections.Generic

Public Class IaipUserSecurity

#Region " Properties "

    Private Property savedSessions As DataTable
    Private Property selectedSession As String

#End Region

#Region " Load "

    Private Sub IaipSecurity_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadSavedSessions()
    End Sub

    Private Sub LoadSavedSessions()
        savedSessions = DAL.GetSavedSessions(CurrentUser.UserID)
        dgvSavedSessions.DataSource = savedSessions
        dgvSavedSessions.Columns("SessionId").Visible = False
        dgvSavedSessions.SanelyResizeColumns(minWidth:=120)
        ClearSelection()
        If savedSessions.Rows.Count = 0 Then
            btnRevokeAll.Enabled = False
        Else
            btnRevokeAll.Enabled = True
            AddHandler dgvSavedSessions.CellEnter, AddressOf dgvSavedSessions_CellEnter
            dgvSavedSessions.CurrentCell = dgvSavedSessions.Rows(0).Cells("Computer")
        End If
    End Sub

#End Region

#Region " Button Events "

    Private Sub btnRevokeAll_Click(sender As Object, e As EventArgs) Handles btnRevokeAll.Click
        RevokeAllSessions()
    End Sub

    Private Sub btnRevokeSelection_Click(sender As Object, e As EventArgs) Handles btnRevokeSelection.Click
        RevokeSession()
    End Sub

#End Region

#Region " Grid Events "

    Private Sub dgvSavedSessions_CellEnter(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvSavedSessions.RowCount Then
            selectedSession = dgvSavedSessions.Rows(e.RowIndex).Cells("SessionId").Value.ToString()
            btnRevokeSelection.Enabled = True
        End If
    End Sub

#End Region

    Private Sub RevokeAllSessions()
        If DAL.RevokeAllSessions(CurrentUser.UserID) Then
            ClearGrid()
            ShowMessage(Message.RevokeAllSuccess)
        Else
            LoadSavedSessions()
            ShowMessage(Message.RevokeAllFailure)
        End If
    End Sub

    Private Sub RevokeSession()
        If selectedSession IsNot Nothing Then
            If DAL.RevokeSession(selectedSession) Then
                RemoveSession(selectedSession)
                ShowMessage(Message.RevokeSuccess)
            Else
                LoadSavedSessions()
                ShowMessage(Message.RevokeFailure)
            End If
            ClearSelection()
        End If
    End Sub

    Private Sub ClearSelection()
        dgvSavedSessions.ClearSelection()
        dgvSavedSessions.CurrentCell = Nothing
        selectedSession = Nothing
        btnRevokeSelection.Enabled = False
    End Sub

    Private Sub RemoveSession(sessionId As String)
        savedSessions.AcceptChanges()

        For Each row As DataRow In savedSessions.Rows
            If row.Item("SessionId").ToString() = sessionId Then
                row.Delete()
            End If
        Next

        savedSessions.AcceptChanges()
    End Sub

    Private Sub ShowMessage(message As Message)
        lblStatus.Text = MessageDictionary(message)
    End Sub

    Private Sub ClearGrid()
        RemoveHandler dgvSavedSessions.CellEnter, AddressOf dgvSavedSessions_CellEnter
        savedSessions.Clear()
        ClearSelection()
        btnRevokeAll.Enabled = False
    End Sub

#Region " Data "

    Private Enum Message
        RevokeAllSuccess
        RevokeAllFailure
        RevokeSuccess
        RevokeFailure
    End Enum

    Private ReadOnly MessageDictionary As New Dictionary(Of Message, String) From {
        {Message.RevokeAllFailure, "An error occurred when attempting to revoke all sessions."},
        {Message.RevokeAllSuccess, "All sessions successfully revoked."},
        {Message.RevokeFailure, "An error occurred when attempting to revoke the session."},
        {Message.RevokeSuccess, "Session successfully revoked."}
    }

#End Region

End Class