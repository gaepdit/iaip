'Imports System.DateTime
Imports System.Data.OracleClient


Public Class ISMPReferenceNumber
    Inherits System.Windows.Forms.Form
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnReseed As System.Windows.Forms.Button
    Friend WithEvents txtReSeed As System.Windows.Forms.TextBox
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents TbISMPRefNum As System.Windows.Forms.ToolBar
    Friend WithEvents TbbClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents mmiClose As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ISMPReferenceNumber))
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnReseed = New System.Windows.Forms.Button
        Me.txtReSeed = New System.Windows.Forms.TextBox
        Me.TbISMPRefNum = New System.Windows.Forms.ToolBar
        Me.TbbClose = New System.Windows.Forms.ToolBarButton
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mmiClose = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(264, 40)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "You only need to do this once at the beginning of each year. "
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(344, 48)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "To set the Reference Number for the year, enter in the current year followed by f" & _
        "our zeros and one.  i.e. 200400001."
        '
        'btnReseed
        '
        Me.btnReseed.Location = New System.Drawing.Point(144, 54)
        Me.btnReseed.Name = "btnReseed"
        Me.btnReseed.Size = New System.Drawing.Size(72, 24)
        Me.btnReseed.TabIndex = 11
        Me.btnReseed.Text = "ReSeed"
        '
        'txtReSeed
        '
        Me.txtReSeed.Location = New System.Drawing.Point(16, 56)
        Me.txtReSeed.Name = "txtReSeed"
        Me.txtReSeed.Size = New System.Drawing.Size(104, 20)
        Me.txtReSeed.TabIndex = 10
        Me.txtReSeed.Text = ""
        '
        'TbISMPRefNum
        '
        Me.TbISMPRefNum.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.TbbClose})
        Me.TbISMPRefNum.ButtonSize = New System.Drawing.Size(23, 22)
        Me.TbISMPRefNum.DropDownArrows = True
        Me.TbISMPRefNum.ImageList = Me.Image_List_All
        Me.TbISMPRefNum.Location = New System.Drawing.Point(0, 0)
        Me.TbISMPRefNum.Name = "TbISMPRefNum"
        Me.TbISMPRefNum.ShowToolTips = True
        Me.TbISMPRefNum.Size = New System.Drawing.Size(352, 28)
        Me.TbISMPRefNum.TabIndex = 46
        '
        'TbbClose
        '
        Me.TbbClose.ImageIndex = 6
        '
        'Image_List_All
        '
        Me.Image_List_All.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.Image_List_All.ImageSize = New System.Drawing.Size(16, 16)
        Me.Image_List_All.ImageStream = CType(Resources.GetObject("Image_List_All.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Image_List_All.TransparentColor = System.Drawing.Color.Transparent
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiClose})
        Me.MenuItem1.Text = "File"
        '
        'mmiClose
        '
        Me.mmiClose.Index = 0
        Me.mmiClose.Text = "Close Form"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 1
        Me.mmiHelp.Text = "Help"
        '
        '" & DBNameSpace & ".ISMPReferenceNumber
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(352, 225)
        Me.Controls.Add(Me.txtReSeed)
        Me.Controls.Add(Me.TbISMPRefNum)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnReseed)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "" & DBNameSpace & ".ISMPReferenceNumber"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ISMP Reference Number Manager"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ISMPReferenceNumber_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub CreateStatusBar()
        Try

            panel1.Text = "Please update the Current Reference Number..."
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
    Private Sub btnReseed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReseed.Click
        Dim SQL As String

        Try

            If txtReSeed.Text <> "" Then
                SQL = "Update " & DBNameSpace & ".ISMPReferenceNumber set " & _
                "strReferenceNumber = '" & txtReSeed.Text & "' "

                Dim cmd As New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                Dim dr As OracleDataReader = cmd.ExecuteReader
                If Conn.State = ConnectionState.Open Then
                    'conn.close()
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
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label2, HELP_URL)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClose.Click
        Try

            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            ISMPRefNum = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub ISMPReferenceNumber_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub TbISMPRefNum_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TbISMPRefNum.ButtonClick
        Try

            Select Case TbISMPRefNum.Buttons.IndexOf(e.Button)
                Case 0
                    Me.Close()
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
End Class
