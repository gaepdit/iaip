Imports System.Data.OracleClient


Public Class IAIPEditContacts
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsContacts As DataSet
    Dim daContacts As OracleDataAdapter

    Private Sub APBAddContacts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate
            LoadContactsDataset("PageLoad")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#Region "Page Load"
    Sub LoadContactsDataset(ByVal LoadSource As String)
        Dim key As String = ""
        Dim ContactKey As String = ""
        Dim UserProgram As String = ""

        Try

            If txtAIRSNumber.Text <> "" Then
                If LoadSource = "PageLoad" Then
                    lblKeySource.Text = "Air Protection Branch"

                    Select Case UserBranch
                        Case "1"
                            Select Case UserProgram
                                Case "1"
                                    lblKeySource.Text = "Mobile & Area"
                                Case "2"
                                    lblKeySource.Text = "Planning & Support"
                                Case "3"
                                    lblKeySource.Text = "ISMP"
                                Case "4"
                                    lblKeySource.Text = "SSCP"
                                Case "5"
                                    lblKeySource.Text = "SSPP"
                                Case "6"
                                    lblKeySource.Text = "Ambient Monitoring"
                                Case Else
                                    lblKeySource.Text = "Air Protection Branch"
                            End Select

                        Case "2"

                        Case "5"
                            Select Case UserProgram
                                Case "7", "9", "10", "11", "12", "13", "14", "15"
                                    lblKeySource.Text = "District"
                                Case Else
                                    lblKeySource.Text = "Air Protection Branch"
                            End Select
                        Case Else
                            lblKeySource.Text = "Air Protection Branch"
                    End Select

                    Select Case UserProgram
                        Case "Air Protection Branch"
                            'lblKeySource.Text = "Air Protection Branch"
                        Case "Ambient Monitoring"
                            'lblKeySource.Text = "Ambient Monitoring"
                        Case "District"
                            'lblKeySource.Text = "District"
                        Case "Industrial Source Monitoring"
                            'lblKeySource.Text = "ISMP"
                        Case "Mobile & Area Sources"
                            'lblKeySource.Text = "Mobile & Area"
                        Case "Planning & Support"
                            'lblKeySource.Text = "Planning & Support"
                        Case "Stationary Source Compliance"
                            'lblKeySource.Text = "SSCP"
                        Case "Stationary Source Permitting"
                            'lblKeySource.Text = "SSPP"
                        Case Else
                            'lblKeySource.Text = "Air Protection Branch"
                    End Select
                Else
                    lblKeySource.Text = LoadSource
                End If

                If clbContactKey.Items.Count <> 0 Then
                    clbContactKey.Items.Clear()
                End If

                Select Case lblKeySource.Text
                    Case "ISMP"
                        SQL = "Select " & _
                        "strContactKey, " & _
                        "strContactFirstName, strContactLastname, " & _
                        "strContactPrefix, strContactSuffix, strContactTitle, " & _
                        "strContactCompanyName, strContactPhoneNumber1, " & _
                        "Case  " & _
                        "    when strContactPhoneNumber2 is NULL then 'N/A' " & _
                        "    Else strContactPhoneNumber2 " & _
                        "END as ContactPhoneNumber2,  " & _
                        "case " & _
                        "    when strContactFaxNumber is Null then 'N/A' " & _
                        "    else strContactFaxNumber " & _
                        "END as strContactFaxNumber, " & _
                        "Case " & _
                        "    when strContactEmail is Null then 'N/A' " & _
                        "    ELSE strContactEmail " & _
                        "END as ContactEmail, " & _
                        "strContactAddress1, strContactAddress2, " & _
                        "strContactCity, strContactState, strContactZipCode, " & _
                        "Case " & _
                        "    when strContactDescription is Null then 'N/A' " & _
                        "    ELSE strContactDescription " & _
                        "END as ContactDescription " & _
                        "from " & connNameSpace & ".APBContactInformation " & _
                        "where strContactKey like '0413" & txtAIRSNumber.Text & "1%'"
                        clbContactKey.Items.Add("ISMP Contact 1", False)
                        clbContactKey.Items.Add("ISMP Contact 2", False)
                        clbContactKey.Items.Add("ISMP Contact 3", False)
                        clbContactKey.Items.Add("ISMP Contact 4", False)
                        clbContactKey.Items.Add("ISMP Contact 5", False)
                        clbContactKey.Items.Add("ISMP Contact 6", False)
                        clbContactKey.Items.Add("ISMP Contact 7", False)
                        clbContactKey.Items.Add("ISMP Contact 8", False)
                        clbContactKey.Items.Add("ISMP Contact 9", False)
                        clbContactKey.Items.Add("ISMP Contact 10", False)
                    Case "SSCP"
                        SQL = "Select " & _
                      "strContactKey, " & _
                      "strContactFirstName, strContactLastname, " & _
                      "strContactPrefix, strContactSuffix, strContactTitle, " & _
                      "strContactCompanyName, strContactPhoneNumber1, " & _
                      "Case  " & _
                      "    when strContactPhoneNumber2 is NULL then 'N/A' " & _
                      "    Else strContactPhoneNumber2 " & _
                      "END as ContactPhoneNumber2,  " & _
                      "case " & _
                      "    when strContactFaxNumber is Null then 'N/A' " & _
                      "    else strContactFaxNumber " & _
                      "END as strContactFaxNumber, " & _
                      "Case " & _
                      "    when strContactEmail is Null then 'N/A' " & _
                      "    ELSE strContactEmail " & _
                      "END as ContactEmail, " & _
                      "strContactAddress1, strContactAddress2, " & _
                      "strContactCity, strContactState, strContactZipCode, " & _
                      "Case " & _
                      "    when strContactDescription is Null then 'N/A' " & _
                      "    ELSE strContactDescription " & _
                      "END as ContactDescription " & _
                      "from " & connNameSpace & ".APBContactInformation " & _
                      "where strContactKey like '0413" & txtAIRSNumber.Text & "2%'"
                        clbContactKey.Items.Add("SSCP Contact 1", False)
                        clbContactKey.Items.Add("SSCP Contact 2", False)
                        clbContactKey.Items.Add("SSCP Contact 3", False)
                        clbContactKey.Items.Add("SSCP Contact 4", False)
                        clbContactKey.Items.Add("SSCP Contact 5", False)
                        clbContactKey.Items.Add("SSCP Contact 6", False)
                        clbContactKey.Items.Add("SSCP Contact 7", False)
                        clbContactKey.Items.Add("SSCP Contact 8", False)
                        clbContactKey.Items.Add("SSCP Contact 9", False)
                        clbContactKey.Items.Add("SSCP Contact 10", False)
                    Case "SSPP"
                        SQL = "Select " & _
                          "strContactKey, " & _
                          "strContactFirstName, strContactLastname, " & _
                          "strContactPrefix, strContactSuffix, strContactTitle, " & _
                          "strContactCompanyName, strContactPhoneNumber1, " & _
                          "Case  " & _
                          "    when strContactPhoneNumber2 is NULL then 'N/A' " & _
                          "    Else strContactPhoneNumber2 " & _
                          "END as ContactPhoneNumber2,  " & _
                          "case " & _
                          "    when strContactFaxNumber is Null then 'N/A' " & _
                          "    else strContactFaxNumber " & _
                          "END as strContactFaxNumber, " & _
                          "Case " & _
                          "    when strContactEmail is Null then 'N/A' " & _
                          "    ELSE strContactEmail " & _
                          "END as ContactEmail, " & _
                          "strContactAddress1, strContactAddress2, " & _
                          "strContactCity, strContactState, strContactZipCode, " & _
                          "Case " & _
                          "    when strContactDescription is Null then 'N/A' " & _
                          "    ELSE strContactDescription " & _
                          "END as ContactDescription " & _
                          "from " & connNameSpace & ".APBContactInformation " & _
                          "where strContactKey like '0413" & txtAIRSNumber.Text & "3%'"
                        clbContactKey.Items.Add("SSPP Contact 1", False)
                        clbContactKey.Items.Add("SSPP Contact 2", False)
                        clbContactKey.Items.Add("SSPP Contact 3", False)
                        clbContactKey.Items.Add("SSPP Contact 4", False)
                        clbContactKey.Items.Add("SSPP Contact 5", False)
                        clbContactKey.Items.Add("SSPP Contact 6", False)
                        clbContactKey.Items.Add("SSPP Contact 7", False)
                        clbContactKey.Items.Add("SSPP Contact 8", False)
                        clbContactKey.Items.Add("SSPP Contact 9", False)
                        clbContactKey.Items.Add("SSPP Contact 10", False)
                    Case "Web"
                        SQL = "Select " & _
                          "strContactKey, " & _
                          "strContactFirstName, strContactLastname, " & _
                          "strContactPrefix, strContactSuffix, strContactTitle, " & _
                          "strContactCompanyName, strContactPhoneNumber1, " & _
                          "Case  " & _
                          "    when strContactPhoneNumber2 is NULL then 'N/A' " & _
                          "    Else strContactPhoneNumber2 " & _
                          "END as ContactPhoneNumber2,  " & _
                          "case " & _
                          "    when strContactFaxNumber is Null then 'N/A' " & _
                          "    else strContactFaxNumber " & _
                          "END as strContactFaxNumber, " & _
                          "Case " & _
                          "    when strContactEmail is Null then 'N/A' " & _
                          "    ELSE strContactEmail " & _
                          "END as ContactEmail, " & _
                          "strContactAddress1, strContactAddress2, " & _
                          "strContactCity, strContactState, strContactZipCode, " & _
                          "Case " & _
                          "    when strContactDescription is Null then 'N/A' " & _
                          "    ELSE strContactDescription " & _
                          "END as ContactDescription " & _
                          "from " & connNameSpace & ".APBContactInformation " & _
                          "where strContactKey like '0413" & txtAIRSNumber.Text & "4%'"
                        clbContactKey.Items.Add("Fees Contact", False)
                        clbContactKey.Items.Add("Emission Inventory Contact", False)
                        clbContactKey.Items.Add("Emission Statement Contact", False)
                        clbContactKey.Items.Add("Web Contact 4", False)
                        clbContactKey.Items.Add("Web Contact 5", False)
                        clbContactKey.Items.Add("Web Contact 6", False)
                        clbContactKey.Items.Add("Web Contact 7", False)
                        clbContactKey.Items.Add("Web Contact 8", False)
                        clbContactKey.Items.Add("Web Contact 9", False)
                        clbContactKey.Items.Add("Web Contact 10", False)
                    Case "Air Protection Branch"
                        SQL = "Select " & _
                          "strContactKey, " & _
                          "strContactFirstName, strContactLastname, " & _
                          "strContactPrefix, strContactSuffix, strContactTitle, " & _
                          "strContactCompanyName, strContactPhoneNumber1, " & _
                          "Case  " & _
                          "    when strContactPhoneNumber2 is NULL then 'N/A' " & _
                          "    Else strContactPhoneNumber2 " & _
                          "END as ContactPhoneNumber2,  " & _
                          "case " & _
                          "    when strContactFaxNumber is Null then 'N/A' " & _
                          "    else strContactFaxNumber " & _
                          "END as strContactFaxNumber, " & _
                          "Case " & _
                          "    when strContactEmail is Null then 'N/A' " & _
                          "    ELSE strContactEmail " & _
                          "END as ContactEmail, " & _
                          "strContactAddress1, strContactAddress2, " & _
                          "strContactCity, strContactState, strContactZipCode, " & _
                          "Case " & _
                          "    when strContactDescription is Null then 'N/A' " & _
                          "    ELSE strContactDescription " & _
                          "END as ContactDescription " & _
                          "from " & connNameSpace & ".APBContactInformation " & _
                          "where strContactKey like '0413" & txtAIRSNumber.Text & "%'"
                        clbContactKey.Items.Add("A&M Contact 1", False)
                        clbContactKey.Items.Add("A&M Contact 2", False)
                        clbContactKey.Items.Add("A&M Contact 3", False)
                        clbContactKey.Items.Add("A&M Contact 4", False)
                        clbContactKey.Items.Add("A&M Contact 5", False)
                        clbContactKey.Items.Add("A&M Contact 6", False)
                        clbContactKey.Items.Add("A&M Contact 7", False)
                        clbContactKey.Items.Add("A&M Contact 8", False)
                        clbContactKey.Items.Add("A&M Contact 9", False)
                        clbContactKey.Items.Add("A&M Contact 10", False)
                        clbContactKey.Items.Add("District Contact 1", False)
                        clbContactKey.Items.Add("District Contact 2", False)
                        clbContactKey.Items.Add("District Contact 3", False)
                        clbContactKey.Items.Add("District Contact 4", False)
                        clbContactKey.Items.Add("District Contact 5", False)
                        clbContactKey.Items.Add("District Contact 6", False)
                        clbContactKey.Items.Add("District Contact 7", False)
                        clbContactKey.Items.Add("District Contact 8", False)
                        clbContactKey.Items.Add("District Contact 9", False)
                        clbContactKey.Items.Add("District Contact 10", False)
                        clbContactKey.Items.Add("Emission Inventory Contact", False)
                        clbContactKey.Items.Add("Emission Statement Contact", False)
                        clbContactKey.Items.Add("Fees Contact", False)
                        clbContactKey.Items.Add("ISMP Contact 1", False)
                        clbContactKey.Items.Add("ISMP Contact 2", False)
                        clbContactKey.Items.Add("ISMP Contact 3", False)
                        clbContactKey.Items.Add("ISMP Contact 4", False)
                        clbContactKey.Items.Add("ISMP Contact 5", False)
                        clbContactKey.Items.Add("ISMP Contact 6", False)
                        clbContactKey.Items.Add("ISMP Contact 7", False)
                        clbContactKey.Items.Add("ISMP Contact 8", False)
                        clbContactKey.Items.Add("ISMP Contact 9", False)
                        clbContactKey.Items.Add("ISMP Contact 10", False)
                        clbContactKey.Items.Add("P&S Contact 1", False)
                        clbContactKey.Items.Add("P&S Contact 2", False)
                        clbContactKey.Items.Add("P&S Contact 3", False)
                        clbContactKey.Items.Add("P&S Contact 4", False)
                        clbContactKey.Items.Add("P&S Contact 5", False)
                        clbContactKey.Items.Add("P&S Contact 6", False)
                        clbContactKey.Items.Add("P&S Contact 7", False)
                        clbContactKey.Items.Add("P&S Contact 8", False)
                        clbContactKey.Items.Add("P&S Contact 9", False)
                        clbContactKey.Items.Add("P&S Contact 10", False)
                        clbContactKey.Items.Add("SSCP Contact 1", False)
                        clbContactKey.Items.Add("SSCP Contact 2", False)
                        clbContactKey.Items.Add("SSCP Contact 3", False)
                        clbContactKey.Items.Add("SSCP Contact 4", False)
                        clbContactKey.Items.Add("SSCP Contact 5", False)
                        clbContactKey.Items.Add("SSCP Contact 6", False)
                        clbContactKey.Items.Add("SSCP Contact 7", False)
                        clbContactKey.Items.Add("SSCP Contact 8", False)
                        clbContactKey.Items.Add("SSCP Contact 9", False)
                        clbContactKey.Items.Add("SSCP Contact 10", False)
                        clbContactKey.Items.Add("SSPP Contact 1", False)
                        clbContactKey.Items.Add("SSPP Contact 2", False)
                        clbContactKey.Items.Add("SSPP Contact 3", False)
                        clbContactKey.Items.Add("SSPP Contact 4", False)
                        clbContactKey.Items.Add("SSPP Contact 5", False)
                        clbContactKey.Items.Add("SSPP Contact 6", False)
                        clbContactKey.Items.Add("SSPP Contact 7", False)
                        clbContactKey.Items.Add("SSPP Contact 8", False)
                        clbContactKey.Items.Add("SSPP Contact 9", False)
                        clbContactKey.Items.Add("SSPP Contact 10", False)
                        clbContactKey.Items.Add("Web Contact 4", False)
                        clbContactKey.Items.Add("Web Contact 5", False)
                        clbContactKey.Items.Add("Web Contact 6", False)
                        clbContactKey.Items.Add("Web Contact 7", False)
                        clbContactKey.Items.Add("Web Contact 8", False)
                        clbContactKey.Items.Add("Web Contact 9", False)
                        clbContactKey.Items.Add("Web Contact 10", False)
                    Case "Ambient Monitoring"
                        SQL = "Select " & _
                          "strContactKey, " & _
                          "strContactFirstName, strContactLastname, " & _
                          "strContactPrefix, strContactSuffix, strContactTitle, " & _
                          "strContactCompanyName, strContactPhoneNumber1, " & _
                          "Case  " & _
                          "    when strContactPhoneNumber2 is NULL then 'N/A' " & _
                          "    Else strContactPhoneNumber2 " & _
                          "END as ContactPhoneNumber2,  " & _
                          "case " & _
                          "    when strContactFaxNumber is Null then 'N/A' " & _
                          "    else strContactFaxNumber " & _
                          "END as strContactFaxNumber, " & _
                          "Case " & _
                          "    when strContactEmail is Null then 'N/A' " & _
                          "    ELSE strContactEmail " & _
                          "END as ContactEmail, " & _
                          "strContactAddress1, strContactAddress2, " & _
                          "strContactCity, strContactState, strContactZipCode, " & _
                          "Case " & _
                          "    when strContactDescription is Null then 'N/A' " & _
                          "    ELSE strContactDescription " & _
                          "END as ContactDescription " & _
                          "from " & connNameSpace & ".APBContactInformation " & _
                          "where strContactKey like '0413" & txtAIRSNumber.Text & "5%'"
                        clbContactKey.Items.Add("A&M Contact 1", False)
                        clbContactKey.Items.Add("A&M Contact 2", False)
                        clbContactKey.Items.Add("A&M Contact 3", False)
                        clbContactKey.Items.Add("A&M Contact 4", False)
                        clbContactKey.Items.Add("A&M Contact 5", False)
                        clbContactKey.Items.Add("A&M Contact 6", False)
                        clbContactKey.Items.Add("A&M Contact 7", False)
                        clbContactKey.Items.Add("A&M Contact 8", False)
                        clbContactKey.Items.Add("A&M Contact 9", False)
                        clbContactKey.Items.Add("A&M Contact 10", False)
                    Case "District"
                        SQL = "Select " & _
                          "strContactKey, " & _
                          "strContactFirstName, strContactLastname, " & _
                          "strContactPrefix, strContactSuffix, strContactTitle, " & _
                          "strContactCompanyName, strContactPhoneNumber1, " & _
                          "Case  " & _
                          "    when strContactPhoneNumber2 is NULL then 'N/A' " & _
                          "    Else strContactPhoneNumber2 " & _
                          "END as ContactPhoneNumber2,  " & _
                          "case " & _
                          "    when strContactFaxNumber is Null then 'N/A' " & _
                          "    else strContactFaxNumber " & _
                          "END as strContactFaxNumber, " & _
                          "Case " & _
                          "    when strContactEmail is Null then 'N/A' " & _
                          "    ELSE strContactEmail " & _
                          "END as ContactEmail, " & _
                          "strContactAddress1, strContactAddress2, " & _
                          "strContactCity, strContactState, strContactZipCode, " & _
                          "Case " & _
                          "    when strContactDescription is Null then 'N/A' " & _
                          "    ELSE strContactDescription " & _
                          "END as ContactDescription " & _
                          "from " & connNameSpace & ".APBContactInformation " & _
                          "where strContactKey like '0413" & txtAIRSNumber.Text & "7%'"
                        clbContactKey.Items.Add("District Contact 1", False)
                        clbContactKey.Items.Add("District Contact 2", False)
                        clbContactKey.Items.Add("District Contact 3", False)
                        clbContactKey.Items.Add("District Contact 4", False)
                        clbContactKey.Items.Add("District Contact 5", False)
                        clbContactKey.Items.Add("District Contact 6", False)
                        clbContactKey.Items.Add("District Contact 7", False)
                        clbContactKey.Items.Add("District Contact 8", False)
                        clbContactKey.Items.Add("District Contact 9", False)
                        clbContactKey.Items.Add("District Contact 10", False)
                    Case "Planning & Support"
                        SQL = "Select " & _
                          "strContactKey, " & _
                          "strContactFirstName, strContactLastname, " & _
                          "strContactPrefix, strContactSuffix, strContactTitle, " & _
                          "strContactCompanyName, strContactPhoneNumber1, " & _
                          "Case  " & _
                          "    when strContactPhoneNumber2 is NULL then 'N/A' " & _
                          "    Else strContactPhoneNumber2 " & _
                          "END as ContactPhoneNumber2,  " & _
                          "case " & _
                          "    when strContactFaxNumber is Null then 'N/A' " & _
                          "    else strContactFaxNumber " & _
                          "END as strContactFaxNumber, " & _
                          "Case " & _
                          "    when strContactEmail is Null then 'N/A' " & _
                          "    ELSE strContactEmail " & _
                          "END as ContactEmail, " & _
                          "strContactAddress1, strContactAddress2, " & _
                          "strContactCity, strContactState, strContactZipCode, " & _
                          "Case " & _
                          "    when strContactDescription is Null then 'N/A' " & _
                          "    ELSE strContactDescription " & _
                          "END as ContactDescription " & _
                          "from " & connNameSpace & ".APBContactInformation " & _
                          "where strContactKey like '0413" & txtAIRSNumber.Text & "40' or strContactKey = '0413" & txtAIRSNumber.Text & "6%' "
                        clbContactKey.Items.Add("Fees Contact", False)
                        clbContactKey.Items.Add("P&S Contact 1", False)
                        clbContactKey.Items.Add("P&S Contact 2", False)
                        clbContactKey.Items.Add("P&S Contact 3", False)
                        clbContactKey.Items.Add("P&S Contact 4", False)
                        clbContactKey.Items.Add("P&S Contact 5", False)
                        clbContactKey.Items.Add("P&S Contact 6", False)
                        clbContactKey.Items.Add("P&S Contact 7", False)
                        clbContactKey.Items.Add("P&S Contact 8", False)
                        clbContactKey.Items.Add("P&S Contact 9", False)
                        clbContactKey.Items.Add("P&S Contact 10", False)
                    Case Else
                        SQL = "Select " & _
                        "strContactKey, " & _
                        "strContactFirstName, strContactLastname, " & _
                        "strContactPrefix, strContactSuffix, strContactTitle, " & _
                        "strContactCompanyName, strContactPhoneNumber1, " & _
                        "Case  " & _
                        "    when strContactPhoneNumber2 is NULL then 'N/A' " & _
                        "    Else strContactPhoneNumber2 " & _
                        "END as ContactPhoneNumber2,  " & _
                        "case " & _
                        "    when strContactFaxNumber is Null then 'N/A' " & _
                        "    else strContactFaxNumber " & _
                        "END as strContactFaxNumber, " & _
                        "Case " & _
                        "    when strContactEmail is Null then 'N/A' " & _
                        "    ELSE strContactEmail " & _
                        "END as ContactEmail, " & _
                        "strContactAddress1, strContactAddress2, " & _
                        "strContactCity, strContactState, strContactZipCode, " & _
                        "Case " & _
                        "    when strContactDescription is Null then 'N/A' " & _
                        "    ELSE strContactDescription " & _
                        "END as ContactDescription " & _
                        "from " & connNameSpace & ".APBContactInformation " & _
                        "where strContactKey like '0413" & txtAIRSNumber.Text & "%'"
                        clbContactKey.Items.Add("A&M Contact 1", False)
                        clbContactKey.Items.Add("A&M Contact 2", False)
                        clbContactKey.Items.Add("A&M Contact 3", False)
                        clbContactKey.Items.Add("A&M Contact 4", False)
                        clbContactKey.Items.Add("A&M Contact 5", False)
                        clbContactKey.Items.Add("A&M Contact 6", False)
                        clbContactKey.Items.Add("A&M Contact 7", False)
                        clbContactKey.Items.Add("A&M Contact 8", False)
                        clbContactKey.Items.Add("A&M Contact 9", False)
                        clbContactKey.Items.Add("A&M Contact 10", False)
                        clbContactKey.Items.Add("District Contact 1", False)
                        clbContactKey.Items.Add("District Contact 2", False)
                        clbContactKey.Items.Add("District Contact 3", False)
                        clbContactKey.Items.Add("District Contact 4", False)
                        clbContactKey.Items.Add("District Contact 5", False)
                        clbContactKey.Items.Add("District Contact 6", False)
                        clbContactKey.Items.Add("District Contact 7", False)
                        clbContactKey.Items.Add("District Contact 8", False)
                        clbContactKey.Items.Add("District Contact 9", False)
                        clbContactKey.Items.Add("District Contact 10", False)
                        clbContactKey.Items.Add("Emission Inventory Contact", False)
                        clbContactKey.Items.Add("Emission Statement Contact", False)
                        clbContactKey.Items.Add("Fees Contact", False)
                        clbContactKey.Items.Add("ISMP Contact 1", False)
                        clbContactKey.Items.Add("ISMP Contact 2", False)
                        clbContactKey.Items.Add("ISMP Contact 3", False)
                        clbContactKey.Items.Add("ISMP Contact 4", False)
                        clbContactKey.Items.Add("ISMP Contact 5", False)
                        clbContactKey.Items.Add("ISMP Contact 6", False)
                        clbContactKey.Items.Add("ISMP Contact 7", False)
                        clbContactKey.Items.Add("ISMP Contact 8", False)
                        clbContactKey.Items.Add("ISMP Contact 9", False)
                        clbContactKey.Items.Add("ISMP Contact 10", False)
                        clbContactKey.Items.Add("P&S Contact 1", False)
                        clbContactKey.Items.Add("P&S Contact 2", False)
                        clbContactKey.Items.Add("P&S Contact 3", False)
                        clbContactKey.Items.Add("P&S Contact 4", False)
                        clbContactKey.Items.Add("P&S Contact 5", False)
                        clbContactKey.Items.Add("P&S Contact 6", False)
                        clbContactKey.Items.Add("P&S Contact 7", False)
                        clbContactKey.Items.Add("P&S Contact 8", False)
                        clbContactKey.Items.Add("P&S Contact 9", False)
                        clbContactKey.Items.Add("P&S Contact 10", False)
                        clbContactKey.Items.Add("SSCP Contact 1", False)
                        clbContactKey.Items.Add("SSCP Contact 2", False)
                        clbContactKey.Items.Add("SSCP Contact 3", False)
                        clbContactKey.Items.Add("SSCP Contact 4", False)
                        clbContactKey.Items.Add("SSCP Contact 5", False)
                        clbContactKey.Items.Add("SSCP Contact 6", False)
                        clbContactKey.Items.Add("SSCP Contact 7", False)
                        clbContactKey.Items.Add("SSCP Contact 8", False)
                        clbContactKey.Items.Add("SSCP Contact 9", False)
                        clbContactKey.Items.Add("SSCP Contact 10", False)
                        clbContactKey.Items.Add("SSPP Contact 1", False)
                        clbContactKey.Items.Add("SSPP Contact 2", False)
                        clbContactKey.Items.Add("SSPP Contact 3", False)
                        clbContactKey.Items.Add("SSPP Contact 4", False)
                        clbContactKey.Items.Add("SSPP Contact 5", False)
                        clbContactKey.Items.Add("SSPP Contact 6", False)
                        clbContactKey.Items.Add("SSPP Contact 7", False)
                        clbContactKey.Items.Add("SSPP Contact 8", False)
                        clbContactKey.Items.Add("SSPP Contact 9", False)
                        clbContactKey.Items.Add("SSPP Contact 10", False)
                        clbContactKey.Items.Add("Web Contact 4", False)
                        clbContactKey.Items.Add("Web Contact 5", False)
                        clbContactKey.Items.Add("Web Contact 6", False)
                        clbContactKey.Items.Add("Web Contact 7", False)
                        clbContactKey.Items.Add("Web Contact 8", False)
                        clbContactKey.Items.Add("Web Contact 9", False)
                        clbContactKey.Items.Add("Web Contact 10", False)
                End Select

                dsContacts = New DataSet
                daContacts = New OracleDataAdapter(SQL, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                daContacts.Fill(dsContacts, "Contacts")
                dgvContacts.DataSource = dsContacts
                dgvContacts.DataMember = "Contacts"

                If conn.State = ConnectionState.Open Then
                    'conn.close()
                End If

                dgvContacts.RowHeadersVisible = False
                dgvContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvContacts.AllowUserToResizeColumns = True
                dgvContacts.AllowUserToAddRows = False
                dgvContacts.AllowUserToDeleteRows = False
                dgvContacts.AllowUserToOrderColumns = True
                dgvContacts.AllowUserToResizeRows = True
                dgvContacts.Columns("strContactKey").HeaderText = "Key"
                dgvContacts.Columns("strContactKey").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactKey").DisplayIndex = 16
                dgvContacts.Columns("strContactKey").Visible = False
                dgvContacts.Columns("strContactPrefix").HeaderText = "Social Title"
                dgvContacts.Columns("strContactPrefix").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactPrefix").DisplayIndex = 1
                dgvContacts.Columns("strContactFirstName").HeaderText = "First Name"
                dgvContacts.Columns("strContactFirstName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactFirstName").DisplayIndex = 2
                dgvContacts.Columns("strContactLastName").HeaderText = "Last Name"
                dgvContacts.Columns("strContactLastName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactLastName").DisplayIndex = 3
                dgvContacts.Columns("strContactSuffix").HeaderText = "Pedigree"
                dgvContacts.Columns("strContactSuffix").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactSuffix").DisplayIndex = 4
                dgvContacts.Columns("strContactTitle").HeaderText = "Title"
                dgvContacts.Columns("strContactTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactTitle").DisplayIndex = 5
                dgvContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
                dgvContacts.Columns("strContactCompanyName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactCompanyName").DisplayIndex = 6
                dgvContacts.Columns("strContactPhoneNumber1").HeaderText = "Phone Number 1"
                dgvContacts.Columns("strContactPhoneNumber1").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactPhoneNumber1").DisplayIndex = 7
                dgvContacts.Columns("ContactPhoneNumber2").HeaderText = "Phone Number 2"
                dgvContacts.Columns("ContactPhoneNumber2").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("ContactPhoneNumber2").DisplayIndex = 8
                dgvContacts.Columns("strContactFaxNumber").HeaderText = "Fax Number"
                dgvContacts.Columns("strContactFaxNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactFaxNumber").DisplayIndex = 9
                dgvContacts.Columns("ContactEmail").HeaderText = "Email Address"
                dgvContacts.Columns("ContactEmail").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("ContactEmail").DisplayIndex = 10
                dgvContacts.Columns("strContactAddress1").HeaderText = "Address Line 1"
                dgvContacts.Columns("strContactAddress1").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactAddress1").DisplayIndex = 11
                dgvContacts.Columns("strContactAddress2").HeaderText = "Address Line 2"
                dgvContacts.Columns("strContactAddress2").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactAddress2").DisplayIndex = 12
                dgvContacts.Columns("strContactCity").HeaderText = "City"
                dgvContacts.Columns("strContactCity").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactCity").DisplayIndex = 13
                dgvContacts.Columns("strContactState").HeaderText = "State"
                dgvContacts.Columns("strContactState").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactState").DisplayIndex = 14
                dgvContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
                dgvContacts.Columns("strContactZipCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("strContactZipCode").DisplayIndex = 15
                dgvContacts.Columns("ContactDescription").HeaderText = "Description"
                dgvContacts.Columns("ContactDescription").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvContacts.Columns("ContactDescription").DisplayIndex = 0

                'Else
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
#Region "Subs and Functions"
    Sub ClearContactInfo()
        Dim i As Integer = 0

        Try

            txtContactKey.Clear()
            txtContactSocialTitle.Clear()
            txtContactFirstName.Clear()
            txtContactLastName.Clear()
            txtContactPedigree.Clear()
            txtContactTitle.Clear()
            txtContactCompanyName.Clear()
            txtContactAddress.Clear()
            txtContactAddress2.Clear()
            txtContactCity.Clear()
            txtContactState.Clear()
            txtContactZipCode.Clear()
            txtContactAreaCode1.Clear()
            txtContactAreaCode2.Clear()
            txtContactAreaCode3.Clear()
            txtContactPhone1.Clear()
            txtContactPhone2.Clear()
            txtContactFax.Clear()
            txtContactEmail.Clear()
            txtContactDescription.Clear()

            If clbContactKey.Items.Count <> 0 Then
                For i = 0 To clbContactKey.Items.Count - 1
                    clbContactKey.SetItemCheckState(i, CheckState.Unchecked)
                Next
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub ContactKeyChange(ByVal UpdateStatus As String)
        Dim ContactKey As String = ""
        Dim i As Integer = 0

        Try

            If UpdateStatus = True Then

                If txtContactKey.Text <> "" Then

                    txtContactSocialTitle.Clear()
                    txtContactFirstName.Clear()
                    txtContactLastName.Clear()
                    txtContactPedigree.Clear()
                    txtContactTitle.Clear()
                    txtContactCompanyName.Clear()
                    txtContactAddress.Clear()
                    txtContactAddress2.Clear()
                    txtContactCity.Clear()
                    txtContactState.Clear()
                    txtContactZipCode.Clear()
                    txtContactAreaCode1.Clear()
                    txtContactAreaCode2.Clear()
                    txtContactAreaCode3.Clear()
                    txtContactPhone1.Clear()
                    txtContactPhone2.Clear()
                    txtContactFax.Clear()
                    txtContactEmail.Clear()
                    txtContactDescription.Clear()
                    If clbContactKey.Items.Count <> 0 Then
                        For i = 0 To clbContactKey.Items.Count - 1
                            clbContactKey.SetItemCheckState(i, CheckState.Unchecked)
                        Next
                    End If
                    Select Case Mid(txtContactKey.Text, 13, 1)
                        Case "1"
                            Select Case Mid(txtContactKey.Text, 14, 1)
                                Case "0"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(23, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(0, CheckState.Checked)
                                    End If
                                Case "1"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(24, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(1, CheckState.Checked)
                                    End If
                                Case "2"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(25, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(2, CheckState.Checked)
                                    End If
                                Case "3"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(26, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(3, CheckState.Checked)
                                    End If
                                Case "4"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(27, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(4, CheckState.Checked)
                                    End If
                                Case "5"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(28, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(5, CheckState.Checked)
                                    End If
                                Case "6"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(28, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(6, CheckState.Checked)
                                    End If
                                Case "7"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(29, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(7, CheckState.Checked)
                                    End If
                                Case "8"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(30, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(8, CheckState.Checked)
                                    End If
                                Case "9"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(31, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(9, CheckState.Checked)
                                    End If
                            End Select
                        Case "2"
                            Select Case Mid(txtContactKey.Text, 14, 1)
                                Case "0"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(43, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(0, CheckState.Checked)
                                    End If
                                Case "1"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(44, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(1, CheckState.Checked)
                                    End If
                                Case "2"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(45, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(2, CheckState.Checked)
                                    End If
                                Case "3"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(46, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(3, CheckState.Checked)
                                    End If
                                Case "4"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(47, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(4, CheckState.Checked)
                                    End If
                                Case "5"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(48, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(5, CheckState.Checked)
                                    End If
                                Case "6"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(49, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(6, CheckState.Checked)
                                    End If
                                Case "7"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(50, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(7, CheckState.Checked)
                                    End If
                                Case "8"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(51, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(8, CheckState.Checked)
                                    End If
                                Case "9"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(52, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(9, CheckState.Checked)
                                    End If
                            End Select
                        Case "3"
                            Select Case Mid(txtContactKey.Text, 14, 1)
                                Case "0"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(53, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(0, CheckState.Checked)
                                    End If
                                Case "1"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(54, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(1, CheckState.Checked)
                                    End If
                                Case "2"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(55, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(2, CheckState.Checked)
                                    End If
                                Case "3"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(56, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(3, CheckState.Checked)
                                    End If
                                Case "4"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(57, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(4, CheckState.Checked)
                                    End If
                                Case "5"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(58, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(5, CheckState.Checked)
                                    End If
                                Case "6"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(59, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(6, CheckState.Checked)
                                    End If
                                Case "7"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(60, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(7, CheckState.Checked)
                                    End If
                                Case "8"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(61, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(8, CheckState.Checked)
                                    End If
                                Case "9"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(62, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(9, CheckState.Checked)
                                    End If
                            End Select
                        Case "4"
                            Select Case Mid(txtContactKey.Text, 14, 1)
                                Case "0"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(22, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(0, CheckState.Checked)
                                    End If
                                Case "1"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(20, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(1, CheckState.Checked)
                                    End If
                                Case "2"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(21, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(2, CheckState.Checked)
                                    End If
                                Case "3"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(22, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(3, CheckState.Checked)
                                    End If
                                Case "4"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(63, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(4, CheckState.Checked)
                                    End If
                                Case "5"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(64, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(5, CheckState.Checked)
                                    End If
                                Case "6"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(65, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(6, CheckState.Checked)
                                    End If
                                Case "7"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(66, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(7, CheckState.Checked)
                                    End If
                                Case "8"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(67, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(8, CheckState.Checked)
                                    End If
                                Case "9"
                                    If clbContactKey.Items.Count = 70 Then
                                        clbContactKey.SetItemCheckState(68, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(9, CheckState.Checked)
                                    End If
                            End Select
                        Case "5"
                            Select Case Mid(txtContactKey.Text, 14, 1)
                                Case "0"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(0, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(0, CheckState.Checked)
                                    End If
                                Case "1"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(1, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(1, CheckState.Checked)
                                    End If
                                Case "2"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(2, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(2, CheckState.Checked)
                                    End If
                                Case "3"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(3, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(3, CheckState.Checked)
                                    End If
                                Case "4"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(4, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(4, CheckState.Checked)
                                    End If
                                Case "5"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(5, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(5, CheckState.Checked)
                                    End If
                                Case "6"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(6, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(6, CheckState.Checked)
                                    End If
                                Case "7"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(7, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(7, CheckState.Checked)
                                    End If
                                Case "8"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(8, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(8, CheckState.Checked)
                                    End If
                                Case "9"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(9, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(9, CheckState.Checked)
                                    End If
                            End Select
                        Case "6"
                            Select Case Mid(txtContactKey.Text, 14, 1)
                                Case "0"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(22, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(1, CheckState.Checked)
                                    End If
                                Case "1"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(33, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(2, CheckState.Checked)
                                    End If
                                Case "2"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(34, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(3, CheckState.Checked)
                                    End If
                                Case "3"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(35, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(4, CheckState.Checked)
                                    End If
                                Case "4"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(36, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(5, CheckState.Checked)
                                    End If
                                Case "5"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(37, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(6, CheckState.Checked)
                                    End If
                                Case "6"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(38, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(7, CheckState.Checked)
                                    End If
                                Case "7"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(39, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(8, CheckState.Checked)
                                    End If
                                Case "8"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(40, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(9, CheckState.Checked)
                                    End If
                                Case "9"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(41, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(10, CheckState.Checked)
                                    End If
                            End Select
                        Case "7"
                            Select Case Mid(txtContactKey.Text, 14, 1)
                                Case "0"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(10, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(0, CheckState.Checked)
                                    End If
                                Case "1"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(11, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(1, CheckState.Checked)
                                    End If
                                Case "2"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(12, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(2, CheckState.Checked)
                                    End If
                                Case "3"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(13, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(3, CheckState.Checked)
                                    End If
                                Case "4"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(14, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(4, CheckState.Checked)
                                    End If
                                Case "5"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(15, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(5, CheckState.Checked)
                                    End If
                                Case "6"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(16, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(6, CheckState.Checked)
                                    End If
                                Case "7"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(17, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(7, CheckState.Checked)
                                    End If
                                Case "8"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(18, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(8, CheckState.Checked)
                                    End If
                                Case "9"
                                    If clbContactKey.Items.Count <> 10 Then
                                        clbContactKey.SetItemCheckState(19, CheckState.Checked)
                                    Else
                                        clbContactKey.SetItemCheckState(9, CheckState.Checked)
                                    End If
                            End Select
                        Case "8"
                            Select Case Mid(txtContactKey.Text, 14, 1)
                                Case "0"

                                Case "1"

                                Case "2"

                                Case "3"

                                Case "4"

                                Case "5"

                                Case "6"

                                Case "7"

                                Case "8"

                                Case "9"

                            End Select
                        Case "9"
                            Select Case Mid(txtContactKey.Text, 14, 1)
                                Case "0"

                                Case "1"

                                Case "2"

                                Case "3"

                                Case "4"

                                Case "5"

                                Case "6"

                                Case "7"

                                Case "8"

                                Case "9"

                            End Select
                    End Select

                    SQL = "Select * " & _
                    "from " & connNameSpace & ".APBContactInformation " & _
                    "where strContactkey = '" & txtContactKey.Text & "'"

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strContactPrefix")) Then
                            txtContactSocialTitle.Text = ""
                        Else
                            txtContactSocialTitle.Text = dr.Item("strContactPrefix")
                        End If
                        If IsDBNull(dr.Item("strContactFirstName")) Then
                            txtContactFirstName.Text = ""
                        Else
                            txtContactFirstName.Text = dr.Item("strContactFirstName")
                        End If
                        If IsDBNull(dr.Item("strContactLastName")) Then
                            txtContactLastName.Text = ""
                        Else
                            txtContactLastName.Text = dr.Item("strContactLastName")
                        End If
                        If IsDBNull(dr.Item("strContactSuffix")) Then
                            txtContactPedigree.Text = ""
                        Else
                            txtContactPedigree.Text = dr.Item("strContactSuffix")
                        End If
                        If IsDBNull(dr.Item("strContactTitle")) Then
                            txtContactTitle.Text = ""
                        Else
                            txtContactTitle.Text = dr.Item("strContactTitle")
                        End If
                        If IsDBNull(dr.Item("strContactCompanyName")) Then
                            txtContactCompanyName.Text = ""
                        Else
                            txtContactCompanyName.Text = dr.Item("strContactCompanyName")
                        End If
                        If IsDBNull(dr.Item("strContactAddress1")) Then
                            txtContactAddress.Text = ""
                        Else
                            txtContactAddress.Text = dr.Item("strContactAddress1")
                        End If
                        If IsDBNull(dr.Item("strContactAddress2")) Then
                            txtContactAddress2.Text = ""
                        Else
                            txtContactAddress2.Text = dr.Item("strContactAddress2")
                        End If
                        If IsDBNull(dr.Item("strContactCity")) Then
                            txtContactCity.Text = ""
                        Else
                            txtContactCity.Text = dr.Item("strContactCity")
                        End If
                        If IsDBNull(dr.Item("strContactState")) Then
                            txtContactState.Text = ""
                        Else
                            txtContactState.Text = dr.Item("strContactState")
                        End If
                        If IsDBNull(dr.Item("strContactZipCode")) Then
                            txtContactZipCode.Text = ""
                        Else
                            txtContactZipCode.Text = dr.Item("strContactZipCode")
                        End If
                        If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                            txtContactAreaCode1.Text = ""
                            txtContactPhone1.Text = ""
                        Else
                            If dr.Item("strContactphonenumber1") = "N/A" Then
                                txtContactAreaCode1.Text = ""
                                txtContactPhone1.Text = ""
                            Else
                                txtContactAreaCode1.Text = Mid(dr.Item("strContactPhoneNumber1"), 1, 3)
                                txtContactPhone1.Text = Mid(dr.Item("strContactPhoneNumber1"), 4)
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactPhoneNumber2")) Then
                            txtContactAreaCode2.Text = ""
                            txtContactPhone2.Text = ""
                        Else
                            If dr.Item("strContactPhoneNumber2") = "N/A" Then
                                txtContactAreaCode2.Text = ""
                                txtContactPhone2.Text = ""
                            Else
                                txtContactAreaCode2.Text = Mid(dr.Item("strContactPhoneNumber2"), 1, 3)
                                txtContactPhone2.Text = Mid(dr.Item("strContactPhoneNumber2"), 4)
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactFaxNumber")) Then
                            txtContactAreaCode3.Text = ""
                            txtContactFax.Text = ""
                        Else
                            If dr.Item("strContactFaxNumber") = "N/A" Then
                                txtContactAreaCode3.Text = ""
                                txtContactFax.Text = ""
                            Else
                                txtContactAreaCode3.Text = Mid(dr.Item("strContactFaxNumber"), 1, 3)
                                txtContactFax.Text = Mid(dr.Item("strContactFaxNumber"), 4)
                            End If
                        End If
                        If IsDBNull(dr.Item("strContactEmail")) Then
                            txtContactEmail.Text = ""
                        Else
                            txtContactEmail.Text = dr.Item("strContactEmail")
                        End If
                        If IsDBNull(dr.Item("strContactDescription")) Then
                            txtContactDescription.Text = ""
                        Else
                            txtContactDescription.Text = dr.Item("strContactDescription")
                        End If
                    End While

                    If conn.State = ConnectionState.Open Then
                        'conn.close()
                    End If

                Else
                    ClearContactInfo()
                End If
            Else

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub SaveContact()
        Dim ContactKey As String = ""
        Dim ContactSocialTitle As String = ""
        Dim ContactFirstName As String = ""
        Dim ContactLastName As String = ""
        Dim ContactPedigree As String = ""
        Dim ContactTitle As String = ""
        Dim ContactCompanyName As String = ""
        Dim ContactAddress As String = ""
        Dim ContactAddress2 As String = ""
        Dim ContactCity As String = ""
        Dim ContactState As String = ""
        Dim ContactZipCode As String = ""
        Dim ContactPhone1 As String = ""
        Dim ContactPhone2 As String = ""
        Dim ContactFax As String = ""
        Dim ContactEmail As String = ""
        Dim ContactDescription As String = ""
        Dim ErrorCheck As Boolean = False
        Dim x As Integer

        Try

            If txtContactSocialTitle.Text = "" Then
                ContactSocialTitle = "N/A"
            Else
                ContactSocialTitle = txtContactSocialTitle.Text
            End If
            ContactSocialTitle = Replace(ContactSocialTitle, "'", "''")

            If txtContactFirstName.Text = "" Then
                ContactFirstName = "N/A"
            Else
                ContactFirstName = txtContactFirstName.Text
            End If
            ContactFirstName = Replace(ContactFirstName, "'", "''")

            If txtContactLastName.Text = "" Then
                ContactLastName = "N/A"
            Else
                ContactLastName = txtContactLastName.Text
            End If
            ContactLastName = Replace(ContactLastName, "'", "''")

            If txtContactPedigree.Text = "" Then
                ContactPedigree = "N/A"
            Else
                ContactPedigree = txtContactPedigree.Text
            End If
            ContactPedigree = Replace(ContactPedigree, "'", "''")

            If txtContactTitle.Text = "" Then
                ContactTitle = "N/A"
            Else
                ContactTitle = txtContactTitle.Text
            End If
            ContactTitle = Replace(ContactTitle, "'", "''")

            If txtContactCompanyName.Text = "" Then
                ContactCompanyName = "N/A"
            Else
                ContactCompanyName = txtContactCompanyName.Text
            End If
            ContactCompanyName = Replace(ContactCompanyName, "'", "''")

            If txtContactAddress.Text = "" Then
                ContactAddress = "N/A"
            Else
                ContactAddress = txtContactAddress.Text
            End If
            ContactAddress = Replace(ContactAddress, "'", "''")

            If txtContactAddress2.Text = "" Then
                ContactAddress2 = "N/A"
            Else
                ContactAddress2 = txtContactAddress2.Text
            End If
            ContactAddress2 = Replace(ContactAddress2, "'", "''")

            If txtContactCity.Text = "" Then
                ContactCity = "N/A"
            Else
                ContactCity = txtContactCity.Text
            End If
            ContactCity = Replace(ContactCity, "'", "''")

            If txtContactState.Text = "" Then
                ContactState = "GA"
            Else
                ContactState = txtContactState.Text
            End If
            ContactState = Replace(ContactState, "'", "''")

            If txtContactZipCode.Text = "" Then
                ContactZipCode = "00000"
            Else
                For x = 1 To txtContactZipCode.Text.Length
                    If IsNumeric(Mid(txtContactZipCode.Text, x, 1)) Then ContactZipCode = ContactZipCode & Mid(txtContactZipCode.Text, x, 1)
                Next
                If ContactZipCode = "" Then
                    ContactZipCode = "00000"
                End If
            End If
            ContactZipCode = Replace(ContactZipCode, "'", "''")

            If txtContactPhone1.Text = "" Or txtContactAreaCode1.Text = "" Then
                ContactPhone1 = "N/A"
            Else
                For x = 1 To txtContactAreaCode1.Text.Length
                    If IsNumeric(Mid(txtContactAreaCode1.Text, x, 1)) Then ContactPhone1 = ContactPhone1 & Mid(txtContactAreaCode1.Text, x, 1)
                Next
                For x = 1 To txtContactPhone1.Text.Length
                    If IsNumeric(Mid(txtContactPhone1.Text, x, 1)) Then ContactPhone1 = ContactPhone1 & Mid(txtContactPhone1.Text, x, 1)
                Next
                If ContactPhone1 = "" Then
                    ContactPhone1 = "N/A"
                End If
            End If
            ContactPhone1 = Replace(ContactPhone1, "'", "''")

            If txtContactPhone2.Text = "" Or txtContactAreaCode2.Text = "" Then
                ContactPhone2 = "N/A"
            Else
                For x = 1 To txtContactAreaCode2.Text.Length
                    If IsNumeric(Mid(txtContactAreaCode2.Text, x, 1)) Then ContactPhone2 = ContactPhone2 & Mid(txtContactAreaCode2.Text, x, 1)
                Next
                For x = 1 To txtContactPhone2.Text.Length
                    If IsNumeric(Mid(txtContactPhone2.Text, x, 1)) Then ContactPhone2 = ContactPhone2 & Mid(txtContactPhone2.Text, x, 1)
                Next
                If ContactPhone2 = "" Then
                    ContactPhone2 = "N/A"
                End If
            End If
            ContactPhone2 = Replace(ContactPhone2, "'", "''")

            If txtContactFax.Text = "" Or txtContactAreaCode3.Text = "" Then
                ContactFax = ""
            Else
                For x = 1 To txtContactAreaCode3.Text.Length
                    If IsNumeric(Mid(txtContactAreaCode3.Text, x, 1)) Then ContactFax = ContactFax & Mid(txtContactAreaCode3.Text, x, 1)
                Next
                For x = 1 To txtContactFax.Text.Length
                    If IsNumeric(Mid(txtContactFax.Text, x, 1)) Then ContactFax = ContactFax & Mid(txtContactFax.Text, x, 1)
                Next
                If ContactFax = "" Then
                    ContactFax = ""
                End If
            End If
            ContactFax = Replace(ContactFax, "'", "''")

            If txtContactEmail.Text = "" Then
                ContactEmail = "No@Email.com"
            Else
                ContactEmail = txtContactEmail.Text
            End If
            ContactEmail = Replace(ContactEmail, "'", "''")

            If txtContactDescription.Text = "" Then
                ContactDescription = "N/A"
            Else
                ContactDescription = txtContactDescription.Text
            End If
            ContactDescription = Replace(ContactDescription, "'", "''")

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            If txtAIRSNumber.Text <> "" Then
                If txtContactKey.Text.Length = 14 Then
                    ContactKey = txtContactKey.Text
                Else
                    Select Case lblKeySource.Text
                        Case "ISMP"
                            ContactKey = "0413" & txtAIRSNumber.Text & "1" & clbContactKey.SelectedIndex
                        Case "SSCP"
                            ContactKey = "0413" & txtAIRSNumber.Text & "2" & clbContactKey.SelectedIndex
                        Case "SSPP"
                            ContactKey = "0413" & txtAIRSNumber.Text & "3" & clbContactKey.SelectedIndex
                        Case "Web"
                            ContactKey = "0413" & txtAIRSNumber.Text & "4" & clbContactKey.SelectedIndex
                        Case "Air Protection Branch"
                            Select Case clbContactKey.SelectedIndex
                                Case "0" To "9"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "5" & clbContactKey.SelectedIndex
                                Case "10" To "19"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "7" & clbContactKey.SelectedIndex - 10
                                Case "20"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "41"
                                Case "21"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "42"
                                Case "22"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "40"
                                Case "23" To "32"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "1" & clbContactKey.SelectedIndex - 23
                                Case "33" To "42"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "6" & clbContactKey.SelectedIndex - 32
                                Case "43" To "52"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "2" & clbContactKey.SelectedIndex - 43
                                Case "53" To "62"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "3" & clbContactKey.SelectedIndex - 53
                                Case "63" To "70"
                                    ContactKey = "0413" & txtAIRSNumber.Text & "4" & clbContactKey.SelectedIndex - 59
                                Case Else
                                    ContactKey = ""
                            End Select
                        Case "Ambient Monitoring"
                            ContactKey = "0413" & txtAIRSNumber.Text & "5" & clbContactKey.SelectedIndex
                        Case "District"
                            ContactKey = "0413" & txtAIRSNumber.Text & "7" & clbContactKey.SelectedIndex
                        Case "Planning & Support"
                            If clbContactKey.SelectedIndex = 0 Then
                                ContactKey = "0413" & txtAIRSNumber.Text & "4" & clbContactKey.SelectedIndex
                            Else
                                ContactKey = "0413" & txtAIRSNumber.Text & "6" & clbContactKey.SelectedIndex - 1
                            End If
                        Case Else
                            ContactKey = ""
                    End Select
                End If

                If ContactKey <> "" Then
                    SQL = "Select strKey " & _
                        "from " & connNameSpace & ".APBContactInformation " & _
                        "where strContactKey = '" & ContactKey & "' "
                    cmd = New OracleCommand(SQL, conn)
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        SQL = "Update " & connNameSpace & ".APBContactInformation set " & _
                         "strContactFirstname = '" & ContactFirstName & "', " & _
                         "strContactLastName = '" & ContactLastName & "', " & _
                         "strContactPrefix = '" & ContactSocialTitle & "', " & _
                         "strContactSuffix = '" & ContactPedigree & "', " & _
                         "strContactTitle = '" & ContactTitle & "', " & _
                         "strContactCompanyName = '" & ContactCompanyName & "', " & _
                         "strcontactPhoneNumber1 = '" & ContactPhone1 & "', " & _
                         "strContactPhoneNumber2 = '" & ContactPhone2 & "', " & _
                         "strContactFaxNumber = '" & ContactFax & "', " & _
                         "strContactEmail = '" & ContactEmail & "', " & _
                         "strContactAddress1 = '" & ContactAddress & "', " & _
                         "strContactAddress2 = '" & ContactAddress2 & "', " & _
                         "strContactCity = '" & ContactCity & "', " & _
                         "strContactState = '" & ContactState & "', " & _
                         "strContactZipCode = '" & ContactZipCode & "', " & _
                         "strmodifingPerson = '" & UserGCode & "', " & _
                         "datModifingDate = '" & OracleDate & "', " & _
                         "strContactDescription = '" & ContactDescription & "' " & _
                         "where strContactKEy = '" & ContactKey & "' "
                    Else
                        SQL = "Insert into " & connNameSpace & ".APBContactInformation " & _
                        "(strContactKey, strAIRSNumber, strKey, " & _
                        "strContactFirstName, strContactLastName, " & _
                        "strCOntactPrefix, strContactSuffix, " & _
                        "strContactTitle, strContactCompanyName, " & _
                        "strContactPhoneNumber1, strContactPhoneNumber2, " & _
                        "strContactFaxNumber, strContactEmail, " & _
                        "strContactAddress1, strContactAddress2, " & _
                        "strContactCity, strContactState, strCOntactZipCode, " & _
                        "strModifingPerson, datModifingDate, " & _
                        "strContactDescription) " & _
                        "values " & _
                        "('" & ContactKey & "', '0413" & txtAIRSNumber.Text & "', " & _
                        "'" & Mid(ContactKey, 13) & "', '" & ContactFirstName & "', '" & ContactLastName & "', " & _
                        "'" & ContactSocialTitle & "', '" & ContactPedigree & "', " & _
                        "'" & ContactTitle & "', '" & ContactCompanyName & "', " & _
                        "'" & ContactPhone1 & "', '" & ContactPhone2 & "', '" & ContactFax & "', " & _
                        "'" & ContactEmail & "', '" & ContactAddress & "', '" & ContactAddress2 & "', " & _
                        "'" & ContactCity & "', '" & ContactState & "', '" & ContactZipCode & "', " & _
                        "'" & UserGCode & "', '" & OracleDate & "', " & _
                        "'" & ContactDescription & "') "
                    End If

                    Try

                        cmd = New OracleCommand(SQL, conn)
                        dr = cmd.ExecuteReader

                        dr.Close()
                    Catch ex As Exception
                        MsgBox(ex.ToString())

                    End Try

                End If
            End If

            LoadContactsDataset("PageLoad")
            If FacilitySummary2 Is Nothing Then
            Else
                '  IAIPFacilitySummary.LoadContactInformation()
            End If
            If SSCPRequest Is Nothing Then
            Else
                SSCPRequest.LoadFacilityContactInformation()
            End If

            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub DeleteContact()
        Try
            If txtContactKey.Text.Length = 14 Then
                SQL = "Delete " & connNameSpace & ".APBContactInformation " & _
                "where strContactKey = '" & txtContactKey.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadContactsDataset("PageLoad")

                If FacilitySummary2 Is Nothing Then
                Else
                    ' IAIPFacilitySummary.LoadContactInformation()
                End If
                If SSCPRequest Is Nothing Then
                Else
                    SSCPRequest.LoadFacilityContactInformation()
                End If
                ClearContactInfo()

            Else
                MsgBox("Please select a valid contact.", MsgBoxStyle.Information, "IAIP Edit Contact Information")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try
    End Sub
    Sub Back()
        Try

            EditContacts = Nothing
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
#Region "Declarations"
#Region "Main Menus"
    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try

            ClearContactInfo()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("^X)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("^C)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("^V)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiSave.Click
        Try

            SaveContact()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label1, "https://sites.google.com/a/dnr.state.ga.us/iaip-docs/")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
    Private Sub clbContactKey_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles clbContactKey.MouseUp
        Dim SelectedItem As String = clbContactKey.SelectedIndex
        Dim SelectedState As String = clbContactKey.GetItemChecked(SelectedItem)

        Try

            If SelectedState = True Then
                clbContactKey.SetItemCheckState(0, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(1, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(2, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(3, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(4, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(5, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(6, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(7, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(8, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(9, CheckState.Unchecked)
                clbContactKey.SetItemCheckState(SelectedItem, CheckState.Checked)
                txtContactKey.Clear()
            Else

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub dgrContacts_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvContacts.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvContacts.HitTest(e.X, e.Y)

        Try


            If dgvContacts.RowCount > 0 And hti.RowIndex <> -1 Then
                txtContactKey.Text = dgvContacts(0, hti.RowIndex).Value
                ContactKeyChange(True)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub TBAddContacts_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBAddContacts.ButtonClick
        Try

            Select Case TBAddContacts.Buttons.IndexOf(e.Button)
                Case 0
                    SaveContact()
                Case 1

                Case 2
                    ClearContactInfo()
                Case 3
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub APBAddContacts_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            EditContacts = Nothing
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditWebContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditWebContacts.Click
        Try

            LoadContactsDataset("Web")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditISMPContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditISMPContacts.Click
        Try

            LoadContactsDataset("ISMP")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditSSCPContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditSSCPContacts.Click
        Try

            LoadContactsDataset("SSCP")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditSSPPContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditSSPPContacts.Click
        Try

            LoadContactsDataset("SSPP")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditAllContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditAllContact.Click
        Try

            LoadContactsDataset("All")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiPlanningContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPlanningContact.Click
        Try

            LoadContactsDataset("Planning & Support")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditWebContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditWebContact.Click
        Try

            LoadContactsDataset("Web")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditISMPContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditISMPContact.Click
        Try

            LoadContactsDataset("ISMP")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditSSCPContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditSSCPContact.Click
        Try

            LoadContactsDataset("SSCP")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditSSPPContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditSSPPContact.Click
        Try

            LoadContactsDataset("SSPP")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditAllContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditAllContacts.Click
        Try

            LoadContactsDataset("All")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiViewEditPlanningContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewEditPlanningContacts.Click
        Try

            LoadContactsDataset("Planning & Support")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnDeleteContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteContact.Click
        Try


            DeleteContact()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region

    

End Class