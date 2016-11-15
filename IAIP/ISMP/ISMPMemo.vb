Imports System.Data.SqlClient

Public Class ISMPMemo

    Private Sub ISMPMemo_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            LoadMemo()
            If AccountFormAccess(15, 0) = "1" Or AccountFormAccess(15, 1) = "1" Or AccountFormAccess(15, 2) = "1" Or AccountFormAccess(15, 3) = "1" Then
            Else
                TBMemo.Buttons.Remove(TbbSave)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub SaveMemo()
        Dim query As String
        Dim dashes As String = "--------------------------------------------------------------------------------------------"
        Dim MemoTemp As String

        Try

            If txtReferenceNumber.Text <> "" Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                query = "Select strReferenceNumber " &
                "from ISMPMaster " &
                "where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", txtReferenceNumber.Text)

                If DB.ValueExists(query, p) Then
                    query = "Select strMemorandumField " &
                    "from ISMPTestREportMemo " &
                    "where strReferenceNumber = @ref "

                    txtMemoIN.Text = TodayFormatted + vbCrLf + txtMemoIN.Text + vbCrLf + CurrentUser.AlphaName + vbCrLf + dashes + vbCrLf

                    MemoTemp = DB.GetString(query, p)

                    If DB.ValueExists(query, p) Then
                        MemoTemp = DB.GetString(query, p) & vbCrLf & Replace(txtMemoIN.Text, " '", "''")

                        query = "Update ISMPTestREportMemo set " &
                        "strMemorandumField = @memo " &
                        "where strReferenceNumber = @ref "
                    Else
                        MemoTemp = txtMemoIN.Text

                        query = "Insert into ISMPTestREportMemo " &
                        "(strReferenceNumber, strMemorandumField) " &
                        "values " &
                        "(@ref, @memo)'"
                    End If

                    Dim p2 As SqlParameter() = {
                        New SqlParameter("@memo", MemoTemp),
                        p
                    }

                    DB.RunCommand(query, p2)
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadMemo()
        Dim query As String

        Try

            If txtReferenceNumber.Text <> "" Then
                query = "Select strMemorandumField " &
                "from ISMPTestREportMemo " &
                "where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", txtReferenceNumber.Text)

                txtMemoOut.Text = DB.GetString(query, p)

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub TBMemo_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles TBMemo.ButtonClick
        Try

            Select Case TBMemo.Buttons.IndexOf(e.Button)
                Case 0
                    SaveMemo()
                    txtMemoOut.Clear()
                    LoadMemo()
                    txtMemoIN.Clear()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

End Class
