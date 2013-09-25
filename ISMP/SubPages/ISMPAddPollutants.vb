Imports Oracle.DataAccess.Client


Public Class ISMPAddPollutants
    Inherits DefaultForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Paneltemp1 As String
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsPollutant As DataSet
    Dim daPollutant As OracleDataAdapter

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
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents mmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents tbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPollutant As System.Windows.Forms.TextBox
    Friend WithEvents txtPollutantCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents llbGetNextValue As System.Windows.Forms.LinkLabel
    Friend WithEvents dgrPollutants As System.Windows.Forms.DataGrid
    Friend WithEvents chbDeletePollutant As System.Windows.Forms.CheckBox
    Friend WithEvents TBAddPollutant As System.Windows.Forms.ToolBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPAddPollutants))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiCut = New System.Windows.Forms.MenuItem
        Me.mmiCopy = New System.Windows.Forms.MenuItem
        Me.mmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem11 = New System.Windows.Forms.MenuItem
        Me.mmiClear = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.TBAddPollutant = New System.Windows.Forms.ToolBar
        Me.tbbSave = New System.Windows.Forms.ToolBarButton
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chbDeletePollutant = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPollutantCode = New System.Windows.Forms.TextBox
        Me.txtPollutant = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.llbGetNextValue = New System.Windows.Forms.LinkLabel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.dgrPollutants = New System.Windows.Forms.DataGrid
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgrPollutants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave, Me.MenuItem10, Me.MmiBack})
        Me.MenuItem1.Text = "File"
        '
        'MmiSave
        '
        Me.MmiSave.Index = 0
        Me.MmiSave.Text = "Save"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 1
        Me.MenuItem10.Text = "-"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 2
        Me.MmiBack.Text = "Back"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiCut, Me.mmiCopy, Me.mmiPaste, Me.MenuItem11, Me.mmiClear})
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
        Me.mmiCopy.Text = "Copy"
        '
        'mmiPaste
        '
        Me.mmiPaste.Index = 2
        Me.mmiPaste.Text = "Paste"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 3
        Me.MenuItem11.Text = "-"
        '
        'mmiClear
        '
        Me.mmiClear.Index = 4
        Me.mmiClear.Text = "Clear"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 2
        Me.mmiHelp.Text = "Help"
        '
        'TBAddPollutant
        '
        Me.TBAddPollutant.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSave, Me.tbbClear, Me.tbbBack})
        Me.TBAddPollutant.DropDownArrows = True
        Me.TBAddPollutant.ImageList = Me.Image_List_All
        Me.TBAddPollutant.Location = New System.Drawing.Point(0, 0)
        Me.TBAddPollutant.Name = "TBAddPollutant"
        Me.TBAddPollutant.ShowToolTips = True
        Me.TBAddPollutant.Size = New System.Drawing.Size(492, 28)
        Me.TBAddPollutant.TabIndex = 141
        '
        'tbbSave
        '
        Me.tbbSave.ImageIndex = 65
        Me.tbbSave.Name = "tbbSave"
        '
        'tbbClear
        '
        Me.tbbClear.ImageIndex = 84
        Me.tbbClear.Name = "tbbClear"
        '
        'tbbBack
        '
        Me.tbbBack.ImageIndex = 2
        Me.tbbBack.Name = "tbbBack"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chbDeletePollutant)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtPollutantCode)
        Me.GroupBox1.Controls.Add(Me.txtPollutant)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.llbGetNextValue)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(492, 76)
        Me.GroupBox1.TabIndex = 142
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pollutant Information"
        '
        'chbDeletePollutant
        '
        Me.chbDeletePollutant.Location = New System.Drawing.Point(72, 48)
        Me.chbDeletePollutant.Name = "chbDeletePollutant"
        Me.chbDeletePollutant.Size = New System.Drawing.Size(104, 16)
        Me.chbDeletePollutant.TabIndex = 145
        Me.chbDeletePollutant.Text = "Delete Pollutant"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(248, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Pollutant Code:"
        '
        'txtPollutantCode
        '
        Me.txtPollutantCode.Location = New System.Drawing.Point(336, 24)
        Me.txtPollutantCode.Name = "txtPollutantCode"
        Me.txtPollutantCode.Size = New System.Drawing.Size(100, 20)
        Me.txtPollutantCode.TabIndex = 2
        '
        'txtPollutant
        '
        Me.txtPollutant.Location = New System.Drawing.Point(72, 24)
        Me.txtPollutant.Name = "txtPollutant"
        Me.txtPollutant.Size = New System.Drawing.Size(168, 20)
        Me.txtPollutant.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pollutant:"
        '
        'llbGetNextValue
        '
        Me.llbGetNextValue.AutoSize = True
        Me.llbGetNextValue.Location = New System.Drawing.Point(336, 48)
        Me.llbGetNextValue.Name = "llbGetNextValue"
        Me.llbGetNextValue.Size = New System.Drawing.Size(77, 13)
        Me.llbGetNextValue.TabIndex = 144
        Me.llbGetNextValue.TabStop = True
        Me.llbGetNextValue.Text = "Get Next Code"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 104)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(492, 5)
        Me.Splitter1.TabIndex = 143
        Me.Splitter1.TabStop = False
        '
        'dgrPollutants
        '
        Me.dgrPollutants.DataMember = ""
        Me.dgrPollutants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrPollutants.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrPollutants.Location = New System.Drawing.Point(0, 109)
        Me.dgrPollutants.Name = "dgrPollutants"
        Me.dgrPollutants.ReadOnly = True
        Me.dgrPollutants.Size = New System.Drawing.Size(492, 236)
        Me.dgrPollutants.TabIndex = 144
        '
        'ISMPAddPollutants
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(492, 345)
        Me.Controls.Add(Me.dgrPollutants)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TBAddPollutant)
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPAddPollutants"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Add Pollutants"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgrPollutants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub ISMPAddPollutants_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            FormatdgrPollutants()
            LoadDataSet()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Page Load"
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub LoadDataSet()
        Try

            SQL = "Select strPollutantCode, strPOllutantDescription " & _
                 "from " & DBNameSpace & ".LookUPPollutants " & _
                 "Order by strPollutantDescription"

            dsPollutant = New DataSet

            daPollutant = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daPollutant.Fill(dsPollutant, "Pollutant")
            dgrPollutants.DataSource = dsPollutant
            dgrPollutants.DataMember = "Pollutant"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub FormatdgrPollutants()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Pollutant"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strPollutantCode"
            objtextcol.HeaderText = "Pollutant Code"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strPOllutantDescription"
            objtextcol.HeaderText = "Pollutant Description"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 400
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrPollutants.TableStyles.Clear()
            dgrPollutants.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrPollutants.CaptionText = "Pollutant(s)"
            dgrPollutants.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region
