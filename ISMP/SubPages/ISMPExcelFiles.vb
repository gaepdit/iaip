'Imports System.DateTime
Imports Oracle.DataAccess.Client

Imports System.Windows.Forms
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Core
'Imports System.IO
Imports System
Imports System.Data
'Imports System.Text
'Imports System.Collections

Public Class ISMPExcelFiles
    Inherits DefaultForm

    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Paneltemp1 As String
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader

    Dim dsExcelFiles As DataSet
    Dim daExcelFiles As OracleDataAdapter
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
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents dgrExcelFiles As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents llbOpenExcelFile As System.Windows.Forms.LinkLabel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents PanelExcelFile As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPExcelFiles))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MmiExit = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.dgrExcelFiles = New System.Windows.Forms.DataGrid
        Me.PanelExcelFile = New System.Windows.Forms.Panel
        Me.llbOpenExcelFile = New System.Windows.Forms.LinkLabel
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        CType(Me.dgrExcelFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelExcelFile.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiBack, Me.MenuItem9, Me.MmiExit})
        Me.MenuItem1.Text = "File"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 0
        Me.MmiBack.Text = "Back"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 1
        Me.MenuItem9.Text = "-"
        '
        'MmiExit
        '
        Me.MmiExit.Index = 2
        Me.MmiExit.Text = "E&xit"
        '
        'MmiHelp
        '
        Me.MmiHelp.Index = 1
        Me.MmiHelp.Text = "Help"
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
        'dgrExcelFiles
        '
        Me.dgrExcelFiles.DataMember = ""
        Me.dgrExcelFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrExcelFiles.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrExcelFiles.Location = New System.Drawing.Point(0, 0)
        Me.dgrExcelFiles.Name = "dgrExcelFiles"
        Me.dgrExcelFiles.ReadOnly = True
        Me.dgrExcelFiles.Size = New System.Drawing.Size(792, 266)
        Me.dgrExcelFiles.TabIndex = 0
        '
        'PanelExcelFile
        '
        Me.PanelExcelFile.Controls.Add(Me.llbOpenExcelFile)
        Me.PanelExcelFile.Controls.Add(Me.txtFileName)
        Me.PanelExcelFile.Controls.Add(Me.Label1)
        Me.PanelExcelFile.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelExcelFile.Location = New System.Drawing.Point(424, 0)
        Me.PanelExcelFile.Name = "PanelExcelFile"
        Me.PanelExcelFile.Size = New System.Drawing.Size(368, 266)
        Me.PanelExcelFile.TabIndex = 1
        '
        'llbOpenExcelFile
        '
        Me.llbOpenExcelFile.AutoSize = True
        Me.llbOpenExcelFile.Location = New System.Drawing.Point(104, 48)
        Me.llbOpenExcelFile.Name = "llbOpenExcelFile"
        Me.llbOpenExcelFile.Size = New System.Drawing.Size(81, 13)
        Me.llbOpenExcelFile.TabIndex = 2
        Me.llbOpenExcelFile.TabStop = True
        Me.llbOpenExcelFile.Text = "Open Excel File"
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(104, 16)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(100, 20)
        Me.txtFileName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "File Name"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Location = New System.Drawing.Point(419, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(5, 266)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'ISMPExcelFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 266)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.PanelExcelFile)
        Me.Controls.Add(Me.dgrExcelFiles)
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPExcelFiles"
        Me.Text = "ISMP Excel Files"
        CType(Me.dgrExcelFiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelExcelFile.ResumeLayout(False)
        Me.PanelExcelFile.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub ISMPExcelFiles_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            LoadExcelDataSet()
            FormatExcelDataGrid()
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

            'Display panels in the StatusBar control.
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
    Sub LoadExcelDataSet()
        Try

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            SQL = "Select FileID, FileTitle " & _
            "From " & DBNameSpace & ".ISMPTestReportAids"

            dsExcelFiles = New DataSet

            cmd = New OracleCommand(SQL, Conn)

            daExcelFiles = New OracleDataAdapter(cmd)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daExcelFiles.Fill(dsExcelFiles, "ExcelFiles")
            dgrExcelFiles.DataSource = dsExcelFiles
            dgrExcelFiles.DataMember = "ExcelFiles"

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub FormatExcelDataGrid()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "ExcelFiles"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "FileID"
            objtextcol.HeaderText = "ID Number"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "FileTitle"
            objtextcol.HeaderText = "Name of File"
            objtextcol.Width = 300
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrExcelFiles.TableStyles.Clear()
            dgrExcelFiles.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrExcelFiles.CaptionText = "Excel Files Currently Saved"
            dgrExcelFiles.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub



#End Region
    Sub OpenExcelFile()
        Dim FileID As String
        Dim FileName As String
        Dim path As New SaveFileDialog
        'Dim ExcelApp As New Excel.ApplicationClass
        'Dim excelDoc As Excel.WorkbookClass
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook

        Dim DestFilePath As String = "N/A"

        Dim OutPutFile As String = ""

        Try


            If txtFileName.Text <> "" Then
                FileID = txtFileName.Text
                FileID = Mid(FileID, 1, FileID.IndexOf(" - "))

                FileName = txtFileName.Text
                FileName = Mid(FileName, FileName.IndexOf(" - ") + 4)

                path.InitialDirectory = "C:\"
                path.FileName = FileName
                path.Filter = "Microsoft Office Excel Workbook (.xls)|.xls"
                path.FilterIndex = 1
                path.DefaultExt = ".xls"

                If path.ShowDialog = Windows.Forms.DialogResult.OK Then
                    DestFilePath = path.FileName.ToString
                Else
                    DestFilePath = "N/A"
                End If

                If DestFilePath <> "N/A" Then
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If

                    SQL = "Select " & _
                    "FileId, FileTitle, ISMPBlob " & _
                    "from " & DBNameSpace & ".ISMPTestReportAids " & _
                    "Where FileID = '" & FileID & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    dr = cmd.ExecuteReader

                    dr.Read()
                    Dim b(dr.GetBytes(2, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                    dr.GetBytes(2, 0, b, 0, b.Length)
                    dr.Close()

                    Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                    fs.Write(b, 0, b.Length)
                    fs.Close()

                    excelDoc = ExcelApp.Workbooks.Open(DestFilePath)
                    excelDoc.Activate()
                    If ExcelApp.Visible = False Then
                        ExcelApp.Visible = True
                    End If

                    If Conn.State = ConnectionState.Open Then
                        'conn.close()
                    End If

                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub dgrExcelFiles_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrExcelFiles.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrExcelFiles.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrExcelFiles(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrExcelFiles(hti.Row, 1)) Then
                    Else
                        txtFileName.Text = dgrExcelFiles(hti.Row, 0) & " - " & dgrExcelFiles(hti.Row, 1)
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

    Private Sub llbOpenExcelFile_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbOpenExcelFile.LinkClicked
        Try

            OpenExcelFile()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub ISMPExcelFiles_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            ISMPExcelFilePage = Nothing
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

            ISMPExcelFilePage = Nothing
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
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
End Class
