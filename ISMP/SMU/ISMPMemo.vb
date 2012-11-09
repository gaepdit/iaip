Imports System.Data.OracleClient
'Imports System.Runtime.InteropServices

Public Class ISMPMemo
    Inherits System.Windows.Forms.Form
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Friend WithEvents TCISMPMemo As System.Windows.Forms.TabControl
    Friend WithEvents TPInternalMemo As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents TPFuturePrintOption As System.Windows.Forms.TabPage
    Dim Paneltemp1 As String


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
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents TbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents TbbClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtMemoIN As System.Windows.Forms.TextBox
    Friend WithEvents MmiClose As System.Windows.Forms.MenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtReferenceNumber As System.Windows.Forms.TextBox
    Friend WithEvents MmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents TBMemo As System.Windows.Forms.ToolBar
    Friend WithEvents txtMemoOut As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISMPMemo))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiClose = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MmiCopy = New System.Windows.Forms.MenuItem
        Me.MmiCut = New System.Windows.Forms.MenuItem
        Me.MmiPaste = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.TBMemo = New System.Windows.Forms.ToolBar
        Me.TbbSave = New System.Windows.Forms.ToolBarButton
        Me.TbbClose = New System.Windows.Forms.ToolBarButton
        Me.txtMemoIN = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtReferenceNumber = New System.Windows.Forms.TextBox
        Me.txtMemoOut = New System.Windows.Forms.TextBox
        Me.TCISMPMemo = New System.Windows.Forms.TabControl
        Me.TPInternalMemo = New System.Windows.Forms.TabPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.TPFuturePrintOption = New System.Windows.Forms.TabPage
        Me.TCISMPMemo.SuspendLayout()
        Me.TPInternalMemo.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel4.SuspendLayout()
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
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem5})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave, Me.MenuItem10, Me.MmiClose})
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
        'MmiClose
        '
        Me.MmiClose.Index = 2
        Me.MmiClose.Text = "Close"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiCopy, Me.MmiCut, Me.MmiPaste})
        Me.MenuItem2.Text = "Edit"
        '
        'MmiCopy
        '
        Me.MmiCopy.Index = 0
        Me.MmiCopy.Text = "Copy"
        '
        'MmiCut
        '
        Me.MmiCut.Index = 1
        Me.MmiCut.Text = "Cut"
        '
        'MmiPaste
        '
        Me.MmiPaste.Index = 2
        Me.MmiPaste.Text = "Paste"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 2
        Me.MenuItem5.Text = "Help"
        '
        'TBMemo
        '
        Me.TBMemo.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.TbbSave, Me.TbbClose})
        Me.TBMemo.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TBMemo.DropDownArrows = True
        Me.TBMemo.ImageList = Me.Image_List_All
        Me.TBMemo.Location = New System.Drawing.Point(0, 0)
        Me.TBMemo.Name = "TBMemo"
        Me.TBMemo.ShowToolTips = True
        Me.TBMemo.Size = New System.Drawing.Size(950, 28)
        Me.TBMemo.TabIndex = 47
        '
        'TbbSave
        '
        Me.TbbSave.ImageIndex = 65
        Me.TbbSave.Name = "TbbSave"
        '
        'TbbClose
        '
        Me.TbbClose.ImageIndex = 2
        Me.TbbClose.Name = "TbbClose"
        '
        'txtMemoIN
        '
        Me.txtMemoIN.AcceptsReturn = True
        Me.txtMemoIN.AcceptsTab = True
        Me.txtMemoIN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMemoIN.Location = New System.Drawing.Point(0, 0)
        Me.txtMemoIN.Multiline = True
        Me.txtMemoIN.Name = "txtMemoIN"
        Me.txtMemoIN.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMemoIN.Size = New System.Drawing.Size(936, 222)
        Me.txtMemoIN.TabIndex = 52
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(253, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(221, 22)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "ISMP Test Report Memo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(480, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 14)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "(based on Reference Number)"
        '
        'txtReferenceNumber
        '
        Me.txtReferenceNumber.Location = New System.Drawing.Point(55, 7)
        Me.txtReferenceNumber.Name = "txtReferenceNumber"
        Me.txtReferenceNumber.Size = New System.Drawing.Size(16, 20)
        Me.txtReferenceNumber.TabIndex = 56
        Me.txtReferenceNumber.Visible = False
        '
        'txtMemoOut
        '
        Me.txtMemoOut.AcceptsReturn = True
        Me.txtMemoOut.AcceptsTab = True
        Me.txtMemoOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMemoOut.Location = New System.Drawing.Point(0, 0)
        Me.txtMemoOut.Multiline = True
        Me.txtMemoOut.Name = "txtMemoOut"
        Me.txtMemoOut.ReadOnly = True
        Me.txtMemoOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMemoOut.Size = New System.Drawing.Size(936, 316)
        Me.txtMemoOut.TabIndex = 53
        '
        'TCISMPMemo
        '
        Me.TCISMPMemo.Controls.Add(Me.TPInternalMemo)
        Me.TCISMPMemo.Controls.Add(Me.TPFuturePrintOption)
        Me.TCISMPMemo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCISMPMemo.Location = New System.Drawing.Point(0, 58)
        Me.TCISMPMemo.Name = "TCISMPMemo"
        Me.TCISMPMemo.SelectedIndex = 0
        Me.TCISMPMemo.Size = New System.Drawing.Size(950, 580)
        Me.TCISMPMemo.TabIndex = 57
        '
        'TPInternalMemo
        '
        Me.TPInternalMemo.Controls.Add(Me.SplitContainer1)
        Me.TPInternalMemo.Location = New System.Drawing.Point(4, 22)
        Me.TPInternalMemo.Name = "TPInternalMemo"
        Me.TPInternalMemo.Padding = New System.Windows.Forms.Padding(3)
        Me.TPInternalMemo.Size = New System.Drawing.Size(942, 554)
        Me.TPInternalMemo.TabIndex = 0
        Me.TPInternalMemo.Text = "Internal Memo"
        Me.TPInternalMemo.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMemoOut)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtMemoIN)
        Me.SplitContainer1.Size = New System.Drawing.Size(936, 548)
        Me.SplitContainer1.SplitterDistance = 316
        Me.SplitContainer1.SplitterWidth = 10
        Me.SplitContainer1.TabIndex = 54
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.txtReferenceNumber)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 28)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(950, 30)
        Me.Panel4.TabIndex = 58
        '
        'TPFuturePrintOption
        '
        Me.TPFuturePrintOption.Location = New System.Drawing.Point(4, 22)
        Me.TPFuturePrintOption.Name = "TPFuturePrintOption"
        Me.TPFuturePrintOption.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFuturePrintOption.Size = New System.Drawing.Size(942, 554)
        Me.TPFuturePrintOption.TabIndex = 1
        Me.TPFuturePrintOption.Text = "Future Print option"
        Me.TPFuturePrintOption.UseVisualStyleBackColor = True
        '
        'ISMPMemo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(950, 638)
        Me.Controls.Add(Me.TCISMPMemo)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.TBMemo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "ISMPMemo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Memo"
        Me.TCISMPMemo.ResumeLayout(False)
        Me.TPInternalMemo.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub ISMPMemo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            TCISMPMemo.TabPages.Remove(TPFuturePrintOption)

            CreateStatusBar()
            LoadMemo()
            If AccountArray(15, 0) = "1" Or AccountArray(15, 1) = "1" Or AccountArray(15, 2) = "1" Or AccountArray(15, 3) = "1" Then
            Else
                TBMemo.Buttons.Remove(TbbSave)
                MmiSave.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try
 
    End Sub


