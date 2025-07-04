Imports Microsoft.Data.SqlClient
Imports System.IO

Public Class SSPPPermitUploader
    Private Property MasterApp As String

#Region "Form events"

    Private Sub IAIPPermitUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            TCPermitUploader.TabPages.Remove(TPTV)
            TCPermitUploader.TabPages.Remove(TPPSD)
            TCPermitUploader.TabPages.Remove(TPOther)

            DTPFinalOnWeb.Enabled = False
            DTPFinalOnWeb.Visible = False
            lblFinalOnWeb.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "General tools"

    Private Sub btnFindApplication_Click(sender As Object, e As EventArgs) Handles btnFindApplication.Click, btnClear.Click
        Try

            If txtApplicationNumber.Text <> "" Then
                FindApplicationInformation()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtApplicationNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtApplicationNumber.KeyPress
        Try

            If e.KeyChar = ChrW(13) AndAlso txtApplicationNumber.Text <> "" Then
                FindApplicationInformation()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FindApplicationInformation()
        Try
            Dim ZipCode As String = ""
            Dim PermitType As String = ""
            Dim AppType As String = ""
            Dim Status As Boolean = False
            Dim temp As String = ""
            Dim SQL As String

            txtApplicationInformation.Clear()
            txtApplicationLinks.Clear()
            btnUploadFile.Enabled = True

            SQL = "Select " &
            "strAIRSNumber " &
            "from SSPPApplicationMaster " &
            "where strApplicationNumber = @appnum "

            Dim p As New SqlParameter("@appnum", txtApplicationNumber.Text)

            If DB.ValueExists(SQL, p) Then
                SQL = "Select " &
                "SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5,8) as strAIRSnumber, " &
                "APBFacilityInformation.strFacilityName, APBFacilityInformation.strFacilityStreet1, " &
                "APBFacilityInformation.strFacilityCity, APBFacilityInformation.strFacilityZipCode, " &
                "datFinalizedDate, strCountyName, strApplicationTypeDesc, strPermitTypeDescription, " &
                "datPermitIssued, concat(strLastName,', ',strFirstName) as StaffResponsible, " &
                "datFinalOnWeb " &
                "from SSPPApplicationMaster " &
                "left join SSPPApplicationTracking " &
                "on SSPPApplicationMaster.strApplicationNumber  = SSPPApplicationTracking.strApplicationNumber " &
                "left join APBFacilityInformation " &
                "on SSPPApplicationMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber  " &
                "left join LookUpCountyInformation " &
                "on SUBSTRING(SSPPApplicationMaster.strAIRSnumber, 5, 3)  = LookUpCountyInformation.strCountyCode " &
                "left join LookUpApplicationTypes " &
                "on SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                "left join LookUpPermitTypes " &
                "on SSPPApplicationMaster.strPermitType = LookUpPermitTypes.strPermitTypeCode " &
                "left join EPDUserProfiles " &
                "on SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                "where ssppapplicationtracking.strApplicationNumber = @appnum "

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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
                    If Not IsDBNull(dr.Item("datFinalizedDate")) Then
                        Status = True
                    End If
                    If IsDBNull(dr.Item("datFinalOnWeb")) Then
                        DTPFinalOnWeb.Value = Today
                        DTPFinalOnWeb.Visible = False
                        lblFinalOnWeb.Visible = False
                    Else
                        DTPFinalOnWeb.Text = dr.Item("datFinalOnWeb")
                        lblFinalOnWeb.Visible = True
                        DTPFinalOnWeb.Visible = True
                    End If
                End If
            Else
                txtApplicationInformation.Text = "No Application Data available."
            End If

            SQL = "select strMasterApplication " &
            "from SSPPApplicationLinking " &
            "where strApplicationNumber = @appnum "

            If DB.ValueExists(SQL, p) Then
                MasterApp = DB.GetString(SQL, p)
            Else
                MasterApp = txtApplicationNumber.Text
            End If

            Dim p2 As New SqlParameter("@appnum", MasterApp)

            If MasterApp <> "" Then
                SQL = "Select strApplicationNumber " &
                "from SSPPApplicationLinking " &
                "where strMasterApplication = @appnum "

                Dim dt As DataTable = DB.GetDataTable(SQL, p2)

                For Each dr As DataRow In dt.Rows
                    txtApplicationLinks.Text = txtApplicationLinks.Text & dr.Item("strApplicationNumber") & vbCrLf
                Next
            Else
                txtApplicationLinks.Clear()
            End If

            rdbTitleVPermit.Checked = False
            rdbPSDPermit.Checked = False
            rdbOtherPermit.Checked = False

            SQL = "select " &
            "top (1) APBPermits.strFileName  " &
            "from APBpermits " &
            "left join SSPPApplicationLinking " &
            "on SUBSTRING(APBpermits.strFileName, 4,10) = SSPPAPPlicationLinking.strmasterapplication " &
            "where (SSPPApplicationLinking.strApplicationNumber = @appnum " &
            "or APBPermits.strFileName like @appnumlike) "

            Dim p3 As SqlParameter() = {
                p2,
                New SqlParameter("@appnumlike", "%-" & MasterApp)
            }

            Dim dr2 As DataRow = DB.GetDataRow(SQL, p3)

            If dr2 IsNot Nothing Then
                temp = Mid(dr2.Item("strFileName"), 1, 1)
                Select Case temp
                    Case "V"
                        rdbTitleVPermit.Checked = True
                    Case "P"
                        rdbPSDPermit.Checked = True
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

            If Status Then
                If CurrentUser.HasPermission(UserCan.UploadPermitFile) Then
                    btnUploadFile.Enabled = True
                Else
                    btnUploadFile.Enabled = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Save files"

    Private Sub btnUploadFile_Click(sender As Object, e As EventArgs) Handles btnUploadFile.Click
        Try
            SaveFiles()
            FindApplicationInformation()
            MsgBox("Done", MsgBoxStyle.Information, "Permit Uploader")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub UploadFile(FileName As String,
                           DocLocation As String,
                           DocxLocation As String,
                           PDFLocation As String,
                           DocOnFile As String)
        Try
            Dim Flag As String = "00"
            Dim DocFile As String = ""
            Dim ResultDoc As DialogResult
            Dim PDFFile As String = ""
            Dim ResultPDF As DialogResult
            Dim SQL As String

            SQL = "Select " &
            "strDOCFileSize, strPDFFileSize " &
            "From ApbPermits " &
            "where strFileName = @FileName "

            Dim p As New SqlParameter("@FileName", FileName)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)
            If dr IsNot Nothing Then
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

            If DocFile <> "" AndAlso (DocLocation <> "" OrElse DocxLocation <> "") Then
                Select Case Mid(FileName, 1, 2)
                    Case "VN"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Narrative." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "VD"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Draft Permit." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "VP"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Public Notice." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "VF"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Title V Final Permit." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PA"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Application Summary." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PP"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Preliminary Determination." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PT"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Narrative." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PD"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Draft Permit." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PN"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Public Notice." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PH"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Hearing Notice." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PF"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Final Determination." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "PI"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this PSD Final Permit." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "ON"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Other Narrative." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case "OP"
                        ResultDoc = MessageBox.Show("A Word file currently exists for this Other Permit." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    Case Else
                        ResultDoc = MessageBox.Show("A Word file currently exists for this 'Unknown' application." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                End Select
                Select Case ResultDoc
                    Case DialogResult.Yes
                        Flag = "10"
                    Case Else
                        Flag = "00"
                End Select
            Else
                If DocLocation <> "" OrElse DocxLocation <> "" Then
                    Flag = "10"
                Else
                    Flag = "00"
                End If
            End If
            If (PDFFile <> "" AndAlso Mid(Flag, 1, 1) = "1") OrElse DocOnFile = "On File" Then
                SQL = "update APBPermits set " &
                "PDFPermitData = null, " &
                "strPDFFileSize = null, " &
                "strPDFModifingPerson = null, " &
                "datPDFModifingDate = null " &
                "where strFileName = @FileName "

                DB.RunCommand(SQL, p)

            Else
                If PDFFile <> "" AndAlso PDFLocation <> "" Then
                    Select Case Mid(FileName, 1, 2)
                        Case "VN"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Narrative." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "VD"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Draft Permit." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "VP"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Public Notice." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "VF"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Title V Final Permit." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PA"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Application Summary." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PP"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Preliminary Determination." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PT"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Narrative." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PD"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Draft Permit." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PN"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Public Notice." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PH"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Hearing Notice." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PF"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Final Determination." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "PI"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this PSD Final Permit." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "ON"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Other Narrative." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case "OP"
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this Other Permit." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Case Else
                            ResultPDF = MessageBox.Show("A PDF file currently exists for this 'Unknown' application." & vbCrLf &
                            "Do you want to overwrite this file?", "Permit Uploader",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    End Select
                    Select Case ResultPDF
                        Case DialogResult.Yes
                            Flag = Mid(Flag, 1, 1) & "1"
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
                Dim exists As Boolean = True

                If Flag <> "00" Then
                    SQL = "Delete APBPermits " &
                    "where strFileName = @FileName "

                    DB.RunCommand(SQL, p)

                    SQL = "select " &
                    "[rowCount] " &
                    "from APBPermits " &
                    "where strFileName = @FileName "

                    Dim dr2 As DataRow = DB.GetDataRow(SQL, p)
                    If dr2 IsNot Nothing Then
                        If IsDBNull(dr2.Item("rowCount")) Then
                            rowCount = ""
                        Else
                            rowCount = dr2.Item("RowCount")
                        End If
                    Else
                        rowCount = ""
                    End If

                    If rowCount = "" Then
                        exists = False

                        SQL = "select " &
                        "(max([rowCount]) + 1) as [rowCount] " &
                        "from APBPermits "

                        Dim dr3 As DataRow = DB.GetDataRow(SQL, p)
                        If dr3 IsNot Nothing Then
                            If IsDBNull(dr3.Item("RowCount")) Then
                                rowCount = "1"
                            Else
                                rowCount = dr3.Item("RowCount")
                            End If
                        End If
                    End If

                    Dim fs As FileStream

                    Try
                        If DocLocation <> "" AndAlso Mid(Flag, 1, 1) = "1" Then
                            fs = New FileStream(DocLocation, FileMode.OpenOrCreate, FileAccess.Read)
                        ElseIf DocxLocation <> "" AndAlso Mid(Flag, 1, 1) = "1" Then
                            fs = New FileStream(DocxLocation, FileMode.OpenOrCreate, FileAccess.Read)
                        Else
                            fs = New FileStream(PDFLocation, FileMode.OpenOrCreate, FileAccess.Read)
                        End If
                    Catch ex As IOException
                        If ex.Message.Contains("it is being used by another process") Then
                            MessageBox.Show("The file is currently in use. Please close the file and try again.")
                            Return
                        Else
                            Throw
                        End If
                    End Try

                    Dim rawData As Byte() = New Byte(fs.Length) {}
                    If DocxLocation <> "" Then
                        ReDim rawData(fs.Length - 1)
                    End If

#Disable Warning CA2022 ' Avoid inexact read with 'Stream.Read'
                    fs.Read(rawData, 0, Convert.ToInt32(fs.Length))
#Enable Warning CA2022 ' Avoid inexact read with 'Stream.Read'
                    fs.Close()

                    If exists Then
                        If (DocLocation <> "" OrElse DocxLocation <> "") AndAlso Mid(Flag, 1, 1) = "1" Then
                            SQL = "UPDATE APBPermits set " &
                                "strFileName = @filename, " &
                                "docPermitData = @rawdata , " &
                                "strDocFileSize = @length , " &
                                "strDocModifingPerson = @user , " &
                                "datDocModifingDate = getdate() " &
                                "where [rowCount] = @rowCount "
                        Else
                            SQL = "UPDATE APBPermits set " &
                                "strFileName = @filename, " &
                                "pdfPermitData = @rawdata , " &
                                "strPDFFileSize = @length , " &
                                "strPDFModifingPerson = @user , " &
                                "datPDFModifingDate = getdate() " &
                                "where [rowCount] = @rowCount "
                        End If
                    Else
                        If (DocLocation <> "" OrElse DocxLocation <> "") AndAlso Mid(Flag, 1, 1) = "1" Then
                            SQL = "insert into APBPermits " &
                                "([rowCount], strFileName, docPermitData, strDocFileSize, strDocModifingPerson, datDocModifingDate) " &
                                "Values " &
                                "(@rowCount, @filename, @rawdata, @length, @user, getdate()) "
                        Else
                            SQL = "insert into APBPermits " &
                                "([rowCount], strFileName, pdfPermitData, strPDFFileSize, strPDFModifingPerson, datPDFModifingDate) " &
                                "Values " &
                                "(@rowCount, @filename, @rawdata, @length, @user, getdate()) "
                        End If
                    End If

                    Dim pf As SqlParameter() = {
                        New SqlParameter("@rowcount", rowCount),
                        New SqlParameter("@filename", FileName),
                        New SqlParameter("@rawdata", rawData),
                        New SqlParameter("@length", rawData.Length),
                        New SqlParameter("@user", CurrentUser.UserID)
                    }

                    DB.RunCommand(SQL, pf)
                End If

                If Mid(FileName, 1, 2) = "OP" Then
                    SQL = "Update SSPPApplicationTracking set " &
                    "datFinalOnWeb =  GETDATE()  " &
                    "where strApplicationNumber = @appnum "

                    DB.RunCommand(SQL, New SqlParameter("@appnum", MasterApp))
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveFiles()
        Try
            Dim doc As String = ""
            Dim docx As String = ""
            Dim pdf As String = ""
            Dim docOnFile As String = ""
            Dim SQL As String

            If rdbTitleVPermit.Checked Then
                If chbTVNarrative.Checked Then    'Prefix VN-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtTVNarrativeDoc.Text <> "" AndAlso txtTVNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtTVNarrativeDoc.Text, (txtTVNarrativeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtTVNarrativeDoc.Text
                        Else
                            If txtTVNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVNarrativeDoc.Text <> "" AndAlso txtTVNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtTVNarrativeDoc.Text, (txtTVNarrativeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtTVNarrativeDoc.Text
                        Else
                            If txtTVNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVNarrativePDF.Text <> "" AndAlso
                        txtTVNarrativePDF.Text <> "N/A" AndAlso
                        Mid(txtTVNarrativePDF.Text, (txtTVNarrativePDF.Text.Length - 3)).ToUpper = ".PDF" Then
                        pdf = txtTVNarrativePDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("VN-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbTVDraft.Checked Then         'Prefix VD-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtTVDraftDoc.Text <> "" AndAlso txtTVDraftDoc.Text <> "N/A" Then
                        If (Mid(txtTVDraftDoc.Text, (txtTVDraftDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtTVDraftDoc.Text
                        Else
                            If txtTVDraftDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVDraftDoc.Text <> "" AndAlso txtTVDraftDoc.Text <> "N/A" Then
                        If (Mid(txtTVDraftDoc.Text, (txtTVDraftDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtTVDraftDoc.Text
                        Else
                            If txtTVDraftDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVDraftPDF.Text <> "" AndAlso txtTVDraftPDF.Text <> "N/A" AndAlso
                        Mid(txtTVDraftPDF.Text, (txtTVDraftPDF.Text.Length - 3)).ToUpper = ".PDF" Then
                        pdf = txtTVDraftPDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("VD-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbTVPublicNotice.Checked Then   'Prefix VP-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtTVPublicNoticeDoc.Text <> "" AndAlso txtTVPublicNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtTVPublicNoticeDoc.Text, (txtTVPublicNoticeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtTVPublicNoticeDoc.Text
                        Else
                            If txtTVPublicNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVPublicNoticeDoc.Text <> "" AndAlso txtTVPublicNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtTVPublicNoticeDoc.Text, (txtTVPublicNoticeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtTVPublicNoticeDoc.Text
                        Else
                            If txtTVPublicNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVPublicNoticePDF.Text <> "" AndAlso txtTVPublicNoticePDF.Text <> "N/A" AndAlso
                        Mid(txtTVPublicNoticePDF.Text, (txtTVPublicNoticePDF.Text.Length - 3)).ToUpper = ".PDF" Then
                        pdf = txtTVPublicNoticePDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("VP-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbTVFinal.Checked Then          'Prefix VF-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtTVFinalDoc.Text <> "" AndAlso txtTVFinalDoc.Text <> "N/A" Then
                        If (Mid(txtTVFinalDoc.Text, (txtTVFinalDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtTVFinalDoc.Text
                        Else
                            If txtTVFinalDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVFinalDoc.Text <> "" AndAlso txtTVFinalDoc.Text <> "N/A" Then
                        If (Mid(txtTVFinalDoc.Text, (txtTVFinalDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtTVFinalDoc.Text
                        Else
                            If txtTVFinalDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtTVFinalPDF.Text <> "" AndAlso txtTVFinalPDF.Text <> "N/A" AndAlso
                        Mid(txtTVFinalPDF.Text, (txtTVFinalPDF.Text.Length - 3)).ToUpper = ".PDF" Then
                        pdf = txtTVFinalPDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("VF-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If

                    SQL = "Select datFinalOnWeb " &
                    "from SSPPApplicationTracking " &
                    "where strApplicationNumber = @appnum "

                    Dim p As New SqlParameter("@appnum", txtApplicationNumber.Text)

                    If DB.ValueExists(SQL, p) Then
                        SQL = "Update SSPPApplicationTracking set " &
                        "datFinalOnWeb =  GETDATE()  " &
                        "where strApplicationNumber = @appnum "
                        DB.RunCommand(SQL, p)
                    End If

                End If
            End If
            If rdbPSDPermit.Checked Then
                If chbPSDApplicationSummary.Checked Then 'Prefix PA-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDAppSummaryDoc.Text <> "" AndAlso txtPSDAppSummaryDoc.Text <> "N/A" Then
                        If (Mid(txtPSDAppSummaryDoc.Text, (txtPSDAppSummaryDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDAppSummaryDoc.Text
                        Else
                            If txtPSDAppSummaryDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDAppSummaryDoc.Text <> "" AndAlso txtPSDAppSummaryDoc.Text <> "N/A" Then
                        If (Mid(txtPSDAppSummaryDoc.Text, (txtPSDAppSummaryDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDAppSummaryDoc.Text
                        Else
                            If txtPSDAppSummaryDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDAppSummaryPDF.Text <> "" AndAlso txtPSDAppSummaryPDF.Text <> "N/A" AndAlso
                        Mid(txtPSDAppSummaryPDF.Text, (txtPSDAppSummaryPDF.Text.Length - 3)).ToUpper = ".PDF" Then
                        pdf = txtPSDAppSummaryPDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("PA-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDPrelimDet.Checked Then          'Prefix PP-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDPrelimDetDoc.Text <> "" AndAlso txtPSDPrelimDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDPrelimDetDoc.Text, (txtPSDPrelimDetDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDPrelimDetDoc.Text
                        Else
                            If txtPSDPrelimDetDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPrelimDetDoc.Text <> "" AndAlso txtPSDPrelimDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDPrelimDetDoc.Text, (txtPSDPrelimDetDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDPrelimDetDoc.Text
                        Else
                            If txtPSDPrelimDetDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPrelimDetPDF.Text <> "" AndAlso txtPSDPrelimDetPDF.Text <> "N/A" AndAlso
                        Mid(txtPSDPrelimDetPDF.Text, (txtPSDPrelimDetPDF.Text.Length - 3)).ToUpper = ".PDF" Then
                        pdf = txtPSDPrelimDetPDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("PP-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDNarrative.Checked Then          'Prefix PT-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDNarrativeDoc.Text <> "" AndAlso txtPSDNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDNarrativeDoc.Text, (txtPSDNarrativeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDNarrativeDoc.Text
                        Else
                            If txtPSDNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDNarrativeDoc.Text <> "" AndAlso txtPSDNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDNarrativeDoc.Text, (txtPSDNarrativeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDNarrativeDoc.Text
                        Else
                            If txtPSDNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDNarrativePDF.Text <> "" AndAlso txtPSDNarrativePDF.Text <> "N/A" AndAlso
                        Mid(txtPSDNarrativePDF.Text, (txtPSDNarrativePDF.Text.Length - 3)).ToUpper = ".PDF" Then
                        pdf = txtPSDNarrativePDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("PT-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDDraftPermit.Checked Then        'Prefix PD-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDPrelimDetDoc.Text <> "" AndAlso txtPSDPrelimDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDDraftPermitDoc.Text, (txtPSDDraftPermitDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDDraftPermitDoc.Text
                        Else
                            If txtPSDDraftPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPrelimDetDoc.Text <> "" AndAlso txtPSDPrelimDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDDraftPermitDoc.Text, (txtPSDDraftPermitDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDDraftPermitDoc.Text
                        Else
                            If txtPSDDraftPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDDraftPermitPDF.Text <> "" AndAlso txtPSDDraftPermitPDF.Text <> "N/A" AndAlso
                        Mid(txtPSDDraftPermitPDF.Text, (txtPSDDraftPermitPDF.Text.Length - 3)).ToUpper = ".PDF" Then
                        pdf = txtPSDDraftPermitPDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("PD-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDPublicNotice.Checked Then       'Prefix PN-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDPublicNoticeDoc.Text <> "" AndAlso txtPSDPublicNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDPublicNoticeDoc.Text, (txtPSDPublicNoticeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDPublicNoticeDoc.Text
                        Else
                            If txtPSDPublicNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPublicNoticeDoc.Text <> "" AndAlso txtPSDPublicNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDPublicNoticeDoc.Text, (txtPSDPublicNoticeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDPublicNoticeDoc.Text
                        Else
                            If txtPSDPublicNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDPublicNoticePDF.Text <> "" AndAlso txtPSDPublicNoticePDF.Text <> "N/A" AndAlso
                        (Mid(txtPSDPublicNoticePDF.Text, (txtPSDPublicNoticePDF.Text.Length - 3))).ToUpper = ".PDF" Then
                        pdf = txtPSDPublicNoticePDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("PN-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDHearingNotice.Checked Then      'Prefix PH-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDHearingNoticeDoc.Text <> "" AndAlso txtPSDHearingNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDHearingNoticeDoc.Text, (txtPSDHearingNoticeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDHearingNoticeDoc.Text
                        Else
                            If txtPSDHearingNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDHearingNoticeDoc.Text <> "" AndAlso txtPSDHearingNoticeDoc.Text <> "N/A" Then
                        If (Mid(txtPSDHearingNoticeDoc.Text, (txtPSDHearingNoticeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDHearingNoticeDoc.Text
                        Else
                            If txtPSDHearingNoticeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDHearingNoticePDF.Text <> "" AndAlso txtPSDHearingNoticePDF.Text <> "N/A" AndAlso
                        (Mid(txtPSDHearingNoticePDF.Text, (txtPSDHearingNoticePDF.Text.Length - 3))).ToUpper = ".PDF" Then
                        pdf = txtPSDHearingNoticePDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("PH-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDFinalDet.Checked Then           'Prefix PF- 
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDFinalDetDoc.Text <> "" AndAlso txtPSDFinalDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDFinalDetDoc.Text, (txtPSDFinalDetDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDFinalDetDoc.Text
                        Else
                            If txtPSDFinalDetDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDFinalDetDoc.Text <> "" AndAlso txtPSDFinalDetDoc.Text <> "N/A" Then
                        If (Mid(txtPSDFinalDetDoc.Text, (txtPSDFinalDetDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDFinalDetDoc.Text
                        Else
                            If txtPSDFinalDetDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDFinalDetPDF.Text <> "" AndAlso txtPSDFinalDetPDF.Text <> "N/A" AndAlso
                        (Mid(txtPSDFinalDetPDF.Text, (txtPSDFinalDetPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                        pdf = txtPSDFinalDetPDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("PF-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbPSDFinalPermit.Checked Then        'Prefix PI-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtPSDFinalPermitDoc.Text <> "" AndAlso txtPSDFinalPermitDoc.Text <> "N/A" Then
                        If (Mid(txtPSDFinalPermitDoc.Text, (txtPSDFinalPermitDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtPSDFinalPermitDoc.Text
                        Else
                            If txtPSDFinalPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDFinalPermitDoc.Text <> "" AndAlso txtPSDFinalPermitDoc.Text <> "N/A" Then
                        If (Mid(txtPSDFinalPermitDoc.Text, (txtPSDFinalPermitDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtPSDFinalPermitDoc.Text
                        Else
                            If txtPSDFinalPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtPSDFinalPermitPDF.Text <> "" AndAlso txtPSDFinalPermitPDF.Text <> "N/A" AndAlso
                        (Mid(txtPSDFinalPermitPDF.Text, (txtPSDFinalPermitPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                        pdf = txtPSDFinalPermitPDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("PI-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If

                    SQL = "Select datFinalOnWeb " &
                    "from SSPPApplicationTracking " &
                    "where strApplicationNumber = @appnum "

                    Dim p As New SqlParameter("@appnum", txtApplicationNumber.Text)

                    If DB.ValueExists(SQL, p) Then
                        SQL = "Update SSPPApplicationTracking set " &
                        "datFinalOnWeb =  GETDATE()  " &
                        "where strApplicationNumber = @appnum "
                        DB.RunCommand(SQL, p)
                    End If

                End If
            End If
            If rdbOtherPermit.Checked Then
                If chbOtherNarrative.Checked Then       'Prefix ON-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtOtherNarrativeDoc.Text <> "" AndAlso txtOtherNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtOtherNarrativeDoc.Text, (txtOtherNarrativeDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtOtherNarrativeDoc.Text
                        Else
                            If txtOtherNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtOtherNarrativeDoc.Text <> "" AndAlso txtOtherNarrativeDoc.Text <> "N/A" Then
                        If (Mid(txtOtherNarrativeDoc.Text, (txtOtherNarrativeDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtOtherNarrativeDoc.Text
                        Else
                            If txtOtherNarrativeDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtOtherNarrativePDF.Text <> "" AndAlso txtOtherNarrativePDF.Text <> "N/A" AndAlso
                        (Mid(txtOtherNarrativePDF.Text, (txtOtherNarrativePDF.Text.Length - 3))).ToUpper = ".PDF" Then
                        pdf = txtOtherNarrativePDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("ON-" & MasterApp, doc, docx, pdf, docOnFile)
                    End If
                End If
                If chbOtherPermit.Checked Then             'Prefix OP-
                    doc = ""
                    pdf = ""
                    docOnFile = ""
                    If txtOtherPermitDoc.Text <> "" AndAlso txtOtherPermitDoc.Text <> "N/A" Then
                        If (Mid(txtOtherPermitDoc.Text, (txtOtherPermitDoc.Text.Length - 3))).ToUpper = ".DOC" Then
                            doc = txtOtherPermitDoc.Text
                        Else
                            If txtOtherPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtOtherPermitDoc.Text <> "" AndAlso txtOtherPermitDoc.Text <> "N/A" Then
                        If (Mid(txtOtherPermitDoc.Text, (txtOtherPermitDoc.Text.Length - 4))).ToUpper = ".DOCX" Then
                            docx = txtOtherPermitDoc.Text
                        Else
                            If txtOtherPermitDoc.Text = "On File" Then
                                docOnFile = "On File"
                            End If
                        End If
                    End If
                    If txtOtherPermitPDF.Text <> "" AndAlso txtOtherPermitPDF.Text <> "N/A" AndAlso
                        (Mid(txtOtherPermitPDF.Text, (txtOtherPermitPDF.Text.Length - 3))).ToUpper = ".PDF" Then
                        pdf = txtOtherPermitPDF.Text
                    End If
                    If doc <> "" OrElse docx <> "" OrElse pdf <> "" Then
                        UploadFile("OP-" & MasterApp, doc, docx, pdf, docOnFile)

                        SQL = "Select datFinalOnWeb " &
                            "from SSPPApplicationTracking " &
                            "where strApplicationNumber = @appnum "

                        Dim p As New SqlParameter("@appnum", txtApplicationNumber.Text)

                        If DB.ValueExists(SQL, p) Then
                            SQL = "Update SSPPApplicationTracking set " &
                                "datFinalOnWeb =  GETDATE()  " &
                                "where strApplicationNumber = @appnum "
                            DB.RunCommand(SQL, p)
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Delete files"

    Sub DeleteFile(FileType As String)
        Try
            Dim ResultDoc As DialogResult
            Dim SQL As String

            SQL = "Select " &
            "strFileName " &
            "from APBPermits " &
            "where strFileName = @filename "

            Dim p As New SqlParameter("@filename", FileType & "-" & txtApplicationNumber.Text)

            If DB.ValueExists(SQL, p) Then
                ResultDoc = MessageBox.Show("Are you positive you want to delete this file?" & vbCrLf &
                        "It will not be recoverable if you delete it.", "Permit Delete",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If ResultDoc = DialogResult.Yes Then
                    SQL = "Delete APBPermits " &
                    "where strFileName = @filename "

                    DB.RunCommand(SQL, p)

                    FindApplicationInformation()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Permit type radio buttons"

    Sub DisplayPermitPanel()
        Try
            If TCPermitUploader.TabPages.Contains(TPTV) Then
                TCPermitUploader.TabPages.Remove(TPTV)
            End If
            If TCPermitUploader.TabPages.Contains(TPPSD) Then
                TCPermitUploader.TabPages.Remove(TPPSD)
            End If
            If TCPermitUploader.TabPages.Contains(TPOther) Then
                TCPermitUploader.TabPages.Remove(TPOther)
            End If
            If rdbTitleVPermit.Checked Then
                TCPermitUploader.TabPages.Add(TPTV)
            ElseIf rdbPSDPermit.Checked Then
                TCPermitUploader.TabPages.Add(TPPSD)
            ElseIf rdbOtherPermit.Checked Then
                TCPermitUploader.TabPages.Add(TPOther)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbTitleVPermit_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTitleVPermit.CheckedChanged
        Try
            Dim TVNarrative As String = ""
            Dim TVDraft As String = ""
            Dim TVNotice As String = ""
            Dim TVFinal As String = ""
            Dim SQL As String

            chbTVNarrative.Checked = False
            chbTVDraft.Checked = False
            chbTVPublicNotice.Checked = False
            chbTVFinal.Checked = False

            DisplayPermitPanel()

            If rdbTitleVPermit.Checked AndAlso MasterApp <> "" Then
                SQL = "select " &
                "strFileName " &
                "from APBPermits " &
                "where strFileName like @filename "

                Dim p As New SqlParameter("@filename", "V_-" & MasterApp)

                Dim dt As DataTable = DB.GetDataTable(SQL, p)

                For Each dr As DataRow In dt.Rows
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
                Next

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbPSDPermit_CheckedChanged(sender As Object, e As EventArgs) Handles rdbPSDPermit.CheckedChanged
        Try
            Dim PSDAppSummary As String = ""
            Dim PSDPrelimDet As String = ""
            Dim PSDNarrative As String = ""
            Dim PSDDraft As String = ""
            Dim PSDNotice As String = ""
            Dim PSDHearing As String = ""
            Dim PSDFinal As String = ""
            Dim PSDPermit As String = ""
            Dim SQL As String

            chbPSDApplicationSummary.Checked = False
            chbPSDPrelimDet.Checked = False
            chbPSDNarrative.Checked = False
            chbPSDDraftPermit.Checked = False
            chbPSDPublicNotice.Checked = False
            chbPSDHearingNotice.Checked = False
            chbPSDFinalDet.Checked = False
            chbPSDFinalPermit.Checked = False

            DisplayPermitPanel()

            If rdbPSDPermit.Checked AndAlso MasterApp <> "" Then
                SQL = "select " &
                "strFileName " &
                "from APBPermits " &
                "where strFileName like @filename "

                Dim p As New SqlParameter("@filename", "P_-" & MasterApp)

                Dim dt As DataTable = DB.GetDataTable(SQL, p)

                For Each dr As DataRow In dt.Rows
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
                Next

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbOtherPermit_CheckedChanged(sender As Object, e As EventArgs) Handles rdbOtherPermit.CheckedChanged
        Try
            Dim OtherNarrative As String = ""
            Dim OtherPermit As String = ""
            Dim SQL As String

            chbOtherNarrative.Checked = False
            chbOtherPermit.Checked = False

            DisplayPermitPanel()

            If rdbOtherPermit.Checked AndAlso MasterApp <> "" Then
                SQL = "select " &
                "strFileName " &
                "from APBPermits " &
                "where strFileName like @filename "

                Dim p As New SqlParameter("@filename", "O_-" & MasterApp)

                Dim dt As DataTable = DB.GetDataTable(SQL, p)

                For Each dr As DataRow In dt.Rows
                    Select Case Mid(dr.Item("strFileName"), 1, 2)
                        Case "ON"
                            OtherNarrative = "True"
                        Case "OP"
                            OtherPermit = "True"
                    End Select
                Next

                If OtherNarrative = "True" Then
                    chbOtherNarrative.Checked = True
                End If
                If OtherPermit = "True" Then
                    chbOtherPermit.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Document checkboxes"

    Private Sub chbTVNarrative_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVNarrative.CheckedChanged
        Try
            Dim SQL As String

            If chbTVNarrative.Checked AndAlso MasterApp <> "" Then

                txtTVNarrativeDoc.Visible = True
                txtTVNarrativePDF.Visible = True
                btnTVNarrative.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                "case " &
                "when docPermitData is Null then null " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then null " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @fn ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then  null  " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then null " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then  null  " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUSerProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUSerProfiles.numUserID  " &
                "and numUserID = strPDFModifingPerson " &
                "and strFileName = @fn ) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then  null  " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @fn "

                Dim p As New SqlParameter("@fn", "VN-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtTVNarrativeDoc.Text = "On File" OrElse txtTVNarrativePDF.Text = "On File" Then
                    btnTVNarrativeDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbTVDraft_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVDraft.CheckedChanged
        Try
            Dim SQL As String

            If chbTVDraft.Checked AndAlso MasterApp <> "" Then
                txtTVDraftDoc.Visible = True
                txtTVDraftPDF.Visible = True
                btnTVDraft.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                "case " &
                "when docPermitData is Null then  null  " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then  null  " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUSerProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUSerProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @fn ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then  null  " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then  null  " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then  null  " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                "and numUserID = strPDFModifingPerson " &
                "and strFileName = @fn ) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then  null  " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "VD-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtTVDraftDoc.Text = "On File" OrElse txtTVDraftPDF.Text = "On File" Then
                    btnTVDraftDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbTVPublicNotice_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVPublicNotice.CheckedChanged
        Try
            Dim SQL As String

            If chbTVPublicNotice.Checked AndAlso MasterApp <> "" Then
                txtTVPublicNoticeDoc.Visible = True
                txtTVPublicNoticePDF.Visible = True
                btnTVPublicNotice.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                "case " &
                "when docPermitData is Null then  null  " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then  null  " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @fn ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then  null  " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then  null  " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then  null  " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                "and numUserID = strPDFModifingPerson " &
                "and strFileName = @fn ) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then  null  " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "VP-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtTVPublicNoticeDoc.Text = "On File" OrElse txtTVPublicNoticePDF.Text = "On File" Then
                    btnTVPublicNoticeDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbTVFinal_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVFinal.CheckedChanged
        Try
            Dim SQL As String

            If chbTVFinal.Checked AndAlso MasterApp <> "" Then
                txtTVFinalDoc.Visible = True
                txtTVFinalPDF.Visible = True
                btnTVFinal.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                 "case " &
                 "when docPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then  null  " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then  null  " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "VF-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtTVFinalDoc.Text = "On File" OrElse txtTVFinalPDF.Text = "On File" Then
                    btnTVFinalDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbPSDApplicationSummary_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDApplicationSummary.CheckedChanged
        Try
            Dim SQL As String

            If chbPSDApplicationSummary.Checked AndAlso MasterApp <> "" Then
                txtPSDAppSummaryDoc.Visible = True

                txtPSDAppSummaryPDF.Visible = True
                btnPSDAppSummary.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                 "case " &
                 "when docPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then  null  " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then  null  " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "PA-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtPSDAppSummaryDoc.Text = "On File" OrElse txtPSDAppSummaryPDF.Text = "On File" Then
                    btnPSDAppSummaryDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbPSDPrelimDet_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDPrelimDet.CheckedChanged
        Try
            Dim SQL As String

            If chbPSDPrelimDet.Checked AndAlso MasterApp <> "" Then
                txtPSDPrelimDetDoc.Visible = True
                txtPSDPrelimDetPDF.Visible = True
                btnPSDPrelimDet.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True


                'btnPSDPrelimDet.Location = New System.Drawing.Point(119, 15)
                'txtPSDPrelimDetDoc.Location = New System.Drawing.Point(149, 16)
                'txtPSDPrelimDetPDF.Location = New System.Drawing.Point(396, 16)
                'btnPSDPrelimDetDownload.Location = New System.Drawing.Point(643, 15)
                'lblPSDPrelimDetSRDoc.Location = New System.Drawing.Point(155, 40)
                'lblPSDPrelimDetDUDoc.Location = New System.Drawing.Point(155, 58)
                'lblPSDPrelimDetSRPDF.Location = New System.Drawing.Point(405, 40)
                'lblPSDPrelimDetDUPDF.Location = New System.Drawing.Point(405, 58)
                'btnDeletePSDPrelimDet.Location = New System.Drawing.Point(643, 38)


                SQL = "select " &
                 "case " &
                 "when docPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then  null  " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then  null  " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "PP-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtPSDPrelimDetDoc.Text = "On File" OrElse txtPSDPrelimDetPDF.Text = "On File" Then
                    btnPSDPrelimDetDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbPSDNarrative_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDNarrative.CheckedChanged
        Try
            Dim SQL As String

            If chbPSDNarrative.Checked AndAlso MasterApp <> "" Then
                txtPSDNarrativeDoc.Visible = True
                txtPSDNarrativePDF.Visible = True
                btnPSDNarrative.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                 "case " &
                 "when docPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then  null  " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then  null  " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "PT-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtPSDNarrativeDoc.Text = "On File" OrElse txtPSDNarrativePDF.Text = "On File" Then
                    btnPSDNarrativeDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbPSDDraftPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDDraftPermit.CheckedChanged
        Try
            Dim SQL As String

            If chbPSDDraftPermit.Checked AndAlso MasterApp <> "" Then
                txtPSDDraftPermitDoc.Visible = True
                txtPSDDraftPermitPDF.Visible = True
                btnPSDDraftPermit.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                 "case " &
                 "when docPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then  null  " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then  null  " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "PD-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtPSDDraftPermitDoc.Text = "On File" OrElse txtPSDDraftPermitPDF.Text = "On File" Then
                    btnPSDDraftPermitDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbPSDPublicNotice_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDPublicNotice.CheckedChanged
        Try
            Dim SQL As String

            If chbPSDPublicNotice.Checked AndAlso MasterApp <> "" Then
                txtPSDPublicNoticeDoc.Visible = True
                txtPSDPublicNoticePDF.Visible = True
                btnPSDPublicNotice.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                "case " &
                "when docPermitData is Null then  null  " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then  null  " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @fn ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then  null  " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then  null  " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then  null  " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                "and numUserID = strPDFModifingPerson " &
                "and strFileName = @fn ) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then  null  " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "PN-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtPSDPublicNoticeDoc.Text = "On File" OrElse txtPSDPublicNoticePDF.Text = "On File" Then
                    btnPSDPublicNoticeDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbPSDHearingNotice_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDHearingNotice.CheckedChanged
        Try
            Dim SQL As String

            If chbPSDHearingNotice.Checked AndAlso MasterApp <> "" Then
                txtPSDHearingNoticeDoc.Visible = True
                txtPSDHearingNoticePDF.Visible = True
                btnPSDHearingNotice.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                 "case " &
                 "when docPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then  null  " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then  null  " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "PH-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtPSDHearingNoticeDoc.Text = "On File" OrElse txtPSDHearingNoticePDF.Text = "On File" Then
                    btnPSDHearingNoticeDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbPSDFinalDet_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDFinalDet.CheckedChanged
        Try
            Dim SQL As String

            If chbPSDFinalDet.Checked AndAlso MasterApp <> "" Then
                txtPSDFinalDetDoc.Visible = True
                txtPSDFinalDetPDF.Visible = True
                btnPSDFinalDet.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                 "case " &
                 "when docPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then  null  " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then  null  " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "PF-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtPSDFinalDetDoc.Text = "On File" OrElse txtPSDFinalDetPDF.Text = "On File" Then
                    btnPSDFinalDetDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbPSDFinalPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDFinalPermit.CheckedChanged
        Try
            Dim SQL As String

            If chbPSDFinalPermit.Checked AndAlso MasterApp <> "" Then
                txtPSDFinalPermitDoc.Visible = True
                txtPSDFinalPermitPDF.Visible = True
                btnPSDFinalPermit.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                "case " &
                "when docPermitData is Null then  null  " &
                "Else 'True' " &
                "End DocData, " &
                "case " &
                "when strDocModifingPerson is Null then  null  " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                "and numUserID = strDocModifingPerson " &
                "and strFileName = @fn ) " &
                "end DocStaffResponsible, " &
                "case " &
                "when datDocModifingDate is Null then  null  " &
                "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                "End datDocModifingDate, " &
                "case " &
                "when pdfPermitData is Null then  null  " &
                "Else 'True' " &
                "End PDFData, " &
                "case " &
                "when strPDFModifingPerson is Null then  null  " &
                "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                "from APBPermits, EPDUserProfiles " &
                "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                "and numUserID = strPDFModifingPerson " &
                "and strFileName = @fn ) " &
                "end PDFStaffResponsible, " &
                "case " &
                "when datPDFModifingDate is Null then  null  " &
                "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                "End datPDFModifingDate " &
                "from APBPermits " &
                "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "PI-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtPSDFinalPermitDoc.Text = "On File" OrElse txtPSDFinalPermitPDF.Text = "On File" Then
                    btnPSDFinalPermitDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbOtherNarrative_CheckedChanged(sender As Object, e As EventArgs) Handles chbOtherNarrative.CheckedChanged
        Try
            Dim SQL As String

            If chbOtherNarrative.Checked AndAlso MasterApp <> "" Then
                txtOtherNarrativeDoc.Visible = True
                txtOtherNarrativePDF.Visible = True
                btnOtherNarrative.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                 "case " &
                 "when docPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End DocData, " &
                 "case " &
                 "when strDocModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                 "and numUserID = strDocModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end DocStaffResponsible, " &
                 "case " &
                 "when datDocModifingDate is Null then  null  " &
                 "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                 "End datDocModifingDate, " &
                 "case " &
                 "when pdfPermitData is Null then  null  " &
                 "Else 'True' " &
                 "End PDFData, " &
                 "case " &
                 "when strPDFModifingPerson is Null then  null  " &
                 "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                 "from APBPermits, EPDUserProfiles " &
                 "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                 "and numUserID = strPDFModifingPerson " &
                 "and strFileName = @fn ) " &
                 "end PDFStaffResponsible, " &
                 "case " &
                 "when datPDFModifingDate is Null then  null  " &
                 "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                 "End datPDFModifingDate " &
                 "from APBPermits " &
                 "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "ON-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtOtherNarrativeDoc.Text = "On File" OrElse txtOtherNarrativePDF.Text = "On File" Then
                    btnOtherNarrativeDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbOtherPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbOtherPermit.CheckedChanged
        Try
            Dim SQL As String

            If chbOtherPermit.Checked AndAlso MasterApp <> "" Then
                txtOtherPermitDoc.Visible = True
                txtOtherPermitPDF.Visible = True
                btnOtherPermit.Visible = True
                lblWord.Visible = True
                lblPDF.Visible = True

                SQL = "select " &
                  "case " &
                  "when docPermitData is Null then  null  " &
                  "Else 'True' " &
                  "End DocData, " &
                  "case " &
                  "when strDocModifingPerson is Null then  null  " &
                  "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                  "from APBPermits, EPDUserProfiles " &
                  "where APBPermits.strDocModifingPerson = EPDUserProfiles.numUserID " &
                  "and numUserID = strDocModifingPerson " &
                  "and strFileName = @fn ) " &
                  "end DocStaffResponsible, " &
                  "case " &
                  "when datDocModifingDate is Null then  null  " &
                  "else format(datDocModifingDate, 'dd-MMM-yyyy') " &
                  "End datDocModifingDate, " &
                  "case " &
                  "when pdfPermitData is Null then  null  " &
                  "Else 'True' " &
                  "End PDFData, " &
                  "case " &
                  "when strPDFModifingPerson is Null then  null  " &
                  "else (select concat(strLastName,', ',strFirstName) as StaffName " &
                  "from APBPermits, EPDUserProfiles " &
                  "where APBPermits.strPDFModifingPerson = EPDUserProfiles.numUserID  " &
                  "and numUserID = strPDFModifingPerson " &
                  "and strFileName = @fn ) " &
                  "end PDFStaffResponsible, " &
                  "case " &
                  "when datPDFModifingDate is Null then  null  " &
                  "else format(datPDFModifingdate, 'dd-MMM-yyyy') " &
                  "End datPDFModifingDate " &
                  "from APBPermits " &
                  "where strFileName = @fn  "

                Dim p As New SqlParameter("@fn", "OP-" & MasterApp)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
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

                If txtOtherPermitDoc.Text = "On File" OrElse txtOtherPermitPDF.Text = "On File" Then
                    btnOtherPermitDownload.Visible = True
                    If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Document upload buttons"

    Private Sub UploadButtons_Click(sender As Object, e As EventArgs) Handles btnOtherNarrative.Click, btnOtherPermit.Click,
        btnPSDAppSummary.Click, btnPSDDraftPermit.Click, btnPSDFinalDet.Click,
        btnPSDFinalPermit.Click, btnPSDHearingNotice.Click, btnPSDNarrative.Click,
        btnPSDPrelimDet.Click, btnPSDPublicNotice.Click,
        btnTVDraft.Click, btnTVFinal.Click, btnTVNarrative.Click, btnTVPublicNotice.Click

        Using dialog As New OpenFileDialog
            Try
                dialog.InitialDirectory = GetUserSetting(UserSetting.FileUploadLocation)
                dialog.Filter = "Word files (*.docx, *.doc)|*.docx;*.doc|PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
                dialog.FilterIndex = 3

                If dialog.ShowDialog = DialogResult.OK Then
                    If File.Exists(dialog.FileName) Then

                        If Path.GetDirectoryName(dialog.FileName) <> dialog.InitialDirectory Then
                            SaveUserSetting(UserSetting.FileUploadLocation, Path.GetDirectoryName(dialog.FileName))
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
                            MessageBox.Show("There was an error selecting the file. Please contact EPD IT.")
                        End Try

                    Else
                        MessageBox.Show("Could not read file. Please try again.")
                    End If
                End If

            Catch ex As Exception
                ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            End Try
        End Using

    End Sub

#End Region

#Region "Download files"

    Private Sub DownloadFile(FileName As String, FileType As String)
        Try
            Dim PermitNumber As String = ""

            Using path As New SaveFileDialog
                Dim DestFilePath As String = "N/A"
                Dim SQL As String

                If FileType <> "00" Then
                    SQL = "select " &
                    "strPermitNumber,  " &
                    "concat(SUBSTRING(strPermitNumber,1, 4) ,'-',SUBSTRING(strPermitNumber, 5,3) " &
                    "   ,'-',SUBSTRING(strPermitNumber, 8,4),'-',SUBSTRING(strPermitNumber, 12, 1)  " &
                    "   ,'-',SUBSTRING(strPermitNumber, 13, 2) ,'-',SUBSTRING(strPermitNumber, 15,1)) as PermitNumber " &
                    "from SSPPApplicationData  " &
                    "where strApplicationNumber = @appnum "

                    Dim p As New SqlParameter("@appnum", MasterApp)

                    Dim dr As DataRow = DB.GetDataRow(SQL, p)

                    If dr IsNot Nothing Then
                        PermitNumber = dr.Item("PermitNumber")
                    Else
                        PermitNumber = Mid(FileName, 3)
                    End If

                    Select Case FileType
                        Case "10"
                            path.InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
                            path.FileName = PermitNumber
                            path.Filter = "Microsoft Office Work file (*.doc)|.doc"
                            path.FilterIndex = 1
                            path.DefaultExt = ".doc"

                            If path.ShowDialog() = DialogResult.OK Then
                                DestFilePath = path.FileName.ToString

                                If IO.Path.GetDirectoryName(path.FileName) <> path.InitialDirectory Then
                                    SaveUserSetting(UserSetting.FileDownloadLocation, IO.Path.GetDirectoryName(path.FileName))
                                End If

                                SQL = "select " &
                                    "DocPermitData " &
                                    "from APBPermits " &
                                    "where strFileName = @filename "

                                Dim p2 As New SqlParameter("@filename", FileName)

                                SaveBinaryFileFromDB(DestFilePath, SQL, p2)
                            End If

                        Case "01"
                            path.InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
                            path.FileName = PermitNumber
                            path.Filter = "Adobe PDF Files (*.pdf)|.pdf"
                            path.FilterIndex = 1
                            path.DefaultExt = ".pdf"

                            If path.ShowDialog() = DialogResult.OK Then
                                DestFilePath = path.FileName.ToString

                                If IO.Path.GetDirectoryName(path.FileName) <> path.InitialDirectory Then
                                    SaveUserSetting(UserSetting.FileDownloadLocation, IO.Path.GetDirectoryName(path.FileName))
                                End If

                                SQL = "select " &
                                    "pdfPermitData " &
                                    "from APBPermits " &
                                    "where strFileName = @filename "

                                Dim p2 As New SqlParameter("@filename", FileName)

                                SaveBinaryFileFromDB(DestFilePath, SQL, p2)
                            End If

                        Case "11"
                            path.InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
                            path.FileName = PermitNumber
                            path.Filter = "Microsoft Office Work file (*.doc)|.doc"
                            path.FilterIndex = 1
                            path.DefaultExt = ".doc"

                            If path.ShowDialog() = DialogResult.OK Then
                                DestFilePath = path.FileName.ToString

                                If IO.Path.GetDirectoryName(path.FileName) <> path.InitialDirectory Then
                                    SaveUserSetting(UserSetting.FileDownloadLocation, IO.Path.GetDirectoryName(path.FileName))
                                End If

                                SQL = "select " &
                                    "DocPermitData " &
                                    "from APBPermits " &
                                    "where strFileName = @filename "

                                Dim p2 As New SqlParameter("@filename", FileName)

                                SaveBinaryFileFromDB(DestFilePath, SQL, p2)
                            End If

                            path.InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
                            path.FileName = PermitNumber
                            path.Filter = "Adobe PDF Files (*.pdf)|.pdf"
                            path.FilterIndex = 1
                            path.DefaultExt = ".pdf"

                            If path.ShowDialog() = DialogResult.OK Then
                                DestFilePath = path.FileName.ToString

                                If IO.Path.GetDirectoryName(path.FileName) <> path.InitialDirectory Then
                                    SaveUserSetting(UserSetting.FileDownloadLocation, IO.Path.GetDirectoryName(path.FileName))
                                End If

                                SQL = "select " &
                                    "pdfPermitData " &
                                    "from APBPermits " &
                                    "where strFileName = @filename "

                                Dim p2 As New SqlParameter("@filename", FileName)

                                SaveBinaryFileFromDB(DestFilePath, SQL, p2)
                            End If
                        Case Else
                    End Select

                    If DestFilePath <> "N/A" Then
                        Process.Start(DestFilePath)
                    End If
                End If
            End Using

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Buttons "

    Private Sub btnOtherNarrativeDownload_Click(sender As Object, e As EventArgs) Handles btnOtherNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtOtherNarrativeDoc.Text = "On File" OrElse txtOtherNarrativePDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtOtherNarrativeDoc.Text = "On File" AndAlso txtOtherNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnOtherPermitDownload_Click(sender As Object, e As EventArgs) Handles btnOtherPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtOtherPermitDoc.Text = "On File" OrElse txtOtherPermitPDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtOtherPermitDoc.Text = "On File" AndAlso txtOtherPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnTVNarrativeDownload_Click(sender As Object, e As EventArgs) Handles btnTVNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVNarrativeDoc.Text = "On File" OrElse txtTVNarrativePDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtTVNarrativeDoc.Text = "On File" AndAlso txtTVNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnTVDraftDownload_Click(sender As Object, e As EventArgs) Handles btnTVDraftDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVDraftDoc.Text = "On File" OrElse txtTVDraftPDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtTVDraftDoc.Text = "On File" AndAlso txtTVDraftPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnTVPublicNoticeDownload_Click(sender As Object, e As EventArgs) Handles btnTVPublicNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVPublicNoticeDoc.Text = "On File" OrElse txtTVPublicNoticePDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtTVPublicNoticeDoc.Text = "On File" AndAlso txtTVPublicNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnTVFinalDownload_Click(sender As Object, e As EventArgs) Handles btnTVFinalDownload.Click
        Try
            Dim Result As String = ""

            If (txtTVFinalDoc.Text = "On File" OrElse txtTVFinalPDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtTVFinalDoc.Text = "On File" AndAlso txtTVFinalPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPSDAppSummaryDownload_Click(sender As Object, e As EventArgs) Handles btnPSDAppSummaryDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDAppSummaryDoc.Text = "On File" OrElse txtPSDAppSummaryPDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtPSDAppSummaryDoc.Text = "On File" AndAlso txtPSDAppSummaryPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPSDPrelimDetDownload_Click(sender As Object, e As EventArgs) Handles btnPSDPrelimDetDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDPrelimDetDoc.Text = "On File" OrElse txtPSDPrelimDetPDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtPSDPrelimDetDoc.Text = "On File" AndAlso txtPSDPrelimDetPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPSDNarrativeDownload_Click(sender As Object, e As EventArgs) Handles btnPSDNarrativeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDNarrativeDoc.Text = "On File" OrElse txtPSDNarrativePDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtPSDNarrativeDoc.Text = "On File" AndAlso txtPSDNarrativePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPSDDraftPermitDownload_Click(sender As Object, e As EventArgs) Handles btnPSDDraftPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDDraftPermitDoc.Text = "On File" OrElse txtPSDDraftPermitPDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtPSDDraftPermitDoc.Text = "On File" AndAlso txtPSDDraftPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPSDPublicNoticeDownload_Click(sender As Object, e As EventArgs) Handles btnPSDPublicNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDPublicNoticeDoc.Text = "On File" OrElse txtPSDPublicNoticePDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtPSDPublicNoticeDoc.Text = "On File" AndAlso txtPSDPublicNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPSDHearingNoticeDownload_Click(sender As Object, e As EventArgs) Handles btnPSDHearingNoticeDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDHearingNoticeDoc.Text = "On File" OrElse txtPSDHearingNoticePDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtPSDHearingNoticeDoc.Text = "On File" AndAlso txtPSDHearingNoticePDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPSDFinalDetDownload_Click(sender As Object, e As EventArgs) Handles btnPSDFinalDetDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDFinalDetDoc.Text = "On File" OrElse txtPSDFinalDetPDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtPSDFinalDetDoc.Text = "On File" AndAlso txtPSDFinalDetPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPSDFinalPermitDownload_Click(sender As Object, e As EventArgs) Handles btnPSDFinalPermitDownload.Click
        Try
            Dim Result As String = ""

            If (txtPSDFinalPermitDoc.Text = "On File" OrElse txtPSDFinalPermitPDF.Text = "On File") AndAlso txtApplicationNumber.Text <> "" Then
                If txtPSDFinalPermitDoc.Text = "On File" AndAlso txtPSDFinalPermitPDF.Text = "On File" Then
                    Result = InputBox("If you want to download the word document type 'Word'." & vbCrLf &
                    "If you want to download the pdf file type 'pdf'." & vbCrLf &
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Delete buttons"
    Private Sub btnDeletePSDAppSummary_Click(sender As Object, e As EventArgs) Handles btnDeletePSDAppSummary.Click
        Try
            DeleteFile("PA")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDPrelimDet_Click(sender As Object, e As EventArgs) Handles btnDeletePSDPrelimDet.Click
        Try
            DeleteFile("PP")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDNarrative_Click(sender As Object, e As EventArgs) Handles btnDeletePSDNarrative.Click
        Try
            DeleteFile("PT")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDDraftPermit_Click(sender As Object, e As EventArgs) Handles btnDeletePSDDraftPermit.Click
        Try
            DeleteFile("PD")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDPublicNotice_Click(sender As Object, e As EventArgs) Handles btnDeletePSDPublicNotice.Click
        Try
            DeleteFile("PN")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDHearingNotice_Click(sender As Object, e As EventArgs) Handles btnDeletePSDHearingNotice.Click
        Try
            DeleteFile("PH")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDFinalDet_Click(sender As Object, e As EventArgs) Handles btnDeletePSDFinalDet.Click
        Try
            DeleteFile("PF")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePSDFinalPermit_Click(sender As Object, e As EventArgs) Handles btnDeletePSDFinalPermit.Click
        Try
            DeleteFile("PI")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTVNarrative_Click(sender As Object, e As EventArgs) Handles btnDeleteTVNarrative.Click
        Try
            DeleteFile("VN")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTVDraft_Click(sender As Object, e As EventArgs) Handles btnDeleteTVDraft.Click
        Try
            DeleteFile("VD")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTVPublicNot_Click(sender As Object, e As EventArgs) Handles btnDeleteTVPublicNot.Click
        Try
            DeleteFile("VP")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTVFinal_Click(sender As Object, e As EventArgs) Handles btnDeleteTVFinal.Click
        Try
            DeleteFile("VF")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteOtherNarrative_Click(sender As Object, e As EventArgs) Handles btnDeleteOtherNarrative.Click
        Try
            DeleteFile("ON")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteOtherPermit_Click(sender As Object, e As EventArgs) Handles btnDeleteOtherPermit.Click
        Try
            DeleteFile("OP")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Class