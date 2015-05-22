Imports Oracle.DataAccess.Client

'Imports Microsoft.Office.Interop

Public Class SSCPInspectionscheduleLink
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Panel1temp As String

    Dim SQL, SQL2 As String
    Dim cmd, cmd2 As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim recExist As Boolean
    Dim dsInspectionSchedule As DataSet
    Dim daInspectionschedule As OracleDataAdapter


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents TBSSCPInspectionScheduleLink As System.Windows.Forms.ToolBar
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents txtInspectionTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtInspectionNumber As System.Windows.Forms.TextBox
    Friend WithEvents PanelInspectionSchedule As System.Windows.Forms.Panel
    Friend WithEvents dgrScheduledInspections As System.Windows.Forms.DataGrid
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtScheduleEndDate As System.Windows.Forms.TextBox
    Friend WithEvents txtScheduleStartDate As System.Windows.Forms.TextBox
    Friend WithEvents lblLinkInspections As System.Windows.Forms.LinkLabel
    Friend WithEvents llbAllScheduledInspections As System.Windows.Forms.LinkLabel
    Friend WithEvents llbUnlinkedInspections As System.Windows.Forms.LinkLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSCPInspectionscheduleLink))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtInspectionTrackingNumber = New System.Windows.Forms.TextBox
        Me.TBSSCPInspectionScheduleLink = New System.Windows.Forms.ToolBar
        Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MmiFile = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.PanelInspectionSchedule = New System.Windows.Forms.Panel
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.txtInspectionNumber = New System.Windows.Forms.TextBox
        Me.txtScheduleEndDate = New System.Windows.Forms.TextBox
        Me.txtScheduleStartDate = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblLinkInspections = New System.Windows.Forms.LinkLabel
        Me.llbAllScheduledInspections = New System.Windows.Forms.LinkLabel
        Me.llbUnlinkedInspections = New System.Windows.Forms.LinkLabel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.dgrScheduledInspections = New System.Windows.Forms.DataGrid
        Me.PanelInspectionSchedule.SuspendLayout()
        CType(Me.dgrScheduledInspections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Inspection Tracking Number:"
        '
        'txtInspectionTrackingNumber
        '
        Me.txtInspectionTrackingNumber.Location = New System.Drawing.Point(176, 16)
        Me.txtInspectionTrackingNumber.Name = "txtInspectionTrackingNumber"
        Me.txtInspectionTrackingNumber.ReadOnly = True
        Me.txtInspectionTrackingNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtInspectionTrackingNumber.TabIndex = 1
        '
        'TBSSCPInspectionScheduleLink
        '
        Me.TBSSCPInspectionScheduleLink.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.ToolBarButton4})
        Me.TBSSCPInspectionScheduleLink.DropDownArrows = True
        Me.TBSSCPInspectionScheduleLink.ImageList = Me.Image_List_All
        Me.TBSSCPInspectionScheduleLink.Location = New System.Drawing.Point(0, 0)
        Me.TBSSCPInspectionScheduleLink.Name = "TBSSCPInspectionScheduleLink"
        Me.TBSSCPInspectionScheduleLink.ShowToolTips = True
        Me.TBSSCPInspectionScheduleLink.Size = New System.Drawing.Size(448, 28)
        Me.TBSSCPInspectionScheduleLink.TabIndex = 247
        '
        'ToolBarButton4
        '
        Me.ToolBarButton4.ImageIndex = 2
        Me.ToolBarButton4.Name = "ToolBarButton4"
        '
        'Image_List_All
        '
        Me.Image_List_All.ImageStream = CType(resources.GetObject("Image_List_All.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Image_List_All.TransparentColor = System.Drawing.Color.Transparent
        Me.Image_List_All.Images.SetKeyName(0, "")
        Me.Image_List_All.Images.SetKeyName(1, "")
        Me.Image_List_All.Images.SetKeyName(2, "")
        Me.Image_List_All.Images.SetKeyName(3, "")
        Me.Image_List_All.Images.SetKeyName(4, "")
        Me.Image_List_All.Images.SetKeyName(5, "")
        Me.Image_List_All.Images.SetKeyName(6, "")
        Me.Image_List_All.Images.SetKeyName(7, "")
        Me.Image_List_All.Images.SetKeyName(8, "")
        Me.Image_List_All.Images.SetKeyName(9, "")
        Me.Image_List_All.Images.SetKeyName(10, "")
        Me.Image_List_All.Images.SetKeyName(11, "")
        Me.Image_List_All.Images.SetKeyName(12, "")
        Me.Image_List_All.Images.SetKeyName(13, "")
        Me.Image_List_All.Images.SetKeyName(14, "")
        Me.Image_List_All.Images.SetKeyName(15, "")
        Me.Image_List_All.Images.SetKeyName(16, "")
        Me.Image_List_All.Images.SetKeyName(17, "")
        Me.Image_List_All.Images.SetKeyName(18, "")
        Me.Image_List_All.Images.SetKeyName(19, "")
        Me.Image_List_All.Images.SetKeyName(20, "")
        Me.Image_List_All.Images.SetKeyName(21, "")
        Me.Image_List_All.Images.SetKeyName(22, "")
        Me.Image_List_All.Images.SetKeyName(23, "")
        Me.Image_List_All.Images.SetKeyName(24, "")
        Me.Image_List_All.Images.SetKeyName(25, "")
        Me.Image_List_All.Images.SetKeyName(26, "")
        Me.Image_List_All.Images.SetKeyName(27, "")
        Me.Image_List_All.Images.SetKeyName(28, "")
        Me.Image_List_All.Images.SetKeyName(29, "")
        Me.Image_List_All.Images.SetKeyName(30, "")
        Me.Image_List_All.Images.SetKeyName(31, "")
        Me.Image_List_All.Images.SetKeyName(32, "")
        Me.Image_List_All.Images.SetKeyName(33, "")
        Me.Image_List_All.Images.SetKeyName(34, "")
        Me.Image_List_All.Images.SetKeyName(35, "")
        Me.Image_List_All.Images.SetKeyName(36, "")
        Me.Image_List_All.Images.SetKeyName(37, "")
        Me.Image_List_All.Images.SetKeyName(38, "")
        Me.Image_List_All.Images.SetKeyName(39, "")
        Me.Image_List_All.Images.SetKeyName(40, "")
        Me.Image_List_All.Images.SetKeyName(41, "")
        Me.Image_List_All.Images.SetKeyName(42, "")
        Me.Image_List_All.Images.SetKeyName(43, "")
        Me.Image_List_All.Images.SetKeyName(44, "")
        Me.Image_List_All.Images.SetKeyName(45, "")
        Me.Image_List_All.Images.SetKeyName(46, "")
        Me.Image_List_All.Images.SetKeyName(47, "")
        Me.Image_List_All.Images.SetKeyName(48, "")
        Me.Image_List_All.Images.SetKeyName(49, "")
        Me.Image_List_All.Images.SetKeyName(50, "")
        Me.Image_List_All.Images.SetKeyName(51, "")
        Me.Image_List_All.Images.SetKeyName(52, "")
        Me.Image_List_All.Images.SetKeyName(53, "")
        Me.Image_List_All.Images.SetKeyName(54, "")
        Me.Image_List_All.Images.SetKeyName(55, "")
        Me.Image_List_All.Images.SetKeyName(56, "")
        Me.Image_List_All.Images.SetKeyName(57, "")
        Me.Image_List_All.Images.SetKeyName(58, "")
        Me.Image_List_All.Images.SetKeyName(59, "")
        Me.Image_List_All.Images.SetKeyName(60, "")
        Me.Image_List_All.Images.SetKeyName(61, "")
        Me.Image_List_All.Images.SetKeyName(62, "")
        Me.Image_List_All.Images.SetKeyName(63, "")
        Me.Image_List_All.Images.SetKeyName(64, "")
        Me.Image_List_All.Images.SetKeyName(65, "")
        Me.Image_List_All.Images.SetKeyName(66, "")
        Me.Image_List_All.Images.SetKeyName(67, "")
        Me.Image_List_All.Images.SetKeyName(68, "")
        Me.Image_List_All.Images.SetKeyName(69, "")
        Me.Image_List_All.Images.SetKeyName(70, "")
        Me.Image_List_All.Images.SetKeyName(71, "")
        Me.Image_List_All.Images.SetKeyName(72, "")
        Me.Image_List_All.Images.SetKeyName(73, "")
        Me.Image_List_All.Images.SetKeyName(74, "")
        Me.Image_List_All.Images.SetKeyName(75, "")
        Me.Image_List_All.Images.SetKeyName(76, "")
        Me.Image_List_All.Images.SetKeyName(77, "")
        Me.Image_List_All.Images.SetKeyName(78, "")
        Me.Image_List_All.Images.SetKeyName(79, "")
        Me.Image_List_All.Images.SetKeyName(80, "")
        Me.Image_List_All.Images.SetKeyName(81, "")
        Me.Image_List_All.Images.SetKeyName(82, "")
        Me.Image_List_All.Images.SetKeyName(83, "")
        Me.Image_List_All.Images.SetKeyName(84, "")
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiFile, Me.MmiHelp})
        '
        'MmiFile
        '
        Me.MmiFile.Index = 0
        Me.MmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiBack})
        Me.MmiFile.Text = "File"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 0
        Me.MmiBack.Text = "Close"
        '
        'MmiHelp
        '
        Me.MmiHelp.Index = 1
        Me.MmiHelp.Text = "Help"
        '
        'PanelInspectionSchedule
        '
        Me.PanelInspectionSchedule.Controls.Add(Me.txtAIRSNumber)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtInspectionNumber)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtScheduleEndDate)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtScheduleStartDate)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label3)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label2)
        Me.PanelInspectionSchedule.Controls.Add(Me.lblLinkInspections)
        Me.PanelInspectionSchedule.Controls.Add(Me.llbAllScheduledInspections)
        Me.PanelInspectionSchedule.Controls.Add(Me.llbUnlinkedInspections)
        Me.PanelInspectionSchedule.Controls.Add(Me.txtInspectionTrackingNumber)
        Me.PanelInspectionSchedule.Controls.Add(Me.Label1)
        Me.PanelInspectionSchedule.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelInspectionSchedule.Location = New System.Drawing.Point(0, 28)
        Me.PanelInspectionSchedule.Name = "PanelInspectionSchedule"
        Me.PanelInspectionSchedule.Size = New System.Drawing.Size(448, 108)
        Me.PanelInspectionSchedule.TabIndex = 248
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(280, 16)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(16, 20)
        Me.txtAIRSNumber.TabIndex = 10
        Me.txtAIRSNumber.Visible = False
        '
        'txtInspectionNumber
        '
        Me.txtInspectionNumber.Location = New System.Drawing.Point(280, 40)
        Me.txtInspectionNumber.Name = "txtInspectionNumber"
        Me.txtInspectionNumber.ReadOnly = True
        Me.txtInspectionNumber.Size = New System.Drawing.Size(16, 20)
        Me.txtInspectionNumber.TabIndex = 9
        Me.txtInspectionNumber.Visible = False
        '
        'txtScheduleEndDate
        '
        Me.txtScheduleEndDate.Location = New System.Drawing.Point(176, 64)
        Me.txtScheduleEndDate.Name = "txtScheduleEndDate"
        Me.txtScheduleEndDate.ReadOnly = True
        Me.txtScheduleEndDate.Size = New System.Drawing.Size(100, 20)
        Me.txtScheduleEndDate.TabIndex = 8
        '
        'txtScheduleStartDate
        '
        Me.txtScheduleStartDate.Location = New System.Drawing.Point(176, 40)
        Me.txtScheduleStartDate.Name = "txtScheduleStartDate"
        Me.txtScheduleStartDate.ReadOnly = True
        Me.txtScheduleStartDate.Size = New System.Drawing.Size(100, 20)
        Me.txtScheduleStartDate.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Scheduled End Date: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Scheduled Start Date: "
        '
        'lblLinkInspections
        '
        Me.lblLinkInspections.AutoSize = True
        Me.lblLinkInspections.Location = New System.Drawing.Point(312, 24)
        Me.lblLinkInspections.Name = "lblLinkInspections"
        Me.lblLinkInspections.Size = New System.Drawing.Size(79, 13)
        Me.lblLinkInspections.TabIndex = 4
        Me.lblLinkInspections.TabStop = True
        Me.lblLinkInspections.Text = "Link Inspection"
        '
        'llbAllScheduledInspections
        '
        Me.llbAllScheduledInspections.AutoSize = True
        Me.llbAllScheduledInspections.Location = New System.Drawing.Point(240, 88)
        Me.llbAllScheduledInspections.Name = "llbAllScheduledInspections"
        Me.llbAllScheduledInspections.Size = New System.Drawing.Size(155, 13)
        Me.llbAllScheduledInspections.TabIndex = 3
        Me.llbAllScheduledInspections.TabStop = True
        Me.llbAllScheduledInspections.Text = "View All Scheduled Inspections"
        '
        'llbUnlinkedInspections
        '
        Me.llbUnlinkedInspections.AutoSize = True
        Me.llbUnlinkedInspections.Location = New System.Drawing.Point(24, 88)
        Me.llbUnlinkedInspections.Name = "llbUnlinkedInspections"
        Me.llbUnlinkedInspections.Size = New System.Drawing.Size(156, 13)
        Me.llbUnlinkedInspections.TabIndex = 2
        Me.llbUnlinkedInspections.TabStop = True
        Me.llbUnlinkedInspections.Text = "View Only Unlinked Inspections"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.Firebrick
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 136)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(448, 5)
        Me.Splitter1.TabIndex = 249
        Me.Splitter1.TabStop = False
        '
        'dgrScheduledInspections
        '
        Me.dgrScheduledInspections.DataMember = ""
        Me.dgrScheduledInspections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrScheduledInspections.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrScheduledInspections.Location = New System.Drawing.Point(0, 141)
        Me.dgrScheduledInspections.Name = "dgrScheduledInspections"
        Me.dgrScheduledInspections.ReadOnly = True
        Me.dgrScheduledInspections.Size = New System.Drawing.Size(448, 148)
        Me.dgrScheduledInspections.TabIndex = 250
        '
        'SSCPInspectionscheduleLink
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(448, 289)
        Me.Controls.Add(Me.dgrScheduledInspections)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.PanelInspectionSchedule)
        Me.Controls.Add(Me.TBSSCPInspectionScheduleLink)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPInspectionscheduleLink"
        Me.Text = "Compliance Inspection Schedule Link"
        Me.PanelInspectionSchedule.ResumeLayout(False)
        Me.PanelInspectionSchedule.PerformLayout()
        CType(Me.dgrScheduledInspections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub SSCPInspectionscheduleLink_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()

            FormatdgrScheduledInspections()

            LoadInspectionDataset(0)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub


#Region "Page Load Functions"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select a Function..."
            panel2.Text = UserName
            panel3.Text = OracleDate

            panel1.AutoSize = StatusBarPanelAutoSize.Spring
            panel2.AutoSize = StatusBarPanelAutoSize.Contents
            panel3.AutoSize = StatusBarPanelAutoSize.Contents

            panel1.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel2.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel3.BorderStyle = StatusBarPanelBorderStyle.Sunken

            panel1.Alignment = HorizontalAlignment.Left
            panel2.Alignment = HorizontalAlignment.Left
            panel3.Alignment = HorizontalAlignment.Right

            ' Display panels in the StatusBar control.
            statusBar1.ShowPanels = True

            ' Add both panels to the StatusBarPanelCollection of the StatusBar.            
            statusBar1.Panels.Add(panel1)
            statusBar1.Panels.Add(panel2)
            statusBar1.Panels.Add(panel3)

            ' Add the StatusBar to the form.
            Me.Controls.Add(statusBar1)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub CheckForLink()
        Try

            SQL = "Select InspectionKey, " & _
               "datScheduleDateStart, datScheduleDateEnd " & _
               "from AIRBRANCH.SSCPInspectionTracking " & _
               "where SSCPTrackingNumber = '" & txtInspectionTrackingNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then

                txtScheduleStartDate.Text = Format(dr.Item("datScheduleDateStart"), "dd-MMM-yyyy")
                txtScheduleEndDate.Text = Format(dr.Item("datScheduleDateEnd"), "dd-MMM-yyyy")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadInspectionDataset(ByVal Source As String)
        Try


            SQL = "Select InspectionKey, SSCPTrackingNumber, " & _
            "datScheduleDateStart, datScheduleDateEnd " & _
            "from AIRBRANCH.SSCPInspectionTracking " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            Select Case Source
                Case 0
                    SQL = SQL & "and sscpTrackingNumber is Null "
                Case 1
                    SQL = SQL & "and sscpTrackingNumber is not Null "
                Case Else
                    SQL = SQL & "and sscpTrackingNumber is Null "
            End Select



            dsInspectionSchedule = New DataSet

            daInspectionschedule = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daInspectionschedule.Fill(dsInspectionSchedule, "ScheduledInspections")
            dgrScheduledInspections.DataSource = dsInspectionSchedule
            dgrScheduledInspections.DataMember = "ScheduledInspections"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub FormatdgrScheduledInspections()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "ScheduledInspections"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            '0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "InspectionKey"
            objtextcol.HeaderText = "Inspection Key"
            objtextcol.Width = 0
            objGrid.GridColumnStyles.Add(objtextcol)

            '1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "SSCPTrackingNumber"
            objtextcol.HeaderText = "Tracking #"
            objtextcol.Width = 75
            objGrid.GridColumnStyles.Add(objtextcol)

            '2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "datScheduleDateStart"
            objtextcol.HeaderText = "Scheduled InspectionStart"
            objtextcol.Format = "dd-MMM-yyyy"
            objtextcol.Width = 250
            objGrid.GridColumnStyles.Add(objtextcol)

            '3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "datScheduleDateEnd"
            objtextcol.HeaderText = "Schedule Inspection End"
            objtextcol.Format = "dd-MMM-yyyy"
            objtextcol.Width = 250
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrScheduledInspections.TableStyles.Clear()
            dgrScheduledInspections.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrScheduledInspections.CaptionText = "Scheduled Inspections"
            dgrScheduledInspections.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

#End Region
#Region "Subs and Functions"
    Sub SaveScheduleLink()
        Dim temp As String

        Try

            If txtInspectionTrackingNumber.Text <> "" And txtInspectionNumber.Text <> "" Then
                SQL = "select Inspectionkey " & _
                "from AIRBRANCH.SSCPInspectionTracking " & _
                "where SSCPTrackingNumber = '" & txtInspectionTrackingNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    temp = dr.Item("inspectionKey")

                    SQL = "Update AIRBRANCH.SSCPInspectionTracking set " & _
                    "SSCPTrackingNumber = '' " & _
                    "where InspectionKey = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    Try

                        dr = cmd.ExecuteReader
                    Catch ex As Exception
                        MsgBox(ex.ToString())

                    End Try
                    ' 

                End If

                SQL = "Update AIRBRANCH.SSCPInspectionTracking set " & _
                "SSCPTrackingNumber = '" & txtInspectionTrackingNumber.Text & "' " & _
                "where InspectionKey = '" & txtInspectionNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                Try

                    dr = cmd.ExecuteReader
                Catch ex As Exception
                    MsgBox(ex.ToString())
                End Try
                ' 

                SQL = "Update AIRBRANCH.SSCPInspectionTracking set " & _
                "datActualDateStart = (Select datInspectionDateStart " & _
                "from AIRBRANCH.SSCPInspections where strTrackingNumber = '" & txtInspectionTrackingNumber.Text & "'), " & _
                "datActualDateEnd = " & _
                "(Select datInspectionDateEnd from AIRBRANCH.SSCPInspections " & _
                "where strTrackingNumber = '" & txtInspectionTrackingNumber.Text & "') " & _
                "where InspectionKey = '" & txtInspectionNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                Try

                    dr = cmd.ExecuteReader
                Catch ex As Exception
                    MsgBox(ex.ToString())
                End Try
                ' 



            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#End Region

    Private Sub dgrScheduledInspections_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrScheduledInspections.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrScheduledInspections.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrScheduledInspections(hti.Row, 0)) Then
                Else
                    'If IsDBNull(dgrScheduledInspections(hti.Row, 1)) Then
                    'Else
                    If IsDBNull(dgrScheduledInspections(hti.Row, 2)) Then
                    Else
                        If IsDBNull(dgrScheduledInspections(hti.Row, 3)) Then
                        Else
                            txtInspectionNumber.Clear()
                            txtScheduleStartDate.Clear()
                            txtScheduleEndDate.Clear()

                            txtInspectionNumber.Text = dgrScheduledInspections(hti.Row, 0)
                            txtScheduleStartDate.Text = Format(dgrScheduledInspections(hti.Row, 2), "dd-MMM-yyyy")
                            txtScheduleEndDate.Text = Format(dgrScheduledInspections(hti.Row, 3), "dd-MMM-yyyy")
                        End If
                    End If
                End If
                'End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub




    Private Sub lblLinkInspections_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblLinkInspections.LinkClicked
        Try

            SaveScheduleLink()
            SSCPReports.CheckforInspectionLink()
            LoadInspectionDataset(0)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub llbUnlinkedInspections_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbUnlinkedInspections.LinkClicked
        Try

            LoadInspectionDataset(0)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub llbAllScheduledInspections_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbAllScheduledInspections.LinkClicked
        Try

            LoadInspectionDataset(1)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class
