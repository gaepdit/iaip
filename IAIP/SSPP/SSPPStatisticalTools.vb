Imports System.Data.SqlClient


Public Class SSPPStatisticalTools
    'Dim SQL As String
    'Dim cmd As SqlCommand
    'Dim dr As SqlDataReader
    Dim recExist As Boolean
    Dim dsViewCount As DataSet
    Dim daViewCount As SqlDataAdapter
    Dim dsPermittingUnits As DataSet
    Dim daPermittingUnits As SqlDataAdapter
    Dim tempLoad As String

    Dim SQL, SQL2, SQL3, SQL4 As String
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim dsPart60 As DataSet
    Dim daPart60 As SqlDataAdapter
    Dim dsPart61 As DataSet
    Dim daPart61 As SqlDataAdapter
    Dim dsPart63 As DataSet
    Dim daPart63 As SqlDataAdapter
    Dim dsSIP As DataSet
    Dim daSIP As SqlDataAdapter

    Private Sub SSPPStatisticalTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            DTPPermitCountStart.Value = Today
            DTPPermitCountEnd.Value = Today

            SetDateRange()
            LoadEPAReportYear()
            tempLoad = "Load"
            LoadComboBoxs()
            tempLoad = ""

            If cboSSPPUnits.SelectedIndex > -1 Then
                cboSSPPUnits.SelectedIndex = 0
            End If
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load"
    Sub SetDateRange()
        Try

            Dim Month As String = ""
            Dim Year As String = ""

            Year = Now.Date.Year
            Month = Now.Date.Month
            Select Case Month
                Case "1"
                    DTPPermitCountStart.Text = "01-Jan-" & Year
                    DTPPermitCountEnd.Text = "31-Jan-" & Year
                Case "2"
                    DTPPermitCountStart.Text = "01-Feb-" & Year
                    DTPPermitCountEnd.Text = "28-Feb-" & Year
                Case "3"
                    DTPPermitCountStart.Text = "01-Mar-" & Year
                    DTPPermitCountEnd.Text = "31-Mar-" & Year
                Case "4"
                    DTPPermitCountStart.Text = "01-Apr-" & Year
                    DTPPermitCountEnd.Text = "30-Apr-" & Year
                Case "5"
                    DTPPermitCountStart.Text = "01-May-" & Year
                    DTPPermitCountEnd.Text = "31-May-" & Year
                Case "6"
                    DTPPermitCountStart.Text = "01-Jun-" & Year
                    DTPPermitCountEnd.Text = "30-Jun-" & Year
                Case "7"
                    DTPPermitCountStart.Text = "01-Jul-" & Year
                    DTPPermitCountEnd.Text = "31-Jul-" & Year
                Case "8"
                    DTPPermitCountStart.Text = "01-Aug-" & Year
                    DTPPermitCountEnd.Text = "31-Aug-" & Year
                Case "9"
                    DTPPermitCountStart.Text = "01-Sep-" & Year
                    DTPPermitCountEnd.Text = "30-Sep-" & Year
                Case "10"
                    DTPPermitCountStart.Text = "01-Oct-" & Year
                    DTPPermitCountEnd.Text = "31-Oct-" & Year
                Case "11"
                    DTPPermitCountStart.Text = "01-Nov-" & Year
                    DTPPermitCountEnd.Text = "30-Nov-" & Year
                Case "12"
                    DTPPermitCountStart.Text = "01-Dec-" & Year
                    DTPPermitCountEnd.Text = "31-Dec-" & Year
                Case Else
                    DTPPermitCountStart.Value = Today
                    DTPPermitCountEnd.Value = Today
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Sub LoadEPAReportYear()
        Try


            Dim i As Integer = 0
            Dim Year As String

            Year = Now.Year
            cboEPAYear.Items.Add(Now.AddMonths(12).Year)

            Do Until Year = "2005"
                cboEPAYear.Items.Add(Year)
                i += 12
                Year = Now.AddMonths(-i).Year
            Loop

            cboEPAYear.Text = Now.Year
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Sub LoadComboBoxs()
        Try

            Dim dtPermittingUnits As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "select " &
            "strUnitDesc, numUnitCode  " &
            "from LookUpEPDUnits  " &
            "where numProgramCode = '5'  " &
            "order by strUnitDesc "

            dsPermittingUnits = New DataSet
            daPermittingUnits = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daPermittingUnits.Fill(dsPermittingUnits, "PermittingUnits")

            dtPermittingUnits.Columns.Add("strUnitDesc", GetType(System.String))
            dtPermittingUnits.Columns.Add("numUnitCode", GetType(System.String))

            For Each drDSRow In dsPermittingUnits.Tables("PermittingUnits").Rows()
                drNewRow = dtPermittingUnits.NewRow
                drNewRow("strUnitDesc") = drDSRow("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow("numUnitCode")
                dtPermittingUnits.Rows.Add(drNewRow)
            Next

            With cboSSPPUnits
                .DataSource = dtPermittingUnits
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 4
            End With

            With cboSSPPUnits2
                .DataSource = dtPermittingUnits
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 4
            End With

            With cboSSPPUnits3
                .DataSource = dtPermittingUnits
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 4
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
#End Region
#Region "Subs and Functions"
    Sub RunPermitsIssued()
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim MedianTime As String = 0
            'Dim PercentileTime As String = 0
            Dim n As Integer = 0
            Dim MedianArray(n) As Decimal
            Dim EngineerLine As String = ""

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            SQL = "select count(*) as TVInitial " &
            "from SSPPApplicationMaster, SSPPApplicationTracking, " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '14' " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
            "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
            "or strPermitType = '13') " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtTitleVInitialCount.Text = dr.Item("TVInitial")
            End While
            dr.Close()

            If txtTitleVInitialCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and strApplicationType = '14'  " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
                "or strPermitType = '13') " &
                EngineerLine &
                "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtTVInitialMedian.Text = MedianTime
            Else
                txtTVInitialMedian.Text = "0"
            End If

            SQL = "select count(*) as TVRenewal " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,   " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '16' " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
            "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
            "or strPermitType = '13') " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtTitleVRenewalCount.Text = dr.Item("TVRenewal")
            End While
            dr.Close()

            If txtTitleVRenewalCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and strApplicationType = '16'  " &
                    "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                    "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
                    "or strPermitType = '13') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtTVRenewalMedian.Text = MedianTime
            Else
                txtTVRenewalMedian.Text = "0"
            End If

            If txtTitleVInitialCount.Text <> "0" And txtTitleVRenewalCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and (strApplicationType = '16' or strApplicationType = '14') " &
                    "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                    "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
                    "or strPermitType = '13') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtTVTotalMedian.Text = MedianTime
            Else
                txtTVTotalMedian.Text = "0"
            End If

            SQL = "select count(*) as SigMod " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '22' or strApplicationType = '21') " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
            "and (strPermitType = '4' or strPermitType = '7') " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtSigModCount.Text = dr.Item("SigMod")
            End While
            dr.Close()

            If txtSigModCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and (strApplicationType = '22' or strApplicationType = '21') " &
                    "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                    "and (strPermitType = '4' or strPermitType = '7') " &
                    EngineerLine &
                    "Order by Diff desc "


                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtSigModMedian.Text = MedianTime
            Else
                txtSigModMedian.Text = "0"
            End If

            SQL = "select count(*) as MinorMod " &
            "from SSPPApplicationMaster, SSPPApplicationTracking, " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '19' or strApplicationType = '20') " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
            "and (strPermitType = '4' or strPermitType = '7') " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtMinorModCount.Text = dr.Item("MinorMod")
            End While
            dr.Close()

            If txtMinorModCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and (strApplicationType = '19' or strApplicationType = '20') " &
                    "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                    "and (strPermitType = '4' or strPermitType = '7') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtMinorMedian.Text = MedianTime
            Else
                txtMinorMedian.Text = "0"
            End If

            SQL = "select count(*) as Mod502 " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '15' " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
            "and (strPermitType = '4' or strPermitType = '7') " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txt502Count.Text = dr.Item("Mod502")
            End While
            dr.Close()

            If txt502Count.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and strApplicationType = '15' " &
                    "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                    "and (strPermitType = '4' or strPermitType = '7') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txt502Median.Text = MedianTime
            Else
                txt502Median.Text = "0"
            End If

            SQL = "select count(*) as AA " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '26' " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
            "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '1') " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtAACount.Text = dr.Item("AA")
            End While
            dr.Close()


            If txtAACount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                        "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                        "EPDUserProfiles " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                        "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                        "and strApplicationType = '26' " &
                        "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                        "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '1') " &
                        EngineerLine &
                        "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtAAMedian.Text = MedianTime
            Else
                txtAAMedian.Text = "0"
            End If

            SQL = "select count(*) as SM " &
            "from SSPPApplicationMaster, SSPPApplicationTracking, " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '12' " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
            "and (strPermitType = '4' or strPermitType = '7') " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtSMCount.Text = dr.Item("SM")
            End While
            dr.Close()

            If txtSMCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and strApplicationType = '12' " &
                    "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                    "and (strPermitType = '4' or strPermitType = '7') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtSMMedian.Text = MedianTime

            Else
                txtSMMedian.Text = "0"
            End If

            SQL = "select count(*) as PBR " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '9' " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
            "and strPermitType = '6' " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtPBRCount.Text = dr.Item("PBR")
            End While
            dr.Close()

            If txtPBRCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and strApplicationType = '9' " &
                    "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                    "and strPermitType = '6' " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtPBRMedian.Text = MedianTime
            Else
                txtPBRMedian.Text = "0"
            End If

            SQL = "select count(*) as Other " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '11' OR strApplicationType = '8' " &
            "OR strApplicationType = '4' OR strapplicationType = '3' " &
            "OR strApplicationType = '25' OR strApplicationType = '2') " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
            "and (strPermitType = '7' or strPermitType = '4') " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtOtherCount.Text = dr.Item("Other")
            End While
            dr.Close()

            If txtOtherCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and (strApplicationType = '11' OR strApplicationType = '8' " &
                    "OR strApplicationType = '4' OR strapplicationType = '3' " &
                    "OR strApplicationType = '25' OR strApplicationType = '2') " &
                    "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                    "and (strPermitType = '7' or strPermitType = '4') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtOtherMedian.Text = MedianTime

            Else
                txtOtherMedian.Text = "0"
            End If

            SQL = "select count(*) as Closed " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datPermitIssued IS not Null  " &
            "and datPermitIssued > '" & FirstDay & "' and datPermitIssued < '" & LastDay & "' " &
            "and strPermitType <> '4' " &
            "and strPermitType <> '7' " &
            "and strPermitType <> '12' " &
            "and strPermitType <> '13' " &
            "and strPermitType <> '6' " &
            EngineerLine


            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtNonPermitCount.Text = dr.Item("Closed")
            End While
            dr.Close()

            If txtNonPermitCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and datPermitIssued IS not Null  " &
                    "and datPermitIssued > '" & FirstDay & "' and datPermitIssued < '" & LastDay & "' " &
                    "and strPermitType <> '4' " &
                    "and strPermitType <> '7' " &
                    "and strPermitType <> '12' " &
                    "and strPermitType <> '13' " &
                    "and strPermitType <> '6' " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtNonPermitMedian.Text = MedianTime
            Else
                txtNonPermitMedian.Text = "0"
            End If

            SQL = "select count(*) as PSD " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "SSPPApplicationData,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
            "and SUBSTRING(strTrackedRules, 1, 1) = '1'  " &
            "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "'  " &
            "and strPermitType <> '9' " &
            "and strPermitType <> '10' " &
            "and strPermitType <> '11' " &
            EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtPSDCount.Text = dr.Item("PSD")
            End While
            dr.Close()

            If txtPSDCount.Text <> "0" Then
                SQL = "select (datPermitIssued - datReceivedDate) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "SSPPApplicationData,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SUBSTRING(strTrackedRules, 1, 1) = '1'  " &
                    "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                    "and strPermitType <> '9' " &
                    "and strPermitType <> '10' " &
                    "and strPermitType <> '11' " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                While dr.Read
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = CInt(dr.Item("Diff"))
                    n = n + 1
                End While
                dr.Close()

                Array.Sort(MedianArray)

                If MedianArray.GetLength(0) Mod 2 = 0 Then
                    MedianTime = (MedianArray((MedianArray.GetLength(0) / 2) - 1) + MedianArray((MedianArray.GetLength(0) / 2))) / 2
                Else
                    MedianTime = MedianArray(MedianArray.GetLength(0) \ 2)
                End If
                txtPSDMedian.Text = MedianTime
            Else
                txtPSDMedian.Text = "0"
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Sub RunOpenApplications()
        Try

            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If cboSSPPUnits2.Text = "SSPP Administrative" Then
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers2.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            SQL = "select count(*) as OpenCount " &
            "from SSPPApplicationMaster,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtAllOpenCount.Text = dr.Item("OpenCount")
            End While
            dr.Close()

            SQL = "select count(*) as OpenDOCount " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datToDirector is Not Null  " &
            "and (datDraftIssued is Null or datDraftIssued < datToDirector) " &
                    EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtToDOCount.Text = dr.Item("OpenDOCount")
            End While
            dr.Close()

            SQL = "select count(*) as OpenBCCount " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datToBranchCheif is Not Null  " &
            "and datToDirector is Null  " &
            "and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) " &
                    EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtToBCCount.Text = dr.Item("OpenBCCount")
            End While
            dr.Close()

            SQL = "select count(*) as Open45Days " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datEPAEnds is Not Null  " &
            "and datDraftIssued is Not Null " &
                    EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtOpen45DayCount.Text = dr.Item("Open45Days")
            End While
            dr.Close()

            SQL = "select count(*) as OpenPublicNotice " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datPNExpires is Not Null and datPNExpires < GETDATE() " &
            "and datEPAEnds is Null  " &
                    EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtPublicNoticeCount.Text = dr.Item("OpenPublicNotice")
            End While
            dr.Close()

            SQL = "select count(*) as OpenDraftIssued " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and ((datPNExpires is Not Null and datPNExpires >= GETDATE())  " &
            "or (datDraftIssued is not Null and datPNExpires is Null))  " &
            "and datToBranchCheif is Null  " &
            "and datToDirector is Null  " &
            "and datEPAEnds is Null  " &
                    EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtDraftIssuedCount.Text = dr.Item("OpenDraftIssued")
            End While
            dr.Close()

            SQL = "select count(*) as OpenPMIICount " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datToBranchCheif is Null  " &
            "and datToDirector is Null  " &
            "and datEPAEnds is Null  " &
            "and datPNExpires is Null  " &
            "and datDraftIssued is Null " &
            "and datToPMII is Not Null " &
                    EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtToPMCount.Text = dr.Item("OpenPMIICount")
            End While
            dr.Close()

            SQL = "select count(*) as OpenPMICount " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datToBranchCheif is Null  " &
            "and datToDirector is Null  " &
            "and datEPAEnds is Null  " &
            "and datPNExpires is Null  " &
            "and datDraftIssued is Null " &
            "and datToPMII is Null " &
            "and datToPMI is Not Null " &
                    EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtToUCCount.Text = dr.Item("OpenPMICount")
            End While
            dr.Close()

            SQL = "select count(*) as OpenStaffCount " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datToBranchCheif is Null  " &
            "and datToDirector is Null  " &
            "and datEPAEnds is Null  " &
            "and datPNExpires is Null  " &
            "and datDraftIssued is Null " &
            "and datToPMII is Null " &
            "and datToPMI is Null " &
                    EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtWStaffCount.Text = dr.Item("OpenStaffCount")
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Sub RunTVAgeOfApplications()
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            SQL = "Select count(*) as TVTotalOpen " &
            "from SSPPApplicationMaster,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '14' or strApplicationType = '16' " &
            "or strApplicationType = '27' or strApplicationType = '17') " &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtTVTotalOpenCount.Text = dr.Item("TVTotalOpen")
            End While
            dr.Close()

            SQL = "Select count(*) as TVYearOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '14' or strApplicationType = '16'   " &
            "or strApplicationType = '27' or strApplicationType = '17') " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate > DATEADD(month, -12, GETDATE())  " &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtTVOneYearCount.Text = dr.Item("TVYearOpen")
            End While
            dr.Close()

            SQL = "Select count(*) as TV12MonthsOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numuserID " &
            "and (strApplicationType = '14' or strApplicationType = '16'   " &
            "or strApplicationType = '27' or strApplicationType = '17') " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate >= DATEADD(month, -18, GETDATE()) " &
            "and datReceivedDate < DATEADD(month, -12, GETDATE()) " &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtTVTwelveCount.Text = dr.Item("TV12MonthsOpen")
            End While
            dr.Close()

            SQL = "Select count(*) as TV18MonthsOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '14' or strApplicationType = '16'   " &
            "or strApplicationType = '27' or strApplicationType = '17') " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate < DATEADD(month, -18, GETDATE())" &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtTVGreaterCount.Text = dr.Item("TV18MonthsOpen")
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Sub RunNonTVAgeOfApplications()
        Try


            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            SQL = "Select count(*) as NonTVTotalOpen " &
            "from SSPPApplicationMaster,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType <> '16' and strApplicationType <> '14' " &
            "and strApplicationType <> '17' and strApplicationType <> '27' " &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtTotalOpenCount.Text = dr.Item("NonTVTotalOpen")
            End While
            dr.Close()

            SQL = "Select count(*) as NonTVThreeMonthOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType <> '16' and strApplicationType <> '14' " &
            "and strApplicationType <> '17' and strApplicationType <> '27' " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate >= DATEADD(month, -3, GETDATE())  " &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtThreeMonthOpenCount.Text = dr.Item("NonTVThreeMonthOpen")
            End While
            dr.Close()

            SQL = "Select count(*) as NonTVSixMonthsOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType <> '16' and strApplicationType <> '14' " &
            "and strApplicationType <> '17' and strApplicationType <> '27' " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate >= DATEADD(month, -6, GETDATE()) " &
            "and datReceivedDate < DATEADD(month, -3, GETDATE()) " &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtSixMonthOpenCount.Text = dr.Item("NonTVSixMonthsOpen")
            End While
            dr.Close()

            SQL = "Select count(*) as NonTVNineMonthsOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,   " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType <> '16' and strApplicationType <> '14' " &
            "and strApplicationType <> '17' and strApplicationType <> '27' " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate >= DATEADD(month, -9, GETDATE()) " &
            "and datReceivedDate < DATEADD(month, -6, GETDATE()) " &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtNineMonthOpenCount.Text = dr.Item("NonTVNineMonthsOpen")
            End While
            dr.Close()

            SQL = "Select count(*) as NonTVTwelveMonthsOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking, " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType <> '16' and strApplicationType <> '14' " &
            "and strApplicationType <> '17' and strApplicationType <> '27' " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate >= DATEADD(month, -12, GETDATE()) " &
            "and datReceivedDate < DATEADD(month, -9, GETDATE()) " &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtTwelveMonthOpenCount.Text = dr.Item("NonTVTwelveMonthsOpen")
            End While
            dr.Close()

            SQL = "Select count(*) as NonTVGreaterThanOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking, " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType <> '16' and strApplicationType <> '14' " &
            "and strApplicationType <> '17' and strApplicationType <> '27' " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate < DATEADD(month, -12, GETDATE())" &
                EngineerLine

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtGreaterThanOpenCount.Text = dr.Item("NonTVGreaterThanOpen")
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Sub RunEPAReport()
        Try

            Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            txtEPA1a.Text = "N/A"
            txtEPA1b.Text = "N/A"
            txtEPA1c.Text = "N/A"

            SQL = "select (EPA2ab + EPA2aa) as EPA2a " &
            "from " &
            "(select count(*) as EPA2aa " &
            "from APBHeaderData, APBSupplamentalData  " &
            "where APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
            "AND (SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
            "and (strEPATOPSExcluded is null or strEPATOPSExcluded = 'False')  " &
            "and strOperationalStatus = 'O')) EPA2a1,  " &
            "(select count(*) as EPA2ab " &
            "from APBHeaderData, APBSupplamentalData,  " &
            "SSPPApplicationMaster, SSPPApplicationTracking  " &
            "where APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
            "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber (+)  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strAPplicationNumber  " &
            "AND (SUBSTRING(strAirProgramCodes, 13, 1) <> '1'  " &
            "and datPermitIssued is null  " &
            "and strApplicationType = '14'  " &
            "and datFinalizeddate is null " &
            "and (strEPATOPSExcluded is null or strEPATOPSExcluded = 'False'))) EPA2a2 "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA2a.Text = dr.Item("EPA2a")
            End While
            dr.Close()

            txtEPA2b.Text = "0"
            txtEPA2c.Text = (CInt(txtEPA2a.Text) + CInt(txtEPA2b.Text)).ToString

            SQL = "select (EPA2db + EPA2da) as EPA2d " &
            "from " &
            "(select count(*) as EPA2da " &
            "from APBHeaderData  " &
            "where (SUBSTRING(strAirProgramCodes, 13, 1) = '1' " &
            "and strOperationalStatus = 'O')) EPA2d1,  " &
            "(select count(*) as EPA2db " &
            "from APBHeaderData, " &
            "SSPPApplicationMaster, SSPPApplicationTracking  " &
            "where APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber (+) " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strAPplicationNumber  " &
            "AND (SUBSTRING(strAirProgramCodes, 13, 1) <> '1'  " &
            "and datPermitIssued is null  " &
            "and strApplicationType = '14'  " &
            "and datFinalizeddate is null )) EPA2d2 "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA2d.Text = dr.Item("EPA2d")
            End While
            dr.Close()

            SQL = "select count(*) as EPA3a " &
            "from APBHeaderData " &
            "where SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
            "and strOperationalStatus = 'O' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA3a.Text = dr.Item("EPA3a")
            End While
            dr.Close()

            SQL = "SELECT COUNT(*) AS EPA4a " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND strApplicationType = '14'  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > '" & StartDate & "' " &
            "AND datPermitIssued < '" & EndDate & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA4a.Text = dr.Item("EPA4a")
            End While
            dr.Close()

            SQL = "SELECT COUNT(*) AS EPA4b " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND strApplicationType = '14'  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > '" & StartDate & "' " &
            "AND datPermitIssued < '" & EndDate & "' " &
            "and datReceivedDate > DATEADD(month, -18, datPermitIssued) "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA4b.Text = dr.Item("EPA4b")
            End While
            dr.Close()

            If txtEPA4a.Text = "0" Then
                txtEPA4a.Text = "N/A"
                txtEPA4b.Text = "0"
            End If

            SQL = "SELECT COUNT(*) AS EPA5a " &
            "from SSPPApplicationMaster, " &
            "SSPPApplicationTracking, SSPPApplicationData " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "AND strApplicationType = '14' " &
            "and datPermitIssued is Null " &
            "and datReceivedDate < DATEADD(month, -18, '" & EndDate & "') "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA5a.Text = dr.Item("EPA5a")
            End While
            dr.Close()

            SQL = "Select Count(*) as EPA6a " &
            "From " &
            "(select " &
            "distinct(SSPPApplicationMaster.strAIRSnumber) as AIRSNumber,  " &
            "MaxDate " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, APBHeaderData,  " &
            "(select  " &
            "strAIRSNumber,  " &
            "max(datEffective) as MaxDate  " &
            "from SSPPApplicationMaster, SSPPApplicationTracking  " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and datEffective is not null  " &
            "group by strAIRSnumber) Effect,  " &
            "(Select  " &
            "distinct(SSPPApplicationMaster.strAIRSnumber) as AIRSNumber " &
            "from SSPPApplicationMaster, SSPPApplicationTracking  " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and datReceiveddate < DATEADD(month, -6, '" & EndDate & "')  " &
            "and datReceivedDate > DATEADD(month, -54, '" & EndDate & "')  " &
            "and strApplicationType <> '16'  " &
            "and strApplicationType <> '12') PermitRequests   " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber " &
            "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber   " &
            "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
            "and MaxDate = SSPPApplicationTracking.datEffective " &
            "and maxDate < DATEADD(month, -54, '" & EndDate & "') " &
            "and strOperationalStatus = 'O'  " &
            "and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
            "and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber) "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA6a.Text = dr.Item("EPA6a")
            End While
            dr.Close()

            SQL = "Select Count(*) as EPA6b " &
            "From " &
            "(select " &
            "distinct(SUBSTRING(SSPPApplicationMaster.strAIRSnumber, 5,8) ) as AIRSNumber,  " &
            "strFacilityName, " &
            "MaxDate " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, APBHeaderData,  " &
            "APBFacilityInformation,   " &
            "(select  " &
            "strAIRSNumber,  " &
            "max(datEffective) as MaxDate  " &
            "from SSPPApplicationMaster, SSPPApplicationTracking  " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and datEffective is not null  " &
            "group by strAIRSnumber) Effect,  " &
            "(Select  " &
            "distinct(SSPPApplicationMaster.strAIRSnumber) as AIRSNumber " &
            "from SSPPApplicationMaster, SSPPApplicationTracking  " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and datReceiveddate < DATEADD(month, -6, '" & EndDate & "')  " &
            "and datReceivedDate > DATEADD(month, -54, '" & EndDate & "')  " &
            "and (strApplicationType = '16' or strApplicationType = '12')) PermitRequests   " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber " &
            "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber   " &
            "and APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
            "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
            "and MaxDate = SSPPApplicationTracking.datEffective " &
            "and maxDate < DATEADD(month, -54, '" & EndDate & "') " &
            "and strOperationalStatus = 'O'  " &
            "and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
            "and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber)  "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA6b.Text = dr.Item("EPA6b")
            End While
            dr.Close()


            SQL = "select count(*) as EPA6C " &
"from (Select *  From  " &
"(select  " &
"distinct(SUBSTRING(SSPPApplicationMaster.strAIRSnumber, 5,8)) as AIRSNumber,   " &
"MaxDate  " &
"from SSPPApplicationMaster,  SSPPApplicationTracking,  " &
"APBHeaderData,   " &
"(select  strAIRSNumber,  " &
"max(datEffective) as MaxDate   " &
"from SSPPApplicationMaster, SSPPApplicationTracking   " &
"where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber   " &
"and datEffective is not null  GROUP BY strAIRSNumber) Effect,   " &
"(Select  distinct(SSPPApplicationMaster.strAIRSnumber) as AIRSNumber  " &
"from SSPPApplicationMaster, SSPPApplicationTracking   " &
"where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
"and datReceiveddate < DATEADD(month, -6, '" & EndDate & "')   " &
"and datReceivedDate > DATEADD(month, -54, '" & EndDate & "')   " &
"and strApplicationType <> '16'   " &
"and strApplicationType <> '12') PermitRequests    " &
"where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber  " &
"and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber    " &
"and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber   " &
"and MaxDate = SSPPApplicationTracking.datEffective  " &
"and maxDate < DATEADD(month, -54, '" & EndDate & "') " &
"and strOperationalStatus = 'O'   " &
"and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
"and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber))  EPA6A " &
"where not exists  " &
"(select * from (Select *   " &
"From (select distinct(SUBSTRING(SSPPApplicationMaster.strAIRSnumber, 5,8) ) as AIRSNumber,   " &
"strFacilityName, MaxDate from SSPPApplicationMaster,  SSPPApplicationTracking, APBHeaderData,   " &
"APBFacilityInformation,   (select  strAIRSNumber,  max(datEffective) as MaxDate  from SSPPApplicationMaster,  " &
"SSPPApplicationTracking   " &
"where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber   " &
"and datEffective is not null  group by strAIRSnumber) Effect,   " &
"(Select  distinct(SSPPApplicationMaster.strAIRSnumber) as AIRSNumber from SSPPApplicationMaster,  " &
"SSPPApplicationTracking   " &
"where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber   " &
"and datReceiveddate < DATEADD(month, -6, '" & EndDate & "')  and datReceivedDate > DATEADD(month, -54, '" & EndDate & "')   " &
"and (strApplicationType = '16' or strApplicationType = '12')) PermitRequests    " &
"where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber  " &
"and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber    " &
"and APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
 "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
 "and MaxDate = SSPPApplicationTracking.datEffective  " &
"and maxDate < DATEADD(month, -54, '" & EndDate & "')  " &
"and strOperationalStatus = 'O'  and SUBSTRING(strAirProgramCodes, 13, 1) = '1'   " &
"and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber)  ) EPA6b where  EPA6A.airsnumber = EPA6b.airsNumber) "


            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA6C.Text = dr.Item("EPA6C")
            End While
            dr.Close()





            SQL = "SELECT COUNT(*) AS EPA7a " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND (strApplicationType = '22' or strApplicationType = '21')  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > '" & StartDate & "' " &
            "AND datPermitIssued < '" & EndDate & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA7a.Text = dr.Item("EPA7a")
            End While
            dr.Close()

            SQL = "SELECT COUNT(*) AS EPA7b " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND (strApplicationType = '22' or strApplicationType = '21')  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > '" & StartDate & "' " &
            "AND datPermitIssued < '" & EndDate & "' " &
            "and datReceivedDate > DATEADD(month, -18, datPermitIssued) "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA7b.Text = dr.Item("EPA7b")
            End While
            dr.Close()

            SQL = "SELECT COUNT(*) AS EPA7c " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND (strApplicationType = '22' or strApplicationType = '21')  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > '" & StartDate & "' " &
            "AND datPermitIssued < '" & EndDate & "' " &
            "and datReceivedDate > DATEADD(month, -9, datPermitIssued) "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA7c.Text = dr.Item("EPA7c")
            End While
            dr.Close()

            If txtEPA7a.Text = "0" Then
                txtEPA7a.Text = "0"
                txtEPA7b.Text = "N/A"
                txtEPA7c.Text = "N/A"
            End If

            SQL = "SELECT COUNT(*) AS EPA8a " &
            "from SSPPApplicationMaster, " &
            "SSPPApplicationTracking, SSPPApplicationData " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "AND (strApplicationType = '22' or strApplicationType = '21')  " &
            "and datPermitIssued is Null " &
            "and datReceivedDate < DATEADD(month, -18, '" & EndDate & "')"

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                txtEPA8a.Text = dr.Item("EPA8a")
            End While
            dr.Close()

            txtEPA9a.Text = "No Comments"

            UpdateEPAReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub
    Sub UpdateEPAReport()
        Try


            txtEPAReportText.Clear()
            txtEPAReportText.Text = "1. Outstanding Permit Issuance " & vbCrLf &
            vbTab & "a) Number of final Actions: " & txtEPA1a.Text & vbCrLf &
            vbTab & "b) Total commitment universe: " & txtEPA1b.Text & vbCrLf &
            vbTab & "c) Date commitment completed (if applicable): " & txtEPA1c.Text & vbCrLf & vbCrLf &
            "2. Total Current Part 70 Source Universe and Permit Universe " & vbCrLf &
            vbTab & "a) Number of active part 70 sources that have obtained part 70 permits, plus the number of active part 70 sources that have not yet obtained part 70 permits: " & txtEPA2a.Text & vbCrLf &
            vbTab & "b) Nubmer of part 70 sources that have applied to obtain a synthetic minor restriction in lieu of a part 70 permit, and the part 70 program's permit application due dates for those sources have passed: " & txtEPA2b.Text & vbCrLf &
            vbTab & "c) Total number of current part 70 sources (a+b): " & txtEPA2c.Text & vbCrLf &
            vbTab & "d) For permitting authorities that issue multiple part 70 permits to a single source: total number of active part 70 permits issued, plus part 70 permits applied for: " & txtEPA2d.Text & vbCrLf & vbCrLf &
            "3. Total Active Part 70 Permits " & vbCrLf &
            vbTab & "Total number of active part 70 permits: " & txtEPA3a.Text & vbCrLf & vbCrLf &
            "4. Timeliness of Initial Permits (PART element) " & vbCrLf &
            vbTab & "a) Total number of initial part 70 permits issued during 6 month reporting period: " & txtEPA4a.Text & vbCrLf &
            vbTab & "b) Number of initial part 70 permits finalized during 6 month reporting period that were issued within 18 months: " & txtEPA4b.Text & vbCrLf & vbCrLf &
            "5. Total Outstanding Initial Part 70 Applications " & vbCrLf &
            vbTab & "The number of active initial part 70 applications older than 18 months: " & txtEPA5a.Text & vbCrLf & vbCrLf &
            "6. Outstanding Renewal Permit Actions " & vbCrLf &
            vbTab & "a) Total number of expired permits for active part 70 sources: " & txtEPA6a.Text & vbCrLf &
            vbTab & "b) Total number of active permits with terms extended past 5 years: " & txtEPA6b.Text & vbCrLf & vbCrLf &
            "7. Timeliness of Significant Modifications (PART element - a and b only) " & vbCrLf &
            vbTab & "a) Total number of significant modifications issued during 6 month reporting period: " & txtEPA7a.Text & vbCrLf &
            vbTab & "b) Number of significant modifications finalized during 6 month reporting period that were issued within 18 months: " & txtEPA7b.Text & vbCrLf &
            vbTab & "c) Number of significant modifications finalized during 6 month reporting period that were issued within 9 months: " & txtEPA7c.Text & vbCrLf & vbCrLf &
            "8. Outstanding Significant Permit Modifications" & vbCrLf &
            vbTab & "Total number of active significant modification applications older than 18 months: " & txtEPA8a.Text & vbCrLf & vbCrLf &
            "9. Comments and Additional Information" & vbCrLf &
            vbTab & txtEPA9a.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
#End Region
#Region "Declarations"
    Private Sub SSPPApplicationLog_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnRunReport_Click(sender As Object, e As EventArgs) Handles btnRunReport.Click
        Try


            RunPermitsIssued()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewTVCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewTVCount.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtTitleVInitialCount.Text <> "0" And txtTitleVInitialCount.Text <> "") Or
                 (txtTitleVRenewalCount.Text <> "0" And txtTitleVRenewalCount.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc, " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes, " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                "and (strApplicationType = '14' or strApplicationType = '16')  " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "'  " &
                "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
                "or strPermitType = '13') " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub dgvApplicationCount_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvApplicationCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvApplicationCount.HitTest(e.X, e.Y)

        Try


            If dgvApplicationCount.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvApplicationCount.Columns(0).HeaderText = "APL #" Then
                    If IsDBNull(dgvApplicationCount(0, hti.RowIndex).Value) Then
                        txtRecordNumber.Clear()
                    Else
                        txtRecordNumber.Text = dgvApplicationCount(0, hti.RowIndex).Value
                    End If
                Else
                    If dgvApplicationCount.Columns(0).HeaderText = "AIRS #" Then
                        If IsDBNull(dgvApplicationCount(0, hti.RowIndex).Value) Then
                            txtRecordNumber.Clear()
                        Else
                            txtRecordNumber.Text = dgvApplicationCount(0, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnViewAppLogCount_Click(sender As Object, e As EventArgs) Handles btnViewAppLogCount.Click
        Try

            If txtRecordNumber.Text <> "" Then
                If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtRecordNumber.Text) Then
                    OpenFormFacilitySummary(txtRecordNumber.Text)
                Else
                    OpenFormPermitApplication(txtRecordNumber.Text)
                End If
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub lblViewSigModCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewSigModCount.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtSigModCount.Text <> "0" And txtSigModCount.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,     " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                "and (strApplicationType = '22' or strApplicationType = '21')  " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "'  " &
                "and (strPermitType = '4' or strPermitType = '7') " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewMinorModCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewMinorModCount.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtMinorModCount.Text <> "0" And txtMinorModCount.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,     " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                "and (strApplicationType = '20' or strApplicationType = '19')  " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "'  " &
                "and (strPermitType = '4' or strPermitType = '7') " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbView502Count_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbView502Count.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txt502Count.Text <> "0" And txt502Count.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,     " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                "and strApplicationType = '15' " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "'  " &
                "and (strPermitType = '4' or strPermitType = '7') " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewAACount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewAACount.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtAACount.Text <> "0" And txtAACount.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastName||', '||strFirstname) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,     " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                "and strApplicationType = '26' " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "'  " &
                "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '1') " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewSMCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewSMCount.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtSMCount.Text <> "0" And txtSMCount.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,     " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                "and strApplicationType = '12' " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "'  " &
                 "and (strPermitType = '4' or strPermitType = '7') " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewPBRCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewPBRCount.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtPBRCount.Text <> "0" And txtPBRCount.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,     " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                "and strApplicationType = '9' " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "' " &
                "and strPermitType = '6' " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewOtherCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewOtherCount.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtOtherCount.Text <> "0" And txtOtherCount.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,    " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.nuMUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                "and (strApplicationType = '11' OR strApplicationType = '8' " &
                "OR strApplicationType = '4' OR strapplicationType = '3' " &
                "OR strApplicationType = '25' OR strApplicationType = '2') " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "'  " &
                "and (strPermitType = '7' or strPermitType = '4') " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewNoPermitCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewNoPermitCount.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtNonPermitCount.Text <> "0" And txtNonPermitCount.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "case " &
                "when strApplicationTypeDesc is Null  then '' " &
                "else strApplicationTypeDesc " &
                "End strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastname||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datPermitIssued IS not Null  " &
                "and datPermitIssued > '" & FirstDay & "' and datPermitIssued < '" & LastDay & "'  " &
                "and strPermitType <> '4' " &
                "and strPermitType <> '7' " &
                "and strPermitType <> '12' " &
                "and strPermitType <> '13' " &
                "and strPermitType <> '6' " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewPSDCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewPSDCount.LinkClicked
        Try

            Dim FirstDay As String = ""
            Dim LastDay As String = ""
            Dim EngineerLine As String = ""

            FirstDay = Format(DTPPermitCountStart.Value.AddDays(-1), "dd-MMM-yyyy")
            LastDay = Format(DTPPermitCountEnd.Value.AddDays(1), "dd-MMM-yyyy")

            If chbAllApps.Checked = False Then
                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtPSDCount.Text <> "0" And txtPSDCount.Text <> "") Then

                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else 'Linked - '|| strMasterApplication " &
                "end Link, " &
                "(datPermitIssued - datReceivedDate) as Diff, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes, " &
                "SSPPApplicationLinking, " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber (+) " &
                "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                "and SUBSTRING(strTrackedRules, 1, 1) = '1'  " &
                "and DatPermitIssued > '" & FirstDay & "' and datPermitissued < '" & LastDay & "'  " &
                "and strPermitType <> '9' " &
                "and strPermitType <> '10' " &
                "and strPermitType <> '11' " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 2
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 3
                dgvApplicationCount.Columns("Diff").HeaderText = "Days to Issue"
                dgvApplicationCount.Columns("Diff").DisplayIndex = 4
                dgvApplicationCount.Columns("Link").HeaderText = "Linking"
                dgvApplicationCount.Columns("Link").DisplayIndex = 5
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 6
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub btnRunOpenReport_Click(sender As Object, e As EventArgs) Handles btnRunOpenReport.Click
        Try


            RunOpenApplications()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewAllOpenCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewAllOpenCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If cboSSPPUnits2.Text = "SSPP Administrative" Then
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers2.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtAllOpenCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc,  " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus,     " &
                "(strLastname||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and datFinalizedDate is Null " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewToBCDOCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewToBCDOCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If cboSSPPUnits2.Text = "SSPP Administrative" Then
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers2.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtToDOCount.Text <> "" Or txtToBCCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc,  " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus,     " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and datFinalizedDate is Null " &
                "and ((datToBranchCheif is Not Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif)) " &
                "or (datToDirector is Not Null and (datDraftIssued is Null or datDraftIssued < datToDirector))) " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewOpen45DayCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewOpen45DayCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If cboSSPPUnits2.Text = "SSPP Administrative" Then
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers2.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtOpen45DayCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc,  " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus,    " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and datFinalizedDate is Null " &
                "and datEPAEnds is Not Null " &
                "and datDraftIssued is Not Null " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewPublicNoticeCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewPublicNoticeCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If cboSSPPUnits2.Text = "SSPP Administrative" Then
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers2.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtPublicNoticeCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc,  " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus,     " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and datFinalizedDate is Null " &
                "and datPNExpires is Not Null and datPNExpires < GETDATE() " &
                "and datEPAEnds is Null " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewDraftIssuedCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewDraftIssuedCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If cboSSPPUnits2.Text = "SSPP Administrative" Then
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers2.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtDraftIssuedCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc,  " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus,    " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and datFinalizedDate is Null " &
                "and ((datPNExpires is Not Null and datPNExpires >= GETDATE())  " &
                "or (datDraftIssued is not Null and datPNExpires is Null))  " &
                "and datToBranchCheif is Null  " &
                "and datToDirector is Null  " &
                "and datEPAEnds is Null  " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewToPMCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewToPMCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If cboSSPPUnits2.Text = "SSPP Administrative" Then
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers2.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtToPMCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc,  " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus,     " &
                "(strLastname||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and datFinalizedDate is Null " &
                "and datToBranchCheif is Null  " &
                "and datToDirector is Null  " &
                "and datEPAEnds is Null  " &
                "and datPNExpires is Null  " &
                "and datDraftIssued is Null " &
                "and datToPMII is Not Null " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewToUCCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewToUCCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If cboSSPPUnits2.Text = "SSPP Administrative" Then
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers2.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtToUCCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc,  " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus,   " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and datFinalizedDate is Null " &
                "and datToBranchCheif is Null  " &
                "and datToDirector is Null  " &
                "and datEPAEnds is Null  " &
                "and datPNExpires is Null  " &
                "and datDraftIssued is Null " &
                "and datToPMII is Null " &
                "and datToPMI is Not Null " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewWStaffCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewWStaffCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If cboSSPPUnits2.Text = "SSPP Administrative" Then
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers2.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers2.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers2.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtWStaffCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc,  " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus,   " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and datFinalizedDate is Null " &
                "and datToBranchCheif is Null  " &
                "and datToDirector is Null  " &
                "and datEPAEnds is Null  " &
                "and datPNExpires is Null  " &
                "and datDraftIssued is Null " &
                "and datToPMII is Null " &
                "and datToPMI is Null " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub btnRunTVAge_Click(sender As Object, e As EventArgs) Handles btnRunTVAge.Click
        Try


            RunTVAgeOfApplications()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewTVTotalOpenCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewTVTotalOpenCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTVTotalOpenCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and (strApplicationType = '14' or strApplicationType = '16') " &
                EngineerLine


                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewTVOneYearCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewTVOneYearCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTVOneYearCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and (strApplicationType = '14' or strApplicationType = '16') " &
                "and datReceivedDate > DATEADD(month, -12, GETDATE())  " &
                EngineerLine


                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewTVTwelveCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewTVTwelveCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTVTwelveCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and (strApplicationType = '14' or strApplicationType = '16') " &
                "and datReceivedDate >= DATEADD(month, -18, GETDATE()) " &
                "and datReceivedDate < DATEADD(month, -12, GETDATE()) " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llvViewTVGreaterCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llvViewTVGreaterCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTVGreaterCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and (strApplicationType = '14' or strApplicationType = '16') " &
                "and datReceivedDate < DATEADD(month, -18, GETDATE())" &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub btnRunAge_Click(sender As Object, e As EventArgs) Handles btnRunAge.Click
        Try


            RunNonTVAgeOfApplications()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewTotalOpenCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewTotalOpenCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTotalOpenCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewThreeMonthOpenCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewThreeMonthOpenCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtThreeMonthOpenCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate >= DATEADD(month, -3, GETDATE())  " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewSixMonthOpenCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewSixMonthOpenCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtSixMonthOpenCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate >= DATEADD(month, -6, GETDATE()) " &
                "and datReceivedDate < DATEADD(month, -3, GETDATE()) " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewNineMonthOpenCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewNineMonthOpenCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtNineMonthOpenCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate >= DATEADD(month, -9, GETDATE()) " &
                "and datReceivedDate < DATEADD(month, -6, GETDATE()) " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewTwelveMonthOpenCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewTwelveMonthOpenCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTwelveMonthOpenCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate >= DATEADD(month, -12, GETDATE()) " &
                "and datReceivedDate < DATEADD(month, -9, GETDATE()) " &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewGreaterThanOpenCount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewGreaterThanOpenCount.LinkClicked
        Try

            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If cboSSPPUnits3.Text = "SSPP Administrative" Then
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        For Each Engineer As String In clbEngineers3.Items
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                Else
                    If clbEngineers3.CheckedIndices.Contains(0) = True Then
                        EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                    Else
                        For Each Engineer As String In clbEngineers3.CheckedItems
                            If EngineerLine = "" Then
                                EngineerLine = "and ( "
                            End If
                            EngineerLine = EngineerLine & " (strLastName||', '||strFirstName) = '" & Engineer & "' or "
                        Next
                        If EngineerLine <> "" Then
                            EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                        End If
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtGreaterThanOpenCount.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "(strLastName||', '||strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "and datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate < DATEADD(month, -12, GETDATE())" &
                EngineerLine

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("UserName").HeaderText = "Staff Responsible"
                dgvApplicationCount.Columns("UserName").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub btnRunEPAReport_Click(sender As Object, e As EventArgs) Handles btnRunEPAReport.Click
        Try

            If cboEPAYear.Text <> "" And (rdbJanuaryReport.Checked = True Or rdbJulyReport.Checked = True) Then
                RunEPAReport()
            Else
                MsgBox("Either a year is not selected or the Reporting period is not selected.", MsgBoxStyle.Information, "Reports and Statistical Tools")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA2a_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA2a.LinkClicked
        Try


            If txtEPA2a.Text <> "" Then
                SQL = "Select " &
                "distinct(SUBSTRING(APBFacilityInformation.strAIRSNumber, 5,8)) as AIRSNumber,  " &
                "APBFacilityInformation.strFacilityName,  " &
                "case  " &
                "   when SUBSTRING(strAirprogramCodes, 13, 1) = '1' then 'Title V'  " &
                "Else 'Non Title V'  " &
                "End TVStatus,  " &
                "case  " &
                "   when strOperationalStatus = 'O' then 'O-Operating'  " &
                "Else 'Not Operating'  " &
                "End strOperationalStatus  " &
                "from APBFacilityInformation, APBHeaderData,  " &
                "(select APBHeaderData.strAIRSnumber as AIRSNumber1 " &
                "from APBHeaderData, APBSupplamentalData " &
                "where APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber  " &
                "AND SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
                "and (strEPATOPSExcluded is null or strEPATOPSExcluded = 'False')   " &
                "and strOperationalStatus = 'O') EPA1,  " &
                "(select APBHeaderData.strAIRSNumber as AIRSNumber2 " &
                "from APBHeaderData, APBSupplamentalData,   " &
                "SSPPApplicationMaster, SSPPApplicationTracking   " &
                "where APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber  " &
                "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber (+)   " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strAPplicationNumber   " &
                "AND SUBSTRING(strAirProgramCodes, 13, 1) <> '1'   " &
                "and datPermitIssued is null   " &
                "and strApplicationType = '14'   " &
                "and datFinalizeddate is null  " &
                "and (strEPATOPSExcluded is null or strEPATOPSExcluded = 'False')) EPA2 " &
                "where APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSnumber  " &
                "and (APBHeaderData.strAIRSnumber = EPA1.AIRSNumber1  " &
                "or APBHeaderData.strAIRSnumber = EPA2.AIRSNumber2) "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvApplicationCount.Columns("AIRSNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("TVStatus").HeaderText = "Title V Status"
                dgvApplicationCount.Columns("TVStatus").DisplayIndex = 2
                dgvApplicationCount.Columns("strOperationalStatus").HeaderText = "Operating Status"
                dgvApplicationCount.Columns("strOperationalStatus").DisplayIndex = 3
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA2d_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA2d.LinkClicked
        Try


            If txtEPA2d.Text <> "" Then
                SQL = "Select " &
                "distinct(SUBSTRING(APBFacilityInformation.strAIRSNumber, 5,8)) as AIRSNumber,   " &
                "APBFacilityInformation.strFacilityName,   " &
                "case      " &
                "   when SUBSTRING(strAirprogramCodes, 13, 1) = '1' then 'Title V'   " &
                "Else 'Non Title V'   " &
                "End TVStatus,   " &
                "case      " &
                "   when strOperationalStatus = 'O' then 'O-Operating'   " &
                "Else 'Not Operating'   " &
                "End strOperationalStatus,  " &
                "case " &
                "when strEPATOPSExcluded is NUll then ' ' " &
                "   when strEPATOPSExcluded = 'True' then 'X'  " &
                "   when strEPATOPSExcluded = 'False' then ' '  " &
                "else ' '  " &
                "End strEPATOPSExcluded    " &
                "from APBFacilityInformation, APBHeaderData,   " &
                "APBSupplamentalData,   " &
                "(select APBHeaderData.strAIRSnumber as AIRSNumber1  " &
                "from APBHeaderData, APBSupplamentalData  " &
                "where APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber   " &
                "AND SUBSTRING(strAirProgramCodes, 13, 1) = '1'   " &
                "and strOperationalStatus = 'O') EPA1,   " &
                "(select APBHeaderData.strAIRSNumber as AIRSNumber2  " &
                "from APBHeaderData, APBSupplamentalData,    " &
                "SSPPApplicationMaster, SSPPApplicationTracking    " &
                "where APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber   " &
                "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber (+)    " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strAPplicationNumber    " &
                "AND SUBSTRING(strAirProgramCodes, 13, 1) <> '1'    " &
                "and datPermitIssued is null    " &
                "and strApplicationType = '14'   " &
                "and datFinalizeddate is null) EPA2   " &
                "where APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSnumber   " &
                "and APBFacilityInformation.strAIRSNumber = APBSupplamentalData.strAIRSNumber  " &
                "and (APBHeaderData.strAIRSnumber = EPA1.AIRSNumber1   " &
                "or APBHeaderData.strAIRSnumber = EPA2.AIRSNumber2) "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvApplicationCount.Columns("AIRSNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("TVStatus").HeaderText = "Title V Status"
                dgvApplicationCount.Columns("TVStatus").DisplayIndex = 2
                dgvApplicationCount.Columns("strOperationalStatus").HeaderText = "Operating Status"
                dgvApplicationCount.Columns("strOperationalStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("strEPATOPSExcluded").HeaderText = "Exclusion Status"
                dgvApplicationCount.Columns("strEPATOPSExcluded").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA3a_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA3a.LinkClicked
        Try


            If txtEPA3a.Text <> "" Then
                SQL = "Select " &
                "SUBSTRING(APBFacilityInformation.strAIRSNumber, 5,8) as AIRSNumber,  " &
                "strFacilityName,  " &
                "case  " &
                " when SUBSTRING(strAirprogramCodes, 13, 1) = '1' then 'Title V'  " &
                "else 'Non Title V'  " &
                "end TVStatus,  " &
                "case  " &
                " when strOperationalStatus = 'O' then 'O-Operating' " &
                "else 'Not Operating'  " &
                "end strOperationalStatus  " &
                "from APBHeaderData, APBFacilityInformation  " &
                "where APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
                "and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
                "and strOPerationalStatus = 'O'  "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvApplicationCount.Columns("AIRSNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("TVStatus").HeaderText = "Title V Status"
                dgvApplicationCount.Columns("TVStatus").DisplayIndex = 2
                dgvApplicationCount.Columns("strOperationalStatus").HeaderText = "Operating Status"
                dgvApplicationCount.Columns("strOperationalStatus").DisplayIndex = 3
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA4a_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA4a.LinkClicked
        Try

            Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA4a.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "AND datPermitIssued IS NOT NULL " &
                "AND strApplicationType = '14'  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > '" & StartDate & "' " &
                "AND datPermitIssued < '" & EndDate & "' "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA4b_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA4b.LinkClicked
        Try

            Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA4b.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "AND datPermitIssued IS NOT NULL " &
                "AND strApplicationType = '14'  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > '" & StartDate & "' " &
                "AND datPermitIssued < '" & EndDate & "' " &
                "and datReceivedDate > DATEADD(month, -18, datPermitIssued) "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA5a_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA5a.LinkClicked
        Try
            ' Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    'StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    ' StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                'StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA5a.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "AND strApplicationType = '14' " &
                "and datPermitIssued is Null " &
                "and datReceivedDate < DATEADD(month, -18, '" & EndDate & "') "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA6a_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA6a.LinkClicked
        Try
            'Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    'StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    'StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                'StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA6a.Text <> "" Then
                SQL = "select " &
                "distinct(SUBSTRING(SSPPApplicationMaster.strAIRSnumber, 5,8)) as AIRSNumber,  " &
                "strFacilityName,  " &
                "to_char(MaxDate, 'RRRR-MM-dd') as MaxDate " &
                "from SSPPApplicationMaster,  " &
                "SSPPApplicationTracking, APBHeaderData,  " &
                "APBFacilityInformation,   " &
                "(select  " &
                "strAIRSNumber, " &
                "max(datEffective) as MaxDate  " &
                "from SSPPApplicationMaster, SSPPApplicationTracking  " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and datEffective is not null  " &
                "group by strAIRSnumber) Effect,  " &
                "(Select  " &
                "distinct(SSPPApplicationMaster.strAIRSnumber) as AIRSNumber " &
                "from SSPPApplicationMaster, SSPPApplicationTracking  " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and datReceiveddate < DATEADD(month, -6, '" & EndDate & "')  " &
                "and datReceivedDate > DATEADD(month, -54, '" & EndDate & "')  " &
                "and strApplicationType <> '16'  " &
                "and strApplicationType <> '12') PermitRequests   " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber " &
                "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber   " &
                "and APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
                "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
                "and MaxDate = SSPPApplicationTracking.datEffective " &
                "and maxDate < DATEADD(month, -54, '" & EndDate & "') " &
                "and strOperationalStatus = 'O'  " &
                "and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
                "and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvApplicationCount.Columns("AIRSNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("MaxDate").HeaderText = "Last Effective Date"
                dgvApplicationCount.Columns("MaxDate").DisplayIndex = 2
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA6b_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA6b.LinkClicked
        Try
            'Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    'StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    'StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                'StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA6b.Text <> "" Then
                SQL = "select " &
            "distinct(SUBSTRING(SSPPApplicationMaster.strAIRSnumber, 5,8) ) as AIRSNumber,  " &
            "strFacilityName, " &
            "to_char(MaxDate, 'RRRR-MM-dd') as MaxDate " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, APBHeaderData,  " &
            "APBFacilityInformation,   " &
            "(select  " &
         "strAIRSNumber,  " &
            "max(datEffective) as MaxDate  " &
            "from SSPPApplicationMaster, SSPPApplicationTracking  " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and datEffective is not null  " &
            "group by strAIRSnumber) Effect,  " &
            "(Select  " &
            "distinct(SSPPApplicationMaster.strAIRSnumber) as AIRSNumber " &
            "from SSPPApplicationMaster, SSPPApplicationTracking  " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and datReceiveddate < DATEADD(month, -6, '" & EndDate & "')  " &
            "and datReceivedDate > DATEADD(month, -54, '" & EndDate & "')  " &
            "and (strApplicationType = '16' or strApplicationType = '12')) PermitRequests   " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber " &
            "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber   " &
            "and APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
            "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
            "and MaxDate = SSPPApplicationTracking.datEffective " &
            "and maxDate < DATEADD(month, , '" & EndDate & "') " &
            "and strOperationalStatus = 'O'  " &
            "and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
            "and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber  " &
            "order by AIRSNumber "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvApplicationCount.Columns("AIRSNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("MaxDate").HeaderText = "Last Effective Date"
                dgvApplicationCount.Columns("MaxDate").DisplayIndex = 2
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub

    Private Sub llbViewEPA6c_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA6c.LinkClicked
        Try
            'Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    'StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    ' StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                'StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA6C.Text <> "" Then
                SQL = "select * " &
                "from (Select *  From  " &
                "(select  " &
                "distinct(SUBSTRING(SSPPApplicationMaster.strAIRSnumber, 5,8)) as AIRSNumber,   " &
                "strFacilityName, MaxDate  " &
                "from SSPPApplicationMaster,  SSPPApplicationTracking,  " &
                "APBHeaderData, APBFacilityInformation,  " &
                "(select  strAIRSNumber,  " &
                "max(datEffective) as MaxDate   " &
                "from SSPPApplicationMaster, SSPPApplicationTracking   " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber   " &
                "and datEffective is not null  GROUP BY strAIRSNumber) Effect,   " &
                "(Select  distinct(SSPPApplicationMaster.strAIRSnumber) as AIRSNumber  " &
                "from SSPPApplicationMaster, SSPPApplicationTracking   " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and datReceiveddate < DATEADD(month, -6, '" & EndDate & "')   " &
                "and datReceivedDate > DATEADD(month, -54, '" & EndDate & "')   " &
                "and strApplicationType <> '16'   " &
                "and strApplicationType <> '12') PermitRequests    " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber  " &
                "and APBHeaderData.strAIRSnumber = APBFacilityInformation.strAIRSNumber " &
                "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber    " &
                "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber   " &
                "and MaxDate = SSPPApplicationTracking.datEffective  " &
                "and maxDate < DATEADD(month, -54, '" & EndDate & "') " &
                "and strOperationalStatus = 'O'   " &
                "and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
                "and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber))  EPA6A " &
                "where not exists  " &
                "(select * from (Select *   " &
                "From (select distinct(SUBSTRING(SSPPApplicationMaster.strAIRSnumber, 5,8) ) as AIRSNumber,   " &
                "strFacilityName, MaxDate from SSPPApplicationMaster,  SSPPApplicationTracking, APBHeaderData,   " &
                "APBFacilityInformation,   (select  strAIRSNumber,  max(datEffective) as MaxDate  from SSPPApplicationMaster,  " &
                "SSPPApplicationTracking   " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber   " &
                "and datEffective is not null  group by strAIRSnumber) Effect,   " &
                "(Select  distinct(SSPPApplicationMaster.strAIRSnumber) as AIRSNumber from SSPPApplicationMaster,  " &
                "SSPPApplicationTracking   " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber   " &
                "and datReceiveddate < DATEADD(month, -6, '" & EndDate & "' )  and datReceivedDate > DATEADD(month, -54, '" & EndDate & "')   " &
                "and (strApplicationType = '16' or strApplicationType = '12')) PermitRequests    " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber  " &
                "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber    " &
                "and APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
                "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
                "and MaxDate = SSPPApplicationTracking.datEffective  " &
                "and maxDate < DATEADD(month, -54, '" & EndDate & "')  " &
                "and strOperationalStatus = 'O'  and SUBSTRING(strAirProgramCodes, 13, 1) = '1'   " &
                "and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber)  ) EPA6b where  EPA6A.airsnumber = EPA6b.airsNumber) "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvApplicationCount.Columns("AIRSNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("MaxDate").HeaderText = "Last Effective Date"
                dgvApplicationCount.Columns("MaxDate").DisplayIndex = 2
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub


    Private Sub llbViewEPA7a_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA7a.LinkClicked
        Try


            Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA7a.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "AND datPermitIssued IS NOT NULL " &
                "AND (strApplicationType = '22' or strApplicationType = '21')  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > '" & StartDate & "' " &
                "AND datPermitIssued < '" & EndDate & "' "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA7b_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA7b.LinkClicked
        Try

            Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA7b.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "AND datPermitIssued IS NOT NULL " &
                "AND (strApplicationType = '22' or strApplicationType = '21')  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > '" & StartDate & "' " &
                "AND datPermitIssued < '" & EndDate & "' " &
                "and datReceivedDate > DATEADD(month, -18, datPermitIssued) "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA7c_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA7c.LinkClicked
        Try

            Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA7c.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate, " &
                "to_char(datPermitIssued, 'RRRR-MM-dd') as datPermitIssued " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "AND datPermitIssued IS NOT NULL " &
                "AND (strApplicationType = '22' or strApplicationType = '21')  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > '" & StartDate & "' " &
                "AND datPermitIssued < '" & EndDate & "' " &
                "and datReceivedDate > DATEADD(month, -9, datPermitIssued) "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
                dgvApplicationCount.Columns("datPermitIssued").HeaderText = "Date Permit Issued"
                dgvApplicationCount.Columns("datPermitIssued").DisplayIndex = 5
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA8a_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA8a.LinkClicked
        Try
            'Dim StartDate As String
            Dim EndDate As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    'StartDate = "31-Dec-" & (CDate("31-Dec-" & cboEPAYear.Text).AddMonths(-12).Year).ToString
                    EndDate = "01-Jul-" & cboEPAYear.Text
                Else
                    'StartDate = "30-Jun-" & cboEPAYear.Text
                    EndDate = "01-Jan-" & (CDate("01-Jan-" & cboEPAYear.Text).AddMonths(12).Year).ToString
                End If
            Else
                'StartDate = "31-Dec-" & (Now.AddMonths(-12).Year).ToString
                EndDate = "01-Jul-" & Now.Year.ToString
            End If

            If txtEPA8a.Text <> "" Then
                SQL = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strApplicationTypeDesc, " &
                "case       " &
                "when datFinalizedDate is Not Null then '11 - Closed Out'        " &
                "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'       " &
                "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'       " &
                "when datEPAEnds is not Null then '08 - EPA 45-day Review'       " &
                "when datPNExpires is Not Null and datPNExpires < GETDATE() then '07 - Public Notice Expired'       " &
                "when datPNExpires is Not Null and datPNExpires >= GETDATE() then '06 - Public Notice'        " &
                "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'        " &
                "when dattoPMII is Not Null then '04 - AT PM'        " &
                "when dattoPMI is Not Null then '03 - At UC'        " &
                "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'       " &
                "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'         " &
                "else '01 - At Engineer'        " &
                "end as AppStatus, " &
                "to_char(datReceivedDate, 'RRRR-MM-dd') as datReceivedDate " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode (+) " &
                "AND (strApplicationType = '22' or strApplicationType = '21')  " &
                "and datPermitIssued is Null " &
                "and datReceivedDate < DATEADD(month, -18, '" & EndDate & "') "

                dsViewCount = New DataSet
                daViewCount = New SqlDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvApplicationCount.DataSource = dsViewCount
                dgvApplicationCount.DataMember = "ViewCount"

                dgvApplicationCount.RowHeadersVisible = False
                dgvApplicationCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvApplicationCount.AllowUserToResizeColumns = True
                dgvApplicationCount.AllowUserToAddRows = False
                dgvApplicationCount.AllowUserToDeleteRows = False
                dgvApplicationCount.AllowUserToOrderColumns = True
                dgvApplicationCount.AllowUserToResizeRows = True

                dgvApplicationCount.Columns("strApplicationNumber").HeaderText = "APL #"
                dgvApplicationCount.Columns("strApplicationNumber").DisplayIndex = 0
                dgvApplicationCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvApplicationCount.Columns("strFacilityName").DisplayIndex = 1
                dgvApplicationCount.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgvApplicationCount.Columns("strApplicationTypeDesc").DisplayIndex = 2
                dgvApplicationCount.Columns("AppStatus").HeaderText = "App Status"
                dgvApplicationCount.Columns("AppStatus").DisplayIndex = 3
                dgvApplicationCount.Columns("datReceivedDate").HeaderText = "Date Received"
                dgvApplicationCount.Columns("datReceivedDate").DisplayIndex = 4
            End If

            txtApplicationCount.Text = dgvApplicationCount.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub btnUpdateEPAReport_Click(sender As Object, e As EventArgs) Handles btnUpdateEPAReport.Click
        Try


            UpdateEPAReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub cboSSPPUnits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSSPPUnits.SelectedIndexChanged
        Try


            If cboSSPPUnits.Text <> "" And cboSSPPUnits.Text <> "System.Data.DataRowView" And tempLoad <> "Load" Then
                clbEngineers.Items.Clear()
                clbEngineers2.Items.Clear()
                clbEngineers3.Items.Clear()
                clbEngineers.Items.Add("All Engineers")
                clbEngineers2.Items.Add("All Engineers")
                clbEngineers3.Items.Add("All Engineers")

                SQL = "SELECT " &
                "(strLastName||', '||strFirstName) AS UserName,   " &
                "numUserID  " &
                "from EPDUserProfiles   " &
                "WHERE numUnit = '" & cboSSPPUnits.SelectedValue & "'   " &
                "Order by UserName  "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    clbEngineers.Items.Add(dr.Item("UserName"))
                    clbEngineers2.Items.Add(dr.Item("Username"))
                    clbEngineers3.Items.Add(dr.Item("UserName"))
                End While
                dr.Close()

                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    SQL = "select (strLastName||', '||strFirstName) as UserName,  " &
                    "numUSerID    " &
                    "from    " &
                    "EPDUserProfiles,    " &
                    "(select distinct(strStaffResponsible) As Users   " &
                    "from SSPPApplicationMaster   " &
                    "minus    " &
                    "select to_char(numUserID)     " &
                    "from EPDUserProfiles where numProgram = '5') AppUsers   " &
                    "where EPDUserProfiles.numUserID = AppUsers.Users    " &
                    "Order by Username  "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        clbEngineers.Items.Add(dr.Item("UserName"))
                        clbEngineers2.Items.Add(dr.Item("Username"))
                        clbEngineers3.Items.Add(dr.Item("UserName"))
                    End While
                    dr.Close()
                End If

                clbEngineers.SetItemCheckState(0, CheckState.Checked)
                clbEngineers2.SetItemCheckState(0, CheckState.Checked)
                clbEngineers3.SetItemCheckState(0, CheckState.Checked)

            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub clbEngineers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clbEngineers.SelectedIndexChanged
        Try


            For Index As Integer = clbEngineers.Items.Count - 1 To 0 Step -1
                clbEngineers2.SetItemCheckState(Index, clbEngineers.GetItemCheckState(Index))
                clbEngineers3.SetItemCheckState(Index, clbEngineers.GetItemCheckState(Index))
            Next Index



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub clbEngineers2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clbEngineers2.SelectedIndexChanged
        Try


            For Index As Integer = clbEngineers2.Items.Count - 1 To 0 Step -1
                clbEngineers.SetItemCheckState(Index, clbEngineers2.GetItemCheckState(Index))
                clbEngineers3.SetItemCheckState(Index, clbEngineers2.GetItemCheckState(Index))
            Next Index



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub clbEngineers3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clbEngineers3.SelectedIndexChanged
        Try


            For Index As Integer = clbEngineers3.Items.Count - 1 To 0 Step -1
                clbEngineers.SetItemCheckState(Index, clbEngineers3.GetItemCheckState(Index))
                clbEngineers2.SetItemCheckState(Index, clbEngineers3.GetItemCheckState(Index))
            Next Index



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub chbAllApps_CheckedChanged(sender As Object, e As EventArgs) Handles chbAllApps.CheckedChanged
        Try

            If chbAllApps.CheckState = CheckState.Checked Then
                chbAllApps2.Checked = True
                chbAllApps3.Checked = True
                cboSSPPUnits.Enabled = False
                cboSSPPUnits2.Enabled = False
                cboSSPPUnits3.Enabled = False
                clbEngineers.Enabled = False
                clbEngineers2.Enabled = False
                clbEngineers3.Enabled = False
            Else
                chbAllApps2.Checked = False
                chbAllApps3.Checked = False
                cboSSPPUnits.Enabled = True
                cboSSPPUnits2.Enabled = True
                cboSSPPUnits3.Enabled = True
                clbEngineers.Enabled = True
                clbEngineers2.Enabled = True
                clbEngineers3.Enabled = True
            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub chbAllApps2_CheckedChanged(sender As Object, e As EventArgs) Handles chbAllApps2.CheckedChanged
        Try

            If chbAllApps2.CheckState = CheckState.Checked Then
                chbAllApps.Checked = True
                chbAllApps3.Checked = True
                cboSSPPUnits.Enabled = False
                cboSSPPUnits2.Enabled = False
                cboSSPPUnits3.Enabled = False
                clbEngineers.Enabled = False
                clbEngineers2.Enabled = False
                clbEngineers3.Enabled = False
            Else
                chbAllApps.Checked = False
                chbAllApps3.Checked = False
                cboSSPPUnits.Enabled = True
                cboSSPPUnits2.Enabled = True
                cboSSPPUnits3.Enabled = True
                clbEngineers.Enabled = True
                clbEngineers2.Enabled = True
                clbEngineers3.Enabled = True
            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub chbAllApps3_CheckedChanged(sender As Object, e As EventArgs) Handles chbAllApps3.CheckedChanged
        Try

            If chbAllApps3.CheckState = CheckState.Checked Then
                chbAllApps.Checked = True
                chbAllApps2.Checked = True
                cboSSPPUnits.Enabled = False
                cboSSPPUnits2.Enabled = False
                cboSSPPUnits3.Enabled = False
                clbEngineers.Enabled = False
                clbEngineers2.Enabled = False
                clbEngineers3.Enabled = False
            Else
                chbAllApps.Checked = False
                chbAllApps2.Checked = False
                cboSSPPUnits.Enabled = True
                cboSSPPUnits2.Enabled = True
                cboSSPPUnits3.Enabled = True
                clbEngineers.Enabled = True
                clbEngineers2.Enabled = True
                clbEngineers3.Enabled = True
            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub

#End Region
    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvApplicationCount.ExportToExcel(Me)
    End Sub

#Region "Subpart Tool"
    Sub LoadSubPartData()
        Try
            Dim dtPart60 As New DataTable
            Dim dtPart61 As New DataTable
            Dim dtPart63 As New DataTable
            Dim dtSIP As New DataTable
            Dim drDSRow As DataRow
            Dim drDSRow2 As DataRow
            Dim drDSRow3 As DataRow
            Dim drDSRow4 As DataRow
            Dim drNewRow As DataRow

            SQL = "Select * from LookupSubPart60 order by strSubpart "
            SQL2 = "Select * from LookupSubPart61 order by strSubpart "
            SQL3 = "Select * from LookupSubPart63 order by strSubpart "
            SQL4 = "Select * from LookUpSubPartSIP order by strSubPart "

            dsPart60 = New DataSet
            dsPart61 = New DataSet
            dsPart63 = New DataSet
            dsSIP = New DataSet

            daPart60 = New SqlDataAdapter(SQL, CurrentConnection)
            daPart61 = New SqlDataAdapter(SQL2, CurrentConnection)
            daPart63 = New SqlDataAdapter(SQL3, CurrentConnection)
            daSIP = New SqlDataAdapter(SQL4, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daPart60.Fill(dsPart60, "Part60")
            daPart61.Fill(dsPart61, "Part61")
            daPart63.Fill(dsPart63, "Part63")
            daSIP.Fill(dsSIP, "SIP")

            dgvNSPS.DataSource = dsPart60
            dgvNSPS.DataMember = "Part60"
            dgvNESHAP.DataSource = dsPart61
            dgvNESHAP.DataMember = "Part61"
            dgvMACT.DataSource = dsPart63
            dgvMACT.DataMember = "Part63"
            dgvSIP.DataSource = dsSIP
            dgvSIP.DataMember = "SIP"

            dgvNSPS.RowHeadersVisible = False
            dgvNSPS.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPS.AllowUserToResizeColumns = True
            dgvNSPS.AllowUserToAddRows = False
            dgvNSPS.AllowUserToDeleteRows = False
            dgvNSPS.AllowUserToOrderColumns = True
            dgvNSPS.AllowUserToResizeRows = True
            dgvNSPS.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvNSPS.Columns("strSubPart").DisplayIndex = 0
            dgvNSPS.Columns("strDescription").HeaderText = "Description"
            dgvNSPS.Columns("strdescription").DisplayIndex = 1
            dgvNSPS.Columns("strdescription").Width = 500

            dgvNESHAP.RowHeadersVisible = False
            dgvNESHAP.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNESHAP.AllowUserToResizeColumns = True
            dgvNESHAP.AllowUserToAddRows = False
            dgvNESHAP.AllowUserToDeleteRows = False
            dgvNESHAP.AllowUserToOrderColumns = True
            dgvNESHAP.AllowUserToResizeRows = True
            dgvNESHAP.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvNESHAP.Columns("strSubPart").DisplayIndex = 0
            dgvNESHAP.Columns("strDescription").HeaderText = "Description"
            dgvNESHAP.Columns("strdescription").DisplayIndex = 1
            dgvNESHAP.Columns("strdescription").Width = 500

            dgvMACT.RowHeadersVisible = False
            dgvMACT.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvMACT.AllowUserToResizeColumns = True
            dgvMACT.AllowUserToAddRows = False
            dgvMACT.AllowUserToDeleteRows = False
            dgvMACT.AllowUserToOrderColumns = True
            dgvMACT.AllowUserToResizeRows = True
            dgvMACT.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvMACT.Columns("strSubPart").DisplayIndex = 0
            dgvMACT.Columns("strDescription").HeaderText = "Description"
            dgvMACT.Columns("strdescription").DisplayIndex = 1
            dgvMACT.Columns("strdescription").Width = 500

            dgvSIP.RowHeadersVisible = False
            dgvSIP.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSIP.AllowUserToResizeColumns = True
            dgvSIP.AllowUserToAddRows = False
            dgvSIP.AllowUserToDeleteRows = False
            dgvSIP.AllowUserToOrderColumns = True
            dgvSIP.AllowUserToResizeRows = True
            dgvSIP.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvSIP.Columns("strSubPart").DisplayIndex = 0
            dgvSIP.Columns("strDescription").HeaderText = "Description"
            dgvSIP.Columns("strdescription").DisplayIndex = 1
            dgvSIP.Columns("strdescription").Width = 500

            dtSIP.Columns.Add("strSubPart", GetType(System.String))
            dtSIP.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtSIP.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtSIP.Rows.Add(drNewRow)

            For Each drDSRow4 In dsSIP.Tables("SIP").Rows()
                drNewRow = dtSIP.NewRow()
                drNewRow("strSubPart") = drDSRow4("strSubPart")
                drNewRow("strDescription") = drDSRow4("strSubPart") & " - " & drDSRow4("strDescription")
                dtSIP.Rows.Add(drNewRow)
            Next

            dtPart60.Columns.Add("strSubPart", GetType(System.String))
            dtPart60.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtPart60.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtPart60.Rows.Add(drNewRow)

            For Each drDSRow In dsPart60.Tables("Part60").Rows()
                drNewRow = dtPart60.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("strDescription") = drDSRow("strSubPart") & " - " & drDSRow("strDescription")
                'drNewRow("strDescription") = drDSRow("strDescription")
                dtPart60.Rows.Add(drNewRow)
            Next

            dtPart61.Columns.Add("strSubPart", GetType(System.String))
            dtPart61.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtPart61.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtPart61.Rows.Add(drNewRow)

            For Each drDSRow2 In dsPart61.Tables("Part61").Rows()
                drNewRow = dtPart61.NewRow()
                drNewRow("strSubPart") = drDSRow2("strSubPart")
                drNewRow("strDescription") = drDSRow2("strSubPart") & " - " & drDSRow2("strDescription")
                dtPart61.Rows.Add(drNewRow)
            Next

            dtPart63.Columns.Add("strSubPart", GetType(System.String))
            dtPart63.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtPart63.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtPart63.Rows.Add(drNewRow)

            For Each drDSRow3 In dsPart63.Tables("Part63").Rows()
                drNewRow = dtPart63.NewRow()
                drNewRow("strSubPart") = drDSRow3("strSubPart")
                drNewRow("strDescription") = drDSRow3("strSubPart") & " - " & drDSRow3("strDescription")
                dtPart63.Rows.Add(drNewRow)
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#Region "Declarations"
    Private Sub btnEditSIP_Click(sender As Object, e As EventArgs) Handles btnEditSIP.Click
        Try
            If txtSIPCode.Text <> "" And txtSIPDescription.Text <> "" Then
                txtSIPCode.BackColor = Color.White
                txtSIPDescription.BackColor = Color.White

                SQL = "Select strSubPart " &
                "From LookUpSubpartSIP " &
                "where strSubPart = '" & txtSIPCode.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update LookUpSubpartSIP set " &
                    "strDescription = '" & Replace(txtSIPDescription.Text, "'", "''") & "' " &
                    "where strSubpart = '" & txtSIPCode.Text & "' "
                Else
                    SQL = "Insert into LookUpSubpartSIP " &
                    "values " &
                    "('" & Replace(txtSIPCode.Text, "'", "''") & "', " &
                    "'" & Replace(txtSIPDescription.Text, "'", "''") & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtSIPCode.Text = "" Then
                    txtSIPCode.BackColor = Color.Tomato
                Else
                    txtSIPCode.BackColor = Color.White
                End If
                If txtSIPDescription.Text = "" Then
                    txtSIPDescription.BackColor = Color.Tomato
                Else
                    txtSIPDescription.BackColor = Color.White
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditNSPS_Click(sender As Object, e As EventArgs) Handles btnEditNSPS.Click
        Try
            If txtNSPSCode.Text <> "" And txtNSPSDescription.Text <> "" Then
                txtNSPSCode.BackColor = Color.White
                txtNSPSDescription.BackColor = Color.White

                SQL = "Select strSubPart " &
                "From LookUpSubpart60 " &
                "where strSubPart = '" & txtNSPSCode.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update LookUpSubpart60 set " &
                    "strDescription = '" & Replace(txtNSPSDescription.Text, "'", "''") & "' " &
                    "where strSubpart = '" & txtNSPSCode.Text & "' "
                Else
                    SQL = "Insert into LookUpSubpart60 " &
                    "values " &
                    "('" & Replace(txtNSPSCode.Text, "'", "''") & "', " &
                    "'" & Replace(txtNSPSDescription.Text, "'", "''") & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtNSPSCode.Text = "" Then
                    txtNSPSCode.BackColor = Color.Tomato
                Else
                    txtNSPSCode.BackColor = Color.White
                End If
                If txtNSPSDescription.Text = "" Then
                    txtNSPSDescription.BackColor = Color.Tomato
                Else
                    txtNSPSDescription.BackColor = Color.White
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditNESHAP_Click(sender As Object, e As EventArgs) Handles btnEditNESHAP.Click
        Try
            If txtNESHAPCode.Text <> "" And txtNESHAPDescription.Text <> "" Then
                txtNESHAPCode.BackColor = Color.White
                txtNESHAPDescription.BackColor = Color.White

                SQL = "Select strSubPart " &
                "From LookUpSubpart61 " &
                "where strSubPart = '" & txtNESHAPCode.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update LookUpSubpart61 set " &
                    "strDescription = '" & Replace(txtNESHAPDescription.Text, "'", "''") & "' " &
                    "where strSubpart = '" & txtNESHAPCode.Text & "' "
                Else
                    SQL = "Insert into LookUpSubpart61 " &
                    "values " &
                    "('" & Replace(txtNESHAPCode.Text, "'", "''") & "', " &
                    "'" & Replace(txtNESHAPDescription.Text, "'", "''") & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtNESHAPCode.Text = "" Then
                    txtNESHAPCode.BackColor = Color.Tomato
                Else
                    txtNESHAPCode.BackColor = Color.White
                End If
                If txtNESHAPDescription.Text = "" Then
                    txtNESHAPDescription.BackColor = Color.Tomato
                Else
                    txtNESHAPDescription.BackColor = Color.White
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditMACT_Click(sender As Object, e As EventArgs) Handles btnEditMACT.Click
        Try
            If txtMACTCode.Text <> "" And txtMACTDescription.Text <> "" Then
                txtMACTCode.BackColor = Color.White
                txtMACTDescription.BackColor = Color.White

                SQL = "Select strSubPart " &
                "From LookUpSubpart63 " &
                "where strSubPart = '" & txtMACTCode.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update LookUpSubpart63 set " &
                    "strDescription = '" & Replace(txtMACTDescription.Text, "'", "''") & "' " &
                    "where strSubpart = '" & txtMACTCode.Text & "' "
                Else
                    SQL = "Insert into LookUpSubpart63 " &
                    "values " &
                    "('" & Replace(txtMACTCode.Text, "'", "''") & "', " &
                    "'" & Replace(txtMACTDescription.Text, "'", "''") & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtMACTCode.Text = "" Then
                    txtMACTCode.BackColor = Color.Tomato
                Else
                    txtMACTCode.BackColor = Color.White
                End If
                If txtMACTDescription.Text = "" Then
                    txtMACTDescription.BackColor = Color.Tomato
                Else
                    txtMACTDescription.BackColor = Color.White
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteSIPSubpart_Click_1(sender As Object, e As EventArgs) Handles btnDeleteSIPSubpart.Click
        Try
            SQL = "Delete LookUpSubpartSIP " &
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteNSPSSubpart_Click_1(sender As Object, e As EventArgs) Handles btnDeleteNSPSSubpart.Click
        Try
            SQL = "Delete LookUpSubpart60 " &
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteNESHAPSubpart_Click_1(sender As Object, e As EventArgs) Handles btnDeleteNESHAPSubpart.Click
        Try
            SQL = "Delete LookUpSubpart61 " &
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteMACTSubpart_Click_1(sender As Object, e As EventArgs) Handles btnDeleteMACTSubpart.Click
        Try
            SQL = "Delete LookUpSubpart63 " &
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearSIP_Click(sender As Object, e As EventArgs) Handles btnClearSIP.Click
        Try
            txtSIPCode.Clear()
            txtSIPCode.BackColor = Color.White
            txtSIPDescription.Clear()
            txtSIPDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearNSPS_Click(sender As Object, e As EventArgs) Handles btnClearNSPS.Click
        Try
            txtNSPSCode.Clear()
            txtNSPSCode.BackColor = Color.White
            txtNSPSDescription.Clear()
            txtNSPSDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearNESHAP_Click(sender As Object, e As EventArgs) Handles btnClearNESHAP.Click
        Try
            txtNESHAPCode.Clear()
            txtNESHAPCode.BackColor = Color.White
            txtNESHAPDescription.Clear()
            txtNESHAPDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearMACT_Click(sender As Object, e As EventArgs) Handles btnClearMACT.Click
        Try
            txtMACTCode.Clear()
            txtMACTCode.BackColor = Color.White
            txtMACTDescription.Clear()
            txtMACTDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region
    Private Sub dgvSIP_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvSIP.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvSIP.HitTest(e.X, e.Y)
            If dgvSIP.Columns(0).HeaderText = "Subpart Code" Then
                If dgvSIP.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtSIPCode.BackColor = Color.White
                    txtSIPDescription.BackColor = Color.White

                    txtSIPCode.Text = dgvSIP(0, hti.RowIndex).Value
                    txtSIPDescription.Text = dgvSIP(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvNSPS_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvNSPS.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvNSPS.HitTest(e.X, e.Y)
            If dgvNSPS.Columns(0).HeaderText = "Subpart Code" Then
                If dgvNSPS.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtNSPSCode.BackColor = Color.White
                    txtNSPSDescription.BackColor = Color.White

                    txtNSPSCode.Text = dgvNSPS(0, hti.RowIndex).Value
                    txtNSPSDescription.Text = dgvNSPS(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvNESHAP_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvNESHAP.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvNESHAP.HitTest(e.X, e.Y)
            If dgvNESHAP.Columns(0).HeaderText = "Subpart Code" Then
                If dgvNESHAP.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtNESHAPCode.BackColor = Color.White
                    txtNESHAPDescription.BackColor = Color.White

                    txtNESHAPCode.Text = dgvNESHAP(0, hti.RowIndex).Value
                    txtNESHAPDescription.Text = dgvNESHAP(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvMACT_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvMACT.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvMACT.HitTest(e.X, e.Y)
            If dgvMACT.Columns(0).HeaderText = "Subpart Code" Then
                If dgvMACT.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtMACTCode.BackColor = Color.White
                    txtMACTDescription.BackColor = Color.White

                    txtMACTCode.Text = dgvMACT(0, hti.RowIndex).Value
                    txtMACTDescription.Text = dgvMACT(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region



End Class