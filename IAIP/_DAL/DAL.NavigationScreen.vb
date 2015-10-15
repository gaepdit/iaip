Imports Oracle.ManagedDataAccess.Client

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

            Dim parameter As New OracleParameter("pId", parameterValue)

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
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                    "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                    "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                      "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                    "and  Status = 'Open' "

                Case WorkViewerType.ISMP_UC
                    ' Requires :pId = UserUnit
                    SQL = "select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                        "from AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                         "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                        "and status = 'Open' " & _
                        "and strUserUnit = " & _
                        "(select strUnitDesc from AIRBranch.LookUpEPDUnits where numUnitCode = :pId) "

                Case WorkViewerType.ISMP_Staff
                    ' Requires :pId = ReviewingEngineer
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                    "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                    "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                     "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                    "and Status = 'Open' " & _
                    "and ReviewingEngineer = :pId"

                Case WorkViewerType.SSCP_PM
                    SQL = "Select " & _
                        "distinct(to_number(AIRBranch.sscp_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber,  " & _
                        "substr(AIRBranch.sscp_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                        "case   " & _
                        "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                        "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                        "when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                        "When strStatus Is Null then '1 - At Staff'   " & _
                        "else 'Unknown'   " & _
                        "end as EnforcementStatus,   " & _
                        "Case     " & _
                        "when datDiscoveryDate is Null then ''    " & _
                        "else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                        "END as Violationdate,     " & _
                        "strActionType as HPVStatus,    " & _
                        "Case    " & _
                        "when datEnforcementFinalized Is Not NULL then 'Closed'    " & _
                        "when datEnforcementFinalized is NUll then 'Open'    " & _
                        "Else 'Open'    " & _
                        "End as Status,    " & _
                        "strFacilityName,    " & _
                        "(strLastName||', '||strFirstName) as Staff     " & _
                        "from AIRBranch.sscp_AuditedEnforcement,     " & _
                        "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,    " & _
                        "(select numUserID  " & _
                        "from AIRBranch.EPDUserProfiles where numUnit is null) UnitStaff " & _
                        "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.sscp_AuditedEnforcement.strAIRSNumber    " & _
                        "and (strStatus IS Null or strStatus = 'UC')    " & _
                        "and datEnforcementFinalized is NULL   " & _
                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.sscp_AuditedEnforcement.numStaffResponsible    " & _
                        "order by strENforcementNumber DESC   "

                Case WorkViewerType.SSCP_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select to_number(AIRBranch.SSCP_aUDITEDEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                         "substr(AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                         "case   " & _
                         "    when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                         "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                         "    when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                         "    When strStatus Is Null then '1 - At Staff'   " & _
                         "   else 'Unknown'   " & _
                         "end as EnforcementStatus, " & _
                        " Case    " & _
                        " 	when datDiscoveryDate is Null then ''   " & _
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                         "END as Violationdate,    " & _
                         "strActionType as HPVStatus,   " & _
                         "Case   " & _
                         " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " & _
                         "	when datEnforcementFinalized is NUll then 'Open'   " & _
                         "Else 'Open'   " & _
                         "End as Status,   " & _
                         "strFacilityName,   " & _
                         "(strLastName||', '||strFirstName) as Staff   " & _
                         "from AIRBranch.SSCP_aUDITEDEnforcement,    " & _
                         "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,   " & _
                         "( select numUserID from AIRBranch.EPDUserProfiles where numUnit = :pId " & _
                         "group by numUserID ) UnitStaff   " & _
                         "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber   " & _
                         "and (strStatus IS Null or strStatus = 'UC')   " & _
                         "and numStaffResponsible = UnitStaff.numUserID   " & _
                         "and datEnforcementFinalized is NULL   " & _
                         "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_aUDITEDEnforcement.numStaffResponsible   " & _
                         "order by strENforcementNumber DESC  "

                Case WorkViewerType.SSCP_DistrictLiaison
                    SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                        "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                        "case   " & _
                        "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                        "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                        "when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                        "When strStatus Is Null then '1 - At Staff'   " & _
                        "   else 'Unknown'   " & _
                        "end as EnforcementStatus, " & _
                        "Case    " & _
                        " 	when datDiscoveryDate is Null then ''   " & _
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                        "END as Violationdate,    " & _
                        "strActionType as HPVStatus,   " & _
                        "Case   " & _
                        " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " & _
                        "	when datEnforcementFinalized is NUll then 'Open'   " & _
                        "Else 'Open'   " & _
                        "End as Status,   " & _
                        "strFacilityName,   " & _
                        "(strLastName||', '||strFirstName) as Staff   " & _
                        "from AIRBranch.SSCP_AuditedEnforcement,  " & _
                        "AIRBranch.APBFacilityInformation, AIRBranch.EPDUSerProfiles,   " & _
                        "(select numuserId  " & _
                        "from AIRBranch.EPDUserProfiles  " & _
                        "where strLastName = 'District' or (numBranch = '1' and numProgram = '4' and numUnit is null )  " & _
                        "group by numUserID) UnitStaff   " & _
                        "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber   " & _
                        "and (strStatus IS Null or strStatus = 'UC')   " & _
                        "and numStaffResponsible = UnitStaff.numUserID   " & _
                        "and datEnforcementFinalized is NULL   " & _
                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_AuditedEnforcement.numStaffResponsible   " & _
                        "order by strENforcementNumber DESC   "

                Case WorkViewerType.SSCP_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                     "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                     "case  " & _
                     "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                     "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                     "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                     "When strStatus Is Null then '1 - At Staff'  " & _
                     "else 'Unknown'  " & _
                     "end as EnforcementStatus,  " & _
                     "Case   " & _
                     " 	when datDiscoveryDate is Null then ''  " & _
                     "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                     "END as Violationdate,   " & _
                     "strActionType as HPVStatus,   " & _
                     "Case  " & _
                     " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                     " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                     "Else 'Open'  " & _
                     "End as Status,  " & _
                     "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                     "(strLastName||', '||strFirstName) as Staff  " & _
                     "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                     "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                     "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                     "Where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                     "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = '0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                     "and (numStaffResponsible = :pId or numSSCPEngineer = :pId)  " & _
                     "and (strStatus IS Null or strStatus = 'UC')  " & _
                     "and datEnforcementFinalized is Null  " & _
                     "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                     "order by strENforcementNumber DESC  "

                Case WorkViewerType.SSPP_PM
                    SQL = "Select " & _
                      "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " & _
                      "case " & _
                      " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " & _
                      "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " & _
                      "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " & _
                      "end as strAIRSNumber, " & _
                      "case " & _
                      "	when strApplicationTypeDesc IS Null then ' ' " & _
                      "Else strApplicationTypeDesc " & _
                      "End as strApplicationType, " & _
                      "case " & _
                      " 	when datReceivedDate is Null then ' ' " & _
                      "Else to_char(datReceivedDate, 'RRRR-MM-dd') " & _
                      " End as datReceivedDate, " & _
                      "case  " & _
                      "when strPermitNumber is NULL then ' '  " & _
                      "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
                      " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
                      " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
                      "end As strPermitNumber, " & _
                      "case " & _
                      " 	when numUserID= '0' then ' ' " & _
                      "	when numUserID is Null then ' ' " & _
                      "else (strLastName||', '||strFirstName) " & _
                      "end as StaffResponsible, " & _
                      "case  " & _
                      "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " & _
                      "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
                      "when datToDirector is Not Null and datFinalizedDate is Null " & _
                      "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " & _
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " & _
                      "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " & _
                      "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " & _
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " & _
                      "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " & _
                      "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " & _
                      "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " & _
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " & _
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " & _
                      "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " & _
                      "end as StatusDate,  " & _
                      "case  " & _
                      " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " & _
                      "else AIRBranch.SSPPApplicationData.strFacilityName  " & _
                      "end as strFacilityName,  " & _
                      "case " & _
                      "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " & _
                      "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " & _
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " & _
                      "when datEPAEnds is not Null then '08 - EPA 45-day Review' " & _
                      "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " & _
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " & _
                      "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " & _
                      "when dattoPMII is Not Null then '04 - AT PM'  " & _
                      "when dattoPMI is Not Null then '03 - At UC'  " & _
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " & _
                      "then '02 - Internal Review' " & _
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " & _
                      "else '01 - At Engineer'  " & _
                      "end as AppStatus, " & _
                      "case " & _
                      " 	when strPermitTypeDescription is Null then '' " & _
                      "else strPermitTypeDescription " & _
                      "End as strPermitType " & _
                      "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " & _
                      "AIRBranch.SSPPApplicationData, " & _
                      "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " & _
                      "AIRBranch.EPDuserProfiles  " & _
                      "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " & _
                      "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " & _
                      "and strApplicationType = strApplicationTypeCode (+) " & _
                      "and strPermitType = strPermitTypeCode (+) " & _
                      "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                      "and datFinalizedDate is NULL " & _
                      "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                Case WorkViewerType.SSPP_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select " & _
                         "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,  " & _
                         "case  " & _
                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' '  " & _
                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' '  " & _
                         "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5)  " & _
                         "end as strAIRSNumber,  " & _
                         "case  " & _
                         "	when strApplicationTypeDesc IS Null then ' '  " & _
                         "Else strApplicationTypeDesc  " & _
                         "End as strApplicationType,  " & _
                         "case  " & _
                         "	when datReceivedDate is Null then ' '  " & _
                         "Else to_char(datReceivedDate, 'RRRR-MM-dd')  " & _
                         "End as datReceivedDate,  " & _
                         "case   " & _
                         "  when strPermitNumber is NULL then ' '   " & _
                         "   else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'   " & _
                         " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'  " & _
                         " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)  " & _
                         "end As strPermitNumber,  " & _
                         "case  " & _
                         "	when numUserID = '0' then ' '  " & _
                         "	when numUserID is Null then ' '  " & _
                         "else (strLastName||', '||strFirstName)  " & _
                         "end as StaffResponsible,  " & _
                         "case   " & _
                         "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')     " & _
                         "when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')  " & _
                         "when datToDirector is Not Null and datFinalizedDate is Null  " & _
                         "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')  " & _
                         "when datToBranchCheif is Not Null and datFinalizedDate is Null  " & _
                         "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')   " & _
                         "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')    " & _
                         "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')    " & _
                         "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
                         "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')     " & _
                         "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')     " & _
                         "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')     " & _
                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')    " & _
                         "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'     " & _
                         "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')     " & _
                         "end as StatusDate,   " & _
                         "case   " & _
                         "	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '   " & _
                         "else AIRBranch.SSPPApplicationData.strFacilityName   " & _
                         "end as strFacilityName,   " & _
                         "case  " & _
                         "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'  " & _
                         "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'  " & _
                         "when datToBranchCheif is Not Null and datFinalizedDate is Null  " & _
                         "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'  " & _
                         "when datEPAEnds is not Null then '08 - EPA 45-day Review'  " & _
                         "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'  " & _
                         "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'   " & _
                         "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'   " & _
                         "when dattoPMII is Not Null then '04 - AT PM'   " & _
                         "when dattoPMI is Not Null then '03 - At UC'   " & _
                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'  " & _
                         "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'    " & _
                         "else '01 - At Engineer'   " & _
                         "end as AppStatus,  " & _
                         "case  " & _
                         "	when strPermitTypeDescription is Null then ''  " & _
                         "else strPermitTypeDescription  " & _
                         "End as strPermitType  " & _
                         "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking,  " & _
                         "AIRBranch.SSPPApplicationData,  " & _
                         "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes,  " & _
                         "AIRBranch.EPDUserProfiles " & _
                         "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)   " & _
                         "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+)  " & _
                         "and strApplicationType = strApplicationTypeCode (+)  " & _
                         "and strPermitType = strPermitTypeCode (+)  " & _
                         "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible  " & _
                         "and datFinalizedDate is NULL  " & _
                         "and (AIRBranch.EPDUserProfiles.numUnit = :pId   " & _
                         "or (APBUnit = :pId ))  "

                Case WorkViewerType.SSPP_Administrative
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " & _
                        "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,   " & _
                        "case   " & _
                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' '   " & _
                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' '   " & _
                        " else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5)   " & _
                        " end as strAIRSNumber,   " & _
                        " case   " & _
                        " 	when strApplicationTypeDesc IS Null then ' '   " & _
                        "Else strApplicationTypeDesc   " & _
                        "End as strApplicationType,   " & _
                        "case   " & _
                        "	when datReceivedDate is Null then ' '   " & _
                        "Else to_char(datReceivedDate, 'RRRR-MM-dd')   " & _
                        "End as datReceivedDate,   " & _
                        "case    " & _
                        " when strPermitNumber is NULL then ' '    " & _
                        " else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'    " & _
                        "   ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'   " & _
                        "   ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)   " & _
                        "end As strPermitNumber,   " & _
                        "case   " & _
                        "	when numUserID = '0' then ' '   " & _
                        "	when numUserID is Null then ' '   " & _
                        "else (strLastName||', '||strFirstName)   " & _
                        "end as StaffResponsible,   " & _
                        "case    " & _
                        "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')      " & _
                        "when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')   " & _
                        "when datToDirector is Not Null and datFinalizedDate is Null   " & _
                        "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')   " & _
                        "when datToBranchCheif is Not Null and datFinalizedDate is Null   " & _
                     "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')    " & _
                        "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')     " & _
                         "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
                         "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')      " & _
                         "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')      " & _
                         "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')      " & _
                         "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')      " & _
                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')     " & _
                         "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'      " & _
                         "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')      " & _
                         "end as StatusDate,    " & _
                         "case    " & _
                         " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '    " & _
                         "else AIRBranch.SSPPApplicationData.strFacilityName    " & _
                         "end as strFacilityName,    " & _
                         "case   " & _
                         "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'   " & _
                  "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'   " & _
                         "when datToBranchCheif is Not Null and datFinalizedDate is Null   " & _
                         "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'   " & _
                         "when datEPAEnds is not Null then '08 - EPA 45-day Review'   " & _
                         "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'   " & _
                         "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'    " & _
                         "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'    " & _
                         "when dattoPMII is Not Null then '04 - AT PM'    " & _
                         "when dattoPMI is Not Null then '03 - At UC'    " & _
                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'   " & _
                         "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'     " & _
                         "else '01 - At Engineer'    " & _
                         "end as AppStatus,   " & _
                         "case   " & _
                          "when strPermitTypeDescription is Null then ''   " & _
                         "else strPermitTypeDescription   " & _
                         "End as strPermitType   " & _
                         "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking,   " & _
                         "AIRBranch.SSPPApplicationData,   " & _
                         "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes,   " & _
                         "AIRBranch.EPDUserProfiles    " & _
                         "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)    " & _
                         "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+)   " & _
                         "and strApplicationType = strApplicationTypeCode (+)   " & _
                         "and strPermitType = strPermitTypeCode (+)   " & _
                         "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " & _
                        "and datFinalizedDate is NULL   " & _
                         "and numUserID = :pId  "

                Case WorkViewerType.SSPP_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " & _
                    "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,   " & _
                    "case   " & _
                    " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' '   " & _
                    " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' '   " & _
                    " else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5)   " & _
                    "end as strAIRSNumber,   " & _
                    "   case   " & _
                    " 	when strApplicationTypeDesc IS Null then ' '   " & _
                    "Else strApplicationTypeDesc   " & _
                    "End as strApplicationType,   " & _
                    "case   " & _
                    " 	when datReceivedDate is Null then ' '   " & _
                    "Else to_char(datReceivedDate, 'RRRR-MM-dd')   " & _
                    " End as datReceivedDate,   " & _
                    " case    " & _
                    " when strPermitNumber is NULL then ' '    " & _
                    "  else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'    " & _
                    " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'   " & _
                    " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)   " & _
                    "end As strPermitNumber,   " & _
                    "case   " & _
                    " 	when numUserID = '0' then ' '   " & _
                    " 	when numUserID is Null then ' '   " & _
                    "else (strLastName||', '||strFirstName)   " & _
                    "end as StaffResponsible,   " & _
                    "case    " & _
                    "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')      " & _
                    "when datFinalizeddate is Not Null then to_char(datFinalizeddate, 'RRRR-MM-dd')   " & _
                    "when datToDirector is Not Null and datFinalizedDate is Null and   " & _
                    "(datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')   " & _
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null and   " & _
                    "datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')    " & _
                    "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')     " & _
                    "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')      " & _
                    "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')      " & _
                    "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')      " & _
                    "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')      " & _
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')     " & _
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'      " & _
                    "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')      " & _
                    "end as StatusDate,    " & _
                    "case    " & _
                    "	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '    " & _
                    "else AIRBranch.SSPPApplicationData.strFacilityName    " & _
                    "end as strFacilityName,    " & _
                    "case   " & _
                    "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'   " & _
             "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'   " & _
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null   " & _
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'   " & _
                    "when datEPAEnds is not Null then '08 - EPA 45-day Review'   " & _
                    "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'   " & _
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'    " & _
                    "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'    " & _
                    "when dattoPMII is Not Null then '04 - AT PM'    " & _
                    "when dattoPMI is Not Null then '03 - At UC'    " & _
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'   " & _
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'     " & _
                    "else '01 - At Engineer'    " & _
                    "end as AppStatus,   " & _
                    "case   " & _
                    " 	when strPermitTypeDescription is Null then ''   " & _
                    "else strPermitTypeDescription   " & _
                    "End as strPermitType   " & _
                    "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking,   " & _
                    "AIRBranch.SSPPApplicationData,   " & _
                    "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes,   " & _
                    "AIRBranch.EPDUserProfiles  " & _
             "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)    " & _
             "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+)   " & _
                    "and strApplicationType = strApplicationTypeCode (+)   " & _
                    "and strPermitType = strPermitTypeCode (+)   " & _
                    "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " & _
                    "and datFinalizedDate is NULL   " & _
                    "and numUserID = :pId "

                Case WorkViewerType.ProgCoord_PM
                    SQL = "Select " & _
                    "distinct(to_number(AIRBranch.sscp_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber,  " & _
                    "substr(AIRBranch.sscp_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                    "case   " & _
                    "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                    "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                    "when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                    "When strStatus Is Null then '1 - At Staff'   " & _
                    "else 'Unknown'   " & _
                    "end as EnforcementStatus,   " & _
                    "Case     " & _
                    "when datDiscoveryDate is Null then ''    " & _
                    "else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                    "END as Violationdate,     " & _
                    "strActionType as HPVStatus,    " & _
                    "Case    " & _
                    "when datEnforcementFinalized Is Not NULL then 'Closed'    " & _
                    "when datEnforcementFinalized is NUll then 'Open'    " & _
                    "Else 'Open'    " & _
                    "End as Status,    " & _
                    "strFacilityName,    " & _
                    "(strLastName||', '||strFirstName) as Staff     " & _
                    "from AIRBranch.sscp_AuditedEnforcement,     " & _
                    "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,    " & _
                    "(select numUserID  " & _
                    "from AIRBranch.EPDUserProfiles where numUnit is null) UnitStaff " & _
                    "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.sscp_AuditedEnforcement.strAIRSNumber    " & _
                    "and (strStatus IS Null or strStatus = 'UC')    " & _
                    "and datEnforcementFinalized is NULL   " & _
                    "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.sscp_AuditedEnforcement.numStaffResponsible    " & _
                    "order by strENforcementNumber DESC   "

                Case WorkViewerType.ProgCoord_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select to_number(AIRBranch.SSCP_aUDITEDEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                         "substr(AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                         "case   " & _
                         "    when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                         "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                         "    when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                         "    When strStatus Is Null then '1 - At Staff'   " & _
                         "   else 'Unknown'   " & _
                         "end as EnforcementStatus, " & _
                        " Case    " & _
                        " 	when datDiscoveryDate is Null then ''   " & _
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                         "END as Violationdate,    " & _
                         "strActionType as HPVStatus,   " & _
                         "Case   " & _
                         " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " & _
                         "	when datEnforcementFinalized is NUll then 'Open'   " & _
                         "Else 'Open'   " & _
                         "End as Status,   " & _
                         "strFacilityName,   " & _
                         "(strLastName||', '||strFirstName) as Staff   " & _
                         "from AIRBranch.SSCP_aUDITEDEnforcement,    " & _
                         "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,   " & _
                         "( select numUserID from AIRBranch.EPDUserProfiles where numUnit = :pId  " & _
                         "group by numUserID ) UnitStaff   " & _
                         "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber   " & _
                         "and (strStatus IS Null or strStatus = 'UC')   " & _
                         "and numStaffResponsible = UnitStaff.numUserID   " & _
                         "and datEnforcementFinalized is NULL   " & _
                         "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_aUDITEDEnforcement.numStaffResponsible   " & _
                         "order by strENforcementNumber DESC  "

                Case WorkViewerType.ProgCoord_DistrictLiaison
                    SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                        "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                        "case   " & _
                        "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                        "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                        "when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                        "When strStatus Is Null then '1 - At Staff'   " & _
                        "   else 'Unknown'   " & _
                        "end as EnforcementStatus, " & _
                        "Case    " & _
                        " 	when datDiscoveryDate is Null then ''   " & _
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                        "END as Violationdate,    " & _
                        "strActionType as HPVStatus,   " & _
                        "Case   " & _
                        " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " & _
                        "	when datEnforcementFinalized is NUll then 'Open'   " & _
                        "Else 'Open'   " & _
                        "End as Status,   " & _
                        "strFacilityName,   " & _
                        "(strLastName||', '||strFirstName) as Staff   " & _
                        "from AIRBranch.SSCP_AuditedEnforcement,  " & _
                        "AIRBranch.APBFacilityInformation, AIRBranch.EPDUSerProfiles,   " & _
                        "(select numuserId  " & _
                        "from AIRBranch.EPDUserProfiles  " & _
                        "where strLastName = 'District' or (numBranch = '1' and numProgram = '4' and numUnit is null )  " & _
                        "group by numUserID) UnitStaff   " & _
                        "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber   " & _
                        "and (strStatus IS Null or strStatus = 'UC')   " & _
                        "and numStaffResponsible = UnitStaff.numUserID   " & _
                        "and datEnforcementFinalized is NULL   " & _
                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_AuditedEnforcement.numStaffResponsible   " & _
                        "order by strENforcementNumber DESC   "

                Case WorkViewerType.ProgCoord_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                     "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                     "case  " & _
                     "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                     "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                     "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                     "When strStatus Is Null then '1 - At Staff'  " & _
                     "else 'Unknown'  " & _
                     "end as EnforcementStatus,  " & _
                     "Case   " & _
                     " 	when datDiscoveryDate is Null then ''  " & _
                     "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                     "END as Violationdate,   " & _
                     "strActionType as HPVStatus,   " & _
                     "Case  " & _
                     " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                     " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                     "Else 'Open'  " & _
                     "End as Status,  " & _
                     "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                     "(strLastName||', '||strFirstName) as Staff  " & _
                     "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                     "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                     "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                     "Where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                     "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = '0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                     "and (numStaffResponsible = :pId or numSSCPEngineer = :pId)  " & _
                     "and (strStatus IS Null or strStatus = 'UC')  " & _
                     "and datEnforcementFinalized is Null  " & _
                     "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                     "order by strENforcementNumber DESC  "

                Case WorkViewerType.ComplianceFacilitiesAssigned_Program
                    ' Requires :pId = UserProgram
                    SQL = "Select distinct " & _
                   "substr(AIRBranch.SSCPInspectionsRequired.strAIRSnumber, 5) as AIRSNumber, " & _
                   "AIRBranch.APBFacilityInformation.strFacilityName, " & _
                   "(strLastName||', '||strFirstName) as Staff " & _
                   "from AIRBranch.SSCPInspectionsRequired, AIRBranch.APBFacilityInformation, " & _
                   "AIRBranch.EPDUserProfiles " & _
                   "where AIRBranch.SSCPInspectionsRequired.strAIRSNumber = AIRBranch.APBFacilityInformation.strAIRSNumber " & _
                   "and AIRBranch.SSCPInspectionsRequired.numSSCPEngineer = AIRBranch.EPDUserProfiles.numUserID " & _
                   "and numProgram = :pId " & _
                   "order by AIRSNumber  "

                Case WorkViewerType.ComplianceFacilitiesAssigned_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select distinct " & _
                   "substr(AIRBranch.SSCPInspectionsRequired.strAIRSnumber, 5) as AIRSNumber, " & _
                   "AIRBranch.APBFacilityInformation.strFacilityName, " & _
                   "(strLastName||', '||strFirstName) as Staff " & _
                   "from AIRBranch.SSCPInspectionsRequired, AIRBranch.APBFacilityInformation, " & _
                   "AIRBranch.EPDUserProfiles " & _
                   "where AIRBranch.SSCPInspectionsRequired.strAIRSNumber = AIRBranch.APBFacilityInformation.strAIRSNumber " & _
                   "and AIRBranch.SSCPInspectionsRequired.numSSCPEngineer = AIRBranch.EPDUserProfiles.numUserID " & _
                   "and numSSCPEngineer = :pId " & _
                   "order by AIRSNumber  "

                Case WorkViewerType.ComplianceWork_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " & _
                        "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                        "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                        "(strLastName||', '||strFirstName) as Staff,  " & _
                        "strResponsibleStaff, " & _
                        "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived, " & _
                        "AIRBranch.APBFacilityInformation.strFacilityName, StrActivityName    " & _
                        "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                        "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,   " & _
                        "AIRBranch.VW_SSCPInspection_List " & _
                        "where AIRBranch.EPDUserProfiles.numUserID = " & _
                        "AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " & _
                        "and AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                        "AIRBRANCH.SSCPItemMaster.strAIRSNumber  " & _
                        "and AIRBranch.LookUPComplianceActivities.strActivityType = " & _
                        "AIRBRANCH.SSCPItemMaster.strEventType  " & _
                        " and AIRBranch.SSCPItemMaster.strAIRSnumber = '0413'||" & _
                        "AIRBRANCH.VW_SSCPInspection_List.AIRSNumber  " & _
                        "and (strResponsibleStaff = :pId or numSSCPEngineer = :pId) " & _
                        "and DatCompleteDate is Null  " & _
                        "and strDelete is Null "

                Case WorkViewerType.ComplianceWork_UC_ProgCoord
                    ' Requires :pId = UserProgram
                    SQL = "select " & _
                            "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                            "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                            "(strLastName||', '||strFirstName) as Staff,  " & _
                            "strResponsibleStaff, " & _
                            "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                            "strFacilityName, StrActivityName    " & _
                            "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                            "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,  " & _
                            "(select numUserID from AIRBranch.EPDUserProfiles where numProgram = :pId)  " & _
                            "UnitStaff    " & _
                            "where AIRBranch.EPDUserProfiles.numUserID = " & _
                            "AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " & _
                            "and AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                            "AIRBRANCH.SSCPItemMaster.strAIRSNumber  " & _
                            "and AIRBranch.LookUPComplianceActivities.strActivityType = " & _
                            "AIRBRANCH.SSCPItemMaster.strEventType " & _
                            "and DatCompleteDate is Null   " & _
                            "and strResponsibleStaff = UnitStaff.numUserID " & _
                            "and strDelete is Null "

                Case WorkViewerType.ComplianceWork_UC
                    ' Requires :pId = UserUnit
                    SQL = "select " & _
                     "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                     "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                     "(strLastName||', '||strFirstName) as Staff,  " & _
                     "strResponsibleStaff, " & _
                     "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                     "strFacilityName, StrActivityName    " & _
                     "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                     "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,  " & _
                     "(select numUserID from AIRBranch.EPDUserProfiles where numUnit = :pId)  " & _
                     "UnitStaff    " & _
                     "where AIRBranch.EPDUserProfiles.numUserID = " & _
                     "AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " & _
                     "and AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                     "AIRBRANCH.SSCPItemMaster.strAIRSNumber  " & _
                     "and AIRBranch.LookUPComplianceActivities.strActivityType = " & _
                     "AIRBRANCH.SSCPItemMaster.strEventType " & _
                     "and DatCompleteDate is Null   " & _
                     "and strResponsibleStaff = UnitStaff.numUserID " & _
                     "and strDelete is Null "

                Case WorkViewerType.ComplianceWork_PM
                    SQL = "select " & _
                       "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                       "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                        " case when AIRBranch.SSCPItemMaster.STRRESPONSIBLESTAFF = 0 then ': No one assigned' " & _
                        " when AIRBranch.SSCPItemMaster.STRRESPONSIBLESTAFF is null then ': Not assigned' " & _
                        "Else STRLASTNAME || ', ' || STRFIRSTNAME end AS Staff, " & _
                       "strResponsibleStaff, " & _
                       "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                       "strFacilityName, StrActivityName    " & _
                       "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                       "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities " & _
                       "where AIRBranch.EPDUserProfiles.numUserID(+) = " & _
                                "AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " & _
                       "and AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                       "AIRBRANCH.SSCPItemMaster.strAIRSNumber  " & _
                       "and AIRBranch.LookUPComplianceActivities.strActivityType = " & _
                       "AIRBRANCH.SSCPItemMaster.strEventType " & _
                       "and DatCompleteDate is Null   " & _
                       "and strDelete is Null "

                Case WorkViewerType.DelinquentFCEs
                    Dim StartCMSA As String = Format(CDate(OracleDate).AddDays(-730), "yyyy-MM-dd")
                    Dim StartCMSS As String = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")
                    SQL = "Select * " & _
                    "from AIRBranch.VW_SSCP_CMSWarning " & _
                    "where AIRSNumber is not Null " & _
                    " and strCMSMember is not null " & _
                    " and ((strCMSMember = 'A' and lastFCE < '" & StartCMSA & "') " & _
                    "or (strCMSMember = 'S' and LastFCE < '" & StartCMSS & "')) "

                Case WorkViewerType.Enforcement_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " & _
                      "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                      "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                      "case  " & _
                      "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                      "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                      "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                      "When strStatus Is Null then '1 - At Staff'  " & _
                      "else 'Unknown'  " & _
                      "end as EnforcementStatus,  " & _
                      "Case   " & _
                      " 	when datDiscoveryDate is Null then ''  " & _
                      "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                      "END as Violationdate,   " & _
                      "strActionType as HPVStatus,   " & _
                      "Case  " & _
                      " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                      " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                      "Else 'Open'  " & _
                      "End as Status,  " & _
                      "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                      "(strLastName||', '||strFirstName) as Staff  " & _
                      "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                      "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                      "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                      "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                      "AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                      "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " & _
                      "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                      "and (strStatus IS Null or strStatus = 'UC')  " & _
                      "and datEnforcementFinalized is Null  " & _
                      "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                      "and (numStaffResponsible = :pId or numSSCPEngineer = :pId) " & _
                    "order by strENforcementNumber DESC  "

                Case WorkViewerType.Enforcement_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select " & _
                    "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                    "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "case  " & _
                    "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                    "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                    "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                    "When strStatus Is Null then '1 - At Staff'  " & _
                    "else 'Unknown'  " & _
                    "end as EnforcementStatus,  " & _
                    "Case   " & _
                    " 	when datDiscoveryDate is Null then ''  " & _
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                    "END as Violationdate,   " & _
                    "strActionType as HPVStatus,   " & _
                    "Case  " & _
                    " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                    " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                    "Else 'Open'  " & _
                    "End as Status,  " & _
                    "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                    "(strLastName||', '||strFirstName) as Staff  " & _
                    "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                    "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                    "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                    "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                    "AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                    "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " & _
                    "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                    "and (strStatus IS Null or strStatus = 'UC')  " & _
                    "and datEnforcementFinalized is Null  " & _
                    "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                    " and numUnit = :pId " & _
                    "order by strENforcementNumber DESC  "

                Case WorkViewerType.Enforcement_UC_ProgCoord
                    ' Requires :pId = UserProgram
                    SQL = "Select " & _
                    "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                    "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "case  " & _
                    "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                    "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                    "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                    "When strStatus Is Null then '1 - At Staff'  " & _
                    "else 'Unknown'  " & _
                    "end as EnforcementStatus,  " & _
                    "Case   " & _
                    " 	when datDiscoveryDate is Null then ''  " & _
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                    "END as Violationdate,   " & _
                    "strActionType as HPVStatus,   " & _
                    "Case  " & _
                    " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                    " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                    "Else 'Open'  " & _
                    "End as Status,  " & _
                    "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                    "(strLastName||', '||strFirstName) as Staff  " & _
                    "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                    "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                    "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                    "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                    "AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                    "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " & _
                    "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                    "and (strStatus IS Null or strStatus = 'UC')  " & _
                    "and datEnforcementFinalized is Null  " & _
                    "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                    " and numProgram = :pId " & _
                    "order by strENforcementNumber DESC  "

                Case WorkViewerType.Enforcement_PM
                    SQL = "Select " & _
                   "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                   "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                   "case  " & _
                   "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                   "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                   "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                   "When strStatus Is Null then '1 - At Staff'  " & _
                   "else 'Unknown'  " & _
                   "end as EnforcementStatus,  " & _
                   "Case   " & _
                   " 	when datDiscoveryDate is Null then ''  " & _
                   "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                   "END as Violationdate,   " & _
                   "strActionType as HPVStatus,   " & _
                   "Case  " & _
                   " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                   " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                   "Else 'Open'  " & _
                   "End as Status,  " & _
                   "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                   "(strLastName||', '||strFirstName) as Staff  " & _
                   "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                   "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                   "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                   "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                   "AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                   "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " & _
                   "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                   "and (strStatus IS Null or strStatus = 'UC')  " & _
                   "and datEnforcementFinalized is Null  " & _
                   "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                    "order by strENforcementNumber DESC  "

                Case WorkViewerType.FacilitiesWithSubparts
                    SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " & _
                   "strFacilityName " & _
                   "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " & _
                   "where ( exists (select * " & _
                   "from AIRBranch.APBSubpartData " & _
                   "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                   "and substr(strSubPartKey, 13, 1) = 'M') " & _
                   "and subStr(strAirProgramCodes, 12, 1) = '1' " & _
                   "or  exists (select * " & _
                   "from AIRBranch.APBSubpartData " & _
                   "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                   "and substr(strSubPartKey, 13, 1) = '9') " & _
                   "and subStr(strAirProgramCodes, 8, 1) = '1' " & _
                   "or  exists (select * " & _
                   "from AIRBranch.APBSubpartData " & _
                   "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                   "and substr(strSubPartKey, 13, 1) = '8') " & _
                   "and subStr(strAirProgramCodes, 7, 1) = '1' ) " & _
                   "and AIRBranch.APBHeaderData.strAIRSnumber = " & _
                   "AIRBRANCH.APBFacilityInformation.strAIRsnumber " & _
                   "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " & _
                   "order by AIRSNumber "

                Case WorkViewerType.FacilitiesMissingSubparts
                    SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " & _
                    "strFacilityName " & _
                    "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " & _
                    "where (Not exists (select * " & _
                    "from AIRBranch.APBSubpartData " & _
                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                    "and substr(strSubPartKey, 13, 1) = 'M') " & _
                    "and subStr(strAirProgramCodes, 12, 1) = '1' " & _
                    "or Not exists (select * " & _
                    "from AIRBranch.APBSubpartData " & _
                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                    "and substr(strSubPartKey, 13, 1) = '9') " & _
                    "and subStr(strAirProgramCodes, 8, 1) = '1' " & _
                    "or Not exists (select * " & _
                    "from AIRBranch.APBSubpartData " & _
                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                    "and substr(strSubPartKey, 13, 1) = '8') " & _
                    "and subStr(strAirProgramCodes, 7, 1) = '1' ) " & _
                    "and AIRBranch.APBHeaderData.strAIRSnumber = " & _
                    "AIRBRANCH.APBFacilityInformation.strAIRsnumber " & _
                    "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " & _
                    "order by AIRSNumber "

                Case WorkViewerType.MonitoringTestReports_Staff
                    ' Requires :pId = ReviewingEngineer
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                        "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                        "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                        "and  Status = 'Open' " & _
                        " and ReviewingEngineer = :pId "

                Case WorkViewerType.MonitoringTestReports_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                        "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                        "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                        "and  Status = 'Open' " & _
                        "and strUserUnit = " & _
                          "(select strUnitDesc from AIRBranch.LookUpEPDUnits where numUnitCode = :pId) "

                Case WorkViewerType.MonitoringTestReports_PM
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                        "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                        "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                        "and  Status = 'Open' "

                Case WorkViewerType.MonitoringTestNotifications
                    SQL = "select  " & _
                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,    " & _
                    "case    " & _
                    "when strReferenceNumber is null then ''    " & _
                    "else strReferenceNumber    " & _
                    "end RefNum,    " & _
                    "case  " & _
                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''  " & _
                    "else AIRBranch.APBFacilityInformation.strFacilityName    " & _
                    "End FacilityName,  " & _
                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "strEmissionUnit,   " & _
                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,  " & _
                    "case  " & _
                    "when strFirstName is Null then ''  " & _
                    "else(strLastName||', '||strFirstName)   " & _
                    "END StaffResponsible  " & _
                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,  " & _
                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink  " & _
                    "where AIRBranch.ismptestnotification.strairsnumber = " & _
                    "AIRBRANCH.apbfacilityinformation.strairsnumber (+)    " & _
                    "and AIRBranch.ismptestnotification.strstaffresponsible = " & _
                    "AIRBRANCH.EPDUserProfiles.numUserID (+)  " & _
                    "and AIRBranch.ISMPTestnotification.strTestLogNumber = " & _
                    "AIRBRANCH.ISMPTestLogLink.strTestLogNumber (+)   " & _
                    "and datProposedStartDate > (sysdate - 180)    " & _
                    "and strReferenceNumber is null    " & _
                    "union    " & _
                    "select    " & _
                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,  " & _
                    "AIRBranch.ISMpReportInformation.strReferenceNumber as RefNum,    " & _
                    "case  " & _
                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''  " & _
                    "else AIRBranch.APBFacilityInformation.strFacilityName    " & _
                    "End FacilityName,  " & _
                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "strEmissionUnit,   " & _
                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,  " & _
                    "case  " & _
                    "when strFirstName is Null then ''  " & _
                    "else(strLastName||', '||strFirstName)   " & _
                    "END StaffResponsible  " & _
                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,  " & _
                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink,    " & _
                    "AIRBranch.ISMPReportInformation    " & _
                    "where AIRBranch.ismptestnotification.strairsnumber = " & _
                    "AIRBRANCH.apbfacilityinformation.strairsnumber (+)    " & _
                    "and AIRBranch.ismptestnotification.strstaffresponsible = " & _
                    "AIRBRANCH.EPDUserProfiles.numUserID (+)  " & _
                    "and AIRBranch.ISMPTestNotification.strTestLogNumber = " & _
                    "AIRBRANCH.ISMPTestLogLink.strTestLogNumber (+)    " & _
                    "and AIRBranch.ISMPTestLogLink.strReferencenumber = " & _
                    "AIRBRANCH.ISMPReportInformation.strReferenceNumber (+)    " & _
                    "and datProposedStartDate > (sysdate - 180)    " & _
                    "and AIRBranch.ISMPTestLogLink.strReferenceNumber is not null    " & _
                    "and strClosed = 'False'  "

                Case WorkViewerType.PermitApplications_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "Select " & _
              "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " & _
              "case " & _
              " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " & _
              "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " & _
              "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " & _
              "end as strAIRSNumber, " & _
              "case " & _
              "	when strApplicationTypeDesc IS Null then ' ' " & _
              "Else strApplicationTypeDesc " & _
              "End as strApplicationType, " & _
              "case " & _
              " 	when datReceivedDate is Null then ' ' " & _
              "Else to_char(datReceivedDate, 'RRRR-MM-dd') " & _
              " End as datReceivedDate, " & _
              "case  " & _
              "when strPermitNumber is NULL then ' '  " & _
              "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
              " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
              " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
              "end As strPermitNumber, " & _
              "case " & _
              " 	when numUserID= '0' then ' ' " & _
              "	when numUserID is Null then ' ' " & _
              "else (strLastName||', '||strFirstName) " & _
              "end as StaffResponsible, " & _
              "case  " & _
              "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " & _
              "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
              "when datToDirector is Not Null and datFinalizedDate is Null " & _
              "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " & _
              "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
              "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " & _
              "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " & _
              "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " & _
              "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " & _
              "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " & _
              "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " & _
              "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " & _
              "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " & _
              "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " & _
              "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " & _
              "end as StatusDate,  " & _
              "case  " & _
              " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " & _
              "else AIRBranch.SSPPApplicationData.strFacilityName  " & _
              "end as strFacilityName,  " & _
              "case " & _
              "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " & _
              "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " & _
              "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
              "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " & _
              "when datEPAEnds is not Null then '08 - EPA 45-day Review' " & _
              "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " & _
              "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " & _
              "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " & _
              "when dattoPMII is Not Null then '04 - AT PM'  " & _
              "when dattoPMI is Not Null then '03 - At UC'  " & _
              "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " & _
              "then '02 - Internal Review' " & _
              "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " & _
              "else '01 - At Engineer'  " & _
              "end as AppStatus, " & _
              "case " & _
              " 	when strPermitTypeDescription is Null then '' " & _
              "else strPermitTypeDescription " & _
              "End as strPermitType " & _
              "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " & _
              "AIRBranch.SSPPApplicationData, " & _
              "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " & _
              "AIRBranch.EPDuserProfiles  " & _
              "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " & _
              "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " & _
              "and strApplicationType = strApplicationTypeCode (+) " & _
              "and strPermitType = strPermitTypeCode (+) " & _
              "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
              "and datFinalizedDate is NULL " & _
              "and numUserID = :pId " & _
                      "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                Case WorkViewerType.PermitApplications_UC
                    ' Requires :pId = UserUnit
                    SQL = "Select " & _
                    "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " & _
                    "case " & _
                    " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " & _
                    "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " & _
                    "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " & _
                    "end as strAIRSNumber, " & _
                    "case " & _
                    "	when strApplicationTypeDesc IS Null then ' ' " & _
                    "Else strApplicationTypeDesc " & _
                    "End as strApplicationType, " & _
                    "case " & _
                    " 	when datReceivedDate is Null then ' ' " & _
                    "Else to_char(datReceivedDate, 'RRRR-MM-dd') " & _
                    " End as datReceivedDate, " & _
                    "case  " & _
                    "when strPermitNumber is NULL then ' '  " & _
                    "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
                    " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
                    " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
                    "end As strPermitNumber, " & _
                    "case " & _
                    " 	when numUserID= '0' then ' ' " & _
                    "	when numUserID is Null then ' ' " & _
                    "else (strLastName||', '||strFirstName) " & _
                    "end as StaffResponsible, " & _
                    "case  " & _
                    "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " & _
                    "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
                    "when datToDirector is Not Null and datFinalizedDate is Null " & _
                    "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " & _
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " & _
                    "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " & _
                    "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " & _
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " & _
                    "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " & _
                    "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " & _
                    "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " & _
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " & _
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " & _
                    "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " & _
                    "end as StatusDate,  " & _
                    "case  " & _
                    " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " & _
                    "else AIRBranch.SSPPApplicationData.strFacilityName  " & _
                    "end as strFacilityName,  " & _
                    "case " & _
                    "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " & _
                    "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " & _
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " & _
                    "when datEPAEnds is not Null then '08 - EPA 45-day Review' " & _
                    "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " & _
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " & _
                    "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " & _
                    "when dattoPMII is Not Null then '04 - AT PM'  " & _
                    "when dattoPMI is Not Null then '03 - At UC'  " & _
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " & _
                    "then '02 - Internal Review' " & _
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " & _
                    "else '01 - At Engineer'  " & _
                    "end as AppStatus, " & _
                    "case " & _
                    " 	when strPermitTypeDescription is Null then '' " & _
                    "else strPermitTypeDescription " & _
                    "End as strPermitType " & _
                    "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " & _
                    "AIRBranch.SSPPApplicationData, " & _
                    "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " & _
                    "AIRBranch.EPDuserProfiles  " & _
                    "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " & _
                    "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " & _
                    "and strApplicationType = strApplicationTypeCode (+) " & _
                    "and strPermitType = strPermitTypeCode (+) " & _
                    "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                    "and datFinalizedDate is NULL " & _
                    " and (AIRBranch.EPDUserProfiles.numUnit = :pId   or (APBUnit = :pId))  " & _
                      "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                Case WorkViewerType.PermitApplications_PM
                    SQL = "Select " & _
                      "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " & _
                      "case " & _
                      " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " & _
                      "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " & _
                      "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " & _
                      "end as strAIRSNumber, " & _
                      "case " & _
                      "	when strApplicationTypeDesc IS Null then ' ' " & _
                      "Else strApplicationTypeDesc " & _
                      "End as strApplicationType, " & _
                      "case " & _
                      " 	when datReceivedDate is Null then ' ' " & _
                      "Else to_char(datReceivedDate, 'RRRR-MM-dd') " & _
                      " End as datReceivedDate, " & _
                      "case  " & _
                      "when strPermitNumber is NULL then ' '  " & _
                      "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
                      " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
                      " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
                      "end As strPermitNumber, " & _
                      "case " & _
                      " 	when numUserID= '0' then ' ' " & _
                      "	when numUserID is Null then ' ' " & _
                      "else (strLastName||', '||strFirstName) " & _
                      "end as StaffResponsible, " & _
                      "case  " & _
                      "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " & _
                      "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
                      "when datToDirector is Not Null and datFinalizedDate is Null " & _
                      "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " & _
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " & _
                      "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " & _
                      "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " & _
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " & _
                      "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " & _
                      "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " & _
                      "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " & _
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " & _
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " & _
                      "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " & _
                      "end as StatusDate,  " & _
                      "case  " & _
                      " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " & _
                      "else AIRBranch.SSPPApplicationData.strFacilityName  " & _
                      "end as strFacilityName,  " & _
                      "case " & _
                      "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " & _
                      "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " & _
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " & _
                      "when datEPAEnds is not Null then '08 - EPA 45-day Review' " & _
                      "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " & _
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " & _
                      "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " & _
                      "when dattoPMII is Not Null then '04 - AT PM'  " & _
                      "when dattoPMI is Not Null then '03 - At UC'  " & _
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " & _
                      "then '02 - Internal Review' " & _
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " & _
                      "else '01 - At Engineer'  " & _
                      "end as AppStatus, " & _
                      "case " & _
                      " 	when strPermitTypeDescription is Null then '' " & _
                      "else strPermitTypeDescription " & _
                      "End as strPermitType " & _
                      "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " & _
                      "AIRBranch.SSPPApplicationData, " & _
                      "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " & _
                      "AIRBranch.EPDuserProfiles  " & _
                      "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " & _
                      "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " & _
                      "and strApplicationType = strApplicationTypeCode (+) " & _
                      "and strPermitType = strPermitTypeCode (+) " & _
                      "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                      "and datFinalizedDate is NULL " & _
                      "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                Case WorkViewerType.SBEAP_Staff
                    ' Requires :pId = CurrentUser.UserID
                    SQL = "select * " & _
                    "from AIRBRANCH.VW_SBEAP_CaseLog " & _
                    "where caseclosed is null " & _
                    "and numstaffresponsible = :pId " & _
                    "order by numcaseid "

                Case WorkViewerType.SBEAP_Program
                    SQL = "select * " & _
                    "from AIRBRANCH.VW_SBEAP_CaseLog " & _
                    "where caseclosed is null " & _
                    "order by numcaseid "

                Case Else
                    SQL = ""

            End Select

            Return SQL
        End Function

    End Module
End Namespace