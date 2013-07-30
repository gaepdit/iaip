Imports System.Data.OracleClient

Public Class SSCPInspectionTool
    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Private Sub SSCPInspectionTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            LoadDefults()
            LoadAssignedStaff()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadDefults()
        Try
            cboFiscalYear.Items.Add(((Date.Now.Year) + 1).ToString)
            cboFiscalYear.Items.Add(((Date.Now.Year)).ToString)

            SQL = "select " & _
            "distinct intyear  " & _
            "from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED " & _
            "order by intyear desc "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("intyear")) Then
                Else
                    If cboFiscalYear.Items.Contains(dr.Item("intyear").ToString) Then
                    Else
                        cboFiscalYear.Items.Add(dr.Item("intyear").ToString)
                    End If

                End If
            End While
            dr.Close()



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadAssignedStaff()
        Try
            Dim dtAssignedStaff As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select  distinct " & _
           "(strLastName||', '||strFirstName) as UserName,  " & _
           "strUnitDesc, numUserID  " & _
           "from AIRBRANCH.EPDUSERPROFILES, AIRBRANCH.LOOKUPEPDUNITS, " & _
           "AIRbranch.sscpinspectionTracking " & _
           "where AIRBRANCH.EPDUSERPROFILES.NUMUNIT = AIRBRANCH.LOOKUPEPDUNITS.NUMUNITCODE (+)  " & _
           "and (((numProgram = '4'  " & _
           "or STRLASTNAME = 'District')  " & _
           "and NUMEMPLOYEESTATUS = '1')   " & _
           "or AIRBRANCH.EPDUSERPROFILES.NUMUSERID = AIRBRANCH.SSCPINSPECTIONTRACKING.STRINSPECTINGENGINEER ) " & _
           "order by username "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "AssignedStaff")

            dtAssignedStaff.Columns.Add("UserName", GetType(System.String))
            dtAssignedStaff.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow In ds.Tables("AssignedStaff").Rows()
                drNewRow = dtAssignedStaff.NewRow()
                drNewRow("UserName") = drDSRow("UserName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtAssignedStaff.Rows.Add(drNewRow)
            Next

            With clbAssignedStaff
                .DataSource = dtAssignedStaff
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSearchFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchFacilities.Click
        Try



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class