Imports System.DateTime
Imports System.Data.OracleClient
Imports System.Security.Cryptography
Imports System.Text
Imports System
Imports System.Security
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.Odbc

Module subMain

#Region "DB Connection Strings"
    Friend Const DBNameSpace As String = "AIRBRANCH"

    Friend PrdConnString As String = "Data Source = PRD; User ID = AIRBRANCH_APP_USER; Password = " & SimpleCrypt("ÁÚ·Ú±Ï") & ";"
    'Public TestConnString As String = "Data Source = TEST; User ID = AIRBRANCH_APP_USER; Password = " & SimpleCrypt("¡…“¡––’”≈“∞≥") & ";"
    Friend DevConnString As String = "Data Source = DEV; User ID = AIRBRANCH; Password = " & SimpleCrypt("ÛÌÔÁ·ÏÂÚÙ") & ";"
    Friend CurrentConnString As String = PrdConnString

    Public TVProjectConnString As String = "Data Source = leia.dnr.state.ga.us:1521/DEV; User ID = airtvproject; Password = airproject;"
    Public TVApplicationConnString As String = "Data Source = leia.dnr.state.ga.us:1521/DEV; User ID = airtvapplication; Password = airapp;"

    Public Conn As New OracleConnection(PrdConnString)
    Public ConnTVProject As New OracleConnection(TVProjectConnString)
    Public ConnTVApplication As New OracleConnection(TVApplicationConnString)

    Public PRDCRLogIn As String = "AirBranch_App_User"
    Public PRDCRPassWord As String = SimpleCrypt("ÁÚ·Ú±Ï")

    'Public TESTCRLogIn As String = "AirBranch_App_User"
    'Public TESTCRPassWord As String = SimpleCrypt("¡…“¡––’”≈“∞≥")

    Public DEVCRLogIn As String = "AirBranch"
    Public DEVCRPassWord As String = SimpleCrypt("ÛÌÔÁ·ÏÂÚÙ")

    Public CRLogIn As String = PRDCRLogIn
    Public CRPassWord As String = PRDCRPassWord
#End Region

#Region "New public variables"
    Friend HELP_URL As String = "https://sites.google.com/site/iaipdocs/"
    Friend DateFormat As String = "dd-MMM-yyyy"
    Friend Today As Date = DateTime.Today
    Friend TodayString As String = Format(Today, DateFormat)
#End Region

#Region "Old public variables"
    Public OracleDate As String = Format(Date.Today, "dd-MMM-yyyy")
    Public UserGCode As String
    Public Permissions As String
    Public UserName As String
    Public UserBranch As String
    Public UserProgram As String
    Public UserUnit As String

    Public SQL, SQL2, SQL3 As String
    Public cmd, cmd2, cmd3 As OracleCommand
    Public dr, dr2, dr3 As OracleDataReader
    Public recExist As Boolean
    Public temp As String
    Public AccountArray(150, 4) As String
    Public j As Integer
    Public i As Integer
    Public DefaultX As Integer
    Public DefaultY As Integer
    Public Oracledll As String

    Public t As New System.Timers.Timer(14400000)
    Public t2 As New System.Timers.Timer(300000)
#End Region

#Region "All Forms"
#Region "Universal Screens"
    Public APB110 As IAIPLogIn

    Public NavigationScreen As IAIPNavigation
    Public FacilityLookUpTool As IAIPFacilityLookUpTool
    Public PrintOut As IAIPPrintOut
    Public FacilitySummary As IAIPFacilitySummary

    Public DevSQLQuery As IAIPQueryGenerator
    Public EditContacts As IAIPEditContacts
    Public EditContacts2 As DEVEditContacts
    Public EditFacilityLocation As IAIPEditFacilityLocation
    Public EditHeaderData As IAIPEditHeaderData
    Public EditAirProgramPollutants As IAIPEditAirProgramPollutants
    Public EditSubParts As IAIPEditSubParts
    Public DevelopmentTeam As IAIPDevelopmentTeam
    Public IAIPDistrictTool As IAIPDistrictSourceTool
    Public PermitUploader As IAIPPermitUploader
    Public Validator As AFSValidator
    Public UserAdminTool As IAIPUserAdminTool
    Public ListTool As IAIPListTool
    Public PhoneList As IAIPPhoneList
    Public ProfileUpdate As IAIPProfileUpdate
    Public AFSCompare As IAIPAFSCompare
    Public FacilityPrintOut As IaipFacilitySummaryPrint
    Public LookUpTables As IAIPLookUpTables
    Public FacilityCreator As IAIPFacilityCreator
    Public EISLog As IAIP_EIS_Log
    Public TitleVProject As DMU_TITLEV_PROJECT

