'Imports System.DateTime
Imports Oracle.DataAccess.Client
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
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim cmd4, cmd5, cmd6 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader
    Dim dr4, dr5, dr6 As OracleDataReader

    Private Sub ISMPStaffReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            DTPUnitStart.Text = Format(Date.Today.AddMonths(-6), "dd-MMM-yyyy")
            DTPUnitEnd.Text = Format(Date.Today, "dd-MMM-yyyy")


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbRunEngineerStatReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbRunEngineerStatReport.LinkClicked
        Try
            EngineerUnitStats()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub EngineerUnitStats()
        Try

            txtEngineerStatistics.Clear()
            RunUnitEngineerStatistics(UserGCode)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub RunUnitEngineerStatistics(ByVal EngineerGCode As String)
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
        Dim OpenGrearerDaysTotal As String = "X"
        Dim OpenMedianTotal As String = "X"
        Dim PercentileOpenTotalDay As String = "X"
        Dim ClosedTotal As String = "X"
        Dim ClosedWitnessedTotal As String = "X"
        Dim ClosedComplianceTotal As String = "X"
        Dim ClosedGreaterTotal As String = "X"
        Dim ClosedGreaterDaysTotal As String = "X"
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
                DateBias = "datTestDateStart between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Tests Conducted between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Test Reports Received between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Test Reports Completed between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitStatsAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were: "
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were:"
            End If

            If EngineerGCode = "" Then

            Else
                SQL = "select " & _
                "distinct(strLastName|| ', ' ||strFirstName) as Staff,  " & _
                "case " & _
                "	when ReceivedByDate is NULL then 0  " & _
                "	Else ReceivedByDate " & _
                "End as ReceivedByDate,  " & _
                "Case  " & _
                "	when OpenByDate is Null then 0  " & _
                "	Else OpenByDate  " & _
                "End as OpenByDate,  " & _
                "Case  " & _
                "	WHEN CloseByDate is Null then 0  " & _
                "	Else CloseByDate " & _
                "End as CloseByDate,  " & _
                "Case  " & _
                "	when WitnessedByDate is Null then 0  " & _
                "	Else WitnessedByDate  " & _
                "End as WitnessedByDate, " & _
                "case  " & _
                "	when OpenWitnessedByDate is NULL then 0  " & _
                "	Else OpenWitnessedByDate  " & _
                "End as OpenWitnessedByDate,  " & _
                "case  " & _
                "	when CloseWitnessedByDate is NULL then 0  " & _
                "	Else CloseWitnessedByDate  " & _
                "End as CloseWitnessedByDate,  " & _
                "Case " & _
                "   when GreaterByDate is NUll then 0 " & _
                "   Else GreaterByDate " & _
                "End as GreaterByDate, " & _
                "case  " & _
                "	when OpenGreaterByDate is NULL then 0  " & _
                "	Else OpenGreaterByDate " & _
                "end as OpenGreaterByDate,    " & _
                "case  " & _
                "	When CloseGreaterByDate is NULL then 0  " & _
                "	Else CloseGreaterByDate  " & _
                "End as CloseGreaterByDate,  " & _
                "Case " & _
                "   when ComplianceByDate is NULL then 0 " & _
                "   Else ComplianceByDate " & _
                "End as ComplianceByDate, " & _
                "Case  " & _
                "	when OpenComplianceByDate is NULL then 0  " & _
                "	Else OpenComplianceByDate " & _
                "End as OpenComplianceByDate,  " & _
                "Case  " & _
                "	When CloseComplianceByDate is NULL then 0  " & _
                "	Else CloseComplianceByDate " & _
                "End as CloseComplianceByDate  " & _
                "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".ISMPReportInformation,  " & _
                "(Select strReviewingEngineer,  count(*) as ReceivedByDate   " & _
                "from " & DBNameSpace & ".ISMPReportInformation   " & _
                "where strDelete is NULL " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) ReceivedByDates,  " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as OpenByDate  " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'False'  " & _
                "and strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) OpenByDates,  " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as CloseByDate  " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'True'  " & _
                "and StrDelete is NULL  " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) CloseByDates,  " & _
                "(Select strWitnessingEngineer,  " & _
                "count(*) as WitnessedByDate  " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "group by strWitnessingEngineer) WitnessedByDates,  " & _
                "(Select strWitnessingEngineer,  " & _
                "count(*) as OpenWitnessedByDate   " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'False'  " & _
                 "and " & DateBias & " " & _
                "group by strWitnessingEngineer) OpenWitnessedByDates,  " & _
                "(select strWitnessingEngineer,  " & _
                "count(*) as CloseWitnessedByDate   " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'True' " & _
                "and " & DateBias & " " & _
                "group by strwitnessingEngineer) CloseWitnessedByDates,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as GreaterByDate " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and datReceivedDate < Decode(strClosed, 'False', (trunc(sysdate) - 50), " & _
                "                                        'True', (-50 + datCompleteDate)) " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) GreaterByDates,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as OpenGreaterByDate " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'False'  " & _
                "and datReceivedDate < (trunc(sysdate) - 50)  " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) OpenGreaterByDates,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as CloseGreaterByDate " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'True'  " & _
                "and datReceivedDate < (-50 + datCompleteDate) " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) CloseGreaterByDates,  " & _
                "(select strReviewingEngineer, " & _
                "count(*) as ComplianceByDate " & _
                "from " & DBNameSpace & ".ISMPReportInformation " & _
                "where strComplianceStatus = '05' " & _
                "and strDelete is NULL " & _
                "and " & DateBias & " " & _
                "group by strReviewingEngineer) ComplianceByDates, " & _
                "(select strReviewingEngineer,   " & _
                "count(*) as OpenComplianceByDate  " & _
                "from " & DBNameSpace & ".ISMPReportInformation   " & _
                "where strComplianceStatus = '05'  " & _
                "and strClosed = 'False'  " & _
                "and strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "group by strReviewingEngineer) OpenComplianceByDates,   " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as CloseComplianceByDate  " & _
                "from " & DBNameSpace & ".ISMPReportInformation   " & _
                "where strComplianceStatus = '05'  " & _
                "and strClosed = 'True'  " & _
                "and strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "group by strReviewingEngineer) CloseComplianceByDates   " & _
                "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = ReceivedByDates.strReviewingEngineer (+) " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenBYDates.strReviewingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = CloseByDates.strReviewingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = WitnessedByDates.strWitnessingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenwitnessedByDates.strWitnessingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = CloseWitnessedByDates.strWitnessingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = GreaterByDates.strReviewingEngineer (+) " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenGreaterByDates.strReviewingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = CloseGreaterByDates.strReviewingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = ComplianceByDates.strReviewingEngineer (+) " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenComplianceByDates.strReviewingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strREviewingEngineer = CloseComplianceByDates.strReviewingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = '" & EngineerGCode & "' "

                SQL2 = "Select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "(trunc(sysdate) - datReceivedDate) as DaysOpenByDate " & _
                "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".ISMPReportInformation " & _
                "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'False' " & _
                "and strDelete is NULL " & _
                "and " & DateBias & " " & _
                "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                "order by DaysOpenByDate ASC "

                SQL3 = "Select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "(datCompleteDate - datReceivedDate) as DaysCloseByDate " & _
                "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".ISMPReportInformation " & _
                "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'True' " & _
                "and strDelete is NULL " & _
                "and " & DateBias & " " & _
                "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                "order by DaysCloseByDate ASC "

                SQL4 = "Select " & _
                "distinct(strLastName|| ', ' ||strFirstName) as Staff,  " & _
                "case  " & _
                "	when ReceivedTotal is NULL then 0  " & _
                "	Else ReceivedTotal  " & _
                "end as ReceivedTotal,  " & _
                "case  " & _
                "	when OpenTotal is NULL then 0  " & _
                "	Else OpenTotal  " & _
                "End as OpenTotal,  " & _
                "Case  " & _
                "	when OpenWitnessedTotal is NULL then 0  " & _
                "	Else OpenWitnessedTotal  " & _
                "End as OpenWitnessedTotal,  " & _
                "Case  " & _
                "	When OpenComplianceTotal is NULL then 0  " & _
                "	Else OpenComplianceTotal  " & _
                "End as OpenComplianceTotal,  " & _
                "Case  " & _
                "	when CloseTotal is NULL then 0  " & _
                "	else CloseTotal  " & _
                "End as CloseTotal,  " & _
                "Case  " & _
                "	when ClosedWitnessedTotal is NULL then 0  " & _
                "	Else ClosedWitnessedTotal  " & _
                "End as ClosedWitnessedTotal,  " & _
                "Case  " & _
                "	when ClosedComplianceTotal is NULL then 0  " & _
                "	Else ClosedComplianceTotal " & _
                "End as ClosedComplianceTotal,  " & _
                "Case  " & _
                "when OpenGreaterTotal is NULL then 0   " & _
                "Else OpenGreaterTotal   " & _
                "End as OpenGreaterTotal, " & _
                "Case  " & _
                "when ClosedGreaterTotal is NULL then 0   " & _
                "Else ClosedGreaterTotal   " & _
                "End as ClosedGreaterTotal   " & _
                "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".ISMPReportInformation, " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as ReceivedTotal  " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "Group by strReviewingEngineer) ReceivedTotals,  " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as OpenTotal " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'False' " & _
                "and strDelete is NULL  " & _
                "Group by strReviewingEngineer) OpenTotals,  " & _
                "(select strWitnessingEngineer,  " & _
                "count(*) as OpenWitnessedTotal  " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'False' " & _
                "and strDelete is Null " & _
                "group by strWitnessingEngineer) OpenWitnessedTotals,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as OpenComplianceTotal  " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strComplianceStatus = '05' " & _
                "and strClosed = 'False' " & _
                "and strDelete is NULL " & _
                "group by strReviewingEngineer) OpenComplianceTotals,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as CloseTotal  " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'True'  " & _
                "and strDelete is NULL " & _
                "Group by strReviewingEngineer) CloseTotals,  " & _
                "(select strWitnessingEngineer,  " & _
                "count(*) as ClosedWitnessedTotal  " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'True' " & _
                "and strDelete is NULL  " & _
                "group by strWitnessingEngineer) ClosedWitnessedTotals,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as ClosedComplianceTotal  " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strComplianceStatus = '05' " & _
                "and strClosed = 'True' " & _
                "and strDelete is NULL " & _
                "group by strReviewingEngineer) ClosedComplianceTotals, " & _
                "(select strReviewingEngineer, count(*) as OpenGreaterTotal " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'False'  " & _
                "and datReceivedDate < (trunc(sysdate) - 50)  " & _
                "Group by strReviewingEngineer) OpenGreaterTotals, " & _
                "(select strReviewingEngineer, count(*) as ClosedGreaterTotal " & _
                "from " & DBNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'True'  " & _
                "and datReceivedDate < (-50 + datCompleteDate)  " & _
                "Group by strReviewingEngineer) ClosedGreaterTotals " & _
                "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = ReceivedTotals.strReviewingEngineer (+) " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenTotals.strReviewingEngineer (+) " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenWitnessedTotals.strWitnessingEngineer (+) " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenComplianceTotals.strReviewingEngineer (+) " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = CloseTotals.strReviewingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = ClosedWitnessedTotals.strWitnessingEngineer (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = ClosedCompliancetotals.strReviewingEngineer (+) " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenGreaterTotals.strReviewingEngineer (+) " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = ClosedGreaterTotals.strReviewingEngineer (+)   " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer = '" & EngineerGCode & "' "

                SQL5 = "Select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "(trunc(sysdate) - datReceivedDate) as DaysOpen " & _
                "from " & DBNameSpace & ".EPDUSerProfiles, " & DBNameSpace & ".ISMPReportInformation " & _
                "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'False' " & _
                "and strDelete is NULL " & _
                "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                "order by DaysOpen ASC "

                SQL6 = "Select " & _
                "(strLastName|| ', ' ||strFirstName) as Staff, " & _
                "(datCompleteDate -datReceivedDate) as DaysClosed " & _
                "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".ISMPReportInformation " & _
                "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'True' " & _
                "and strDelete is NULL " & _
                "and strReviewingEngineer = '" & EngineerGCode & "' " & _
                "order by DaysClosed ASC "

                cmd = New OracleCommand(SQL, CurrentConnection)
                cmd2 = New OracleCommand(SQL2, CurrentConnection)
                cmd3 = New OracleCommand(SQL3, CurrentConnection)
                cmd4 = New OracleCommand(SQL4, CurrentConnection)
                cmd5 = New OracleCommand(SQL5, CurrentConnection)
                cmd6 = New OracleCommand(SQL6, CurrentConnection)

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

                    Dim tempX As String

                    dr3 = cmd3.ExecuteReader
                    While dr3.Read
                        ReDim Preserve MedianArrayByDateClose(j)
                        MedianArrayByDateClose(j) = CInt(dr3.Item("DaysCloseByDate"))
                        tempX = CInt(dr3.Item("DaysCloseByDate"))
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
                    ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
                Finally
                    If CurrentConnection.State = ConnectionState.Open Then
                        'conn.close()
                    End If
                End Try
                ' 

                If CurrentConnection.State = ConnectionState.Open Then
                    'conn.close()
                End If

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

            Statement = Statement & _
            "For the Staff member: " & Staff & vbCrLf & _
            vbTab & DateStatement & vbCrLf & vbCrLf & _
            "1. " & ReceivedByDate & " Test Reports Received " & vbCrLf & _
            "2. " & OpenByDate & " of these " & ReceivedByDate & " Test Reports are currently open" & vbCrLf & _
            "3. " & ClosedByDate & " of these " & ReceivedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf & _
            "4. " & WitnessedByDate & " of these " & ReceivedByDate & " Test Reports were witnessed by " & Staff & vbCrLf & _
            "5. " & OpenWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are still open " & vbCrLf & _
            "6. " & CloseWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf & _
            "7. " & GreaterByDate & " of these " & ReceivedByDate & " Test Reports have been open for more than 50-days" & vbCrLf & _
            "8. " & OpenGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more than 50-days are still open " & vbCrLf & _
            "9. " & CloseGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more then 50-days are currently closed " & vbCrLf & vbCrLf & _
            "10. " & ComplianceByDate & " of these " & ReceivedByDate & " Test Reports were out of compliance" & vbCrLf & _
            "11. " & OpenComplianceByDate & " of these " & ComplianceByDate & " Test Reports are still open " & vbCrLf & _
            "12. " & CloseComplianceByDate & " of these " & ComplianceByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf & _
            "13. The median time taken to complete those " & ClosedByDate & " Closed Test Reports was " & CloseMedianByDate & "-days" & vbCrLf & _
            "14. The 80% Percentile Time taken to complete those " & ClosedByDate & " Closed Test Reports was " & ClosePercentileByDate & "-days" & vbCrLf & _
            "15. The median time of the " & OpenByDate & " Open Test Reports is " & OpenMedianByDate & "-days" & vbCrLf & _
            "16. The 80% Percentile Time of the " & OpenByDate & " Open Test Reports is " & OpenPercentileByDate & "-days" & vbCrLf & vbCrLf & _
            "17. Overall " & Staff & " has received " & ReceivedTotal & " Test Reports" & vbCrLf & vbCrLf & _
            "18. " & OpenTotal & " of " & ReceivedTotal & " Test Reports are currently open" & vbCrLf & _
            "19. " & OpenWitnessedTotal & " of these " & OpenTotal & " Test Reports have been witnessed" & vbCrLf & _
            "20. " & OpenComplianceTotal & " of these " & OpenTotal & " Test Reports are currently out of compliance " & vbCrLf & _
            "21. " & OpenGreaterTotal & " of these " & OpenTotal & " Test Reports have been open for more than 50-days" & vbCrLf & _
            "22. The median time of the " & OpenTotal & " Open Test Reports is " & OpenMedianTotal & "-days" & vbCrLf & _
            "23. The 80% Percentile Time of the " & OpenTotal & " Open Test Reports is " & PercentileOpenTotalDay & "-days" & vbCrLf & vbCrLf & _
            "24. " & ClosedTotal & " of " & ReceivedTotal & " Test Reports are currently closed " & vbCrLf & _
            "25. " & ClosedWitnessedTotal & " of these " & ClosedTotal & " Test Reports have been witnessed" & vbCrLf & _
            "26. " & ClosedComplianceTotal & " of these " & ClosedTotal & " Test Reports are out of compliance " & vbCrLf & _
            "27. " & ClosedGreaterTotal & " of these " & ClosedTotal & " Test Reports were open for more than 50-days" & vbCrLf & _
            "28. The median time of the " & ClosedTotal & " Closed Test Reports was " & ClosedMedianTotal & "-days" & vbCrLf & _
            "29. The 80% Percentile Time of the " & ClosedTotal & " Closed Test Reports was " & PercentileClosedTotalDay & "-days" & vbCrLf & vbCrLf & vbCrLf

            txtEngineerStatistics.Text = txtEngineerStatistics.Text & Statement

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub llbExportStatsToWord_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbExportStatsToWord.LinkClicked
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        OpenHelpUrl(Me)
    End Sub
End Class