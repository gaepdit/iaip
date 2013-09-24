Imports System.IO
Imports System
Imports Oracle.DataAccess.Client


Public Class AFSValidator
    Dim dsValidator As DataSet
    Dim SQL As String
    Dim cmd As oraclecommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean

    Private Sub AFSValidator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        monitor.TrackFeature("Dev." & Me.Name)
    End Sub
    Private Sub btnGet654_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGet654.Click
        Try
            download()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Sub download()
        Dim FileName As String = ""
        Dim path As New OpenFileDialog
        Dim temp As String = ""
        Dim CompleteFile As String = ""
        Dim WorkTemp As String = ""
        Try
            path.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            path.FileName = FileName
            path.Filter = "Text Documents (*.txt)|*.txt|All files (*.*)|*.*"
            path.FilterIndex = 1

            If path.ShowDialog = Windows.Forms.DialogResult.OK Then
                temp = path.FileName.ToString
            Else
                temp = "N/A"
            End If

            If temp <> "" Then
                Dim reader As StreamReader = New StreamReader(temp)
                Do
                    CompleteFile = CompleteFile & reader.ReadLine
                Loop Until reader.Peek = -1
                reader.Close()
            End If
            temp = ""

            txtCompleteFile.Text = CompleteFile.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadFacilityName()
        Dim FacilityName As DataTable
        Dim Address102 As DataTable
        Dim Address103 As DataTable
        Dim Contact105 As DataTable
        Dim CMS181 As DataTable
        Dim AirProgram As DataTable
        Dim Pollutants As DataTable
        Dim SubPart As DataTable
        Dim FCEAction As DataTable
        Dim StackTest As DataTable
        Dim Reports As DataTable
        Dim Inspections As DataTable
        Dim ACCReviewed As DataTable
        Dim ACCDue As DataTable

        Dim AFS101AIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFS101 As DataColumn = New DataColumn("AFS - Action")
        Dim AFSFacility As DataColumn = New DataColumn("AFS - Name")
        Dim IAIPAIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIPAction As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPFacility As DataColumn = New DataColumn("IAIP - Name")
        Dim AFS102AIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFS102 As DataColumn = New DataColumn("AFS - Action")
        Dim AFSAddress As DataColumn = New DataColumn("AFS - Address")
        Dim IAIP102AIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIP102 As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPAddress As DataColumn = New DataColumn("IAIP - Address")
        Dim AFS103AIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFS103 As DataColumn = New DataColumn("AFS - Action")
        Dim AFSCity As DataColumn = New DataColumn("AFS - City")
        Dim AFSZipCode As DataColumn = New DataColumn("AFS - Zip Code")
        Dim AFSSIC As DataColumn = New DataColumn("AFS - SIC")
        Dim IAIP103AIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIP103 As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPCity As DataColumn = New DataColumn("IAIP - City")
        Dim IAIPZipCode As DataColumn = New DataColumn("IAIP - Zip Code")
        Dim IAIPSIC As DataColumn = New DataColumn("IAIP - SIC")
        Dim AFS105AIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFS105 As DataColumn = New DataColumn("AFS - Action")
        Dim AFSContactName As DataColumn = New DataColumn("AFS - Contact Name")
        Dim AFSContactPhone As DataColumn = New DataColumn("AFS - Contact Phone")
        Dim AFSPlantDesc As DataColumn = New DataColumn("AFS - Plant Desc.")
        Dim IAIP105AIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIP105 As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPContactName As DataColumn = New DataColumn("IAIP - Contact Name")
        Dim IAIPContactPhone As DataColumn = New DataColumn("IAIP - Contact Phone")
        Dim IAIPPlantDesc As DataColumn = New DataColumn("IAIP - Plant Desc.")
        Dim AFS181AIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFS181 As DataColumn = New DataColumn("AFS - Action")
        Dim AFSCMS As DataColumn = New DataColumn("AFS - CMS")
        Dim AFSCMSYears As DataColumn = New DataColumn("AFS - CMS Years")
        Dim IAIP181AIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIP181 As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPCMS As DataColumn = New DataColumn("IAIP - CMS")
        Dim IAIPCMSYears As DataColumn = New DataColumn("IAIP - CMS Years")
        Dim AFS121AIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFS121 As DataColumn = New DataColumn("AFS - Action")
        Dim AFSAirCode As DataColumn = New DataColumn("AFS - Air Code")
        Dim AFSAirCodeOp As DataColumn = New DataColumn("AFS - Op Status")
        Dim IAIP121AIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIP121 As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPAirCode As DataColumn = New DataColumn("IAIP - Air Code")
        Dim IAIPAirCodeOp As DataColumn = New DataColumn("IAIP - Op Status")
        Dim AFS131AIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFS131 As DataColumn = New DataColumn("AFS - Action")
        Dim AFS131AirCode As DataColumn = New DataColumn("AFS - Air Code")
        Dim AFSPollutant As DataColumn = New DataColumn("AFS - Pollutant")
        Dim AFS131Compliance As DataColumn = New DataColumn("AFS - Compliance Status")
        Dim AFS131Classification As DataColumn = New DataColumn("AFS - Classification")
        Dim AFS131Attainment As DataColumn = New DataColumn("AFS - Attainment")
        Dim IAIP131AIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIP131 As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIP131AirCode As DataColumn = New DataColumn("IAIP - Air Code")
        Dim IAIPPollutant As DataColumn = New DataColumn("IAIP - Pollutant")
        Dim IAIP131Compliance As DataColumn = New DataColumn("IAIP - Compliance Status")
        Dim IAIP131Classification As DataColumn = New DataColumn("IAIP - Classification")
        Dim IAIP131Attainment As DataColumn = New DataColumn("IAIP - Attainment")
        Dim AFS122AIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFS122 As DataColumn = New DataColumn("AFS - Action")
        Dim AFS122AirCode As DataColumn = New DataColumn("AFS - Air Code")
        Dim AFS122Indicator As DataColumn = New DataColumn("AFS - Indicator")
        Dim AFSSubPart As DataColumn = New DataColumn("AFS - SubPart")
        Dim IAIP122AIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIP122 As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIP122AirCode As DataColumn = New DataColumn("IAIP - Air Code")
        Dim IAIP122Indicator As DataColumn = New DataColumn("IAIP - Indicator")
        Dim IAIPSubPart As DataColumn = New DataColumn("IAIP - SubPart")
        Dim AFSFCEAIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFSFCE As DataColumn = New DataColumn("AFS - Action")
        Dim AFSFCEActionNum As DataColumn = New DataColumn("AFS - Action Num")
        Dim AFSFCEAirCode As DataColumn = New DataColumn("AFS - Air Code")
        Dim AFSFCEActionType As DataColumn = New DataColumn("AFS - Action Type")
        Dim AFSFCEDateAchieved As DataColumn = New DataColumn("AFS - Date Achieved")
        Dim AFSFCEActionResult As DataColumn = New DataColumn("AFS - Action Result")
        Dim IAIPFCEAIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIPFCE As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPFCEActionNum As DataColumn = New DataColumn("IAIP - Action Num")
        Dim IAIPFCEAirCode As DataColumn = New DataColumn("IAIP - Air Code")
        Dim IAIPFCEActionType As DataColumn = New DataColumn("IAIP - Action Type")
        Dim IAIPFCEDateAchieved As DataColumn = New DataColumn("IAIP - Date Achieved")
        Dim AFSStackAIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFSStack As DataColumn = New DataColumn("AFS - Action")
        Dim AFSStackActionNum As DataColumn = New DataColumn("AFS - Action Num")
        Dim AFSStackAirCode As DataColumn = New DataColumn("AFS - Air Code")
        Dim AFSStackActionType As DataColumn = New DataColumn("AFS - Action Type")
        Dim AFSStackDateAchieved As DataColumn = New DataColumn("AFS - Date Achieved")
        Dim AFSStackResult As DataColumn = New DataColumn("AFS - Stack Result")
        Dim IAIPStackAIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIPStack As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPStackActionNum As DataColumn = New DataColumn("IAIP - Action Num")
        Dim IAIPStackAirCode As DataColumn = New DataColumn("IAIP - Air Code")
        Dim IAIPStackActionType As DataColumn = New DataColumn("IAIP - Action Type")
        Dim IAIPStackDateAchieved As DataColumn = New DataColumn("IAIP - Date Achieved")
        Dim IAIPStackResult As DataColumn = New DataColumn("IAIP - Stack Result")
        Dim AFSReportAIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFSReport As DataColumn = New DataColumn("AFS - Action")
        Dim AFSReportActionNum As DataColumn = New DataColumn("AFS - Action Num")
        Dim AFSReportAirCode As DataColumn = New DataColumn("AFS - Air Code")
        Dim AFSReportActionType As DataColumn = New DataColumn("AFS - Action Type")
        Dim AFSReportDateAchieved As DataColumn = New DataColumn("AFS - Date Achieved")
        Dim IAIPReportAIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIPReport As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPReportActionNum As DataColumn = New DataColumn("IAIP - Action Num")
        Dim IAIPReportAirCode As DataColumn = New DataColumn("IAIP - Air Code")
        Dim IAIPReportActionType As DataColumn = New DataColumn("IAIP - Action Type")
        Dim IAIPReportDateAchieved As DataColumn = New DataColumn("IAIP - Date Achieved")

        Dim AFSInspectionAIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFSInspection As DataColumn = New DataColumn("AFS - Action")
        Dim AFSInspectionActionNum As DataColumn = New DataColumn("AFS - Action Num")
        Dim AFSInspectionAirCode As DataColumn = New DataColumn("AFS - Air Code")
        Dim AFSInspectionActionType As DataColumn = New DataColumn("AFS - Action Type")
        Dim AFSInspectionDateAchieved As DataColumn = New DataColumn("AFS - Date Achieved")
        Dim IAIPInspectionAIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIPInspection As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPInspectionActionNum As DataColumn = New DataColumn("IAIP - Action Num")
        Dim IAIPInspectionAirCode As DataColumn = New DataColumn("IAIP - Air Code")
        Dim IAIPInspectionActionType As DataColumn = New DataColumn("IAIP - Action Type")
        Dim IAIPInspectionDateAchieved As DataColumn = New DataColumn("IAIP - Date Achieved")
        Dim AFSACCReviewedAIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFSACCReviewed As DataColumn = New DataColumn("AFS - Action")
        Dim AFSACCReviewedActionNum As DataColumn = New DataColumn("AFS - Action Num")
        Dim AFSACCReviewedAirCode As DataColumn = New DataColumn("AFS - Air Code")
        Dim AFSACCReviewedActionType As DataColumn = New DataColumn("AFS - Action Type")
        Dim AFSACCReviewedDateAchieved As DataColumn = New DataColumn("AFS - Date Achieved")
        Dim IAIPACCReviewedAIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIPACCReviewed As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPACCReviewedActionNum As DataColumn = New DataColumn("IAIP - Action Num")
        Dim IAIPACCReviewedAirCode As DataColumn = New DataColumn("IAIP - Air Code")
        Dim IAIPACCReviewedActionType As DataColumn = New DataColumn("IAIP - Action Type")
        Dim IAIPACCReviewedDateAchieved As DataColumn = New DataColumn("IAIP - Date Achieved")
        Dim AFSACCDueAIRS As DataColumn = New DataColumn("AFS - AIRS")
        Dim AFSACCDue As DataColumn = New DataColumn("AFS - Action")
        Dim AFSACCDueActionNum As DataColumn = New DataColumn("AFS - Action Num")
        Dim AFSACCDueAirCode As DataColumn = New DataColumn("AFS - Air Code")
        Dim AFSACCDueActionType As DataColumn = New DataColumn("AFS - Action Type")
        Dim AFSACCDueDateAchieved As DataColumn = New DataColumn("AFS - Date Achieved")
        Dim IAIPACCDueAIRS As DataColumn = New DataColumn("IAIP - AIRS")
        Dim IAIPACCDue As DataColumn = New DataColumn("IAIP - Action")
        Dim IAIPACCDueActionNum As DataColumn = New DataColumn("IAIP - Action Num")
        Dim IAIPACCDueAirCode As DataColumn = New DataColumn("IAIP - Air Code")
        Dim IAIPACCDueActionType As DataColumn = New DataColumn("IAIP - Action Type")
        Dim IAIPACCDueDateAchieved As DataColumn = New DataColumn("IAIP - Date Achieved")



        Dim row As DataRow
        Dim worktemp As String = ""
        Dim temp As String = ""
        Dim SubPartLoop As String = ""
        Dim tempSubPart As String = ""
        Dim tempDate As String = ""
        Dim AirCodes As String = ""

        Try
            FacilityName = New DataTable("FacilityName")
            AFS101AIRS.DataType = System.Type.GetType("System.String")
            AFS101.DataType = System.Type.GetType("System.String")
            AFSFacility.DataType = System.Type.GetType("System.String")
            IAIPAIRS.DataType = System.Type.GetType("System.String")
            IAIPAction.DataType = System.Type.GetType("System.String")
            IAIPFacility.DataType = System.Type.GetType("System.String")
            FacilityName.Columns.Add(AFS101AIRS)
            FacilityName.Columns.Add(AFS101)
            FacilityName.Columns.Add(AFSFacility)
            FacilityName.Columns.Add(IAIPAIRS)
            FacilityName.Columns.Add(IAIPAction)
            FacilityName.Columns.Add(IAIPFacility)

            Address102 = New DataTable("Address102")
            AFS102AIRS.DataType = System.Type.GetType("System.String")
            AFS102.DataType = System.Type.GetType("System.String")
            AFSAddress.DataType = System.Type.GetType("System.String")
            IAIP102AIRS.DataType = System.Type.GetType("System.String")
            IAIP102.DataType = System.Type.GetType("System.String")
            IAIPAddress.DataType = System.Type.GetType("System.String")
            Address102.Columns.Add(AFS102AIRS)
            Address102.Columns.Add(AFS102)
            Address102.Columns.Add(AFSAddress)
            Address102.Columns.Add(IAIP102AIRS)
            Address102.Columns.Add(IAIP102)
            Address102.Columns.Add(IAIPAddress)

            Address103 = New DataTable("Address103")
            AFS103AIRS.DataType = System.Type.GetType("System.String")
            AFS103.DataType = System.Type.GetType("System.String")
            AFSCity.DataType = System.Type.GetType("System.String")
            AFSZipCode.DataType = System.Type.GetType("System.String")
            AFSSIC.DataType = System.Type.GetType("System.String")
            IAIP103AIRS.DataType = System.Type.GetType("System.String")
            IAIP103.DataType = System.Type.GetType("System.String")
            IAIPCity.DataType = System.Type.GetType("System.String")
            IAIPZipCode.DataType = System.Type.GetType("System.String")
            IAIPSIC.DataType = System.Type.GetType("System.String")
            Address103.Columns.Add(AFS103AIRS)
            Address103.Columns.Add(AFS103)
            Address103.Columns.Add(AFSCity)
            Address103.Columns.Add(AFSZipCode)
            Address103.Columns.Add(AFSSIC)
            Address103.Columns.Add(IAIP103AIRS)
            Address103.Columns.Add(IAIP103)
            Address103.Columns.Add(IAIPCity)
            Address103.Columns.Add(IAIPZipCode)
            Address103.Columns.Add(IAIPSIC)

            Contact105 = New DataTable("Contact105")
            AFS105AIRS.DataType = System.Type.GetType("System.String")
            AFS105.DataType = System.Type.GetType("System.String")
            AFSContactName.DataType = System.Type.GetType("System.String")
            AFSContactPhone.DataType = System.Type.GetType("System.String")
            AFSPlantDesc.DataType = System.Type.GetType("System.String")
            IAIP105AIRS.DataType = System.Type.GetType("System.String")
            IAIP105.DataType = System.Type.GetType("System.String")
            IAIPContactName.DataType = System.Type.GetType("System.String")
            IAIPContactPhone.DataType = System.Type.GetType("System.String")
            IAIPPlantDesc.DataType = System.Type.GetType("System.String")
            Contact105.Columns.Add(AFS105AIRS)
            Contact105.Columns.Add(AFS105)
            Contact105.Columns.Add(AFSContactName)
            Contact105.Columns.Add(AFSContactPhone)
            Contact105.Columns.Add(AFSPlantDesc)
            Contact105.Columns.Add(IAIP105AIRS)
            Contact105.Columns.Add(IAIP105)
            Contact105.Columns.Add(IAIPContactName)
            Contact105.Columns.Add(IAIPContactPhone)
            Contact105.Columns.Add(IAIPPlantDesc)

            CMS181 = New DataTable("CMS181")
            AFS181AIRS.DataType = System.Type.GetType("System.String")
            AFS181.DataType = System.Type.GetType("System.String")
            AFSCMS.DataType = System.Type.GetType("System.String")
            AFSCMSYears.DataType = System.Type.GetType("System.String")
            IAIP181AIRS.DataType = System.Type.GetType("System.String")
            IAIP181.DataType = System.Type.GetType("System.String")
            IAIPCMS.DataType = System.Type.GetType("System.String")
            IAIPCMSYears.DataType = System.Type.GetType("System.String")
            CMS181.Columns.Add(AFS181AIRS)
            CMS181.Columns.Add(AFS181)
            CMS181.Columns.Add(AFSCMS)
            CMS181.Columns.Add(AFSCMSYears)
            CMS181.Columns.Add(IAIP181AIRS)
            CMS181.Columns.Add(IAIP181)
            CMS181.Columns.Add(IAIPCMS)
            CMS181.Columns.Add(IAIPCMSYears)

            AirProgram = New DataTable("AirProgram")
            AFS121AIRS.DataType = System.Type.GetType("System.String")
            AFS121.DataType = System.Type.GetType("System.String")
            AFSAirCode.DataType = System.Type.GetType("System.String")
            AFSAirCodeOp.DataType = System.Type.GetType("System.String")
            IAIP121AIRS.DataType = System.Type.GetType("System.String")
            IAIP121.DataType = System.Type.GetType("System.String")
            IAIPAirCode.DataType = System.Type.GetType("System.String")
            IAIPAirCodeOp.DataType = System.Type.GetType("System.String")
            AirProgram.Columns.Add(AFS121AIRS)
            AirProgram.Columns.Add(AFS121)
            AirProgram.Columns.Add(AFSAirCode)
            AirProgram.Columns.Add(AFSAirCodeOp)
            AirProgram.Columns.Add(IAIP121AIRS)
            AirProgram.Columns.Add(IAIP121)
            AirProgram.Columns.Add(IAIPAirCode)
            AirProgram.Columns.Add(IAIPAirCodeOp)

            Pollutants = New DataTable("Pollutants")
            AFS131AIRS.DataType = System.Type.GetType("System.String")
            AFS131.DataType = System.Type.GetType("System.String")
            AFS131AirCode.DataType = System.Type.GetType("System.String")
            AFSPollutant.DataType = System.Type.GetType("System.String")
            AFS131Compliance.DataType = System.Type.GetType("System.String")
            AFS131Classification.DataType = System.Type.GetType("System.String")
            AFS131Attainment.DataType = System.Type.GetType("System.String")
            IAIP131AIRS.DataType = System.Type.GetType("System.String")
            IAIP131.DataType = System.Type.GetType("System.String")
            IAIP131AirCode.DataType = System.Type.GetType("System.String")
            IAIPPollutant.DataType = System.Type.GetType("System.String")
            IAIP131Compliance.DataType = System.Type.GetType("System.String")
            IAIP131Classification.DataType = System.Type.GetType("System.String")
            IAIP131Attainment.DataType = System.Type.GetType("System.String")
            Pollutants.Columns.Add(AFS131AIRS)
            Pollutants.Columns.Add(AFS131)
            Pollutants.Columns.Add(AFS131AirCode)
            Pollutants.Columns.Add(AFSPollutant)
            Pollutants.Columns.Add(AFS131Compliance)
            Pollutants.Columns.Add(AFS131Classification)
            Pollutants.Columns.Add(AFS131Attainment)
            Pollutants.Columns.Add(IAIP131AIRS)
            Pollutants.Columns.Add(IAIP131)
            Pollutants.Columns.Add(IAIP131AirCode)
            Pollutants.Columns.Add(IAIPPollutant)
            Pollutants.Columns.Add(IAIP131Compliance)
            Pollutants.Columns.Add(IAIP131Classification)
            Pollutants.Columns.Add(IAIP131Attainment)

            SubPart = New DataTable("SubPart")
            AFS122AIRS.DataType = System.Type.GetType("System.String")
            AFS122.DataType = System.Type.GetType("System.String")
            AFS122AirCode.DataType = System.Type.GetType("System.String")
            AFS122Indicator.DataType = System.Type.GetType("System.String")
            AFSSubPart.DataType = System.Type.GetType("System.String")
            IAIP122AIRS.DataType = System.Type.GetType("System.String")
            IAIP122.DataType = System.Type.GetType("System.String")
            IAIP122AirCode.DataType = System.Type.GetType("System.String")
            IAIP122Indicator.DataType = System.Type.GetType("System.String")
            IAIPSubPart.DataType = System.Type.GetType("System.String")
            SubPart.Columns.Add(AFS122AIRS)
            SubPart.Columns.Add(AFS122)
            SubPart.Columns.Add(AFS122AirCode)
            SubPart.Columns.Add(AFS122Indicator)
            SubPart.Columns.Add(AFSSubPart)
            SubPart.Columns.Add(IAIP122AIRS)
            SubPart.Columns.Add(IAIP122)
            SubPart.Columns.Add(IAIP122AirCode)
            SubPart.Columns.Add(IAIP122Indicator)
            SubPart.Columns.Add(IAIPSubPart)

            FCEAction = New DataTable("FCEAction")
            AFSFCEAIRS.DataType = System.Type.GetType("System.String")
            AFSFCE.DataType = System.Type.GetType("System.String")
            AFSFCEActionNum.DataType = System.Type.GetType("System.String")
            AFSFCEAirCode.DataType = System.Type.GetType("System.String")
            AFSFCEActionType.DataType = System.Type.GetType("System.String")
            AFSFCEDateAchieved.DataType = System.Type.GetType("System.String")
            AFSFCEActionResult.DataType = System.Type.GetType("System.String")
            IAIPFCEAIRS.DataType = System.Type.GetType("System.String")
            IAIPFCE.DataType = System.Type.GetType("System.String")
            IAIPFCEActionNum.DataType = System.Type.GetType("System.String")
            IAIPFCEAirCode.DataType = System.Type.GetType("System.String")
            IAIPFCEActionType.DataType = System.Type.GetType("System.String")
            IAIPFCEDateAchieved.DataType = System.Type.GetType("System.String")
            FCEAction.Columns.Add(AFSFCEAIRS)
            FCEAction.Columns.Add(AFSFCE)
            FCEAction.Columns.Add(AFSFCEActionNum)
            FCEAction.Columns.Add(AFSFCEAirCode)
            FCEAction.Columns.Add(AFSFCEActionType)
            FCEAction.Columns.Add(AFSFCEDateAchieved)
            FCEAction.Columns.Add(AFSFCEActionResult)
            FCEAction.Columns.Add(IAIPFCEAIRS)
            FCEAction.Columns.Add(IAIPFCE)
            FCEAction.Columns.Add(IAIPFCEActionNum)
            FCEAction.Columns.Add(IAIPFCEAirCode)
            FCEAction.Columns.Add(IAIPFCEActionType)
            FCEAction.Columns.Add(IAIPFCEDateAchieved)

            StackTest = New DataTable("StackTest")
            AFSStackAIRS.DataType = System.Type.GetType("System.String")
            AFSStack.DataType = System.Type.GetType("System.String")
            AFSStackActionNum.DataType = System.Type.GetType("System.String")
            AFSStackAirCode.DataType = System.Type.GetType("System.String")
            AFSStackActionType.DataType = System.Type.GetType("System.String")
            AFSStackDateAchieved.DataType = System.Type.GetType("System.String")
            AFSStackResult.DataType = System.Type.GetType("System.String")
            IAIPStackAIRS.DataType = System.Type.GetType("System.String")
            IAIPStack.DataType = System.Type.GetType("System.String")
            IAIPStackActionNum.DataType = System.Type.GetType("System.String")
            IAIPStackAirCode.DataType = System.Type.GetType("System.String")
            IAIPStackActionType.DataType = System.Type.GetType("System.String")
            IAIPStackDateAchieved.DataType = System.Type.GetType("System.String")
            IAIPStackResult.DataType = System.Type.GetType("System.String")
            StackTest.Columns.Add(AFSStackAIRS)
            StackTest.Columns.Add(AFSStack)
            StackTest.Columns.Add(AFSStackActionNum)
            StackTest.Columns.Add(AFSStackAirCode)
            StackTest.Columns.Add(AFSStackActionType)
            StackTest.Columns.Add(AFSStackDateAchieved)
            StackTest.Columns.Add(AFSStackResult)
            StackTest.Columns.Add(IAIPStackAIRS)
            StackTest.Columns.Add(IAIPStack)
            StackTest.Columns.Add(IAIPStackActionNum)
            StackTest.Columns.Add(IAIPStackAirCode)
            StackTest.Columns.Add(IAIPStackActionType)
            StackTest.Columns.Add(IAIPStackDateAchieved)
            StackTest.Columns.Add(IAIPStackResult)

            Reports = New DataTable("Reports")
            AFSReportAIRS.DataType = System.Type.GetType("System.String")
            AFSReport.DataType = System.Type.GetType("System.String")
            AFSReportActionNum.DataType = System.Type.GetType("System.String")
            AFSReportAirCode.DataType = System.Type.GetType("System.String")
            AFSReportActionType.DataType = System.Type.GetType("System.String")
            AFSReportDateAchieved.DataType = System.Type.GetType("System.String")
            IAIPReportAIRS.DataType = System.Type.GetType("System.String")
            IAIPReport.DataType = System.Type.GetType("System.String")
            IAIPReportActionNum.DataType = System.Type.GetType("System.String")
            IAIPReportAirCode.DataType = System.Type.GetType("System.String")
            IAIPReportActionType.DataType = System.Type.GetType("System.String")
            IAIPReportDateAchieved.DataType = System.Type.GetType("System.String")
            Reports.Columns.Add(AFSReportAIRS)
            Reports.Columns.Add(AFSReport)
            Reports.Columns.Add(AFSReportActionNum)
            Reports.Columns.Add(AFSReportAirCode)
            Reports.Columns.Add(AFSReportActionType)
            Reports.Columns.Add(AFSReportDateAchieved)
            Reports.Columns.Add(IAIPReportAIRS)
            Reports.Columns.Add(IAIPReport)
            Reports.Columns.Add(IAIPReportActionNum)
            Reports.Columns.Add(IAIPReportAirCode)
            Reports.Columns.Add(IAIPReportActionType)
            Reports.Columns.Add(IAIPReportDateAchieved)

            Inspections = New DataTable("Inspections")
            AFSInspectionAIRS.DataType = System.Type.GetType("System.String")
            AFSInspection.DataType = System.Type.GetType("System.String")
            AFSInspectionActionNum.DataType = System.Type.GetType("System.String")
            AFSInspectionAirCode.DataType = System.Type.GetType("System.String")
            AFSInspectionActionType.DataType = System.Type.GetType("System.String")
            AFSInspectionDateAchieved.DataType = System.Type.GetType("System.String")
            IAIPInspectionAIRS.DataType = System.Type.GetType("System.String")
            IAIPInspection.DataType = System.Type.GetType("System.String")
            IAIPInspectionActionNum.DataType = System.Type.GetType("System.String")
            IAIPInspectionAirCode.DataType = System.Type.GetType("System.String")
            IAIPInspectionActionType.DataType = System.Type.GetType("System.String")
            IAIPInspectionDateAchieved.DataType = System.Type.GetType("System.String")
            Inspections.Columns.Add(AFSInspectionAIRS)
            Inspections.Columns.Add(AFSInspection)
            Inspections.Columns.Add(AFSInspectionActionNum)
            Inspections.Columns.Add(AFSInspectionAirCode)
            Inspections.Columns.Add(AFSInspectionActionType)
            Inspections.Columns.Add(AFSInspectionDateAchieved)
            Inspections.Columns.Add(IAIPInspectionAIRS)
            Inspections.Columns.Add(IAIPInspection)
            Inspections.Columns.Add(IAIPInspectionActionNum)
            Inspections.Columns.Add(IAIPInspectionAirCode)
            Inspections.Columns.Add(IAIPInspectionActionType)
            Inspections.Columns.Add(IAIPInspectionDateAchieved)

            ACCReviewed = New DataTable("ACCReviewed")
            AFSACCReviewedAIRS.DataType = System.Type.GetType("System.String")
            AFSACCReviewed.DataType = System.Type.GetType("System.String")
            AFSACCReviewedActionNum.DataType = System.Type.GetType("System.String")
            AFSACCReviewedAirCode.DataType = System.Type.GetType("System.String")
            AFSACCReviewedActionType.DataType = System.Type.GetType("System.String")
            AFSACCReviewedDateAchieved.DataType = System.Type.GetType("System.String")
            IAIPACCReviewedAIRS.DataType = System.Type.GetType("System.String")
            IAIPACCReviewed.DataType = System.Type.GetType("System.String")
            IAIPACCReviewedActionNum.DataType = System.Type.GetType("System.String")
            IAIPACCReviewedAirCode.DataType = System.Type.GetType("System.String")
            IAIPACCReviewedActionType.DataType = System.Type.GetType("System.String")
            IAIPACCReviewedDateAchieved.DataType = System.Type.GetType("System.String")
            ACCReviewed.Columns.Add(AFSACCReviewedAIRS)
            ACCReviewed.Columns.Add(AFSACCReviewed)
            ACCReviewed.Columns.Add(AFSACCReviewedActionNum)
            ACCReviewed.Columns.Add(AFSACCReviewedAirCode)
            ACCReviewed.Columns.Add(AFSACCReviewedActionType)
            ACCReviewed.Columns.Add(AFSACCReviewedDateAchieved)
            ACCReviewed.Columns.Add(IAIPACCReviewedAIRS)
            ACCReviewed.Columns.Add(IAIPACCReviewed)
            ACCReviewed.Columns.Add(IAIPACCReviewedActionNum)
            ACCReviewed.Columns.Add(IAIPACCReviewedAirCode)
            ACCReviewed.Columns.Add(IAIPACCReviewedActionType)
            ACCReviewed.Columns.Add(IAIPACCReviewedDateAchieved)

            ACCDue = New DataTable("ACCDue")
            AFSACCDueAIRS.DataType = System.Type.GetType("System.String")
            AFSACCDue.DataType = System.Type.GetType("System.String")
            AFSACCDueActionNum.DataType = System.Type.GetType("System.String")
            AFSACCDueAirCode.DataType = System.Type.GetType("System.String")
            AFSACCDueActionType.DataType = System.Type.GetType("System.String")
            AFSACCDueDateAchieved.DataType = System.Type.GetType("System.String")
            IAIPACCDueAIRS.DataType = System.Type.GetType("System.String")
            IAIPACCDue.DataType = System.Type.GetType("System.String")
            IAIPACCDueActionNum.DataType = System.Type.GetType("System.String")
            IAIPACCDueAirCode.DataType = System.Type.GetType("System.String")
            IAIPACCDueActionType.DataType = System.Type.GetType("System.String")
            IAIPACCDueDateAchieved.DataType = System.Type.GetType("System.String")
            ACCDue.Columns.Add(AFSACCDueAIRS)
            ACCDue.Columns.Add(AFSACCDue)
            ACCDue.Columns.Add(AFSACCDueActionNum)
            ACCDue.Columns.Add(AFSACCDueAirCode)
            ACCDue.Columns.Add(AFSACCDueActionType)
            ACCDue.Columns.Add(AFSACCDueDateAchieved)
            ACCDue.Columns.Add(IAIPACCDueAIRS)
            ACCDue.Columns.Add(IAIPACCDue)
            ACCDue.Columns.Add(IAIPACCDueActionNum)
            ACCDue.Columns.Add(IAIPACCDueAirCode)
            ACCDue.Columns.Add(IAIPACCDueActionType)
            ACCDue.Columns.Add(IAIPACCDueDateAchieved)


            Do While Len(txtCompleteFile.Text) > 0
                worktemp = Mid(txtCompleteFile.Text, 1, 80)
                Select Case Mid(worktemp, 11, 3)
                    Case "101" 'Facility Name 
                        row = FacilityName.NewRow()
                        row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                        row.Item("AFS - Action") = "101"
                        row.Item("AFS - Name") = Replace(Mid(worktemp, 16, 63), "  ", "")
                        temp = Mid(worktemp, 3, 8)
                        SQL = "Select " & _
                        "Upper(strFacilityName) as strFacilityName " & _
                        "from " & DBNameSpace & ".APBFacilityInformation " & _
                        "where strAIRSNumber = '0413" & temp & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            row.Item("IAIP - AIRS") = "13" & temp
                            row.Item("IAIP - Action") = "101"
                            row.Item("IAIP - Name") = dr.Item("strFacilityName")
                        End If
                        dr.Close()
                        FacilityName.Rows.Add(row)
                    Case "102" 'Address Data 
                        row = Address102.NewRow()
                        row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                        row.Item("AFS - Action") = "102"
                        row.Item("AFS - Address") = Replace(Mid(worktemp, 16, 63), "  ", "")
                        temp = Mid(worktemp, 3, 8)
                        SQL = "Select " & _
                        "Upper(strFacilityStreet1) as strFacilityStreet1 " & _
                        "from " & DBNameSpace & ".APBFacilityInformation " & _
                        "where strAIRSNumber = '0413" & temp & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            row.Item("IAIP - AIRS") = "13" & temp
                            row.Item("IAIP - Action") = "102"
                            row.Item("IAIP - Address") = dr.Item("strFacilityStreet1")
                        End If
                        dr.Close()
                        Address102.Rows.Add(row)
                    Case "103" 'Address Data 2
                        row = Address103.NewRow()
                        row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                        row.Item("AFS - Action") = "103"
                        row.Item("AFS - City") = Replace(Mid(worktemp, 16, 30), "  ", "")
                        row.Item("AFS - Zip Code") = Replace(Mid(worktemp, 46, 9), "  ", "")
                        row.Item("AFS - SIC") = Replace(Mid(worktemp, 55, 12), "  ", "")

                        temp = Mid(worktemp, 3, 8)
                        SQL = "select " & _
                        "upper(strFacilityCity) as strFacilityCity, " & _
                        "strFacilityZipCode, " & _
                        "strSICCode " & _
                        "from " & DBNameSpace & ".APBFacilityInformation, " & _
                        "" & DBNameSpace & ".APBHeaderData " & _
                        "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber " & _
                        "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & temp & "'"

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            row.Item("IAIP - AIRS") = "13" & temp
                            row.Item("IAIP - Action") = "103"
                            row.Item("IAIP - City") = dr.Item("strFacilityCity")
                            row.Item("IAIP - Zip Code") = dr.Item("strFacilityZipCode")
                            row.Item("IAIP - SIC") = dr.Item("strSICCode") & dr.Item("strSICCode") & dr.Item("strSICCode")
                        End If
                        dr.Close()
                        Address103.Rows.Add(row)
                    Case "105" 'Contact Data
                        row = Contact105.NewRow()
                        row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                        row.Item("AFS - Action") = "105"
                        row.Item("AFS - Contact Name") = Replace(Mid(worktemp, 14, 19), "  ", "")
                        row.Item("AFS - Contact Phone") = Replace(Mid(worktemp, 34, 10), "  ", "")
                        row.Item("AFS - Plant Desc.") = Replace(Mid(worktemp, 44, 36), "  ", "")
                        temp = Mid(worktemp, 3, 8)
                        SQL = "Select " & _
                        "upper(strPlantDescription) as strPlantDescription, " & _
                        "upper(strContactFirstName)|| ' ' ||upper(strContactLastName) as ContactName, " & _
                        "substr(strContactPhoneNumber1, 1, 10) as strContactPhoneNumber1 " & _
                        "from " & DBNameSpace & ".APBHeaderData, " & _
                        "" & DBNameSpace & ".APBContactInformation " & _
                        "where " & DBNameSpace & ".APBHeaderData.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSnumber " & _
                        "and strKey = '30' " & _
                        "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & temp & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            row.Item("IAIP - AIRS") = "13" & temp
                            row.Item("IAIP - Action") = "105"
                            row.Item("IAIP - Contact Name") = dr.Item("ContactName")
                            row.Item("IAIP - Contact Phone") = dr.Item("strContactPhoneNumber1")
                            row.Item("IAIP - Plant Desc.") = dr.Item("strPlantDescription")
                        End If
                        dr.Close()
                        Contact105.Rows.Add(row)
                    Case "121" 'Air Program Codes 
                        row = AirProgram.NewRow()
                        row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                        row.Item("AFS - Action") = "121"
                        row.Item("AFS - Air Code") = Mid(worktemp, 14, 1)
                        row.Item("AFS - Op Status") = Mid(worktemp, 15, 1)
                        temp = Mid(worktemp, 3, 8)
                        SQL = "select " & _
                        "distinct(substr(strAIRPollutantkey, 13,1)) as AirCode, " & _
                        "strOperationalStatus " & _
                        "from " & DBNameSpace & ".apbairprogrampollutants " & _
                        "where strairsnumber = '0413" & temp & "' " & _
                        "and substr(strAIRPollutantkey, 13,1) = '" & Mid(worktemp, 14, 1) & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            row.Item("IAIP - AIRS") = "13" & temp
                            row.Item("IAIP - Action") = "121"
                            row.Item("IAIP - Air Code") = dr.Item("AirCode")
                            row.Item("IAIP - Op Status") = dr.Item("strOperationalStatus")
                        End If
                        dr.Close()
                        AirProgram.Rows.Add(row)
                    Case "122" 'Sub Part data 
                        SubPartLoop = Mid(worktemp, 16, 45)
                        Do While SubPartLoop <> ""
                            tempSubPart = Mid(SubPartLoop, 1, 5)
                            tempSubPart = Replace(tempSubPart, " ", "")
                            If tempSubPart <> "" Then
                                row = SubPart.NewRow()
                                row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                                row.Item("AFS - Action") = "122"
                                row.Item("AFS - Air Code") = Mid(worktemp, 14, 1)
                                row.Item("AFS - Indicator") = Mid(worktemp, 15, 1)
                                row.Item("AFS - SubPart") = tempSubPart

                                temp = Mid(worktemp, 3, 8)
                                SQL = "select " & _
                                "substr(strSubPartKey, 13,1) as strSubPartkey, " & _
                                "strSubPart " & _
                                "from " & DBNameSpace & ".APBSubPartData " & _
                                "where strAIRSNumber = '0413" & temp & "' " & _
                                "and upper(strSubPart) = Upper('" & tempSubPart & "') "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                recExist = dr.Read
                                If recExist = True Then
                                    row.Item("IAIP - AIRS") = "13" & temp
                                    row.Item("IAIP - Action") = "122"
                                    row.Item("IAIP - Air Code") = dr.Item("strSubPartkey")
                                    row.Item("IAIP - Indicator") = "R"
                                    row.Item("IAIP - SubPart") = dr.Item("strSubPart")
                                End If
                                dr.Close()
                                SubPart.Rows.Add(row)

                                SubPartLoop = Replace(SubPartLoop, tempSubPart, "")
                            Else
                                SubPartLoop = Mid(SubPartLoop, 5)
                            End If
                        Loop
                    Case "131" 'Pollutants
                        row = Pollutants.NewRow()
                        row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                        row.Item("AFS - Action") = "181"
                        row.Item("AFS - Air Code") = Mid(worktemp, 14, 1)
                        row.Item("AFS - Pollutant") = Replace(Mid(worktemp, 15, 9), " ", "")
                        row.Item("AFS - Compliance Status") = Mid(worktemp, 24, 1)
                        row.Item("AFS - Classification") = Replace(Mid(worktemp, 25, 2), " ", "")
                        row.Item("AFS - Attainment") = Mid(worktemp, 27, 1)
                        temp = Mid(worktemp, 3, 8)
                        SQL = "select " & _
                        "distinct(substr(strAIRPollutantkey, 13,1)) as AirCode, " & _
                        "strPollutantKey, strComplianceStatus, " & _
                        "strClass, " & _
                        "case " & _
                        "    when strAttainmentStatus is null then 'A' " & _
                        "    when strAttainmentStatus = '00000' then 'A' " & _
                        "else 'N' " & _
                        "end strAttainmentStatus " & _
                        "from " & DBNameSpace & ".apbairprogrampollutants, " & _
                        "" & DBNameSpace & ".APBHeaderData  " & _
                        "where " & DBNameSpace & ".APBAirProgramPollutants.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber  " & _
                        "and " & DBNameSpace & ".apbheaderdata.strairsnumber = '0413" & temp & "' " & _
                        "and substr(strAIRPollutantkey, 13,1) = '" & Mid(worktemp, 14, 1) & "' " & _
                        "and Upper(strPollutantKey) = Upper('" & Replace(Mid(worktemp, 15, 5), " ", "") & "') "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            row.Item("IAIP - AIRS") = "13" & temp
                            row.Item("IAIP - Action") = "131"
                            row.Item("IAIP - Air Code") = dr.Item("AirCode")
                            row.Item("IAIP - Pollutant") = dr.Item("strPollutantKey")
                            row.Item("IAIP - Compliance Status") = dr.Item("strComplianceStatus")
                            row.Item("IAIP - Classification") = dr.Item("strClass")
                            row.Item("IAIP - Attainment") = dr.Item("strAttainmentStatus")
                        End If
                        dr.Close()
                        Pollutants.Rows.Add(row)

                    Case "161" 'Actions
                        Select Case Mid(worktemp, 23, 2)
                            Case "FS", "FF"  'Full Compliance Evaluation
                                row = FCEAction.NewRow()
                                row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                                row.Item("AFS - Action") = "161"
                                row.Item("AFS - Action Num") = Mid(worktemp, 14, 3)
                                row.Item("AFS - Air Code") = Replace(Mid(worktemp, 17, 6), " ", "")
                                row.Item("AFS - Action Type") = Mid(worktemp, 23, 2)
                                row.Item("AFS - Date Achieved") = Mid(worktemp, 47, 6)
                                row.Item("AFS - Action Result") = Mid(worktemp, 53, 2)
                                temp = Mid(worktemp, 3, 8)
                                Select Case Mid(worktemp, 49, 2)
                                    Case "01"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                    Case "02"
                                        tempDate = Mid(worktemp, 51, 2) & "-Feb-" & Mid(worktemp, 47, 2)
                                    Case "03"
                                        tempDate = Mid(worktemp, 51, 2) & "-Mar-" & Mid(worktemp, 47, 2)
                                    Case "04"
                                        tempDate = Mid(worktemp, 51, 2) & "-Apr-" & Mid(worktemp, 47, 2)
                                    Case "05"
                                        tempDate = Mid(worktemp, 51, 2) & "-May-" & Mid(worktemp, 47, 2)
                                    Case "06"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jun-" & Mid(worktemp, 47, 2)
                                    Case "07"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jul-" & Mid(worktemp, 47, 2)
                                    Case "08"
                                        tempDate = Mid(worktemp, 51, 2) & "-Aug-" & Mid(worktemp, 47, 2)
                                    Case "09"
                                        tempDate = Mid(worktemp, 51, 2) & "-Sep-" & Mid(worktemp, 47, 2)
                                    Case "10"
                                        tempDate = Mid(worktemp, 51, 2) & "-Oct-" & Mid(worktemp, 47, 2)
                                    Case "11"
                                        tempDate = Mid(worktemp, 51, 2) & "-Nov-" & Mid(worktemp, 47, 2)
                                    Case "12"
                                        tempDate = Mid(worktemp, 51, 2) & "-Dec-" & Mid(worktemp, 47, 2)
                                    Case Else
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                End Select

                                SQL = "select " & _
                                "" & DBNameSpace & ".SSCPFCEMaster.strFCENumber,  " & _
                                "to_char(to_date(datFCECompleted, 'YY-mm-DD')) as datFCECompleted, " & _
                                "strAFSActionNumber, strAirProgramCodes, " & _
                                "case " & _
                                "when strSiteInspection = 'True' then 'FS' " & _
                                "else 'FF' " & _
                                "end SiteInspection " & _
                                "from " & DBNameSpace & ".SSCPFCEMaster, " & DBNameSpace & ".SSCPFCE,  " & _
                                "" & DBNameSpace & ".AFSSSCPFCERecords, " & DBNameSpace & ".APBHeaderData  " & _
                                "where " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber  " & _
                                "and " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".AFSSSCPFCERecords.strFCENumber   " & _
                                "and " & DBNameSpace & ".SSCPFCEMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber  " & _
                                "and " & DBNameSpace & ".SSCPFCEMaster.strAIRSnumber = '0413" & temp & "' " & _
                                "and " & DBNameSpace & ".SSCPFCE.datFCECompleted = '" & tempDate & "' "
                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                recExist = dr.Read
                                If recExist = True Then
                                    row.Item("IAIP - AIRS") = "13" & temp
                                    row.Item("IAIP - Action") = "161"
                                    row.Item("IAIP - Action Num") = dr.Item("strAFSActionNumber")
                                    AirCodes = dr.Item("strAirProgramCodes")
                                    If Mid(AirCodes, 1, 1) = "1" Then
                                        temp = "0"
                                    End If
                                    If Mid(AirCodes, 3, 1) = "1" Then
                                        temp = temp & "3"
                                    End If
                                    If Mid(AirCodes, 4, 1) = "1" Then
                                        temp = temp & "4"
                                    End If
                                    If Mid(AirCodes, 5, 1) = "1" Then
                                        temp = temp & "6"
                                    End If
                                    If Mid(AirCodes, 6, 1) = "1" Then
                                        temp = temp & "7"
                                    End If
                                    If Mid(AirCodes, 7, 1) = "1" Then
                                        temp = temp & "8"
                                    End If
                                    If Mid(AirCodes, 8, 1) = "1" Then
                                        temp = temp & "9"
                                    End If
                                    If Mid(AirCodes, 9, 1) = "1" Then
                                        temp = temp & "F"
                                    End If
                                    If Mid(AirCodes, 10, 1) = "1" Then
                                        temp = temp & "A"
                                    End If
                                    If Mid(AirCodes, 11, 1) = "1" Then
                                        temp = temp & "I"
                                    End If
                                    If Mid(AirCodes, 12, 1) = "1" Then
                                        temp = temp & "M"
                                    End If
                                    If Mid(AirCodes, 13, 1) = "1" Then
                                        temp = temp & "V"
                                    End If
                                    row.Item("IAIP - Air Code") = temp
                                    row.Item("IAIP - Action Type") = dr.Item("SiteInspection")
                                    temp = dr.Item("datFCECompleted")
                                    Select Case Mid(temp, 4, 3)
                                        Case "JAN"
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                        Case "FEB"
                                            temp = Mid(temp, 1, 2) & "02" & Mid(temp, 8, 2)
                                        Case "MAR"
                                            temp = Mid(temp, 1, 2) & "03" & Mid(temp, 8, 2)
                                        Case "APR"
                                            temp = Mid(temp, 1, 2) & "04" & Mid(temp, 8, 2)
                                        Case "MAY"
                                            temp = Mid(temp, 1, 2) & "05" & Mid(temp, 8, 2)
                                        Case "JUN"
                                            temp = Mid(temp, 1, 2) & "06" & Mid(temp, 8, 2)
                                        Case "JUL"
                                            temp = Mid(temp, 1, 2) & "07" & Mid(temp, 8, 2)
                                        Case "AUG"
                                            temp = Mid(temp, 1, 2) & "08" & Mid(temp, 8, 2)
                                        Case "SEP"
                                            temp = Mid(temp, 1, 2) & "09" & Mid(temp, 8, 2)
                                        Case "OCT"
                                            temp = Mid(temp, 1, 2) & "10" & Mid(temp, 8, 2)
                                        Case "NOV"
                                            temp = Mid(temp, 1, 2) & "11" & Mid(temp, 8, 2)
                                        Case "DEC"
                                            temp = Mid(temp, 1, 2) & "12" & Mid(temp, 8, 2)
                                        Case Else
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                    End Select
                                    row.Item("IAIP - Date Achieved") = temp
                                End If
                                dr.Close()
                                FCEAction.Rows.Add(row)
                            Case "TR", "23", "24", "59", "B8"  'Stack Test
                                row = StackTest.NewRow()
                                row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                                row.Item("AFS - Action") = "161"
                                row.Item("AFS - Action Num") = Mid(worktemp, 14, 3)
                                row.Item("AFS - Air Code") = Replace(Mid(worktemp, 17, 6), " ", "")
                                row.Item("AFS - Action Type") = Mid(worktemp, 23, 2)
                                row.Item("AFS - Date Achieved") = Mid(worktemp, 47, 6)
                                row.Item("AFS - Stack Result") = Mid(worktemp, 53, 2)
                                temp = Mid(worktemp, 3, 8)
                                Select Case Mid(worktemp, 49, 2)
                                    Case "01"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                    Case "02"
                                        tempDate = Mid(worktemp, 51, 2) & "-Feb-" & Mid(worktemp, 47, 2)
                                    Case "03"
                                        tempDate = Mid(worktemp, 51, 2) & "-Mar-" & Mid(worktemp, 47, 2)
                                    Case "04"
                                        tempDate = Mid(worktemp, 51, 2) & "-Apr-" & Mid(worktemp, 47, 2)
                                    Case "05"
                                        tempDate = Mid(worktemp, 51, 2) & "-May-" & Mid(worktemp, 47, 2)
                                    Case "06"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jun-" & Mid(worktemp, 47, 2)
                                    Case "07"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jul-" & Mid(worktemp, 47, 2)
                                    Case "08"
                                        tempDate = Mid(worktemp, 51, 2) & "-Aug-" & Mid(worktemp, 47, 2)
                                    Case "09"
                                        tempDate = Mid(worktemp, 51, 2) & "-Sep-" & Mid(worktemp, 47, 2)
                                    Case "10"
                                        tempDate = Mid(worktemp, 51, 2) & "-Oct-" & Mid(worktemp, 47, 2)
                                    Case "11"
                                        tempDate = Mid(worktemp, 51, 2) & "-Nov-" & Mid(worktemp, 47, 2)
                                    Case "12"
                                        tempDate = Mid(worktemp, 51, 2) & "-Dec-" & Mid(worktemp, 47, 2)
                                    Case Else
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                End Select

                                SQL = "select " & _
                                "" & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
                                "to_char(to_date(datTestDateEnd, 'YY-mm-DD')) as datTestDateEnd, " & _
                                "case " & _
                                "when strComplianceStatus = '01' then '01' " & _
                                "when strComplianceStatus = '02' then 'PP' " & _
                                "when strComplianceStatus = '03' then 'PP' " & _
                                "when strComplianceStatus = '04' then '01' " & _
                                "when strComplianceStatus = '05' then 'FF' " & _
                                "end ResultsCode, " & _
                                "case " & _
                                "when strWitnessingEngineer = '0' then 'TR' " & _
                                "else '23' " & _
                                "end ActionType, " & _
                                "strAirProgramCodes, " & _
                                "strAfsActionNumber " & _
                                "from " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".ISMPReportInformation, " & _
                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".AFSISMPRecords " & _
                                "where " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                                "and " & DBNameSpace & ".ISMPMaster.strAIRSnumber = " & DBNameSpace & ".APBHeaderData.strAirsNumber " & _
                                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".AFSISMPRecords.strReferenceNumber " & _
                                "and " & DBNameSpace & ".ISMPMaster.strAIRSNumber = '0413" & temp & "' " & _
                                "and datTestDateEnd = '" & tempDate & "' "
                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                recExist = dr.Read
                                If recExist = True Then
                                    row.Item("IAIP - AIRS") = "13" & temp
                                    row.Item("IAIP - Action") = "161"
                                    row.Item("IAIP - Action Num") = dr.Item("strAFSActionNumber")
                                    AirCodes = dr.Item("strAirProgramCodes")
                                    If Mid(AirCodes, 1, 1) = "1" Then
                                        temp = "0"
                                    End If
                                    If Mid(AirCodes, 3, 1) = "1" Then
                                        temp = temp & "3"
                                    End If
                                    If Mid(AirCodes, 4, 1) = "1" Then
                                        temp = temp & "4"
                                    End If
                                    If Mid(AirCodes, 5, 1) = "1" Then
                                        temp = temp & "6"
                                    End If
                                    If Mid(AirCodes, 6, 1) = "1" Then
                                        temp = temp & "7"
                                    End If
                                    If Mid(AirCodes, 7, 1) = "1" Then
                                        temp = temp & "8"
                                    End If
                                    If Mid(AirCodes, 8, 1) = "1" Then
                                        temp = temp & "9"
                                    End If
                                    If Mid(AirCodes, 9, 1) = "1" Then
                                        temp = temp & "F"
                                    End If
                                    If Mid(AirCodes, 10, 1) = "1" Then
                                        temp = temp & "A"
                                    End If
                                    If Mid(AirCodes, 11, 1) = "1" Then
                                        temp = temp & "I"
                                    End If
                                    If Mid(AirCodes, 12, 1) = "1" Then
                                        temp = temp & "M"
                                    End If
                                    If Mid(AirCodes, 13, 1) = "1" Then
                                        temp = temp & "V"
                                    End If
                                    row.Item("IAIP - Air Code") = temp

                                    row.Item("IAIP - Action Type") = dr.Item("ActionType")
                                    temp = dr.Item("datTestDateEnd")
                                    Select Case Mid(temp, 4, 3)
                                        Case "JAN"
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                        Case "FEB"
                                            temp = Mid(temp, 1, 2) & "02" & Mid(temp, 8, 2)
                                        Case "MAR"
                                            temp = Mid(temp, 1, 2) & "03" & Mid(temp, 8, 2)
                                        Case "APR"
                                            temp = Mid(temp, 1, 2) & "04" & Mid(temp, 8, 2)
                                        Case "MAY"
                                            temp = Mid(temp, 1, 2) & "05" & Mid(temp, 8, 2)
                                        Case "JUN"
                                            temp = Mid(temp, 1, 2) & "06" & Mid(temp, 8, 2)
                                        Case "JUL"
                                            temp = Mid(temp, 1, 2) & "07" & Mid(temp, 8, 2)
                                        Case "AUG"
                                            temp = Mid(temp, 1, 2) & "08" & Mid(temp, 8, 2)
                                        Case "SEP"
                                            temp = Mid(temp, 1, 2) & "09" & Mid(temp, 8, 2)
                                        Case "OCT"
                                            temp = Mid(temp, 1, 2) & "10" & Mid(temp, 8, 2)
                                        Case "NOV"
                                            temp = Mid(temp, 1, 2) & "11" & Mid(temp, 8, 2)
                                        Case "DEC"
                                            temp = Mid(temp, 1, 2) & "12" & Mid(temp, 8, 2)
                                        Case Else
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                    End Select
                                    row.Item("IAIP - Date Achieved") = temp
                                    row.Item("IAIP - Stack Result") = dr.Item("ResultsCode")
                                End If
                                dr.Close()
                                StackTest.Rows.Add(row)
                            Case "37" 'Report Due
                                row = Reports.NewRow()
                                row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                                row.Item("AFS - Action") = "161"
                                row.Item("AFS - Action Num") = Mid(worktemp, 14, 3)
                                row.Item("AFS - Air Code") = Replace(Mid(worktemp, 17, 6), " ", "")
                                row.Item("AFS - Action Type") = Mid(worktemp, 23, 2)
                                row.Item("AFS - Date Achieved") = Mid(worktemp, 47, 6)
                                temp = Mid(worktemp, 3, 8)
                                Select Case Mid(worktemp, 49, 2)
                                    Case "01"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                    Case "02"
                                        tempDate = Mid(worktemp, 51, 2) & "-Feb-" & Mid(worktemp, 47, 2)
                                    Case "03"
                                        tempDate = Mid(worktemp, 51, 2) & "-Mar-" & Mid(worktemp, 47, 2)
                                    Case "04"
                                        tempDate = Mid(worktemp, 51, 2) & "-Apr-" & Mid(worktemp, 47, 2)
                                    Case "05"
                                        tempDate = Mid(worktemp, 51, 2) & "-May-" & Mid(worktemp, 47, 2)
                                    Case "06"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jun-" & Mid(worktemp, 47, 2)
                                    Case "07"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jul-" & Mid(worktemp, 47, 2)
                                    Case "08"
                                        tempDate = Mid(worktemp, 51, 2) & "-Aug-" & Mid(worktemp, 47, 2)
                                    Case "09"
                                        tempDate = Mid(worktemp, 51, 2) & "-Sep-" & Mid(worktemp, 47, 2)
                                    Case "10"
                                        tempDate = Mid(worktemp, 51, 2) & "-Oct-" & Mid(worktemp, 47, 2)
                                    Case "11"
                                        tempDate = Mid(worktemp, 51, 2) & "-Nov-" & Mid(worktemp, 47, 2)
                                    Case "12"
                                        tempDate = Mid(worktemp, 51, 2) & "-Dec-" & Mid(worktemp, 47, 2)
                                    Case Else
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                End Select
                                SQL = "select " & _
                                "case " & _
                                "when strEventType = '01' then '37' " & _
                                "end EventType, " & _
                                "strAFSActionNumber,  " & _
                                "to_char(to_date(datCompleteDate, 'YY-mm-DD')) as AchievedDate, " & _
                                "strAirProgramCodes " & _
                                "from " & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".AFSSSCPRecords,  " & _
                                "" & DBNameSpace & ".APBHeaderData " & _
                                "where " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber  " & _
                                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".AFSSSCPRecords.strTrackingNumber " & _
                                "and " & DBNameSpace & ".sscpitemmaster.strAIRSnumber = '0413" & temp & "' " & _
                                "and " & DBNameSpace & ".SSCPItemMaster.strEventType = '01' " & _
                                "and datCompleteDate = '" & tempDate & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                recExist = dr.Read
                                If recExist = True Then
                                    row.Item("IAIP - AIRS") = "13" & temp
                                    row.Item("IAIP - Action") = "161"
                                    row.Item("IAIP - Action Num") = dr.Item("strAFSActionNumber")
                                    AirCodes = dr.Item("strAirProgramCodes")
                                    If Mid(AirCodes, 1, 1) = "1" Then
                                        temp = "0"
                                    End If
                                    If Mid(AirCodes, 3, 1) = "1" Then
                                        temp = temp & "3"
                                    End If
                                    If Mid(AirCodes, 4, 1) = "1" Then
                                        temp = temp & "4"
                                    End If
                                    If Mid(AirCodes, 5, 1) = "1" Then
                                        temp = temp & "6"
                                    End If
                                    If Mid(AirCodes, 6, 1) = "1" Then
                                        temp = temp & "7"
                                    End If
                                    If Mid(AirCodes, 7, 1) = "1" Then
                                        temp = temp & "8"
                                    End If
                                    If Mid(AirCodes, 8, 1) = "1" Then
                                        temp = temp & "9"
                                    End If
                                    If Mid(AirCodes, 9, 1) = "1" Then
                                        temp = temp & "F"
                                    End If
                                    If Mid(AirCodes, 10, 1) = "1" Then
                                        temp = temp & "A"
                                    End If
                                    If Mid(AirCodes, 11, 1) = "1" Then
                                        temp = temp & "I"
                                    End If
                                    If Mid(AirCodes, 12, 1) = "1" Then
                                        temp = temp & "M"
                                    End If
                                    If Mid(AirCodes, 13, 1) = "1" Then
                                        temp = temp & "V"
                                    End If
                                    row.Item("IAIP - Air Code") = temp

                                    row.Item("IAIP - Action Type") = dr.Item("EventType")
                                    temp = dr.Item("AchievedDate")
                                    Select Case Mid(temp, 4, 3)
                                        Case "JAN"
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                        Case "FEB"
                                            temp = Mid(temp, 1, 2) & "02" & Mid(temp, 8, 2)
                                        Case "MAR"
                                            temp = Mid(temp, 1, 2) & "03" & Mid(temp, 8, 2)
                                        Case "APR"
                                            temp = Mid(temp, 1, 2) & "04" & Mid(temp, 8, 2)
                                        Case "MAY"
                                            temp = Mid(temp, 1, 2) & "05" & Mid(temp, 8, 2)
                                        Case "JUN"
                                            temp = Mid(temp, 1, 2) & "06" & Mid(temp, 8, 2)
                                        Case "JUL"
                                            temp = Mid(temp, 1, 2) & "07" & Mid(temp, 8, 2)
                                        Case "AUG"
                                            temp = Mid(temp, 1, 2) & "08" & Mid(temp, 8, 2)
                                        Case "SEP"
                                            temp = Mid(temp, 1, 2) & "09" & Mid(temp, 8, 2)
                                        Case "OCT"
                                            temp = Mid(temp, 1, 2) & "10" & Mid(temp, 8, 2)
                                        Case "NOV"
                                            temp = Mid(temp, 1, 2) & "11" & Mid(temp, 8, 2)
                                        Case "DEC"
                                            temp = Mid(temp, 1, 2) & "12" & Mid(temp, 8, 2)
                                        Case Else
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                    End Select
                                    row.Item("IAIP - Date Achieved") = temp
                                End If
                                dr.Close()
                                Reports.Rows.Add(row)
                            Case "22", "27", "29", "54", "68", "A4", "A5", "A6", "K3"  'Inspections
                                row = Inspections.NewRow()
                                row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                                row.Item("AFS - Action") = "161"
                                row.Item("AFS - Action Num") = Mid(worktemp, 14, 3)
                                row.Item("AFS - Air Code") = Replace(Mid(worktemp, 17, 6), " ", "")
                                row.Item("AFS - Action Type") = Mid(worktemp, 23, 2)
                                row.Item("AFS - Date Achieved") = Mid(worktemp, 47, 6)

                                temp = Mid(worktemp, 3, 8)
                                Select Case Mid(worktemp, 49, 2)
                                    Case "01"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                    Case "02"
                                        tempDate = Mid(worktemp, 51, 2) & "-Feb-" & Mid(worktemp, 47, 2)
                                    Case "03"
                                        tempDate = Mid(worktemp, 51, 2) & "-Mar-" & Mid(worktemp, 47, 2)
                                    Case "04"
                                        tempDate = Mid(worktemp, 51, 2) & "-Apr-" & Mid(worktemp, 47, 2)
                                    Case "05"
                                        tempDate = Mid(worktemp, 51, 2) & "-May-" & Mid(worktemp, 47, 2)
                                    Case "06"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jun-" & Mid(worktemp, 47, 2)
                                    Case "07"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jul-" & Mid(worktemp, 47, 2)
                                    Case "08"
                                        tempDate = Mid(worktemp, 51, 2) & "-Aug-" & Mid(worktemp, 47, 2)
                                    Case "09"
                                        tempDate = Mid(worktemp, 51, 2) & "-Sep-" & Mid(worktemp, 47, 2)
                                    Case "10"
                                        tempDate = Mid(worktemp, 51, 2) & "-Oct-" & Mid(worktemp, 47, 2)
                                    Case "11"
                                        tempDate = Mid(worktemp, 51, 2) & "-Nov-" & Mid(worktemp, 47, 2)
                                    Case "12"
                                        tempDate = Mid(worktemp, 51, 2) & "-Dec-" & Mid(worktemp, 47, 2)
                                    Case Else
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                End Select
                                SQL = "select " & _
                                "case " & _
                                "when strEventType = '02' then '27' " & _
                                "end EventType, " & _
                                "strAFSActionNumber,  " & _
                                "to_char(to_date(datCompleteDate, 'YY-mm-DD')) as AchievedDate, " & _
                                "strAirProgramCodes " & _
                                "from " & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".AFSSSCPRecords,  " & _
                                "" & DBNameSpace & ".APBHeaderData " & _
                                "where " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber  " & _
                                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".AFSSSCPRecords.strTrackingNumber " & _
                                "and " & DBNameSpace & ".sscpitemmaster.strAIRSnumber = '0413" & temp & "' " & _
                                "and " & DBNameSpace & ".SSCPItemMaster.strEventType = '02' " & _
                                "and datCompleteDate = '" & tempDate & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                recExist = dr.Read
                                If recExist = True Then
                                    row.Item("IAIP - AIRS") = "13" & temp
                                    row.Item("IAIP - Action") = "161"
                                    row.Item("IAIP - Action Num") = dr.Item("strAFSActionNumber")
                                    AirCodes = dr.Item("strAirProgramCodes")
                                    If Mid(AirCodes, 1, 1) = "1" Then
                                        temp = "0"
                                    End If
                                    If Mid(AirCodes, 3, 1) = "1" Then
                                        temp = temp & "3"
                                    End If
                                    If Mid(AirCodes, 4, 1) = "1" Then
                                        temp = temp & "4"
                                    End If
                                    If Mid(AirCodes, 5, 1) = "1" Then
                                        temp = temp & "6"
                                    End If
                                    If Mid(AirCodes, 6, 1) = "1" Then
                                        temp = temp & "7"
                                    End If
                                    If Mid(AirCodes, 7, 1) = "1" Then
                                        temp = temp & "8"
                                    End If
                                    If Mid(AirCodes, 8, 1) = "1" Then
                                        temp = temp & "9"
                                    End If
                                    If Mid(AirCodes, 9, 1) = "1" Then
                                        temp = temp & "F"
                                    End If
                                    If Mid(AirCodes, 10, 1) = "1" Then
                                        temp = temp & "A"
                                    End If
                                    If Mid(AirCodes, 11, 1) = "1" Then
                                        temp = temp & "I"
                                    End If
                                    If Mid(AirCodes, 12, 1) = "1" Then
                                        temp = temp & "M"
                                    End If
                                    If Mid(AirCodes, 13, 1) = "1" Then
                                        temp = temp & "V"
                                    End If
                                    row.Item("IAIP - Air Code") = temp

                                    row.Item("IAIP - Action Type") = dr.Item("EventType")
                                    temp = dr.Item("AchievedDate")
                                    Select Case Mid(temp, 4, 3)
                                        Case "JAN"
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                        Case "FEB"
                                            temp = Mid(temp, 1, 2) & "02" & Mid(temp, 8, 2)
                                        Case "MAR"
                                            temp = Mid(temp, 1, 2) & "03" & Mid(temp, 8, 2)
                                        Case "APR"
                                            temp = Mid(temp, 1, 2) & "04" & Mid(temp, 8, 2)
                                        Case "MAY"
                                            temp = Mid(temp, 1, 2) & "05" & Mid(temp, 8, 2)
                                        Case "JUN"
                                            temp = Mid(temp, 1, 2) & "06" & Mid(temp, 8, 2)
                                        Case "JUL"
                                            temp = Mid(temp, 1, 2) & "07" & Mid(temp, 8, 2)
                                        Case "AUG"
                                            temp = Mid(temp, 1, 2) & "08" & Mid(temp, 8, 2)
                                        Case "SEP"
                                            temp = Mid(temp, 1, 2) & "09" & Mid(temp, 8, 2)
                                        Case "OCT"
                                            temp = Mid(temp, 1, 2) & "10" & Mid(temp, 8, 2)
                                        Case "NOV"
                                            temp = Mid(temp, 1, 2) & "11" & Mid(temp, 8, 2)
                                        Case "DEC"
                                            temp = Mid(temp, 1, 2) & "12" & Mid(temp, 8, 2)
                                        Case Else
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                    End Select
                                    row.Item("IAIP - Date Achieved") = temp
                                End If
                                dr.Close()
                                Inspections.Rows.Add(row)

                            Case "ER", "SR" 'ACC Reveiwed
                                row = ACCReviewed.NewRow()
                                row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                                row.Item("AFS - Action") = "161"
                                row.Item("AFS - Action Num") = Mid(worktemp, 14, 3)
                                row.Item("AFS - Air Code") = Replace(Mid(worktemp, 17, 6), " ", "")
                                row.Item("AFS - Action Type") = Mid(worktemp, 23, 2)
                                row.Item("AFS - Date Achieved") = Mid(worktemp, 47, 6)

                                temp = Mid(worktemp, 3, 8)
                                Select Case Mid(worktemp, 49, 2)
                                    Case "01"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                    Case "02"
                                        tempDate = Mid(worktemp, 51, 2) & "-Feb-" & Mid(worktemp, 47, 2)
                                    Case "03"
                                        tempDate = Mid(worktemp, 51, 2) & "-Mar-" & Mid(worktemp, 47, 2)
                                    Case "04"
                                        tempDate = Mid(worktemp, 51, 2) & "-Apr-" & Mid(worktemp, 47, 2)
                                    Case "05"
                                        tempDate = Mid(worktemp, 51, 2) & "-May-" & Mid(worktemp, 47, 2)
                                    Case "06"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jun-" & Mid(worktemp, 47, 2)
                                    Case "07"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jul-" & Mid(worktemp, 47, 2)
                                    Case "08"
                                        tempDate = Mid(worktemp, 51, 2) & "-Aug-" & Mid(worktemp, 47, 2)
                                    Case "09"
                                        tempDate = Mid(worktemp, 51, 2) & "-Sep-" & Mid(worktemp, 47, 2)
                                    Case "10"
                                        tempDate = Mid(worktemp, 51, 2) & "-Oct-" & Mid(worktemp, 47, 2)
                                    Case "11"
                                        tempDate = Mid(worktemp, 51, 2) & "-Nov-" & Mid(worktemp, 47, 2)
                                    Case "12"
                                        tempDate = Mid(worktemp, 51, 2) & "-Dec-" & Mid(worktemp, 47, 2)
                                    Case Else
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                End Select

                                SQL = "select " & _
                                "case " & _
                                "when strEventType = '04' then 'SR' " & _
                                "end EventType, " & _
                                "strAFSActionNumber,  " & _
                                "to_char(to_date(datCompleteDate, 'YY-mm-DD')) as AchievedDate, " & _
                                "strAirProgramCodes " & _
                                "from " & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".AFSSSCPRecords,  " & _
                                "" & DBNameSpace & ".APBHeaderData " & _
                                "where " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber  " & _
                                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".AFSSSCPRecords.strTrackingNumber " & _
                                "and " & DBNameSpace & ".sscpitemmaster.strAIRSnumber = '0413" & temp & "' " & _
                                "and " & DBNameSpace & ".SSCPItemMaster.strEventType = '04' " & _
                                "and datCompleteDate = '" & tempDate & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                recExist = dr.Read

                                If recExist = True Then
                                    row.Item("IAIP - AIRS") = "13" & temp
                                    row.Item("IAIP - Action") = "161"
                                    row.Item("IAIP - Action Num") = dr.Item("strAFSActionNumber")
                                    AirCodes = dr.Item("strAirProgramCodes")
                                    If Mid(AirCodes, 1, 1) = "1" Then
                                        temp = "0"
                                    End If
                                    If Mid(AirCodes, 3, 1) = "1" Then
                                        temp = temp & "3"
                                    End If
                                    If Mid(AirCodes, 4, 1) = "1" Then
                                        temp = temp & "4"
                                    End If
                                    If Mid(AirCodes, 5, 1) = "1" Then
                                        temp = temp & "6"
                                    End If
                                    If Mid(AirCodes, 6, 1) = "1" Then
                                        temp = temp & "7"
                                    End If
                                    If Mid(AirCodes, 7, 1) = "1" Then
                                        temp = temp & "8"
                                    End If
                                    If Mid(AirCodes, 8, 1) = "1" Then
                                        temp = temp & "9"
                                    End If
                                    If Mid(AirCodes, 9, 1) = "1" Then
                                        temp = temp & "F"
                                    End If
                                    If Mid(AirCodes, 10, 1) = "1" Then
                                        temp = temp & "A"
                                    End If
                                    If Mid(AirCodes, 11, 1) = "1" Then
                                        temp = temp & "I"
                                    End If
                                    If Mid(AirCodes, 12, 1) = "1" Then
                                        temp = temp & "M"
                                    End If
                                    If Mid(AirCodes, 13, 1) = "1" Then
                                        temp = temp & "V"
                                    End If
                                    row.Item("IAIP - Air Code") = temp

                                    row.Item("IAIP - Action Type") = dr.Item("EventType")
                                    temp = dr.Item("AchievedDate")
                                    Select Case Mid(temp, 4, 3)
                                        Case "JAN"
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                        Case "FEB"
                                            temp = Mid(temp, 1, 2) & "02" & Mid(temp, 8, 2)
                                        Case "MAR"
                                            temp = Mid(temp, 1, 2) & "03" & Mid(temp, 8, 2)
                                        Case "APR"
                                            temp = Mid(temp, 1, 2) & "04" & Mid(temp, 8, 2)
                                        Case "MAY"
                                            temp = Mid(temp, 1, 2) & "05" & Mid(temp, 8, 2)
                                        Case "JUN"
                                            temp = Mid(temp, 1, 2) & "06" & Mid(temp, 8, 2)
                                        Case "JUL"
                                            temp = Mid(temp, 1, 2) & "07" & Mid(temp, 8, 2)
                                        Case "AUG"
                                            temp = Mid(temp, 1, 2) & "08" & Mid(temp, 8, 2)
                                        Case "SEP"
                                            temp = Mid(temp, 1, 2) & "09" & Mid(temp, 8, 2)
                                        Case "OCT"
                                            temp = Mid(temp, 1, 2) & "10" & Mid(temp, 8, 2)
                                        Case "NOV"
                                            temp = Mid(temp, 1, 2) & "11" & Mid(temp, 8, 2)
                                        Case "DEC"
                                            temp = Mid(temp, 1, 2) & "12" & Mid(temp, 8, 2)
                                        Case Else
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                    End Select
                                    row.Item("IAIP - Date Achieved") = temp
                                End If
                                dr.Close()
                                ACCReviewed.Rows.Add(row)

                            Case "CS", "CC" 'ACC Due
                                row = ACCDue.NewRow()
                                row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                                row.Item("AFS - Action") = "161"
                                row.Item("AFS - Action Num") = Mid(worktemp, 14, 3)
                                row.Item("AFS - Air Code") = Replace(Mid(worktemp, 17, 6), " ", "")
                                row.Item("AFS - Action Type") = Mid(worktemp, 23, 2)
                                row.Item("AFS - Date Achieved") = Mid(worktemp, 47, 6)

                                temp = Mid(worktemp, 3, 8)
                                Select Case Mid(worktemp, 49, 2)
                                    Case "01"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                    Case "02"
                                        tempDate = Mid(worktemp, 51, 2) & "-Feb-" & Mid(worktemp, 47, 2)
                                    Case "03"
                                        tempDate = Mid(worktemp, 51, 2) & "-Mar-" & Mid(worktemp, 47, 2)
                                    Case "04"
                                        tempDate = Mid(worktemp, 51, 2) & "-Apr-" & Mid(worktemp, 47, 2)
                                    Case "05"
                                        tempDate = Mid(worktemp, 51, 2) & "-May-" & Mid(worktemp, 47, 2)
                                    Case "06"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jun-" & Mid(worktemp, 47, 2)
                                    Case "07"
                                        tempDate = Mid(worktemp, 51, 2) & "-Jul-" & Mid(worktemp, 47, 2)
                                    Case "08"
                                        tempDate = Mid(worktemp, 51, 2) & "-Aug-" & Mid(worktemp, 47, 2)
                                    Case "09"
                                        tempDate = Mid(worktemp, 51, 2) & "-Sep-" & Mid(worktemp, 47, 2)
                                    Case "10"
                                        tempDate = Mid(worktemp, 51, 2) & "-Oct-" & Mid(worktemp, 47, 2)
                                    Case "11"
                                        tempDate = Mid(worktemp, 51, 2) & "-Nov-" & Mid(worktemp, 47, 2)
                                    Case "12"
                                        tempDate = Mid(worktemp, 51, 2) & "-Dec-" & Mid(worktemp, 47, 2)
                                    Case Else
                                        tempDate = Mid(worktemp, 51, 2) & "-Jan-" & Mid(worktemp, 47, 2)
                                End Select

                                SQL = "select " & _
                                "case " & _
                                "when strEventType = '06' then 'CC' " & _
                                "end EventType, " & _
                                "strAFSActionNumber,  " & _
                                "to_char(to_date(datCompleteDate, 'YY-mm-DD')) as AchievedDate, " & _
                                "strAirProgramCodes " & _
                                "from " & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".AFSSSCPRecords,  " & _
                                "" & DBNameSpace & ".APBHeaderData " & _
                                "where " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber  " & _
                                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".AFSSSCPRecords.strTrackingNumber " & _
                                "and " & DBNameSpace & ".sscpitemmaster.strAIRSnumber = '0413" & temp & "' " & _
                                "and " & DBNameSpace & ".SSCPItemMaster.strEventType = '06' " & _
                                "and datCompleteDate = '" & tempDate & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                recExist = dr.Read

                                If recExist = True Then
                                    row.Item("IAIP - AIRS") = "13" & temp
                                    row.Item("IAIP - Action") = "161"
                                    row.Item("IAIP - Action Num") = dr.Item("strAFSActionNumber")
                                    AirCodes = dr.Item("strAirProgramCodes")
                                    If Mid(AirCodes, 1, 1) = "1" Then
                                        temp = "0"
                                    End If
                                    If Mid(AirCodes, 3, 1) = "1" Then
                                        temp = temp & "3"
                                    End If
                                    If Mid(AirCodes, 4, 1) = "1" Then
                                        temp = temp & "4"
                                    End If
                                    If Mid(AirCodes, 5, 1) = "1" Then
                                        temp = temp & "6"
                                    End If
                                    If Mid(AirCodes, 6, 1) = "1" Then
                                        temp = temp & "7"
                                    End If
                                    If Mid(AirCodes, 7, 1) = "1" Then
                                        temp = temp & "8"
                                    End If
                                    If Mid(AirCodes, 8, 1) = "1" Then
                                        temp = temp & "9"
                                    End If
                                    If Mid(AirCodes, 9, 1) = "1" Then
                                        temp = temp & "F"
                                    End If
                                    If Mid(AirCodes, 10, 1) = "1" Then
                                        temp = temp & "A"
                                    End If
                                    If Mid(AirCodes, 11, 1) = "1" Then
                                        temp = temp & "I"
                                    End If
                                    If Mid(AirCodes, 12, 1) = "1" Then
                                        temp = temp & "M"
                                    End If
                                    If Mid(AirCodes, 13, 1) = "1" Then
                                        temp = temp & "V"
                                    End If
                                    row.Item("IAIP - Air Code") = temp

                                    row.Item("IAIP - Action Type") = dr.Item("EventType")
                                    temp = dr.Item("AchievedDate")
                                    Select Case Mid(temp, 4, 3)
                                        Case "JAN"
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                        Case "FEB"
                                            temp = Mid(temp, 1, 2) & "02" & Mid(temp, 8, 2)
                                        Case "MAR"
                                            temp = Mid(temp, 1, 2) & "03" & Mid(temp, 8, 2)
                                        Case "APR"
                                            temp = Mid(temp, 1, 2) & "04" & Mid(temp, 8, 2)
                                        Case "MAY"
                                            temp = Mid(temp, 1, 2) & "05" & Mid(temp, 8, 2)
                                        Case "JUN"
                                            temp = Mid(temp, 1, 2) & "06" & Mid(temp, 8, 2)
                                        Case "JUL"
                                            temp = Mid(temp, 1, 2) & "07" & Mid(temp, 8, 2)
                                        Case "AUG"
                                            temp = Mid(temp, 1, 2) & "08" & Mid(temp, 8, 2)
                                        Case "SEP"
                                            temp = Mid(temp, 1, 2) & "09" & Mid(temp, 8, 2)
                                        Case "OCT"
                                            temp = Mid(temp, 1, 2) & "10" & Mid(temp, 8, 2)
                                        Case "NOV"
                                            temp = Mid(temp, 1, 2) & "11" & Mid(temp, 8, 2)
                                        Case "DEC"
                                            temp = Mid(temp, 1, 2) & "12" & Mid(temp, 8, 2)
                                        Case Else
                                            temp = Mid(temp, 1, 2) & "01" & Mid(temp, 8, 2)
                                    End Select
                                    row.Item("IAIP - Date Achieved") = temp
                                End If
                                dr.Close()
                                ACCDue.Rows.Add(row)

                            Case "33", "34", "35", "36"  'State Permit

                            Case "01", "03", "04", "40", "56", "57", "60", "64", "74", _
                                    "A8", "AS", "AW", "AZ", "E5", "VZ", "X1", "Z1", "Z4", _
                                       "Z8", "OT"  'Enforcement Types

                            Case "PP", "EO", "EM", "ES", "PS", "PX"    'PCE 

                            Case "00" 'Other

                            Case Else

                        End Select

                    Case "164" 'HPV Enforcement 


                    Case "171"  'Comments 



                    Case "181" 'Compliance Monitoring Staergy
                        row = CMS181.NewRow()
                        row.Item("AFS - AIRS") = Mid(worktemp, 1, 10)
                        row.Item("AFS - Action") = "181"
                        row.Item("AFS - CMS") = Mid(worktemp, 14, 1)
                        row.Item("AFS - CMS Years") = Mid(worktemp, 15, 1)
                        temp = Mid(worktemp, 3, 8)
                        SQL = "select " & _
                        "strCMSMember, " & _
                        "case " & _
                        "  when strcmsmember = 'A' then '2' " & _
                        "  when strcmsmember = 'S' then '5' " & _
                        "end CMSYear  " & _
                        "from " & DBNameSpace & ".apbsupplamentaldata " & _
                        "where strairsnumber = '0413" & temp & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            row.Item("IAIP - AIRS") = "13" & temp
                            row.Item("IAIP - Action") = "181"
                            row.Item("IAIP - CMS") = dr.Item("strCMSMember")
                            row.Item("IAIP - CMS Years") = dr.Item("CMSYear")
                        End If
                        dr.Close()
                        CMS181.Rows.Add(row)



                    Case Else
                        'Make code to caputer text that wasn't matched
                End Select
                txtCompleteFile.Text = Replace(txtCompleteFile.Text, worktemp, "")

            Loop

            dsValidator = New DataSet
            dsValidator.Tables.Add(FacilityName)
            dsValidator.Tables.Add(Address102)
            dsValidator.Tables.Add(Address103)
            dsValidator.Tables.Add(Contact105)
            dsValidator.Tables.Add(CMS181)
            dsValidator.Tables.Add(AirProgram)
            dsValidator.Tables.Add(Pollutants)
            dsValidator.Tables.Add(SubPart)
            dsValidator.Tables.Add(FCEAction)
            dsValidator.Tables.Add(StackTest)
            dsValidator.Tables.Add(Reports)
            dsValidator.Tables.Add(Inspections)
            dsValidator.Tables.Add(ACCReviewed)
            dsValidator.Tables.Add(ACCDue)


            dgvFacilityName.DataSource = dsValidator
            dgvFacilityName.DataMember = "FacilityName"
            dgvAddressData.DataSource = dsValidator
            dgvAddressData.DataMember = "Address102"
            dgvAddressData2.DataSource = dsValidator
            dgvAddressData2.DataMember = "Address103"
            dgvContactData.DataSource = dsValidator
            dgvContactData.DataMember = "Contact105"
            dgvCMS.DataSource = dsValidator
            dgvCMS.DataMember = "CMS181"
            dgvAirProgramData.DataSource = dsValidator
            dgvAirProgramData.DataMember = "AirProgram"
            dgvAirPollutantData.DataSource = dsValidator
            dgvAirPollutantData.DataMember = "Pollutants"
            dgvSubPartData.DataSource = dsValidator
            dgvSubPartData.DataMember = "SubPart"
            dgvFCEAction.DataSource = dsValidator
            dgvFCEAction.DataMember = "FCEAction"
            dgvActionsISMP.DataSource = dsValidator
            dgvActionsISMP.DataMember = "StackTest"
            dgvReports.DataSource = dsValidator
            dgvReports.DataMember = "Reports"
            dgvInspections.DataSource = dsValidator
            dgvInspections.DataMember = "Inspections"
            dgvACCs.DataSource = dsValidator
            dgvACCs.DataMember = "ACCReviewed"
            dgvACCsDue.DataSource = dsValidator
            dgvACCsDue.DataMember = "ACCDue"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnLoadData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadData.Click
        Try
            LoadFacilityName()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            dsValidator = New DataSet
            dgvFacilityName.DataSource = dsValidator
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        Try
            Help.ShowHelp(Label1, HELP_URL)
        Catch ex As Exception
        End Try
    End Sub
End Class