#End Region
#Region "Ambient Monitoring Screens"

#End Region
#Region "ISMP Screens"
    Public ISMPFacility As ISMPTestReportAdministrative
    Public ISMPTestReportInfo As ISMPFacilityInfo
    Public ISMPManagers As ISMPManagersTools
    Public ISMPReportViewer As ISMPMonitoringLog
    Public ISMPMemoViewer As ISMPTestMemoViewer
    Public ISMPRefNum As ISMPReferenceNumber
    Public ISMPMemoEdit As ISMPMemo
    Public DevTestLog As ISMPNotificationLog
    Public ISMPCloseAndPrint As ISMPClosePrint
    Public ISMPExcelFilePage As ISMPExcelFiles
    Public ISMPAddPollutant As ISMPAddPollutants
    Public ISMPAddTestingFirm As ISMPAddTestingFirms
    Public ISMPDMU As ISMPDataManagementTools
    Public DMUOnly As DMUTool
    Public ISMPConfidential As ISMPConfidentialData
    Public ISMPTestReportsEntry As ISMPTestReports
    Public TestFirmComments As ISMPTestFirmComments
    Public SmokeSchool As ISMPSmokeSchool
    Public DevelopersTools As DMUDeveloperTools
    Public StaffTools As DMUStaffTools
    Public TitleVTools As DMUTitleVTools
    Public StaffReports As ISMPStaffReports
    Public FeeAuditTool As IAIPFeeAuditTool

#End Region
#Region "Mobile & Area Screens"
    Public RegistrationTool As MASPRegistrationTool

#End Region
#Region "Planning & Support Screens"
    Public FacilityFeeForm As PASPFacilityFee
    Public FeeDeposits As PASPFeeDeposits
    Public Modifications As PASPModifications
    Public FeesReports As PASPFeeReports
    Public VarianceTool As PASPFeeVarianceCheck
    Public WebAppUser As PASPWebApplicationUser
    Public DepositsAmendments As PASPDepositsAmendments
    ' Public MailoutAndStats As DEVMailoutAndStats
    Public MailoutAndStats As DEVFeeStatistics
    Public FeeStat As DEVMailoutAndStats

    Public FeeTools As PASPFeeTools
    Public ComputerInventory As PASPInventory
    Public FeeStats As PASPFeeAuditLog
    Public FeesLog As PASPFeesLog
    Public FeeManagement As PASPFeeManagement

#End Region
#Region "SSCP Screens"
    Public SSCP_Work As SSCPComplianceLog
    Public ManagersTools As SSCPManagersTools
    Public SSCPREports As SSCPEvents
    'Public SSCP_Enforcement As SSCPEnforcement
    Public SSCP_Enforcement As SSCPEnforcementAudit
    Public SSCPFCE As SSCPFCEWork
    Public SSCPFacAssign As SSCPFacAssignment
    Public SSCPEngWork As SSCPWorkEnTry
    Public SSCPTemplates As SSCPLetterTemplates
    Public SSCPRequest As SSCPInformationRequest
    Public SSCPFCESelector As SSCPFCESelectorTool
    Public SSCPSelectEnforcement As SSCPEnforcementSelector
    Public EnforcementChecklist As SSCPEnforcementChecklist
    Public SSCPInspectionsTool As SSCPEngineerInspectionTool
    Public SSCPInspectionscheduleTool As SSCPInspectionscheduleLink
    Public EmissionSummary As SSCPEmissionSummaryTool
    Public ManagerProfile As SSCPManagerProfile
    Public SSCPAdmin As SSCPAdministrator
    Public InspectionTool As SSCPInspectionTool

#End Region
#Region "SSPP Screens"
    Public ApplicationLog As SSPPApplicationLog
    Public PermitTrackingLog As SSPPApplicationTrackingLog
    Public AttainmentStatus As SSPPAttainmentStatus
    Public PublicLetter2 As SSPPPublicNoticiesAndAdvisories
    Public StatisticalTools As SSPPStatisticalTools
    Public FeeContact As SSPP_FeeContact

#End Region
#End Region

