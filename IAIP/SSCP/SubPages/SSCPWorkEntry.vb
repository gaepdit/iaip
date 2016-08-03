Imports System.Data.SqlClient


Public Class SSCPWorkEnTry
    Inherits BaseForm
    Dim SQL, SQL2 As String
    Dim cmd, cmd2 As SqlCommand
    Dim dr2 As SqlDataReader
    Dim dsCompliance As DataSet
    Dim daCompliance As SqlDataAdapter

    Private Sub SSCPWorkEnTry_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load

        Try

            DTPDateReceived.Value = Date.Today
            LoadDataSets()
            LoadComboBox()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Page Load"
    Private Sub LoadDataSets()
        Dim SQL As String
        Dim cmd As SqlCommand

        Try

            dsCompliance = New DataSet

            SQL = "select strActivityType, strActivityName, strActivityDescription " &
            "from LookUPComplianceActivities " &
            "order by strActivityName"

            daCompliance = New SqlDataAdapter
            cmd = New SqlCommand(SQL, CurrentConnection)

            daCompliance = New SqlDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daCompliance.Fill(dsCompliance, "ComplianceActivity")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadComboBox()
        Dim dtActivity As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            dtActivity.Columns.Add("strActivityName", GetType(System.String))
            dtActivity.Columns.Add("strActivityType", GetType(System.String))

            drNewRow = dtActivity.NewRow()
            drNewRow("strActivityName") = " "
            drNewRow("strActivityType") = " "
            dtActivity.Rows.Add(drNewRow)

            For Each drDSRow In dsCompliance.Tables("ComplianceActivity").Rows()
                drNewRow = dtActivity.NewRow()
                drNewRow("strActivityName") = drDSRow("strActivityName")
                drNewRow("strActivityType") = drDSRow("strActivityType")
                dtActivity.Rows.Add(drNewRow)
            Next

            With cboEvent
                .DataSource = dtActivity
                .DisplayMember = "strActivityName"
                .ValueMember = "strActivityType"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#End Region
#Region "Subs and Functions"
    Sub Save()
        Dim DateReceived As String = DTPDateReceived.Text

        Try

            If cboEvent.Text <> " " Then

                SQL = "Insert into SSCPItemMaster " &
                "(strTrackingNumber, strAIRSnumber, DatReceivedDate, strEventType, " &
                "strModifingPerson, datModifingDate) values " &
                "(SSCPTrackingNumber.nextval, '0413" & txtAIRSNumber.Text & "', '" & DateReceived & "', " &
                "(Select strActivityType from LookUPComplianceActivities where strActivityName = '" & cboEvent.Text & "'), " &
                "'" & CurrentUser.UserID & "', '" & OracleDate & "')"

                SQL2 = "Select SSCPTrackingNumber.Currval from Dual"

                cmd = New SqlCommand(SQL, CurrentConnection)
                cmd2 = New SqlCommand(SQL2, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                cmd.ExecuteReader()

                dr2 = cmd2.ExecuteReader
                While dr2.Read
                    txtTrackingNumber.Text = dr2.Item(0)
                End While



                MsgBox("Done")

            Else
                MsgBox("Please Select an Event type.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub Clear()
        Try

            DTPDateReceived.Value = Date.Today
            cboEvent.Text = " "
            txtTrackingNumber.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub Back()
        Me.Hide()
    End Sub
#End Region

    Private Sub TBComplianceEvents_ButtonClick(sender As System.Object, e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBComplianceEvents.ButtonClick
        Try

            Select Case TBComplianceEvents.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Clear()
                Case 2
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub cboEvent_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cboEvent.SelectedValueChanged
        Dim dtActivity As New DataTable

        Try

            If cboEvent.SelectedIndex = 0 Then
                LabEventDescription.Text = ""
            Else
                dtActivity = dsCompliance.Tables("ComplianceActivity")

                Dim drActivity As DataRow()
                Dim row As DataRow

                drActivity = dtActivity.Select("strActivityName = '" & cboEvent.Text & "'")

                For Each row In drActivity
                    LabEventDescription.Text = row("strActivityDescription").ToString
                Next
            End If

            If LabEventDescription.Text <> "" Then
                LabEventDescription.Visible = True
            Else
                LabEventDescription.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub cboEvent_Leave(sender As Object, e As System.EventArgs) Handles cboEvent.Leave
        Dim dtActivity As New DataTable

        Try

            If cboEvent.SelectedIndex = 0 Then
                LabEventDescription.Text = ""
            Else
                dtActivity = dsCompliance.Tables("ComplianceActivity")

                Dim drActivity As DataRow()
                Dim row As DataRow

                drActivity = dtActivity.Select("strActivityName = '" & cboEvent.Text & "'")

                For Each row In drActivity
                    LabEventDescription.Text = row("strActivityDescription").ToString
                Next
            End If

            If LabEventDescription.Text <> "" Then
                LabEventDescription.Visible = True
            Else
                LabEventDescription.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

End Class
