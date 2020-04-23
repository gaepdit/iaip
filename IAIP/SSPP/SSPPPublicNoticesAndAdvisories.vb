Imports CrystalDecisions.Shared
Imports EpdIt.DBUtilities
Imports System.Collections.Generic
Imports System.Linq
Imports System.Data.SqlClient
Imports System.IO
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
        LoadOldPDFs()

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

    Private Sub LoadOldPDFs()
        cboPAPNReports.DataSource = DB.SPGetDataTable("dbo.GetPublishedPAandPN")
        cboPAPNReports.DisplayMember = "FileName"
        cboPAPNReports.ValueMember = "FileName"
    End Sub

    Private Sub dgvPublicNotice_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvApplications.CellLinkActivated
        OpenFormPermitApplication(e.LinkValue.ToString)
    End Sub

    Private Sub PreviewReport()
        rtbPreview.Clear()

        Dim PANeeded As String = ""
        Dim PublicAdvisories As String
        Dim TVAdvisories As String
        Dim TVInitial As String = ""
        Dim TVRenewal As String = ""
        Dim TVSigMod As String = ""
        Dim Deadline As String

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

        ' Public Advisories

        If selectedAdvisoryApps.Count > 0 Then

            Dim params As SqlParameter() = {
                New SqlParameter("@PAorPN", True),
                selectedAdvisoryApps.AsTvpSqlParameter("@AppNums")
            }

            Dim ds As DataSet = DB.SPGetDataSet("dbo.GetAppDetailsForPAandPN", params)

            For Each dr As DataRow In ds.Tables(0).Rows
                If IsDBNull(dr.Item("strCountyName")) Then
                    PANeeded &= "County Unknown" & vbCrLf & vbCrLf
                Else
                    PANeeded = PANeeded & "*X" & dr.Item("strCountyName").ToString.ToUpper & "X*" & vbCrLf & vbCrLf
                End If

                If IsDBNull(dr.Item("strFacilityName")) Then
                    PANeeded = PANeeded & "Facility Name:X Unknown" & vbCrLf
                Else
                    PANeeded = PANeeded & "Facility Name:X " & dr.Item("strFacilityName").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    PANeeded = PANeeded & "Application No:X Unknown" & vbCrLf
                Else
                    PANeeded = PANeeded & "Application No:X " & dr.Item("strApplicationNumber").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    PANeeded = PANeeded & "Facility Address:X Unknown, "
                Else
                    PANeeded = PANeeded & "Facility Address:X " & dr.Item("strFacilityStreet1").ToString & ", "
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    PANeeded = PANeeded & " Unknown, "
                Else
                    PANeeded = PANeeded & dr.Item("strFacilityCity").ToString & ", "
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    PANeeded = PANeeded & " Unknown "
                Else
                    PANeeded = PANeeded & dr.Item("strFacilityZipCode").ToString & " "
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    PANeeded = PANeeded & "(Unknown County) " & vbCrLf
                Else
                    PANeeded = PANeeded & "(" & dr.Item("strCountyName").ToString & ")" & vbCrLf
                End If
                PANeeded = PANeeded & "EPD Notice Type:X Permit - Application. " & vbCrLf
                If IsDBNull(dr.Item("strPlantDescription")) Then
                    PANeeded = PANeeded & "Description of Operation:X Unknown" & vbCrLf
                Else
                    PANeeded = PANeeded & "Description of Operation:X " & dr.Item("strPlantDescription").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strApplicationNotes")) Then
                    PANeeded = PANeeded & "Reason for Application:X Unknown " & vbCrLf & vbCrLf
                Else
                    PANeeded = PANeeded & "Reason for Application:X " & dr.Item("strApplicationNotes").ToString & vbCrLf & vbCrLf
                End If
            Next
        End If

        ' Public Notices

        If selectedNoticeApps.Count > 0 Then

            Dim params As SqlParameter() = {
                New SqlParameter("@PAorPN", False),
                selectedNoticeApps.AsTvpSqlParameter("@AppNums")
            }

            Dim ds As DataSet = DB.SPGetDataSet("dbo.GetAppDetailsForPAandPN", params)

            ' TV Initials

            For Each dr As DataRow In ds.Tables(0).Rows
                If IsDBNull(dr.Item("strCountyName")) Then
                    TVInitial = TVInitial & "County Unknown" & vbCrLf & vbCrLf
                Else
                    TVInitial = TVInitial & "*X" & dr.Item("strCountyName").ToString.ToUpper & "X*" & vbCrLf & vbCrLf
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    TVInitial = TVInitial & "Facility Name:X Unknown" & vbCrLf
                Else
                    TVInitial = TVInitial & "Facility Name:X " & dr.Item("strFacilityName").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    TVInitial = TVInitial & "Application No:X Unknown" & vbCrLf
                Else
                    TVInitial = TVInitial & "Application No:X " & dr.Item("strApplicationNumber").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    TVInitial = TVInitial & "Facility Address:X Unknown, "
                Else
                    TVInitial = TVInitial & "Facility Address:X " & dr.Item("strFacilityStreet1").ToString & ", "
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    TVInitial = TVInitial & " Unknown, "
                Else
                    TVInitial = TVInitial & dr.Item("strFacilityCity").ToString & ", "
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    TVInitial = TVInitial & " Unknown "
                Else
                    TVInitial = TVInitial & dr.Item("strFacilityZipCode").ToString & " "
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    TVInitial = TVInitial & "(Unknown County) " & vbCrLf
                Else
                    TVInitial = TVInitial & "(" & dr.Item("strCountyName").ToString & ")" & vbCrLf
                End If
                TVInitial = TVInitial & "EPD Notice Type:X Permit - Proposed. " & vbCrLf

                If IsDBNull(dr.Item("strPlantDescription")) Then
                    TVInitial = TVInitial & "Description of Operation:X Unknown" & vbCrLf
                Else
                    TVInitial = TVInitial & "Description of Operation:X " & dr.Item("strPlantDescription").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("datPNExpires")) Then
                    TVInitial = TVInitial & "Comment period/deadline for public hearing request expires on:X (Unknown Date) " & vbCrLf & vbCrLf
                Else
                    TVInitial = TVInitial & "Comment period/deadline for public hearing request expires on:X " & dr.Item("datPNExpires").ToString & vbCrLf & vbCrLf
                End If
            Next

            ' TV Renewals

            For Each dr As DataRow In ds.Tables(1).Rows
                If IsDBNull(dr.Item("strCountyName")) Then
                    TVRenewal = TVRenewal & "County Unknown" & vbCrLf & vbCrLf
                Else
                    TVRenewal = TVRenewal & "*X" & dr.Item("strCountyName").ToString.ToUpper & "X*" & vbCrLf & vbCrLf
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    TVRenewal = TVRenewal & "Facility Name:X Unknown" & vbCrLf
                Else
                    TVRenewal = TVRenewal & "Facility Name:X " & dr.Item("strFacilityName").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    TVRenewal = TVRenewal & "Application No:X Unknown" & vbCrLf
                Else
                    TVRenewal = TVRenewal & "Application No:X " & dr.Item("strApplicationNumber").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    TVRenewal = TVRenewal & "Facility Address:X Unknown, "
                Else
                    TVRenewal = TVRenewal & "Facility Address:X " & dr.Item("strFacilityStreet1").ToString & ", "
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    TVRenewal = TVRenewal & " Unknown, "
                Else
                    TVRenewal = TVRenewal & dr.Item("strFacilityCity").ToString & ", "
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    TVRenewal = TVRenewal & " Unknown "
                Else
                    TVRenewal = TVRenewal & dr.Item("strFacilityZipCode").ToString & " "
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    TVRenewal = TVRenewal & "(Unknown County) " & vbCrLf
                Else
                    TVRenewal = TVRenewal & "(" & dr.Item("strCountyName").ToString & ")" & vbCrLf
                End If
                TVRenewal = TVRenewal & "EPD Notice Type:X Permit - Proposed. " & vbCrLf

                If IsDBNull(dr.Item("strPlantDescription")) Then
                    TVRenewal = TVRenewal & "Description of Operation:X Unknown" & vbCrLf
                Else
                    TVRenewal = TVRenewal & "Description of Operation:X " & dr.Item("strPlantDescription").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("datPNExpires")) Then
                    TVRenewal = TVRenewal & "Comment period/deadline for public hearing request expires on:X (Unknown Date) " & vbCrLf & vbCrLf
                Else
                    TVRenewal = TVRenewal & "Comment period/deadline for public hearing request expires on:X " & dr.Item("datPNExpires").ToString & vbCrLf & vbCrLf
                End If
            Next

            ' TV Sig Mods

            For Each dr As DataRow In ds.Tables(2).Rows
                If IsDBNull(dr.Item("strCountyName")) Then
                    TVSigMod = TVSigMod & "County Unknown" & vbCrLf & vbCrLf
                Else
                    TVSigMod = TVSigMod & "*X" & dr.Item("strCountyName").ToString.ToUpper & "X*" & vbCrLf & vbCrLf
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    TVSigMod = TVSigMod & "Facility Name:X Unknown" & vbCrLf
                Else
                    TVSigMod = TVSigMod & "Facility Name:X " & dr.Item("strFacilityName").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    TVSigMod = TVSigMod & "Application No:X Unknown" & vbCrLf
                Else
                    TVSigMod = TVSigMod & "Application No:X " & dr.Item("strApplicationNumber").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    TVSigMod = TVSigMod & "Facility Address:X Unknown, "
                Else
                    TVSigMod = TVSigMod & "Facility Address:X " & dr.Item("strFacilityStreet1").ToString & ", "
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    TVSigMod = TVSigMod & " Unknown, "
                Else
                    TVSigMod = TVSigMod & dr.Item("strFacilityCity").ToString & ", "
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    TVSigMod = TVSigMod & " Unknown "
                Else
                    TVSigMod = TVSigMod & dr.Item("strFacilityZipCode").ToString & " "
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    TVSigMod = TVSigMod & "(Unknown County) " & vbCrLf
                Else
                    TVSigMod = TVSigMod & "(" & dr.Item("strCountyName").ToString & ")" & vbCrLf
                End If
                TVSigMod = TVSigMod & "EPD Notice Type:X Permit - Proposed. " & vbCrLf
                If IsDBNull(dr.Item("strPlantDescription")) Then
                    TVSigMod = TVSigMod & "Description of Operation:X Unknown" & vbCrLf
                Else
                    TVSigMod = TVSigMod & "Description of Operation:X " & dr.Item("strPlantDescription").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strSignificantComments")) Then
                    TVSigMod = TVSigMod & "Emission Increase/Decrease:X (Unknown) " & vbCrLf
                Else
                    TVSigMod = TVSigMod & "Emission Increase/Decrease:X " & dr.Item("strSignificantComments").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("strApplicationNotes")) Then
                    TVSigMod = TVSigMod & "Description of Requested Modification/Change:X N/A " & vbCrLf
                Else
                    TVSigMod = TVSigMod & "Description of Requested Modification/Change:X " & dr.Item("strApplicationNotes").ToString & vbCrLf
                End If
                If IsDBNull(dr.Item("datPNExpires")) Then
                    TVSigMod = TVSigMod & "Comment period/deadline for public hearing request expires on:X (Unknown Date) " & vbCrLf & vbCrLf
                Else
                    TVSigMod = TVSigMod & "Comment period/deadline for public hearing request expires on:X " & dr.Item("datPNExpires").ToString & vbCrLf & vbCrLf
                End If
            Next
        End If

        ' Final matter

        If dtpExpirationDate.Checked Then
            Deadline = Format(CDate(dtpExpirationDate.Text), "dd-MMMM-yyyy")
        Else
            Deadline = "PUBLICATION DEADLINE"
        End If


        PublicAdvisories = "EPD PUBLIC ADVISORY" &
            vbCrLf & "GEORGIA AIR PROTECTION BRANCH" &
            vbCrLf & vbCrLf & vbCrLf & "SIP PUBLIC ADVISORIES" &
            vbCrLf & vbCrLf & "The following applications have been received for Air Quality Permits. " & vbCrLf &
            "These applications are presently under review. Any comments should be received by " & Deadline & vbCrLf & vbCrLf & vbCrLf

        If PANeeded <> "" Then
            PublicAdvisories = PublicAdvisories & PANeeded & vbCrLf
        Else
            PublicAdvisories = PublicAdvisories & "NO PUBLIC ADVISORIESX" & vbCrLf & vbCrLf
        End If

        PublicAdvisories = PublicAdvisories & "For additional information, contact Eric Cornwell, Program Manager, " & vbCrLf &
            "Stationary Source Permitting Program, Air Protection Branch, " & vbCrLf &
            "4244 International Parkway, Suite 120, Atlanta, Georgia 30354, " & vbCrLf & "(404) 363-7000" & vbCrLf & vbCrLf & vbCrLf

        TVAdvisories = "NOTICE OF DRAFT TITLE V OPERATING PERMITS AND PERMIT MODIFICATIONS " & vbCrLf &
            "GEORGIA ENVIRONMENTAL PROTECTION DIVISION " & vbCrLf &
            "AIR PROTECTION BRANCHX" & vbCrLf &
            "4244 INTERNATIONAL PARKWAY, SUITE 120, ATLANTA, GA 30354 " & vbCrLf & vbCrLf &
            "The Georgia Environmental Protection Division announces its intent to " & vbCrLf &
            "issue initial Title V Operating Permits, Title V Significant " & vbCrLf &
            "modifications, Title V Operating Permit Renewals, and/or other Title V " & vbCrLf &
            "Permit proceedings for the following facilities. The deadlines for " & vbCrLf &
            "submitting comments and requesting a public hearing are specified for " & vbCrLf & "each facility. " & vbCrLf & vbCrLf

        If TVInitial <> "" OrElse TVRenewal <> "" OrElse TVSigMod <> "" Then
            If TVInitial <> "" Then
                TVAdvisories = TVAdvisories & "INITIAL TITLE V OPERATING PERMITSX" & vbCrLf & vbCrLf &
                    TVInitial & vbCrLf
            End If
            If TVRenewal <> "" Then
                TVAdvisories = TVAdvisories & "RENEWAL TITLE V OPERATING PERMITSX" & vbCrLf & vbCrLf &
                    TVRenewal & vbCrLf
            End If
            If TVSigMod <> "" Then
                TVAdvisories = TVAdvisories & "TITLE V SIGNIFICANT MODIFICATIONSX" & vbCrLf & vbCrLf &
                    TVSigMod & vbCrLf
            End If
        Else
            TVAdvisories = TVAdvisories & "NO TITLE V ADVISORIESX" & vbCrLf & vbCrLf
        End If

        TVAdvisories = TVAdvisories & "ADDITIONAL INFORMATIONX: The draft permits and permit amendments and " & vbCrLf &
            "all information used to develop the draft permits and permit amendments " & vbCrLf &
            "are available for review. This includes the application, all relevant " & vbCrLf &
            "supporting materials and all other materials available to the permitting " & vbCrLf &
            "authority used in the permit review process. This information is " & vbCrLf &
            "available for review at the office of the Air Protection Branch, " & vbCrLf &
            "4244 International Parkway, Atlanta Tradeport - Suite 120, Atlanta, Georgia 30354. " & vbCrLf &
            "Copies of the draft permits or permit amendments, narratives, " & vbCrLf &
            "application summaries, and (in most cases) permit applications are also " & vbCrLf &
            "available at our Internet site, https://epd.georgia.gov/. Also " & vbCrLf &
            "available at this Internet site is a copy of the public notice, as it " & vbCrLf &
            "will appear in the legal organ of the county where the facility is " & vbCrLf &
            "located. " & vbCrLf & vbCrLf &
            "If a permit application is not available at our Internet site, the " & vbCrLf &
            "public notice will indicate where a copy of these documents will be " & vbCrLf &
            "available at a location near the facility. " & vbCrLf & vbCrLf &
            "Persons wishing to comment on a draft Initial Title V Operating Permit, " & vbCrLf &
            "Title V Significant modification, Title V Operating Permit Renewal, or " & vbCrLf &
            "other Title V Permit proceedings are required to submit their comments, " & vbCrLf &
            "in writing, to EPD at the above Atlanta Air Protection Branch address. " & vbCrLf &
            "Comments must be received by no later than the deadline indicated for " & vbCrLf &
            "the particular facility. (Should the comment period end on a weekend or " & vbCrLf &
            "holiday, comments will be accepted up until the next working day.) All " & vbCrLf &
            "comments received on or prior to the deadline will be considered by the " & vbCrLf &
            "Division in making its final decision to issue the Title V permit or " & vbCrLf &
            "permit amendment." & vbCrLf & vbCrLf &
            "Any requests for a public hearing must be made prior to the deadline " & vbCrLf &
            "indicated for the particular facility. A request for a hearing should " & vbCrLf &
            "be in writing and should specify, in as much detail as possible, the " & vbCrLf &
            "portion of the Georgia Rules for Air Quality Control or the Federal " & vbCrLf &
            "Rules which the individual making the request is concerned may not have " & vbCrLf &
            "been adequately incorporated. A public hearing may be held if the " & vbCrLf &
            "Director of the EPD finds that such a hearing would assist the EPD in a " & vbCrLf &
            "proper review of the facility's ability to comply with the Federal and " & vbCrLf &
            "State air quality regulations. " & vbCrLf & vbCrLf &
            "For additional information, contact Eric Cornwell, Program Manager, " & vbCrLf &
            "Stationary Source Permitting Program, Air Protection Branch, " & vbCrLf &
            "4244 International Parkway, Suite 120, " & vbCrLf &
            "Atlanta, Georgia 30354, (404) 363-7000" & vbCrLf & vbCrLf & vbCrLf &
            "--------------------------------------------------" & vbCrLf

        rtbPreview.Text = PublicAdvisories & TVAdvisories

        FormatReport()

        btnPublishDocument.Enabled = True
    End Sub

    Private Sub FormatReport()
        Using bfont As New Font(rtbPreview.Font, FontStyle.Bold),
            ufont As New Font(rtbPreview.Font, FontStyle.Underline)

            Dim tempStart As Integer
            Dim tempEnd As Integer
            Dim temp As String
            Dim temp2 As String

            Do While rtbPreview.Text.Contains("*X")
                rtbPreview.SelectionStart = rtbPreview.Find("*X")
                tempStart = rtbPreview.Find("*X")
                tempEnd = rtbPreview.Find("X*")
                temp = Mid(rtbPreview.Text, tempStart + 1, (tempEnd - tempStart) + 2)
                temp2 = Replace(temp, "*X", "")
                temp2 = Replace(temp2, "X*", "")
                rtbPreview.SelectionStart = rtbPreview.Find(temp)
                rtbPreview.SelectionFont = ufont
                rtbPreview.SelectedText = temp2
            Loop

            If rtbPreview.Text.Contains("EPD PUBLIC ADVISORY") Then
                rtbPreview.SelectionStart = rtbPreview.Find("EPD PUBLIC ADVISORY")
                rtbPreview.SelectionAlignment = HorizontalAlignment.Center
                rtbPreview.SelectionFont = bfont
            End If

            If rtbPreview.Text.Contains("GEORGIA AIR PROTECTION BRANCH") Then
                rtbPreview.SelectionStart = rtbPreview.Find("GEORGIA AIR PROTECTION BRANCH")
                rtbPreview.SelectionAlignment = HorizontalAlignment.Center
                rtbPreview.SelectionFont = bfont
            End If

            If rtbPreview.Text.Contains("SIP PUBLIC ADVISORIES") Then
                rtbPreview.SelectionStart = rtbPreview.Find("SIP PUBLIC ADVISORIES")
                rtbPreview.SelectionFont = bfont
            End If

            If rtbPreview.Text.Contains("Any comments should be received by") Then
                rtbPreview.SelectionStart = rtbPreview.Find("Any comments should be received by")
                rtbPreview.SelectionFont = bfont
            End If

            If rtbPreview.Text.Contains("NO PUBLIC ADVISORIESX") Then
                rtbPreview.SelectionStart = rtbPreview.Find("NO PUBLIC ADVISORIESX")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "NO PUBLIC ADVISORIES"
            End If

            Do While rtbPreview.Text.Contains("Facility Name:X")
                rtbPreview.SelectionStart = rtbPreview.Find("Facility Name:X")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "Facility Name:"
            Loop

            Do While rtbPreview.Text.Contains("Application No:X")
                rtbPreview.SelectionStart = rtbPreview.Find("Application No:X")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "Application No:"
            Loop

            Do While rtbPreview.Text.Contains("Facility Address:X")
                rtbPreview.SelectionStart = rtbPreview.Find("Facility Address:X")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "Facility Address:"
            Loop

            Do While rtbPreview.Text.Contains("EPD Notice Type:X")
                rtbPreview.SelectionStart = rtbPreview.Find("EPD Notice Type:X")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "EPD Notice Type:"
            Loop

            Do While rtbPreview.Text.Contains("Description of Operation:X")
                rtbPreview.SelectionStart = rtbPreview.Find("Description of Operation:X")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "Description of Operation:"
            Loop

            Do While rtbPreview.Text.Contains("Reason for Application:X")
                rtbPreview.SelectionStart = rtbPreview.Find("Reason for Application:X")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "Reason for Application:"
            Loop

            Do While rtbPreview.Text.Contains("Comment period/deadline for public hearing request expires on:X")
                rtbPreview.SelectionStart = rtbPreview.Find("Comment period/deadline for public hearing request expires on:X")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "Comment period/deadline for public hearing request expires on:"
            Loop

            Do While rtbPreview.Text.Contains("Description of Requested Modification/Change:X")
                rtbPreview.SelectionStart = rtbPreview.Find("Description of Requested Modification/Change:X")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "Description of Requested Modification/Change:"
            Loop

            Do While rtbPreview.Text.Contains("Emission Increase/Decrease:X")
                rtbPreview.SelectionStart = rtbPreview.Find("Emission Increase/Decrease:X")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "Emission Increase/Decrease:"
            Loop

            If rtbPreview.Text.Contains("NOTICE OF DRAFT TITLE V OPERATING PERMITS AND PERMIT MODIFICATIONS") Then
                rtbPreview.SelectionStart = rtbPreview.Find("NOTICE OF DRAFT TITLE V OPERATING PERMITS AND PERMIT MODIFICATIONS")
                rtbPreview.SelectionAlignment = HorizontalAlignment.Center
                rtbPreview.SelectionFont = bfont
            End If

            If rtbPreview.Text.Contains("GEORGIA ENVIRONMENTAL PROTECTION DIVISION") Then
                rtbPreview.SelectionStart = rtbPreview.Find("GEORGIA ENVIRONMENTAL PROTECTION DIVISION")
                rtbPreview.SelectionAlignment = HorizontalAlignment.Center
                rtbPreview.SelectionFont = bfont
            End If

            Do While rtbPreview.Text.Contains("AIR PROTECTION BRANCHX")
                rtbPreview.SelectionStart = rtbPreview.Find("AIR PROTECTION BRANCHX")
                rtbPreview.SelectionAlignment = HorizontalAlignment.Center
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "AIR PROTECTION BRANCH"
            Loop

            If rtbPreview.Text.Contains("4244 INTERNATIONAL PARKWAY, SUITE 120, ATLANTA, GA 30354") Then
                rtbPreview.SelectionStart = rtbPreview.Find("4244 INTERNATIONAL PARKWAY, SUITE 120, ATLANTA, GA 30354")
                rtbPreview.SelectionAlignment = HorizontalAlignment.Center
                rtbPreview.SelectionFont = bfont
            End If

            If rtbPreview.Text.Contains("INITIAL TITLE V OPERATING PERMITS") Then
                rtbPreview.SelectionStart = rtbPreview.Find("INITIAL TITLE V OPERATING PERMITSX")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "INITIAL TITLE V OPERATING PERMITS"
            End If

            If rtbPreview.Text.Contains("RENEWAL TITLE V OPERATING PERMITS") Then
                rtbPreview.SelectionStart = rtbPreview.Find("RENEWAL TITLE V OPERATING PERMITSX")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "RENEWAL TITLE V OPERATING PERMITS"
            End If

            If rtbPreview.Text.Contains("TITLE V SIGNIFICANT MODIFICATIONS") Then
                rtbPreview.SelectionStart = rtbPreview.Find("TITLE V SIGNIFICANT MODIFICATIONSX")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "TITLE V SIGNIFICANT MODIFICATIONS"
            End If

            If rtbPreview.Text.Contains("NO TITLE V ADVISORIES") Then
                rtbPreview.SelectionStart = rtbPreview.Find("NO TITLE V ADVISORIESX")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "NO TITLE V ADVISORIES"
            End If

            If rtbPreview.Text.Contains("ADDITIONAL INFORMATIONX:") Then
                rtbPreview.SelectionStart = rtbPreview.Find("ADDITIONAL INFORMATIONX:")
                rtbPreview.SelectionFont = bfont
                rtbPreview.SelectedText = "ADDITIONAL INFORMATION:"
            End If

        End Using
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

        Do While Flag = False
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

        Dim Encoder As New Text.ASCIIEncoding
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

    Private Sub ExportPDF(richTextDocument As String, fileName As String)
        If Not CrystalReportsIsAvailable() Then
            Exit Sub
        End If

        ' TODO: Request filename/location
        Using dialog As New SaveFileDialog() With {
            .Filter = "PDF Files (*.pdf)|*.pdf",
            .DefaultExt = ".pdf",
            .FileName = String.Concat(fileName, ".pdf"),
            .InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
        }

            If dialog.ShowDialog() = DialogResult.OK Then
                If Path.GetDirectoryName(dialog.FileName) <> dialog.InitialDirectory Then
                    SaveUserSetting(UserSetting.FileDownloadLocation, Path.GetDirectoryName(dialog.FileName))
                End If

                UseWaitCursor = True

                Dim rpt As New SSPPPublicNotice()
                rpt.SetParameterValue("PublicNotice", richTextDocument)
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, dialog.FileName)
                LogCrystalReportsUsage(rpt)
                Process.Start("explorer.exe", "/select,""" & dialog.FileName & """")

                UseWaitCursor = False
                rpt.Dispose()
            End If
        End Using
    End Sub

    Private Sub OpenExistingDocument()
        btnDownloadAsPdf.Enabled = False
        btnUpdateDocumentChanges.Enabled = False

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

            Using ms As MemoryStream = New MemoryStream(OpenedDocument.GetFileData())
                rtbDocument.LoadFile(ms, RichTextBoxStreamType.RichText)
            End Using

            btnDownloadAsPdf.Enabled = True
            btnUpdateDocumentChanges.Enabled = True
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
            Exit Sub
        End If

        If Not rdbPublicAdvisories.Checked AndAlso Not rdbPublicNotice.Checked Then
            MessageBox.Show("Select either Public Advisory or Public Notice.", "Error")
            Exit Sub
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

            Exit Sub
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
            ExportPDF(rtbPreview.Rtf, newFileName)
            LoadOldPDFs()
            rtbDocument.Rtf = rtbPreview.Rtf
            btnDownloadAsPdf.Enabled = True
        Else
            MessageBox.Show("There was an error saving the document. Contact EPD-IT for assistance.", "Error")
        End If
    End Sub

    Private Sub btnOpenPAPN_Click(sender As Object, e As EventArgs) Handles btnOpenDocument.Click
        OpenExistingDocument()
    End Sub

    Private Sub btnViewOldPDFs_Click(sender As Object, e As EventArgs) Handles btnDownloadAsPdf.Click
        If OpenedDocument IsNot Nothing AndAlso Not String.IsNullOrEmpty(OpenedDocument.FileName) Then
            ExportPDF(rtbDocument.Rtf, OpenedDocument.FileName)
        End If
    End Sub

    'Private Sub btnUpdatePAPNChanges_Click(sender As Object, e As EventArgs) Handles btnUpdatePAPNChanges.Click
    '    If OpenedDocument Is Nothing Then
    '        Exit Sub
    '    End If

    '    With OpenedDocument
    '        .ReviewingManager = If(.ReviewingManager, CurrentUser.UserID)
    '        .PublishingStaff = If(.PublishingStaff, CurrentUser.UserID)
    '        .DateReviewed = If(.DateReviewed, Today)
    '        .DatePublished = If(.DateReviewed, Today)
    '        .CommentsDate = If(.DateReviewed, Today)
    '    End With

    '    Dim query As String = "update SSPPPUBLICLETTERS
    '        set DATPUBLISHEDDATE    = @DatePublished,
    '            DATCOMMENTSDATE     = @CommentsDate,
    '            DATREVIEWED         = @DateReviewed,
    '            STRREVIEWINGMANAGER = @ReviewingManager,
    '            STRPUBLISHINGSTAFF  = @PublishingStaff
    '        where STRFILENAME = @FileName"

    '    Dim params As SqlParameter() = {
    '        New SqlParameter("@DatePublished", OpenedDocument.DatePublished),
    '        New SqlParameter("@CommentsDate", OpenedDocument.CommentsDate),
    '        New SqlParameter("@DateReviewed", OpenedDocument.DateReviewed),
    '        New SqlParameter("@ReviewingManager", OpenedDocument.ReviewingManager),
    '        New SqlParameter("@PublishingStaff", OpenedDocument.PublishingStaff)
    '    }

    '    If DB.RunCommand(query, params) Then
    '        MessageBox.Show("Data updated.", "Success")
    '    Else
    '        MessageBox.Show("There was an error updating the data.", "Error")
    '    End If
    'End Sub

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

End Class