'Imports System.DateTime
Imports System.Data.SqlClient
Imports System.Windows.Forms
'Imports Microsoft.Office.Core
'Imports System.IO
Imports System
Imports System.Data
'Imports System.Text
'Imports System.Data.SqlClient

Public Class ISMPStaffReports
    Dim SQL, SQL2, SQL3 As String
    Dim SQL4, SQL5, SQL6 As String
    Dim cmd, cmd2, cmd3 As SqlCommand
    Dim cmd4, cmd5, cmd6 As SqlCommand
    Dim dr, dr2, dr3 As SqlDataReader
    Dim dr4, dr5, dr6 As SqlDataReader

    Private Sub ISMPStaffReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            DTPUnitStart.Text = Format(Date.Today.AddMonths(-6), "dd-MMM-yyyy")
            DTPUnitEnd.Text = Format(Date.Today, "dd-MMM-yyyy")


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbRunEngineerStatReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbRunEngineerStatReport.LinkClicked
        Try
            EngineerUnitStats()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub EngineerUnitStats()
        Try

            txtEngineerStatistics.Clear()
            RunUnitEngineerStatistics(CurrentUser.UserID)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub RunUnitEngineerStatistics(EngineerGCode As String)
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


            If rdbUnitDateTestStarted.Checked = True Then
                DateBias = "datTestDateStart between '" & DTPUnitStart.Text & "' " &
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Tests Conducted between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPUnitStart.Text & "' " &
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Test Reports Received between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPUnitStart.Text & "' " &
                "and '" & DTPUnitEnd.Text & "'"
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

            If EngineerGCode = "" Then

            Else
                SQL = "select " &
                "distinct(strLastName|| ', ' ||strFirstName) as Staff,  " &
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
                "from EPDUserProfiles, ISMPReportInformation,  " &
                "(Select strReviewingEngineer,  count(*) as ReceivedByDate   " &
                "from ISMPReportInformation   " &
                "where strDelete is NULL " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) ReceivedByDates,  " &
                "(Select strReviewingEngineer,  " &
                "count(*) as OpenByDate  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'False'  " &
                "and strDelete is NULL  " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) OpenByDates,  " &
                "(Select strReviewingEngineer,  " &
                "count(*) as CloseByDate  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'True'  " &
                "and StrDelete is NULL  " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) CloseByDates,  " &
                "(Select strWitnessingEngineer,  " &
                "count(*) as WitnessedByDate  " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and " & DateBias & " " &
                "group by strWitnessingEngineer) WitnessedByDates,  " &
                "(Select strWitnessingEngineer,  " &
                "count(*) as OpenWitnessedByDate   " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'False'  " &
                 "and " & DateBias & " " &
                "group by strWitnessingEngineer) OpenWitnessedByDates,  " &
                "(select strWitnessingEngineer,  " &
                "count(*) as CloseWitnessedByDate   " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'True' " &
                "and " & DateBias & " " &
                "group by strwitnessingEngineer) CloseWitnessedByDates,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as GreaterByDate " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and datReceivedDate < if strClosed = 'False' then DATEADD(day, -50, GETDATE()), " &
                "                                        else DATEADD(day, -50, datCompleteDate) end " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) GreaterByDates,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as OpenGreaterByDate " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'False'  " &
                "and datReceivedDate < (trunc(GETDATE()) - 50)  " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) OpenGreaterByDates,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as CloseGreaterByDate " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'True'  " &
                "and datReceivedDate < (-50 + datCompleteDate) " &
                "and " & DateBias & " " &
                "Group by strReviewingEngineer) CloseGreaterByDates,  " &
                "(select strReviewingEngineer, " &
                "count(*) as ComplianceByDate " &
                "from ISMPReportInformation " &
                "where strComplianceStatus = '05' " &
                "and strDelete is NULL " &
                "and " & DateBias & " " &
                "group by strReviewingEngineer) ComplianceByDates, " &
                "(select strReviewingEngineer,   " &
                "count(*) as OpenComplianceByDate  " &
                "from ISMPReportInformation   " &
                "where strComplianceStatus = '05'  " &
                "and strClosed = 'False'  " &
                "and strDelete is NULL  " &
                "and " & DateBias & " " &
                "group by strReviewingEngineer) OpenComplianceByDates,   " &
                "(Select strReviewingEngineer,  " &
                "count(*) as CloseComplianceByDate  " &
                "from ISMPReportInformation   " &
                "where strComplianceStatus = '05'  " &
                "and strClosed = 'True'  " &
                "and strDelete is NULL  " &
                "and " & DateBias & " " &
                "group by strReviewingEngineer) CloseComplianceByDates   " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReviewingEngineer = ReceivedByDates.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenBYDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = CloseByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = WitnessedByDates.strWitnessingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = OpenwitnessedByDates.strWitnessingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = CloseWitnessedByDates.strWitnessingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = GreaterByDates.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenGreaterByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = CloseGreaterByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = ComplianceByDates.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenComplianceByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strREviewingEngineer = CloseComplianceByDates.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = '" & EngineerGCode & "' "

                SQL2 = "Select " &
                "(strLastName|| ', ' ||strFirstName) as Staff, " &
                "(trunc(GETDATE()) - datReceivedDate) as DaysOpenByDate " &
                "from EPDUserProfiles, ISMPReportInformation " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and strClosed = 'False' " &
                "and strDelete is NULL " &
                "and " & DateBias & " " &
                "and strReviewingEngineer = '" & EngineerGCode & "' " &
                "order by DaysOpenByDate ASC "

                SQL3 = "Select " &
                "(strLastName|| ', ' ||strFirstName) as Staff, " &
                "(datCompleteDate - datReceivedDate) as DaysCloseByDate " &
                "from EPDUserProfiles, ISMPReportInformation " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and strClosed = 'True' " &
                "and strDelete is NULL " &
                "and " & DateBias & " " &
                "and strReviewingEngineer = '" & EngineerGCode & "' " &
                "order by DaysCloseByDate ASC "

                SQL4 = "Select " &
                "distinct(strLastName|| ', ' ||strFirstName) as Staff,  " &
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
                "from EPDUserProfiles, ISMPReportInformation, " &
                "(Select strReviewingEngineer,  " &
                "count(*) as ReceivedTotal  " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "Group by strReviewingEngineer) ReceivedTotals,  " &
                "(Select strReviewingEngineer,  " &
                "count(*) as OpenTotal " &
                "from ISMPReportInformation  " &
                "where strClosed = 'False' " &
                "and strDelete is NULL  " &
                "Group by strReviewingEngineer) OpenTotals,  " &
                "(select strWitnessingEngineer,  " &
                "count(*) as OpenWitnessedTotal  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'False' " &
                "and strDelete is Null " &
                "group by strWitnessingEngineer) OpenWitnessedTotals,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as OpenComplianceTotal  " &
                "from ISMPReportInformation  " &
                "where strComplianceStatus = '05' " &
                "and strClosed = 'False' " &
                "and strDelete is NULL " &
                "group by strReviewingEngineer) OpenComplianceTotals,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as CloseTotal  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'True'  " &
                "and strDelete is NULL " &
                "Group by strReviewingEngineer) CloseTotals,  " &
                "(select strWitnessingEngineer,  " &
                "count(*) as ClosedWitnessedTotal  " &
                "from ISMPReportInformation  " &
                "where strClosed = 'True' " &
                "and strDelete is NULL  " &
                "group by strWitnessingEngineer) ClosedWitnessedTotals,  " &
                "(select strReviewingEngineer,  " &
                "count(*) as ClosedComplianceTotal  " &
                "from ISMPReportInformation  " &
                "where strComplianceStatus = '05' " &
                "and strClosed = 'True' " &
                "and strDelete is NULL " &
                "group by strReviewingEngineer) ClosedComplianceTotals, " &
                "(select strReviewingEngineer, count(*) as OpenGreaterTotal " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'False'  " &
                "and datReceivedDate < (trunc(GETDATE()) - 50)  " &
                "Group by strReviewingEngineer) OpenGreaterTotals, " &
                "(select strReviewingEngineer, count(*) as ClosedGreaterTotal " &
                "from ISMPReportInformation  " &
                "where strDelete is NULL  " &
                "and strClosed = 'True'  " &
                "and datReceivedDate < (-50 + datCompleteDate)  " &
                "Group by strReviewingEngineer) ClosedGreaterTotals " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and ISMPReportInformation.strReviewingEngineer = ReceivedTotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenTotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenWitnessedTotals.strWitnessingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenComplianceTotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = CloseTotals.strReviewingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = ClosedWitnessedTotals.strWitnessingEngineer (+)  " &
                "and ISMPReportInformation.strReviewingEngineer = ClosedCompliancetotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = OpenGreaterTotals.strReviewingEngineer (+) " &
                "and ISMPReportInformation.strReviewingEngineer = ClosedGreaterTotals.strReviewingEngineer (+)   " &
                "and ISMPReportInformation.strReviewingEngineer = '" & EngineerGCode & "' "

                SQL5 = "Select " &
                "(strLastName|| ', ' ||strFirstName) as Staff, " &
                "(trunc(GETDATE()) - datReceivedDate) as DaysOpen " &
                "from EPDUSerProfiles, ISMPReportInformation " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and strClosed = 'False' " &
                "and strDelete is NULL " &
                "and strReviewingEngineer = '" & EngineerGCode & "' " &
                "order by DaysOpen ASC "

                SQL6 = "Select " &
                "(strLastName|| ', ' ||strFirstName) as Staff, " &
                "(datCompleteDate -datReceivedDate) as DaysClosed " &
                "from EPDUserProfiles, ISMPReportInformation " &
                "where EPDUserProfiles.numUserID = ISMPReportInformation.strReviewingEngineer  " &
                "and strClosed = 'True' " &
                "and strDelete is NULL " &
                "and strReviewingEngineer = '" & EngineerGCode & "' " &
                "order by DaysClosed ASC "

                cmd = New SqlCommand(SQL, CurrentConnection)
                cmd2 = New SqlCommand(SQL2, CurrentConnection)
                cmd3 = New SqlCommand(SQL3, CurrentConnection)
                cmd4 = New SqlCommand(SQL4, CurrentConnection)
                cmd5 = New SqlCommand(SQL5, CurrentConnection)
                cmd6 = New SqlCommand(SQL6, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                Try

                    dr = cmd.ExecuteReader

                    While dr.Read
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
                    End While

                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        ReDim Preserve MedianArrayByDateOpen(i)
                        MedianArrayByDateOpen(i) = CInt(dr2.Item("DaysOpenByDate"))
                        i += 1
                    End While

                    dr3 = cmd3.ExecuteReader
                    While dr3.Read
                        ReDim Preserve MedianArrayByDateClose(j)
                        MedianArrayByDateClose(j) = CInt(dr3.Item("DaysCloseByDate"))
                        j += 1
                    End While

                    dr4 = cmd4.ExecuteReader
                    While dr4.Read
                        ReceivedTotal = dr4.Item("ReceivedTotal")
                        OpenTotal = dr4.Item("OpenTotal")
                        OpenWitnessedTotal = dr4.Item("OpenWitnessedTotal")
                        OpenComplianceTotal = dr4.Item("OpenComplianceTotal")
                        OpenGreaterTotal = dr4.Item("OpenGreaterTotal")
                        ClosedTotal = dr4.Item("CloseTotal")
                        ClosedWitnessedTotal = dr4.Item("ClosedWitnessedTotal")
                        ClosedComplianceTotal = dr4.Item("ClosedComplianceTotal")
                        ClosedGreaterTotal = dr4.Item("ClosedGreaterTotal")
                    End While

                    dr5 = cmd5.ExecuteReader
                    While dr5.Read
                        ReDim Preserve MedianArrayOpen(n)
                        MedianArrayOpen(n) = CInt(dr5.Item("DaysOpen"))
                        n += 1
                    End While

                    dr6 = cmd6.ExecuteReader
                    While dr6.Read
                        ReDim Preserve MedianArrayClosed(o)
                        MedianArrayClosed(o) = CInt(dr6.Item("DaysClosed"))
                        o += 1
                    End While

                Catch ex As Exception
                    ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
                Finally

                End Try
                ' 



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

    Private Sub llbExportStatsToWord_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbExportStatsToWord.LinkClicked
        Dim WordText As String
        'Dim WordApp As New Word.ApplicationClass
        'Dim wordDoc As Word.DocumentClass
        Dim wordDoc As Microsoft.Office.Interop.Word.Document
        Dim WordApp As New Microsoft.Office.Interop.Word.Application
        Try

            WordText = txtEngineerStatistics.Text

            wordDoc = WordApp.Documents.Add()
            wordDoc.Activate()
            WordApp.Selection.TypeText(WordText)
            WordApp.Visible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiHelp_Click(sender As Object, e As EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class