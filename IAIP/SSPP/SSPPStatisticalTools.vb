Imports System.Data.SqlClient

Public Class SSPPStatisticalTools
    Private Property tempLoad As String = ""

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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SetDateRange()
        DTPPermitCountStart.Value = New Date(Today.Year, Today.Month, 1)
        DTPPermitCountEnd.Value = DTPPermitCountStart.Value.AddMonths(1).AddDays(-1)
    End Sub
    Private Sub LoadEPAReportYear()
        Dim Year As Integer = Today.AddYears(1).Year

        Do Until Year = 2005
            cboEPAYear.Items.Add(Year)
            Year = Year - 1
        Loop

        cboEPAYear.Text = Today.Year.ToString
    End Sub
    Private Sub LoadComboBoxs()
        Try
            Dim query As String = "select " &
            "strUnitDesc, numUnitCode  " &
            "from LookUpEPDUnits  " &
            "where numProgramCode = '5'  " &
            "order by strUnitDesc "

            Dim dtPermittingUnits As DataTable = DB.GetDataTable(query)

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
    Private Sub RunPermitsIssued()
        Try

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim MedianTime As String = 0
            Dim n As Integer = 0
            Dim MedianArray(n) As Decimal
            Dim EngineerLine As String = ""
            Dim query As String

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName,', ',strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            query = "select count(*) " &
            "from SSPPApplicationMaster, SSPPApplicationTracking, " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '14' " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
            "or strPermitType = '13') " &
            EngineerLine

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            txtTitleVInitialCount.Text = DB.GetInteger(query, p)

            If txtTitleVInitialCount.Text <> "0" Then
                query = "select datediff(day,datReceivedDate,datPermitIssued) as Diff " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and strApplicationType = '14'  " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
                "or strPermitType = '13') " &
                EngineerLine &
                "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*)  " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,   " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '16' " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
            "or strPermitType = '13') " &
            EngineerLine

            txtTitleVRenewalCount.Text = DB.GetInteger(query, p)

            If txtTitleVRenewalCount.Text <> "0" Then
                query = "select datediff(day, datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and strApplicationType = '16'  " &
                    "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
                    "or strPermitType = '13') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            If txtTitleVInitialCount.Text <> "0" AndAlso txtTitleVRenewalCount.Text <> "0" Then
                query = "select datediff(day, datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and (strApplicationType = '16' or strApplicationType = '14') " &
                    "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
                    "or strPermitType = '13') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*)  " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '22' or strApplicationType = '21') " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and (strPermitType = '4' or strPermitType = '7') " &
            EngineerLine

            txtSigModCount.Text = DB.GetInteger(query, p)

            If txtSigModCount.Text <> "0" Then
                query = "select datediff(day, datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and (strApplicationType = '22' or strApplicationType = '21') " &
                    "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and (strPermitType = '4' or strPermitType = '7') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*) as MinorMod " &
            "from SSPPApplicationMaster, SSPPApplicationTracking, " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '19' or strApplicationType = '20') " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and (strPermitType = '4' or strPermitType = '7') " &
            EngineerLine

            txtMinorModCount.Text = DB.GetInteger(query, p)

            If txtMinorModCount.Text <> "0" Then
                query = "select datediff(day, datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and (strApplicationType = '19' or strApplicationType = '20') " &
                    "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and (strPermitType = '4' or strPermitType = '7') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*) as Mod502 " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '15' " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and (strPermitType = '4' or strPermitType = '7') " &
            EngineerLine

            txt502Count.Text = DB.GetInteger(query, p)

            If txt502Count.Text <> "0" Then
                query = "select datediff(day,datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and strApplicationType = '15' " &
                    "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and (strPermitType = '4' or strPermitType = '7') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*) as AA " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '26' " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '1') " &
            EngineerLine

            txtAACount.Text = DB.GetInteger(query, p)

            If txtAACount.Text <> "0" Then
                query = "select datediff(day,datReceivedDate, datPermitIssued ) as Diff " &
                        "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                        "EPDUserProfiles " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                        "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                        "and strApplicationType = '26' " &
                        "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                        "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '1') " &
                        EngineerLine &
                        "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*) as SM " &
            "from SSPPApplicationMaster, SSPPApplicationTracking, " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '12' " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and (strPermitType = '4' or strPermitType = '7') " &
            EngineerLine

            txtSMCount.Text = DB.GetInteger(query, p)

            If txtSMCount.Text <> "0" Then
                query = "select datediff(day,datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and strApplicationType = '12' " &
                    "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and (strPermitType = '4' or strPermitType = '7') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*) as PBR " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType = '9' " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and strPermitType = '6' " &
            EngineerLine

            txtPBRCount.Text = DB.GetInteger(query, p)

            If txtPBRCount.Text <> "0" Then
                query = "select datediff(day,datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and strApplicationType = '9' " &
                    "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and strPermitType = '6' " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*) as Other " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '11' OR strApplicationType = '8' " &
            "OR strApplicationType = '4' OR strapplicationType = '3' " &
            "OR strApplicationType = '25' OR strApplicationType = '2') " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and (strPermitType = '7' or strPermitType = '4') " &
            EngineerLine

            txtOtherCount.Text = DB.GetInteger(query, p)

            If txtOtherCount.Text <> "0" Then
                query = "select datediff(day,datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and (strApplicationType = '11' OR strApplicationType = '8' " &
                    "OR strApplicationType = '4' OR strapplicationType = '3' " &
                    "OR strApplicationType = '25' OR strApplicationType = '2') " &
                    "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and (strPermitType = '7' or strPermitType = '4') " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*) as Closed " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datPermitIssued IS not Null  " &
            "and datPermitIssued > @FirstDay and datPermitissued < @LastDay " &
            "and strPermitType <> '4' " &
            "and strPermitType <> '7' " &
            "and strPermitType <> '12' " &
            "and strPermitType <> '13' " &
            "and strPermitType <> '6' " &
            EngineerLine

            txtNonPermitCount.Text = DB.GetInteger(query, p)

            If txtNonPermitCount.Text <> "0" Then
                query = "select datediff(day,datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking, " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and datPermitIssued IS not Null  " &
                    "and datPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and strPermitType <> '4' " &
                    "and strPermitType <> '7' " &
                    "and strPermitType <> '12' " &
                    "and strPermitType <> '13' " &
                    "and strPermitType <> '6' " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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

            query = "select count(*) as PSD " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "SSPPApplicationData,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
            "and SUBSTRING(strTrackedRules, 1, 1) = '1'  " &
            "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
            "and strPermitType <> '9' " &
            "and strPermitType <> '10' " &
            "and strPermitType <> '11' " &
            EngineerLine

            txtPSDCount.Text = DB.GetInteger(query, p)

            If txtPSDCount.Text <> "0" Then
                query = "select datediff(day,datReceivedDate, datPermitIssued ) as Diff " &
                    "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                    "SSPPApplicationData,  " &
                    "EPDUserProfiles " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SUBSTRING(strTrackedRules, 1, 1) = '1'  " &
                    "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                    "and strPermitType <> '9' " &
                    "and strPermitType <> '10' " &
                    "and strPermitType <> '11' " &
                    EngineerLine &
                    "Order by Diff desc "

                n = 0
                ReDim MedianArray(n)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    ReDim Preserve MedianArray(n)
                    MedianArray(n) = dr.Item("Diff")
                    n = n + 1
                Next

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
    Private Sub RunOpenApplications()
        Try

            Dim EngineerLine As String = ""
            Dim query As String

            If chbAllApps2.Checked = False Then
                If clbEngineers2.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers2.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName,', ',strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            query = "select count(*) as OpenCount " &
            "from SSPPApplicationMaster,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    EngineerLine

            txtAllOpenCount.Text = DB.GetInteger(query)

            query = "select count(*) as OpenDOCount " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datToDirector is Not Null  " &
            "and (datDraftIssued is Null or datDraftIssued < datToDirector) " &
                    EngineerLine

            txtToDOCount.Text = DB.GetInteger(query)

            query = "select count(*) as OpenBCCount " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datToBranchCheif is Not Null  " &
            "and datToDirector is Null  " &
            "and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) " &
                    EngineerLine

            txtToBCCount.Text = DB.GetInteger(query)

            query = "select count(*) as Open45Days " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datEPAEnds is Not Null  " &
            "and datDraftIssued is Not Null " &
                    EngineerLine

            txtOpen45DayCount.Text = DB.GetInteger(query)

            query = "select count(*) as OpenPublicNotice " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null  " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and datPNExpires is Not Null and datPNExpires < GETDATE() " &
            "and datEPAEnds is Null  " &
                    EngineerLine

            txtPublicNoticeCount.Text = DB.GetInteger(query)

            query = "select count(*) as OpenDraftIssued " &
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

            txtDraftIssuedCount.Text = DB.GetInteger(query)

            query = "select count(*) as OpenPMIICount " &
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

            txtToPMCount.Text = DB.GetInteger(query)

            query = "select count(*) as OpenPMICount " &
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

            txtToUCCount.Text = DB.GetInteger(query)

            query = "select count(*) as OpenStaffCount " &
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

            txtWStaffCount.Text = DB.GetInteger(query)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub RunTVAgeOfApplications()
        Try

            Dim EngineerLine As String = ""
            Dim query As String

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName,', ',strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            query = "Select count(*) as TVTotalOpen " &
            "from SSPPApplicationMaster,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '14' or strApplicationType = '16' " &
            "or strApplicationType = '27' or strApplicationType = '17') " &
                EngineerLine

            txtTVTotalOpenCount.Text = DB.GetInteger(query)

            query = "Select count(*) as TVYearOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '14' or strApplicationType = '16'   " &
            "or strApplicationType = '27' or strApplicationType = '17') " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate > DATEADD(month, -12, GETDATE())  " &
                EngineerLine

            txtTVOneYearCount.Text = DB.GetInteger(query)

            query = "Select count(*) as TV12MonthsOpen " &
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

            txtTVTwelveCount.Text = DB.GetInteger(query)

            query = "Select count(*) as TV18MonthsOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and (strApplicationType = '14' or strApplicationType = '16'   " &
            "or strApplicationType = '27' or strApplicationType = '17') " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate < DATEADD(month, -18, GETDATE())" &
                EngineerLine


            txtTVGreaterCount.Text = DB.GetInteger(query)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub RunNonTVAgeOfApplications()
        Try
            Dim EngineerLine As String = ""
            Dim query As String

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName,', ',strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            query = "Select count(*) as NonTVTotalOpen " &
            "from SSPPApplicationMaster,  " &
            "EPDUserProfiles " &
            "where datFinalizedDate is Null " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType <> '16' and strApplicationType <> '14' " &
            "and strApplicationType <> '17' and strApplicationType <> '27' " &
                EngineerLine

            txtTotalOpenCount.Text = DB.GetInteger(query)

            query = "Select count(*) as NonTVThreeMonthOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType <> '16' and strApplicationType <> '14' " &
            "and strApplicationType <> '17' and strApplicationType <> '27' " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate >= DATEADD(month, -3, GETDATE())  " &
                EngineerLine

            txtThreeMonthOpenCount.Text = DB.GetInteger(query)

            query = "Select count(*) as NonTVSixMonthsOpen " &
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

            txtSixMonthOpenCount.Text = DB.GetInteger(query)

            query = "Select count(*) as NonTVNineMonthsOpen " &
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

            txtNineMonthOpenCount.Text = DB.GetInteger(query)

            query = "Select count(*) as NonTVTwelveMonthsOpen " &
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

            txtTwelveMonthOpenCount.Text = DB.GetInteger(query)

            query = "Select count(*) as NonTVGreaterThanOpen " &
            "from SSPPApplicationMaster, SSPPApplicationTracking, " &
            "EPDUserProfiles " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and strApplicationType <> '16' and strApplicationType <> '14' " &
            "and strApplicationType <> '17' and strApplicationType <> '27' " &
            "and datFinalizedDate is NUll  " &
            "and datReceivedDate < DATEADD(month, -12, GETDATE())" &
                EngineerLine

            txtGreaterThanOpenCount.Text = DB.GetInteger(query)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub RunEPAReport()
        Try

            Dim StartDate As Date
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = New Date(CInt(cboEPAYear.Text) - 1, 12, 31)
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    StartDate = New Date(CInt(cboEPAYear.Text), 6, 30)
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                StartDate = New Date((Now.AddMonths(-12).Year), 12, 31)
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@StartDate", StartDate),
                New SqlParameter("@EndDate", EndDate)
            }

            txtEPA1a.Text = "N/A"
            txtEPA1b.Text = "N/A"
            txtEPA1c.Text = "N/A"

            query = "select (EPA2ab + EPA2aa) as EPA2a " &
            "from " &
            "(select count(*) as EPA2aa " &
            "from APBHeaderData, APBSupplamentalData  " &
            "where APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
            "AND (SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
            "and (strEPATOPSExcluded is null or strEPATOPSExcluded = 'False')  " &
            "and strOperationalStatus = 'O')) EPA2a1,  " &
            "(select count(*) as EPA2ab " &
            "FROM APBHeaderData " &
            " INNER JOIN APBSupplamentalData  " &
            " ON APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
            " LEFT JOIN SSPPApplicationMaster " &
            " ON APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber " &
            " INNER JOIN SSPPApplicationTracking  " &
            " ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strAPplicationNumber  " &
            "where (SUBSTRING(strAirProgramCodes, 13, 1) <> '1'  " &
            "and datPermitIssued is null  " &
            "and strApplicationType = '14'  " &
            "and datFinalizeddate is null " &
            "and (strEPATOPSExcluded is null or strEPATOPSExcluded = 'False'))) EPA2a2 "

            txtEPA2a.Text = DB.GetInteger(query)

            txtEPA2b.Text = "0"
            txtEPA2c.Text = (CInt(txtEPA2a.Text) + CInt(txtEPA2b.Text)).ToString

            query = "select (EPA2db + EPA2da) as EPA2d " &
            "from " &
            "(select count(*) as EPA2da " &
            "from APBHeaderData  " &
            "where (SUBSTRING(strAirProgramCodes, 13, 1) = '1' " &
            "and strOperationalStatus = 'O')) EPA2d1,  " &
            "(select count(*) as EPA2db " &
            "FROM APBHeaderData " &
            " LEFT JOIN SSPPApplicationMaster " &
            " ON APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber " &
            " INNER JOIN SSPPApplicationTracking  " &
            " ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strAPplicationNumber  " &
            "where (SUBSTRING(strAirProgramCodes, 13, 1) <> '1'  " &
            "and datPermitIssued is null  " &
            "and strApplicationType = '14'  " &
            "and datFinalizeddate is null )) EPA2d2 "

            txtEPA2d.Text = DB.GetInteger(query)

            query = "select count(*) as EPA3a " &
            "from APBHeaderData " &
            "where SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
            "and strOperationalStatus = 'O' "

            txtEPA3a.Text = DB.GetInteger(query)

            query = "SELECT COUNT(*) AS EPA4a " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND strApplicationType = '14'  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > @StartDate " &
            "AND datPermitIssued < @EndDate "

            txtEPA4a.Text = DB.GetInteger(query, p)

            query = "SELECT COUNT(*) AS EPA4b " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND strApplicationType = '14'  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > @StartDate " &
            "AND datPermitIssued < @EndDate " &
            "and datReceivedDate > DATEADD(month, -18, datPermitIssued) "

            txtEPA4b.Text = DB.GetInteger(query, p)

            If txtEPA4a.Text = "0" Then
                txtEPA4a.Text = "N/A"
                txtEPA4b.Text = "0"
            End If

            query = "SELECT COUNT(*) AS EPA5a " &
            "from SSPPApplicationMaster, " &
            "SSPPApplicationTracking, SSPPApplicationData " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "AND strApplicationType = '14' " &
            "and datPermitIssued is Null " &
            "and datReceivedDate < DATEADD(month, -18, @EndDate) "

            txtEPA5a.Text = DB.GetInteger(query, p)

            query = "Select Count(*) as EPA6a " &
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
            "and datReceiveddate < DATEADD(month, -6, @EndDate)  " &
            "and datReceivedDate > DATEADD(month, -54, @EndDate)  " &
            "and strApplicationType <> '16'  " &
            "and strApplicationType <> '12') PermitRequests   " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber " &
            "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber   " &
            "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
            "and MaxDate = SSPPApplicationTracking.datEffective " &
            "and maxDate < DATEADD(month, -54, @EndDate) " &
            "and strOperationalStatus = 'O'  " &
            "and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
            "and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber) t"

            txtEPA6a.Text = DB.GetInteger(query, p)

            query = "Select Count(*) as EPA6b " &
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
            "and datReceiveddate < DATEADD(month, -6, @EndDate)  " &
            "and datReceivedDate > DATEADD(month, -54, @EndDate)  " &
            "and (strApplicationType = '16' or strApplicationType = '12')) PermitRequests   " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber " &
            "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber   " &
            "and APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
            "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
            "and MaxDate = SSPPApplicationTracking.datEffective " &
            "and maxDate < DATEADD(month, -54, @EndDate) " &
            "and strOperationalStatus = 'O'  " &
            "and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
            "and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber)  t"

            txtEPA6b.Text = DB.GetInteger(query, p)

            query = "select count(*) as EPA6C " &
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
"and datReceiveddate < DATEADD(month, -6, @EndDate)   " &
"and datReceivedDate > DATEADD(month, -54, @EndDate)   " &
"and strApplicationType <> '16'   " &
"and strApplicationType <> '12') PermitRequests    " &
"where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber  " &
"and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber    " &
"and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber   " &
"and MaxDate = SSPPApplicationTracking.datEffective  " &
"and maxDate < DATEADD(month, -54, @EndDate) " &
"and strOperationalStatus = 'O'   " &
"and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
"and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber) t1 )  EPA6A " &
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
"and datReceiveddate < DATEADD(month, -6, @EndDate)  and datReceivedDate > DATEADD(month, -54, @EndDate)   " &
"and (strApplicationType = '16' or strApplicationType = '12')) PermitRequests    " &
"where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber  " &
"and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber    " &
"and APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
 "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
 "and MaxDate = SSPPApplicationTracking.datEffective  " &