#Region "Functions and Subs"
    Sub Save()
        Try

            If txtPollutant.Text <> "" Then
                If txtPollutantCode.Text = "" Then
                    GetNextPollutantCode()
                End If
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                If chbDeletePollutant.Checked = True Then
                    SQL = "Delete " & DBNameSpace & ".LookUPPollutants " & _
                    "where strPollutantCode = '" & txtPollutantCode.Text & "' "
                Else
                    SQL = "Select strPollutantDescription " & _
                    "from " & DBNameSpace & ".LookUPPollutants " & _
                    "where strPollutantcode = '" & txtPollutantCode.Text & "' "

                    cmd = New OracleCommand(SQL, Conn)

                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        SQL = "Update " & DBNameSpace & ".LookUPPollutants set " & _
                        "strPollutantDescription = '" & txtPollutant.Text & "', " & _
                        "strPollutantCode = '" & txtPollutantCode.Text & "' " & _
                        "where strPollutantCode = '" & txtPollutantCode.Text & "' "
                    Else
                        SQL = "Insert into " & DBNameSpace & ".LookUPPollutants " & _
                        "(strPollutantCode, strPollutantDescription) " & _
                        "values " & _
                        "('" & txtPollutantCode.Text & "', '" & txtPollutant.Text & "') "
                    End If
                End If
                cmd = New OracleCommand(SQL, Conn)
                dr = cmd.ExecuteReader

                LoadDataSet()

                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            Else
                MsgBox("You must add a pollutant", MsgBoxStyle.Information, "ISMP Add Pollutant")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub Clear()
        Try

            txtPollutant.Clear()
            txtPollutantCode.Clear()
            chbDeletePollutant.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub Back()
        Try

            ISMPAddPollutant = Nothing
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub GetNextPollutantCode()
        Dim PollutantCode As String
        Dim newPollutantCode As String
        Try

            PollutantCode = "00001"
            newPollutantCode = "00000"

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Do Until newPollutantCode <> "00000"
                Select Case PollutantCode.Length
                    Case 1
                        PollutantCode = "0000" & PollutantCode
                    Case 2
                        PollutantCode = "000" & PollutantCode
                    Case 3
                        PollutantCode = "00" & PollutantCode
                    Case 4
                        PollutantCode = "0" & PollutantCode
                    Case Else
                        'PollutantCode = PollutantCode
                End Select

                SQL = "Select strPollutantCode " & _
                "from " & DBNameSpace & ".LookUPPollutants " & _
                "where strPollutantCode = '" & PollutantCode & "' "
                cmd = New OracleCommand(SQL, Conn)
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    PollutantCode += 1
                Else
                    newPollutantCode = PollutantCode
                End If
            Loop

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            Select Case PollutantCode.Length
                Case 1
                    PollutantCode = "0000" & PollutantCode
                Case 2
                    PollutantCode = "000" & PollutantCode
                Case 3
                    PollutantCode = "00" & PollutantCode
                Case 4
                    PollutantCode = "0" & PollutantCode
                Case Else
                    'PollutantCode = PollutantCode
            End Select
            txtPollutantCode.Text = PollutantCode

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region

    Private Sub TBAddPollutant_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBAddPollutant.ButtonClick
        Try

            Select Case TBAddPollutant.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Clear()
                Case 2
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub dgrPollutants_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrPollutants.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrPollutants.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrPollutants(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrPollutants(hti.Row, 1)) Then
                    Else
                        txtPollutantCode.Text = dgrPollutants(hti.Row, 0)
                        txtPollutant.Text = dgrPollutants(hti.Row, 1)
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub ISMPAddPollutants_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            ISMPAddPollutant = Nothing
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Main Menu Item"
    Private Sub MmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
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
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try

            Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label1, HelpUrl)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region


    Private Sub llbGetNextValue_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbGetNextValue.LinkClicked
        Try

            GetNextPollutantCode()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub


End Class