#Region "Page Load"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Enter memo in the bottom box (limit 4000 characters)..."
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
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region

    Sub SaveMemo()
        Dim SQL As String
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader
        Dim dashes As String = "--------------------------------------------------------------------------------------------"
        Dim MemoTemp As String

        Try

            If txtReferenceNumber.Text <> "" Then
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                SQL = "Select strReferenceNumber " & _
                "from " & connNameSpace & ".ISMPMaster " & _
                "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)

                dr = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist = True Then
                    SQL = "Select strMemorandumField " & _
                    "from " & connNameSpace & ".ISMPTestREportMemo " & _
                    "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                    cmd = New OracleCommand(SQL, conn)

                    dr = cmd.ExecuteReader
                    recExist = dr.Read

                    txtMemoIN.Text = OracleDate + vbCrLf + txtMemoIN.Text + vbCrLf + UserName + vbCrLf + dashes + vbCrLf

                    If recExist = True Then
                        MemoTemp = dr.Item("StrMemorandumField")
                        MemoTemp = MemoTemp & vbCrLf & Replace(txtMemoIN.Text, " '", "''")

                        SQL = "Update " & connNameSpace & ".ISMPTestREportMemo set " & _
                        "strMemorandumField = '" & Replace(MemoTemp, "'", "''") & "' " & _
                        "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                    Else
                        SQL = "Insert into " & connNameSpace & ".ISMPTestREportMemo " & _
                        "(strReferenceNumber, strMemorandumField) " & _
                        "values " & _
                        "('" & txtReferenceNumber.Text & "', '" & Replace(txtMemoIN.Text, "'", "''") & "')"
                    End If
                    cmd = New OracleCommand(SQL, conn)
                    dr = cmd.ExecuteReader
                End If
                If conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub LoadMemo()
        Dim SQL As String
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Try

            If txtReferenceNumber.Text <> "" Then
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                SQL = "Select strMemorandumField " & _
                "from " & connNameSpace & ".ISMPTestREportMemo " & _
                "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)

                dr = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist = True Then
                    txtMemoOut.Text = dr.Item("strMemorandumField")
                End If

                If conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiCopy.Click
        Try

            SendKeys.Send("^(c)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiCut.Click
        Try

            SendKeys.Send("^(x)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiPaste.Click
        Try

            SendKeys.Send("^(v)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub ISMPMemo_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiClose.Click
        Try

            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub TBMemo_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBMemo.ButtonClick
        Try

            Select Case TBMemo.Buttons.IndexOf(e.Button)
                Case 0
                    SaveMemo()
                    txtMemoOut.Clear()
                    LoadMemo()
                    txtMemoIN.Clear()
                Case 1
                    Me.Hide()
            End Select
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

            SaveMemo()
            txtMemoOut.Clear()
            LoadMemo()
            txtMemoIN.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub


    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        Try
            Help.ShowHelp(Label1, "https://sites.google.com/a/dnr.state.ga.us/iaip-docs/")
        Catch ex As Exception
        End Try

    End Sub
End Class
