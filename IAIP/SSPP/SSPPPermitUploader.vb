Imports Oracle.ManagedDataAccess.Client
Imports System.IO
Imports System
Imports System.Data

Public Class SSPPPermitUploader
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim MasterApp As String

#Region "Form events"

    Private Sub IAIPPermitUploader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try


            Panel1.Text = "Enter an application number."
            Panel2.Text = UserName
            Panel3.Text = OracleDate
            TCPermitUploader.TabPages.Remove(TPTV)
            TCPermitUploader.TabPages.Remove(TPPSD)
            TCPermitUploader.TabPages.Remove(TPOther)

            DTPFinalOnWeb.Enabled = False
            DTPFinalOnWeb.Visible = False
            lblFinalOnWeb.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub IAIPPermitUploader_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

#Region "General tools"

    Private Sub btnFindApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindApplication.Click
        Try

            If txtApplicationNumber.Text <> "" Then
                FindApplicationInformation()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub txtApplicationNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtApplicationNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                If txtApplicationNumber.Text <> "" Then
                    FindApplicationInformation()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Sub FindApplicationInformation()
        Try
            Dim ZipCode As String = ""
            Dim PermitType As String = ""
            Dim AppType As String = ""
            Dim Status As String = ""
            Dim temp As String = ""

            txtApplicationInformation.Clear()
            txtApplicationLinks.Clear()
            btnUploadFile.Enabled = True

            SQL = "Select " & _
            "strAIRSNumber " & _
            "from AIRBRANCH.SSPPApplicationMaster " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Select " & _
                "substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5) as strAIRSnumber, " & _
                "AIRBRANCH.APBFacilityInformation.strFacilityName, AIRBRANCH.APBFacilityInformation.strFacilityStreet1, " & _
                "AIRBRANCH.APBFacilityInformation.strFacilityCity, AIRBRANCH.APBFacilityInformation.strFacilityZipCode, " & _
                "datFinalizedDate, strCountyName, strApplicationTypeDesc, strPermitTypeDescription, " & _
                "datPermitIssued, (strLastName||', '||strFirstName) as StaffResponsible, " & _
                "datFinalOnWeb " & _
                "from AIRBRANCH.SSPPApplicationTracking, AIRBRANCH.SSPPApplicationMaster, " & _
                "AIRBRANCH.APBFacilityInformation, AIRBRANCH.LookUpCountyInformation, " & _
                "AIRBRANCH.LookUpApplicationTypes, AIRBRANCH.LookUpPermitTypes, " & _
                "AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber  = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
                "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSnumber, 5, 3)  = AIRBRANCH.LookUpCountyInformation.strCountyCode (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strPermitType = AIRBRANCH.LookUpPermitTypes.strPermitTypeCode (+) " & _
                "and AIRBRANCH.SSPPApplicationMaster.strStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and AIRBRANCH.ssppapplicationtracking.strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strAIRSNumber")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "AIRS #: Unknown " & vbCrLf
                    Else
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "AIRS #: " & dr.Item("strAIRSNumber") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Facility Name: Unknown " & vbCrLf
                    Else
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Facility Name: " & dr.Item("strFacilityName") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Unknown Street Address " & vbCrLf
                    Else
                        txtApplicationInformation.Text = txtApplicationInformation.Text & dr.Item("strFacilityStreet1") & " " & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Unknown City GA, "
                    Else
                        txtApplicationInformation.Text = txtApplicationInformation.Text & dr.Item("strFacilityCity") & " GA, "
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "" & vbCrLf
                    Else
                        ZipCode = dr.Item("strFacilityZipCode")
                        Select Case ZipCode.Length
                            Case 5
                                txtApplicationInformation.Text = txtApplicationInformation.Text & ZipCode & vbCrLf
                            Case Is > 5
                                txtApplicationInformation.Text = txtApplicationInformation.Text & Mid(ZipCode, 1, 5) & "-" & Mid(ZipCode, 6) & vbCrLf
                            Case Else
                                txtApplicationInformation.Text = txtApplicationInformation.Text & ZipCode & vbCrLf
                        End Select
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "County: Unknown" & vbCrLf & vbCrLf
                    Else
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "County: " & dr.Item("strCountyName") & vbCrLf & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "App. Type: Unknown " & vbCrLf
                    Else
                        AppType = dr.Item("strApplicationTypeDesc")
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "App. Type: " & AppType & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strPermitTypeDescription")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Permit Type: Unknown " & vbCrLf
                    Else
                        PermitType = dr.Item("strPermitTypeDescription")
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Permit Type: " & PermitType & vbCrLf
                    End If
                    If IsDBNull(dr.Item("datPermitIssued")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Date Issued: Unknown" & vbCrLf & vbCrLf
                    Else
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Date Issued " & dr.Item("datPermitIssued") & vbCrLf & vbCrLf
                    End If
                    If IsDBNull(dr.Item("StaffResponsible")) Then
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Staff Responsible: Unknown "
                    Else
                        txtApplicationInformation.Text = txtApplicationInformation.Text & "Staff Responsible: " & dr.Item("staffResponsible")
                    End If
                    If IsDBNull(dr.Item("datFinalizedDate")) Then
                        Status = ""
                    Else
                        Status = dr.Item("datFinalizedDate")
                    End If
                    If IsDBNull(dr.Item("datFinalOnWeb")) Then
                        DTPFinalOnWeb.Text = OracleDate
                        DTPFinalOnWeb.Visible = False
                        lblFinalOnWeb.Visible = False
                    Else
                        DTPFinalOnWeb.Text = dr.Item("datFinalOnWeb")
                        lblFinalOnWeb.Visible = True
                        DTPFinalOnWeb.Visible = True
                    End If
                End While
                dr.Close()
            Else
                txtApplicationInformation.Text = "No Application Data available."
            End If

            SQL = "select strMasterApplication " & _
            "from AIRBRANCH.SSPPApplicationLinking " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                MasterApp = dr.Item("strMasterApplication")
            Else
                MasterApp = txtApplicationNumber.Text
            End If
            dr.Close()
            If MasterApp <> "" Then
                SQL = "Select strApplicationNumber " & _
                "from AIRBRANCH.SSPPApplicationLinking " & _
                "where strMasterApplication = '" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    txtApplicationLinks.Text = txtApplicationLinks.Text & dr.Item("strApplicationNumber") & vbCrLf
                End While
                dr.Close()
            Else
                txtApplicationLinks.Clear()
            End If

            rdbTitleVPermit.Checked = False
            rdbPSDPermit.Checked = False
            rdbOtherPermit.Checked = False

            SQL = "select " & _
            "distinct(AIRBRANCH.APBPermits.strFileName)  " & _
            "from AIRBRANCH.APBpermits, AIRBRANCH.SSPPApplicationLinking " & _
            "where substr(AIRBRANCH.APBpermits.strFileName, 4) = AIRBRANCH.SSPPAPPlicationLinking.strmasterapplication (+) " & _
            "and (AIRBRANCH.SSPPApplicationLinking.strApplicationNumber = '" & MasterApp & "' " & _
            "or AIRBRANCH.APBPermits.strFileName like '%-" & MasterApp & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                temp = Mid(dr.Item("strFileName"), 1, 1)
                Select Case temp
                    Case "V"
                        rdbTitleVPermit.Checked = True
                    Case "P"
                        rdbPSDPermit.Checked = True
                    Case "O"
                        rdbOtherPermit.Checked = True
                    Case Else
                        rdbOtherPermit.Checked = True
                End Select
            Else
                Select Case AppType
                    Case "TV-Initial", "TV-Renewal", "TV-Amend", "Title V", "SAWO", "SAW", "AA", "MAW", "MAWO", "502(b)10"
                        rdbTitleVPermit.Checked = True
                    Case "PSD"
                        rdbPSDPermit.Checked = True
                    Case ""
                        rdbTitleVPermit.Checked = False
                        rdbPSDPermit.Checked = False
                        rdbOtherPermit.Checked = False
                    Case Else
                        rdbOtherPermit.Checked = True
                End Select

            End If
            dr.Close()

            If Status <> "" Then
                If UserUnit = "14" Or (UserUnit = "---" And UserProgram = "4") Or (UserUnit = "---" And UserProgram = "5") Then
                    'If Mid(Permissions, 30, 1) = "1" Or Mid(Permissions, 26, 1) = "1" Or Mid(Permissions, 42, 1) = "1" Or Mid(Permissions, 6, 1) = "1" Then
                    btnUploadFile.Enabled = True
                Else
                    btnUploadFile.Enabled = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Sub ClearForm()
        Try
            txtApplicationNumber.Clear()
            txtApplicationInformation.Clear()
            txtApplicationLinks.Clear()
            rdbTitleVPermit.Checked = False
            rdbPSDPermit.Checked = False
            rdbOtherPermit.Checked = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub tbbClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbbClear.Click
        Try
            ClearForm()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub tbbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbbBack.Click
        Try
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

#End Region

#Region "Save files"

    Private Sub btnUploadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Try
            SaveFiles()
            FindApplicationInformation()
            MsgBox("Done", MsgBoxStyle.Information, "Permit Uploader")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Sub UploadFile(ByVal FileName As String, ByVal DocLocation As String, ByVal DocxLocation As String, _
                    ByVal PDFLocation As String, ByVal DocOnFile As String)
        Try
            Dim Flag As String = "00"
            Dim DocFile As String = ""
            Dim ResultDoc As DialogResult
            Dim PDFFile As String = ""
            Dim ResultPDF As DialogResult

            SQL = "Select " & _
            "strDOCFileSize, strPDFFileSize " & _
            "From AIRBRANCH.ApbPermits " & _
            "where strFileName = '" & FileName & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("strDocFileSize")) Then
                    DocFile = ""
                Else
                    DocFile = dr.Item("strDocFileSize")
                End If
                If IsDBNull(dr.Item("strPDFFileSize")) Then
                    PDFFile = ""
                Else
                    PDFFile = dr.Item("strPDFFileSize")
                End If
            Else
                DocFile = ""
                PDFFile = ""
            End If
            dr.Close()

            If DocFile <> "" And (DocLocation <> "" Or DocxLocation <> "") Then
                Select Case Mid(FileName, 1, 2)
                    Case "VN"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Narrative." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "VD"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Draft Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "VP"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Public Notice." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "VF"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Final Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PA"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Application Summary." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PP"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Preliminary Determination." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PT"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Narrative." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PD"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Draft Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PN"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Public Notice." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PH"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Hearing Notice." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PF"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Final Determination." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PI"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Final Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "ON"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Other Narrative." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "OP"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Other Permit." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case Else
                        ResultDoc = MessageBox.Show("A Word file currently exists for this 'Unknown' application." & vbCrLf & _
                        "Do you want to overwrite this file?", "Permit Uploader", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                End Select
                Select Case ResultDoc
                    Case DialogResult.Yes
                        Flag = "10"
                    Case DialogResult.No
                        Flag = "00"
                    Case DialogResult.Cancel
                        Flag = "00"
                    Case Else
                        Flag = "00"
                End Select
            Else
                If DocLocation <> "" Or DocxLocation <> "" Then
                    Flag = "10"
                Else
                    Flag = "00"
                End If
            End If
            If (PDFFile <> "" And Mid(Flag, 1, 1) = "1") Or DocOnFile = "On File" Then
                SQL = "update AIRBRANCH.APBPermits set " & _
                "PDFPermitData = '', " & _
                "strPDFFileSize = '', " & _
                "strPDFModifingPerson = '', " & _
                "datPDFModifingDate = '' " & _
                "where strFileName = '" & FileName & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Read()
                dr.Close()

            Else
                If PDFFile <> "" And PDFLocation <> "" Then
                    Select Case Mid(FileName, 1, 2)
                        Case "VN"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Narrative." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "VD"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Draft Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "VP"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Public Notice." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "VF"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Final Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PA"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Application Summary." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PP"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Preliminary Determination." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PT"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Narrative." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PD"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Draft Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PN"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Public Notice." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PH"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Hearing Notice." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PF"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Final Determination." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PI"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Final Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "ON"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Other Narrative." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "OP"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Other Permit." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case Else
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this 'Unknown' application." & vbCrLf & _
                            "Do you want to overwrite this file?", "Permit Uploader", _
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    End Select
                    Select Case ResultPDF
                        Case DialogResult.Yes
                            Flag = Mid(Flag, 1, 1) & "1"
                        Case DialogResult.No
                            Flag = Mid(Flag, 1, 1) & "0"
                        Case DialogResult.Cancel
                            Flag = Mid(Flag, 1, 1) & "0"
                        Case Else
                            Flag = Mid(Flag, 1, 1) & "0"
                    End Select
                Else
                    If PDFLocation <> "" Then
                        Flag = Mid(Flag, 1, 1) & "1"
                    Else
                        Flag = Mid(Flag, 1, 1) & "0"
                    End If
                End If
            End If
            If Flag <> "00" Then
                Dim rowCount As String = ""
                Dim da As OracleDataAdapter
                Dim ds As DataSet

                If Flag <> "00" Then
                    SQL = "Delete AIRBRANCH.APBPermits " & _
                    "where strFileName = '" & FileName & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "select " & _
                    "rowCount " & _
                    "from AIRBRANCH.APBPermits " & _
                    "where strFileName = '" & FileName & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("rowCount")) Then
                            rowCount = ""
                        Else
                            rowCount = dr.Item("RowCount")
                        End If
                    End While
                    dr.Close()

                    If rowCount = "" Then
                        SQL = "select " & _
                        "(max(rowCount) + 1) as RowCount " & _
                        "from AIRBRANCH.APBPermits "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("RowCount")) Then
                                rowCount = "1"
                            Else
                                rowCount = dr.Item("RowCount")
                            End If
                        End While
                        dr.Close()
                    End If

                    Dim fs As FileStream
                    If DocLocation <> "" And Mid(Flag, 1, 1) = "1" Then
                        fs = New FileStream(DocLocation, FileMode.OpenOrCreate, FileAccess.Read)
                    Else
                        If DocxLocation <> "" And Mid(Flag, 1, 1) = "1" Then
                            fs = New FileStream(DocxLocation, FileMode.OpenOrCreate, FileAccess.Read)
                        Else
                            fs = New FileStream(PDFLocation, FileMode.OpenOrCreate, FileAccess.Read)
                        End If
                    End If

                    Dim rawData() As Byte = New Byte(fs.Length) {}
                    'If DocLocation <> "" Then
                    '    rawData = rawData
                    'End If
                    If DocxLocation <> "" Then
                        ReDim rawData(fs.Length - 1)
                    End If
                    'If PDFLocation <> "" Then
                    '    rawData = rawData
                    'End If

                    fs.Read(rawData, 0, System.Convert.ToInt32(fs.Length))
                    fs.Close()

                    SQL = "Select * from AIRBRANCH.APBPermits " & _
                    "where strFileName = '" & FileName & "' "
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    da = New OracleDataAdapter(SQL, CurrentConnection)
                    ds = New DataSet("PDF")
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                    da.Fill(ds, "PDF")
                    Dim row As DataRow = ds.Tables("PDF").NewRow()
                    row("rowCount") = rowCount
                    row("strFileName") = FileName
                    If (DocLocation <> "" Or DocxLocation <> "") And Mid(Flag, 1, 1) = "1" Then
                        row("docPermitData") = rawData
                        row("strDocFileSize") = rawData.Length
                        row("strDocModifingPerson") = UserGCode
                        row("datDocModifingDate") = OracleDate
                    Else
                        row("pdfPermitData") = rawData
                        row("strPDFFileSize") = rawData.Length
                        row("strPDFModifingPerson") = UserGCode
                        row("datPDFModifingDate") = OracleDate
                    End If
                    ds.Tables("PDF").Rows.Add(row)
                    da.Update(ds, "PDF")

                    If Mid(FileName, 1, 2) = "OP" Then
                        SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                        "datFinalOnWeb = '" & OracleDate & "' " & _
                        "where strApplicationNumber = '" & MasterApp & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If
                End If

                If Mid(FileName, 1, 2) = "OP" Then
                    SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                    "datFinalOnWeb = '" & OracleDate & "' " & _
                    "where strApplicationNumber = '" & MasterApp & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Sub SaveFiles()
        Try
            Dim doc As String = ""
            Dim docx As String = ""
            Dim pdf As String = ""
            Dim docOnFile As String = ""

            If rdbTitleVPermit.Checked = True Then
                If chbTVNarrative.Checked = True Then    'Prefix VN-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtTVNarrativeDoc.Text <> "" And txtTVNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtTVNarrativeDoc.Text, (txtTVNarrativeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtTVNarrativeDoc.Text
                        Else
                            If txtTVNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVNarrativeDoc.Text <> "" And txtTVNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtTVNarrativeDoc.Text, (txtTVNarrativeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtTVNarrativeDoc.Text
                        Else
                            If txtTVNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVNarrativePDF.Text <> "" And txtTVNarrativePDF.Text <> "N/A" Then
                        If (Mid(txtTVNarrativePDF.Text, (txtTVNarrativePDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtTVNarrativePDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("VN-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbTVDraft.Checked = True Then         'Prefix VD-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtTVDraftDoc.Text <> "" And txtTVDraftDoc.Text <> "N/A" Then
                        If (Mid(txtTVDraftDoc.Text, (txtTVDraftDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtTVDraftDoc.Text
                        Else
                            If txtTVDraftDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVDraftDoc.Text <> "" And txtTVDraftDoc.Text <> "N/A" Then
                        If (Mid(txtTVDraftDoc.Text, (txtTVDraftDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtTVDraftDoc.Text
                        Else
                            If txtTVDraftDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVDraftPDF.Text <> "" And txtTVDraftPDF.Text <> "N/A" Then
                        If (Mid(txtTVDraftPDF.Text, (txtTVDraftPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtTVDraftPDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("VD-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbTVPublicNotice.Checked = True Then   'Prefix VP-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtTVPublicNoticeDoc.Text <> "" And txtTVPublicNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtTVPublicNoticeDoc.Text, (txtTVPublicNoticeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtTVPublicNoticeDoc.Text
                        Else
                            If txtTVPublicNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVPublicNoticeDoc.Text <> "" And txtTVPublicNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtTVPublicNoticeDoc.Text, (txtTVPublicNoticeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtTVPublicNoticeDoc.Text
                        Else
                            If txtTVPublicNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVPublicNoticePDF.Text <> "" And txtTVPublicNoticePDF.Text <> "N/A" Then
                        If (Mid(txtTVPublicNoticePDF.Text, (txtTVPublicNoticePDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtTVPublicNoticePDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("VP-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbTVFinal.Checked = True Then          'Prefix VF-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtTVFinalDoc.Text <> "" And txtTVFinalDoc.Text <> "N/A" Then
                        If (Mid(txtTVFinalDoc.Text, (txtTVFinalDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtTVFinalDoc.Text
                        Else
                            If txtTVFinalDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVFinalDoc.Text <> "" And txtTVFinalDoc.Text <> "N/A" Then
                        If (Mid(txtTVFinalDoc.Text, (txtTVFinalDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtTVFinalDoc.Text
                        Else
                            If txtTVFinalDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVFinalPDF.Text <> "" And txtTVFinalPDF.Text <> "N/A" Then
                        If (Mid(txtTVFinalPDF.Text, (txtTVFinalPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtTVFinalPDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("VF-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If

                    SQL = "Select datFinalOnWeb " & _
                    "from AIRBRANCH.SSPPApplicationTracking " & _
                    "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                        "datFinalOnWeb = '" & OracleDate & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If

                End If
            End If
            If rdbPSDPermit.Checked = True Then
                If chbPSDApplicationSummary.Checked = True Then 'Prefix PA-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDAppSummaryDoc.Text <> "" And txtPSDAppSummaryDoc.Text <> "N/A" Then
                        If (Mid(txtPSDAppSummaryDoc.Text, (txtPSDAppSummaryDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDAppSummaryDoc.Text
                        Else
                            If txtPSDAppSummaryDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDAppSummaryDoc.Text <> "" And txtPSDAppSummaryDoc.Text <> "N/A" Then
                        If (Mid(txtPSDAppSummaryDoc.Text, (txtPSDAppSummaryDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDAppSummaryDoc.Text
                        Else
                            If txtPSDAppSummaryDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDAppSummaryPDF.Text <> "" And txtPSDAppSummaryPDF.Text <> "N/A" Then
                        If (Mid(txtPSDAppSummaryPDF.Text, (txtPSDAppSummaryPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtPSDAppSummaryPDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("PA-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDPrelimDet.Checked = True Then          'Prefix PP-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDPrelimDetDoc.Text <> "" And txtPSDPrelimDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDPrelimDetDoc.Text, (txtPSDPrelimDetDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDPrelimDetDoc.Text
                        Else
                            If txtPSDPrelimDetDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPrelimDetDoc.Text <> "" And txtPSDPrelimDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDPrelimDetDoc.Text, (txtPSDPrelimDetDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDPrelimDetDoc.Text
                        Else
                            If txtPSDPrelimDetDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPrelimDetPDF.Text <> "" And txtPSDPrelimDetPDF.Text <> "N/A" Then
                        If (Mid(txtPSDPrelimDetPDF.Text, (txtPSDPrelimDetPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtPSDPrelimDetPDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("PP-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDNarrative.Checked = True Then          'Prefix PT-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDNarrativeDoc.Text <> "" And txtPSDNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDNarrativeDoc.Text, (txtPSDNarrativeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDNarrativeDoc.Text
                        Else
                            If txtPSDNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDNarrativeDoc.Text <> "" And txtPSDNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDNarrativeDoc.Text, (txtPSDNarrativeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDNarrativeDoc.Text
                        Else
                            If txtPSDNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDNarrativePDF.Text <> "" And txtPSDNarrativePDF.Text <> "N/A" Then
                        If (Mid(txtPSDNarrativePDF.Text, (txtPSDNarrativePDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtPSDNarrativePDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("PT-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDDraftPermit.Checked = True Then        'Prefix PD-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDPrelimDetDoc.Text <> "" And txtPSDPrelimDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDDraftPermitDoc.Text, (txtPSDDraftPermitDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDDraftPermitDoc.Text
                        Else
                            If txtPSDDraftPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPrelimDetDoc.Text <> "" And txtPSDPrelimDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDDraftPermitDoc.Text, (txtPSDDraftPermitDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDDraftPermitDoc.Text
                        Else
                            If txtPSDDraftPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDDraftPermitPDF.Text <> "" And txtPSDDraftPermitPDF.Text <> "N/A" Then
                        If (Mid(txtPSDDraftPermitPDF.Text, (txtPSDDraftPermitPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtPSDDraftPermitPDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("PD-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDPublicNotice.Checked = True Then       'Prefix PN-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDPublicNoticeDoc.Text <> "" And txtPSDPublicNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDPublicNoticeDoc.Text, (txtPSDPublicNoticeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDPublicNoticeDoc.Text
                        Else
                            If txtPSDPublicNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPublicNoticeDoc.Text <> "" And txtPSDPublicNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDPublicNoticeDoc.Text, (txtPSDPublicNoticeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDPublicNoticeDoc.Text
                        Else
                            If txtPSDPublicNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPublicNoticePDF.Text <> "" And txtPSDPublicNoticePDF.Text <> "N/A" Then
                        If (Mid(txtPSDPublicNoticePDF.Text, (txtPSDPublicNoticePDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtPSDPublicNoticePDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("PN-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDHearingNotice.Checked = True Then      'Prefix PH-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDHearingNoticeDoc.Text <> "" And txtPSDHearingNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDHearingNoticeDoc.Text, (txtPSDHearingNoticeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDHearingNoticeDoc.Text
                        Else
                            If txtPSDHearingNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDHearingNoticeDoc.Text <> "" And txtPSDHearingNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDHearingNoticeDoc.Text, (txtPSDHearingNoticeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDHearingNoticeDoc.Text
                        Else
                            If txtPSDHearingNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDHearingNoticePDF.Text <> "" And txtPSDHearingNoticePDF.Text <> "N/A" Then
                        If (Mid(txtPSDHearingNoticePDF.Text, (txtPSDHearingNoticePDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtPSDHearingNoticePDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("PH-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDFinalDet.Checked = True Then           'Prefix PF- 
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDFinalDetDoc.Text <> "" And txtPSDFinalDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDFinalDetDoc.Text, (txtPSDFinalDetDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDFinalDetDoc.Text
                        Else
                            If txtPSDFinalDetDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDFinalDetDoc.Text <> "" And txtPSDFinalDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDFinalDetDoc.Text, (txtPSDFinalDetDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDFinalDetDoc.Text
                        Else
                            If txtPSDFinalDetDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDFinalDetPDF.Text <> "" And txtPSDFinalDetPDF.Text <> "N/A" Then
                        If (Mid(txtPSDFinalDetPDF.Text, (txtPSDFinalDetPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtPSDFinalDetPDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("PF-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDFinalPermit.Checked = True Then        'Prefix PI-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDFinalPermitDoc.Text <> "" And txtPSDFinalPermitDoc.Text <> "N/A" Then
                        If (Mid(txtPSDFinalPermitDoc.Text, (txtPSDFinalPermitDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDFinalPermitDoc.Text
                        Else
                            If txtPSDFinalPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDFinalPermitDoc.Text <> "" And txtPSDFinalPermitDoc.Text <> "N/A" Then
                        If (Mid(txtPSDFinalPermitDoc.Text, (txtPSDFinalPermitDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDFinalPermitDoc.Text
                        Else
                            If txtPSDFinalPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDFinalPermitPDF.Text <> "" And txtPSDFinalPermitPDF.Text <> "N/A" Then
                        If (Mid(txtPSDFinalPermitPDF.Text, (txtPSDFinalPermitPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtPSDFinalPermitPDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("PI-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If

                    SQL = "Select datFinalOnWeb " & _
                    "from AIRBRANCH.SSPPApplicationTracking " & _
                    "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                        "datFinalOnWeb = '" & OracleDate & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If

                End If
            End If
            If rdbOtherPermit.Checked = True Then
                If chbOtherNarrative.Checked = True Then       'Prefix ON-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtOtherNarrativeDoc.Text <> "" And txtOtherNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtOtherNarrativeDoc.Text, (txtOtherNarrativeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtOtherNarrativeDoc.Text
                        Else
                            If txtOtherNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtOtherNarrativeDoc.Text <> "" And txtOtherNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtOtherNarrativeDoc.Text, (txtOtherNarrativeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtOtherNarrativeDoc.Text
                        Else
                            If txtOtherNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtOtherNarrativePDF.Text <> "" And txtOtherNarrativePDF.Text <> "N/A" Then
                        If (Mid(txtOtherNarrativePDF.Text, (txtOtherNarrativePDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtOtherNarrativePDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("ON-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbOtherPermit.Checked = True Then             'Prefix OP-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtOtherPermitDoc.Text <> "" And txtOtherPermitDoc.Text <> "N/A" Then
                        If (Mid(txtOtherPermitDoc.Text, (txtOtherPermitDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtOtherPermitDoc.Text
                        Else
                            If txtOtherPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtOtherPermitDoc.Text <> "" And txtOtherPermitDoc.Text <> "N/A" Then
                        If (Mid(txtOtherPermitDoc.Text, (txtOtherPermitDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtOtherPermitDoc.Text
                        Else
                            If txtOtherPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtOtherPermitPDF.Text <> "" And txtOtherPermitPDF.Text <> "N/A" Then
                        If (Mid(txtOtherPermitPDF.Text, (txtOtherPermitPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                            pdf = txtOtherPermitPDF.Text
                        End If
                    End If
                    If doc <> "" Or docx <> "" Or pdf <> "" Then
                        UploadFile("OP-" & MasterApp, doc, docx, pdf, docOnFile)

                        SQL = "Select datFinalOnWeb " & _
                        "from AIRBRANCH.SSPPApplicationTracking " & _
                        "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        dr.Close()
                        If recExist = False Then
                            SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                            "datFinalOnWeb = '" & OracleDate & "' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

#Region "Delete files"

    Sub DeleteFile(ByVal FileType As String)
        Try
            Dim ResultDoc As DialogResult

            SQL = "Select " & _
            "strFileName " & _
            "from AIRBRANCH.APBPermits " & _
            "where strFileName = '" & FileType & "-" & txtApplicationNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                ResultDoc = MessageBox.Show("Are you positive you want to delete this file?" & vbCrLf & _
                        "It will not be recoverable if you delete it.", "Permit Delete", _
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                Select Case ResultDoc
                    Case DialogResult.Yes
                        SQL = "Delete AIRBRANCH.APBPermits " & _
                        "where strFileName = '" & FileType & "-" & txtApplicationNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        FindApplicationInformation()

                    Case DialogResult.No

                    Case DialogResult.Cancel

                    Case Else

                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Permit type radio buttons"

    Sub DisplayPermitPanel()
        Try
            '130, 307
            If TCPermitUploader.TabPages.Contains(TPTV) Then
                TCPermitUploader.TabPages.Remove(TPTV)
            End If
            If TCPermitUploader.TabPages.Contains(TPPSD) Then
                TCPermitUploader.TabPages.Remove(TPPSD)
            End If
            If TCPermitUploader.TabPages.Contains(TPOther) Then
                TCPermitUploader.TabPages.Remove(TPOther)
            End If
            If rdbTitleVPermit.Checked = True Then
                TCPermitUploader.TabPages.Add(TPTV)
            Else
                If rdbPSDPermit.Checked = True Then
                    TCPermitUploader.TabPages.Add(TPPSD)
                Else
                    If rdbOtherPermit.Checked = True Then
                        TCPermitUploader.TabPages.Add(TPOther)
                    Else

                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub rdbTitleVPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTitleVPermit.CheckedChanged
        Try
            Dim TVNarrative As String = ""
            Dim TVDraft As String = ""
            Dim TVNotice As String = ""
            Dim TVFinal As String = ""

            chbTVNarrative.Checked = False
            chbTVDraft.Checked = False
            chbTVPublicNotice.Checked = False
            chbTVFinal.Checked = False

            DisplayPermitPanel()

            If rdbTitleVPermit.Checked = True And MasterApp <> "" Then
                SQL = "select " & _
                "strFileName " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName like 'V_-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                Do While dr.Read
                    If IsDBNull(dr.Item("strFileName")) Then
                    Else
                        Select Case Mid(dr.Item("strFileName"), 1, 2)
                            Case "VN"
                                TVNarrative = "True"
                            Case "VD"
                                TVDraft = "True"
                            Case "VP"
                                TVNotice = "True"
                            Case "VF"
                                TVFinal = "True"
                        End Select
                    End If
                Loop
                dr.Close()
                If TVNarrative = "True" Then
                    chbTVNarrative.Checked = True
                End If
                If TVDraft = "True" Then
                    chbTVDraft.Checked = True
                End If
                If TVNotice = "True" Then
                    chbTVPublicNotice.Checked = True
                End If
                If TVFinal = "True" Then
                    chbTVFinal.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub rdbPSDPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPSDPermit.CheckedChanged
        Try
            Dim PSDAppSummary As String = ""
            Dim PSDPrelimDet As String = ""
            Dim PSDNarrative As String = ""
            Dim PSDDraft As String = ""
            Dim PSDNotice As String = ""
            Dim PSDHearing As String = ""
            Dim PSDFinal As String = ""
            Dim PSDPermit As String = ""

            chbPSDApplicationSummary.Checked = False
            chbPSDPrelimDet.Checked = False
            chbPSDNarrative.Checked = False
            chbPSDDraftPermit.Checked = False
            chbPSDPublicNotice.Checked = False
            chbPSDHearingNotice.Checked = False
            chbPSDFinalDet.Checked = False
            chbPSDFinalPermit.Checked = False

            DisplayPermitPanel()

            If rdbPSDPermit.Checked = True And MasterApp <> "" Then
                SQL = "select " & _
                "strFileName " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName like 'P_-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                Do While dr.Read
                    If IsDBNull(dr.Item("strFileName")) Then
                    Else
                        Select Case Mid(dr.Item("strFileName"), 1, 2)
                            Case "PA"
                                PSDAppSummary = "True"
                            Case "PP"
                                PSDPrelimDet = "True"
                            Case "PT"
                                PSDNarrative = "True"
                            Case "PD"
                                PSDDraft = "True"
                            Case "PN"
                                PSDNotice = "True"
                            Case "PH"
                                PSDHearing = "True"
                            Case "PF"
                                PSDFinal = "True"
                            Case "PI"
                                PSDPermit = "True"
                        End Select
                    End If
                Loop
                dr.Close()
                If PSDAppSummary = "True" Then
                    chbPSDApplicationSummary.Checked = True
                End If
                If PSDPrelimDet = "True" Then
                    chbPSDPrelimDet.Checked = True
                End If
                If PSDNarrative = "True" Then
                    chbPSDNarrative.Checked = True
                End If
                If PSDDraft = "True" Then
                    chbPSDDraftPermit.Checked = True
                End If
                If PSDNotice = "True" Then
                    chbPSDPublicNotice.Checked = True
                End If
                If PSDHearing = "True" Then
                    chbPSDHearingNotice.Checked = True
                End If
                If PSDFinal = "True" Then
                    chbPSDFinalDet.Checked = True
                End If
                If PSDPermit = "True" Then
                    chbPSDFinalPermit.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub rdbOtherPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbOtherPermit.CheckedChanged
        Try
            Dim OtherNarrative As String = ""
            Dim OtherPermit As String = ""

            chbOtherNarrative.Checked = False
            chbOtherPermit.Checked = False

            DisplayPermitPanel()

            If rdbOtherPermit.Checked = True And MasterApp <> "" Then
                SQL = "select " & _
                "strFileName " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName like 'O_-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                Do While dr.Read
                    If IsDBNull(dr.Item("strFileName")) Then
                    Else
                        Select Case Mid(dr.Item("strFileName"), 1, 2)
                            Case "ON"
                                OtherNarrative = "True"
                            Case "OP"
                                OtherPermit = "True"
                        End Select
                    End If
                Loop
                dr.Close()
                If OtherNarrative = "True" Then
                    chbOtherNarrative.Checked = True
                End If
                If OtherPermit = "True" Then
                    chbOtherPermit.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

#Region "Document checkboxes"

    Private Sub chbTVNarrative_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbTVNarrative.CheckedChanged
        Try

            If chbTVNarrative.Checked = True And MasterApp <> "" Then

                txtTVNarrativeDoc.Visible = True
                txtTVNarrativePDF.Visible = True
                btnTVNarrative.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'VN-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUSerProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUSerProfiles.numUserID  " & _
                "and numUserID = strPDFModifingPerson " & _
                "and strFileName = 'VN-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'VN-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtTVNarrativeDoc.Text = ""
                    Else
                        txtTVNarrativeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblTVNarrativeSRDoc.Visible = False
                    Else
                        lblTVNarrativeSRDoc.Visible = True
                        lblTVNarrativeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblTVNarrativeDUDoc.Visible = False
                    Else
                        lblTVNarrativeDUDoc.Visible = True
                        lblTVNarrativeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If

                    If IsDBNull(dr.Item("PDFData")) Then
                        txtTVNarrativePDF.Text = ""
                    Else
                        txtTVNarrativePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblTVNarrativeSRPDF.Visible = False
                    Else
                        lblTVNarrativeSRPDF.Visible = True
                        lblTVNarrativeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblTVNarrativeDUPDF.Visible = False
                    Else
                        lblTVNarrativeDUPDF.Visible = True
                        lblTVNarrativeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtTVNarrativeDoc.Text = "On File" Or txtTVNarrativePDF.Text = "On File" Then
                    btnTVNarrativeDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeleteTVNarrative.Visible = True
                    Else
                        btnDeleteTVNarrative.Visible = False
                    End If
                Else
                    btnTVNarrativeDownload.Visible = False
                    btnDeleteTVNarrative.Visible = False
                End If
            Else
                txtTVNarrativeDoc.Clear()
                txtTVNarrativePDF.Clear()
                txtTVNarrativeDoc.Visible = False
                lblTVNarrativeSRDoc.Visible = False
                lblTVNarrativeDUDoc.Visible = False
                txtTVNarrativePDF.Visible = False
                lblTVNarrativeSRPDF.Visible = False
                lblTVNarrativeDUPDF.Visible = False
                btnTVNarrative.Visible = False
                btnTVNarrativeDownload.Visible = False
                btnDeleteTVNarrative.Visible = False
            End If

        Catch ex As Exception
            MsgBox(ex.ToString())
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbTVDraft_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbTVDraft.CheckedChanged
        Try

            If chbTVDraft.Checked = True And MasterApp <> "" Then
                txtTVDraftDoc.Visible = True
                txtTVDraftPDF.Visible = True
                btnTVDraft.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUSerProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUSerProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'VD-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                "and numUserID = strPDFModifingPerson " & _
                "and strFileName = 'VD-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'VD-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtTVDraftDoc.Text = ""
                    Else
                        txtTVDraftDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblTVDraftSRDoc.Visible = False
                    Else
                        lblTVDraftSRDoc.Visible = True
                        lblTVDraftSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblTVDraftDUDoc.Visible = False
                    Else
                        lblTVDraftDUDoc.Visible = True
                        lblTVDraftDUDoc.Text = dr.Item("datDocModifingDate")
                    End If

                    If IsDBNull(dr.Item("PDFData")) Then
                        txtTVDraftPDF.Text = ""
                    Else
                        txtTVDraftPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblTVDraftSRPDF.Visible = False
                    Else
                        lblTVDraftSRPDF.Visible = True
                        lblTVDraftSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblTVDraftDUPDF.Visible = False
                    Else
                        lblTVDraftDUPDF.Visible = True
                        lblTVDraftDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtTVDraftDoc.Text = "On File" Or txtTVDraftPDF.Text = "On File" Then
                    btnTVDraftDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeleteTVDraft.Visible = True
                    Else
                        btnDeleteTVDraft.Visible = False
                    End If
                Else
                    btnTVDraftDownload.Visible = False
                    btnDeleteTVDraft.Visible = False
                End If
            Else
                txtTVDraftDoc.Clear()
                txtTVDraftPDF.Clear()
                txtTVDraftDoc.Visible = False
                lblTVDraftSRDoc.Visible = False
                lblTVDraftDUDoc.Visible = False
                txtTVDraftPDF.Visible = False
                lblTVDraftSRPDF.Visible = False
                lblTVDraftDUPDF.Visible = False
                btnTVDraft.Visible = False
                btnTVDraftDownload.Visible = False
                btnDeleteTVDraft.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbTVPublicNotice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbTVPublicNotice.CheckedChanged
        Try

            If chbTVPublicNotice.Checked = True And MasterApp <> "" Then
                txtTVPublicNoticeDoc.Visible = True
                txtTVPublicNoticePDF.Visible = True
                btnTVPublicNotice.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'VP-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                "and numUserID = strPDFModifingPerson " & _
                "and strFileName = 'VP-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'VP-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtTVPublicNoticeDoc.Text = ""
                    Else
                        txtTVPublicNoticeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblTVPublicNoticeSRDoc.Visible = False
                    Else
                        lblTVPublicNoticeSRDoc.Visible = True
                        lblTVPublicNoticeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblTVPublicNoticeDUDoc.Visible = False
                    Else
                        lblTVPublicNoticeDUDoc.Visible = True
                        lblTVPublicNoticeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If

                    If IsDBNull(dr.Item("PDFData")) Then
                        txtTVPublicNoticePDF.Text = ""
                    Else
                        txtTVPublicNoticePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblTVPublicNoticeSRPDF.Visible = False
                    Else
                        lblTVPublicNoticeSRPDF.Visible = True
                        lblTVPublicNoticeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblTVPublicNoticeDUPDF.Visible = False
                    Else
                        lblTVPublicNoticeDUPDF.Visible = True
                        lblTVPublicNoticeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtTVPublicNoticeDoc.Text = "On File" Or txtTVPublicNoticePDF.Text = "On File" Then
                    btnTVPublicNoticeDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeleteTVPublicNot.Visible = True
                    Else
                        btnDeleteTVPublicNot.Visible = False
                    End If
                Else
                    btnTVPublicNoticeDownload.Visible = False
                    btnDeleteTVPublicNot.Visible = False
                End If
            Else
                txtTVPublicNoticeDoc.Clear()
                txtTVPublicNoticePDF.Clear()
                txtTVPublicNoticeDoc.Visible = False
                lblTVPublicNoticeSRDoc.Visible = False
                lblTVPublicNoticeDUDoc.Visible = False
                txtTVPublicNoticePDF.Visible = False
                lblTVPublicNoticeSRPDF.Visible = False
                lblTVPublicNoticeDUPDF.Visible = False
                btnTVPublicNotice.Visible = False
                btnTVPublicNoticeDownload.Visible = False
                btnDeleteTVPublicNot.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbTVFinal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbTVFinal.CheckedChanged
        Try

            If chbTVFinal.Checked = True And MasterApp <> "" Then
                txtTVFinalDoc.Visible = True
                txtTVFinalPDF.Visible = True
                btnTVFinal.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'VF-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'VF-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'VF-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtTVFinalDoc.Text = ""
                    Else
                        txtTVFinalDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblTVFinalSRDoc.Visible = False
                    Else
                        lblTVFinalSRDoc.Visible = True
                        lblTVFinalSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblTVFinalDUDoc.Visible = False
                    Else
                        lblTVFinalDUDoc.Visible = True
                        lblTVFinalDUDoc.Text = dr.Item("datDocModifingDate")
                    End If

                    If IsDBNull(dr.Item("PDFData")) Then
                        txtTVFinalPDF.Text = ""
                    Else
                        txtTVFinalPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblTVFinalSRPDF.Visible = False
                    Else
                        lblTVFinalSRPDF.Visible = True
                        lblTVFinalSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblTVFinalDUPDF.Visible = False
                    Else
                        lblTVFinalDUPDF.Visible = True
                        lblTVFinalDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtTVFinalDoc.Text = "On File" Or txtTVFinalPDF.Text = "On File" Then
                    btnTVFinalDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeleteTVFinal.Visible = True
                    Else
                        btnDeleteTVFinal.Visible = False
                    End If
                Else
                    btnTVFinalDownload.Visible = False
                    btnDeleteTVFinal.Visible = False
                End If
            Else
                txtTVFinalDoc.Clear()
                txtTVFinalPDF.Clear()
                txtTVFinalDoc.Visible = False
                lblTVFinalSRDoc.Visible = False
                lblTVFinalDUDoc.Visible = False
                txtTVFinalPDF.Visible = False
                lblTVFinalSRPDF.Visible = False
                lblTVFinalDUPDF.Visible = False
                btnTVFinal.Visible = False
                btnTVFinalDownload.Visible = False
                btnDeleteTVFinal.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPSDApplicationSummary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDApplicationSummary.CheckedChanged
        Try

            If chbPSDApplicationSummary.Checked = True And MasterApp <> "" Then
                txtPSDAppSummaryDoc.Visible = True

                txtPSDAppSummaryPDF.Visible = True
                btnPSDAppSummary.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                'btnPSDAppSummary.Location = New System.Drawing.Point(119, 357)
                'txtPSDAppSummaryDoc.Location = New System.Drawing.Point(149, 358)
                'lblPSDAppSummarySRDoc.Location = New System.Drawing.Point(155, 382)
                'lblPSDAppSummaryDUDoc.Location = New System.Drawing.Point(155, 399)
                'txtPSDAppSummaryPDF.Location = New System.Drawing.Point(396, 358)
                'lblPSDAppSummarySRPDF.Location = New System.Drawing.Point(405, 382)
                'lblPSDAppSummaryDUPDF.Location = New System.Drawing.Point(405, 399)
                'btnPSDAppSummaryDownload.Location = New System.Drawing.Point(643, 357)
                'btnDeletePSDAppSummary.Location = New System.Drawing.Point(643, 380)

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PA-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PA-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PA-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDAppSummaryDoc.Text = ""
                    Else
                        txtPSDAppSummaryDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDAppSummarySRDoc.Visible = False
                    Else
                        lblPSDAppSummarySRDoc.Visible = True
                        lblPSDAppSummarySRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDAppSummaryDUDoc.Visible = False
                    Else
                        lblPSDAppSummaryDUDoc.Visible = True
                        lblPSDAppSummaryDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDAppSummaryPDF.Text = ""
                    Else
                        txtPSDAppSummaryPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDAppSummarySRPDF.Visible = False
                    Else
                        lblPSDAppSummarySRPDF.Visible = True
                        lblPSDAppSummarySRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDAppSummaryDUPDF.Visible = False
                    Else
                        lblPSDAppSummaryDUPDF.Visible = True
                        lblPSDAppSummaryDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDAppSummaryDoc.Text = "On File" Or txtPSDAppSummaryPDF.Text = "On File" Then
                    btnPSDAppSummaryDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeletePSDAppSummary.Visible = True
                    Else
                        btnDeletePSDAppSummary.Visible = False
                    End If
                Else
                    btnPSDAppSummaryDownload.Visible = False
                    btnDeletePSDAppSummary.Visible = False
                End If
            Else
                txtPSDAppSummaryDoc.Clear()
                txtPSDAppSummaryPDF.Clear()
                txtPSDAppSummaryDoc.Visible = False
                lblPSDAppSummarySRDoc.Visible = False
                lblPSDAppSummaryDUDoc.Visible = False
                txtPSDAppSummaryPDF.Visible = False
                lblPSDAppSummarySRPDF.Visible = False
                lblPSDAppSummaryDUPDF.Visible = False
                btnPSDAppSummary.Visible = False
                btnPSDAppSummaryDownload.Visible = False
                btnDeletePSDAppSummary.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPSDPrelimDet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDPrelimDet.CheckedChanged
        Try

            If chbPSDPrelimDet.Checked = True And MasterApp <> "" Then
                txtPSDPrelimDetDoc.Visible = True
                txtPSDPrelimDetPDF.Visible = True
                btnPSDPrelimDet.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True


                btnPSDPrelimDet.Location = New System.Drawing.Point(119, 15)
                txtPSDPrelimDetDoc.Location = New System.Drawing.Point(149, 16)
                txtPSDPrelimDetPDF.Location = New System.Drawing.Point(396, 16)
                btnPSDPrelimDetDownload.Location = New System.Drawing.Point(643, 15)
                lblPSDPrelimDetSRDoc.Location = New System.Drawing.Point(155, 40)
                lblPSDPrelimDetDUDoc.Location = New System.Drawing.Point(155, 58)
                lblPSDPrelimDetSRPDF.Location = New System.Drawing.Point(405, 40)
                lblPSDPrelimDetDUPDF.Location = New System.Drawing.Point(405, 58)
                btnDeletePSDPrelimDet.Location = New System.Drawing.Point(643, 38)


                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PP-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PP-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PP-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDPrelimDetDoc.Text = ""
                    Else
                        txtPSDPrelimDetDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDPrelimDetSRDoc.Visible = False
                    Else
                        lblPSDPrelimDetSRDoc.Visible = True
                        lblPSDPrelimDetSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDPrelimDetDUDoc.Visible = False
                    Else
                        lblPSDPrelimDetDUDoc.Visible = True
                        lblPSDPrelimDetDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDPrelimDetPDF.Text = ""
                    Else
                        txtPSDPrelimDetPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDPrelimDetSRPDF.Visible = False
                    Else
                        lblPSDPrelimDetSRPDF.Visible = True
                        lblPSDPrelimDetSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDPrelimDetDUPDF.Visible = False
                    Else
                        lblPSDPrelimDetDUPDF.Visible = True
                        lblPSDPrelimDetDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDPrelimDetDoc.Text = "On File" Or txtPSDPrelimDetPDF.Text = "On File" Then
                    btnPSDPrelimDetDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeletePSDPrelimDet.Visible = True
                    Else
                        btnDeletePSDPrelimDet.Visible = False
                    End If
                Else
                    btnPSDPrelimDetDownload.Visible = False
                    btnDeletePSDPrelimDet.Visible = False
                End If
            Else
                txtPSDPrelimDetDoc.Clear()
                txtPSDPrelimDetPDF.Clear()
                txtPSDPrelimDetDoc.Visible = False
                lblPSDPrelimDetSRDoc.Visible = False
                lblPSDPrelimDetDUDoc.Visible = False
                txtPSDPrelimDetPDF.Visible = False
                lblPSDPrelimDetSRPDF.Visible = False
                lblPSDPrelimDetDUPDF.Visible = False
                btnPSDPrelimDet.Visible = False
                btnPSDPrelimDetDownload.Visible = False
                btnDeletePSDPrelimDet.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPSDNarrative_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDNarrative.CheckedChanged
        Try

            If chbPSDNarrative.Checked = True And MasterApp <> "" Then
                txtPSDNarrativeDoc.Visible = True
                txtPSDNarrativePDF.Visible = True
                btnPSDNarrative.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                'btnPSDNarrative.Location = New System.Drawing.Point(119, 414)
                'txtPSDNarrativeDoc.Location = New System.Drawing.Point(149, 415)
                'lblPSDNarrativeSRDoc.Location = New System.Drawing.Point(155, 438)
                'lblPSDNarrativeDUDoc.Location = New System.Drawing.Point(155, 454)
                'txtPSDNarrativePDF.Location = New System.Drawing.Point(396, 415)
                'lblPSDNarrativeSRPDF.Location = New System.Drawing.Point(405, 438)
                'lblPSDNarrativeDUPDF.Location = New System.Drawing.Point(406, 454)
                'btnPSDNarrativeDownload.Location = New System.Drawing.Point(643, 414)
                'btnDeletePSDNarrative.Location = New System.Drawing.Point(643, 438)

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PT-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PT-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PT-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDNarrativeDoc.Text = ""
                    Else
                        txtPSDNarrativeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDNarrativeSRDoc.Visible = False
                    Else
                        lblPSDNarrativeSRDoc.Visible = True
                        lblPSDNarrativeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDNarrativeDUDoc.Visible = False
                    Else
                        lblPSDNarrativeDUDoc.Visible = True
                        lblPSDNarrativeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDNarrativePDF.Text = ""
                    Else
                        txtPSDNarrativePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDNarrativeSRPDF.Visible = False
                    Else
                        lblPSDNarrativeSRPDF.Visible = True
                        lblPSDNarrativeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDNarrativeDUPDF.Visible = False
                    Else
                        lblPSDNarrativeDUPDF.Visible = True
                        lblPSDNarrativeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDNarrativeDoc.Text = "On File" Or txtPSDNarrativePDF.Text = "On File" Then
                    btnPSDNarrativeDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeletePSDNarrative.Visible = True
                    Else
                        btnDeletePSDNarrative.Visible = False
                    End If
                Else
                    btnPSDNarrativeDownload.Visible = False
                    btnDeletePSDNarrative.Visible = False
                End If
            Else
                txtPSDNarrativeDoc.Clear()
                txtPSDNarrativePDF.Clear()
                txtPSDNarrativeDoc.Visible = False
                lblPSDNarrativeSRDoc.Visible = False
                lblPSDNarrativeDUDoc.Visible = False
                txtPSDNarrativePDF.Visible = False
                lblPSDNarrativeSRPDF.Visible = False
                lblPSDNarrativeDUPDF.Visible = False
                btnPSDNarrative.Visible = False
                btnPSDNarrativeDownload.Visible = False
                btnDeletePSDNarrative.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPSDDraftPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDDraftPermit.CheckedChanged
        Try

            If chbPSDDraftPermit.Checked = True And MasterApp <> "" Then
                txtPSDDraftPermitDoc.Visible = True
                txtPSDDraftPermitPDF.Visible = True
                btnPSDDraftPermit.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                'btnPSDDraftPermit.Location = New System.Drawing.Point(119, 73)
                'txtPSDDraftPermitDoc.Location = New System.Drawing.Point(149, 74)
                'lblPSDDraftPermitSRDoc.Location = New System.Drawing.Point(155, 98)
                'lblPSDDraftPermitDUDoc.Location = New System.Drawing.Point(155, 115)
                'txtPSDDraftPermitPDF.Location = New System.Drawing.Point(396, 74)
                'lblPSDDraftPermitSRPDF.Location = New System.Drawing.Point(405, 98)
                'lblPSDDraftPermitDUPDF.Location = New System.Drawing.Point(405, 115)
                'btnPSDDraftPermitDownload.Location = New System.Drawing.Point(643, 73)
                'btnDeletePSDDraftPermit.Location = New System.Drawing.Point(643, 96)

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PD-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PD-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PD-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDDraftPermitDoc.Text = ""
                    Else
                        txtPSDDraftPermitDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDDraftPermitSRDoc.Visible = False
                    Else
                        lblPSDDraftPermitSRDoc.Visible = True
                        lblPSDDraftPermitSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDDraftPermitDUDoc.Visible = False
                    Else
                        lblPSDDraftPermitDUDoc.Visible = True
                        lblPSDDraftPermitDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDDraftPermitPDF.Text = ""
                    Else
                        txtPSDDraftPermitPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDDraftPermitSRPDF.Visible = False
                    Else
                        lblPSDDraftPermitSRPDF.Visible = True
                        lblPSDDraftPermitSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDDraftPermitDUPDF.Visible = False
                    Else
                        lblPSDDraftPermitDUPDF.Visible = True
                        lblPSDDraftPermitDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDDraftPermitDoc.Text = "On File" Or txtPSDDraftPermitPDF.Text = "On File" Then
                    btnPSDDraftPermitDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeletePSDDraftPermit.Visible = True
                    Else
                        btnDeletePSDDraftPermit.Visible = False
                    End If
                Else
                    btnPSDDraftPermitDownload.Visible = False
                    btnDeletePSDDraftPermit.Visible = False
                End If
            Else
                txtPSDDraftPermitDoc.Clear()
                txtPSDDraftPermitPDF.Clear()
                txtPSDDraftPermitDoc.Visible = False
                lblPSDDraftPermitSRDoc.Visible = False
                lblPSDDraftPermitDUDoc.Visible = False
                txtPSDDraftPermitPDF.Visible = False
                lblPSDDraftPermitSRPDF.Visible = False
                lblPSDDraftPermitDUPDF.Visible = False
                btnPSDDraftPermit.Visible = False
                btnPSDDraftPermitDownload.Visible = False
                btnDeletePSDDraftPermit.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPSDPublicNotice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDPublicNotice.CheckedChanged
        Try

            If chbPSDPublicNotice.Checked = True And MasterApp <> "" Then
                txtPSDPublicNoticeDoc.Visible = True
                txtPSDPublicNoticePDF.Visible = True
                btnPSDPublicNotice.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                'btnPSDPublicNotice.Location = New System.Drawing.Point(119, 130)
                'txtPSDPublicNoticeDoc.Location = New System.Drawing.Point(149, 131)
                'lblPSDPublicNoticeSRDoc.Location = New System.Drawing.Point(155, 154)
                'lblPSDPublicNoticeDUDoc.Location = New System.Drawing.Point(155, 174)
                'txtPSDPublicNoticePDF.Location = New System.Drawing.Point(396, 131)
                'lblPSDPublicNoticeSRPDF.Location = New System.Drawing.Point(405, 154)
                'lblPSDPublicNoticeDUPDF.Location = New System.Drawing.Point(405, 174)
                'btnPSDPublicNoticeDownload.Location = New System.Drawing.Point(643, 130)
                'btnDeletePSDPublicNotice.Location = New System.Drawing.Point(643, 154)

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'PN-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                "and numUserID = strPDFModifingPerson " & _
                "and strFileName = 'PN-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'PN-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDPublicNoticeDoc.Text = ""
                    Else
                        txtPSDPublicNoticeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDPublicNoticeSRDoc.Visible = False
                    Else
                        lblPSDPublicNoticeSRDoc.Visible = True
                        lblPSDPublicNoticeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDPublicNoticeDUDoc.Visible = False
                    Else
                        lblPSDPublicNoticeDUDoc.Visible = True
                        lblPSDPublicNoticeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDPublicNoticePDF.Text = ""
                    Else
                        txtPSDPublicNoticePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDPublicNoticeSRPDF.Visible = False
                    Else
                        lblPSDPublicNoticeSRPDF.Visible = True
                        lblPSDPublicNoticeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDPublicNoticeDUPDF.Visible = False
                    Else
                        lblPSDPublicNoticeDUPDF.Visible = True
                        lblPSDPublicNoticeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDPublicNoticeDoc.Text = "On File" Or txtPSDPublicNoticePDF.Text = "On File" Then
                    btnPSDPublicNoticeDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeletePSDPublicNotice.Visible = True
                    Else
                        btnDeletePSDPublicNotice.Visible = False
                    End If
                Else
                    btnPSDPublicNoticeDownload.Visible = False
                    btnDeletePSDPublicNotice.Visible = False
                End If
            Else
                txtPSDPublicNoticeDoc.Clear()
                txtPSDPublicNoticePDF.Clear()
                txtPSDPublicNoticeDoc.Visible = False
                lblPSDPublicNoticeSRDoc.Visible = False
                lblPSDPublicNoticeDUDoc.Visible = False
                txtPSDPublicNoticePDF.Visible = False
                lblPSDPublicNoticeSRPDF.Visible = False
                lblPSDPublicNoticeDUPDF.Visible = False
                btnPSDPublicNotice.Visible = False
                btnPSDPublicNoticeDownload.Visible = False
                btnDeletePSDPublicNotice.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPSDHearingNotice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDHearingNotice.CheckedChanged
        Try

            If chbPSDHearingNotice.Checked = True And MasterApp <> "" Then
                txtPSDHearingNoticeDoc.Visible = True
                txtPSDHearingNoticePDF.Visible = True
                btnPSDHearingNotice.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                'btnPSDHearingNotice.Location = New System.Drawing.Point(119, 189)
                'txtPSDHearingNoticeDoc.Location = New System.Drawing.Point(149, 190)
                'lblPSDHearingNoticeSRDoc.Location = New System.Drawing.Point(155, 213)
                'lblPSDHearingNoticeDUDoc.Location = New System.Drawing.Point(155, 230)
                'txtPSDHearingNoticePDF.Location = New System.Drawing.Point(396, 190)
                'lblPSDHearingNoticeSRPDF.Location = New System.Drawing.Point(405, 213)
                'lblPSDHearingNoticeDUPDF.Location = New System.Drawing.Point(405, 230)
                'btnPSDHearingNoticeDownload.Location = New System.Drawing.Point(643, 189)
                'btnDeletePSDHearingNotice.Location = New System.Drawing.Point(643, 213)

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PH-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PH-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PH-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDHearingNoticeDoc.Text = ""
                    Else
                        txtPSDHearingNoticeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDHearingNoticeSRDoc.Visible = False
                    Else
                        lblPSDHearingNoticeSRDoc.Visible = True
                        lblPSDHearingNoticeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDHearingNoticeDUDoc.Visible = False
                    Else
                        lblPSDHearingNoticeDUDoc.Visible = True
                        lblPSDHearingNoticeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDHearingNoticePDF.Text = ""
                    Else
                        txtPSDHearingNoticePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDHearingNoticeSRPDF.Visible = False
                    Else
                        lblPSDHearingNoticeSRPDF.Visible = True
                        lblPSDHearingNoticeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDHearingNoticeDUPDF.Visible = False
                    Else
                        lblPSDHearingNoticeDUPDF.Visible = True
                        lblPSDHearingNoticeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDHearingNoticeDoc.Text = "On File" Or txtPSDHearingNoticePDF.Text = "On File" Then
                    btnPSDHearingNoticeDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeletePSDHearingNotice.Visible = True
                    Else
                        btnDeletePSDHearingNotice.Visible = False
                    End If
                Else
                    btnPSDHearingNoticeDownload.Visible = False
                    btnDeletePSDHearingNotice.Visible = False
                End If
            Else
                txtPSDHearingNoticeDoc.Clear()
                txtPSDHearingNoticePDF.Clear()
                txtPSDHearingNoticeDoc.Visible = False
                lblPSDHearingNoticeSRDoc.Visible = False
                lblPSDHearingNoticeDUDoc.Visible = False
                txtPSDHearingNoticePDF.Visible = False
                lblPSDHearingNoticeSRPDF.Visible = False
                lblPSDHearingNoticeDUPDF.Visible = False
                btnPSDHearingNotice.Visible = False
                btnPSDHearingNoticeDownload.Visible = False
                btnDeletePSDHearingNotice.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPSDFinalDet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDFinalDet.CheckedChanged
        Try

            If chbPSDFinalDet.Checked = True And MasterApp <> "" Then
                txtPSDFinalDetDoc.Visible = True
                txtPSDFinalDetPDF.Visible = True
                btnPSDFinalDet.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                'btnPSDFinalDet.Location = New System.Drawing.Point(119, 245)
                'txtPSDFinalDetDoc.Location = New System.Drawing.Point(149, 246)
                'lblPSDFinalDetSRDoc.Location = New System.Drawing.Point(155, 268)
                'lblPSDFinalDetDUDoc.Location = New System.Drawing.Point(155, 284)
                'txtPSDFinalDetPDF.Location = New System.Drawing.Point(396, 246)
                'lblPSDFinalDetSRPDF.Location = New System.Drawing.Point(405, 268)
                'lblPSDFinalDetDUPDF.Location = New System.Drawing.Point(405, 284)
                'btnPSDFinalDetDownload.Location = New System.Drawing.Point(643, 245)
                'btnDeletePSDFinalDet.Location = New System.Drawing.Point(643, 268)

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'PF-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'PF-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'PF-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDFinalDetDoc.Text = ""
                    Else
                        txtPSDFinalDetDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDFinalDetSRDoc.Visible = False
                    Else
                        lblPSDFinalDetSRDoc.Visible = True
                        lblPSDFinalDetSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDFinalDetDUDoc.Visible = False
                    Else
                        lblPSDFinalDetDUDoc.Visible = True
                        lblPSDFinalDetDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDFinalDetPDF.Text = ""
                    Else
                        txtPSDFinalDetPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDFinalDetSRPDF.Visible = False
                    Else
                        lblPSDFinalDetSRPDF.Visible = True
                        lblPSDFinalDetSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDFinalDetDUPDF.Visible = False
                    Else
                        lblPSDFinalDetDUPDF.Visible = True
                        lblPSDFinalDetDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDFinalDetDoc.Text = "On File" Or txtPSDFinalDetPDF.Text = "On File" Then
                    btnPSDFinalDetDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeletePSDFinalDet.Visible = True
                    Else
                        btnDeletePSDFinalDet.Visible = False
                    End If
                Else
                    btnPSDFinalDetDownload.Visible = False
                    btnDeletePSDFinalDet.Visible = False
                End If
            Else
                txtPSDFinalDetDoc.Clear()
                txtPSDFinalDetPDF.Clear()
                txtPSDFinalDetDoc.Visible = False
                lblPSDFinalDetSRDoc.Visible = False
                lblPSDFinalDetDUDoc.Visible = False
                txtPSDFinalDetPDF.Visible = False
                lblPSDFinalDetSRPDF.Visible = False
                lblPSDFinalDetDUPDF.Visible = False
                btnPSDFinalDet.Visible = False
                btnPSDFinalDetDownload.Visible = False
                btnDeletePSDFinalDet.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPSDFinalPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPSDFinalPermit.CheckedChanged
        Try

            If chbPSDFinalPermit.Checked = True And MasterApp <> "" Then
                txtPSDFinalPermitDoc.Visible = True
                txtPSDFinalPermitPDF.Visible = True
                btnPSDFinalPermit.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                'btnPSDFinalPermit.Location = New System.Drawing.Point(119, 302)
                'txtPSDFinalPermitDoc.Location = New System.Drawing.Point(149, 303)
                'lblPSDFinalPermitSRDoc.Location = New System.Drawing.Point(155, 326)
                'lblPSDFinalPermitDUDoc.Location = New System.Drawing.Point(155, 342)
                'txtPSDFinalPermitPDF.Location = New System.Drawing.Point(396, 303)
                'lblPSDFinalPermitSRPDF.Location = New System.Drawing.Point(405, 326)
                'lblPSDFinalPermitDUPDF.Location = New System.Drawing.Point(406, 342)
                'btnPSDFinalPermitDownload.Location = New System.Drawing.Point(643, 302)
                'btnDeletePSDFinalPermit.Location = New System.Drawing.Point(643, 326)

                SQL = "select " & _
                "case " & _
                "when docPermitData is Null then '' " & _
                "Else 'True' " & _
                "End DocData, " & _
                "case " & _
                "when strDocModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                "and numUserID = strDocModifingPerson " & _
                "and strFileName = 'PI-" & MasterApp & "') " & _
                "end DocStaffResponsible, " & _
                "case " & _
                "when datDocModifingDate is Null then '' " & _
                "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                "End datDocModifingDate, " & _
                "case " & _
                "when pdfPermitData is Null then '' " & _
                "Else 'True' " & _
                "End PDFData, " & _
                "case " & _
                "when strPDFModifingPerson is Null then '' " & _
                "else (select (strLastName||', '||strFirstName) as StaffName " & _
                "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                "and numUserID = strPDFModifingPerson " & _
                "and strFileName = 'PI-" & MasterApp & "') " & _
                "end PDFStaffResponsible, " & _
                "case " & _
                "when datPDFModifingDate is Null then '' " & _
                "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                "End datPDFModifingDate " & _
                "from AIRBRANCH.APBPermits " & _
                "where strFileName = 'PI-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtPSDFinalPermitDoc.Text = ""
                    Else
                        txtPSDFinalPermitDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblPSDFinalPermitSRDoc.Visible = False
                    Else
                        lblPSDFinalPermitSRDoc.Visible = True
                        lblPSDFinalPermitSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblPSDFinalPermitDUDoc.Visible = False
                    Else
                        lblPSDFinalPermitDUDoc.Visible = True
                        lblPSDFinalPermitDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtPSDFinalPermitPDF.Text = ""
                    Else
                        txtPSDFinalPermitPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblPSDFinalPermitSRPDF.Visible = False
                    Else
                        lblPSDFinalPermitSRPDF.Visible = True
                        lblPSDFinalPermitSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblPSDFinalPermitDUPDF.Visible = False
                    Else
                        lblPSDFinalPermitDUPDF.Visible = True
                        lblPSDFinalPermitDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtPSDFinalPermitDoc.Text = "On File" Or txtPSDFinalPermitPDF.Text = "On File" Then
                    btnPSDFinalPermitDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeletePSDFinalPermit.Visible = True
                    Else
                        btnDeletePSDFinalPermit.Visible = False
                    End If
                Else
                    btnPSDFinalPermitDownload.Visible = False
                    btnDeletePSDFinalPermit.Visible = False
                End If
            Else
                txtPSDFinalPermitDoc.Clear()
                txtPSDFinalPermitPDF.Clear()
                txtPSDFinalPermitDoc.Visible = False
                lblPSDFinalPermitSRDoc.Visible = False
                lblPSDFinalPermitDUDoc.Visible = False
                txtPSDFinalPermitPDF.Visible = False
                lblPSDFinalPermitSRPDF.Visible = False
                lblPSDFinalPermitDUPDF.Visible = False
                btnPSDFinalPermit.Visible = False
                btnPSDFinalPermitDownload.Visible = False
                btnDeletePSDFinalPermit.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbOtherNarrative_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbOtherNarrative.CheckedChanged
        Try
            If chbOtherNarrative.Checked = True And MasterApp <> "" Then
                txtOtherNarrativeDoc.Visible = True
                txtOtherNarrativePDF.Visible = True
                btnOtherNarrative.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                 "case " & _
                 "when docPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End DocData, " & _
                 "case " & _
                 "when strDocModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                 "and numUserID = strDocModifingPerson " & _
                 "and strFileName = 'ON-" & MasterApp & "') " & _
                 "end DocStaffResponsible, " & _
                 "case " & _
                 "when datDocModifingDate is Null then '' " & _
                 "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                 "End datDocModifingDate, " & _
                 "case " & _
                 "when pdfPermitData is Null then '' " & _
                 "Else 'True' " & _
                 "End PDFData, " & _
                 "case " & _
                 "when strPDFModifingPerson is Null then '' " & _
                 "else (select (strLastName||', '||strFirstName) as StaffName " & _
                 "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                 "and numUserID = strPDFModifingPerson " & _
                 "and strFileName = 'ON-" & MasterApp & "') " & _
                 "end PDFStaffResponsible, " & _
                 "case " & _
                 "when datPDFModifingDate is Null then '' " & _
                 "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                 "End datPDFModifingDate " & _
                 "from AIRBRANCH.APBPermits " & _
                 "where strFileName = 'ON-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtOtherNarrativeDoc.Text = ""
                    Else
                        txtOtherNarrativeDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblOtherNarrativeSRDoc.Visible = False
                    Else
                        lblOtherNarrativeSRDoc.Visible = True
                        lblOtherNarrativeSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblOtherNarrativeDUDoc.Visible = False
                    Else
                        lblOtherNarrativeDUDoc.Visible = True
                        lblOtherNarrativeDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtOtherNarrativePDF.Text = ""
                    Else
                        txtOtherNarrativePDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblOtherNarrativeSRPDF.Visible = False
                    Else
                        lblOtherNarrativeSRPDF.Visible = True
                        lblOtherNarrativeSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblOtherNarrativeDUPDF.Visible = False
                    Else
                        lblOtherNarrativeDUPDF.Visible = True
                        lblOtherNarrativeDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtOtherNarrativeDoc.Text = "On File" Or txtOtherNarrativePDF.Text = "On File" Then
                    btnOtherNarrativeDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeleteOtherNarrative.Visible = True
                    Else
                        btnDeleteOtherNarrative.Visible = False
                    End If
                Else
                    btnOtherNarrativeDownload.Visible = False
                    btnDeleteOtherNarrative.Visible = False
                End If
            Else
                txtOtherNarrativeDoc.Clear()
                txtOtherNarrativePDF.Clear()
                txtOtherNarrativeDoc.Visible = False
                lblOtherNarrativeSRDoc.Visible = False
                lblOtherNarrativeDUDoc.Visible = False
                txtOtherNarrativePDF.Clear()
                txtOtherNarrativePDF.Visible = False
                lblOtherNarrativeSRPDF.Visible = False
                lblOtherNarrativeDUPDF.Visible = False
                btnOtherNarrative.Visible = False
                btnOtherNarrativeDownload.Visible = False
                btnDeleteOtherNarrative.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbOtherPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbOtherPermit.CheckedChanged
        Try

            If chbOtherPermit.Checked = True And MasterApp <> "" Then
                txtOtherPermitDoc.Visible = True
                txtOtherPermitPDF.Visible = True
                btnOtherPermit.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " & _
                  "case " & _
                  "when docPermitData is Null then '' " & _
                  "Else 'True' " & _
                  "End DocData, " & _
                  "case " & _
                  "when strDocModifingPerson is Null then '' " & _
                  "else (select (strLastName||', '||strFirstName) as StaffName " & _
                  "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                  "where AIRBRANCH.APBPermits.strDocModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID " & _
                  "and numUserID = strDocModifingPerson " & _
                  "and strFileName = 'OP-" & MasterApp & "') " & _
                  "end DocStaffResponsible, " & _
                  "case " & _
                  "when datDocModifingDate is Null then '' " & _
                  "else to_char(datDocModifingDate, 'dd-Mon-yyyy') " & _
                  "End datDocModifingDate, " & _
                  "case " & _
                  "when pdfPermitData is Null then '' " & _
                  "Else 'True' " & _
                  "End PDFData, " & _
                  "case " & _
                  "when strPDFModifingPerson is Null then '' " & _
                  "else (select (strLastName||', '||strFirstName) as StaffName " & _
                  "from AIRBRANCH.APBPermits, AIRBRANCH.EPDUserProfiles " & _
                  "where AIRBRANCH.APBPermits.strPDFModifingPerson = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                  "and numUserID = strPDFModifingPerson " & _
                  "and strFileName = 'OP-" & MasterApp & "') " & _
                  "end PDFStaffResponsible, " & _
                  "case " & _
                  "when datPDFModifingDate is Null then '' " & _
                  "else to_char(datPDFModifingdate, 'dd-Mon-yyyy') " & _
                  "End datPDFModifingDate " & _
                  "from AIRBRANCH.APBPermits " & _
                  "where strFileName = 'OP-" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("DocData")) Then
                        txtOtherPermitDoc.Text = ""
                    Else
                        txtOtherPermitDoc.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("DocstaffResponsible")) Then
                        lblOtherPermitSRDoc.Visible = False
                    Else
                        lblOtherPermitSRDoc.Visible = True
                        lblOtherPermitSRDoc.Text = dr.Item("DocStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datDocModifingdate")) Then
                        lblOtherPermitDUDoc.Visible = False
                    Else
                        lblOtherPermitDUDoc.Visible = True
                        lblOtherPermitDUDoc.Text = dr.Item("datDocModifingDate")
                    End If
                    If IsDBNull(dr.Item("PDFData")) Then
                        txtOtherPermitPDF.Text = ""
                    Else
                        txtOtherPermitPDF.Text = "On File"
                    End If
                    If IsDBNull(dr.Item("PDFstaffResponsible")) Then
                        lblOtherPermitSRPDF.Visible = False
                    Else
                        lblOtherPermitSRPDF.Visible = True
                        lblOtherPermitSRPDF.Text = dr.Item("PDFStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datPDFModifingdate")) Then
                        lblOtherPermitDUPDF.Visible = False
                    Else
                        lblOtherPermitDUPDF.Visible = True
                        lblOtherPermitDUPDF.Text = dr.Item("datPDFModifingDate")
                    End If
                End If
                dr.Close()
                If txtOtherPermitDoc.Text = "On File" Or txtOtherPermitPDF.Text = "On File" Then
                    btnOtherPermitDownload.Visible = True
                    If AccountFormAccess(9, 3) = "1" Then
                        btnDeleteOtherPermit.Visible = True
                    Else
                        btnDeleteOtherPermit.Visible = False
                    End If
                Else
                    btnOtherPermitDownload.Visible = False
                    btnDeleteOtherPermit.Visible = False
                End If

            Else
                txtOtherPermitDoc.Clear()
                txtOtherPermitPDF.Clear()
                txtOtherPermitDoc.Visible = False
                lblOtherPermitSRDoc.Visible = False
                lblOtherPermitDUDoc.Visible = False
                txtOtherPermitPDF.Visible = False
                lblOtherPermitSRPDF.Visible = False
                lblOtherPermitDUPDF.Visible = False
                btnOtherPermit.Visible = False
                btnOtherPermitDownload.Visible = False
                btnDeleteOtherPermit.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

#Region "Document upload buttons"

    Private Sub UploadButtons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles btnOtherNarrative.Click, btnOtherPermit.Click, _
        btnPSDAppSummary.Click, btnPSDDraftPermit.Click, btnPSDFinalDet.Click, _
        btnPSDFinalPermit.Click, btnPSDHearingNotice.Click, btnPSDNarrative.Click, _
        btnPSDPrelimDet.Click, btnPSDPublicNotice.Click, _
        btnTVDraft.Click, btnTVFinal.Click, btnTVNarrative.Click, btnTVPublicNotice.Click

        Dim dialog As New OpenFileDialog

        Try
            dialog.InitialDirectory = GetUserSetting(UserSetting.PermitUploadLocation)
            dialog.Filter = "Word files (*.docx, *.doc)|*.docx;*.doc|PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
            dialog.FilterIndex = 3

            If dialog.ShowDialog = DialogResult.OK Then
                If File.Exists(dialog.FileName) Then

                    If Not Path.GetDirectoryName(dialog.FileName) = dialog.InitialDirectory Then
                        SaveUserSetting(UserSetting.PermitUploadLocation, Path.GetDirectoryName(dialog.FileName))
                    End If

                    Dim thisButton As Button = DirectCast(sender, Button)
                    Dim thisControlName As String = thisButton.Name.Replace("btn", "txt")
                    Dim thisControl As TextBox = Nothing

                    Try
                        Select Case Path.GetExtension(dialog.FileName).ToUpper
                            Case ".DOC", ".DOCX"
                                thisControl = DirectCast(thisButton.Parent.Controls(thisControlName & "Doc"), TextBox)
                            Case ".PDF"
                                thisControl = DirectCast(thisButton.Parent.Controls(thisControlName & "PDF"), TextBox)
                        End Select

                        If thisControl IsNot Nothing Then thisControl.Text = dialog.FileName

                    Catch ex As Exception
                        MessageBox.Show("There was an error selecting the file. Please contact the DMU.")
                    End Try

                Else
                    MessageBox.Show("Could not read file. Please try again.")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            dialog.Dispose()
        End Try
    End Sub

#End Region

#Region "Download files"

    Sub DownloadFile(ByVal FileName As String, ByVal FileType As String)
        Try
            Dim PermitNumber As String = ""
            Dim path As New SaveFileDialog
            Dim DestFilePath As String = "N/A"

            If FileType <> "00" Then
                SQL = "select strApplicationNumber, " & _
                "strPermitNumber,  " & _
                "(substr(strPermitNumber,1, 4) ||'-'||substr(strPermitNumber, 5,3) " & _
                "   ||'-'||substr(strPermitNumber, 8,4)||'-'||substr(strPermitNumber, 12, 1)  " & _
                "   ||'-'||substr(strPermitNumber, 13, 2) ||'-'||substr(strPermitNumber, 15,1)) as PermitNumber " & _
                "from AIRBRANCH.SSPPApplicationData  " & _
                "where strApplicationNumber like '" & MasterApp & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    PermitNumber = dr.Item("PermitNumber")
                Else
                    PermitNumber = Mid(FileName, 3)
                End If
                dr.Close()

                Select Case FileType
                    Case "10"
                        path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        path.FileName = PermitNumber
                        path.Filter = "Microsoft Office Work file (*.doc)|.doc"
                        'path.Filter = "docx files (*.docx)|*.docx"
                        path.FilterIndex = 1
                        path.DefaultExt = ".doc"
                        'path.DefaultExt = ".docx"

                        If path.ShowDialog = DialogResult.OK Then
                            DestFilePath = path.FileName.ToString
                        Else
                            DestFilePath = "N/A"
                        End If
                        If DestFilePath <> "N/A" Then
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            SQL = "select " & _
                            "DocPermitData " & _
                            "from AIRBRANCH.APBPermits " & _
                            "where strFileName = '" & FileName & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            dr = cmd.ExecuteReader

                            dr.Read()
                            Dim b(dr.GetBytes(0, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                            dr.GetBytes(0, 0, b, 0, b.Length)
                            dr.Close()

                            Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                            fs.Write(b, 0, b.Length)
                            fs.Close()
                        End If
                    Case "01"
                        path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        path.FileName = PermitNumber
                        path.Filter = "Adobe PDF Files (*.pdf)|.pdf"
                        path.FilterIndex = 1
                        path.DefaultExt = ".pdf"

                        If path.ShowDialog = DialogResult.OK Then
                            DestFilePath = path.FileName.ToString
                        Else
                            DestFilePath = "N/A"
                        End If

                        If DestFilePath <> "N/A" Then
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            SQL = "select " & _
                            "pdfPermitData " & _
                            "from AIRBRANCH.APBPermits " & _
                            "where strFileName = '" & FileName & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            dr = cmd.ExecuteReader

                            dr.Read()
                            Dim b(dr.GetBytes(0, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                            dr.GetBytes(0, 0, b, 0, b.Length)
                            dr.Close()

                            Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                            fs.Write(b, 0, b.Length)
                            fs.Close()
                        End If
                    Case "11"
                        path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        path.FileName = PermitNumber
                        path.Filter = "Microsoft Office Work file (*.doc)|.doc"
                        path.FilterIndex = 1
                        path.DefaultExt = ".doc"

                        If path.ShowDialog = DialogResult.OK Then
                            DestFilePath = path.FileName.ToString
                        Else
                            DestFilePath = "N/A"
                        End If
                        If DestFilePath <> "N/A" Then
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            SQL = "select " & _
                            "DocPermitData " & _
                            "from AIRBRANCH.APBPermits " & _
                            "where strFileName = '" & FileName & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            dr = cmd.ExecuteReader

                            dr.Read()
                            Dim b(dr.GetBytes(0, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                            dr.GetBytes(0, 0, b, 0, b.Length)
                            dr.Close()

                            Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                            fs.Write(b, 0, b.Length)
                            fs.Close()
                        End If
                        path.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        path.FileName = PermitNumber
                        path.Filter = "Adobe PDF Files (*.pdf)|.pdf"
                        path.FilterIndex = 1
                        path.DefaultExt = ".pdf"

                        If path.ShowDialog = DialogResult.OK Then
                            DestFilePath = path.FileName.ToString
                        Else
                            DestFilePath = "N/A"
                        End If

                        If DestFilePath <> "N/A" Then
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            SQL = "select " & _
                            "pdfPermitData " & _
                            "from AIRBRANCH.APBPermits " & _
                            "where strFileName = '" & FileName & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            dr = cmd.ExecuteReader

                            dr.Read()
                            Dim b(dr.GetBytes(0, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                            dr.GetBytes(0, 0, b, 0, b.Length)
                            dr.Close()

                            Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                            fs.Write(b, 0, b.Length)
                            fs.Close()
                        End If
                    Case Else
                End Select

                If DestFilePath <> "N/A" Then
                    Diagnostics.Process.Start(DestFilePath)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnOtherNarrativeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtherNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtOtherNarrativeDoc.Text = "On File" Or txtOtherNarrativePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtOtherNarrativeDoc.Text = "On File" And txtOtherNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("ON-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("ON-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("ON-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtOtherNarrativeDoc.Text = "On File" Then
                        DownloadFile("ON-" & MasterApp, "10")
                    End If
                    If txtOtherNarrativePDF.Text = "On File" Then
                        DownloadFile("ON-" & MasterApp, "01")
                    End If
                End If

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnOtherPermitDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtherPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtOtherPermitDoc.Text = "On File" Or txtOtherPermitPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtOtherPermitDoc.Text = "On File" And txtOtherPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("OP-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("OP-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("OP-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtOtherPermitDoc.Text = "On File" Then
                        DownloadFile("OP-" & MasterApp, "10")
                    End If
                    If txtOtherPermitPDF.Text = "On File" Then
                        DownloadFile("OP-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnTVNarrativeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVNarrativeDoc.Text = "On File" Or txtTVNarrativePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtTVNarrativeDoc.Text = "On File" And txtTVNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("VN-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("VN-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("VN-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtTVNarrativeDoc.Text = "On File" Then
                        DownloadFile("VN-" & MasterApp, "10")
                    End If
                    If txtTVNarrativePDF.Text = "On File" Then
                        DownloadFile("VN-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnTVDraftDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVDraftDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVDraftDoc.Text = "On File" Or txtTVDraftPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtTVDraftDoc.Text = "On File" And txtTVDraftPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("VD-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("VD-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("VD-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtTVDraftDoc.Text = "On File" Then
                        DownloadFile("VD-" & MasterApp, "10")
                    End If
                    If txtTVDraftPDF.Text = "On File" Then
                        DownloadFile("VD-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnTVPublicNoticeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVPublicNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVPublicNoticeDoc.Text = "On File" Or txtTVPublicNoticePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtTVPublicNoticeDoc.Text = "On File" And txtTVPublicNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("VP-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("VP-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("VP-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtTVPublicNoticeDoc.Text = "On File" Then
                        DownloadFile("VP-" & MasterApp, "10")
                    End If
                    If txtTVPublicNoticePDF.Text = "On File" Then
                        DownloadFile("VP-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnTVFinalDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVFinalDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVFinalDoc.Text = "On File" Or txtTVFinalPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtTVFinalDoc.Text = "On File" And txtTVFinalPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("VF-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("VF-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("VF-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtTVFinalDoc.Text = "On File" Then
                        DownloadFile("VF-" & MasterApp, "10")
                    End If
                    If txtTVFinalPDF.Text = "On File" Then
                        DownloadFile("VF-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDAppSummaryDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDAppSummaryDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDAppSummaryDoc.Text = "On File" Or txtPSDAppSummaryPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDAppSummaryDoc.Text = "On File" And txtPSDAppSummaryPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PA-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PA-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PA-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDAppSummaryDoc.Text = "On File" Then
                        DownloadFile("PA-" & MasterApp, "10")
                    End If
                    If txtPSDAppSummaryPDF.Text = "On File" Then
                        DownloadFile("PA-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDPrelimDetDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDPrelimDetDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDPrelimDetDoc.Text = "On File" Or txtPSDPrelimDetPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDPrelimDetDoc.Text = "On File" And txtPSDPrelimDetPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PP-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PP-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PP-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDPrelimDetDoc.Text = "On File" Then
                        DownloadFile("PP-" & MasterApp, "10")
                    End If
                    If txtPSDPrelimDetPDF.Text = "On File" Then
                        DownloadFile("PP-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDNarrativeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDNarrativeDoc.Text = "On File" Or txtPSDNarrativePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDNarrativeDoc.Text = "On File" And txtPSDNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PT-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PT-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PT-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDNarrativeDoc.Text = "On File" Then
                        DownloadFile("PT-" & MasterApp, "10")
                    End If
                    If txtPSDNarrativePDF.Text = "On File" Then
                        DownloadFile("PT-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDDraftPermitDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDDraftPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDDraftPermitDoc.Text = "On File" Or txtPSDDraftPermitPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDDraftPermitDoc.Text = "On File" And txtPSDDraftPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PD-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PD-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PD-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDDraftPermitDoc.Text = "On File" Then
                        DownloadFile("PD-" & MasterApp, "10")
                    End If
                    If txtPSDDraftPermitPDF.Text = "On File" Then
                        DownloadFile("PD-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDPublicNoticeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDPublicNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDPublicNoticeDoc.Text = "On File" Or txtPSDPublicNoticePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDPublicNoticeDoc.Text = "On File" And txtPSDPublicNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PN-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PN-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PN-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDPublicNoticeDoc.Text = "On File" Then
                        DownloadFile("PN-" & MasterApp, "10")
                    End If
                    If txtPSDPublicNoticePDF.Text = "On File" Then
                        DownloadFile("PN-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDHearingNoticeDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDHearingNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDHearingNoticeDoc.Text = "On File" Or txtPSDHearingNoticePDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDHearingNoticeDoc.Text = "On File" And txtPSDHearingNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PH-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PH-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PH-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDHearingNoticeDoc.Text = "On File" Then
                        DownloadFile("PH-" & MasterApp, "10")
                    End If
                    If txtPSDHearingNoticePDF.Text = "On File" Then
                        DownloadFile("PH-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDFinalDetDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDFinalDetDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDFinalDetDoc.Text = "On File" Or txtPSDFinalDetPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDFinalDetDoc.Text = "On File" And txtPSDFinalDetPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PF-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PF-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PF-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDFinalDetDoc.Text = "On File" Then
                        DownloadFile("PF-" & MasterApp, "10")
                    End If
                    If txtPSDFinalDetPDF.Text = "On File" Then
                        DownloadFile("PF-" & MasterApp, "01")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPSDFinalPermitDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPSDFinalPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDFinalPermitDoc.Text = "On File" Or txtPSDFinalPermitPDF.Text = "On File") And txtApplicationNumber.Text <> "" Then
                If txtPSDFinalPermitDoc.Text = "On File" And txtPSDFinalPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf & _
                    "If you want to download the pdf file type 'pdf'." & vbCrLf & _
                    "If you want to download both type 'Both'.", "Permit Downloader", "Cancel")
                    Select Case Result.ToUpper
                        Case "WORD"
                            DownloadFile("PI-" & MasterApp, "10")
                        Case "PDF"
                            DownloadFile("PI-" & MasterApp, "01")
                        Case "BOTH"
                            DownloadFile("PI-" & MasterApp, "11")
                        Case Else

                    End Select
                Else
                    If txtPSDFinalPermitDoc.Text = "On File" Then
                        DownloadFile("PI-" & MasterApp, "10")
                    End If
                    If txtPSDFinalPermitPDF.Text = "On File" Then
                        DownloadFile("PI-" & MasterApp, "01")
                    End If
                End If

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region

#Region "Delete buttons"
    Private Sub btnDeletePSDAppSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePSDAppSummary.Click
        Try
            DeleteFile("PA")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDPrelimDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePSDPrelimDet.Click
        Try
            DeleteFile("PP")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDNarrative_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePSDNarrative.Click
        Try
            DeleteFile("PT")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDDraftPermit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePSDDraftPermit.Click
        Try
            DeleteFile("PD")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDPublicNotice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePSDPublicNotice.Click
        Try
            DeleteFile("PN")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDHearingNotice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePSDHearingNotice.Click
        Try
            DeleteFile("PH")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDFinalDet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePSDFinalDet.Click
        Try
            DeleteFile("PF")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDFinalPermit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePSDFinalPermit.Click
        Try
            DeleteFile("PI")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTVNarrative_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTVNarrative.Click
        Try
            DeleteFile("VN")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTVDraft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTVDraft.Click
        Try
            DeleteFile("VD")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTVPublicNot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTVPublicNot.Click
        Try
            DeleteFile("VP")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTVFinal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTVFinal.Click
        Try
            DeleteFile("VF")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteOtherNarrative_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteOtherNarrative.Click
        Try
            DeleteFile("ON")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteOtherPermit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteOtherPermit.Click
        Try
            DeleteFile("OP")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

End Class