"and maxDate < DATEADD(month, -54, @EndDate)  " &
"and strOperationalStatus = 'O'  and SUBSTRING(strAirProgramCodes, 13, 1) = '1'   " &
"and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber) t2 ) EPA6b where  EPA6A.airsnumber = EPA6b.airsNumber) "

            txtEPA6C.Text = DB.GetInteger(query, p)

            query = "SELECT COUNT(*) AS EPA7a " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND (strApplicationType = '22' or strApplicationType = '21')  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > @StartDate " &
            "AND datPermitIssued < @EndDate "

            txtEPA7a.Text = DB.GetInteger(query, p)

            query = "SELECT COUNT(*) AS EPA7b " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND (strApplicationType = '22' or strApplicationType = '21')  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > @StartDate " &
            "AND datPermitIssued < @EndDate " &
            "and datReceivedDate > DATEADD(month, -18, datPermitIssued) "

            txtEPA7b.Text = DB.GetInteger(query, p)

            query = "SELECT COUNT(*) AS EPA7c " &
            "from SSPPApplicationMaster,  " &
            "SSPPApplicationTracking, SSPPApplicationData  " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber  " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND datPermitIssued IS NOT NULL " &
            "AND (strApplicationType = '22' or strApplicationType = '21')  " &
            "AND strPermitType = '7'  " &
            "AND datPermitIssued > @StartDate " &
            "AND datPermitIssued < @EndDate " &
            "and datReceivedDate > DATEADD(month, -9, datPermitIssued) "

            txtEPA7c.Text = DB.GetInteger(query, p)

            If txtEPA7a.Text = "0" Then
                txtEPA7a.Text = "0"
                txtEPA7b.Text = "N/A"
                txtEPA7c.Text = "N/A"
            End If

            query = "SELECT COUNT(*) AS EPA8a " &
            "from SSPPApplicationMaster, " &
            "SSPPApplicationTracking, SSPPApplicationData " &
            "WHERE SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationTracking.strApplicationNumber " &
            "AND SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "AND (strApplicationType = '22' or strApplicationType = '21')  " &
            "and datPermitIssued is Null " &
            "and datReceivedDate < DATEADD(month, -18, @EndDate)"

            txtEPA8a.Text = DB.GetInteger(query, p)

            txtEPA9a.Text = "No Comments"

            UpdateEPAReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub
    Private Sub UpdateEPAReport()
        Try
            txtEPAReportText.Clear()
            txtEPAReportText.Text = "1. Outstanding Permit Issuance " & vbCrLf &
            vbTab & "a) Number of final Actions: " & txtEPA1a.Text & vbCrLf &
            vbTab & "b) Total commitment universe: " & txtEPA1b.Text & vbCrLf &
            vbTab & "c) Date commitment completed (if applicable): " & txtEPA1c.Text & vbCrLf & vbCrLf &
            "2. Total Current Part 70 Source Universe and Permit Universe " & vbCrLf &
            vbTab & "a) Number of active part 70 sources that have obtained part 70 permits, plus the number of active part 70 sources that have not yet obtained part 70 permits: " & txtEPA2a.Text & vbCrLf &
            vbTab & "b) Number of part 70 sources that have applied to obtain a synthetic minor restriction in lieu of a part 70 permit, and the part 70 program's permit application due dates for those sources have passed: " & txtEPA2b.Text & vbCrLf &
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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtTitleVInitialCount.Text <> "0" AndAlso txtTitleVInitialCount.Text <> "") OrElse
                 (txtTitleVRenewalCount.Text <> "0" AndAlso txtTitleVRenewalCount.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc, " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN LookUpApplicationTypes " &
                "ON LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where (strApplicationType = '14' or strApplicationType = '16')  " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
                "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '12' " &
                "or strPermitType = '13') " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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


            If dgvApplicationCount.RowCount > 0 AndAlso hti.RowIndex <> -1 Then
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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtSigModCount.Text <> "0" AndAlso txtSigModCount.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN LookUpApplicationTypes " &
                "ON LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where (strApplicationType = '22' or strApplicationType = '21')  " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
                "and (strPermitType = '4' or strPermitType = '7') " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtMinorModCount.Text <> "0" AndAlso txtMinorModCount.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN LookUpApplicationTypes " &
                "ON LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where (strApplicationType = '20' or strApplicationType = '19')  " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
                "and (strPermitType = '4' or strPermitType = '7') " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txt502Count.Text <> "0" AndAlso txt502Count.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN LookUpApplicationTypes " &
                "ON LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where strApplicationType = '15' " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
                "and (strPermitType = '4' or strPermitType = '7') " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtAACount.Text <> "0" AndAlso txtAACount.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN LookUpApplicationTypes " &
                "ON LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where strApplicationType = '26' " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
                "and (strPermitType = '4' or strPermitType = '7' or strPermitType = '1') " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtSMCount.Text <> "0" AndAlso txtSMCount.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN LookUpApplicationTypes " &
                "ON LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where strApplicationType = '12' " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
                 "and (strPermitType = '4' or strPermitType = '7') " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtPBRCount.Text <> "0" AndAlso txtPBRCount.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN LookUpApplicationTypes " &
                "ON LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where strApplicationType = '9' " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay " &
                "and strPermitType = '6' " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtOtherCount.Text <> "0" AndAlso txtOtherCount.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN LookUpApplicationTypes " &
                "ON LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where (strApplicationType = '11' OR strApplicationType = '8' " &
                "OR strApplicationType = '4' OR strapplicationType = '3' " &
                "OR strApplicationType = '25' OR strApplicationType = '2') " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
                "and (strPermitType = '7' or strPermitType = '4') " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtNonPermitCount.Text <> "0" AndAlso txtNonPermitCount.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "case " &
                "when strApplicationTypeDesc is Null  then '' " &
                "else strApplicationTypeDesc " &
                "End strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datPermitIssued IS not Null  " &
                "and datPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
                "and strPermitType <> '4' " &
                "and strPermitType <> '7' " &
                "and strPermitType <> '12' " &
                "and strPermitType <> '13' " &
                "and strPermitType <> '6' " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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

            Dim FirstDay As Date
            Dim LastDay As Date
            Dim query As String
            Dim EngineerLine As String = ""

            FirstDay = DTPPermitCountStart.Value.AddDays(-1)
            LastDay = DTPPermitCountEnd.Value.AddDays(1)

            Dim p As SqlParameter() = {
                New SqlParameter("@FirstDay", FirstDay),
                New SqlParameter("@LastDay", LastDay)
            }

            If chbAllApps.Checked = False Then
                If clbEngineers.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If (txtPSDCount.Text <> "0" AndAlso txtPSDCount.Text <> "") Then

                query = "select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName,  " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued,  " &
                "strApplicationTypeDesc,  " &
                "case " &
                "when strMasterApplication is Null then '' " &
                "else concat('Linked - ', strMasterApplication) " &
                "end Link, " &
                "datediff(day, datReceivedDate, datPermitIssued ) as Diff, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN LookUpApplicationTypes " &
                "ON LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where SUBSTRING(strTrackedRules, 1, 1) = '1'  " &
                "and DatPermitIssued > @FirstDay and datPermitissued < @LastDay  " &
                "and strPermitType <> '9' " &
                "and strPermitType <> '10' " &
                "and strPermitType <> '11' " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If clbEngineers2.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers2.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtAllOpenCount.Text <> "" Then
                query = "select " &
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
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "from SSPPApplicationMaster, SSPPApplicationTracking,  " &
                "SSPPApplicationData, LookUpApplicationTypes,  " &
                "EPDUserProfiles " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and datFinalizedDate is Null " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If clbEngineers2.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers2.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtToDOCount.Text <> "" OrElse txtToBCCount.Text <> "" Then
                query = "select " &
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
                " concat(strLastName, ', ', strFirstName) as UserName " &
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

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If clbEngineers2.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers2.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtOpen45DayCount.Text <> "" Then
                query = "select " &
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
                " concat(strLastName, ', ', strFirstName) as UserName " &
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

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If clbEngineers2.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers2.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtPublicNoticeCount.Text <> "" Then
                query = "select " &
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
                " concat(strLastName, ', ', strFirstName) as UserName " &
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

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If clbEngineers2.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers2.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtDraftIssuedCount.Text <> "" Then
                query = "select " &
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
                " concat(strLastName, ', ', strFirstName) as UserName " &
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

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If clbEngineers2.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers2.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtToPMCount.Text <> "" Then
                query = "select " &
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
                " concat(strLastName, ', ', strFirstName) as UserName " &
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

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If clbEngineers2.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers2.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtToUCCount.Text <> "" Then
                query = "select " &
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
                " concat(strLastName, ', ', strFirstName) as UserName " &
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

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps2.Checked = False Then
                If clbEngineers2.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits2.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers2.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtWStaffCount.Text <> "" Then
                query = "select " &
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
                " concat(strLastName, ', ', strFirstName) as UserName " &
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

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTVTotalOpenCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " LEFT JOIN SSPPApplicationLinking " &
                "ON SSPPApplicationMaster.strApplicatioNnumber = SSPPApplicationLinking.strApplicationNumber " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datFinalizedDate is Null " &
                "and (strApplicationType = '14' or strApplicationType = '16') " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTVOneYearCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datFinalizedDate is Null " &
                "and (strApplicationType = '14' or strApplicationType = '16') " &
                "and datReceivedDate > DATEADD(month, -12, GETDATE())  " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTVTwelveCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datFinalizedDate is Null " &
                "and (strApplicationType = '14' or strApplicationType = '16') " &
                "and datReceivedDate >= DATEADD(month, -18, GETDATE()) " &
                "and datReceivedDate < DATEADD(month, -12, GETDATE()) " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTVGreaterCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicatioNNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datFinalizedDate is Null " &
                "and (strApplicationType = '14' or strApplicationType = '16') " &
                "and datReceivedDate < DATEADD(month, -18, GETDATE())" &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTotalOpenCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN  SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes  " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtThreeMonthOpenCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN  SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes  " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "Where datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate >= DATEADD(month, -3, GETDATE())  " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtSixMonthOpenCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN  SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes  " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate >= DATEADD(month, -6, GETDATE()) " &
                "and datReceivedDate < DATEADD(month, -3, GETDATE()) " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtNineMonthOpenCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN  SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes  " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate >= DATEADD(month, -9, GETDATE()) " &
                "and datReceivedDate < DATEADD(month, -6, GETDATE()) " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtTwelveMonthOpenCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN  SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes  " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate >= DATEADD(month, -12, GETDATE()) " &
                "and datReceivedDate < DATEADD(month, -9, GETDATE()) " &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String
            Dim EngineerLine As String = ""

            If chbAllApps3.Checked = False Then
                If clbEngineers3.CheckedIndices.Contains(0) = True Then
                    EngineerLine = " and numUnit = '" & cboSSPPUnits3.SelectedValue & "' "
                Else
                    For Each Engineer As String In clbEngineers3.CheckedItems
                        If EngineerLine = "" Then
                            EngineerLine = "and ( "
                        End If
                        EngineerLine = EngineerLine & "  concat(strLastName, ', ', strFirstName) = '" & Engineer & "' or "
                    Next
                    If EngineerLine <> "" Then
                        EngineerLine = Mid(EngineerLine, 1, (EngineerLine.Length - 3)) & " ) "
                    End If
                End If
            Else
                EngineerLine = ""
            End If

            If txtGreaterThanOpenCount.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                " concat(strLastName, ', ', strFirstName) as UserName " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN  SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes  " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN EPDUserProfiles " &
                "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where datFinalizedDate is Null " &
                "and strApplicationType <> '16' and strApplicationType <> '14' " &
                "and strApplicationType <> '17' and strApplicationType <> '27' " &
                "and datReceivedDate < DATEADD(month, -12, GETDATE())" &
                EngineerLine

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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

            If cboEPAYear.Text <> "" AndAlso (rdbJanuaryReport.Checked = True OrElse rdbJulyReport.Checked = True) Then
                RunEPAReport()
            Else
                MsgBox("Either a year is not selected or the reporting period is not selected.", MsgBoxStyle.Information, "Reports and Statistical Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub
    Private Sub llbViewEPA2a_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewEPA2a.LinkClicked
        Try
            Dim query As String

            If txtEPA2a.Text <> "" Then
                query = "Select " &
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
                "FROM APBHeaderData " &
                " INNER JOIN APBSupplamentalData   " &
                "ON APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber  " &
                " LEFT JOIN SSPPApplicationMaster " &
                "ON APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber " &
                " INNER JOIN SSPPApplicationTracking   " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strAPplicationNumber   " &
                "where SUBSTRING(strAirProgramCodes, 13, 1) <> '1'   " &
                "and datPermitIssued is null   " &
                "and strApplicationType = '14'   " &
                "and datFinalizeddate is null  " &
                "and (strEPATOPSExcluded is null or strEPATOPSExcluded = 'False')) EPA2 " &
                "where APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSnumber  " &
                "and (APBHeaderData.strAIRSnumber = EPA1.AIRSNumber1  " &
                "or APBHeaderData.strAIRSnumber = EPA2.AIRSNumber2) "

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String

            If txtEPA2d.Text <> "" Then
                query = "Select " &
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
                "FROM APBHeaderData " &
                " INNER JOIN APBSupplamentalData   " &
                "ON APBHeaderData.strAIRSNumber = APBSupplamentalData.strAIRSNumber  " &
                " LEFT JOIN SSPPApplicationMaster " &
                "ON APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber " &
                " INNER JOIN SSPPApplicationTracking   " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strAPplicationNumber   " &
                "where SUBSTRING(strAirProgramCodes, 13, 1) <> '1'    " &
                "and datPermitIssued is null    " &
                "and strApplicationType = '14'   " &
                "and datFinalizeddate is null) EPA2   " &
                "where APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSnumber   " &
                "and APBFacilityInformation.strAIRSNumber = APBSupplamentalData.strAIRSNumber  " &
                "and (APBHeaderData.strAIRSnumber = EPA1.AIRSNumber1   " &
                "or APBHeaderData.strAIRSnumber = EPA2.AIRSNumber2) "

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim query As String

            If txtEPA3a.Text <> "" Then
                query = "Select " &
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

                dgvApplicationCount.DataSource = DB.GetDataTable(query)

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
            Dim StartDate As Date
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = New Date(CInt(cboEPAYear.Text) - 1, 12, 31)
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    StartDate = New Date(CInt(cboEPAYear.Text), 6, 30)
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                StartDate = New Date((Now.AddMonths(-12).Year), 12, 31)
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@StartDate", StartDate),
                New SqlParameter("@EndDate", EndDate)
            }

            If txtEPA4a.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                "where datPermitIssued IS NOT NULL " &
                "AND strApplicationType = '14'  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > @StartDate " &
                "AND datPermitIssued < @EndDate "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim StartDate As Date
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = New Date(CInt(cboEPAYear.Text) - 1, 12, 31)
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    StartDate = New Date(CInt(cboEPAYear.Text), 6, 30)
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                StartDate = New Date((Now.AddMonths(-12).Year), 12, 31)
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@StartDate", StartDate),
                New SqlParameter("@EndDate", EndDate)
            }

            If txtEPA4b.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                "where datPermitIssued IS NOT NULL " &
                "AND strApplicationType = '14'  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > @StartDate " &
                "AND datPermitIssued < @EndDate " &
                "and datReceivedDate > DATEADD(month, -18, datPermitIssued) "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As New SqlParameter("@EndDate", EndDate)

            If txtEPA5a.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                "where strApplicationType = '14' " &
                "and datPermitIssued is Null " &
                "and datReceivedDate < DATEADD(month, -18, @EndDate) "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As New SqlParameter("@EndDate", EndDate)

            If txtEPA6a.Text <> "" Then
                query = "select " &
                "distinct(SUBSTRING(SSPPApplicationMaster.strAIRSnumber, 5,8)) as AIRSNumber,  " &
                "strFacilityName,  " &
                "format(MaxDate, 'yyyy-MM-dd') as MaxDate " &
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
                "and datReceiveddate < DATEADD(month, -6, @EndDate)  " &
                "and datReceivedDate > DATEADD(month, -54, @EndDate)  " &
                "and strApplicationType <> '16'  " &
                "and strApplicationType <> '12') PermitRequests   " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationnumber " &
                "and APBHeaderData.strAIRSNumber = SSPPApplicationMaster.strAIRSNumber   " &
                "and APBHeaderData.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
                "and SSPPApplicationMaster.strAIRSNumber = Effect.strAIRSnumber  " &
                "and MaxDate = SSPPApplicationTracking.datEffective " &
                "and maxDate < DATEADD(month, -54, @EndDate) " &
                "and strOperationalStatus = 'O'  " &
                "and SUBSTRING(strAirProgramCodes, 13, 1) = '1'  " &
                "and SSPPApplicationMaster.strAIRSNumber = PermitRequests.AIRSNumber "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As New SqlParameter("@EndDate", EndDate)

            If txtEPA6b.Text <> "" Then
                query = "SELECT DISTINCT
                        SUBSTRING(m.strAIRSnumber, 5, 8) AS AIRSNumber,
                        strFacilityName,
                        format(MaxDate, 'yyyy-MM-dd')    AS MaxDate
                    FROM SSPPApplicationMaster m
                        INNER JOIN SSPPApplicationTracking t
                            ON m.strApplicationNumber = t.strApplicationnumber
                        INNER JOIN APBHeaderData h
                            ON h.strAIRSNumber = m.strAIRSNumber
                        INNER JOIN APBFacilityInformation i
                            ON h.strAIRSNumber = i.strAIRSNumber
                        INNER JOIN
                        (
                            SELECT
                                strAIRSNumber,
                                max(datEffective) AS MaxDate
                            FROM SSPPApplicationMaster m
                                INNER JOIN SSPPApplicationTracking t
                                    ON m.strApplicationNumber = t.strApplicationNumber
                            WHERE datEffective IS NOT NULL
                            GROUP BY strAIRSnumber) Effect
                            ON m.strAIRSNumber = Effect.strAIRSnumber
                        INNER JOIN
                        (
                            SELECT DISTINCT (m.strAIRSnumber) AS AIRSNumber
                            FROM SSPPApplicationMaster m
                                INNER JOIN SSPPApplicationTracking t
                                    ON m.strApplicationNumber = t.strApplicationNumber
                            WHERE datReceiveddate < DATEADD(MONTH, -6, @EndDate)
                                  AND datReceivedDate > DATEADD(MONTH, -54, @EndDate)
                                  AND (strApplicationType = '16' OR strApplicationType = '12')) PermitRequests
                            ON m.strAIRSNumber = PermitRequests.AIRSNumber
                    WHERE MaxDate = t.datEffective
                          AND maxDate < DATEADD(MONTH, -54, @EndDate)
                          AND strOperationalStatus = 'O'
                          AND SUBSTRING(strAirProgramCodes, 13, 1) = '1'
                    ORDER BY AIRSNumber "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As New SqlParameter("@EndDate", EndDate)

            If txtEPA6C.Text <> "" Then
                query = "SELECT *
                    FROM (
                             SELECT DISTINCT
                                 SUBSTRING(m.strAIRSnumber, 5, 8) AS AIRSNumber,
                                 strFacilityName,
                                 MaxDate
                             FROM SSPPApplicationMaster m
                                 INNER JOIN SSPPApplicationTracking t
                                     ON m.strApplicationNumber = t.strApplicationnumber
                                 INNER JOIN APBHeaderData h
                                     ON h.strAIRSNumber = m.strAIRSNumber
                                 INNER JOIN APBFacilityInformation i
                                     ON h.strAIRSnumber = i.strAIRSNumber
                                 INNER JOIN
                                 (
                                     SELECT
                                         strAIRSNumber,
                                         max(datEffective) AS MaxDate
                                     FROM
                                         SSPPApplicationMaster m
                                         INNER JOIN SSPPApplicationTracking t
                                             ON m.strApplicationNumber = t.strApplicationNumber
                                     WHERE datEffective IS NOT NULL
                                     GROUP BY strAIRSNumber
                                 ) Effect
                                     ON m.strAIRSNumber = Effect.strAIRSnumber
                                 INNER JOIN
                                 (
                                     SELECT DISTINCT (strAIRSnumber) AS AIRSNumber
                                     FROM SSPPApplicationMaster m
                                         INNER JOIN SSPPApplicationTracking t
                                             ON m.strApplicationNumber = t.strApplicationNumber
                                     WHERE datReceiveddate < DATEADD(MONTH, -6, @EndDate)
                                           AND datReceivedDate > DATEADD(MONTH, -54, @EndDate)
                                           AND strApplicationType <> '16'
                                           AND strApplicationType <> '12'
                                 ) PermitRequests
                                     ON m.strAIRSNumber = PermitRequests.AIRSNumber
                             WHERE MaxDate = t.datEffective
                                   AND maxDate < DATEADD(MONTH, -54, @EndDate)
                                   AND strOperationalStatus = 'O'
                                   AND SUBSTRING(strAirProgramCodes, 13, 1) = '1'
                         ) EPA6A
                    WHERE NOT exists(
                        SELECT *
                        FROM (
                                 SELECT DISTINCT
                                     SUBSTRING(m.strAIRSnumber, 5, 8) AS AIRSNumber,
                                     strFacilityName,
                                     MaxDate
                                 FROM SSPPApplicationMaster m
                                     INNER JOIN SSPPApplicationTracking t
                                         ON m.strApplicationNumber = t.strApplicationnumber
                                     INNER JOIN APBHeaderData h
                                         ON h.strAIRSNumber = m.strAIRSNumber
                                     INNER JOIN APBFacilityInformation i
                                         ON h.strAIRSNumber = i.strAIRSNumber
                                     INNER JOIN
                                     (
                                         SELECT
                                             strAIRSNumber,
                                             max(datEffective) AS MaxDate
                                         FROM SSPPApplicationMaster,
                                             SSPPApplicationTracking
                                         WHERE SSPPApplicationMaster.strApplicationNumber =
                                               SSPPApplicationTracking.strApplicationNumber
                                               AND datEffective IS NOT NULL
                                         GROUP BY strAIRSnumber
                                     ) Effect
                                         ON m.strAIRSNumber = Effect.strAIRSnumber
                                     INNER JOIN
                                     (
                                         SELECT DISTINCT (m.strAIRSnumber) AS AIRSNumber
                                         FROM SSPPApplicationMaster m
                                             INNER JOIN SSPPApplicationTracking t
                                                 ON m.strApplicationNumber = t.strApplicationNumber
                                         WHERE datReceiveddate < DATEADD(MONTH, -6, @EndDate)
                                               AND datReceivedDate > DATEADD(MONTH, -54, @EndDate)
                                               AND (strApplicationType = '16' OR strApplicationType = '12')
                                     ) PermitRequests
                                         ON m.strAIRSNumber = PermitRequests.AIRSNumber
                                 WHERE MaxDate = t.datEffective
                                       AND maxDate < DATEADD(MONTH, -54, @EndDate)
                                       AND strOperationalStatus = 'O'
                                       AND SUBSTRING(strAirProgramCodes, 13, 1) = '1'
                             ) EPA6b
                        WHERE EPA6A.airsnumber = EPA6b.airsNumber) "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim StartDate As Date
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = New Date(CInt(cboEPAYear.Text) - 1, 12, 31)
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    StartDate = New Date(CInt(cboEPAYear.Text), 6, 30)
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                StartDate = New Date((Now.AddMonths(-12).Year), 12, 31)
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@StartDate", StartDate),
                New SqlParameter("@EndDate", EndDate)
            }

            If txtEPA7a.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                "where datPermitIssued IS NOT NULL " &
                "AND (strApplicationType = '22' or strApplicationType = '21')  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > @StartDate " &
                "AND datPermitIssued < @EndDate "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim StartDate As Date
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = New Date(CInt(cboEPAYear.Text) - 1, 12, 31)
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    StartDate = New Date(CInt(cboEPAYear.Text), 6, 30)
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                StartDate = New Date((Now.AddMonths(-12).Year), 12, 31)
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@StartDate", StartDate),
                New SqlParameter("@EndDate", EndDate)
            }

            If txtEPA7b.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                "where datPermitIssued IS NOT NULL " &
                "AND (strApplicationType = '22' or strApplicationType = '21')  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > @StartDate " &
                "AND datPermitIssued < @EndDate " &
                "and datReceivedDate > DATEADD(month, -18, datPermitIssued) "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim StartDate As Date
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    StartDate = New Date(CInt(cboEPAYear.Text) - 1, 12, 31)
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    StartDate = New Date(CInt(cboEPAYear.Text), 6, 30)
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                StartDate = New Date((Now.AddMonths(-12).Year), 12, 31)
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@StartDate", StartDate),
                New SqlParameter("@EndDate", EndDate)
            }

            If txtEPA7c.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate, " &
                "format(datPermitIssued, 'yyyy-MM-dd') as datPermitIssued " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                "where datPermitIssued IS NOT NULL " &
                "AND (strApplicationType = '22' or strApplicationType = '21')  " &
                "AND strPermitType = '7'  " &
                "AND datPermitIssued > @StartDate " &
                "AND datPermitIssued < @EndDate " &
                "and datReceivedDate > DATEADD(month, -9, datPermitIssued) "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim EndDate As Date
            Dim query As String

            If cboEPAYear.Text <> "" Then
                If rdbJanuaryReport.Checked = True Then
                    EndDate = New Date(CInt(cboEPAYear.Text), 7, 1)
                Else
                    EndDate = New Date(CInt(cboEPAYear.Text) + 1, 1, 1)
                End If
            Else
                EndDate = New Date(Now.Year, 7, 1)
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@EndDate", EndDate)
            }

            If txtEPA8a.Text <> "" Then
                query = "select " &
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
                "format(datReceivedDate, 'yyyy-MM-dd') as datReceivedDate " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationTracking  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationData " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                "where (strApplicationType = '22' or strApplicationType = '21')  " &
                "and datPermitIssued is Null " &
                "and datReceivedDate < DATEADD(month, -18, @EndDate) "

                dgvApplicationCount.DataSource = DB.GetDataTable(query, p)

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
            Dim query As String

            If cboSSPPUnits.Text <> "" AndAlso cboSSPPUnits.Text <> "System.Data.DataRowView" AndAlso tempLoad <> "Load" Then
                clbEngineers.Items.Clear()
                clbEngineers2.Items.Clear()
                clbEngineers3.Items.Clear()
                clbEngineers.Items.Add("All Engineers")
                clbEngineers2.Items.Add("All Engineers")
                clbEngineers3.Items.Add("All Engineers")

                query = "SELECT " &
                " concat(strLastName, ', ', strFirstName) AS UserName,   " &
                "numUserID  " &
                "from EPDUserProfiles   " &
                "WHERE numUnit = @unit " &
                "Order by UserName  "

                Dim p As New SqlParameter("@unit", cboSSPPUnits.SelectedValue)

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
                    clbEngineers.Items.Add(dr.Item("UserName"))
                    clbEngineers2.Items.Add(dr.Item("Username"))
                    clbEngineers3.Items.Add(dr.Item("UserName"))
                Next

                If cboSSPPUnits.Text = "SSPP Administrative" Then
                    query = "select  concat(strLastName, ', ', strFirstName) as UserName,  " &
                    "numUSerID    " &
                    "from    " &
                    "EPDUserProfiles,    " &
                    "(select distinct(strStaffResponsible) As Users   " &
                    "from SSPPApplicationMaster   " &
                    "minus    " &
                    "select convert(varchar(3),numUserID)     " &
                    "from EPDUserProfiles where numProgram = '5') AppUsers   " &
                    "where EPDUserProfiles.numUserID = AppUsers.Users    " &
                    "Order by Username  "

                    Dim dt2 As DataTable = DB.GetDataTable(query, p)

                    For Each dr As DataRow In dt2.Rows
                        clbEngineers.Items.Add(dr.Item("UserName"))
                        clbEngineers2.Items.Add(dr.Item("Username"))
                        clbEngineers3.Items.Add(dr.Item("UserName"))
                    Next
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

#Region " index changed and checkbox changed "

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

End Class