#Region "Public Procedures"

    Public Sub ErrorReport(ByVal ErrorMessage As String, ByVal ErrorLocation As String)
        Dim SQL As String
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader
        Dim ErrorMess As String

        If UserGCode = "" Then
            UserGCode = "0"
        End If

        If ErrorMessage.Contains("System.UnauthorizedAccessException: Access to the path 'C:\APB\Defaults.txt' is denied.") Then
            If File.Exists("C:\APB\Defaults.txt") Then
            Else
                Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.Create, IO.FileAccess.Write)
                fs.Close()
            End If
            MsgBox("The defualt log in file was corrupted and deleted." & vbCrLf & _
                   "Restart the platform." & vbCrLf & vbCrLf & _
                   "If this error persists close the platform and delete the file C:\APB\Defaults.txt", MsgBoxStyle.Information, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-12592") Then
            MsgBox("There was a connectivity error with the database." & vbCrLf & "Please run the task that caused this error again to verify the data is correct." & vbCrLf & "If the error presists, try waiting until the internet connection improves or contact the Data Management Unit." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenance.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If

        If ErrorMessage.Contains("ORA-12545") Or ErrorMessage.Contains("ORA-604") Or ErrorMessage.Contains("ORA-257") Then
            MsgBox("There is no connection to the database at this time." & vbCrLf & "Verify that you currently have an internet connection." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenance.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-03114") Then
            MsgBox("There is no connection to the database at this time." & vbCrLf & "Verify that you currently have an internet connection." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenance.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-1033") Or ErrorMessage.Contains("ORA-01033") Then
            MsgBox("The database is currently undergoing a restart procedure." & vbCrLf & "Please wait 15 minutes and try again." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenance.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-12154") Then
            MsgBox("This PC has a connection error." & vbCrLf & "Please contact the Data Management Unit for assistance." & vbCrLf & _
             "Sorry for the inconvenance.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("Could not load file or assembly 'CrystalDecisions.") Or _
                      ErrorMessage.Contains("Integrated Air Information Platf") Then
            MsgBox("This machine needs to run the Crystal Report Patch." & vbCrLf & _
                   "The patch is available at http://airpermit.dnr.state.ga.us/iaip/crpatch08.zip" & vbCrLf & _
                   "If you cannot run the patch please contact the Data Management Unit for assistance" & vbCrLf & _
                   "The Address has been saved to the clipboard. Ctrl-V will paste it where you want.", _
                    MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")

            Clipboard.SetDataObject("http://airpermit.dnr.state.ga.us/iaip/crpatch08.zip", True)
            Exit Sub
        End If
        If ErrorMessage.Contains("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently") Then
            MsgBox("The platform is running multiple processing threads and needs time to complete them." & vbCrLf & _
                   "Please allow time for the process to run.", MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-03113") Or ErrorMessage.Contains("ORA-12637") Then
            MsgBox("The platform experianced a connection error." & vbCrLf & "Try reloading the form" & vbCrLf & _
                   "If the problem persists please contact the Data Management Unit.", MsgBoxStyle.Information, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("ORA-12571") Or ErrorMessage.Contains("ORA-01033") Or ErrorMessage.Contains("ORA-12545") Then
            MsgBox("The platform experianced a connection error." & vbCrLf & "Try reloading the form" & vbCrLf & _
                   "If the problem persists please contact the Data Management Unit.", MsgBoxStyle.Information, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("System.ComponentModel.Win32Exception: The system cannot find the file specified") Then
            MsgBox("The platform is attempting to contact an external server." & vbCrLf & _
                   "The server maybe unavailable at thit time." & vbCrLf & "Please try again.", MsgBoxStyle.Exclamation, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("System.InvalidOperationException: Invalid operation. The connection is closed") Then
            MsgBox("The connection to the database was temporarily lost." & vbCrLf & "Please try to reload the page." & vbCrLf & _
                   "If the problem persists contact the Data Management Unit.", MsgBoxStyle.Exclamation, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("Access to the path 'C:\APB\Defaults.txt' is denied") And ErrorLocation.Contains("NavigationScreen_Closed") Then
            Exit Sub
        End If
        If ErrorMessage.Contains("Exception of type 'System.OutOfMemoryException' was thrown") Then
            MsgBox("It appears that this computer has thrown a System.OutOfMemoryException. " & vbCrLf & _
                   "Try freeing up memory by closing any un-needed open computer applications.", MsgBoxStyle.Exclamation, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        If ErrorMessage.Contains("Attempted to read or write protected memory") Then
            MsgBox("There was a unique error." & vbCrLf & _
                   "Try to perform your action again. If the error returns close out the IAIP application and try again." & vbCrLf & _
                   "If this error persists contact the Data Management Unit", MsgBoxStyle.Information, _
                   "Integrated Air Information Platform - ERROR MESSAGE")
            Exit Sub
        End If
        Try
            ErrorMessage = FileVersionInfo.GetVersionInfo("C:\APB\johngaltproject.exe").ProductVersion.ToString & vbCrLf & ErrorMessage
            ErrorMess = Mid(ErrorMessage, 1, 4000)

            SQL = "Insert into " & DBNameSpace & ".IAIPErrorLog " & _
            "(strErrorNumber, strUser, " & _
            "strErrorLocation, strErrorMessage, " & _
            "datErrorDate) " & _
            "values " & _
            "(" & DBNameSpace & ".IAIPErrornumber.nextval, '" & UserGCode & "', " & _
            "'" & Replace(ErrorLocation, "'", "''") & "', '" & Replace(ErrorMess, "'", "''") & "', " & _
            "sysdate) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Read()
            dr.Close()


            MsgBox("An Error has occurred." & vbCrLf & "The error has been logged and sent to the developers." & vbCrLf & _
            "Please contact the Data Management Unit if this error is hindering your work." & vbCrLf & "Sorry for the inconvenience.", _
            MsgBoxStyle.Information, "Integrated Air Information Platform - ERROR MESSAGE")
        Catch ex As Exception
            MsgBox("There was an error in logging this problem." & vbCrLf & "Please contact the Data Management Unit with this problem." & vbCrLf & _
                   ErrorMessage, MsgBoxStyle.Exclamation, "Integrated Air Information Platform - ERROR MESSAGE")
        End Try

    End Sub
    Public Sub TimerFired(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Dim Result As DialogResult

        AddHandler t2.Elapsed, AddressOf TimerFired2
        t2.Enabled = True

        Result = MessageBox.Show("The Integrated Air Information Platform has been open for 4 hours." & vbCrLf & _
        "Do you want to continue to have it open?" & vbCrLf & vbCrLf & "IAIP will be terminated in 5 minutes.", "IAIP Connection Time Out.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

        Select Case Result
            Case DialogResult.Yes
                t2.Enabled = False
            Case DialogResult.No
                End
            Case Else
                t2.Enabled = False
        End Select
    End Sub
    Public Sub TimerFired2(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        End
    End Sub

#End Region

#Region "Encryption Script"

    Public Function SimpleCrypt(ByVal Text As String) As String
        ' Encrypts/decrypts the passed string using
        ' a simple ASCII value-swapping algorithm
        ' DWW comment: You've got to be kidding me. ROT128? We're using an encryption scheme used by Julius Caesar?
        Dim strTempChar As String = ""
        Dim i As Integer

        For i = 1 To Len(Text)
            If Asc(Mid$(Text, i, 1)) < 128 Then
                strTempChar = _
          CType(Asc(Mid$(Text, i, 1)) + 128, String)
            ElseIf Asc(Mid$(Text, i, 1)) > 128 Then
                strTempChar = _
          CType(Asc(Mid$(Text, i, 1)) - 128, String)
            End If
            Mid$(Text, i, 1) = _
                Chr(CType(strTempChar, Integer))
        Next i
        Return Text
    End Function
    Public Class EncryptDecrypt
        ' Encrypt the text
        Public Shared Function EncryptText(ByVal strText As String) As String
            Return Encrypt(strText, "&%#@?,:*")
        End Function
        'Decrypt the text 
        Public Shared Function DecryptText(ByVal strText As String) As String
            Return Decrypt(strText, "&%#@?,:*")
        End Function
        'The function used to encrypt the text
        Private Shared Function Encrypt(ByVal strText As String, ByVal strEncrKey _
                 As String) As String
            Dim byKey() As Byte = {}
            Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

            Try
                byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))

                Dim des As New DESCryptoServiceProvider()
                Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
                Dim ms As New MemoryStream()
                Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Return Convert.ToBase64String(ms.ToArray())

            Catch ex As Exception
                Return ex.Message
            End Try

        End Function
        'The function used to decrypt the text
        Private Shared Function Decrypt(ByVal strText As String, ByVal sDecrKey _
                   As String) As String
            Dim byKey() As Byte = {}
            Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
            Dim inputByteArray(strText.Length) As Byte

            Try
                byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8))
                Dim des As New DESCryptoServiceProvider()
                inputByteArray = Convert.FromBase64String(strText)
                Dim ms As New MemoryStream()
                Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)

                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8

                Return encoding.GetString(ms.ToArray())

            Catch ex As Exception
                Return ex.Message
            End Try

        End Function
    End Class

#End Region

    Public Declare Function SendMessage Lib "user32" Alias _
       "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As _
       Integer, ByVal wParam As Integer, ByRef lParam As _
       Integer) As Integer
    Public Const EM_SETTABSTOPS As Integer = &HCB


End Module