Imports System.Data.SqlClient
'Imports System.Runtime.InteropServices

Public Class ISMPMemo
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel

    Private Sub ISMPMemo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            TCISMPMemo.TabPages.Remove(TPFuturePrintOption)

            CreateStatusBar()
            LoadMemo()
            If AccountFormAccess(15, 0) = "1" Or AccountFormAccess(15, 1) = "1" Or AccountFormAccess(15, 2) = "1" Or AccountFormAccess(15, 3) = "1" Then
            Else
                TBMemo.Buttons.Remove(TbbSave)
                MmiSave.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub


#Region "Page Load"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Enter memo in the bottom box (limit 4000 characters)..."
            panel2.Text = CurrentUser.AlphaName
            panel3.Text = TodayFormatted

            panel1.AutoSize = StatusBarPanelAutoSize.Spring
            panel2.AutoSize = StatusBarPanelAutoSize.Contents
            panel3.AutoSize = StatusBarPanelAutoSize.Contents

            panel1.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel2.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel3.BorderStyle = StatusBarPanelBorderStyle.Sunken

            panel1.Alignment = HorizontalAlignment.Left
            panel2.Alignment = HorizontalAlignment.Left
            panel3.Alignment = HorizontalAlignment.Right

            ' Display panels in the StatusBar control.
            statusBar1.ShowPanels = True

            ' Add both panels to the StatusBarPanelCollection of the StatusBar.            
            statusBar1.Panels.Add(panel1)
            statusBar1.Panels.Add(panel2)
            statusBar1.Panels.Add(panel3)

            ' Add the StatusBar to the form.
            Me.Controls.Add(statusBar1)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Sub SaveMemo()
        Dim SQL As String
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader
        Dim dashes As String = "--------------------------------------------------------------------------------------------"
        Dim MemoTemp As String

        Try

            If txtReferenceNumber.Text <> "" Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                SQL = "Select strReferenceNumber " &
                "from ISMPMaster " &
                "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                cmd = New SqlCommand(SQL, CurrentConnection)

                dr = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist = True Then
                    SQL = "Select strMemorandumField " &
                    "from ISMPTestREportMemo " &
                    "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                    cmd = New SqlCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader
                    recExist = dr.Read

                    txtMemoIN.Text = TodayFormatted + vbCrLf + txtMemoIN.Text + vbCrLf + CurrentUser.AlphaName + vbCrLf + dashes + vbCrLf

                    If recExist = True Then
                        MemoTemp = dr.Item("StrMemorandumField")
                        MemoTemp = MemoTemp & vbCrLf & Replace(txtMemoIN.Text, " '", "''")

                        SQL = "Update ISMPTestREportMemo set " &
                        "strMemorandumField = '" & Replace(MemoTemp, "'", "''") & "' " &
                        "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                    Else
                        SQL = "Insert into ISMPTestREportMemo " &
                        "(strReferenceNumber, strMemorandumField) " &
                        "values " &
                        "('" & txtReferenceNumber.Text & "', '" & Replace(txtMemoIN.Text, "'", "''") & "')"
                    End If
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadMemo()
        Dim SQL As String
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader

        Try

            If txtReferenceNumber.Text <> "" Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                SQL = "Select strMemorandumField " &
                "from ISMPTestREportMemo " &
                "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                cmd = New SqlCommand(SQL, CurrentConnection)

                dr = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist = True Then
                    txtMemoOut.Text = dr.Item("strMemorandumField")
                End If


            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiCopy_Click(sender As Object, e As EventArgs) Handles MmiCopy.Click
        Try

            SendKeys.Send("^(c)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiCut_Click(sender As Object, e As EventArgs) Handles MmiCut.Click
        Try

            SendKeys.Send("^(x)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiPaste_Click(sender As Object, e As EventArgs) Handles MmiPaste.Click
        Try

            SendKeys.Send("^(v)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub ISMPMemo_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiClose_Click(sender As Object, e As EventArgs) Handles MmiClose.Click
        Try

            Me.Hide()
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
                Case 1
                    Me.Hide()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiSave_Click(sender As Object, e As EventArgs) Handles MmiSave.Click
        Try

            SaveMemo()
            txtMemoOut.Clear()
            LoadMemo()
            txtMemoIN.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub


    Private Sub MenuItem5_Click(sender As Object, e As EventArgs) Handles MenuItem5.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class
