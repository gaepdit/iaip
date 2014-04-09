Imports Oracle.DataAccess.Client
Imports System.Xml

Public Class SBEAPPrintForms
    Dim dr2 As OracleDataReader
    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Private Sub SBEAPPrintForms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            TCPrintForms.TabPages.Remove(TPPrintOuts)
            TCPrintForms.TabPages.Remove(TP2)
            TCPrintForms.TabPages.Remove(TP3)

            Select Case txtOrigin.Text
                Case "Case Work"
                    TCPrintForms.TabPages.Add(TPPrintOuts)
                    'PrintCaseWork()
                    PrintCaseWork2()

                Case Else

            End Select

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Print Outs"
    Sub PrintCaseWork()
        Try
            Dim rpt As New crCaseSummary

            Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
            Dim ParameterField As CrystalDecisions.Shared.ParameterField
            Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

            Dim CaseID As String = ""
            Dim Client As String = ""
            Dim StaffResponsible As String = ""
            Dim DateOpened As String = ""
            Dim DateClosed As String = ""
            Dim CaseSummary As String = ""
            Dim CompliantBased As String = ""
            Dim DateReferred As String = ""
            Dim ReferralComments As String = ""
            Dim ActionText As String = ""
            Dim ActionID As String = ""
            Dim ActionType As String = ""
            Dim Action As String = ""
            Dim CreatingStaff As String = ""
            Dim DateCreated As String = ""
            Dim DateOccured As String = ""

            Dim AssistLevel As String = ""
            Dim InitialContact As String = ""
            Dim AssistStart As String = ""
            Dim AssistEnd As String = ""
            Dim AssistRequest As String = ""
            Dim AIRSNumber As String = ""
            Dim AssistNotes As String = ""
            Dim CallerInfo As String = ""
            Dim PhoneNumber As String = ""
            Dim OneTimeAssist As String = ""
            Dim FrontDeskCall As String = ""
            Dim CallNotes As String = ""

            Dim Conference As String = ""
            Dim ConLocation As String = ""
            Dim ConTopic As String = ""
            Dim ConStart As String = ""
            Dim StaffAttending As String = ""
            Dim Presentation As String = ""
            Dim Attendees As String = ""
            Dim BusinessSectors As String = ""
            Dim ConFollowUp As String = ""

            Dim CaseNotes As String = ""

            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "select " & _
            "AIRBranch.SBEAPCaseLog.numCaseID,  " & _
            "numStaffResponsible,  " & _
            "(strLastName||', '||strFirstName) as StaffResponsible,  " & _
            "AIRBranch.SBEAPClients.ClientID, " & _
            "strCompanyName, " & _
            "datCaseOpened,  " & _
            "strCaseSummary,  " & _
            "datCaseClosed,  " & _
            "strReferralComments,  " & _
            "datReferralDate, strComplaintBased  " & _
            "from AIRBranch.SBEAPCaseLog, AIRBranch.EPDUserProfiles, " & _
            "AIRBranch.SBEAPClients, AIRBranch.SBEAPCaseLogLink " & _
            "where AIRBranch.SBEAPCaseLog.numStaffResponsible = AIRBranch.EPDUserProfiles.numUserID (+)   " & _
            "and AIRBranch.SBEAPCaseLog.numCaseID = AIRBranch.SBEAPCaseLogLink.numCaseID (+) " & _
            "and AIRBranch.SBEAPCaseLogLink.ClientID = AIRBranch.SBEAPClients.ClientID (+)  " & _
            "and  AIRBranch.SBEAPCaseLog.numCaseID = '" & txtSource.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numCaseID")) Then
                    CaseID = " "
                Else
                    CaseID = dr.Item("numCaseID") & vbCrLf
                End If
                If IsDBNull(dr.Item("ClientID")) Then
                    Client = " "
                Else
                    Client = " "
                    If IsDBNull(dr.Item("strCompanyName")) Then
                        Client = Client & " - "
                    Else
                        Client = Client & " - " & dr.Item("strCompanyName") & vbCrLf
                    End If
                End If
                If IsDBNull(dr.Item("StaffResponsible")) Then
                    StaffResponsible = " "
                Else
                    StaffResponsible = dr.Item("StaffResponsible")
                End If
                If IsDBNull(dr.Item("datCaseOpened")) Then
                    DateOpened = " "
                Else
                    DateOpened = Format(dr.Item("datCaseOpened"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("datCaseClosed")) Then
                    DateClosed = " "
                Else
                    DateClosed = Format(dr.Item("datCaseClosed"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("strCaseSummary")) Then
                    CaseSummary = " "
                Else
                    CaseSummary = dr.Item("strCaseSummary")
                End If
                If IsDBNull(dr.Item("strComplaintBased")) Then
                    CompliantBased = "NO"
                Else
                    If dr.Item("strComplaintBased") = True Then
                        CompliantBased = "YES"
                    Else
                        CompliantBased = "NO"
                    End If
                End If
                If IsDBNull(dr.Item("datReferralDate")) Then
                    DateReferred = "N/A"
                Else
                    DateReferred = Format(dr.Item("datReferralDate"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("strReferralComments")) Then
                    ReferralComments = "N/A"
                Else
                    ReferralComments = dr.Item("strReferralComments")
                End If
            End While
            dr.Close()

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "CaseID"
            spValue.Value = CaseID
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Client"
            spValue.Value = Client
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "StaffResponsible"
            spValue.Value = StaffResponsible
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "DateOpened"
            spValue.Value = DateOpened
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "DateClosed"
            spValue.Value = DateClosed
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "CaseSummary"
            spValue.Value = CaseSummary
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "CompliantBased"
            spValue.Value = CompliantBased
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "DateReferred"
            spValue.Value = DateReferred
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "ReferralComments"
            spValue.Value = ReferralComments
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            SQL = "select " & _
            "AIRBranch.SBEAPActionLog.numactionID, " & _
            "AIRBranch.SBEAPActionLog.numActionType, " & _
            "strWorkDescription, " & _
            "strCreatingStaff, " & _
            "case " & _
            "when strCreatingStaff is null then '' " & _
            "else (strLastName||', '||strFirstName) " & _
            "End CreatingStaff, " & _
            "datCreationDate, datActionOccured  " & _
            "from AIRBranch.SBEAPActionLog, AIRBranch.EPDUserProfiles, " & _
            "AIRBranch.LookUpSBEAPCaseWork " & _
            "where AIRBranch.SBEAPActionLog.strCreatingStaff = AIRBranch.EPDUserProfiles.numUserID (+) " & _
            "and AIRBranch.SBEAPActionLog.numActionType = AIRBranch.LookUpSBEAPCaseWork.numActionType " & _
            "and  AIRBranch.SBEAPActionLog.numCaseID = '" & txtSource.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numActionID")) Then
                    ActionID = ""
                Else
                    ActionID = dr.Item("numActionID")
                End If
                If IsDBNull(dr.Item("numActionType")) Then
                    ActionType = ""
                Else
                    ActionType = dr.Item("numActionType")
                End If
                If IsDBNull(dr.Item("strWorkDescription")) Then
                    Action = ""
                Else
                    Action = dr.Item("strWorkDescription")
                End If
                If IsDBNull(dr.Item("CreatingStaff")) Then
                    CreatingStaff = ""
                Else
                    CreatingStaff = dr.Item("CreatingStaff")
                End If
                If IsDBNull(dr.Item("datCreationDate")) Then
                    DateCreated = ""
                Else
                    DateCreated = Format(dr.Item("datCreationDate"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("datActionOccured")) Then
                    DateOccured = ""
                Else
                    DateOccured = Format(dr.Item("datActionOccured"), "dd-MMM-yyyy")
                End If

                ActionText = ActionText & _
                "Date Action Occured: " & DateOccured & vbCrLf & _
                "Date Action Created: " & DateCreated & vbTab & vbTab & "Creating Staff: " & CreatingStaff & vbCrLf & _
                "Action Type: " & Action & vbCrLf & vbCrLf

                Select Case ActionType
                    Case "10" 'Technical Assist
                        SQL = "select " & _
                        "strTechnicalAssistType, " & _
                        "datInitialContactDate, " & _
                        "datAssistStartDate, " & _
                        "datAssistEndDate, " & _
                        "strAssistanceRequest, strAIRSnumber, " & _
                        "strTechnicalAssistNotes " & _
                        "from AIRBranch.SBEAPTechnicalAssist " & _
                        "where numActionID = '" & ActionID & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strTechnicalAssistType")) Then
                                AssistLevel = ""
                            Else
                                AssistLevel = dr2.Item("strTechnicalAssistType")
                            End If
                            If IsDBNull(dr2.Item("datInitialContactDate")) Then
                                InitialContact = ""
                            Else
                                InitialContact = dr2.Item("datInitialContactDate")
                            End If
                            If IsDBNull(dr2.Item("datAssistStartDate")) Then
                                AssistStart = ""
                            Else
                                AssistStart = dr2.Item("datAssistStartDate")
                            End If
                            If IsDBNull(dr2.Item("datAssistEndDate")) Then
                                AssistEnd = ""
                            Else
                                AssistEnd = dr2.Item("datAssistEndDate")
                            End If
                            If IsDBNull(dr2.Item("strAssistanceRequest")) Then
                                AssistRequest = ""
                            Else
                                AssistRequest = dr2.Item("strAssistanceRequest")
                            End If
                            If IsDBNull(dr2.Item("strAIRSnumber")) Then
                                AIRSNumber = ""
                            Else
                                AIRSNumber = dr2.Item("strAIRSnumber")
                            End If
                            If IsDBNull(dr2.Item("strTechnicalAssistNotes")) Then
                                AssistNotes = ""
                            Else
                                AssistNotes = dr2.Item("strTechnicalAssistNotes")
                            End If
                        End While
                        dr2.Close()

                        ActionText = ActionText & "Assist Level: " & AssistLevel & vbCrLf & _
                        "Inital Contact Date: " & InitialContact & vbTab & vbTab & "Assit Start: " & AssistStart & vbTab & vbTab & "Assist End: " & AssistEnd & vbCrLf & _
                        "Assist " & vbCrLf & _
                        "Notes: " & AssistNotes & vbCrLf & vbCrLf
                    Case "6" 'Phone Calls
                        SQL = "select " & _
                        "strCallerInformation, " & _
                        "replace(to_char(numCallerPhoneNumber, '999,999,9999'), ',','-') as PhoneNumber, " & _
                        "strPhoneLogNotes, strOneTimeAssist, " & _
                        "strFrontDeskCall  " & _
                        "from airbranch.SBEAPPhoneLog " & _
                        "where numActionID = '" & ActionID & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strCallerInformation")) Then
                                CallerInfo = "N/A"
                            Else
                                CallerInfo = dr2.Item("strCallerInformation")
                            End If
                            If IsDBNull(dr2.Item("PhoneNumber")) Then
                                PhoneNumber = " - "
                            Else
                                PhoneNumber = dr2.Item("PhoneNumber")
                            End If
                            If IsDBNull(dr2.Item("strOneTimeAssist")) Then
                                OneTimeAssist = "No"
                            Else
                                If dr2.Item("strOneTimeAssist") = "True" Then
                                    OneTimeAssist = "Yes"
                                Else
                                    OneTimeAssist = "No"
                                End If
                            End If
                            If IsDBNull(dr2.Item("strFrontDeskCall")) Then
                                FrontDeskCall = "No"
                            Else
                                If dr2.Item("strFrontDeskCall") = "True" Then
                                    FrontDeskCall = "Yes"
                                Else
                                    FrontDeskCall = "No"
                                End If
                            End If
                            If IsDBNull(dr2.Item("strPhoneLogNotes")) Then
                                CallNotes = "N/A"
                            Else
                                CallNotes = dr2.Item("strPhoneLogNotes")
                            End If
                        End While
                        dr2.Close()
                        ActionText = ActionText & vbTab & vbTab & "Caller Information: " & CallerInfo & vbCrLf & _
                                     vbTab & vbTab & "Phone Number: " & PhoneNumber & vbCrLf & _
                                     vbTab & vbTab & "One Time Assist: " & OneTimeAssist & vbTab & vbTab & _
                                     vbTab & vbTab & "Front Desk Call: " & FrontDeskCall & vbCrLf & _
                                     vbTab & vbTab & vbTab & "Call Notes: " & CallNotes & vbCrLf & _
                                     "___________________________________________________________________________________________________" & vbCrLf & vbCrLf
                    Case "4" 'Conferences
                        SQL = "select  " & _
                        "strConferenceAttended, " & _
                        "strConferenceLocation, " & _
                        "strConferenceTopic, " & _
                        "datConferenceStarted, " & _
                        "strStaffAttending, " & _
                        "strSBEAPPresentation, " & _
                        "strAttendees, " & _
                        "strListOfBusinessSectors, " & _
                        "strConferenceFollowUp " & _
                        "from AIRBranch.SBEAPConferenceLog  " & _
                        "where numActionID = '" & ActionID & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strConferenceAttended")) Then
                                Conference = "N/A"
                            Else
                                Conference = dr2.Item("strConferenceAttended")
                            End If
                            If IsDBNull(dr2.Item("strConferenceLocation")) Then
                                ConLocation = "N/A"
                            Else
                                ConLocation = dr2.Item("strConferenceLocation")
                            End If
                            If IsDBNull(dr2.Item("strConferenceTopic")) Then
                                ConTopic = "N/A"
                            Else
                                ConTopic = dr2.Item("strConferenceTopic")
                            End If
                            If IsDBNull(dr2.Item("datConferenceStarted")) Then
                                ConStart = "N/A"
                            Else
                                ConStart = dr2.Item("datConferenceStarted")
                            End If
                            If IsDBNull(dr2.Item("strStaffAttending")) Then
                                StaffAttending = "N/A"
                            Else
                                StaffAttending = dr2.Item("strStaffAttending")
                            End If
                            If IsDBNull(dr2.Item("strSBEAPPresentation")) Then
                                Presentation = "No"
                            Else
                                If dr2.Item("strSBEAPPresentation") = "True" Then
                                    Presentation = "Yes"
                                Else
                                    Presentation = "No"
                                End If
                            End If
                            If IsDBNull(dr2.Item("strAttendees")) Then
                                Attendees = "N/A"
                            Else
                                Attendees = dr2.Item("strAttendees")
                            End If
                            If IsDBNull(dr2.Item("strListOfBusinessSectors")) Then
                                BusinessSectors = "N/A"
                            Else
                                BusinessSectors = dr2.Item("strListOfBusinessSectors")
                            End If
                            If IsDBNull(dr2.Item("strConferenceFollowUp")) Then
                                ConFollowUp = "No"
                            Else
                                ConFollowUp = dr2.Item("strConferenceFollowUp")
                            End If
                        End While
                        dr2.Close()
                        ActionText = ActionText & vbTab & vbTab & "Meeting/Conference Attended: " & Conference & vbCrLf & _
                        vbTab & vbTab & "Location: " & ConLocation & vbCrLf & _
                        vbTab & vbTab & "Meeting/Confernece Topic: " & ConTopic & vbCrLf & _
                        vbTab & vbTab & "Date: " & ConStart & vbCrLf & _
                        vbTab & vbTab & "Attending Staff: " & StaffAttending & vbCrLf & vbCrLf & _
                        vbTab & vbTab & "SBEAP Presentation: " & Presentation & vbTab & vbTab & "# of Attendees: " & Attendees & vbCrLf & _
                        vbTab & vbTab & "Business Secotors/Organizations Present: " & BusinessSectors & vbCrLf & _
                        vbTab & vbTab & "Follow Up Activities: " & ConFollowUp & vbCrLf & _
                        "___________________________________________________________________________________________________" & vbCrLf & vbCrLf

                    Case Else
                        SQL = "Select " & _
                        "strCaseNotes " & _
                        "from " & DBNameSpace & ".SBEAPOtherLog " & _
                        "where numActionId = '" & ActionID & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strCaseNotes")) Then
                                casenotes = ""
                            Else
                                casenotes = dr2.Item("strCaseNotes")
                            End If
                        End While
                        dr2.Close()
                        ActionText = ActionText & vbTab & vbTab & "Other Notes: " & CaseNotes & vbCrLf & _
                        "___________________________________________________________________________________________________" & vbCrLf & vbCrLf

                End Select
            End While
            dr.Close()

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "ActionDesc"
            spValue.Value = ActionText
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            CRVDemo.ParameterFieldInfo = ParameterFields

            'Display the Report
            CRVDemo.ReportSource = rpt

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Sub PrintCaseWork2()
        Try
            Dim rpt As New crCaseReport

            Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
            Dim ParameterField As CrystalDecisions.Shared.ParameterField
            Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

            Dim CaseID As String = ""
            Dim Client As String = ""
            Dim StaffResponsible As String = ""
            Dim DateOpened As String = ""
            Dim DateClosed As String = ""
            Dim CaseSummary As String = ""
            Dim CompliantBased As String = ""
            Dim DateReferred As String = ""
            Dim ReferralComments As String = ""
            Dim ActionText As String = ""
            Dim ActionID As String = ""
            Dim ActionType As String = ""
            Dim Action As String = ""
            Dim CreatingStaff As String = ""
            Dim DateCreated As String = ""
            Dim DateOccured As String = ""

            Dim AssistLevel As String = ""
            Dim InitialContact As String = ""
            Dim AssistStart As String = ""
            Dim AssistEnd As String = ""
            Dim AssistRequest As String = ""
            Dim AIRSNumber As String = ""
            Dim AssistNotes As String = ""
            Dim CallerInfo As String = ""
            Dim PhoneNumber As String = ""
            Dim OneTimeAssist As String = ""
            Dim FrontDeskCall As String = ""
            Dim CallNotes As String = ""

            Dim Conference As String = ""
            Dim ConLocation As String = ""
            Dim ConTopic As String = ""
            Dim ConStart As String = ""
            Dim StaffAttending As String = ""
            Dim Presentation As String = ""
            Dim Attendees As String = ""
            Dim BusinessSectors As String = ""
            Dim ConFollowUp As String = ""

            Dim CaseNotes As String = ""

            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "select " & _
            "AIRBranch.SBEAPCaseLog.numCaseID,  " & _
            "numStaffResponsible,  " & _
            "(strLastName||', '||strFirstName) as StaffResponsible,  " & _
            "AIRBranch.SBEAPClients.ClientID, " & _
            "strCompanyName, " & _
            "datCaseOpened,  " & _
            "strCaseSummary,  " & _
            "datCaseClosed,  " & _
            "strReferralComments,  " & _
            "datReferralDate, strComplaintBased  " & _
            "from AIRBranch.SBEAPCaseLog, AIRBranch.EPDUserProfiles, " & _
            "AIRBranch.SBEAPClients, AIRBranch.SBEAPCaseLogLink " & _
            "where AIRBranch.SBEAPCaseLog.numStaffResponsible = AIRBranch.EPDUserProfiles.numUserID (+)   " & _
            "and AIRBranch.SBEAPCaseLog.numCaseID = AIRBranch.SBEAPCaseLogLink.numCaseID (+) " & _
            "and AIRBranch.SBEAPCaseLogLink.ClientID = AIRBranch.SBEAPClients.ClientID (+)  " & _
            "and  AIRBranch.SBEAPCaseLog.numCaseID = '" & txtSource.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numCaseID")) Then
                    CaseID = " "
                Else
                    CaseID = dr.Item("numCaseID") & vbCrLf
                End If
                If IsDBNull(dr.Item("ClientID")) Then
                    Client = " "
                Else
                    Client = " "
                    If IsDBNull(dr.Item("strCompanyName")) Then
                        Client = Client & " - "
                    Else
                        Client = Client & " - " & dr.Item("strCompanyName") & vbCrLf
                    End If
                End If
                If IsDBNull(dr.Item("StaffResponsible")) Then
                    StaffResponsible = " "
                Else
                    StaffResponsible = dr.Item("StaffResponsible")
                End If
                If IsDBNull(dr.Item("datCaseOpened")) Then
                    DateOpened = " "
                Else
                    DateOpened = Format(dr.Item("datCaseOpened"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("datCaseClosed")) Then
                    DateClosed = " "
                Else
                    DateClosed = Format(dr.Item("datCaseClosed"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("strCaseSummary")) Then
                    CaseSummary = " "
                Else
                    CaseSummary = dr.Item("strCaseSummary")
                End If
                If IsDBNull(dr.Item("strComplaintBased")) Then
                    CompliantBased = "NO"
                Else
                    If dr.Item("strComplaintBased") = True Then
                        CompliantBased = "YES"
                    Else
                        CompliantBased = "NO"
                    End If
                End If
                If IsDBNull(dr.Item("datReferralDate")) Then
                    DateReferred = "N/A"
                Else
                    DateReferred = Format(dr.Item("datReferralDate"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("strReferralComments")) Then
                    ReferralComments = "N/A"
                Else
                    ReferralComments = dr.Item("strReferralComments")
                End If
            End While
            dr.Close()

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "CaseID"
            spValue.Value = CaseID
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Client"
            spValue.Value = Client
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "StaffResponsible"
            spValue.Value = StaffResponsible
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "DateOpened"
            spValue.Value = DateOpened
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "DateClosed"
            spValue.Value = DateClosed
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "CaseSummary"
            spValue.Value = CaseSummary
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "CompliantBased"
            spValue.Value = CompliantBased
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "DateReferred"
            spValue.Value = DateReferred
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "ReferralComments"
            spValue.Value = ReferralComments
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            SQL = "select " & _
            "AIRBranch.SBEAPActionLog.numactionID, " & _
            "AIRBranch.SBEAPActionLog.numActionType, " & _
            "strWorkDescription, " & _
            "strCreatingStaff, " & _
            "case " & _
            "when strCreatingStaff is null then '' " & _
            "else (strLastName||', '||strFirstName) " & _
            "End CreatingStaff, " & _
            "datCreationDate, datActionOccured  " & _
            "from AIRBranch.SBEAPActionLog, AIRBranch.EPDUserProfiles, " & _
            "AIRBranch.LookUpSBEAPCaseWork " & _
            "where AIRBranch.SBEAPActionLog.strCreatingStaff = AIRBranch.EPDUserProfiles.numUserID (+) " & _
            "and AIRBranch.SBEAPActionLog.numActionType = AIRBranch.LookUpSBEAPCaseWork.numActionType " & _
            "and  AIRBranch.SBEAPActionLog.numCaseID = '" & txtSource.Text & "' "

            ds = New DataSet
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "CaseActions")
            rpt.SetDataSource(ds)

            CRVDemo.ParameterFieldInfo = ParameterFields
            CRVDemo.ReportSource = rpt
            DisplayReport(CRVDemo, "Hello World")
            'Display the Report
            CRVDemo.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region

   
End Class