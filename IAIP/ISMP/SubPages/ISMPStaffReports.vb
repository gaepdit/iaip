Imports System.Data.SqlClient

Public Class ISMPStaffReports

    Private Sub ISMPStaffReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DTPUnitStart.Value = Today.AddMonths(-6)
        DTPUnitEnd.Value = Today
    End Sub

    Private Sub btnRunReport_Click(sender As Object, e As EventArgs) Handles btnRunReport.Click
        txtEngineerStatistics.Clear()
        RunUnitEngineerStatistics(332)
    End Sub

    Private Sub RunUnitEngineerStatistics(EngineerGCode As String)
        Dim query As String

        Dim DateBias As String = ""

        Dim Staff As String = ""
        Dim DateStatement As String = ""
        Dim ReceivedByDate As String = "X"
        Dim OpenByDate As String = "X"
        Dim ClosedByDate As String = "X"
        Dim WitnessedByDate As String = "X"
        Dim OpenWitnessedByDate As String = "X"
        Dim CloseWitnessedByDate As String = "X"
        Dim GreaterByDate As String = "X"
        Dim OpenGreaterByDate As String = "X"
        Dim CloseGreaterByDate As String = "X"
        Dim ComplianceByDate As String = "X"
        Dim OpenComplianceByDate As String = "X"
        Dim CloseComplianceByDate As String = "X"
        Dim OpenMedianByDate As String = "X"
        Dim CloseMedianByDate As String = "X"
        Dim OpenPercentileByDate As String = "X"
        Dim ClosePercentileByDate As String = "X"

        Dim ReceivedTotal As String = "X"
        Dim OpenTotal As String = "X"
        Dim OpenWitnessedTotal As String = "X"
        Dim OpenComplianceTotal As String = "X"
        Dim OpenGreaterTotal As String = "X"
        Dim OpenMedianTotal As String = "X"
        Dim PercentileOpenTotalDay As String = "X"
        Dim ClosedTotal As String = "X"
        Dim ClosedWitnessedTotal As String = "X"
        Dim ClosedComplianceTotal As String = "X"
        Dim ClosedGreaterTotal As String = "X"
        Dim ClosedMedianTotal As String = "X"
        Dim PercentileClosedTotalDay As String = "X"
        Dim Statement As String = ""

        Dim i As Integer = 0
        Dim MedianArrayByDateOpen(i) As Decimal
        Dim j As Integer = 0
        Dim MedianArrayByDateClose(j) As Decimal
        Dim n As Integer = 0
        Dim MedianArrayOpen(n) As Decimal
        Dim o As Integer = 0
        Dim MedianArrayClosed(o) As Decimal

        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@startdate", DTPUnitStart.Value),
                New SqlParameter("@enddate", DTPUnitEnd.Value),
                New SqlParameter("@userid", EngineerGCode)
            }

            If rdbUnitDateTestStarted.Checked = True Then
                DateBias = "datTestDateStart between @startdate " &
                "and @enddate"
                DateStatement = "For all Tests Conducted between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateReceived.Checked = True Then
                DateBias = "datReceivedDate between @startdate " &
                "and @enddate"
                DateStatement = "For all Test Reports Received between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between @startdate " &
                "and @enddate"
                DateStatement = "For all Test Reports Completed between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitStatsAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were: "
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " &
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were:"
            End If

            query = "select " &
            "distinct concat(strLastName, ', ' ,strFirstName) as Staff,  " &
            "case " &
            "	when ReceivedByDate is NULL then 0  " &
            "	Else ReceivedByDate " &
            "End as ReceivedByDate,  " &
            "Case  " &
            "	when OpenByDate is Null then 0  " &
            "	Else OpenByDate  " &
            "End as OpenByDate,  " &
            "Case  " &
            "	WHEN CloseByDate is Null then 0  " &
            "	Else CloseByDate " &
            "End as CloseByDate,  " &
            "Case  " &
            "	when WitnessedByDate is Null then 0  " &
            "	Else WitnessedByDate  " &
            "End as WitnessedByDate, " &
            "case  " &
            "	when OpenWitnessedByDate is NULL then 0  " &
            "	Else OpenWitnessedByDate  " &
            "End as OpenWitnessedByDate,  " &
            "case  " &
            "	when CloseWitnessedByDate is NULL then 0  " &
            "	Else CloseWitnessedByDate  " &
            "End as CloseWitnessedByDate,  " &
            "Case " &
            "   when GreaterByDate is NUll then 0 " &
            "   Else GreaterByDate " &
            "End as GreaterByDate, " &
            "case  " &
            "	when OpenGreaterByDate is NULL then 0  " &
            "	Else OpenGreaterByDate " &
            "end as OpenGreaterByDate,    " &
            "case  " &
            "	When CloseGreaterByDate is NULL then 0  " &
            "	Else CloseGreaterByDate  " &
            "End as CloseGreaterByDate,  " &
            "Case " &
            "   when ComplianceByDate is NULL then 0 " &
            "   Else ComplianceByDate " &
            "End as ComplianceByDate, " &
            "Case  " &
            "	when OpenComplianceByDate is NULL then 0  " &
            "	Else OpenComplianceByDate " &
            "End as OpenComplianceByDate,  " &
            "Case  " &
            "	When CloseComplianceByDate is NULL then 0  " &
            "	Else CloseComplianceByDate " &
            "End as CloseComplianceByDate  " &
            "FROM ISMPReportInformation  " &
            " INNER JOIN EPDUserProfiles " &
            "ON EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
            " LEFT JOIN (Select strReviewingEngineer,  count(*) as ReceivedByDate   " &
            "from ISMPReportInformation   " &
            "where strDelete is NULL " &
            "and " & DateBias & " " &
            "Group by strReviewingEngineer) ReceivedByDates " &
            "ON ISMPReportInformation.strReviewingEngineer = ReceivedByDates.strReviewingEngineer " &
            " LEFT JOIN (Select strReviewingEngineer,  " &
            "count(*) as OpenByDate  " &
            "from ISMPReportInformation  " &
            "where strClosed = 'False'  " &
            "and strDelete is NULL  " &
            "and " & DateBias & " " &
            "Group by strReviewingEngineer) OpenByDates " &
            "ON ISMPReportInformation.strReviewingEngineer = OpenBYDates.strReviewingEngineer " &
            " LEFT JOIN (Select strReviewingEngineer,  " &
            "count(*) as CloseByDate  " &
            "from ISMPReportInformation  " &
            "where strClosed = 'True'  " &
            "and StrDelete is NULL  " &
            "and " & DateBias & " " &
            "Group by strReviewingEngineer) CloseByDates " &
            "ON ISMPReportInformation.strReviewingEngineer = CloseByDates.strReviewingEngineer " &
            " LEFT JOIN (Select strWitnessingEngineer,  " &
            "count(*) as WitnessedByDate  " &
            "from ISMPReportInformation  " &
            "where strDelete is NULL  " &
            "and " & DateBias & " " &
            "group by strWitnessingEngineer) WitnessedByDates  " &
            "ON ISMPReportInformation.strReviewingEngineer = WitnessedByDates.strWitnessingEngineer " &
            " LEFT JOIN (Select strWitnessingEngineer,  " &
            "count(*) as OpenWitnessedByDate   " &
            "from ISMPReportInformation  " &
            "where strDelete is NULL  " &
            "and strClosed = 'False'  " &
             "and " & DateBias & " " &
            "group by strWitnessingEngineer) OpenWitnessedByDates  " &
            "ON ISMPReportInformation.strReviewingEngineer = OpenwitnessedByDates.strWitnessingEngineer " &
            " LEFT JOIN (select strWitnessingEngineer,  " &
            "count(*) as CloseWitnessedByDate   " &
            "from ISMPReportInformation  " &
            "where strDelete is NULL  " &
            "and strClosed = 'True' " &
            "and " & DateBias & " " &
            "group by strwitnessingEngineer) CloseWitnessedByDates  " &
            "ON ISMPReportInformation.strReviewingEngineer = CloseWitnessedByDates.strWitnessingEngineer " &
            " LEFT JOIN (select strReviewingEngineer,  " &
            "count(*) as GreaterByDate " &
            "from ISMPReportInformation  " &
            "where strDelete is NULL  " &
            "and datReceivedDate < ( case when strClosed = 'False' then DATEADD(day, -50, GETDATE()) " &
            "else DATEADD(day, -50, datCompleteDate) end ) " &
            "and " & DateBias & " " &
            "Group by strReviewingEngineer) GreaterByDates  " &
            "ON ISMPReportInformation.strReviewingEngineer = GreaterByDates.strReviewingEngineer " &
            " LEFT JOIN (select strReviewingEngineer,  " &
            "count(*) as OpenGreaterByDate " &
            "from ISMPReportInformation  " &
            "where strDelete is NULL  " &
            "and strClosed = 'False'  " &
            "and datReceivedDate < DATEADD(day, -50, GETDATE() ) " &
            "and " & DateBias & " " &
            "Group by strReviewingEngineer) OpenGreaterByDates  " &
            "ON ISMPReportInformation.strReviewingEngineer = OpenGreaterByDates.strReviewingEngineer " &
            " LEFT JOIN (select strReviewingEngineer,  " &
            "count(*) as CloseGreaterByDate " &
            "from ISMPReportInformation  " &
            "where strDelete is NULL  " &
            "and strClosed = 'True'  " &
            "and datReceivedDate < dateadd(day,-50, datCompleteDate) " &
            "and " & DateBias & " " &
            "Group by strReviewingEngineer) CloseGreaterByDates  " &
            "ON ISMPReportInformation.strReviewingEngineer = CloseGreaterByDates.strReviewingEngineer " &
            " LEFT JOIN (select strReviewingEngineer, " &
            "count(*) as ComplianceByDate " &
            "from ISMPReportInformation " &
            "where strComplianceStatus = '05' " &
            "and strDelete is NULL " &
            "and " & DateBias & " " &
            "group by strReviewingEngineer) ComplianceByDates " &
            "ON ISMPReportInformation.strReviewingEngineer = ComplianceByDates.strReviewingEngineer " &
            " LEFT JOIN (select strReviewingEngineer,   " &
            "count(*) as OpenComplianceByDate  " &
            "from ISMPReportInformation   " &
            "where strComplianceStatus = '05'  " &
            "and strClosed = 'False'  " &
            "and strDelete is NULL  " &
            "and " & DateBias & " " &
            "group by strReviewingEngineer) OpenComplianceByDates   " &
            "ON ISMPReportInformation.strReviewingEngineer = OpenComplianceByDates.strReviewingEngineer   " &
            " LEFT JOIN (Select strReviewingEngineer,  " &
            "count(*) as CloseComplianceByDate  " &
            "from ISMPReportInformation   " &
            "where strComplianceStatus = '05'  " &
            "and strClosed = 'True'  " &
            "and strDelete is NULL  " &
            "and " & DateBias & " " &
            "group by strReviewingEngineer) CloseComplianceByDates   " &
            "ON ISMPReportInformation.strREviewingEngineer = CloseComplianceByDates.strReviewingEngineer   " &
            "where ISMPReportInformation.strReviewingEngineer = @userid "

            Dim dr As DataRow = DB.GetDataRow(query, p)

            If dr IsNot Nothing Then
                Staff = dr.Item("Staff")
                ReceivedByDate = dr.Item("ReceivedByDate")
                OpenByDate = dr.Item("OpenbyDate")
                ClosedByDate = dr.Item("CLoseByDate")
                WitnessedByDate = dr.Item("WitnessedByDate")
                OpenWitnessedByDate = dr.Item("OpenWitnessedByDate")
                CloseWitnessedByDate = dr.Item("Closewitnessedbydate")
                GreaterByDate = dr.Item("GreaterByDate")
                OpenGreaterByDate = dr.Item("OpenGreaterByDate")
                CloseGreaterByDate = dr.Item("CloseGreaterByDate")
                ComplianceByDate = dr.Item("ComplianceByDate")
                OpenComplianceByDate = dr.Item("OpenComplianceByDate")
                CloseComplianceByDate = dr.Item("CloseComplianceByDate")
            End If


            query = "Select " &
                "concat(strLastName, ', ' ,strFirstName) as Staff, " &
                "DATEDIFF(day, datReceivedDate, GETDATE() ) as DaysOpenByDate " &
                "from EPDUserProfiles, ISMPReportInformation " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and strClosed = 'False' " &
                "and strDelete is NULL " &
                "and " & DateBias & " " &
                "and strReviewingEngineer = @userid " &
                "order by DaysOpenByDate ASC "

            Dim dt2 As DataTable = DB.GetDataTable(query, p)
            For Each dr2 As DataRow In dt2.Rows
                ReDim Preserve MedianArrayByDateOpen(i)
                MedianArrayByDateOpen(i) = CInt(dr2.Item("DaysOpenByDate"))
                i += 1
            Next

            query = "Select " &
            "concat(strLastName, ', ' ,strFirstName) as Staff, " &
            "datediff(day, datReceivedDate, datCompleteDate ) as DaysCloseByDate " &
            "from EPDUserProfiles, ISMPReportInformation " &
            "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
            "and strClosed = 'True' " &
            "and strDelete is NULL " &
            "and " & DateBias & " " &
            "and strReviewingEngineer = @userid " &
            "order by DaysCloseByDate ASC "

            Dim dt3 As DataTable = DB.GetDataTable(query, p)
            For Each dr3 As DataRow In dt3.Rows
                ReDim Preserve MedianArrayByDateClose(j)
                MedianArrayByDateClose(j) = CInt(dr3.Item("DaysCloseByDate"))
                j += 1
            Next

            query = "Select " &
            "distinct concat(strLastName, ', ' ,strFirstName) as Staff,  " &
            "case  " &
            "	when ReceivedTotal is NULL then 0  " &
            "	Else ReceivedTotal  " &
            "end as ReceivedTotal,  " &
            "case  " &
            "	when OpenTotal is NULL then 0  " &
            "	Else OpenTotal  " &
            "End as OpenTotal,  " &
            "Case  " &
            "	when OpenWitnessedTotal is NULL then 0  " &
            "	Else OpenWitnessedTotal  " &
            "End as OpenWitnessedTotal,  " &
            "Case  " &
            "	When OpenComplianceTotal is NULL then 0  " &
            "	Else OpenComplianceTotal  " &
            "End as OpenComplianceTotal,  " &
            "Case  " &
            "	when CloseTotal is NULL then 0  " &
            "	else CloseTotal  " &
            "End as CloseTotal,  " &
            "Case  " &
            "	when ClosedWitnessedTotal is NULL then 0  " &
            "	Else ClosedWitnessedTotal  " &
            "End as ClosedWitnessedTotal,  " &
            "Case  " &
            "	when ClosedComplianceTotal is NULL then 0  " &
            "	Else ClosedComplianceTotal " &
            "End as ClosedComplianceTotal,  " &
            "Case  " &
            "when OpenGreaterTotal is NULL then 0   " &
            "Else OpenGreaterTotal   " &
            "End as OpenGreaterTotal, " &
            "Case  " &
            "when ClosedGreaterTotal is NULL then 0   " &
            "Else ClosedGreaterTotal   " &
            "End as ClosedGreaterTotal   " &
            "from EPDUserProfiles " &
            " INNER JOIN ISMPReportInformation " &
            "ON EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
            " LEFT JOIN (Select strReviewingEngineer,  " &
            "count(*) as ReceivedTotal  " &
            "from ISMPReportInformation  " &
            "where strDelete is NULL  " &
            "Group by strReviewingEngineer) ReceivedTotals " &
            "ON ISMPReportInformation.strReviewingEngineer = ReceivedTotals.strReviewingEngineer " &
            " LEFT JOIN (Select strReviewingEngineer,  " &
            "count(*) as OpenTotal " &
            "from ISMPReportInformation  " &
            "where strClosed = 'False' " &
            "and strDelete is NULL  " &
            "Group by strReviewingEngineer) OpenTotals  " &
            "ON ISMPReportInformation.strReviewingEngineer = OpenTotals.strReviewingEngineer " &
            " LEFT JOIN (select strWitnessingEngineer,  " &
            "count(*) as OpenWitnessedTotal  " &
            "from ISMPReportInformation  " &
            "where strClosed = 'False' " &
            "and strDelete is Null " &
            "group by strWitnessingEngineer) OpenWitnessedTotals  " &
            "ON ISMPReportInformation.strReviewingEngineer = OpenWitnessedTotals.strWitnessingEngineer " &
            " LEFT JOIN (select strReviewingEngineer,  " &
            "count(*) as OpenComplianceTotal  " &
            "from ISMPReportInformation  " &
            "where strComplianceStatus = '05' " &
            "and strClosed = 'False' " &
            "and strDelete is NULL " &
            "group by strReviewingEngineer) OpenComplianceTotals  " &
            "ON ISMPReportInformation.strReviewingEngineer = OpenComplianceTotals.strReviewingEngineer " &
            " LEFT JOIN (select strReviewingEngineer,  " &
            "count(*) as CloseTotal  " &
            "from ISMPReportInformation  " &
            "where strClosed = 'True'  " &
            "and strDelete is NULL " &
            "Group by strReviewingEngineer) CloseTotals  " &
            "ON ISMPReportInformation.strReviewingEngineer = CloseTotals.strReviewingEngineer " &
            " LEFT JOIN (select strWitnessingEngineer,  " &
            "count(*) as ClosedWitnessedTotal  " &
            "from ISMPReportInformation  " &
            "where strClosed = 'True' " &
            "and strDelete is NULL  " &
            "group by strWitnessingEngineer) ClosedWitnessedTotals  " &
            "ON ISMPReportInformation.strReviewingEngineer = ClosedWitnessedTotals.strWitnessingEngineer " &
            " LEFT JOIN (select strReviewingEngineer,  " &
            "count(*) as ClosedComplianceTotal  " &
            "from ISMPReportInformation  " &
            "where strComplianceStatus = '05' " &
            "and strClosed = 'True' " &
            "and strDelete is NULL " &
            "group by strReviewingEngineer) ClosedComplianceTotals " &
            "ON ISMPReportInformation.strReviewingEngineer = ClosedCompliancetotals.strReviewingEngineer " &
            " LEFT JOIN (select strReviewingEngineer, count(*) as OpenGreaterTotal " &
            "from ISMPReportInformation  " &
            "where strDelete is NULL  " &
            "and strClosed = 'False'  " &
            "and datReceivedDate < DATEADD(day, -50, GETDATE() )  " &
            "Group by strReviewingEngineer) OpenGreaterTotals " &
            "ON ISMPReportInformation.strReviewingEngineer = OpenGreaterTotals.strReviewingEngineer " &
            " LEFT JOIN (select strReviewingEngineer, count(*) as ClosedGreaterTotal " &
            "from ISMPReportInformation  " &
            "where strDelete is NULL  " &
            "and strClosed = 'True'  " &
            "and datReceivedDate < dateadd(day,-50, datCompleteDate)  " &
            "Group by strReviewingEngineer) ClosedGreaterTotals " &
            "ON ISMPReportInformation.strReviewingEngineer = ClosedGreaterTotals.strReviewingEngineer " &
            "where ISMPReportInformation.strReviewingEngineer = @userid "

            Dim dt4 As DataTable = DB.GetDataTable(query, p)
            For Each dr4 As DataRow In dt4.Rows
                ReceivedTotal = dr4.Item("ReceivedTotal")
                OpenTotal = dr4.Item("OpenTotal")
                OpenWitnessedTotal = dr4.Item("OpenWitnessedTotal")
                OpenComplianceTotal = dr4.Item("OpenComplianceTotal")
                OpenGreaterTotal = dr4.Item("OpenGreaterTotal")
                ClosedTotal = dr4.Item("CloseTotal")
                ClosedWitnessedTotal = dr4.Item("ClosedWitnessedTotal")
                ClosedComplianceTotal = dr4.Item("ClosedComplianceTotal")
                ClosedGreaterTotal = dr4.Item("ClosedGreaterTotal")
            Next

            query = "Select " &
            "concat(strLastName, ', ' ,strFirstName) as Staff, " &
            "DATEDIFF(day, datReceivedDate, GETDATE() ) as DaysOpen " &
            "from EPDUSerProfiles, ISMPReportInformation " &
            "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
            "and strClosed = 'False' " &
            "and strDelete is NULL " &
            "and strReviewingEngineer = @userid " &
            "order by DaysOpen ASC "

            Dim dt5 As DataTable = DB.GetDataTable(query, p)
            For Each dr5 As DataRow In dt5.Rows
                ReDim Preserve MedianArrayOpen(n)
                MedianArrayOpen(n) = CInt(dr5.Item("DaysOpen"))
                n += 1
            Next

            query = "Select " &
            " concat(strLastName, ', ' ,strFirstName) as Staff, " &
            "datediff(day,datReceivedDate,datCompleteDate ) as DaysClosed " &
            "from EPDUserProfiles, ISMPReportInformation " &
            "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
            "and strClosed = 'True' " &
            "and strDelete is NULL " &
            "and strReviewingEngineer = @userid " &
            "order by DaysClosed ASC "


            Dim dt6 As DataTable = DB.GetDataTable(query, p)
            For Each dr6 As DataRow In dt6.Rows
                ReDim Preserve MedianArrayClosed(o)
                MedianArrayClosed(o) = CInt(dr6.Item("DaysClosed"))
                o += 1
            Next

            If MedianArrayByDateOpen.GetLength(0) Mod 2 = 0 Then
                    OpenMedianByDate = (MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) / 2) - 1) + MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) / 2))) / 2
                    If MedianArrayByDateOpen.GetLength(0) <= 2 Then
                        OpenPercentileByDate = "Unavailable"
                    Else
                        OpenPercentileByDate = (MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) * 0.8) - 1) + MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    OpenMedianByDate = MedianArrayByDateOpen(MedianArrayByDateOpen.GetLength(0) \ 2)
                    If MedianArrayByDateOpen.GetLength(0) <= 2 Then
                        OpenPercentileByDate = "Unavailable"
                    Else
                        OpenPercentileByDate = MedianArrayByDateOpen(MedianArrayByDateOpen.GetLength(0) * 0.8)
                    End If
                End If

                If MedianArrayByDateClose.GetLength(0) Mod 2 = 0 Then
                    CloseMedianByDate = (MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) / 2) - 1) + MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) / 2))) / 2
                    If MedianArrayByDateClose.GetLength(0) <= 2 Then
                        ClosePercentileByDate = "Unavailable"
                    Else
                        ClosePercentileByDate = (MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) * 0.8) - 1) + MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    CloseMedianByDate = MedianArrayByDateClose(MedianArrayByDateClose.GetLength(0) \ 2)
                    If MedianArrayByDateClose.GetLength(0) <= 2 Then
                        ClosePercentileByDate = "Unavailable"
                    Else
                        ClosePercentileByDate = MedianArrayByDateClose(MedianArrayByDateClose.GetLength(0) * 0.8)
                    End If
                End If

                If MedianArrayOpen.GetLength(0) Mod 2 = 0 Then
                    OpenMedianTotal = (MedianArrayOpen((MedianArrayOpen.GetLength(0) / 2) - 1) + MedianArrayOpen((MedianArrayOpen.GetLength(0) / 2))) / 2
                    If MedianArrayOpen.GetLength(0) <= 2 Then
                        PercentileOpenTotalDay = "Unavailable"
                    Else
                        PercentileOpenTotalDay = (MedianArrayOpen((MedianArrayOpen.GetLength(0) * 0.8) - 1) + MedianArrayOpen((MedianArrayOpen.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    OpenMedianTotal = MedianArrayOpen(MedianArrayOpen.GetLength(0) \ 2)
                    If MedianArrayOpen.GetLength(0) <= 2 Then
                        PercentileOpenTotalDay = "Unavailable"
                    Else
                        PercentileOpenTotalDay = MedianArrayOpen(MedianArrayOpen.GetLength(0) * 0.8)
                    End If
                End If

                If MedianArrayClosed.GetLength(0) Mod 2 = 0 Then
                    ClosedMedianTotal = (MedianArrayClosed((MedianArrayClosed.GetLength(0) / 2) - 1) + MedianArrayClosed((MedianArrayClosed.GetLength(0) / 2))) / 2
                    If MedianArrayClosed.GetLength(0) <= 2 Then
                        PercentileClosedTotalDay = "Unavailable"
                    Else
                        PercentileClosedTotalDay = (MedianArrayClosed((MedianArrayClosed.GetLength(0) * 0.8) - 1) + MedianArrayClosed((MedianArrayClosed.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    ClosedMedianTotal = MedianArrayClosed(MedianArrayClosed.GetLength(0) \ 2)
                    If MedianArrayClosed.GetLength(0) <= 2 Then
                        PercentileClosedTotalDay = "Unavailable"
                    Else
                        PercentileClosedTotalDay = MedianArrayClosed(MedianArrayClosed.GetLength(0) * 0.8)
                    End If
                End If

                Statement = Statement &
                "For the Staff member: " & Staff & vbCrLf &
                vbTab & DateStatement & vbCrLf & vbCrLf &
                "1. " & ReceivedByDate & " Test Reports Received " & vbCrLf &
                "2. " & OpenByDate & " of these " & ReceivedByDate & " Test Reports are currently open" & vbCrLf &
                "3. " & ClosedByDate & " of these " & ReceivedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf &
                "4. " & WitnessedByDate & " of these " & ReceivedByDate & " Test Reports were witnessed by " & Staff & vbCrLf &
                "5. " & OpenWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are still open " & vbCrLf &
                "6. " & CloseWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf &
                "7. " & GreaterByDate & " of these " & ReceivedByDate & " Test Reports have been open for more than 50-days" & vbCrLf &
                "8. " & OpenGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more than 50-days are still open " & vbCrLf &
                "9. " & CloseGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more then 50-days are currently closed " & vbCrLf & vbCrLf &
                "10. " & ComplianceByDate & " of these " & ReceivedByDate & " Test Reports were out of compliance" & vbCrLf &
                "11. " & OpenComplianceByDate & " of these " & ComplianceByDate & " Test Reports are still open " & vbCrLf &
                "12. " & CloseComplianceByDate & " of these " & ComplianceByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf &
                "13. The median time taken to complete those " & ClosedByDate & " Closed Test Reports was " & CloseMedianByDate & "-days" & vbCrLf &
                "14. The 80% Percentile Time taken to complete those " & ClosedByDate & " Closed Test Reports was " & ClosePercentileByDate & "-days" & vbCrLf &
                "15. The median time of the " & OpenByDate & " Open Test Reports is " & OpenMedianByDate & "-days" & vbCrLf &
                "16. The 80% Percentile Time of the " & OpenByDate & " Open Test Reports is " & OpenPercentileByDate & "-days" & vbCrLf & vbCrLf &
                "17. Overall " & Staff & " has received " & ReceivedTotal & " Test Reports" & vbCrLf & vbCrLf &
                "18. " & OpenTotal & " of " & ReceivedTotal & " Test Reports are currently open" & vbCrLf &
                "19. " & OpenWitnessedTotal & " of these " & OpenTotal & " Test Reports have been witnessed" & vbCrLf &
                "20. " & OpenComplianceTotal & " of these " & OpenTotal & " Test Reports are currently out of compliance " & vbCrLf &
                "21. " & OpenGreaterTotal & " of these " & OpenTotal & " Test Reports have been open for more than 50-days" & vbCrLf &
                "22. The median time of the " & OpenTotal & " Open Test Reports is " & OpenMedianTotal & "-days" & vbCrLf &
                "23. The 80% Percentile Time of the " & OpenTotal & " Open Test Reports is " & PercentileOpenTotalDay & "-days" & vbCrLf & vbCrLf &
                "24. " & ClosedTotal & " of " & ReceivedTotal & " Test Reports are currently closed " & vbCrLf &
                "25. " & ClosedWitnessedTotal & " of these " & ClosedTotal & " Test Reports have been witnessed" & vbCrLf &
                "26. " & ClosedComplianceTotal & " of these " & ClosedTotal & " Test Reports are out of compliance " & vbCrLf &
                "27. " & ClosedGreaterTotal & " of these " & ClosedTotal & " Test Reports were open for more than 50-days" & vbCrLf &
                "28. The median time of the " & ClosedTotal & " Closed Test Reports was " & ClosedMedianTotal & "-days" & vbCrLf &
                "29. The 80% Percentile Time of the " & ClosedTotal & " Closed Test Reports was " & PercentileClosedTotalDay & "-days" & vbCrLf & vbCrLf & vbCrLf

                txtEngineerStatistics.Text = txtEngineerStatistics.Text & Statement

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

End Class