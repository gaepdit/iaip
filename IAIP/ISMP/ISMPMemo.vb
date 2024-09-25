Imports Microsoft.Data.SqlClient

Public Class ISMPMemo

    Private Sub ISMPMemo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If AccountFormAccess(15, 0) = "1" OrElse
            AccountFormAccess(15, 1) = "1" OrElse
            AccountFormAccess(15, 2) = "1" OrElse
            AccountFormAccess(15, 3) = "1" Then
            bbtSave.Enabled = True
        Else
            bbtSave.Enabled = False
        End If
    End Sub

    Private Sub SaveMemo()
        Dim query As String
        Const dashes As String = "--------------------------------------------------------------------------------------------"
        Dim MemoTemp As String

        Try

            If lblReferenceNumber.Text <> "" Then

                query = "Select strReferenceNumber " &
                "from ISMPMaster " &
                "where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", lblReferenceNumber.Text)

                If DB.ValueExists(query, p) Then
                    query = "Select strMemorandumField " &
                    "from ISMPTestREportMemo " &
                    "where strReferenceNumber = @ref "

                    txtMemoIN.Text = TodayFormatted & vbCrLf & txtMemoIN.Text & vbCrLf & CurrentUser.AlphaName & vbCrLf & dashes & vbCrLf

                    If DB.ValueExists(query, p) Then
                        MemoTemp = DB.GetString(query, p) & vbCrLf & txtMemoIN.Text

                        query = "Update ISMPTestREportMemo set " &
                        "strMemorandumField = @memo " &
                        "where strReferenceNumber = @ref "
                    Else
                        MemoTemp = txtMemoIN.Text

                        query = "Insert into ISMPTestREportMemo " &
                        "(strReferenceNumber, strMemorandumField) " &
                        "values " &
                        "(@ref, @memo)"
                    End If

                    Dim p2 As SqlParameter() = {New SqlParameter("@memo", MemoTemp), p}

                    DB.RunCommand(query, p2)
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Public Sub LoadMemo()
        If lblReferenceNumber.Text <> "" Then
            Dim query As String = "Select strMemorandumField " &
            "from ISMPTestREportMemo " &
            "where strReferenceNumber = @ref "

            Dim p As New SqlParameter("@ref", lblReferenceNumber.Text)

            txtMemoOut.Text = DB.GetString(query, p)
        End If
    End Sub

    Private Sub bbtSave_Click(sender As Object, e As EventArgs) Handles bbtSave.Click
        SaveMemo()
        txtMemoOut.Clear()
        LoadMemo()
        txtMemoIN.Clear()
    End Sub
End Class
