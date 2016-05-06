Imports System.Data.SqlClient

Namespace DAL
    Module NavigationScreenData

        ''' <summary>
        ''' Enumeration of the various work list types (contexts) available on the main Navigation Screen
        ''' </summary>
        ''' <remarks>"None" may not be useful but is included just in case</remarks>
        Public Enum WorkViewerType
            None
            ComplianceFacilitiesAssigned_Staff
            ComplianceFacilitiesAssigned_Program
            ComplianceWork_PM
            ComplianceWork_Staff
            ComplianceWork_UC_ProgCoord
            ComplianceWork_UC
            DelinquentFCEs
            Enforcement_Staff
            Enforcement_UC
            Enforcement_UC_ProgCoord
            Enforcement_PM
            FacilitiesWithSubparts
            FacilitiesMissingSubparts
            MonitoringTestReports_Staff
            MonitoringTestReports_UC
            MonitoringTestReports_PM
            MonitoringTestNotifications
            PermitApplications_Staff
            PermitApplications_UC
            PermitApplications_PM
            ISMP_PM
            ISMP_UC
            ISMP_Staff
            SSCP_PM
            SSCP_UC
            SSCP_DistrictLiaison
            SSCP_Staff
            SSPP_PM
            SSPP_UC
            SSPP_Administrative
            SSPP_Staff
            ProgCoord_PM
            ProgCoord_UC
            ProgCoord_DistrictLiaison
            ProgCoord_Staff
            SBEAP_Staff
            SBEAP_Program
        End Enum

        ''' <summary>
        ''' Returns a table of current work items of various types based on context
        ''' </summary>
        ''' <param name="workViewerList">The type of work item list desired</param>
        ''' <param name="parameterValue">An optional parameter possibly required for the selected work type</param>
        ''' <returns>A DataTable of current work items</returns>
        ''' <remarks>The DataTable schema and formatting are handled by IAIPNavigation's FormatWorkViewer procedure</remarks>
        Public Function GetWorkViewerListAsDataTable(ByVal workViewerList As WorkViewerType, Optional ByVal parameterValue As String = "") As DataTable
            Dim query As String = GetWorkViewerSQL(workViewerList)
            If query Is Nothing Or query = "" Then Return Nothing

            Dim parameter As New SqlParameter("pId", parameterValue)

            Return DB.GetDataTable(query, parameter)
        End Function

        ''' <summary>
        ''' Returns the SQL required by GetWorkViewerListAsDataTable based on context
        ''' </summary>
        ''' <param name="workViewerList">The type of work item list desired</param>
        ''' <returns>SQL as String</returns>
        Private Function GetWorkViewerSQL(ByVal workViewerList As WorkViewerType) As String
            ' TODO DWW: Review SQL on case-by-case basis for efficiency (maybe move some to Oracle Views)
            ' Can use EQATEC monitoring to prioritize


            Dim SQL As String = "" ' Default value

            Select Case workViewerList

                Case WorkViewerType.ISMP_PM
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " &
                    "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " &
                    "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " &
                      "AIRBranch.ISMPReportInformation.strReferenceNumber  " &
                    "and  Status = 'Open' "

                Case WorkViewerType.ISMP_UC
                    ' Requires :pId = UserUnit
                    SQL = "select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " &
                        "from AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " &
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " &
                         "AIRBranch.ISMPReportInformation.strReferenceNumber  " &
                        "and status = 'Open' " &
                        "and strUserUnit = " &
                        "(select strUnitDesc from AIRBranch.LookUpEPDUnits where numUnitCode = :pId) "

                Case WorkViewerType.ISMP_Staff
                    ' Requires :pId = ReviewingEngineer
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " &
                    "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " &
                    "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " &
                     "AIRBranch.ISMPReportInformation.strReferenceNumber  " &
                    "and Status = 'Open' " &
                    "and ReviewingEngineer = :pId"

                Case WorkViewerType.SSCP_PM
                    SQL = "Select " &
                        "distinct(to_number(AIRBranch.sscp_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber,  " &
                        "substr(AIRBranch.sscp_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " &
                        "case   " &
                        "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " &
                        "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " &
                        "when strStatus = 'UC' then '2 - Submitted to UC'   " &
                        "When strStatus Is Null then '1 - At Staff'   " &
                        "else 'Unknown'   " &
                        "end as EnforcementStatus,   " &
                        "Case     " &
                        "when datDiscoveryDate is Null then ''    " &
                        "else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " &
                        "END as Violationdate,     " &
                        "strActionType as HPVStatus,    " &
                        "Case    " &
                        "when datEnforcementFinalized Is Not NULL then 'Closed'    " &
                        "when datEnforcementFinalized is NUll then 'Open'    " &
                        "Else 'Open'    " &
                        "End as Status,    " &
                        "strFacilityName,    " &
                        "(strLastName||', '||strFirstName) as Staff     " &
                        "from AIRBranch.sscp_AuditedEnforcement,     " &
                        "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,    " &
                        "(select numUserID  " &
                        "from AIRBranch.EPDUserProfiles where numUnit is null) UnitStaff " &
                        "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.sscp_AuditedEnforcement.strAIRSNumber    " &
                        "and (strStatus IS Null or strStatus = 'UC')    " &
                        "and datEnforcementFinalized is NULL   " &
                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.sscp_AuditedEnforcement.numStaffResponsible    " &
                        "order by strENforcementNumber DESC   "

                Case WorkViewerType.SSCP_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select to_number(AIRBranch.SSCP_aUDITEDEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                         "substr(AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber, 5) as AIRSNumber,   " &
                         "case   " &
                         "    when datEnforcementFinalized is Not Null then '4 - Closed Out'   " &
                         "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " &
                         "    when strStatus = 'UC' then '2 - Submitted to UC'   " &
                         "    When strStatus Is Null then '1 - At Staff'   " &
                         "   else 'Unknown'   " &
                         "end as EnforcementStatus, " &
                        " Case    " &
                        " 	when datDiscoveryDate is Null then ''   " &
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " &
                         "END as Violationdate,    " &
                         "strActionType as HPVStatus,   " &
                         "Case   " &
                         " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " &
                         "	when datEnforcementFinalized is NUll then 'Open'   " &
                         "Else 'Open'   " &
                         "End as Status,   " &
                         "strFacilityName,   " &
                         "(strLastName||', '||strFirstName) as Staff   " &
                         "from AIRBranch.SSCP_aUDITEDEnforcement,    " &
                         "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,   " &
                         "( select numUserID from AIRBranch.EPDUserProfiles where numUnit = :pId " &
                         "group by numUserID ) UnitStaff   " &
                         "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber   " &
                         "and (strStatus IS Null or strStatus = 'UC')   " &
                         "and numStaffResponsible = UnitStaff.numUserID   " &
                         "and datEnforcementFinalized is NULL   " &
                         "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_aUDITEDEnforcement.numStaffResponsible   " &
                         "order by strENforcementNumber DESC  "

                Case WorkViewerType.SSCP_DistrictLiaison
                    SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                        "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " &
                        "case   " &
                        "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " &
                        "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " &
                        "when strStatus = 'UC' then '2 - Submitted to UC'   " &
                        "When strStatus Is Null then '1 - At Staff'   " &
                        "   else 'Unknown'   " &
                        "end as EnforcementStatus, " &
                        "Case    " &
                        " 	when datDiscoveryDate is Null then ''   " &
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " &
                        "END as Violationdate,    " &
                        "strActionType as HPVStatus,   " &
                        "Case   " &
                        " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " &
                        "	when datEnforcementFinalized is NUll then 'Open'   " &
                        "Else 'Open'   " &
                        "End as Status,   " &
                        "strFacilityName,   " &
                        "(strLastName||', '||strFirstName) as Staff   " &
                        "from AIRBranch.SSCP_AuditedEnforcement,  " &
                        "AIRBranch.APBFacilityInformation, AIRBranch.EPDUSerProfiles,   " &
                        "(select numuserId  " &
                        "from AIRBranch.EPDUserProfiles  " &
                        "where strLastName = 'District' or (numBranch = '1' and numProgram = '4' and numUnit is null )  " &
                        "group by numUserID) UnitStaff   " &
                        "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber   " &
                        "and (strStatus IS Null or strStatus = 'UC')   " &
                        "and numStaffResponsible = UnitStaff.numUserID   " &
                        "and datEnforcementFinalized is NULL   " &
                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_AuditedEnforcement.numStaffResponsible   " &
                        "order by strENforcementNumber DESC   "

                Case WorkViewerType.SSCP_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                     "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " &
                     "case  " &
                     "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " &
                     "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " &
                     "when strStatus = 'UC' then '2 - Submitted to UC'  " &
                     "When strStatus Is Null then '1 - At Staff'  " &
                     "else 'Unknown'  " &
                     "end as EnforcementStatus,  " &
                     "Case   " &
                     " 	when datDiscoveryDate is Null then ''  " &
                     "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " &
                     "END as Violationdate,   " &
                     "strActionType as HPVStatus,   " &
                     "Case  " &
                     " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " &
                     " 	when datEnforcementFinalized is NUll then 'Open'  " &
                     "Else 'Open'  " &
                     "End as Status,  " &
                     "AIRBranch.APBFacilityInformation.strFacilityName,  " &
                     "(strLastName||', '||strFirstName) as Staff  " &
                     "from AIRBranch.SSCP_AuditedEnforcement,   " &
                     "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " &
                     "AIRBranch.VW_SSCPINSPECTION_LIST " &
                     "Where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber  " &
                     "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = '0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " &
                     "and (numStaffResponsible = :pId or numSSCPEngineer = :pId)  " &
                     "and (strStatus IS Null or strStatus = 'UC')  " &
                     "and datEnforcementFinalized is Null  " &
                     "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " &
                     "order by strENforcementNumber DESC  "

                Case WorkViewerType.SSPP_PM
                    SQL = "Select " &
                      "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " &
                      "case " &
                      " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " &
                      "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " &
                      "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " &
                      "end as strAIRSNumber, " &
                      "case " &
                      "	when strApplicationTypeDesc IS Null then ' ' " &
                      "Else strApplicationTypeDesc " &
                      "End as strApplicationType, " &
                      "case " &
                      " 	when datReceivedDate is Null then ' ' " &
                      "Else to_char(datReceivedDate, 'RRRR-MM-dd') " &
                      " End as datReceivedDate, " &
                      "case  " &
                      "when strPermitNumber is NULL then ' '  " &
                      "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " &
                      " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " &
                      " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " &
                      "end As strPermitNumber, " &
                      "case " &
                      " 	when numUserID= '0' then ' ' " &
                      "	when numUserID is Null then ' ' " &
                      "else (strLastName||', '||strFirstName) " &
                      "end as StaffResponsible, " &
                      "case  " &
                      "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " &
                      "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " &
                      "when datToDirector is Not Null and datFinalizedDate is Null " &
                      "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " &
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " &
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " &
                      "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " &
                      "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " &
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " &
                      "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " &
                      "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " &
                      "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " &
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " &
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " &
                      "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " &
                      "end as StatusDate,  " &
                      "case  " &
                      " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " &
                      "else AIRBranch.SSPPApplicationData.strFacilityName  " &
                      "end as strFacilityName,  " &
                      "case " &
                      "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " &
                      "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " &
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " &
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " &
                      "when datEPAEnds is not Null then '08 - EPA 45-day Review' " &
                      "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " &
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " &
                      "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " &
                      "when dattoPMII is Not Null then '04 - AT PM'  " &
                      "when dattoPMI is Not Null then '03 - At UC'  " &
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " &
                      "then '02 - Internal Review' " &
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " &
                      "else '01 - At Engineer'  " &
                      "end as AppStatus, " &
                      "case " &
                      " 	when strPermitTypeDescription is Null then '' " &
                      "else strPermitTypeDescription " &
                      "End as strPermitType " &
                      "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " &
                      "AIRBranch.SSPPApplicationData, " &
                      "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " &
                      "AIRBranch.EPDuserProfiles  " &
                      "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " &
                      "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " &
                      "and strApplicationType = strApplicationTypeCode (+) " &
                      "and strPermitType = strPermitTypeCode (+) " &
                      "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " &
                      "and datFinalizedDate is NULL " &
                      "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                Case WorkViewerType.SSPP_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select " &
                         "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,  " &
                         "case  " &
                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' '  " &
                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' '  " &
                         "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5)  " &
                         "end as strAIRSNumber,  " &
                         "case  " &
                         "	when strApplicationTypeDesc IS Null then ' '  " &
                         "Else strApplicationTypeDesc  " &
                         "End as strApplicationType,  " &
                         "case  " &
                         "	when datReceivedDate is Null then ' '  " &
                         "Else to_char(datReceivedDate, 'RRRR-MM-dd')  " &
                         "End as datReceivedDate,  " &
                         "case   " &
                         "  when strPermitNumber is NULL then ' '   " &
                         "   else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'   " &
                         " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'  " &
                         " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)  " &
                         "end As strPermitNumber,  " &
                         "case  " &
                         "	when numUserID = '0' then ' '  " &
                         "	when numUserID is Null then ' '  " &
                         "else (strLastName||', '||strFirstName)  " &
                         "end as StaffResponsible,  " &
                         "case   " &
                         "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')     " &
                         "when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')  " &
                         "when datToDirector is Not Null and datFinalizedDate is Null  " &
                         "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')  " &
                         "when datToBranchCheif is Not Null and datFinalizedDate is Null  " &
                         "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')   " &
                         "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')    " &
                         "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')    " &
                         "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " &
                         "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')     " &
                         "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')     " &
                         "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')     " &
                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')    " &
                         "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'     " &
                         "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')     " &
                         "end as StatusDate,   " &
                         "case   " &
                         "	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '   " &
                         "else AIRBranch.SSPPApplicationData.strFacilityName   " &
                         "end as strFacilityName,   " &
                         "case  " &
                         "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'  " &
                         "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'  " &
                         "when datToBranchCheif is Not Null and datFinalizedDate is Null  " &
                         "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'  " &
                         "when datEPAEnds is not Null then '08 - EPA 45-day Review'  " &
                         "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'  " &
                         "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'   " &
                         "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'   " &
                         "when dattoPMII is Not Null then '04 - AT PM'   " &
                         "when dattoPMI is Not Null then '03 - At UC'   " &
                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'  " &
                         "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'    " &
                         "else '01 - At Engineer'   " &
                         "end as AppStatus,  " &
                         "case  " &
                         "	when strPermitTypeDescription is Null then ''  " &
                         "else strPermitTypeDescription  " &
                         "End as strPermitType  " &
                         "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking,  " &
                         "AIRBranch.SSPPApplicationData,  " &
                         "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes,  " &
                         "AIRBranch.EPDUserProfiles " &
                         "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)   " &
                         "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+)  " &
                         "and strApplicationType = strApplicationTypeCode (+)  " &
                         "and strPermitType = strPermitTypeCode (+)  " &
                         "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible  " &
                         "and datFinalizedDate is NULL  " &
                         "and (AIRBranch.EPDUserProfiles.numUnit = :pId   " &
                         "or (APBUnit = :pId ))  "

                Case WorkViewerType.SSPP_Administrative
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " &
                        "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,   " &
                        "case   " &
                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' '   " &
                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' '   " &
                        " else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5)   " &
                        " end as strAIRSNumber,   " &
                        " case   " &
                        " 	when strApplicationTypeDesc IS Null then ' '   " &
                        "Else strApplicationTypeDesc   " &
                        "End as strApplicationType,   " &
                        "case   " &
                        "	when datReceivedDate is Null then ' '   " &
                        "Else to_char(datReceivedDate, 'RRRR-MM-dd')   " &
                        "End as datReceivedDate,   " &
                        "case    " &
                        " when strPermitNumber is NULL then ' '    " &
                        " else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'    " &
                        "   ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'   " &
                        "   ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)   " &
                        "end As strPermitNumber,   " &
                        "case   " &
                        "	when numUserID = '0' then ' '   " &
                        "	when numUserID is Null then ' '   " &
                        "else (strLastName||', '||strFirstName)   " &
                        "end as StaffResponsible,   " &
                        "case    " &
                        "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')      " &
                        "when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')   " &
                        "when datToDirector is Not Null and datFinalizedDate is Null   " &
                        "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')   " &
                        "when datToBranchCheif is Not Null and datFinalizedDate is Null   " &
                     "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')    " &
                        "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')     " &
                         "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " &
                         "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')      " &
                         "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')      " &
                         "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')      " &
                         "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')      " &
                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')     " &
                         "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'      " &
                         "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')      " &
                         "end as StatusDate,    " &
                         "case    " &
                         " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '    " &
                         "else AIRBranch.SSPPApplicationData.strFacilityName    " &
                         "end as strFacilityName,    " &
                         "case   " &
                         "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'   " &
                  "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'   " &
                         "when datToBranchCheif is Not Null and datFinalizedDate is Null   " &
                         "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'   " &
                         "when datEPAEnds is not Null then '08 - EPA 45-day Review'   " &
                         "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'   " &
                         "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'    " &
                         "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'    " &
                         "when dattoPMII is Not Null then '04 - AT PM'    " &
                         "when dattoPMI is Not Null then '03 - At UC'    " &
                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'   " &
                         "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'     " &
                         "else '01 - At Engineer'    " &
                         "end as AppStatus,   " &
                         "case   " &
                          "when strPermitTypeDescription is Null then ''   " &
                         "else strPermitTypeDescription   " &
                         "End as strPermitType   " &
                         "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking,   " &
                         "AIRBranch.SSPPApplicationData,   " &
                         "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes,   " &
                         "AIRBranch.EPDUserProfiles    " &
                         "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)    " &
                         "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+)   " &
                         "and strApplicationType = strApplicationTypeCode (+)   " &
                         "and strPermitType = strPermitTypeCode (+)   " &
                         "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " &
                        "and datFinalizedDate is NULL   " &
                         "and numUserID = :pId  "

                Case WorkViewerType.SSPP_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " &
                    "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,   " &
                    "case   " &
                    " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' '   " &
                    " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' '   " &
                    " else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5)   " &
                    "end as strAIRSNumber,   " &
                    "   case   " &
                    " 	when strApplicationTypeDesc IS Null then ' '   " &
                    "Else strApplicationTypeDesc   " &
                    "End as strApplicationType,   " &
                    "case   " &
                    " 	when datReceivedDate is Null then ' '   " &
                    "Else to_char(datReceivedDate, 'RRRR-MM-dd')   " &
                    " End as datReceivedDate,   " &
                    " case    " &
                    " when strPermitNumber is NULL then ' '    " &
                    "  else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'    " &
                    " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'   " &
                    " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)   " &
                    "end As strPermitNumber,   " &
                    "case   " &
                    " 	when numUserID = '0' then ' '   " &
                    " 	when numUserID is Null then ' '   " &
                    "else (strLastName||', '||strFirstName)   " &
                    "end as StaffResponsible,   " &
                    "case    " &
                    "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')      " &
                    "when datFinalizeddate is Not Null then to_char(datFinalizeddate, 'RRRR-MM-dd')   " &
                    "when datToDirector is Not Null and datFinalizedDate is Null and   " &
                    "(datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')   " &
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null and   " &
                    "datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')    " &
                    "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')     " &
                    "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " &
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')      " &
                    "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')      " &
                    "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')      " &
                    "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')      " &
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')     " &
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'      " &
                    "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')      " &
                    "end as StatusDate,    " &
                    "case    " &
                    "	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '    " &
                    "else AIRBranch.SSPPApplicationData.strFacilityName    " &
                    "end as strFacilityName,    " &
                    "case   " &
                    "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'   " &
             "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'   " &
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null   " &
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'   " &
                    "when datEPAEnds is not Null then '08 - EPA 45-day Review'   " &
                    "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'   " &
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'    " &
                    "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'    " &
                    "when dattoPMII is Not Null then '04 - AT PM'    " &
                    "when dattoPMI is Not Null then '03 - At UC'    " &
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'   " &
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'     " &
                    "else '01 - At Engineer'    " &
                    "end as AppStatus,   " &
                    "case   " &
                    " 	when strPermitTypeDescription is Null then ''   " &
                    "else strPermitTypeDescription   " &
                    "End as strPermitType   " &
                    "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking,   " &
                    "AIRBranch.SSPPApplicationData,   " &
                    "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes,   " &
                    "AIRBranch.EPDUserProfiles  " &
             "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)    " &
             "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+)   " &
                    "and strApplicationType = strApplicationTypeCode (+)   " &
                    "and strPermitType = strPermitTypeCode (+)   " &
                    "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " &
                    "and datFinalizedDate is NULL   " &
                    "and numUserID = :pId "

                Case WorkViewerType.ProgCoord_PM
                    SQL = "Select " &
                    "distinct(to_number(AIRBranch.sscp_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber,  " &
                    "substr(AIRBranch.sscp_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " &
                    "case   " &
                    "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " &
                    "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " &
                    "when strStatus = 'UC' then '2 - Submitted to UC'   " &
                    "When strStatus Is Null then '1 - At Staff'   " &
                    "else 'Unknown'   " &
                    "end as EnforcementStatus,   " &
                    "Case     " &
                    "when datDiscoveryDate is Null then ''    " &
                    "else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " &
                    "END as Violationdate,     " &
                    "strActionType as HPVStatus,    " &
                    "Case    " &
                    "when datEnforcementFinalized Is Not NULL then 'Closed'    " &
                    "when datEnforcementFinalized is NUll then 'Open'    " &
                    "Else 'Open'    " &
                    "End as Status,    " &
                    "strFacilityName,    " &
                    "(strLastName||', '||strFirstName) as Staff     " &
                    "from AIRBranch.sscp_AuditedEnforcement,     " &
                    "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,    " &
                    "(select numUserID  " &
                    "from AIRBranch.EPDUserProfiles where numUnit is null) UnitStaff " &
                    "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.sscp_AuditedEnforcement.strAIRSNumber    " &
                    "and (strStatus IS Null or strStatus = 'UC')    " &
                    "and datEnforcementFinalized is NULL   " &
                    "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.sscp_AuditedEnforcement.numStaffResponsible    " &
                    "order by strENforcementNumber DESC   "

                Case WorkViewerType.ProgCoord_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select to_number(AIRBranch.SSCP_aUDITEDEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                         "substr(AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber, 5) as AIRSNumber,   " &
                         "case   " &
                         "    when datEnforcementFinalized is Not Null then '4 - Closed Out'   " &
                         "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " &
                         "    when strStatus = 'UC' then '2 - Submitted to UC'   " &
                         "    When strStatus Is Null then '1 - At Staff'   " &
                         "   else 'Unknown'   " &
                         "end as EnforcementStatus, " &
                        " Case    " &
                        " 	when datDiscoveryDate is Null then ''   " &
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " &
                         "END as Violationdate,    " &
                         "strActionType as HPVStatus,   " &
                         "Case   " &
                         " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " &
                         "	when datEnforcementFinalized is NUll then 'Open'   " &
                         "Else 'Open'   " &
                         "End as Status,   " &
                         "strFacilityName,   " &
                         "(strLastName||', '||strFirstName) as Staff   " &
                         "from AIRBranch.SSCP_aUDITEDEnforcement,    " &
                         "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,   " &
                         "( select numUserID from AIRBranch.EPDUserProfiles where numUnit = :pId  " &
                         "group by numUserID ) UnitStaff   " &
                         "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber   " &
                         "and (strStatus IS Null or strStatus = 'UC')   " &
                         "and numStaffResponsible = UnitStaff.numUserID   " &
                         "and datEnforcementFinalized is NULL   " &
                         "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_aUDITEDEnforcement.numStaffResponsible   " &
                         "order by strENforcementNumber DESC  "

                Case WorkViewerType.ProgCoord_DistrictLiaison
                    SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                        "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " &
                        "case   " &
                        "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " &
                        "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " &
                        "when strStatus = 'UC' then '2 - Submitted to UC'   " &
                        "When strStatus Is Null then '1 - At Staff'   " &
                        "   else 'Unknown'   " &
                        "end as EnforcementStatus, " &
                        "Case    " &
                        " 	when datDiscoveryDate is Null then ''   " &
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " &
                        "END as Violationdate,    " &
                        "strActionType as HPVStatus,   " &
                        "Case   " &
                        " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " &
                        "	when datEnforcementFinalized is NUll then 'Open'   " &
                        "Else 'Open'   " &
                        "End as Status,   " &
                        "strFacilityName,   " &
                        "(strLastName||', '||strFirstName) as Staff   " &
                        "from AIRBranch.SSCP_AuditedEnforcement,  " &
                        "AIRBranch.APBFacilityInformation, AIRBranch.EPDUSerProfiles,   " &
                        "(select numuserId  " &
                        "from AIRBranch.EPDUserProfiles  " &
                        "where strLastName = 'District' or (numBranch = '1' and numProgram = '4' and numUnit is null )  " &
                        "group by numUserID) UnitStaff   " &
                        "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber   " &
                        "and (strStatus IS Null or strStatus = 'UC')   " &
                        "and numStaffResponsible = UnitStaff.numUserID   " &
                        "and datEnforcementFinalized is NULL   " &
                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_AuditedEnforcement.numStaffResponsible   " &
                        "order by strENforcementNumber DESC   "

                Case WorkViewerType.ProgCoord_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                     "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " &
                     "case  " &
                     "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " &
                     "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " &
                     "when strStatus = 'UC' then '2 - Submitted to UC'  " &
                     "When strStatus Is Null then '1 - At Staff'  " &
                     "else 'Unknown'  " &
                     "end as EnforcementStatus,  " &
                     "Case   " &
                     " 	when datDiscoveryDate is Null then ''  " &
                     "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " &
                     "END as Violationdate,   " &
                     "strActionType as HPVStatus,   " &
                     "Case  " &
                     " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " &
                     " 	when datEnforcementFinalized is NUll then 'Open'  " &
                     "Else 'Open'  " &
                     "End as Status,  " &
                     "AIRBranch.APBFacilityInformation.strFacilityName,  " &
                     "(strLastName||', '||strFirstName) as Staff  " &
                     "from AIRBranch.SSCP_AuditedEnforcement,   " &
                     "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " &
                     "AIRBranch.VW_SSCPINSPECTION_LIST " &
                     "Where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber  " &
                     "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = '0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " &
                     "and (numStaffResponsible = :pId or numSSCPEngineer = :pId)  " &
                     "and (strStatus IS Null or strStatus = 'UC')  " &
                     "and datEnforcementFinalized is Null  " &
                     "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " &
                     "order by strENforcementNumber DESC  "

                Case WorkViewerType.ComplianceFacilitiesAssigned_Program
                    ' Requires :pId = UserProgram
                    SQL = "Select distinct " &
                   "substr(AIRBranch.SSCPInspectionsRequired.strAIRSnumber, 5) as AIRSNumber, " &
                   "AIRBranch.APBFacilityInformation.strFacilityName, " &
                   "(strLastName||', '||strFirstName) as Staff " &
                   "from AIRBranch.SSCPInspectionsRequired, AIRBranch.APBFacilityInformation, " &
                   "AIRBranch.EPDUserProfiles " &
                   "where AIRBranch.SSCPInspectionsRequired.strAIRSNumber = AIRBranch.APBFacilityInformation.strAIRSNumber " &
                   "and AIRBranch.SSCPInspectionsRequired.numSSCPEngineer = AIRBranch.EPDUserProfiles.numUserID " &
                   "and numProgram = :pId " &
                   "order by AIRSNumber  "

                Case WorkViewerType.ComplianceFacilitiesAssigned_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select distinct " &
                   "substr(AIRBranch.SSCPInspectionsRequired.strAIRSnumber, 5) as AIRSNumber, " &
                   "AIRBranch.APBFacilityInformation.strFacilityName, " &
                   "(strLastName||', '||strFirstName) as Staff " &
                   "from AIRBranch.SSCPInspectionsRequired, AIRBranch.APBFacilityInformation, " &
                   "AIRBranch.EPDUserProfiles " &
                   "where AIRBranch.SSCPInspectionsRequired.strAIRSNumber = AIRBranch.APBFacilityInformation.strAIRSNumber " &
                   "and AIRBranch.SSCPInspectionsRequired.numSSCPEngineer = AIRBranch.EPDUserProfiles.numUserID " &
                   "and numSSCPEngineer = :pId " &
                   "order by AIRSNumber  "

                Case WorkViewerType.ComplianceWork_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " &
                        "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " &
                        "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " &
                        "(strLastName||', '||strFirstName) as Staff,  " &
                        "strResponsibleStaff, " &
                        "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived, " &
                        "AIRBranch.APBFacilityInformation.strFacilityName, StrActivityName    " &
                        "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " &
                        "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,   " &
                        "AIRBranch.VW_SSCPInspection_List " &
                        "where AIRBranch.EPDUserProfiles.numUserID = " &
                        "AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " &
                        "and AIRBranch.APBFacilityInformation.strAIRSNumber = " &
                        "AIRBRANCH.SSCPItemMaster.strAIRSNumber  " &
                        "and AIRBranch.LookUPComplianceActivities.strActivityType = " &
                        "AIRBRANCH.SSCPItemMaster.strEventType  " &
                        " and AIRBranch.SSCPItemMaster.strAIRSnumber = '0413'||" &
                        "AIRBRANCH.VW_SSCPInspection_List.AIRSNumber  " &
                        "and (strResponsibleStaff = :pId or numSSCPEngineer = :pId) " &
                        "and DatCompleteDate is Null  " &
                        "and strDelete is Null "

                Case WorkViewerType.ComplianceWork_UC_ProgCoord
                    ' Requires :pId = UserProgram
                    SQL = "select " &
                            "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " &
                            "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " &
                            "(strLastName||', '||strFirstName) as Staff,  " &
                            "strResponsibleStaff, " &
                            "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " &
                            "strFacilityName, StrActivityName    " &
                            "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " &
                            "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,  " &
                            "(select numUserID from AIRBranch.EPDUserProfiles where numProgram = :pId)  " &
                            "UnitStaff    " &
                            "where AIRBranch.EPDUserProfiles.numUserID = " &
                            "AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " &
                            "and AIRBranch.APBFacilityInformation.strAIRSNumber = " &
                            "AIRBRANCH.SSCPItemMaster.strAIRSNumber  " &
                            "and AIRBranch.LookUPComplianceActivities.strActivityType = " &
                            "AIRBRANCH.SSCPItemMaster.strEventType " &
                            "and DatCompleteDate is Null   " &
                            "and strResponsibleStaff = UnitStaff.numUserID " &
                            "and strDelete is Null "

                Case WorkViewerType.ComplianceWork_UC
                    ' Requires :pId = UserUnit
                    SQL = "select " &
                     "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " &
                     "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " &
                     "(strLastName||', '||strFirstName) as Staff,  " &
                     "strResponsibleStaff, " &
                     "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " &
                     "strFacilityName, StrActivityName    " &
                     "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " &
                     "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,  " &
                     "(select numUserID from AIRBranch.EPDUserProfiles where numUnit = :pId)  " &
                     "UnitStaff    " &
                     "where AIRBranch.EPDUserProfiles.numUserID = " &
                     "AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " &
                     "and AIRBranch.APBFacilityInformation.strAIRSNumber = " &
                     "AIRBRANCH.SSCPItemMaster.strAIRSNumber  " &
                     "and AIRBranch.LookUPComplianceActivities.strActivityType = " &
                     "AIRBRANCH.SSCPItemMaster.strEventType " &
                     "and DatCompleteDate is Null   " &
                     "and strResponsibleStaff = UnitStaff.numUserID " &
                     "and strDelete is Null "

                Case WorkViewerType.ComplianceWork_PM
                    SQL = "select " &
                       "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " &
                       "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " &
                        " case when AIRBranch.SSCPItemMaster.STRRESPONSIBLESTAFF = 0 then ': No one assigned' " &
                        " when AIRBranch.SSCPItemMaster.STRRESPONSIBLESTAFF is null then ': Not assigned' " &
                        "Else STRLASTNAME || ', ' || STRFIRSTNAME end AS Staff, " &
                       "strResponsibleStaff, " &
                       "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " &
                       "strFacilityName, StrActivityName    " &
                       "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " &
                       "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities " &
                       "where AIRBranch.EPDUserProfiles.numUserID(+) = " &
                                "AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " &
                       "and AIRBranch.APBFacilityInformation.strAIRSNumber = " &
                       "AIRBRANCH.SSCPItemMaster.strAIRSNumber  " &
                       "and AIRBranch.LookUPComplianceActivities.strActivityType = " &
                       "AIRBRANCH.SSCPItemMaster.strEventType " &
                       "and DatCompleteDate is Null   " &
                       "and strDelete is Null "

                Case WorkViewerType.DelinquentFCEs
                    Dim StartCMSA As String = Format(CDate(OracleDate).AddDays(-730), "yyyy-MM-dd")
                    Dim StartCMSS As String = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")
                    SQL = "Select * " &
                    "from AIRBranch.VW_SSCP_CMSWarning " &
                    "where AIRSNumber is not Null " &
                    " and strCMSMember is not null " &
                    " and ((strCMSMember = 'A' and lastFCE < '" & StartCMSA & "') " &
                    "or (strCMSMember = 'S' and LastFCE < '" & StartCMSS & "')) "

                Case WorkViewerType.Enforcement_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " &
                      "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                      "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " &
                      "case  " &
                      "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " &
                      "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " &
                      "when strStatus = 'UC' then '2 - Submitted to UC'  " &
                      "When strStatus Is Null then '1 - At Staff'  " &
                      "else 'Unknown'  " &
                      "end as EnforcementStatus,  " &
                      "Case   " &
                      " 	when datDiscoveryDate is Null then ''  " &
                      "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " &
                      "END as Violationdate,   " &
                      "strActionType as HPVStatus,   " &
                      "Case  " &
                      " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " &
                      " 	when datEnforcementFinalized is NUll then 'Open'  " &
                      "Else 'Open'  " &
                      "End as Status,  " &
                      "AIRBranch.APBFacilityInformation.strFacilityName,  " &
                      "(strLastName||', '||strFirstName) as Staff  " &
                      "from AIRBranch.SSCP_AuditedEnforcement,   " &
                      "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " &
                      "AIRBranch.VW_SSCPINSPECTION_LIST " &
                      "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " &
                      "AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " &
                      "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " &
                      "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " &
                      "and (strStatus IS Null or strStatus = 'UC')  " &
                      "and datEnforcementFinalized is Null  " &
                      "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " &
                      "and (numStaffResponsible = :pId or numSSCPEngineer = :pId) " &
                    "order by strENforcementNumber DESC  "

                Case WorkViewerType.Enforcement_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select " &
                    "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                    "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " &
                    "case  " &
                    "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " &
                    "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " &
                    "when strStatus = 'UC' then '2 - Submitted to UC'  " &
                    "When strStatus Is Null then '1 - At Staff'  " &
                    "else 'Unknown'  " &
                    "end as EnforcementStatus,  " &
                    "Case   " &
                    " 	when datDiscoveryDate is Null then ''  " &
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " &
                    "END as Violationdate,   " &
                    "strActionType as HPVStatus,   " &
                    "Case  " &
                    " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " &
                    " 	when datEnforcementFinalized is NUll then 'Open'  " &
                    "Else 'Open'  " &
                    "End as Status,  " &
                    "AIRBranch.APBFacilityInformation.strFacilityName,  " &
                    "(strLastName||', '||strFirstName) as Staff  " &
                    "from AIRBranch.SSCP_AuditedEnforcement,   " &
                    "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " &
                    "AIRBranch.VW_SSCPINSPECTION_LIST " &
                    "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " &
                    "AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " &
                    "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " &
                    "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " &
                    "and (strStatus IS Null or strStatus = 'UC')  " &
                    "and datEnforcementFinalized is Null  " &
                    "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " &
                    " and numUnit = :pId " &
                    "order by strENforcementNumber DESC  "

                Case WorkViewerType.Enforcement_UC_ProgCoord
                    ' Requires :pId = UserProgram
                    SQL = "Select " &
                    "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                    "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " &
                    "case  " &
                    "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " &
                    "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " &
                    "when strStatus = 'UC' then '2 - Submitted to UC'  " &
                    "When strStatus Is Null then '1 - At Staff'  " &
                    "else 'Unknown'  " &
                    "end as EnforcementStatus,  " &
                    "Case   " &
                    " 	when datDiscoveryDate is Null then ''  " &
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " &
                    "END as Violationdate,   " &
                    "strActionType as HPVStatus,   " &
                    "Case  " &
                    " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " &
                    " 	when datEnforcementFinalized is NUll then 'Open'  " &
                    "Else 'Open'  " &
                    "End as Status,  " &
                    "AIRBranch.APBFacilityInformation.strFacilityName,  " &
                    "(strLastName||', '||strFirstName) as Staff  " &
                    "from AIRBranch.SSCP_AuditedEnforcement,   " &
                    "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " &
                    "AIRBranch.VW_SSCPINSPECTION_LIST " &
                    "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " &
                    "AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " &
                    "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " &
                    "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " &
                    "and (strStatus IS Null or strStatus = 'UC')  " &
                    "and datEnforcementFinalized is Null  " &
                    "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " &
                    " and numProgram = :pId " &
                    "order by strENforcementNumber DESC  "

                Case WorkViewerType.Enforcement_PM
                    SQL = "Select " &
                   "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " &
                   "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " &
                   "case  " &
                   "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " &
                   "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " &
                   "when strStatus = 'UC' then '2 - Submitted to UC'  " &
                   "When strStatus Is Null then '1 - At Staff'  " &
                   "else 'Unknown'  " &
                   "end as EnforcementStatus,  " &
                   "Case   " &
                   " 	when datDiscoveryDate is Null then ''  " &
                   "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " &
                   "END as Violationdate,   " &
                   "strActionType as HPVStatus,   " &
                   "Case  " &
                   " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " &
                   " 	when datEnforcementFinalized is NUll then 'Open'  " &
                   "Else 'Open'  " &
                   "End as Status,  " &
                   "AIRBranch.APBFacilityInformation.strFacilityName,  " &
                   "(strLastName||', '||strFirstName) as Staff  " &
                   "from AIRBranch.SSCP_AuditedEnforcement,   " &
                   "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " &
                   "AIRBranch.VW_SSCPINSPECTION_LIST " &
                   "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " &
                   "AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " &
                   "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " &
                   "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " &
                   "and (strStatus IS Null or strStatus = 'UC')  " &
                   "and datEnforcementFinalized is Null  " &
                   "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " &
                    "order by strENforcementNumber DESC  "

                Case WorkViewerType.FacilitiesWithSubparts
                    SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " &
                   "strFacilityName " &
                   "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " &
                   "where ( exists (select * " &
                   "from AIRBranch.APBSubpartData " &
                   "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " &
                   "and substr(strSubPartKey, 13, 1) = 'M') " &
                   "and subStr(strAirProgramCodes, 12, 1) = '1' " &
                   "or  exists (select * " &
                   "from AIRBranch.APBSubpartData " &
                   "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " &
                   "and substr(strSubPartKey, 13, 1) = '9') " &
                   "and subStr(strAirProgramCodes, 8, 1) = '1' " &
                   "or  exists (select * " &
                   "from AIRBranch.APBSubpartData " &
                   "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " &
                   "and substr(strSubPartKey, 13, 1) = '8') " &
                   "and subStr(strAirProgramCodes, 7, 1) = '1' ) " &
                   "and AIRBranch.APBHeaderData.strAIRSnumber = " &
                   "AIRBRANCH.APBFacilityInformation.strAIRsnumber " &
                   "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " &
                   "order by AIRSNumber "

                Case WorkViewerType.FacilitiesMissingSubparts
                    SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " &
                    "strFacilityName " &
                    "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " &
                    "where (Not exists (select * " &
                    "from AIRBranch.APBSubpartData " &
                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " &
                    "and substr(strSubPartKey, 13, 1) = 'M') " &
                    "and subStr(strAirProgramCodes, 12, 1) = '1' " &
                    "or Not exists (select * " &
                    "from AIRBranch.APBSubpartData " &
                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " &
                    "and substr(strSubPartKey, 13, 1) = '9') " &
                    "and subStr(strAirProgramCodes, 8, 1) = '1' " &
                    "or Not exists (select * " &
                    "from AIRBranch.APBSubpartData " &
                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " &
                    "and substr(strSubPartKey, 13, 1) = '8') " &
                    "and subStr(strAirProgramCodes, 7, 1) = '1' ) " &
                    "and AIRBranch.APBHeaderData.strAIRSnumber = " &
                    "AIRBRANCH.APBFacilityInformation.strAIRsnumber " &
                    "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " &
                    "order by AIRSNumber "

                Case WorkViewerType.MonitoringTestReports_Staff
                    ' Requires :pId = ReviewingEngineer
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " &
                        "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " &
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " &
                        "AIRBranch.ISMPReportInformation.strReferenceNumber  " &
                        "and  Status = 'Open' " &
                        " and ReviewingEngineer = :pId "

                Case WorkViewerType.MonitoringTestReports_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " &
                        "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " &
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " &
                        "AIRBranch.ISMPReportInformation.strReferenceNumber  " &
                        "and  Status = 'Open' " &
                        "and strUserUnit = " &
                          "(select strUnitDesc from AIRBranch.LookUpEPDUnits where numUnitCode = :pId) "

                Case WorkViewerType.MonitoringTestReports_PM
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " &
                        "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " &
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " &
                        "AIRBranch.ISMPReportInformation.strReferenceNumber  " &
                        "and  Status = 'Open' "

                Case WorkViewerType.MonitoringTestNotifications
                    SQL = "select  " &
                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,    " &
                    "case    " &
                    "when strReferenceNumber is null then ''    " &
                    "else strReferenceNumber    " &
                    "end RefNum,    " &
                    "case  " &
                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''  " &
                    "else AIRBranch.APBFacilityInformation.strFacilityName    " &
                    "End FacilityName,  " &
                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,  " &
                    "strEmissionUnit,   " &
                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,  " &
                    "case  " &
                    "when strFirstName is Null then ''  " &
                    "else(strLastName||', '||strFirstName)   " &
                    "END StaffResponsible  " &
                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,  " &
                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink  " &
                    "where AIRBranch.ismptestnotification.strairsnumber = " &
                    "AIRBRANCH.apbfacilityinformation.strairsnumber (+)    " &
                    "and AIRBranch.ismptestnotification.strstaffresponsible = " &
                    "AIRBRANCH.EPDUserProfiles.numUserID (+)  " &
                    "and AIRBranch.ISMPTestnotification.strTestLogNumber = " &
                    "AIRBRANCH.ISMPTestLogLink.strTestLogNumber (+)   " &
                    "and datProposedStartDate > (sysdate - 180)    " &
                    "and strReferenceNumber is null    " &
                    "union    " &
                    "select    " &
                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,  " &
                    "AIRBranch.ISMpReportInformation.strReferenceNumber as RefNum,    " &
                    "case  " &
                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''  " &
                    "else AIRBranch.APBFacilityInformation.strFacilityName    " &
                    "End FacilityName,  " &
                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,  " &
                    "strEmissionUnit,   " &
                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,  " &
                    "case  " &
                    "when strFirstName is Null then ''  " &
                    "else(strLastName||', '||strFirstName)   " &
                    "END StaffResponsible  " &
                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,  " &
                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink,    " &
                    "AIRBranch.ISMPReportInformation    " &
                    "where AIRBranch.ismptestnotification.strairsnumber = " &
                    "AIRBRANCH.apbfacilityinformation.strairsnumber (+)    " &
                    "and AIRBranch.ismptestnotification.strstaffresponsible = " &
                    "AIRBRANCH.EPDUserProfiles.numUserID (+)  " &
                    "and AIRBranch.ISMPTestNotification.strTestLogNumber = " &
                    "AIRBRANCH.ISMPTestLogLink.strTestLogNumber (+)    " &
                    "and AIRBranch.ISMPTestLogLink.strReferencenumber = " &
                    "AIRBRANCH.ISMPReportInformation.strReferenceNumber (+)    " &
                    "and datProposedStartDate > (sysdate - 180)    " &
                    "and AIRBranch.ISMPTestLogLink.strReferenceNumber is not null    " &
                    "and strClosed = 'False'  "

                Case WorkViewerType.PermitApplications_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " &
              "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " &
              "case " &
              " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " &
              "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " &
              "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " &
              "end as strAIRSNumber, " &
              "case " &
              "	when strApplicationTypeDesc IS Null then ' ' " &
              "Else strApplicationTypeDesc " &
              "End as strApplicationType, " &
              "case " &
              " 	when datReceivedDate is Null then ' ' " &
              "Else to_char(datReceivedDate, 'RRRR-MM-dd') " &
              " End as datReceivedDate, " &
              "case  " &
              "when strPermitNumber is NULL then ' '  " &
              "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " &
              " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " &
              " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " &
              "end As strPermitNumber, " &
              "case " &
              " 	when numUserID= '0' then ' ' " &
              "	when numUserID is Null then ' ' " &
              "else (strLastName||', '||strFirstName) " &
              "end as StaffResponsible, " &
              "case  " &
              "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " &
              "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " &
              "when datToDirector is Not Null and datFinalizedDate is Null " &
              "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " &
              "when datToBranchCheif is Not Null and datFinalizedDate is Null " &
              "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " &
              "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " &
              "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " &
              "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " &
              "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " &
              "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " &
              "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " &
              "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " &
              "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " &
              "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " &
              "end as StatusDate,  " &
              "case  " &
              " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " &
              "else AIRBranch.SSPPApplicationData.strFacilityName  " &
              "end as strFacilityName,  " &
              "case " &
              "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " &
              "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " &
              "when datToBranchCheif is Not Null and datFinalizedDate is Null " &
              "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " &
              "when datEPAEnds is not Null then '08 - EPA 45-day Review' " &
              "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " &
              "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " &
              "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " &
              "when dattoPMII is Not Null then '04 - AT PM'  " &
              "when dattoPMI is Not Null then '03 - At UC'  " &
              "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " &
              "then '02 - Internal Review' " &
              "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " &
              "else '01 - At Engineer'  " &
              "end as AppStatus, " &
              "case " &
              " 	when strPermitTypeDescription is Null then '' " &
              "else strPermitTypeDescription " &
              "End as strPermitType " &
              "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " &
              "AIRBranch.SSPPApplicationData, " &
              "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " &
              "AIRBranch.EPDuserProfiles  " &
              "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " &
              "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " &
              "and strApplicationType = strApplicationTypeCode (+) " &
              "and strPermitType = strPermitTypeCode (+) " &
              "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " &
              "and datFinalizedDate is NULL " &
              "and numUserID = :pId " &
                      "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                Case WorkViewerType.PermitApplications_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select " &
                    "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " &
                    "case " &
                    " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " &
                    "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " &
                    "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " &
                    "end as strAIRSNumber, " &
                    "case " &
                    "	when strApplicationTypeDesc IS Null then ' ' " &
                    "Else strApplicationTypeDesc " &
                    "End as strApplicationType, " &
                    "case " &
                    " 	when datReceivedDate is Null then ' ' " &
                    "Else to_char(datReceivedDate, 'RRRR-MM-dd') " &
                    " End as datReceivedDate, " &
                    "case  " &
                    "when strPermitNumber is NULL then ' '  " &
                    "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " &
                    " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " &
                    " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " &
                    "end As strPermitNumber, " &
                    "case " &
                    " 	when numUserID= '0' then ' ' " &
                    "	when numUserID is Null then ' ' " &
                    "else (strLastName||', '||strFirstName) " &
                    "end as StaffResponsible, " &
                    "case  " &
                    "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " &
                    "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " &
                    "when datToDirector is Not Null and datFinalizedDate is Null " &
                    "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " &
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null " &
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " &
                    "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " &
                    "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " &
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " &
                    "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " &
                    "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " &
                    "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " &
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " &
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " &
                    "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " &
                    "end as StatusDate,  " &
                    "case  " &
                    " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " &
                    "else AIRBranch.SSPPApplicationData.strFacilityName  " &
                    "end as strFacilityName,  " &
                    "case " &
                    "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " &
                    "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " &
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null " &
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " &
                    "when datEPAEnds is not Null then '08 - EPA 45-day Review' " &
                    "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " &
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " &
                    "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " &
                    "when dattoPMII is Not Null then '04 - AT PM'  " &
                    "when dattoPMI is Not Null then '03 - At UC'  " &
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " &
                    "then '02 - Internal Review' " &
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " &
                    "else '01 - At Engineer'  " &
                    "end as AppStatus, " &
                    "case " &
                    " 	when strPermitTypeDescription is Null then '' " &
                    "else strPermitTypeDescription " &
                    "End as strPermitType " &
                    "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " &
                    "AIRBranch.SSPPApplicationData, " &
                    "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " &
                    "AIRBranch.EPDuserProfiles  " &
                    "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " &
                    "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " &
                    "and strApplicationType = strApplicationTypeCode (+) " &
                    "and strPermitType = strPermitTypeCode (+) " &
                    "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " &
                    "and datFinalizedDate is NULL " &
                    " and (AIRBranch.EPDUserProfiles.numUnit = :pId   or (APBUnit = :pId))  " &
                      "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                Case WorkViewerType.PermitApplications_PM
                    SQL = "Select " &
                      "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " &
                      "case " &
                      " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " &
                      "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " &
                      "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " &
                      "end as strAIRSNumber, " &
                      "case " &
                      "	when strApplicationTypeDesc IS Null then ' ' " &
                      "Else strApplicationTypeDesc " &
                      "End as strApplicationType, " &
                      "case " &
                      " 	when datReceivedDate is Null then ' ' " &
                      "Else to_char(datReceivedDate, 'RRRR-MM-dd') " &
                      " End as datReceivedDate, " &
                      "case  " &
                      "when strPermitNumber is NULL then ' '  " &
                      "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " &
                      " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " &
                      " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " &
                      "end As strPermitNumber, " &
                      "case " &
                      " 	when numUserID= '0' then ' ' " &
                      "	when numUserID is Null then ' ' " &
                      "else (strLastName||', '||strFirstName) " &
                      "end as StaffResponsible, " &
                      "case  " &
                      "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " &
                      "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " &
                      "when datToDirector is Not Null and datFinalizedDate is Null " &
                      "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " &
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " &
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " &
                      "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " &
                      "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " &
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " &
                      "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " &
                      "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " &
                      "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " &
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " &
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " &
                      "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " &
                      "end as StatusDate,  " &
                      "case  " &
                      " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " &
                      "else AIRBranch.SSPPApplicationData.strFacilityName  " &
                      "end as strFacilityName,  " &
                      "case " &
                      "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " &
                      "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " &
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " &
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " &
                      "when datEPAEnds is not Null then '08 - EPA 45-day Review' " &
                      "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " &
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " &
                      "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " &
                      "when dattoPMII is Not Null then '04 - AT PM'  " &
                      "when dattoPMI is Not Null then '03 - At UC'  " &
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " &
                      "then '02 - Internal Review' " &
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " &
                      "else '01 - At Engineer'  " &
                      "end as AppStatus, " &
                      "case " &
                      " 	when strPermitTypeDescription is Null then '' " &
                      "else strPermitTypeDescription " &
                      "End as strPermitType " &
                      "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " &
                      "AIRBranch.SSPPApplicationData, " &
                      "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " &
                      "AIRBranch.EPDuserProfiles  " &
                      "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " &
                      "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " &
                      "and strApplicationType = strApplicationTypeCode (+) " &
                      "and strPermitType = strPermitTypeCode (+) " &
                      "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " &
                      "and datFinalizedDate is NULL " &
                      "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                Case WorkViewerType.SBEAP_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "select * " &
                    "from AIRBRANCH.VW_SBEAP_CaseLog " &
                    "where caseclosed is null " &
                    "and numstaffresponsible = :pId " &
                    "order by numcaseid "

                Case WorkViewerType.SBEAP_Program
                    SQL = "select * " &
                    "from AIRBRANCH.VW_SBEAP_CaseLog " &
                    "where caseclosed is null " &
                    "order by numcaseid "

                Case Else
                    SQL = ""

            End Select

            Return SQL
        End Function

    End Module
End Namespace