Imports Oracle.DataAccess.Client


Public Class PASPFeeVarianceCheck
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Panel1temp As String

    Dim dsWorkEntry As DataSet
    Dim daWorkEnTry As OracleDataAdapter
    Dim recExist As Boolean
    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader
    Friend WithEvents PanelFacility As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents dgrVariance As System.Windows.Forms.DataGrid
    Friend WithEvents lblVerified As System.Windows.Forms.LinkLabel
    Friend WithEvents lblNotVerified As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboFeeYear3 As System.Windows.Forms.ComboBox
    Friend WithEvents PanelVariance As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button

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
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TBFacilitySummary As System.Windows.Forms.ToolBar
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PASPFeeVarianceCheck))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem13 = New System.Windows.Forms.MenuItem
        Me.mmiExit = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.mmiClear = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBFacilitySummary = New System.Windows.Forms.ToolBar
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.PanelFacility = New System.Windows.Forms.Panel
        Me.cboFeeYear3 = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblNotVerified = New System.Windows.Forms.LinkLabel
        Me.lblVerified = New System.Windows.Forms.LinkLabel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.dgrVariance = New System.Windows.Forms.DataGrid
        Me.PanelVariance = New System.Windows.Forms.Panel
        Me.PanelFacility.SuspendLayout()
        CType(Me.dgrVariance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelVariance.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiBack, Me.MenuItem13, Me.mmiExit})
        Me.MenuItem1.Text = "File"
        '
        'mmiBack
        '
        Me.mmiBack.Index = 0
        Me.mmiBack.Text = "Back"
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 1
        Me.MenuItem13.Text = "-"
        '
        'mmiExit
        '
        Me.mmiExit.Index = 2
        Me.mmiExit.Text = "Exit"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste, Me.MenuItem6, Me.mmiClear})
        Me.MenuItem2.Text = "Edit"
        '
        'mmiCut
        '
        Me.mmiCut.Index = 0
        Me.mmiCut.Text = "Cut"
        '
        'mmiCopy
        '
        Me.mmiCopy.Index = 1
        Me.mmiCopy.Text = "Copy "
        '
        'mmiPaste
        '
        Me.mmiPaste.Index = 2
        Me.mmiPaste.Text = "Paste"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 3
        Me.MenuItem6.Text = "-"
        '
        'mmiClear
        '
        Me.mmiClear.Index = 4
        Me.mmiClear.Text = "Clear"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "View"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 3
        Me.mmiHelp.Text = "Help"
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
        'TBFacilitySummary
        '
        Me.TBFacilitySummary.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbBack})
        Me.TBFacilitySummary.DropDownArrows = True
        Me.TBFacilitySummary.ImageList = Me.Image_List_All
        Me.TBFacilitySummary.Location = New System.Drawing.Point(0, 0)
        Me.TBFacilitySummary.Name = "TBFacilitySummary"
        Me.TBFacilitySummary.ShowToolTips = True
        Me.TBFacilitySummary.Size = New System.Drawing.Size(969, 28)
        Me.TBFacilitySummary.TabIndex = 140
        '
        'tbbBack
        '
        Me.tbbBack.ImageIndex = 2
        Me.tbbBack.Name = "tbbBack"
        Me.tbbBack.ToolTipText = "Back"
        '
        'PanelFacility
        '
        Me.PanelFacility.Controls.Add(Me.cboFeeYear3)
        Me.PanelFacility.Controls.Add(Me.Label1)
        Me.PanelFacility.Controls.Add(Me.lblNotVerified)
        Me.PanelFacility.Controls.Add(Me.lblVerified)
        Me.PanelFacility.Controls.Add(Me.btnCancel)
        Me.PanelFacility.Controls.Add(Me.btnSave)
        Me.PanelFacility.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFacility.Location = New System.Drawing.Point(0, 0)
        Me.PanelFacility.Name = "PanelFacility"
        Me.PanelFacility.Size = New System.Drawing.Size(969, 31)
        Me.PanelFacility.TabIndex = 271
        '
        'cboFeeYear3
        '
        Me.cboFeeYear3.FormattingEnabled = True
        Me.cboFeeYear3.Items.AddRange(New Object() {"2005", "2006"})
        Me.cboFeeYear3.Location = New System.Drawing.Point(57, 1)
        Me.cboFeeYear3.Name = "cboFeeYear3"
        Me.cboFeeYear3.Size = New System.Drawing.Size(66, 21)
        Me.cboFeeYear3.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Fee Year:"
        '
        'lblNotVerified
        '
        Me.lblNotVerified.AutoSize = True
        Me.lblNotVerified.Location = New System.Drawing.Point(257, 5)
        Me.lblNotVerified.Name = "lblNotVerified"
        Me.lblNotVerified.Size = New System.Drawing.Size(89, 13)
        Me.lblNotVerified.TabIndex = 3
        Me.lblNotVerified.TabStop = True
        Me.lblNotVerified.Text = "Load Not Verified"
        '
        'lblVerified
        '
        Me.lblVerified.AutoSize = True
        Me.lblVerified.Location = New System.Drawing.Point(144, 5)
        Me.lblVerified.Name = "lblVerified"
        Me.lblVerified.Size = New System.Drawing.Size(107, 13)
        Me.lblVerified.TabIndex = 2
        Me.lblVerified.TabStop = True
        Me.lblVerified.Text = "Load Already Verified"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(517, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(58, 20)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(389, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(59, 20)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Update"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'dgrVariance
        '
        Me.dgrVariance.DataMember = ""
        Me.dgrVariance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrVariance.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrVariance.Location = New System.Drawing.Point(0, 31)
        Me.dgrVariance.Name = "dgrVariance"
        Me.dgrVariance.ReadOnly = True
        Me.dgrVariance.Size = New System.Drawing.Size(969, 523)
        Me.dgrVariance.TabIndex = 272
        '
        'PanelVariance
        '
        Me.PanelVariance.Controls.Add(Me.dgrVariance)
        Me.PanelVariance.Controls.Add(Me.PanelFacility)
        Me.PanelVariance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelVariance.Location = New System.Drawing.Point(0, 28)
        Me.PanelVariance.Name = "PanelVariance"
        Me.PanelVariance.Size = New System.Drawing.Size(969, 554)
        Me.PanelVariance.TabIndex = 273
        '
        'PASPFeeVarianceCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(969, 582)
        Me.Controls.Add(Me.PanelVariance)
        Me.Controls.Add(Me.TBFacilitySummary)
        Me.Menu = Me.MainMenu1
        Me.Name = "PASPFeeVarianceCheck"
        Me.Text = "PASP Fee Variance Check"
        Me.PanelFacility.ResumeLayout(False)
        Me.PanelFacility.PerformLayout()
        CType(Me.dgrVariance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelVariance.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub PASPFeeVarianceCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            FormatDataGridForWorkEnTry()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

    Sub CreateStatusBar()
        Try

            panel1.Text = "..."
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
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Sub LoadDataGridNotVerified()
        FormatDataGridForWorkEnTry()
        Dim SQL As String = ""
        Try

            dsWorkEntry = New DataSet
            If Not dsWorkEntry.Tables("tblworkentry") Is Nothing Then
                dsWorkEntry.Tables("tblworkentry").Clear()
                dsWorkEntry.Tables.Remove("tblworkentry")
                dsWorkEntry.AcceptChanges()
            End If

            Select Case cboFeeYear3.Text
                Case 2005
                    SQL = "Select strairsnumber, Difference2005, Fee2004, Fee2005, " _
                        + "Vcheck2005, comments2005 from airbranch.feevariance " _
                        + "where Difference2005 <> 0 and vcheck2005 = 'NO'"

                Case 2006
                    SQL = "Select strairsnumber, Difference2006, Fee2005, Fee2006, " _
                        + "Vcheck2006, comments2006 from airbranch.feevariance " _
                        + "where Difference2006 <> 0 and vcheck2006 = 'NO'"
            End Select

            daWorkEnTry = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daWorkEnTry.Fill(dsWorkEntry, "tblWorkEnTry")

            dgrVariance.DataSource = dsWorkEntry
            dgrVariance.DataMember = "tblWorkEntry"

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try



    End Sub

    Sub LoadDataGridVerified()
        FormatDataGridForWorkEnTry()
        Dim SQL As String = ""
        Try

            dsWorkEntry = New DataSet

            If Not dsWorkEntry.Tables("tblworkentry") Is Nothing Then
                dsWorkEntry.Tables("tblworkentry").Clear()
                dsWorkEntry.Tables.Remove("tblworkentry")
                dsWorkEntry.AcceptChanges()
            End If

            Select Case cboFeeYear3.Text
                Case 2005
                    SQL = "Select strairsnumber, Difference2005, Fee2004, Fee2005, " _
                        + "Vcheck2005, comments2005 from airbranch.feevariance " _
                        + "where Difference2005 <> 0 and vcheck2005 = 'YES'"

                Case 2006
                    SQL = "Select strairsnumber, Difference2006, Fee2005, Fee2006, " _
                        + "Vcheck2006, comments2006 from airbranch.feevariance " _
                        + "where Difference2006 <> 0 and vcheck2006 = 'YES'"
            End Select

            daWorkEnTry = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daWorkEnTry.Fill(dsWorkEntry, "tblWorkEnTry")

            dgrVariance.DataSource = dsWorkEntry
            dgrVariance.DataMember = "tblWorkEntry"

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
            FormatDataGridForWorkEnTry()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

    Sub FormatDataGridForWorkEnTry()
        Select Case cboFeeYear3.Text
            Case 2005
                Try

                    'Formatting our DataGrid
                    Dim objGrid As New DataGridTableStyle
                    Dim objtextcol As New DataGridTextBoxColumn
                    Dim objbooleancol As New DataGridBoolColumn

                    objGrid.AlternatingBackColor = Color.WhiteSmoke
                    objGrid.MappingName = "tblWorkEntry"
                    objGrid.RowHeadersVisible = False
                    objGrid.AllowSorting = True

                    'Formatting our DataGrid 0
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "strairsnumber"
                    objtextcol.HeaderText = "AIRS Number"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = True
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 1
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "Fee2004"
                    objtextcol.HeaderText = "2004 Fee"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = True
                    objtextcol.Format = "C" 'Format for Currency
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 2
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "Fee2005"
                    objtextcol.HeaderText = "2005 Fee"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = True
                    objtextcol.Format = "C" 'Format for Currency
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 3
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "Difference2005"
                    objtextcol.HeaderText = "Difference"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = True
                    objtextcol.Format = "C" 'Format for Currency
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 4
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "vcheck2005"
                    objtextcol.HeaderText = "Verified?"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = False
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 5
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "comments2005"
                    objtextcol.HeaderText = "Comments"
                    objtextcol.Width = 500
                    objtextcol.ReadOnly = False
                    objGrid.GridColumnStyles.Add(objtextcol)


                    'Applying the above formating 
                    dgrVariance.TableStyles.Clear()
                    dgrVariance.TableStyles.Add(objGrid)

                    'Setting the DataGrid Caption, which defines the table title
                    dgrVariance.CaptionText = "Variance Checking Tool for 2005"
                    dgrVariance.ColumnHeadersVisible = True

                Catch ex As Exception
                    ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
                Finally
                    If CurrentConnection.State = ConnectionState.Open Then
                        'conn.close()
                    End If
                End Try
            Case 2006
                Try

                    'Formatting our DataGrid
                    Dim objGrid As New DataGridTableStyle
                    Dim objtextcol As New DataGridTextBoxColumn
                    Dim objbooleancol As New DataGridBoolColumn

                    objGrid.AlternatingBackColor = Color.WhiteSmoke
                    objGrid.MappingName = "tblWorkEntry"
                    objGrid.RowHeadersVisible = False
                    objGrid.AllowSorting = True

                    'Formatting our DataGrid 0
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "strairsnumber"
                    objtextcol.HeaderText = "AIRS Number"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = True
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 1
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "Fee2005"
                    objtextcol.HeaderText = "2005 Fee"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = True
                    objtextcol.Format = "C" 'Format for Currency
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 2
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "Fee2006"
                    objtextcol.HeaderText = "2006 Fee"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = True
                    objtextcol.Format = "C" 'Format for Currency
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 3
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "Difference2006"
                    objtextcol.HeaderText = "Difference"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = True
                    objtextcol.Format = "C" 'Format for Currency
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 4
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "vcheck2006"
                    objtextcol.HeaderText = "Verified?"
                    objtextcol.Width = 100
                    objtextcol.ReadOnly = False
                    objGrid.GridColumnStyles.Add(objtextcol)

                    'Formatting our DataGrid 5
                    objtextcol = New DataGridTextBoxColumn
                    objtextcol.MappingName = "comments2006"
                    objtextcol.HeaderText = "Comments"
                    objtextcol.Width = 500
                    objtextcol.ReadOnly = False
                    objGrid.GridColumnStyles.Add(objtextcol)


                    'Applying the above formating 
                    dgrVariance.TableStyles.Clear()
                    dgrVariance.TableStyles.Add(objGrid)

                    'Setting the DataGrid Caption, which defines the table title
                    dgrVariance.CaptionText = "Variance Checking Tool for 2006"
                    dgrVariance.ColumnHeadersVisible = True

                Catch ex As Exception
                    ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
                Finally
                    If CurrentConnection.State = ConnectionState.Open Then
                        'conn.close()
                    End If
                End Try
        End Select


    End Sub

    Private Sub TBFacilitySummary_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFacilitySummary.ButtonClick
        Try

            Select Case TBFacilitySummary.Buttons.IndexOf(e.Button)
                Case 0
                    Me.Close()
                Case Else
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub PASPFeeVarianceCheck_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            FeeDeposits = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim dt As DataTable
            Dim airsno As String
            Dim varcheck As String
            Dim comments As String
            dt = dsWorkEntry.Tables("tblWorkEntry").GetChanges
            If dt Is Nothing Then
            Else
                Dim Row As DataRow
                Dim intColumn As Integer
                For Each Row In dt.Rows
                    Select Case Row.RowState
                        'Case DataRowState.Added
                        '    blnDataChanged = True
                        'Case DataRowState.Deleted
                        '    blnDataChanged = True
                        Case DataRowState.Modified
                            For intColumn = 4 To 5
                                If Not IsDBNull(Row(intColumn, DataRowVersion.Original)) And Not IsDBNull(Row(intColumn, DataRowVersion.Current)) Then
                                    If Row(intColumn, DataRowVersion.Original) <> Row(intColumn, DataRowVersion.Current) Then
                                        airsno = "0413" & Row(0, DataRowVersion.Original).ToString
                                        varcheck = Row(4, DataRowVersion.Current).ToString
                                        comments = Row(5, DataRowVersion.Current).ToString
                                        UpdateRecords(airsno, varcheck, comments)
                                        'Exit For
                                    End If
                                End If
                            Next
                    End Select
                Next
            End If
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")
            dgrVariance.DataSource = Nothing
            dgrVariance.Refresh()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub UpdateRecords(ByVal airsno As String, ByVal varcheck As String, ByVal comments As String)

        Try


            SQL = "Update " & DBNameSpace & ".FSCalculations set " _
            + "variancecheck = '" & UCase(varcheck) & "', " _
            + "variancecomments = '" & Replace(comments, "'", "''") & "' " _
            + "where strairsnumber = '" & airsno & "' and " _
            + "intyear = '" & CInt(cboFeeYear3.Text) & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

    Private Sub lblVerified_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblVerified.LinkClicked
        Try

            LoadDataGridVerified()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub lblNotVerified_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblNotVerified.LinkClicked
        Try

            LoadDataGridNotVerified()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub


    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try
            Help.ShowHelp(Label1, HelpUrl)
        Catch ex As Exception
        End Try

    End Sub
End Class
