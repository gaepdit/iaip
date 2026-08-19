Imports GaEpd
Imports Microsoft.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class SSPPPermitUploader
    Private Property MasterApp As String
    Private Const OnFileText As String = "On File"

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

    Private Sub btnFindApplication_Click(sender As Object, e As EventArgs) Handles btnFindApplication.Click
        If txtApplicationNumber.Text <> "" Then FindApplicationInformation()
    End Sub

    Friend Sub FindApplicationInformation()
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
                Dim linkedSB As New StringBuilder(txtApplicationLinks.Text)

                For Each dr As DataRow In dt.Rows
                    linkedSB.AppendLine(dr.Item("strApplicationNumber"))
                Next

                txtApplicationLinks.Text = linkedSB.ToString
            Else
                txtApplicationLinks.Clear()
            End If

            rdbTitleVPermit.Checked = False
            rdbPSDPermit.Checked = False
            rdbOtherPermit.Checked = False

            SQL = "select top (1) p.STRFILENAME
                from APBPERMITS p
                    left join SSPPAPPLICATIONLINKING l
                        on SUBSTRING(p.STRFILENAME, 4, 10) = l.STRMASTERAPPLICATION
                where (l.STRAPPLICATIONNUMBER = @appnum
                    or p.STRFILENAME like @appnumlike) "

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

    Private Function UploadFile(fileName As String, pdfLocation As String) As Boolean
        Try
            Dim query As String = "select convert(bit, count(*)) from APBPERMITS where STRFILENAME = @FileName "
            Dim p As New SqlParameter("@FileName", fileName)
            Dim fileExists As Boolean = DB.GetBoolean(query, p)

            If fileExists Then
                Dim ResultPDF As DialogResult
                Dim msg As String

                Select Case Mid(fileName, 1, 2)
                    Case "VN"
                        msg = "Title V Narrative"
                    Case "VD"
                        msg = "Title V Draft Permit"
                    Case "VP"
                        msg = "Title V Public Notice"
                    Case "VF"
                        msg = "Title V Final Permit"
                    Case "PA"
                        msg = "PSD Application Summary"
                    Case "PP"
                        msg = "PSD Preliminary Determination"
                    Case "PT"
                        msg = "PSD Narrative"
                    Case "PD"
                        msg = "PSD Draft Permit"
                    Case "PN"
                        msg = "PSD Public Notice"
                    Case "PH"
                        msg = "PSD Hearing Notice"
                    Case "PF"
                        msg = "PSD Final Determination"
                    Case "PI"
                        msg = "PSD Final Permit"
                    Case "ON"
                        msg = "Other Narrative"
                    Case "OP"
                        msg = "Other Permit"
                    Case Else
                        msg = "'Unknown' application"
                End Select

                ResultPDF = MessageBox.Show($"A PDF file currently exists for this {msg}." & vbCrLf &
                        "Do you want to overwrite this file?", "Permit Uploader",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If ResultPDF = DialogResult.No Then
                    Return False
                Else
                    query = "delete APBPERMITS where STRFILENAME = @FileName "
                    DB.RunCommand(query, p)
                End If
            End If

            Dim fs As FileStream

            Try
                fs = New FileStream(pdfLocation, FileMode.OpenOrCreate, FileAccess.Read)
            Catch ex As IOException
                If ex.Message.Contains("it is being used by another process") Then
                    MessageBox.Show("The file is currently in use. Please close the file and try again.")
                    Return False
                Else
                    Throw
                End If
            End Try

            Dim rawData As Byte() = New Byte(fs.Length) {}

#Disable Warning CA2022 ' Avoid inexact read with 'Stream.Read'
            fs.Read(rawData, 0, Convert.ToInt32(fs.Length))
#Enable Warning CA2022 ' Avoid inexact read with 'Stream.Read'

            fs.Close()

            query = "insert into APBPERMITS
                ([ROWCOUNT], STRFILENAME, PDFPERMITDATA, STRPDFMODIFINGPERSON, DATPDFMODIFINGDATE)
                values ((select (max([ROWCOUNT]) + 1) from APBPERMITS), @filename, @rawdata, @user, getdate()) "

            Dim pf As SqlParameter() = {
                New SqlParameter("@filename", fileName),
                New SqlParameter("@rawdata", rawData),
                New SqlParameter("@user", CurrentUser.UserID)
            }

            DB.RunCommand(query, pf)

            Return True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Sub SaveFiles()
        Try
            Const SQL As String = "Update SSPPApplicationTracking set " &
                "datFinalOnWeb =  GETDATE()  " &
                "where strApplicationNumber = @appnum and datfinalonweb is not null "

            Dim p As New SqlParameter("@appnum", MasterApp)

            If rdbTitleVPermit.Checked Then
                If chbTVNarrative.Checked AndAlso txtTVNarrativePDF.Text <> "" AndAlso txtTVNarrativePDF.Text <> "N/A" AndAlso
                        Mid(txtTVNarrativePDF.Text, (txtTVNarrativePDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("VN-" & MasterApp, txtTVNarrativePDF.Text)
                End If
                If chbTVDraft.Checked AndAlso txtTVDraftPDF.Text <> "" AndAlso txtTVDraftPDF.Text <> "N/A" AndAlso
                        Mid(txtTVDraftPDF.Text, (txtTVDraftPDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("VD-" & MasterApp, txtTVDraftPDF.Text)
                End If
                If chbTVPublicNotice.Checked AndAlso txtTVPublicNoticePDF.Text <> "" AndAlso txtTVPublicNoticePDF.Text <> "N/A" AndAlso
                        Mid(txtTVPublicNoticePDF.Text, (txtTVPublicNoticePDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("VP-" & MasterApp, txtTVPublicNoticePDF.Text)
                End If
                If chbTVFinal.Checked AndAlso txtTVFinalPDF.Text <> "" AndAlso txtTVFinalPDF.Text <> "N/A" AndAlso
                        Mid(txtTVFinalPDF.Text, (txtTVFinalPDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then

                    If UploadFile("VF-" & MasterApp, txtTVFinalPDF.Text) Then DB.RunCommand(SQL, p)

                End If
            End If

            If rdbPSDPermit.Checked Then
                If chbPSDApplicationSummary.Checked AndAlso txtPSDAppSummaryPDF.Text <> "" AndAlso txtPSDAppSummaryPDF.Text <> "N/A" AndAlso
                        Mid(txtPSDAppSummaryPDF.Text, (txtPSDAppSummaryPDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("PA-" & MasterApp, txtPSDAppSummaryPDF.Text)
                End If
                If chbPSDPrelimDet.Checked AndAlso txtPSDPrelimDetPDF.Text <> "" AndAlso txtPSDPrelimDetPDF.Text <> "N/A" AndAlso
                        Mid(txtPSDPrelimDetPDF.Text, (txtPSDPrelimDetPDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("PP-" & MasterApp, txtPSDPrelimDetPDF.Text)
                End If
                If chbPSDNarrative.Checked AndAlso txtPSDNarrativePDF.Text <> "" AndAlso txtPSDNarrativePDF.Text <> "N/A" AndAlso
                        Mid(txtPSDNarrativePDF.Text, (txtPSDNarrativePDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("PT-" & MasterApp, txtPSDNarrativePDF.Text)
                End If
                If chbPSDDraftPermit.Checked AndAlso txtPSDDraftPermitPDF.Text <> "" AndAlso txtPSDDraftPermitPDF.Text <> "N/A" AndAlso
                        Mid(txtPSDDraftPermitPDF.Text, (txtPSDDraftPermitPDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("PD-" & MasterApp, txtPSDDraftPermitPDF.Text)
                End If
                If chbPSDPublicNotice.Checked AndAlso txtPSDPublicNoticePDF.Text <> "" AndAlso txtPSDPublicNoticePDF.Text <> "N/A" AndAlso
                        Mid(txtPSDPublicNoticePDF.Text, (txtPSDPublicNoticePDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("PN-" & MasterApp, txtPSDPublicNoticePDF.Text)
                End If
                If chbPSDHearingNotice.Checked AndAlso txtPSDHearingNoticePDF.Text <> "" AndAlso txtPSDHearingNoticePDF.Text <> "N/A" AndAlso
                        Mid(txtPSDHearingNoticePDF.Text, (txtPSDHearingNoticePDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("PH-" & MasterApp, txtPSDHearingNoticePDF.Text)
                End If
                If chbPSDFinalDet.Checked AndAlso txtPSDFinalDetPDF.Text <> "" AndAlso txtPSDFinalDetPDF.Text <> "N/A" AndAlso
                        Mid(txtPSDFinalDetPDF.Text, (txtPSDFinalDetPDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("PF-" & MasterApp, txtPSDFinalDetPDF.Text)
                End If
                If chbPSDFinalPermit.Checked AndAlso txtPSDFinalPermitPDF.Text <> "" AndAlso txtPSDFinalPermitPDF.Text <> "N/A" AndAlso
                        Mid(txtPSDFinalPermitPDF.Text, (txtPSDFinalPermitPDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then

                    If UploadFile("PI-" & MasterApp, txtPSDFinalPermitPDF.Text) Then DB.RunCommand(SQL, p)

                End If
            End If

            If rdbOtherPermit.Checked Then
                If chbOtherNarrative.Checked AndAlso txtOtherNarrativePDF.Text <> "" AndAlso txtOtherNarrativePDF.Text <> "N/A" AndAlso
                        Mid(txtOtherNarrativePDF.Text, (txtOtherNarrativePDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then
                    UploadFile("ON-" & MasterApp, txtOtherNarrativePDF.Text)
                End If
                If chbOtherPermit.Checked AndAlso txtOtherPermitPDF.Text <> "" AndAlso txtOtherPermitPDF.Text <> "N/A" AndAlso
                        Mid(txtOtherPermitPDF.Text, (txtOtherPermitPDF.Text.Length - 3)).Equals(".PDF", StringComparison.CurrentCultureIgnoreCase) Then

                    If UploadFile("OP-" & MasterApp, txtOtherPermitPDF.Text) Then DB.RunCommand(SQL, p)

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
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this file?" & vbCrLf &
                    "It will not be recoverable if you delete it.", "Permit Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

            If result = DialogResult.Yes Then
                Dim sql As String = "delete APBPERMITS where STRFILENAME = @filename "
                Dim p As New SqlParameter("@filename", FileType & "-" & MasterApp)

                DB.RunCommand(sql, p)

                FindApplicationInformation()
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
                SQL = "select STRFILENAME from APBPERMITS where STRFILENAME like @filename "

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
                SQL = "select STRFILENAME from APBPERMITS where STRFILENAME like @filename "

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
                SQL = "select STRFILENAME from APBPERMITS where STRFILENAME like @filename "

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

    Private Sub LoadDocDetails(ft As String, chb As CheckBox, txt As TextBox,
                               btnUp As Button, btnDown As Button, btnDelete As Button,
                               lblStaff As Label, lblDate As Label)
        If Not chb.Checked OrElse MasterApp = "" Then
            txt.Clear()
            txt.Visible = False
            lblStaff.Visible = False
            lblDate.Visible = False
            btnUp.Visible = False
            btnDown.Visible = False
            btnDelete.Visible = False
            Return
        End If

        txt.Visible = True
        btnUp.Visible = True

        Const SQL As String = "select IIF(p.PDFPERMITDATA is null, null, 'True')        PDFDataExists,
                    concat_ws(', ', u.STRLASTNAME, u.STRFIRSTNAME) as StaffName,
                    convert(date, p.DATPDFMODIFINGDATE)            as DateUploaded
            from dbo.APBPERMITS p
                left join dbo.EPDUSERPROFILES u
                    on u.NUMUSERID = p.STRPDFMODIFINGPERSON
            where p.STRFILENAME = @fn"

        Dim p As New SqlParameter("@fn", $"{ft}-{MasterApp}")

        Dim dr As DataRow = DB.GetDataRow(SQL, p)

        If dr IsNot Nothing Then
            If IsDBNull(dr.Item("PDFDataExists")) Then
                txt.Text = ""
            Else
                txt.Text = OnFileText
            End If
            lblStaff.Visible = True
            lblStaff.Text = DBUtilities.GetNullableString(dr.Item("StaffName"))
            lblDate.Visible = True
            lblDate.Text = Format(DBUtilities.GetNullableDateTime(dr.Item("DateUploaded")), "dd-MMM-yyyy")
        End If

        If txt.Text = OnFileText Then
            btnDown.Visible = True
            If CurrentUser.HasPermission(UserCan.DeletePermitFile) Then
                btnDelete.Visible = True
            Else
                btnDelete.Visible = False
            End If
        Else
            btnDown.Visible = False
            btnDelete.Visible = False
        End If
    End Sub

    Private Sub chbTVNarrative_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVNarrative.CheckedChanged
        LoadDocDetails("VN", chbTVNarrative, txtTVNarrativePDF,
                       btnTVNarrative, btnTVNarrativeDownload, btnDeleteTVNarrative,
                       lblTVNarrativeSRPDF, lblTVNarrativeDUPDF)
    End Sub
    Private Sub chbTVDraft_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVDraft.CheckedChanged
        LoadDocDetails("VD", chbTVDraft, txtTVDraftPDF,
                       btnTVDraft, btnTVDraftDownload, btnDeleteTVDraft,
                       lblTVDraftSRPDF, lblTVDraftDUPDF)
    End Sub
    Private Sub chbTVPublicNotice_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVPublicNotice.CheckedChanged
        LoadDocDetails("VP", chbTVPublicNotice, txtTVPublicNoticePDF,
                       btnTVPublicNotice, btnTVPublicNoticeDownload, btnDeleteTVPublicNot,
                       lblTVPublicNoticeSRPDF, lblTVPublicNoticeDUPDF)
    End Sub
    Private Sub chbTVFinal_CheckedChanged(sender As Object, e As EventArgs) Handles chbTVFinal.CheckedChanged
        LoadDocDetails("VF", chbTVFinal, txtTVFinalPDF,
                       btnTVFinal, btnTVFinalDownload, btnDeleteTVFinal,
                       lblTVFinalSRPDF, lblTVFinalDUPDF)
    End Sub
    Private Sub chbPSDApplicationSummary_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDApplicationSummary.CheckedChanged
        LoadDocDetails("PA", chbPSDApplicationSummary, txtPSDAppSummaryPDF,
                       btnPSDAppSummary, btnPSDAppSummaryDownload, btnDeletePSDAppSummary,
                       lblPSDAppSummarySRPDF, lblPSDAppSummaryDUPDF)
    End Sub
    Private Sub chbPSDPrelimDet_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDPrelimDet.CheckedChanged
        LoadDocDetails("PP", chbPSDPrelimDet, txtPSDPrelimDetPDF,
                       btnPSDPrelimDet, btnPSDPrelimDetDownload, btnDeletePSDPrelimDet,
                       lblPSDPrelimDetSRPDF, lblPSDPrelimDetDUPDF)
    End Sub
    Private Sub chbPSDNarrative_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDNarrative.CheckedChanged
        LoadDocDetails("PT", chbPSDNarrative, txtPSDNarrativePDF,
                       btnPSDNarrative, btnPSDNarrativeDownload, btnDeletePSDNarrative,
                       lblPSDNarrativeSRPDF, lblPSDNarrativeDUPDF)
    End Sub
    Private Sub chbPSDDraftPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDDraftPermit.CheckedChanged
        LoadDocDetails("PD", chbPSDDraftPermit, txtPSDDraftPermitPDF,
                       btnPSDDraftPermit, btnPSDDraftPermitDownload, btnDeletePSDDraftPermit,
                       lblPSDDraftPermitSRPDF, lblPSDDraftPermitDUPDF)
    End Sub
    Private Sub chbPSDPublicNotice_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDPublicNotice.CheckedChanged
        LoadDocDetails("PN", chbPSDPublicNotice, txtPSDPublicNoticePDF,
                       btnPSDPublicNotice, btnPSDPublicNoticeDownload, btnDeletePSDPublicNotice,
                       lblPSDPublicNoticeSRPDF, lblPSDPublicNoticeDUPDF)
    End Sub
    Private Sub chbPSDHearingNotice_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDHearingNotice.CheckedChanged
        LoadDocDetails("PH", chbPSDHearingNotice, txtPSDHearingNoticePDF,
                       btnPSDHearingNotice, btnPSDHearingNoticeDownload, btnDeletePSDHearingNotice,
                       lblPSDHearingNoticeSRPDF, lblPSDHearingNoticeDUPDF)
    End Sub
    Private Sub chbPSDFinalDet_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDFinalDet.CheckedChanged
        LoadDocDetails("PF", chbPSDFinalDet, txtPSDFinalDetPDF,
                       btnPSDFinalDet, btnPSDFinalDetDownload, btnDeletePSDFinalDet,
                       lblPSDFinalDetSRPDF, lblPSDFinalDetDUPDF)
    End Sub
    Private Sub chbPSDFinalPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbPSDFinalPermit.CheckedChanged
        LoadDocDetails("PI", chbPSDFinalPermit, txtPSDFinalPermitPDF,
                       btnPSDFinalPermit, btnPSDFinalPermitDownload, btnDeletePSDFinalPermit,
                       lblPSDFinalPermitSRPDF, lblPSDFinalPermitDUPDF)
    End Sub
    Private Sub chbOtherNarrative_CheckedChanged(sender As Object, e As EventArgs) Handles chbOtherNarrative.CheckedChanged
        LoadDocDetails("ON", chbOtherNarrative, txtOtherNarrativePDF,
                       btnOtherNarrative, btnOtherNarrativeDownload, btnDeleteOtherNarrative,
                       lblOtherNarrativeSRPDF, lblOtherNarrativeDUPDF)
    End Sub
    Private Sub chbOtherPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbOtherPermit.CheckedChanged
        LoadDocDetails("OP", chbOtherPermit, txtOtherPermitPDF,
                       btnOtherPermit, btnOtherPermitDownload, btnDeleteOtherPermit,
                       lblOtherPermitSRPDF, lblOtherPermitDUPDF)
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
                dialog.Filter = "PDF files (*.pdf)|*.pdf"
                dialog.FilterIndex = 1

                If dialog.ShowDialog = DialogResult.OK Then
                    If File.Exists(dialog.FileName) Then

                        If Path.GetDirectoryName(dialog.FileName) <> dialog.InitialDirectory Then
                            SaveUserSetting(UserSetting.FileUploadLocation, Path.GetDirectoryName(dialog.FileName))
                        End If

                        Dim thisButton As Button = DirectCast(sender, Button)
                        Dim thisControlName As String = thisButton.Name.Replace("btn", "txt")
                        Dim thisControl As TextBox = Nothing

                        Try
                            thisControl = DirectCast(thisButton.Parent.Controls(thisControlName & "PDF"), TextBox)
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

    Private Sub DownloadFile(fileType As String)
        If MasterApp = "" Then Return
        Dim fileName As String = $"{fileType}-{MasterApp}"

        Using saveFile As New SaveFileDialog
            saveFile.InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
            saveFile.FileName = fileName
            saveFile.Filter = "Adobe PDF Files (*.pdf)|.pdf"
            saveFile.FilterIndex = 1
            saveFile.DefaultExt = ".pdf"

            If saveFile.ShowDialog() = DialogResult.OK Then
                If Path.GetDirectoryName(saveFile.FileName) <> saveFile.InitialDirectory Then
                    SaveUserSetting(UserSetting.FileDownloadLocation, IO.Path.GetDirectoryName(saveFile.FileName))
                End If

                Dim sql As String = "select PDFPERMITDATA from APBPERMITS where STRFILENAME = @filename "
                Dim p2 As New SqlParameter("@filename", fileName)
                SaveBinaryFileFromDB(saveFile.FileName, sql, p2)

                Process.Start(saveFile.FileName)
            End If
        End Using
    End Sub

#End Region

#Region " Buttons "

    Private Sub btnOtherNarrativeDownload_Click(sender As Object, e As EventArgs) Handles btnOtherNarrativeDownload.Click
        If txtOtherNarrativePDF.Text = OnFileText Then DownloadFile("ON")
    End Sub
    Private Sub btnOtherPermitDownload_Click(sender As Object, e As EventArgs) Handles btnOtherPermitDownload.Click
        If txtOtherPermitPDF.Text = OnFileText Then DownloadFile("OP")
    End Sub
    Private Sub btnTVNarrativeDownload_Click(sender As Object, e As EventArgs) Handles btnTVNarrativeDownload.Click
        If txtTVNarrativePDF.Text = OnFileText Then DownloadFile("VN")
    End Sub
    Private Sub btnTVDraftDownload_Click(sender As Object, e As EventArgs) Handles btnTVDraftDownload.Click
        If txtTVDraftPDF.Text = OnFileText Then DownloadFile("VD")
    End Sub
    Private Sub btnTVPublicNoticeDownload_Click(sender As Object, e As EventArgs) Handles btnTVPublicNoticeDownload.Click
        If txtTVPublicNoticePDF.Text = OnFileText Then DownloadFile("VP")
    End Sub
    Private Sub btnTVFinalDownload_Click(sender As Object, e As EventArgs) Handles btnTVFinalDownload.Click
        If txtTVFinalPDF.Text = OnFileText Then DownloadFile("VF")
    End Sub
    Private Sub btnPSDAppSummaryDownload_Click(sender As Object, e As EventArgs) Handles btnPSDAppSummaryDownload.Click
        If txtPSDAppSummaryPDF.Text = OnFileText Then DownloadFile("PA")
    End Sub
    Private Sub btnPSDPrelimDetDownload_Click(sender As Object, e As EventArgs) Handles btnPSDPrelimDetDownload.Click
        If txtPSDPrelimDetPDF.Text = OnFileText Then DownloadFile("PP")
    End Sub
    Private Sub btnPSDNarrativeDownload_Click(sender As Object, e As EventArgs) Handles btnPSDNarrativeDownload.Click
        If txtPSDNarrativePDF.Text = OnFileText Then DownloadFile("PT")
    End Sub
    Private Sub btnPSDDraftPermitDownload_Click(sender As Object, e As EventArgs) Handles btnPSDDraftPermitDownload.Click
        If txtPSDDraftPermitPDF.Text = OnFileText Then DownloadFile("PD")
    End Sub
    Private Sub btnPSDPublicNoticeDownload_Click(sender As Object, e As EventArgs) Handles btnPSDPublicNoticeDownload.Click
        If txtPSDPublicNoticePDF.Text = OnFileText Then DownloadFile("PN")
    End Sub
    Private Sub btnPSDHearingNoticeDownload_Click(sender As Object, e As EventArgs) Handles btnPSDHearingNoticeDownload.Click
        If txtPSDHearingNoticePDF.Text = OnFileText Then DownloadFile("PH")
    End Sub
    Private Sub btnPSDFinalDetDownload_Click(sender As Object, e As EventArgs) Handles btnPSDFinalDetDownload.Click
        If txtPSDFinalDetPDF.Text = OnFileText Then DownloadFile("PF")
    End Sub
    Private Sub btnPSDFinalPermitDownload_Click(sender As Object, e As EventArgs) Handles btnPSDFinalPermitDownload.Click
        If txtPSDFinalPermitPDF.Text = OnFileText Then DownloadFile("PI")
    End Sub

#End Region

#Region "Delete buttons"
    Private Sub btnDeletePSDAppSummary_Click(sender As Object, e As EventArgs) Handles btnDeletePSDAppSummary.Click
        DeleteFile("PA")
    End Sub
    Private Sub btnDeletePSDPrelimDet_Click(sender As Object, e As EventArgs) Handles btnDeletePSDPrelimDet.Click
        DeleteFile("PP")
    End Sub
    Private Sub btnDeletePSDNarrative_Click(sender As Object, e As EventArgs) Handles btnDeletePSDNarrative.Click
        DeleteFile("PT")
    End Sub
    Private Sub btnDeletePSDDraftPermit_Click(sender As Object, e As EventArgs) Handles btnDeletePSDDraftPermit.Click
        DeleteFile("PD")
    End Sub
    Private Sub btnDeletePSDPublicNotice_Click(sender As Object, e As EventArgs) Handles btnDeletePSDPublicNotice.Click
        DeleteFile("PN")
    End Sub
    Private Sub btnDeletePSDHearingNotice_Click(sender As Object, e As EventArgs) Handles btnDeletePSDHearingNotice.Click
        DeleteFile("PH")
    End Sub
    Private Sub btnDeletePSDFinalDet_Click(sender As Object, e As EventArgs) Handles btnDeletePSDFinalDet.Click
        DeleteFile("PF")
    End Sub
    Private Sub btnDeletePSDFinalPermit_Click(sender As Object, e As EventArgs) Handles btnDeletePSDFinalPermit.Click
        DeleteFile("PI")
    End Sub
    Private Sub btnDeleteTVNarrative_Click(sender As Object, e As EventArgs) Handles btnDeleteTVNarrative.Click
        DeleteFile("VN")
    End Sub
    Private Sub btnDeleteTVDraft_Click(sender As Object, e As EventArgs) Handles btnDeleteTVDraft.Click
        DeleteFile("VD")
    End Sub
    Private Sub btnDeleteTVPublicNot_Click(sender As Object, e As EventArgs) Handles btnDeleteTVPublicNot.Click
        DeleteFile("VP")
    End Sub
    Private Sub btnDeleteTVFinal_Click(sender As Object, e As EventArgs) Handles btnDeleteTVFinal.Click
        DeleteFile("VF")
    End Sub
    Private Sub btnDeleteOtherNarrative_Click(sender As Object, e As EventArgs) Handles btnDeleteOtherNarrative.Click
        DeleteFile("ON")
    End Sub
    Private Sub btnDeleteOtherPermit_Click(sender As Object, e As EventArgs) Handles btnDeleteOtherPermit.Click
        DeleteFile("OP")
    End Sub

#End Region

End Class