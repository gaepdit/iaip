Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports EpdIt.DBUtilities
Imports Iaip.DAL

Public Class SSPPPublicNoticesAndAdvisories

    Private Class Document
        Public Property FileName As String
        Public Property ReviewingManager As Integer?
        Public Property DateReviewed As Date?
        Public Property PublishingStaff As Integer?
        Public Property DatePublished As Date?
        Public Property CommentsDate As Date?

        Private fileData As Byte()

        Public Sub SetFileData(value As Byte())
            fileData = value
        End Sub

        Public Function GetFileData() As Byte()
            Return fileData
        End Function
    End Class

    Private ReadOnly Property SelectedRowsCount As Integer
        Get
            Return dgvApplications.Rows.Cast(Of DataGridViewRow).Count(Function(r) CBool(r.Cells("Select").Value))
        End Get
    End Property

    Private Property OpenedDocument As Document

    Private displayedAppList As DataTable
    Private selectedAdvisoryApps As List(Of Integer)
    Private selectedNoticeApps As List(Of Integer)
    Private newFileName As String = ""

    Protected Overrides Sub OnLoad(e As EventArgs)
        ClearMessages()
        AddDgvSelectCheckBox()
        LoadPublicNoticesList()
        LoadExistingDocuments()

        MyBase.OnLoad(e)
    End Sub

    Private Sub ClearMessages()
        lblDocumentName.Text = ""
        lblFileName.Text = ""
        lblExpirationDate.Text = ""
    End Sub

    Private Sub AddDgvSelectCheckBox()
        If Not dgvApplications.Columns.Contains("Select") Then
            Dim colSelect As New DataGridViewCheckBoxColumn With {
                .ThreeState = False,
                .TrueValue = True,
                .FalseValue = False,
                .HeaderText = "Select",
                .Name = "Select"
            }

            dgvApplications.Columns.Add(colSelect)
        End If
    End Sub

    Private Sub LoadPublicNoticesList()
        displayedAppList = DB.SPGetDataTable("dbo.GetCurrentPAandPN")
        dgvApplications.DataSource = displayedAppList
        DisplaySelectionCount()
        SelectUnselectAll(True)
    End Sub

    Private Sub LoadExistingDocuments()
        cboPAPNReports.DataSource = DB.SPGetDataTable("dbo.GetPublishedPAandPN")
        cboPAPNReports.DisplayMember = "FileName"
        cboPAPNReports.ValueMember = "FileName"
    End Sub

    Private Sub dgvPublicNotice_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvApplications.CellLinkActivated
        OpenFormPermitApplication(e.LinkValue.ToString)
    End Sub

    Private Sub PreviewReport()
        rtbPreview.Clear()

        ' Compile lists of included applications
        selectedAdvisoryApps = New List(Of Integer)
        selectedNoticeApps = New List(Of Integer)

        For Each row As DataGridViewRow In dgvApplications.Rows
            If CBool(row.Cells("Select").Value) Then
                If row.Cells("Notification Type").Value.ToString = "Public Advisory" Then
                    selectedAdvisoryApps.Add(CInt(row.Cells("App Number").Value))
                ElseIf row.Cells("Notification Type").Value.ToString = "Public Notice" Then
                    selectedNoticeApps.Add(CInt(row.Cells("App Number").Value))
                End If
            End If
        Next

        ' Start RTF document
        Dim rtfDocument As New StringBuilder("{\rtf1\ansi\deff0{\fonttbl{\f0\fswiss\fcharset0 Arial;}}\uc1\lang1033\fs20")

        ' Public Advisories section header
        Dim DeadlineText As String
        If dtpExpirationDate.Checked Then
            DeadlineText = Format(CDate(dtpExpirationDate.Text), "MMMM d, yyyy")
        Else
            DeadlineText = "PUBLICATION DEADLINE"
        End If

        rtfDocument.AppendLine("\pard\qc{\b EPD PUBLIC ADVISORY}\par")
        rtfDocument.AppendLine("{\b GEORGIA AIR PROTECTION BRANCH}\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("\pard\qj The following applications have been received for Air Quality Permits. These applications are presently under review. ")
        rtfDocument.AppendLine($"{{\b Deadline:}} Any comments should be received by {DeadlineText}.\par")
        rtfDocument.AppendLine("\par")

        ' Public Advisories
        rtfDocument.AppendLine("\pard{\b SIP PUBLIC ADVISORIES}\par")
        rtfDocument.AppendLine("\par")

        If selectedAdvisoryApps.Count > 0 Then
            Dim params As SqlParameter() = {
                New SqlParameter("@PAorPN", True),
                selectedAdvisoryApps.AsTvpSqlParameter("@AppNums")
            }
            Dim dt As DataTable = DB.SPGetDataTable("dbo.GetAppDetailsForPAandPN", params)

            For Each dr As DataRow In dt.Rows
                rtfDocument.AppendLine($"\pard{{\b {GetNullableString(dr.Item("strCountyName")).IfEmpty("Unknown").ToUpper()} COUNTY}}\par")
                rtfDocument.AppendLine($"{{\b Facility Name:}} {GetNullableString(dr.Item("strFacilityName")).IfEmpty("Unknown")}\par")
                rtfDocument.AppendLine($"{{\b Application No:}} {GetNullableString(dr.Item("strApplicationNumber")).IfEmpty("Unknown")}\par")
                rtfDocument.AppendLine("{\b Facility Address:} ")
                rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityStreet1")).IfEmpty("Unknown")}, ")
                rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityCity")).IfEmpty("Unknown")}, ")
                rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityZipCode")).IfEmpty("Unknown")}\par")
                rtfDocument.AppendLine("{\b EPD Notice Type:} Permit Application\par")
                rtfDocument.AppendLine($"{{\b Description of Operation:}} {GetNullableString(dr.Item("strPlantDescription")).IfEmpty("Unknown")}\par")
                rtfDocument.AppendLine($"{{\b Reason for Application:}} {GetNullableString(dr.Item("strApplicationNotes")).IfEmpty("Unknown")}\par")
                rtfDocument.AppendLine("\par")
            Next
        Else
            rtfDocument.AppendLine("\pard None\par")
            rtfDocument.AppendLine("\par")
        End If

        ' Public Advisories section footer
        rtfDocument.AppendLine("\pard\qj {\b ADDITIONAL INFORMATION:} The permit application, additional information, and EPD generated documents are available for review at the ")
        rtfDocument.AppendLine("office of the Air Protection Branch, 4244 International Parkway, Suite 120, Atlanta, Georgia 30354 by appointment only at this time. Permit applications ")
        rtfDocument.AppendLine("listed above can be provided in electronic form upon request.\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("To comment, send written comments via email to epdcomments@dnr.ga.gov (include \ldblquote Air permit application\rdblquote  in the subject line) ")
        rtfDocument.AppendLine("or by mail to Air Permit Manager, 4244 International Parkway, Suite 120, Atlanta, Georgia 30354.\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("All comments received on or prior to the deadline will be considered by the Division in making its final decision to issue the permit. Any comments requesting ")
        rtfDocument.AppendLine("a public hearing must be made prior to the deadline and should specify, in as much detail as possible, the portion of the Georgia Rules for Air Quality Control ")
        rtfDocument.AppendLine("or the Federal Rules which the individual making the request is concerned may not have been adequately incorporated. A public hearing may be held if the ")
        rtfDocument.AppendLine("Director of the EPD finds that such a hearing would assist the EPD in a proper review of the facility's ability to comply with the Federal and State air ")
        rtfDocument.AppendLine("quality regulations.\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("Issued (final) permits are available for public viewing at this link: {\ul https://permitsearch.gaepd.org/}\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("For further information, email ask.epd@dnr.ga.gov, or call (404) 363-7000.\par")
        rtfDocument.AppendLine("\par")

        ' Public Notices section header
        rtfDocument.AppendLine("\pard\qc{\b NOTICE OF DRAFT TITLE V OPERATING PERMITS AND PERMIT MODIFICATIONS}\par")
        rtfDocument.AppendLine("{\b GEORGIA ENVIRONMENTAL PROTECTION DIVISION}\par")
        rtfDocument.AppendLine("{\b AIR PROTECTION BRANCH}\par")
        rtfDocument.AppendLine("{\b 4244 INTERNATIONAL PARKWAY, SUITE 120, ATLANTA, GA 30354}\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("\pard\qj The Georgia Environmental Protection Division announces its intent to issue initial Title V Operating Permits, Title V Significant ")
        rtfDocument.AppendLine("Modifications, Title V Operating Permit Renewals, and/or other Title V Permit proceedings for the following facilities. The deadlines for submitting ")
        rtfDocument.AppendLine("comments and requesting a public hearing are specified for each facility.\par")
        rtfDocument.AppendLine("\par")

        ' Public Notices
        If selectedNoticeApps.Count > 0 Then
            Dim params As SqlParameter() = {
                New SqlParameter("@PAorPN", False),
                selectedNoticeApps.AsTvpSqlParameter("@AppNums")
            }
            Dim ds As DataSet = DB.SPGetDataSet("dbo.GetAppDetailsForPAandPN", params)

            ' Initial Title V Permits
            rtfDocument.AppendLine("\pard{\b INITIAL TITLE V OPERATING PERMITS}\par")
            rtfDocument.AppendLine("\par")

            If ds.Tables(0).Rows.Count = 0 Then
                rtfDocument.AppendLine("\pard None\par")
                rtfDocument.AppendLine("\par")
            Else
                For Each dr As DataRow In ds.Tables(0).Rows
                    rtfDocument.AppendLine($"\pard{{\b {GetNullableString(dr.Item("strCountyName")).IfEmpty("Unknown").ToUpper()} COUNTY}}\par")
                    rtfDocument.AppendLine($"{{\b Facility Name:}} {GetNullableString(dr.Item("strFacilityName")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine($"{{\b Application No:}} {GetNullableString(dr.Item("strApplicationNumber")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine("{\b Facility Address:} ")
                    rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityStreet1")).IfEmpty("Unknown")}, ")
                    rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityCity")).IfEmpty("Unknown")}, ")
                    rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityZipCode")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine("{\b EPD Notice Type:} Proposed Permit\par")
                    rtfDocument.AppendLine($"{{\b Description of Operation:}} {GetNullableString(dr.Item("strPlantDescription")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine($"{{\b Comment period/deadline for public hearing request expires on:}} {GetNullableString(dr.Item("datPNExpires")).IfEmpty("Unknown Date")}\par")
                    rtfDocument.AppendLine("\par")
                Next
            End If

            ' Renewal Title V Permits
            rtfDocument.AppendLine("\pard{\b RENEWAL TITLE V OPERATING PERMITS}\par")
            rtfDocument.AppendLine("\par")

            If ds.Tables(1).Rows.Count = 0 Then
                rtfDocument.AppendLine("\pard None\par")
                rtfDocument.AppendLine("\par")
            Else
                For Each dr As DataRow In ds.Tables(1).Rows
                    rtfDocument.AppendLine($"\pard{{\b {GetNullableString(dr.Item("strCountyName")).IfEmpty("Unknown").ToUpper()} COUNTY}}\par")
                    rtfDocument.AppendLine($"{{\b Facility Name:}} {GetNullableString(dr.Item("strFacilityName")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine($"{{\b Application No:}} {GetNullableString(dr.Item("strApplicationNumber")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine("{\b Facility Address:} ")
                    rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityStreet1")).IfEmpty("Unknown")}, ")
                    rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityCity")).IfEmpty("Unknown")}, ")
                    rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityZipCode")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine("{\b EPD Notice Type:} Proposed Permit\par")
                    rtfDocument.AppendLine($"{{\b Description of Operation:}} {GetNullableString(dr.Item("strPlantDescription")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine($"{{\b Comment period/deadline for public hearing request expires on:}} {GetNullableString(dr.Item("datPNExpires")).IfEmpty("Unknown Date")}\par")
                    rtfDocument.AppendLine("\par")
                Next
            End If

            ' Title V Sig Mods
            rtfDocument.AppendLine("\pard{\b TITLE V SIGNIFICANT MODIFICATIONS}\par")
            rtfDocument.AppendLine("\par")

            If ds.Tables(2).Rows.Count = 0 Then
                rtfDocument.AppendLine("\pard None\par")
                rtfDocument.AppendLine("\par")
            Else
                For Each dr As DataRow In ds.Tables(2).Rows
                    rtfDocument.AppendLine($"\pard{{\b {GetNullableString(dr.Item("strCountyName")).IfEmpty("Unknown").ToUpper()} COUNTY}}\par")
                    rtfDocument.AppendLine($"{{\b Facility Name:}} {GetNullableString(dr.Item("strFacilityName")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine($"{{\b Application No:}} {GetNullableString(dr.Item("strApplicationNumber")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine("{\b Facility Address:} ")
                    rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityStreet1")).IfEmpty("Unknown")}, ")
                    rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityCity")).IfEmpty("Unknown")}, ")
                    rtfDocument.AppendLine($"{GetNullableString(dr.Item("strFacilityZipCode")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine("{\b EPD Notice Type:} Proposed Permit\par")
                    rtfDocument.AppendLine($"{{\b Description of Operation:}} {GetNullableString(dr.Item("strPlantDescription")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine($"{{\b Emission Increase/Decrease:}} {GetNullableString(dr.Item("strSignificantComments")).IfEmpty("Unknown")}\par")
                    rtfDocument.AppendLine($"{{\b Description of Requested Modification/Change:}} {GetNullableString(dr.Item("strApplicationNotes")).IfEmpty("N/A")}\par")
                    rtfDocument.AppendLine($"{{\b Comment period/deadline for public hearing request expires on:}} {GetNullableString(dr.Item("datPNExpires")).IfEmpty("Unknown Date")}\par")
                    rtfDocument.AppendLine("\par")
                Next
            End If
        Else
            rtfDocument.AppendLine("\pard{\b NO TITLE V ADVISORIES}\par")
            rtfDocument.AppendLine("\par")
        End If

        ' Public Notices section footer
        rtfDocument.AppendLine("\pard\qj{\b ADDITIONAL INFORMATION:} The draft permits and permit amendments and all information used to develop the draft permits and permit ")
        rtfDocument.AppendLine("amendments are available for review. This includes the application, all relevant supporting materials and all other materials available to the permitting ")
        rtfDocument.AppendLine("authority used in the permit review process. This information is available for review at the office of the Air Protection Branch, 4244 International Parkway, ")
        rtfDocument.AppendLine("Suite 120, Atlanta, Georgia 30354. ")
        rtfDocument.AppendLine("Copies of the draft permits or permit amendments, narratives, and (in most cases) permit applications are also available at our Internet site ")
        rtfDocument.AppendLine("{\ul https://epd.georgia.gov}. ")
        rtfDocument.AppendLine("The direct link to the draft Title V permits and amendments is {\ul https://epd.georgia.gov/draft-title-v-permitsamendments-other-draft-permits}. Also available ")
        rtfDocument.AppendLine("at this Internet site is a copy of the public notice, as it will appear in the legal organ of the county where the facility is located.\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("Persons wishing to comment on a draft Initial Title V Operating Permit, Title V Significant Modification, Title V Operating Permit Renewal, or other Title V ")
        rtfDocument.AppendLine("Permit proceedings are required to submit their comments in writing. To comment, send written comments via email to epdcomments@dnr.ga.gov (include ")
        rtfDocument.AppendLine("\ldblquote Air permit application\rdblquote  in the subject line) or by mail to Air Permit Manager, 4244 International Parkway, Suite 120, Atlanta, Georgia 30354.\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("Comments must be received by no later than the deadline indicated for the particular facility. (Should the comment period end on a weekend or holiday, ")
        rtfDocument.AppendLine("comments will be accepted up until the next working day.) All comments received on or prior to the deadline will be considered by the Division in making ")
        rtfDocument.AppendLine("its final decision to issue the Title V permit or permit amendment.\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("Any comments requesting a public hearing must be made prior to the deadline and should specify, in as much detail as possible, the portion of the Georgia ")
        rtfDocument.AppendLine("Rules for Air Quality Control or the Federal Rules which the individual making the request is concerned may not have been adequately incorporated. A public ")
        rtfDocument.AppendLine("hearing may be held if the Director of the EPD finds that such a hearing would assist the EPD in a proper review of the facility's ability to comply with ")
        rtfDocument.AppendLine("the Federal and State air quality regulations.\par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("Issued (final) permits are available for public viewing at this link: {\ul https://permitsearch.gaepd.org/} \par")
        rtfDocument.AppendLine("\par")
        rtfDocument.AppendLine("For further information, email ask.epd@dnr.ga.gov or call (404) 363-7000.\par}")
        rtfDocument.AppendLine("\par")

        rtbPreview.Rtf = rtfDocument.ToString()
        btnPublishDocument.Enabled = True
        btnCopyPreviewDocument.Enabled = True
        lblPreviewCopied.Visible = False
    End Sub

    Private Sub GenerateFileName()
        Dim FileMonth As String
        Dim FileYear As String
        Dim FileWeek As String
        Dim Flag As Boolean = False

        FileMonth = Today.Month.ToString.PadLeft(2, "0"c)
        FileYear = Strings.Right(Today.Year.ToString, 2)
        FileWeek = CType(Math.Floor((Today.Day + 6) / 7), String)

        newFileName = "PA" & FileMonth & FileYear & "-" & FileWeek

        Do While Not Flag
            If DB.SPGetBoolean("dbo.PAandPNFileNameExists", New SqlParameter("@FileName", newFileName)) Then
                If newFileName.Length <= 8 Then
                    newFileName &= "a"
                Else
                    Select Case Mid(newFileName, 9, 1)
                        Case "a"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "b"
                        Case "b"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "c"
                        Case "c"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "d"
                        Case "d"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "e"
                        Case "e"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "f"
                        Case "f"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "g"
                        Case "g"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "h"
                        Case "h"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "i"
                        Case "i"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "j"
                        Case "j"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "k"
                        Case "k"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "l"
                        Case "l"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "m"
                        Case "m"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "n"
                        Case "n"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "o"
                        Case "o"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "p"
                        Case "p"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "q"
                        Case "q"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "r"
                        Case "r"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "s"
                        Case "s"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "t"
                        Case "t"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "u"
                        Case "u"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "v"
                        Case "v"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "w"
                        Case "w"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "x"
                        Case "x"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "y"
                        Case "y"
                            newFileName = Mid(newFileName, 1, (newFileName.Length - 1)) & "z"
                    End Select
                End If
            Else
                Flag = True
            End If
        Loop
    End Sub

    Private Function SaveDocument() As Integer
        Dim newDocument As New Document With {
            .FileName = newFileName,
            .ReviewingManager = CurrentUser.UserID,
            .DateReviewed = Today,
            .PublishingStaff = CurrentUser.UserID,
            .DatePublished = Today,
            .CommentsDate = If(dtpExpirationDate.Checked, dtpExpirationDate.Value, Today)
        }

        Dim Encoder As New ASCIIEncoding
        newDocument.SetFileData(Encoder.GetBytes(rtbPreview.Rtf))

        With newDocument
            Dim params As SqlParameter() = {
                New SqlParameter("@FileName", .FileName),
                New SqlParameter("@FileData", .GetFileData()),
                New SqlParameter("@ReviewingManager", .ReviewingManager),
                New SqlParameter("@DateReviewed", .DateReviewed),
                New SqlParameter("@PublishingStaff", .PublishingStaff),
                New SqlParameter("@DatePublished", .DatePublished),
                New SqlParameter("@CommentsDate", .CommentsDate)
            }

            If selectedAdvisoryApps.Count > 0 Then
                params.Add(selectedAdvisoryApps.AsTvpSqlParameter("@PublicAdvisoryAppNums"))
            End If

            If selectedNoticeApps.Count > 0 Then
                params.Add(selectedNoticeApps.AsTvpSqlParameter("@PublicNoticeAppNums"))
            End If

            Return DB.SPReturnValue("dbo.SavePAandPNDocument", params)
        End With
    End Function

    Private Sub ExportDocument(whichTextBox As RichTextBox, fileName As String)
        Using dialog As New SaveFileDialog() With {
            .Filter = "Rich Text Format (*.rtf)|*.rtf",
            .DefaultExt = ".rtf",
            .FileName = String.Concat(fileName, ".rtf"),
            .InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
        }

            If dialog.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            If Not dialog.FileName.EndsWith(".rtf", StringComparison.CurrentCultureIgnoreCase) Then
                dialog.FileName &= ".rtf"
            End If

            If Path.GetDirectoryName(dialog.FileName) <> dialog.InitialDirectory Then
                SaveUserSetting(UserSetting.FileDownloadLocation, Path.GetDirectoryName(dialog.FileName))
            End If

            Try
                UseWaitCursor = True
                whichTextBox.SaveFile(dialog.FileName)
                Process.Start("explorer.exe", "/select,""" & dialog.FileName & """")

            Catch ex As IOException
                If ex.Message.Contains("The process cannot access the file") AndAlso
                  ex.Message.Contains("because it is being used by another process") Then

                    MessageBox.Show("The selected file is in use. Please close it or select a different filename and try again.",
                                    "File in use", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("There was an error creating the file.",
                                    "File error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Catch ex As Exception
                ErrorReport(ex, $"FileName: {dialog.FileName}", MethodBase.GetCurrentMethod.Name)
            Finally
                UseWaitCursor = False
            End Try
        End Using
    End Sub

    Private Sub OpenExistingDocument()
        btnDownloadDocument.Enabled = False
        btnCopyDocument.Enabled = False
        lblDocumentCopied.Visible = False

        Dim dr As DataRow = DB.SPGetDataRow("dbo.OpenPAandPNDocument", New SqlParameter("@filename", cboPAPNReports.Text))

        If dr IsNot Nothing Then

            OpenedDocument = New Document With {
                .CommentsDate = GetNullableDateTime(dr.Item("DATCOMMENTSDATE")),
                .DatePublished = GetNullableDateTime(dr.Item("DATPUBLISHEDDATE")),
                .DateReviewed = GetNullableDateTime(dr.Item("DATREVIEWED")),
                .FileName = GetNullableString(dr.Item("STRFILENAME")),
                .PublishingStaff = GetNullable(Of Integer)(dr.Item("STRPUBLISHINGSTAFF")),
                .ReviewingManager = GetNullable(Of Integer)(dr.Item("STRREVIEWINGMANAGER"))
            }

            OpenedDocument.SetFileData(CType(dr.Item("BATCHFILE"), Byte()))

            lblDocumentName.Text = OpenedDocument.FileName
            lblExpirationDate.Text = OpenedDocument.CommentsDate?.ToShortDateString

            Using ms As New MemoryStream(OpenedDocument.GetFileData())
                rtbDocument.LoadFile(ms, RichTextBoxStreamType.RichText)
            End Using

            btnDownloadDocument.Enabled = True
            btnCopyDocument.Enabled = True
        Else
            OpenedDocument = Nothing

            lblDocumentName.Text = ""
            lblExpirationDate.Text = ""
            rtbDocument.Text = ""

            MessageBox.Show("Error loading document. Please contact EPD-IT for assistance.", "Error")
        End If
    End Sub

    Private Sub btnAddToApplicationList_Click(sender As Object, e As EventArgs) Handles btnAddToApplicationList.Click
        Dim appNumber As Integer

        If Not Integer.TryParse(txtApplicationNumberEditor.Text, appNumber) OrElse Not Sspp.ApplicationExists(appNumber) Then
            MessageBox.Show("Enter a valid application number.", "Error")
            Return
        End If

        If Not rdbPublicAdvisories.Checked AndAlso Not rdbPublicNotice.Checked Then
            MessageBox.Show("Select either Public Advisory or Public Notice.", "Error")
            Return
        End If

        Dim compareString As String = "Public Notice"

        If rdbPublicAdvisories.Checked Then
            compareString = "Public Advisory"
        End If

        If dgvApplications.Rows.Cast(Of DataGridViewRow).
            Any(Function(r)
                    Return CInt(r.Cells("App Number").Value) = appNumber AndAlso
                    r.Cells("Notification Type").Value.ToString = compareString
                End Function) Then

            If dgvApplications.Columns("App Number").ContainsValue(appNumber) Then
                MessageBox.Show("The application is already in the list.", "Error")
            End If

            Return
        End If

        Dim params As SqlParameter() = {
            New SqlParameter("@AppNumber", appNumber),
            New SqlParameter("@PAorPN", rdbPublicAdvisories.Checked)
        }

        Dim dr As DataRow = DB.SPGetDataRow("dbo.GetPAandPNDetailsForApp", params)

        If dr IsNot Nothing Then
            displayedAppList.ImportRow(dr)

            dgvApplications.Rows.Cast(Of DataGridViewRow).
                First(Function(r) CInt(r.Cells("App Number").Value) = appNumber).
                Cells("Select").Value = True
        End If

        DisplaySelectionCount()
    End Sub

    Private Sub btnPreviewDocument_Click(sender As Object, e As EventArgs) Handles btnPreviewDocument.Click
        PreviewReport()
    End Sub

    Private Sub btnPublishDocument_Click(sender As Object, e As EventArgs) Handles btnPublishDocument.Click
        If newFileName = "" Then
            GenerateFileName()
        End If

        If SaveDocument() = 0 Then
            lblFileName.Text = String.Format("Saved as: {0}", newFileName)
            ExportDocument(rtbPreview, newFileName)
            LoadExistingDocuments()
            cboPAPNReports.Text = newFileName
            OpenExistingDocument()
            TCPublicNotices.SelectedTab = tpPrevious
        Else
            MessageBox.Show("There was an error saving the document. Contact EPD-IT for assistance.", "Error")
        End If
    End Sub

    Private Sub btnOpenPAPN_Click(sender As Object, e As EventArgs) Handles btnOpenDocument.Click
        OpenExistingDocument()
    End Sub

    Private Sub btnDownloadDocument_Click(sender As Object, e As EventArgs) Handles btnDownloadDocument.Click
        If OpenedDocument IsNot Nothing AndAlso Not String.IsNullOrEmpty(OpenedDocument.FileName) Then
            ExportDocument(rtbDocument, OpenedDocument.FileName)
        End If
    End Sub

    Private Sub btnRefesh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadPublicNoticesList()
    End Sub

    Private Sub DisplaySelectionCount()
        lblSelectedCount.Text = String.Format("Selected: {0}", SelectedRowsCount.ToString)
    End Sub

    Private Sub SelectUnselectAll(value As Boolean)
        For Each row As DataGridViewRow In dgvApplications.Rows
            row.Cells("Select").Value = value
        Next

        DisplaySelectionCount()
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        SelectUnselectAll(True)
    End Sub

    Private Sub btnSelectNone_Click(sender As Object, e As EventArgs) Handles btnSelectNone.Click
        SelectUnselectAll(False)
    End Sub

    Private Sub dgvApplications_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvApplications.CellClick
        If e.RowIndex <> -1 AndAlso e.ColumnIndex = 0 AndAlso e.RowIndex < dgvApplications.RowCount Then
            Dim row As DataGridViewRow = dgvApplications.Rows(e.RowIndex)

            If CBool(row.Cells("Select").Value) Then
                dgvApplications.Rows(e.RowIndex).Cells("Select").Value = False
            Else
                dgvApplications.Rows(e.RowIndex).Cells("Select").Value = True
            End If

            DisplaySelectionCount()
        End If
    End Sub

    ' Accept Button

    Private Sub cboPAPNReports_Enter(sender As Object, e As EventArgs) Handles cboPAPNReports.Enter
        AcceptButton = btnOpenDocument
    End Sub

    Private Sub txtApplicationNumberEditor_Enter(sender As Object, e As EventArgs) _
        Handles txtApplicationNumberEditor.Enter, rdbPublicAdvisories.Enter, rdbPublicNotice.Enter
        AcceptButton = btnAddToApplicationList
    End Sub

    Private Sub dtpExpirationDate_Enter(sender As Object, e As EventArgs) Handles dtpExpirationDate.Enter
        AcceptButton = btnPreviewDocument
    End Sub

    Private Sub NoAcceptButton(sender As Object, e As EventArgs) _
        Handles cboPAPNReports.Leave,
        txtApplicationNumberEditor.Leave, rdbPublicAdvisories.Leave, rdbPublicNotice.Leave, dtpExpirationDate.Leave
        AcceptButton = Nothing
    End Sub

    'Form overrides dispose to clean up the component list. 
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If displayedAppList IsNot Nothing Then displayedAppList.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub btnCopyPreviewDocument_Click(sender As Object, e As EventArgs) Handles btnCopyPreviewDocument.Click
        rtbPreview.SelectAll()
        rtbPreview.Copy()
        rtbDocument.Select(0, 0)
        lblPreviewCopied.Visible = True
    End Sub

    Private Sub btnCopyDocument_Click(sender As Object, e As EventArgs) Handles btnCopyDocument.Click
        rtbDocument.SelectAll()
        rtbDocument.Copy()
        rtbDocument.Select(0, 0)
        lblDocumentCopied.Visible = True
    End Sub
End Class