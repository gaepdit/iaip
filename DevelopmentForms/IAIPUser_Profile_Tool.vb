Imports System.DateTime
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.IO

Public Class IAIPUser_Profile_Tool
    Inherits System.Windows.Forms.Form
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim SQL, SQL2, SQL3 As String
    Dim SQL4, SQL5, SQL6 As String
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim cmd4, cmd5, cmd6 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader
    Dim dr4, dr5, dr6 As OracleDataReader


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
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MmiFile As System.Windows.Forms.MenuItem
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiExit As System.Windows.Forms.MenuItem
    Friend WithEvents MmiEdit As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCut As System.Windows.Forms.MenuItem
    Friend WithEvents MmiCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MmiPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MmiView As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiClearPage As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiToolbar As System.Windows.Forms.MenuItem
    Friend WithEvents MmiTool As System.Windows.Forms.MenuItem
    Friend WithEvents MmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents MmiFacilityHelp As System.Windows.Forms.MenuItem
    Friend WithEvents cmFind As System.Windows.Forms.ContextMenu
    Friend WithEvents cmiEngineerList As System.Windows.Forms.MenuItem
    Friend WithEvents cmiAllEngineers As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem28 As System.Windows.Forms.MenuItem
    Friend WithEvents cmiAirToxicsUnitEngineer As System.Windows.Forms.MenuItem
    Friend WithEvents cmiChemicals_MineralUnitEngineers As System.Windows.Forms.MenuItem
    Friend WithEvents cmiVOC_CombustionUnitEngineers As System.Windows.Forms.MenuItem
    Friend WithEvents cmiFacilityList As System.Windows.Forms.MenuItem
    Friend WithEvents cmiAllFacilities As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem26 As System.Windows.Forms.MenuItem
    Friend WithEvents cmiAirToxicsUnitFacilities As System.Windows.Forms.MenuItem
    Friend WithEvents cmiChemicals_MineralFacilities As System.Windows.Forms.MenuItem
    Friend WithEvents cmiVOC_CombustionUnitFacilities As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem27 As System.Windows.Forms.MenuItem
    Friend WithEvents cmiRegionalFacilities As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents cmiUnassignedFacilities As System.Windows.Forms.MenuItem
    Friend WithEvents tbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbExit As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbUser_Profile As System.Windows.Forms.TabPage
    Friend WithEvents tbSSCP_UC_Facility_Assignment_Profile As System.Windows.Forms.TabPage
    Friend WithEvents User_Profile As System.Windows.Forms.GroupBox
    Friend WithEvents txtUser_First_Name As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNew_User_First_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtUser_Last_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtNew_User_Last_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtUser_Log_In As System.Windows.Forms.TextBox
    Friend WithEvents txtNew_User_Log_In As System.Windows.Forms.TextBox
    Friend WithEvents txtUser_Password As System.Windows.Forms.TextBox
    Friend WithEvents txtNew_User_Password As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chbSSCP_Unit As System.Windows.Forms.CheckBox
    Friend WithEvents chbDistrict As System.Windows.Forms.CheckBox
    Friend WithEvents chbClass As System.Windows.Forms.CheckBox
    Friend WithEvents chbOperational_Status As System.Windows.Forms.CheckBox
    Friend WithEvents chbCounty As System.Windows.Forms.CheckBox
    Friend WithEvents chbAIRS_Number As System.Windows.Forms.CheckBox
    Friend WithEvents chbCity As System.Windows.Forms.CheckBox
    Friend WithEvents chbSIC_Code As System.Windows.Forms.CheckBox
    Friend WithEvents chbLast_FCE As System.Windows.Forms.CheckBox
    Friend WithEvents chbLast_Inspection_Date As System.Windows.Forms.CheckBox
    Friend WithEvents chbFacility_Name As System.Windows.Forms.CheckBox
    Friend WithEvents TCUser_Profile_Tool As System.Windows.Forms.TabControl
    Friend WithEvents txtAIRS_Number As System.Windows.Forms.TextBox
    Friend WithEvents txtOperational_Status As System.Windows.Forms.TextBox
    Friend WithEvents txtLast_Inspection_Date As System.Windows.Forms.TextBox
    Friend WithEvents txtLast_FCE As System.Windows.Forms.TextBox
    Friend WithEvents txtFacility_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtEngineer As System.Windows.Forms.TextBox
    Friend WithEvents txtCounty As System.Windows.Forms.TextBox
    Friend WithEvents txtClassification As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtDistrict As System.Windows.Forms.TextBox
    Friend WithEvents txtDistrict_Engineer As System.Windows.Forms.TextBox
    Friend WithEvents txtSIC_Code As System.Windows.Forms.TextBox
    Friend WithEvents chbEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents chbDistrict_Engineer As System.Windows.Forms.CheckBox
    Friend WithEvents txtCheckBox_Count As System.Windows.Forms.TextBox
    Friend WithEvents txtSSCP_Unit As System.Windows.Forms.TextBox
    Friend WithEvents TBUserProfileTool As System.Windows.Forms.ToolBar
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents PanelUserProfile As System.Windows.Forms.Panel
    Friend WithEvents txtDistrictResponsible As System.Windows.Forms.TextBox
    Friend WithEvents chbDistrictResponsible As System.Windows.Forms.CheckBox
    Friend WithEvents tbISMPEngineerStats As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbUnitStatsAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rdbUnitDateCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitDateTestStarted As System.Windows.Forms.RadioButton
    Friend WithEvents rdbUnitDateReceived As System.Windows.Forms.RadioButton
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents DTPUnitEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPUnitStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents llbRunEngineerStatReport As System.Windows.Forms.LinkLabel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IAIPUser_Profile_Tool))
        Me.TCUser_Profile_Tool = New System.Windows.Forms.TabControl
        Me.tbUser_Profile = New System.Windows.Forms.TabPage
        Me.User_Profile = New System.Windows.Forms.GroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtNew_User_Password = New System.Windows.Forms.TextBox
        Me.txtUser_Password = New System.Windows.Forms.TextBox
        Me.txtNew_User_Log_In = New System.Windows.Forms.TextBox
        Me.txtUser_Log_In = New System.Windows.Forms.TextBox
        Me.txtNew_User_Last_Name = New System.Windows.Forms.TextBox
        Me.txtUser_Last_Name = New System.Windows.Forms.TextBox
        Me.txtNew_User_First_Name = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtUser_First_Name = New System.Windows.Forms.TextBox
        Me.tbISMPEngineerStats = New System.Windows.Forms.TabPage
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.llbRunEngineerStatReport = New System.Windows.Forms.LinkLabel
        Me.DTPUnitStart = New System.Windows.Forms.DateTimePicker
        Me.DTPUnitEnd = New System.Windows.Forms.DateTimePicker
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.rdbUnitStatsAll = New System.Windows.Forms.RadioButton
        Me.Label8 = New System.Windows.Forms.Label
        Me.rdbUnitDateCompleted = New System.Windows.Forms.RadioButton
        Me.rdbUnitDateTestStarted = New System.Windows.Forms.RadioButton
        Me.rdbUnitDateReceived = New System.Windows.Forms.RadioButton
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.tbSSCP_UC_Facility_Assignment_Profile = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtDistrictResponsible = New System.Windows.Forms.TextBox
        Me.chbDistrictResponsible = New System.Windows.Forms.CheckBox
        Me.txtCheckBox_Count = New System.Windows.Forms.TextBox
        Me.chbEngineer = New System.Windows.Forms.CheckBox
        Me.chbDistrict_Engineer = New System.Windows.Forms.CheckBox
        Me.txtSSCP_Unit = New System.Windows.Forms.TextBox
        Me.txtSIC_Code = New System.Windows.Forms.TextBox
        Me.txtDistrict_Engineer = New System.Windows.Forms.TextBox
        Me.chbFacility_Name = New System.Windows.Forms.CheckBox
        Me.txtDistrict = New System.Windows.Forms.TextBox
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.txtClassification = New System.Windows.Forms.TextBox
        Me.txtCounty = New System.Windows.Forms.TextBox
        Me.txtEngineer = New System.Windows.Forms.TextBox
        Me.txtFacility_Name = New System.Windows.Forms.TextBox
        Me.txtLast_FCE = New System.Windows.Forms.TextBox
        Me.txtLast_Inspection_Date = New System.Windows.Forms.TextBox
        Me.txtOperational_Status = New System.Windows.Forms.TextBox
        Me.chbLast_Inspection_Date = New System.Windows.Forms.CheckBox
        Me.chbLast_FCE = New System.Windows.Forms.CheckBox
        Me.chbSIC_Code = New System.Windows.Forms.CheckBox
        Me.chbCity = New System.Windows.Forms.CheckBox
        Me.chbAIRS_Number = New System.Windows.Forms.CheckBox
        Me.chbCounty = New System.Windows.Forms.CheckBox
        Me.chbOperational_Status = New System.Windows.Forms.CheckBox
        Me.chbClass = New System.Windows.Forms.CheckBox
        Me.chbDistrict = New System.Windows.Forms.CheckBox
        Me.txtAIRS_Number = New System.Windows.Forms.TextBox
        Me.chbSSCP_Unit = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MmiFile = New System.Windows.Forms.MenuItem
        Me.MmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem11 = New System.Windows.Forms.MenuItem
        Me.MmiExit = New System.Windows.Forms.MenuItem
        Me.MmiEdit = New System.Windows.Forms.MenuItem
        Me.MmiCut = New System.Windows.Forms.MenuItem
        Me.MmiCopy = New System.Windows.Forms.MenuItem
        Me.MmiPaste = New System.Windows.Forms.MenuItem
        Me.MmiView = New System.Windows.Forms.MenuItem
        Me.MenuItem20 = New System.Windows.Forms.MenuItem
        Me.MmiClearPage = New System.Windows.Forms.MenuItem
        Me.MenuItem7 = New System.Windows.Forms.MenuItem
        Me.MmiToolbar = New System.Windows.Forms.MenuItem
        Me.MmiTool = New System.Windows.Forms.MenuItem
        Me.MmiHelp = New System.Windows.Forms.MenuItem
        Me.MmiFacilityHelp = New System.Windows.Forms.MenuItem
        Me.cmFind = New System.Windows.Forms.ContextMenu
        Me.cmiEngineerList = New System.Windows.Forms.MenuItem
        Me.cmiAllEngineers = New System.Windows.Forms.MenuItem
        Me.MenuItem28 = New System.Windows.Forms.MenuItem
        Me.cmiAirToxicsUnitEngineer = New System.Windows.Forms.MenuItem
        Me.cmiChemicals_MineralUnitEngineers = New System.Windows.Forms.MenuItem
        Me.cmiVOC_CombustionUnitEngineers = New System.Windows.Forms.MenuItem
        Me.cmiFacilityList = New System.Windows.Forms.MenuItem
        Me.cmiAllFacilities = New System.Windows.Forms.MenuItem
        Me.MenuItem26 = New System.Windows.Forms.MenuItem
        Me.cmiAirToxicsUnitFacilities = New System.Windows.Forms.MenuItem
        Me.cmiChemicals_MineralFacilities = New System.Windows.Forms.MenuItem
        Me.cmiVOC_CombustionUnitFacilities = New System.Windows.Forms.MenuItem
        Me.MenuItem27 = New System.Windows.Forms.MenuItem
        Me.cmiRegionalFacilities = New System.Windows.Forms.MenuItem
        Me.MenuItem12 = New System.Windows.Forms.MenuItem
        Me.cmiUnassignedFacilities = New System.Windows.Forms.MenuItem
        Me.TBUserProfileTool = New System.Windows.Forms.ToolBar
        Me.tbbSave = New System.Windows.Forms.ToolBarButton
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.tbbExit = New System.Windows.Forms.ToolBarButton
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelUserProfile = New System.Windows.Forms.Panel
        Me.TCUser_Profile_Tool.SuspendLayout()
        Me.tbUser_Profile.SuspendLayout()
        Me.User_Profile.SuspendLayout()
        Me.tbISMPEngineerStats.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.tbSSCP_UC_Facility_Assignment_Profile.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.PanelUserProfile.SuspendLayout()
        Me.SuspendLayout()
        '
        'TCUser_Profile_Tool
        '
        Me.TCUser_Profile_Tool.Controls.Add(Me.tbUser_Profile)
        Me.TCUser_Profile_Tool.Controls.Add(Me.tbISMPEngineerStats)
        Me.TCUser_Profile_Tool.Controls.Add(Me.TabPage5)
        Me.TCUser_Profile_Tool.Controls.Add(Me.tbSSCP_UC_Facility_Assignment_Profile)
        Me.TCUser_Profile_Tool.Controls.Add(Me.TabPage4)
        Me.TCUser_Profile_Tool.Controls.Add(Me.TabPage6)
        Me.TCUser_Profile_Tool.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCUser_Profile_Tool.Location = New System.Drawing.Point(0, 0)
        Me.TCUser_Profile_Tool.Name = "TCUser_Profile_Tool"
        Me.TCUser_Profile_Tool.SelectedIndex = 0
        Me.TCUser_Profile_Tool.Size = New System.Drawing.Size(537, 268)
        Me.TCUser_Profile_Tool.TabIndex = 61
        '
        'tbUser_Profile
        '
        Me.tbUser_Profile.Controls.Add(Me.User_Profile)
        Me.tbUser_Profile.Location = New System.Drawing.Point(4, 22)
        Me.tbUser_Profile.Name = "tbUser_Profile"
        Me.tbUser_Profile.Size = New System.Drawing.Size(529, 242)
        Me.tbUser_Profile.TabIndex = 0
        Me.tbUser_Profile.Text = "User Profile"
        '
        'User_Profile
        '
        Me.User_Profile.Controls.Add(Me.Label6)
        Me.User_Profile.Controls.Add(Me.Label5)
        Me.User_Profile.Controls.Add(Me.txtNew_User_Password)
        Me.User_Profile.Controls.Add(Me.txtUser_Password)
        Me.User_Profile.Controls.Add(Me.txtNew_User_Log_In)
        Me.User_Profile.Controls.Add(Me.txtUser_Log_In)
        Me.User_Profile.Controls.Add(Me.txtNew_User_Last_Name)
        Me.User_Profile.Controls.Add(Me.txtUser_Last_Name)
        Me.User_Profile.Controls.Add(Me.txtNew_User_First_Name)
        Me.User_Profile.Controls.Add(Me.Label4)
        Me.User_Profile.Controls.Add(Me.Label3)
        Me.User_Profile.Controls.Add(Me.Label2)
        Me.User_Profile.Controls.Add(Me.Label1)
        Me.User_Profile.Controls.Add(Me.txtUser_First_Name)
        Me.User_Profile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.User_Profile.Location = New System.Drawing.Point(0, 0)
        Me.User_Profile.Name = "User_Profile"
        Me.User_Profile.Size = New System.Drawing.Size(529, 242)
        Me.User_Profile.TabIndex = 0
        Me.User_Profile.TabStop = False
        Me.User_Profile.Text = "User Profile"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(157, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Current"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(269, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "New"
        '
        'txtNew_User_Password
        '
        Me.txtNew_User_Password.Location = New System.Drawing.Point(232, 151)
        Me.txtNew_User_Password.MaxLength = 8
        Me.txtNew_User_Password.Name = "txtNew_User_Password"
        Me.txtNew_User_Password.Size = New System.Drawing.Size(100, 20)
        Me.txtNew_User_Password.TabIndex = 11
        '
        'txtUser_Password
        '
        Me.txtUser_Password.Location = New System.Drawing.Point(128, 151)
        Me.txtUser_Password.Name = "txtUser_Password"
        Me.txtUser_Password.ReadOnly = True
        Me.txtUser_Password.Size = New System.Drawing.Size(100, 20)
        Me.txtUser_Password.TabIndex = 10
        '
        'txtNew_User_Log_In
        '
        Me.txtNew_User_Log_In.Location = New System.Drawing.Point(232, 114)
        Me.txtNew_User_Log_In.MaxLength = 12
        Me.txtNew_User_Log_In.Name = "txtNew_User_Log_In"
        Me.txtNew_User_Log_In.Size = New System.Drawing.Size(100, 20)
        Me.txtNew_User_Log_In.TabIndex = 9
        '
        'txtUser_Log_In
        '
        Me.txtUser_Log_In.Location = New System.Drawing.Point(128, 114)
        Me.txtUser_Log_In.Name = "txtUser_Log_In"
        Me.txtUser_Log_In.ReadOnly = True
        Me.txtUser_Log_In.Size = New System.Drawing.Size(100, 20)
        Me.txtUser_Log_In.TabIndex = 8
        '
        'txtNew_User_Last_Name
        '
        Me.txtNew_User_Last_Name.Location = New System.Drawing.Point(232, 77)
        Me.txtNew_User_Last_Name.MaxLength = 25
        Me.txtNew_User_Last_Name.Name = "txtNew_User_Last_Name"
        Me.txtNew_User_Last_Name.Size = New System.Drawing.Size(100, 20)
        Me.txtNew_User_Last_Name.TabIndex = 7
        '
        'txtUser_Last_Name
        '
        Me.txtUser_Last_Name.Location = New System.Drawing.Point(128, 77)
        Me.txtUser_Last_Name.Name = "txtUser_Last_Name"
        Me.txtUser_Last_Name.ReadOnly = True
        Me.txtUser_Last_Name.Size = New System.Drawing.Size(100, 20)
        Me.txtUser_Last_Name.TabIndex = 6
        '
        'txtNew_User_First_Name
        '
        Me.txtNew_User_First_Name.Location = New System.Drawing.Point(232, 40)
        Me.txtNew_User_First_Name.MaxLength = 25
        Me.txtNew_User_First_Name.Name = "txtNew_User_First_Name"
        Me.txtNew_User_First_Name.Size = New System.Drawing.Size(100, 20)
        Me.txtNew_User_First_Name.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(51, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Last Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(60, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Log In ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(56, 153)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Password"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "First Name"
        '
        'txtUser_First_Name
        '
        Me.txtUser_First_Name.Location = New System.Drawing.Point(128, 40)
        Me.txtUser_First_Name.Name = "txtUser_First_Name"
        Me.txtUser_First_Name.ReadOnly = True
        Me.txtUser_First_Name.Size = New System.Drawing.Size(100, 20)
        Me.txtUser_First_Name.TabIndex = 0
        '
        'tbISMPEngineerStats
        '
        Me.tbISMPEngineerStats.Controls.Add(Me.Splitter1)
        Me.tbISMPEngineerStats.Controls.Add(Me.Panel4)
        Me.tbISMPEngineerStats.Controls.Add(Me.GroupBox10)
        Me.tbISMPEngineerStats.Location = New System.Drawing.Point(4, 22)
        Me.tbISMPEngineerStats.Name = "tbISMPEngineerStats"
        Me.tbISMPEngineerStats.Size = New System.Drawing.Size(439, 206)
        Me.tbISMPEngineerStats.TabIndex = 2
        Me.tbISMPEngineerStats.Text = "ISMP Engineer Stats"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 128)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(439, 5)
        Me.Splitter1.TabIndex = 164
        Me.Splitter1.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.llbRunEngineerStatReport)
        Me.Panel4.Controls.Add(Me.DTPUnitStart)
        Me.Panel4.Controls.Add(Me.DTPUnitEnd)
        Me.Panel4.Controls.Add(Me.Label28)
        Me.Panel4.Controls.Add(Me.Label29)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 128)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(439, 78)
        Me.Panel4.TabIndex = 163
        '
        'llbRunEngineerStatReport
        '
        Me.llbRunEngineerStatReport.AutoSize = True
        Me.llbRunEngineerStatReport.Location = New System.Drawing.Point(280, 24)
        Me.llbRunEngineerStatReport.Name = "llbRunEngineerStatReport"
        Me.llbRunEngineerStatReport.Size = New System.Drawing.Size(65, 13)
        Me.llbRunEngineerStatReport.TabIndex = 158
        Me.llbRunEngineerStatReport.TabStop = True
        Me.llbRunEngineerStatReport.Text = "Run Report "
        '
        'DTPUnitStart
        '
        Me.DTPUnitStart.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitStart.Location = New System.Drawing.Point(24, 24)
        Me.DTPUnitStart.Name = "DTPUnitStart"
        Me.DTPUnitStart.Size = New System.Drawing.Size(104, 20)
        Me.DTPUnitStart.TabIndex = 159
        Me.DTPUnitStart.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'DTPUnitEnd
        '
        Me.DTPUnitEnd.CustomFormat = "dd-MMM-yyyy"
        Me.DTPUnitEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPUnitEnd.Location = New System.Drawing.Point(144, 24)
        Me.DTPUnitEnd.Name = "DTPUnitEnd"
        Me.DTPUnitEnd.Size = New System.Drawing.Size(112, 20)
        Me.DTPUnitEnd.TabIndex = 160
        Me.DTPUnitEnd.Value = New Date(2005, 8, 23, 0, 0, 0, 0)
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(8, 8)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(55, 13)
        Me.Label28.TabIndex = 161
        Me.Label28.Text = "Start Date"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(120, 8)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(55, 13)
        Me.Label29.TabIndex = 162
        Me.Label29.Text = "End Date "
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.rdbUnitStatsAll)
        Me.GroupBox10.Controls.Add(Me.Label8)
        Me.GroupBox10.Controls.Add(Me.rdbUnitDateCompleted)
        Me.GroupBox10.Controls.Add(Me.rdbUnitDateTestStarted)
        Me.GroupBox10.Controls.Add(Me.rdbUnitDateReceived)
        Me.GroupBox10.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox10.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox10.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(439, 128)
        Me.GroupBox10.TabIndex = 144
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Date Bias"
        '
        'rdbUnitStatsAll
        '
        Me.rdbUnitStatsAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitStatsAll.Location = New System.Drawing.Point(8, 88)
        Me.rdbUnitStatsAll.Name = "rdbUnitStatsAll"
        Me.rdbUnitStatsAll.Size = New System.Drawing.Size(104, 16)
        Me.rdbUnitStatsAll.TabIndex = 152
        Me.rdbUnitStatsAll.Text = "All Test Reports"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(24, 104)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(185, 13)
        Me.Label8.TabIndex = 153
        Me.Label8.Text = "(All Dates - Excluding SM23 Archives)"
        '
        'rdbUnitDateCompleted
        '
        Me.rdbUnitDateCompleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateCompleted.Location = New System.Drawing.Point(8, 64)
        Me.rdbUnitDateCompleted.Name = "rdbUnitDateCompleted"
        Me.rdbUnitDateCompleted.Size = New System.Drawing.Size(144, 24)
        Me.rdbUnitDateCompleted.TabIndex = 2
        Me.rdbUnitDateCompleted.Text = "Date Report Completed"
        '
        'rdbUnitDateTestStarted
        '
        Me.rdbUnitDateTestStarted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateTestStarted.Location = New System.Drawing.Point(8, 16)
        Me.rdbUnitDateTestStarted.Name = "rdbUnitDateTestStarted"
        Me.rdbUnitDateTestStarted.Size = New System.Drawing.Size(136, 24)
        Me.rdbUnitDateTestStarted.TabIndex = 1
        Me.rdbUnitDateTestStarted.Text = "Date Test Started"
        '
        'rdbUnitDateReceived
        '
        Me.rdbUnitDateReceived.Checked = True
        Me.rdbUnitDateReceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbUnitDateReceived.Location = New System.Drawing.Point(8, 40)
        Me.rdbUnitDateReceived.Name = "rdbUnitDateReceived"
        Me.rdbUnitDateReceived.Size = New System.Drawing.Size(104, 24)
        Me.rdbUnitDateReceived.TabIndex = 0
        Me.rdbUnitDateReceived.TabStop = True
        Me.rdbUnitDateReceived.Text = "Date Received"
        '
        'TabPage5
        '
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(439, 206)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "TabPage5"
        '
        'tbSSCP_UC_Facility_Assignment_Profile
        '
        Me.tbSSCP_UC_Facility_Assignment_Profile.Controls.Add(Me.GroupBox1)
        Me.tbSSCP_UC_Facility_Assignment_Profile.Location = New System.Drawing.Point(4, 22)
        Me.tbSSCP_UC_Facility_Assignment_Profile.Name = "tbSSCP_UC_Facility_Assignment_Profile"
        Me.tbSSCP_UC_Facility_Assignment_Profile.Size = New System.Drawing.Size(439, 206)
        Me.tbSSCP_UC_Facility_Assignment_Profile.TabIndex = 1
        Me.tbSSCP_UC_Facility_Assignment_Profile.Text = "UC Facility Assignment Profile"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDistrictResponsible)
        Me.GroupBox1.Controls.Add(Me.chbDistrictResponsible)
        Me.GroupBox1.Controls.Add(Me.txtCheckBox_Count)
        Me.GroupBox1.Controls.Add(Me.chbEngineer)
        Me.GroupBox1.Controls.Add(Me.chbDistrict_Engineer)
        Me.GroupBox1.Controls.Add(Me.txtSSCP_Unit)
        Me.GroupBox1.Controls.Add(Me.txtSIC_Code)
        Me.GroupBox1.Controls.Add(Me.txtDistrict_Engineer)
        Me.GroupBox1.Controls.Add(Me.chbFacility_Name)
        Me.GroupBox1.Controls.Add(Me.txtDistrict)
        Me.GroupBox1.Controls.Add(Me.txtCity)
        Me.GroupBox1.Controls.Add(Me.txtClassification)
        Me.GroupBox1.Controls.Add(Me.txtCounty)
        Me.GroupBox1.Controls.Add(Me.txtEngineer)
        Me.GroupBox1.Controls.Add(Me.txtFacility_Name)
        Me.GroupBox1.Controls.Add(Me.txtLast_FCE)
        Me.GroupBox1.Controls.Add(Me.txtLast_Inspection_Date)
        Me.GroupBox1.Controls.Add(Me.txtOperational_Status)
        Me.GroupBox1.Controls.Add(Me.chbLast_Inspection_Date)
        Me.GroupBox1.Controls.Add(Me.chbLast_FCE)
        Me.GroupBox1.Controls.Add(Me.chbSIC_Code)
        Me.GroupBox1.Controls.Add(Me.chbCity)
        Me.GroupBox1.Controls.Add(Me.chbAIRS_Number)
        Me.GroupBox1.Controls.Add(Me.chbCounty)
        Me.GroupBox1.Controls.Add(Me.chbOperational_Status)
        Me.GroupBox1.Controls.Add(Me.chbClass)
        Me.GroupBox1.Controls.Add(Me.chbDistrict)
        Me.GroupBox1.Controls.Add(Me.txtAIRS_Number)
        Me.GroupBox1.Controls.Add(Me.chbSSCP_Unit)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(439, 206)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Facility Assignment Profile"
        '
        'txtDistrictResponsible
        '
        Me.txtDistrictResponsible.Location = New System.Drawing.Point(152, 42)
        Me.txtDistrictResponsible.Name = "txtDistrictResponsible"
        Me.txtDistrictResponsible.ReadOnly = True
        Me.txtDistrictResponsible.Size = New System.Drawing.Size(24, 20)
        Me.txtDistrictResponsible.TabIndex = 30
        '
        'chbDistrictResponsible
        '
        Me.chbDistrictResponsible.Location = New System.Drawing.Point(184, 44)
        Me.chbDistrictResponsible.Name = "chbDistrictResponsible"
        Me.chbDistrictResponsible.Size = New System.Drawing.Size(128, 16)
        Me.chbDistrictResponsible.TabIndex = 29
        Me.chbDistrictResponsible.Text = "District Responsible"
        '
        'txtCheckBox_Count
        '
        Me.txtCheckBox_Count.Location = New System.Drawing.Point(408, 157)
        Me.txtCheckBox_Count.Name = "txtCheckBox_Count"
        Me.txtCheckBox_Count.ReadOnly = True
        Me.txtCheckBox_Count.Size = New System.Drawing.Size(24, 20)
        Me.txtCheckBox_Count.TabIndex = 28
        Me.txtCheckBox_Count.Text = "0"
        Me.txtCheckBox_Count.Visible = False
        '
        'chbEngineer
        '
        Me.chbEngineer.Location = New System.Drawing.Point(184, 63)
        Me.chbEngineer.Name = "chbEngineer"
        Me.chbEngineer.Size = New System.Drawing.Size(72, 24)
        Me.chbEngineer.TabIndex = 27
        Me.chbEngineer.Text = "Engineer"
        '
        'chbDistrict_Engineer
        '
        Me.chbDistrict_Engineer.Location = New System.Drawing.Point(40, 155)
        Me.chbDistrict_Engineer.Name = "chbDistrict_Engineer"
        Me.chbDistrict_Engineer.Size = New System.Drawing.Size(112, 24)
        Me.chbDistrict_Engineer.TabIndex = 26
        Me.chbDistrict_Engineer.Text = "District Engineer"
        '
        'txtSSCP_Unit
        '
        Me.txtSSCP_Unit.Location = New System.Drawing.Point(320, 65)
        Me.txtSSCP_Unit.Name = "txtSSCP_Unit"
        Me.txtSSCP_Unit.ReadOnly = True
        Me.txtSSCP_Unit.Size = New System.Drawing.Size(24, 20)
        Me.txtSSCP_Unit.TabIndex = 24
        '
        'txtSIC_Code
        '
        Me.txtSIC_Code.Location = New System.Drawing.Point(320, 42)
        Me.txtSIC_Code.Name = "txtSIC_Code"
        Me.txtSIC_Code.ReadOnly = True
        Me.txtSIC_Code.Size = New System.Drawing.Size(24, 20)
        Me.txtSIC_Code.TabIndex = 23
        '
        'txtDistrict_Engineer
        '
        Me.txtDistrict_Engineer.Location = New System.Drawing.Point(8, 157)
        Me.txtDistrict_Engineer.Name = "txtDistrict_Engineer"
        Me.txtDistrict_Engineer.ReadOnly = True
        Me.txtDistrict_Engineer.Size = New System.Drawing.Size(24, 20)
        Me.txtDistrict_Engineer.TabIndex = 22
        '
        'chbFacility_Name
        '
        Me.chbFacility_Name.Location = New System.Drawing.Point(184, 86)
        Me.chbFacility_Name.Name = "chbFacility_Name"
        Me.chbFacility_Name.Size = New System.Drawing.Size(96, 24)
        Me.chbFacility_Name.TabIndex = 21
        Me.chbFacility_Name.Text = "Facility Name"
        '
        'txtDistrict
        '
        Me.txtDistrict.Location = New System.Drawing.Point(8, 134)
        Me.txtDistrict.Name = "txtDistrict"
        Me.txtDistrict.ReadOnly = True
        Me.txtDistrict.Size = New System.Drawing.Size(24, 20)
        Me.txtDistrict.TabIndex = 20
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(8, 65)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReadOnly = True
        Me.txtCity.Size = New System.Drawing.Size(24, 20)
        Me.txtCity.TabIndex = 19
        '
        'txtClassification
        '
        Me.txtClassification.Location = New System.Drawing.Point(8, 88)
        Me.txtClassification.Name = "txtClassification"
        Me.txtClassification.ReadOnly = True
        Me.txtClassification.Size = New System.Drawing.Size(24, 20)
        Me.txtClassification.TabIndex = 18
        '
        'txtCounty
        '
        Me.txtCounty.Location = New System.Drawing.Point(8, 111)
        Me.txtCounty.Name = "txtCounty"
        Me.txtCounty.ReadOnly = True
        Me.txtCounty.Size = New System.Drawing.Size(24, 20)
        Me.txtCounty.TabIndex = 17
        '
        'txtEngineer
        '
        Me.txtEngineer.Location = New System.Drawing.Point(152, 65)
        Me.txtEngineer.Name = "txtEngineer"
        Me.txtEngineer.ReadOnly = True
        Me.txtEngineer.Size = New System.Drawing.Size(24, 20)
        Me.txtEngineer.TabIndex = 16
        '
        'txtFacility_Name
        '
        Me.txtFacility_Name.Location = New System.Drawing.Point(152, 88)
        Me.txtFacility_Name.Name = "txtFacility_Name"
        Me.txtFacility_Name.ReadOnly = True
        Me.txtFacility_Name.Size = New System.Drawing.Size(24, 20)
        Me.txtFacility_Name.TabIndex = 15
        '
        'txtLast_FCE
        '
        Me.txtLast_FCE.Location = New System.Drawing.Point(152, 111)
        Me.txtLast_FCE.Name = "txtLast_FCE"
        Me.txtLast_FCE.ReadOnly = True
        Me.txtLast_FCE.Size = New System.Drawing.Size(24, 20)
        Me.txtLast_FCE.TabIndex = 14
        '
        'txtLast_Inspection_Date
        '
        Me.txtLast_Inspection_Date.Location = New System.Drawing.Point(152, 134)
        Me.txtLast_Inspection_Date.Name = "txtLast_Inspection_Date"
        Me.txtLast_Inspection_Date.ReadOnly = True
        Me.txtLast_Inspection_Date.Size = New System.Drawing.Size(24, 20)
        Me.txtLast_Inspection_Date.TabIndex = 13
        '
        'txtOperational_Status
        '
        Me.txtOperational_Status.Location = New System.Drawing.Point(152, 157)
        Me.txtOperational_Status.Name = "txtOperational_Status"
        Me.txtOperational_Status.ReadOnly = True
        Me.txtOperational_Status.Size = New System.Drawing.Size(24, 20)
        Me.txtOperational_Status.TabIndex = 12
        '
        'chbLast_Inspection_Date
        '
        Me.chbLast_Inspection_Date.Location = New System.Drawing.Point(184, 132)
        Me.chbLast_Inspection_Date.Name = "chbLast_Inspection_Date"
        Me.chbLast_Inspection_Date.Size = New System.Drawing.Size(128, 24)
        Me.chbLast_Inspection_Date.TabIndex = 11
        Me.chbLast_Inspection_Date.Text = "Last Inspection Date"
        '
        'chbLast_FCE
        '
        Me.chbLast_FCE.Location = New System.Drawing.Point(184, 109)
        Me.chbLast_FCE.Name = "chbLast_FCE"
        Me.chbLast_FCE.Size = New System.Drawing.Size(72, 24)
        Me.chbLast_FCE.TabIndex = 10
        Me.chbLast_FCE.Text = "Last FCE"
        '
        'chbSIC_Code
        '
        Me.chbSIC_Code.Location = New System.Drawing.Point(352, 40)
        Me.chbSIC_Code.Name = "chbSIC_Code"
        Me.chbSIC_Code.Size = New System.Drawing.Size(72, 24)
        Me.chbSIC_Code.TabIndex = 9
        Me.chbSIC_Code.Text = "SIC Code"
        '
        'chbCity
        '
        Me.chbCity.Location = New System.Drawing.Point(40, 63)
        Me.chbCity.Name = "chbCity"
        Me.chbCity.Size = New System.Drawing.Size(48, 24)
        Me.chbCity.TabIndex = 8
        Me.chbCity.Text = "City"
        '
        'chbAIRS_Number
        '
        Me.chbAIRS_Number.Checked = True
        Me.chbAIRS_Number.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbAIRS_Number.Enabled = False
        Me.chbAIRS_Number.ForeColor = System.Drawing.Color.Firebrick
        Me.chbAIRS_Number.Location = New System.Drawing.Point(40, 40)
        Me.chbAIRS_Number.Name = "chbAIRS_Number"
        Me.chbAIRS_Number.Size = New System.Drawing.Size(96, 24)
        Me.chbAIRS_Number.TabIndex = 7
        Me.chbAIRS_Number.Text = "AIRS Number"
        '
        'chbCounty
        '
        Me.chbCounty.Location = New System.Drawing.Point(40, 109)
        Me.chbCounty.Name = "chbCounty"
        Me.chbCounty.Size = New System.Drawing.Size(64, 24)
        Me.chbCounty.TabIndex = 6
        Me.chbCounty.Text = "County"
        '
        'chbOperational_Status
        '
        Me.chbOperational_Status.Location = New System.Drawing.Point(184, 155)
        Me.chbOperational_Status.Name = "chbOperational_Status"
        Me.chbOperational_Status.Size = New System.Drawing.Size(120, 24)
        Me.chbOperational_Status.TabIndex = 5
        Me.chbOperational_Status.Text = "Operational Status"
        '
        'chbClass
        '
        Me.chbClass.Location = New System.Drawing.Point(40, 86)
        Me.chbClass.Name = "chbClass"
        Me.chbClass.Size = New System.Drawing.Size(96, 24)
        Me.chbClass.TabIndex = 4
        Me.chbClass.Text = "Classification"
        '
        'chbDistrict
        '
        Me.chbDistrict.Location = New System.Drawing.Point(40, 132)
        Me.chbDistrict.Name = "chbDistrict"
        Me.chbDistrict.Size = New System.Drawing.Size(64, 24)
        Me.chbDistrict.TabIndex = 3
        Me.chbDistrict.Text = "District"
        '
        'txtAIRS_Number
        '
        Me.txtAIRS_Number.Location = New System.Drawing.Point(8, 42)
        Me.txtAIRS_Number.Name = "txtAIRS_Number"
        Me.txtAIRS_Number.ReadOnly = True
        Me.txtAIRS_Number.Size = New System.Drawing.Size(24, 20)
        Me.txtAIRS_Number.TabIndex = 2
        Me.txtAIRS_Number.Text = "1"
        '
        'chbSSCP_Unit
        '
        Me.chbSSCP_Unit.Location = New System.Drawing.Point(352, 63)
        Me.chbSSCP_Unit.Name = "chbSSCP_Unit"
        Me.chbSSCP_Unit.Size = New System.Drawing.Size(80, 24)
        Me.chbSSCP_Unit.TabIndex = 1
        Me.chbSSCP_Unit.Text = "SSCP Unit"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(159, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Choose columns seen and order"
        '
        'TabPage4
        '
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(439, 206)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "TabPage4"
        '
        'TabPage6
        '
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(439, 206)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "TabPage6"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiFile, Me.MmiEdit, Me.MmiView, Me.MmiTool, Me.MmiHelp})
        '
        'MmiFile
        '
        Me.MmiFile.Index = 0
        Me.MmiFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave, Me.MenuItem10, Me.MmiBack, Me.MenuItem11, Me.MmiExit})
        Me.MmiFile.Text = "File"
        '
        'MmiSave
        '
        Me.MmiSave.Index = 0
        Me.MmiSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.MmiSave.Text = "&Save"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 1
        Me.MenuItem10.Text = "-"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 2
        Me.MmiBack.Text = "Close Profile Tool"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 3
        Me.MenuItem11.Text = "-"
        '
        'MmiExit
        '
        Me.MmiExit.Index = 4
        Me.MmiExit.Text = "Exit"
        '
        'MmiEdit
        '
        Me.MmiEdit.Index = 1
        Me.MmiEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiCut, Me.MmiCopy, Me.MmiPaste})
        Me.MmiEdit.Text = "Edit"
        '
        'MmiCut
        '
        Me.MmiCut.Index = 0
        Me.MmiCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX
        Me.MmiCut.Text = "Cut"
        '
        'MmiCopy
        '
        Me.MmiCopy.Index = 1
        Me.MmiCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.MmiCopy.Text = "&Copy"
        '
        'MmiPaste
        '
        Me.MmiPaste.Index = 2
        Me.MmiPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV
        Me.MmiPaste.Text = "Paste"
        '
        'MmiView
        '
        Me.MmiView.Index = 2
        Me.MmiView.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem20, Me.MmiClearPage, Me.MenuItem7, Me.MmiToolbar})
        Me.MmiView.Text = "View"
        '
        'MenuItem20
        '
        Me.MenuItem20.Index = 0
        Me.MenuItem20.Text = "-"
        '
        'MmiClearPage
        '
        Me.MmiClearPage.Index = 1
        Me.MmiClearPage.Text = "Clear Page"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 2
        Me.MenuItem7.Text = "-"
        '
        'MmiToolbar
        '
        Me.MmiToolbar.Index = 3
        Me.MmiToolbar.Text = "Toolbar"
        '
        'MmiTool
        '
        Me.MmiTool.Index = 3
        Me.MmiTool.Text = "Tool"
        '
        'MmiHelp
        '
        Me.MmiHelp.Index = 4
        Me.MmiHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiFacilityHelp})
        Me.MmiHelp.Text = "Help"
        '
        'MmiFacilityHelp
        '
        Me.MmiFacilityHelp.Index = 0
        Me.MmiFacilityHelp.Text = "Facility Assignment Help"
        '
        'cmFind
        '
        Me.cmFind.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmiEngineerList, Me.cmiFacilityList})
        '
        'cmiEngineerList
        '
        Me.cmiEngineerList.Index = 0
        Me.cmiEngineerList.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmiAllEngineers, Me.MenuItem28, Me.cmiAirToxicsUnitEngineer, Me.cmiChemicals_MineralUnitEngineers, Me.cmiVOC_CombustionUnitEngineers})
        Me.cmiEngineerList.Text = "Engineer's List"
        '
        'cmiAllEngineers
        '
        Me.cmiAllEngineers.Index = 0
        Me.cmiAllEngineers.Text = "All Engineers"
        '
        'MenuItem28
        '
        Me.MenuItem28.Index = 1
        Me.MenuItem28.Text = "-"
        '
        'cmiAirToxicsUnitEngineer
        '
        Me.cmiAirToxicsUnitEngineer.Index = 2
        Me.cmiAirToxicsUnitEngineer.Text = "Air Toxics Unit"
        '
        'cmiChemicals_MineralUnitEngineers
        '
        Me.cmiChemicals_MineralUnitEngineers.Index = 3
        Me.cmiChemicals_MineralUnitEngineers.Text = "Chemicals/Mineral Unit"
        '
        'cmiVOC_CombustionUnitEngineers
        '
        Me.cmiVOC_CombustionUnitEngineers.Index = 4
        Me.cmiVOC_CombustionUnitEngineers.Text = "VOC/Combustion Unit"
        '
        'cmiFacilityList
        '
        Me.cmiFacilityList.Index = 1
        Me.cmiFacilityList.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmiAllFacilities, Me.MenuItem26, Me.cmiAirToxicsUnitFacilities, Me.cmiChemicals_MineralFacilities, Me.cmiVOC_CombustionUnitFacilities, Me.MenuItem27, Me.cmiRegionalFacilities, Me.MenuItem12, Me.cmiUnassignedFacilities})
        Me.cmiFacilityList.Text = "Facility List"
        '
        'cmiAllFacilities
        '
        Me.cmiAllFacilities.Index = 0
        Me.cmiAllFacilities.Text = "All Facilities"
        '
        'MenuItem26
        '
        Me.MenuItem26.Index = 1
        Me.MenuItem26.Text = "-"
        '
        'cmiAirToxicsUnitFacilities
        '
        Me.cmiAirToxicsUnitFacilities.Index = 2
        Me.cmiAirToxicsUnitFacilities.Text = "Air Toxics Unit"
        '
        'cmiChemicals_MineralFacilities
        '
        Me.cmiChemicals_MineralFacilities.Index = 3
        Me.cmiChemicals_MineralFacilities.Text = "Chemicals/Mineral Unit"
        '
        'cmiVOC_CombustionUnitFacilities
        '
        Me.cmiVOC_CombustionUnitFacilities.Index = 4
        Me.cmiVOC_CombustionUnitFacilities.Text = "VOC/Combustion Unit"
        '
        'MenuItem27
        '
        Me.MenuItem27.Index = 5
        Me.MenuItem27.Text = "-"
        '
        'cmiRegionalFacilities
        '
        Me.cmiRegionalFacilities.Index = 6
        Me.cmiRegionalFacilities.Text = "Regional Facilities"
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = 7
        Me.MenuItem12.Text = "-"
        '
        'cmiUnassignedFacilities
        '
        Me.cmiUnassignedFacilities.Index = 8
        Me.cmiUnassignedFacilities.Text = "Unassigned Facilities"
        '
        'TBUserProfileTool
        '
        Me.TBUserProfileTool.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSave, Me.tbbClear, Me.tbbBack, Me.tbbExit})
        Me.TBUserProfileTool.DropDownArrows = True
        Me.TBUserProfileTool.ImageList = Me.Image_List_All
        Me.TBUserProfileTool.Location = New System.Drawing.Point(0, 0)
        Me.TBUserProfileTool.Name = "TBUserProfileTool"
        Me.TBUserProfileTool.ShowToolTips = True
        Me.TBUserProfileTool.Size = New System.Drawing.Size(537, 28)
        Me.TBUserProfileTool.TabIndex = 62
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
        'tbbExit
        '
        Me.tbbExit.ImageIndex = 81
        Me.tbbExit.Name = "tbbExit"
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
        'PanelUserProfile
        '
        Me.PanelUserProfile.Controls.Add(Me.TCUser_Profile_Tool)
        Me.PanelUserProfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelUserProfile.Location = New System.Drawing.Point(0, 28)
        Me.PanelUserProfile.Name = "PanelUserProfile"
        Me.PanelUserProfile.Size = New System.Drawing.Size(537, 268)
        Me.PanelUserProfile.TabIndex = 63
        '
        'IAIPUser_Profile_Tool
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(537, 296)
        Me.Controls.Add(Me.PanelUserProfile)
        Me.Controls.Add(Me.TBUserProfileTool)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "IAIPUser_Profile_Tool"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "User Profile Tool"
        Me.TCUser_Profile_Tool.ResumeLayout(False)
        Me.tbUser_Profile.ResumeLayout(False)
        Me.User_Profile.ResumeLayout(False)
        Me.User_Profile.PerformLayout()
        Me.tbISMPEngineerStats.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.tbSSCP_UC_Facility_Assignment_Profile.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.PanelUserProfile.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub User_Profile_Tool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor
            CreateStatusBar()
            ShowTabs()

            DTPUnitStart.Text = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPUnitEnd.Text = OracleDate
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.User_Profile_Tool_Load")
        Finally
           
        End Try
        Cursor = Cursors.Default

    End Sub
#Region "Page Load Functions"
    Sub CreateStatusBar()
        Try
            Cursor = Cursors.WaitCursor
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
            ErrorReport(ex.ToString(), "ProfileTool.CreateStatusBar")
        Finally
        
        End Try
        Cursor = Cursors.Default

    End Sub
    Private Sub ShowTabs()
        Dim temp As String
        Dim count As Int32 = 0

        Try
            Cursor = Cursors.WaitCursor
            temp = Permissions

            'If APB310.Visible Then
            TCUser_Profile_Tool.TabPages.Remove(tbUser_Profile)
            TCUser_Profile_Tool.TabPages.Remove(tbSSCP_UC_Facility_Assignment_Profile)
            TCUser_Profile_Tool.TabPages.Remove(tbISMPEngineerStats)
            TCUser_Profile_Tool.TabPages.Remove(TabPage4)
            TCUser_Profile_Tool.TabPages.Remove(TabPage5)
            TCUser_Profile_Tool.TabPages.Remove(TabPage6)

            TCUser_Profile_Tool.TabPages.Add(tbUser_Profile)
            Load_User_Profile()

            If Mid(Permissions, 24, 1) <> 0 Or Mid(Permissions, 25, 1) <> 0 Then
                TCUser_Profile_Tool.TabPages.Add(tbSSCP_UC_Facility_Assignment_Profile)
                Load_UC_Facility_Assignment_Profile()
            End If
            If Mid(Permissions, 7, 2) <> 0 Then
                TCUser_Profile_Tool.TabPages.Add(tbISMPEngineerStats)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.ShowTabs")
        Finally
           
        End Try
        Cursor = Cursors.Default

    End Sub
    Private Sub Load_User_Profile()
        Dim SQL As String

        Try
            Cursor = Cursors.WaitCursor
            If UserGCode <> "" Then
                SQL = "Select " & _
                "" & connNameSpace & ".APBUsers.strUserLogID, strUserFirstName, " & _
                "strUserLastName, strUserPassword " & _
                "from " & connNameSpace & ".APBUsers " & _
                "where " & _
                "strUserGCode = '" & UserGCode & "'"

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    txtUser_First_Name.Text = dr.Item("strUserFirstName")
                    txtUser_Last_Name.Text = dr.Item("strUserLastName")
                    txtUser_Log_In.Text = dr.Item("strUserLogID")
                    txtUser_Password.Text = EncryptDecrypt.DecryptText(dr.Item("strUserPassword"))
                End While
                dr.Close()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.Load_User_Profile")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub Load_UC_Facility_Assignment_Profile()
        Dim SQL As String = ""
        Dim temp As String = ""
        Dim temp2 As String = ""

        Try
            Cursor = Cursors.WaitCursor

            If UserGCode <> "" Then
                SQL = "Select strSSCPFacilityAssignment from " & connNameSpace & ".APBUsers where " & connNameSpace & ".APBUsers.strUserGCode = '" & UserGCode & "' "

                Dim cmd As New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim dr As OracleDataReader = cmd.ExecuteReader
                While dr.Read
                    temp = dr.Item("strSSCPFacilityAssignment")
                End While
             

                If Mid(temp, 2, 1) <> "0" Then
                    temp2 = Mid(temp, 2, 1)
                    chbAIRS_Number.Checked = True
                    Select Case temp2
                        Case 1
                            txtAIRS_Number.Text = "1"
                        Case 2
                            txtAIRS_Number.Text = "2"
                        Case 3
                            txtAIRS_Number.Text = "3"
                        Case 4
                            txtAIRS_Number.Text = "4"
                        Case 5
                            txtAIRS_Number.Text = "5"
                        Case 6
                            txtAIRS_Number.Text = "6"
                        Case 7
                            txtAIRS_Number.Text = "7"
                        Case 8
                            txtAIRS_Number.Text = "8"
                        Case 9
                            txtAIRS_Number.Text = "9"
                        Case "A"
                            txtAIRS_Number.Text = "10"
                        Case "B"
                            txtAIRS_Number.Text = "11"
                        Case "C"
                            txtAIRS_Number.Text = "12"
                        Case "D"
                            txtAIRS_Number.Text = "13"
                        Case "E"
                            txtAIRS_Number.Text = "14"
                        Case Else
                            txtAIRS_Number.Text = "1"
                    End Select
                End If
                If Mid(temp, 3, 1) <> "0" Then
                    temp2 = Mid(temp, 3, 1)
                    chbCity.Checked = True
                    Select Case temp2
                        Case 1
                            txtCity.Text = "1"
                        Case 2
                            txtCity.Text = "2"
                        Case 3
                            txtCity.Text = "3"
                        Case 4
                            txtCity.Text = "4"
                        Case 5
                            txtCity.Text = "5"
                        Case 6
                            txtCity.Text = "6"
                        Case 7
                            txtCity.Text = "7"
                        Case 8
                            txtCity.Text = "8"
                        Case 9
                            txtCity.Text = "9"
                        Case "A"
                            txtCity.Text = "10"
                        Case "B"
                            txtCity.Text = "11"
                        Case "C"
                            txtCity.Text = "12"
                        Case "D"
                            txtCity.Text = "13"
                        Case "E"
                            txtCity.Text = "14"
                        Case Else
                            txtCity.Text = ""
                    End Select
                End If
                If Mid(temp, 4, 1) <> "0" Then
                    temp2 = Mid(temp, 4, 1)
                    chbClass.Checked = True
                    Select Case temp2
                        Case 1
                            txtClassification.Text = "1"
                        Case 2
                            txtClassification.Text = "2"
                        Case 3
                            txtClassification.Text = "3"
                        Case 4
                            txtClassification.Text = "4"
                        Case 5
                            txtClassification.Text = "5"
                        Case 6
                            txtClassification.Text = "6"
                        Case 7
                            txtClassification.Text = "7"
                        Case 8
                            txtClassification.Text = "8"
                        Case 9
                            txtClassification.Text = "9"
                        Case "A"
                            txtClassification.Text = "10"
                        Case "B"
                            txtClassification.Text = "11"
                        Case "C"
                            txtClassification.Text = "12"
                        Case "D"
                            txtClassification.Text = "13"
                        Case "E"
                            txtClassification.Text = "14"
                        Case Else
                            txtClassification.Text = ""
                    End Select
                End If
                If Mid(temp, 5, 1) <> "0" Then
                    temp2 = Mid(temp, 5, 1)
                    chbCounty.Checked = True
                    Select Case temp2
                        Case 1
                            txtCounty.Text = "1"
                        Case 2
                            txtCounty.Text = "2"
                        Case 3
                            txtCounty.Text = "3"
                        Case 4
                            txtCounty.Text = "4"
                        Case 5
                            txtCounty.Text = "5"
                        Case 6
                            txtCounty.Text = "6"
                        Case 7
                            txtCounty.Text = "7"
                        Case 8
                            txtCounty.Text = "8"
                        Case 9
                            txtCounty.Text = "9"
                        Case "A"
                            txtCounty.Text = "10"
                        Case "B"
                            txtCounty.Text = "11"
                        Case "C"
                            txtCounty.Text = "12"
                        Case "D"
                            txtCounty.Text = "13"
                        Case "E"
                            txtCounty.Text = "14"
                        Case Else
                            txtCounty.Text = ""
                    End Select
                End If
                If Mid(temp, 6, 1) <> "0" Then
                    temp2 = Mid(temp, 6, 1)
                    chbDistrict.Checked = True
                    Select Case temp2
                        Case 1
                            txtDistrict.Text = "1"
                        Case 2
                            txtDistrict.Text = "2"
                        Case 3
                            txtDistrict.Text = "3"
                        Case 4
                            txtDistrict.Text = "4"
                        Case 5
                            txtDistrict.Text = "5"
                        Case 6
                            txtDistrict.Text = "6"
                        Case 7
                            txtDistrict.Text = "7"
                        Case 8
                            txtDistrict.Text = "8"
                        Case 9
                            txtDistrict.Text = "9"
                        Case "A"
                            txtDistrict.Text = "10"
                        Case "B"
                            txtDistrict.Text = "11"
                        Case "C"
                            txtDistrict.Text = "12"
                        Case "D"
                            txtDistrict.Text = "13"
                        Case "E"
                            txtDistrict.Text = "14"
                        Case Else
                            txtDistrict.Text = ""
                    End Select
                End If
                If Mid(temp, 7, 1) <> "0" Then
                    temp2 = Mid(temp, 7, 1)
                    chbDistrict_Engineer.Checked = True
                    Select Case temp2
                        Case 1
                            txtDistrict_Engineer.Text = "1"
                        Case 2
                            txtDistrict_Engineer.Text = "2"
                        Case 3
                            txtDistrict_Engineer.Text = "3"
                        Case 4
                            txtDistrict_Engineer.Text = "4"
                        Case 5
                            txtDistrict_Engineer.Text = "5"
                        Case 6
                            txtDistrict_Engineer.Text = "6"
                        Case 7
                            txtDistrict_Engineer.Text = "7"
                        Case 8
                            txtDistrict_Engineer.Text = "8"
                        Case 9
                            txtDistrict_Engineer.Text = "9"
                        Case "A"
                            txtDistrict_Engineer.Text = "10"
                        Case "B"
                            txtDistrict_Engineer.Text = "11"
                        Case "C"
                            txtDistrict_Engineer.Text = "12"
                        Case "D"
                            txtDistrict_Engineer.Text = "13"
                        Case "E"
                            txtDistrict_Engineer.Text = "14"
                        Case Else
                            txtDistrict_Engineer.Text = ""
                    End Select
                End If
                If Mid(temp, 8, 1) <> "0" Then
                    temp2 = Mid(temp, 8, 1)
                    chbDistrictResponsible.Checked = True
                    Select Case temp2
                        Case 1
                            txtDistrictResponsible.Text = "1"
                        Case 2
                            txtDistrictResponsible.Text = "2"
                        Case 3
                            txtDistrictResponsible.Text = "3"
                        Case 4
                            txtDistrictResponsible.Text = "4"
                        Case 5
                            txtDistrictResponsible.Text = "5"
                        Case 6
                            txtDistrictResponsible.Text = "6"
                        Case 7
                            txtDistrictResponsible.Text = "7"
                        Case 8
                            txtDistrictResponsible.Text = "8"
                        Case 9
                            txtDistrictResponsible.Text = "9"
                        Case "A"
                            txtDistrictResponsible.Text = "10"
                        Case "B"
                            txtDistrictResponsible.Text = "11"
                        Case "C"
                            txtDistrictResponsible.Text = "12"
                        Case "D"
                            txtDistrictResponsible.Text = "13"
                        Case "E"
                            txtDistrictResponsible.Text = "14"
                        Case Else
                            txtDistrictResponsible.Text = ""
                    End Select
                End If
                If Mid(temp, 9, 1) <> "0" Then
                    temp2 = Mid(temp, 9, 1)
                    chbEngineer.Checked = True
                    Select Case temp2
                        Case 1
                            txtEngineer.Text = "1"
                        Case 2
                            txtEngineer.Text = "2"
                        Case 3
                            txtEngineer.Text = "3"
                        Case 4
                            txtEngineer.Text = "4"
                        Case 5
                            txtEngineer.Text = "5"
                        Case 6
                            txtEngineer.Text = "6"
                        Case 7
                            txtEngineer.Text = "7"
                        Case 8
                            txtEngineer.Text = "8"
                        Case 9
                            txtEngineer.Text = "9"
                        Case "A"
                            txtEngineer.Text = "10"
                        Case "B"
                            txtEngineer.Text = "11"
                        Case "C"
                            txtEngineer.Text = "12"
                        Case "D"
                            txtEngineer.Text = "13"
                        Case "E"
                            txtEngineer.Text = "14"
                        Case Else
                            txtEngineer.Text = ""
                    End Select
                End If
                If Mid(temp, 10, 1) <> "0" Then
                    temp2 = Mid(temp, 10, 1)
                    chbFacility_Name.Checked = True
                    Select Case temp2
                        Case 1
                            txtFacility_Name.Text = "1"
                        Case 2
                            txtFacility_Name.Text = "2"
                        Case 3
                            txtFacility_Name.Text = "3"
                        Case 4
                            txtFacility_Name.Text = "4"
                        Case 5
                            txtFacility_Name.Text = "5"
                        Case 6
                            txtFacility_Name.Text = "6"
                        Case 7
                            txtFacility_Name.Text = "7"
                        Case 8
                            txtFacility_Name.Text = "8"
                        Case 9
                            txtFacility_Name.Text = "9"
                        Case "A"
                            txtFacility_Name.Text = "10"
                        Case "B"
                            txtFacility_Name.Text = "11"
                        Case "C"
                            txtFacility_Name.Text = "12"
                        Case "D"
                            txtFacility_Name.Text = "13"
                        Case "E"
                            txtFacility_Name.Text = "14"
                        Case Else
                            txtFacility_Name.Text = ""
                    End Select
                End If
                If Mid(temp, 11, 1) <> "0" Then
                    temp2 = Mid(temp, 11, 1)
                    chbLast_FCE.Checked = True
                    Select Case temp2
                        Case 1
                            txtLast_FCE.Text = "1"
                        Case 2
                            txtLast_FCE.Text = "2"
                        Case 3
                            txtLast_FCE.Text = "3"
                        Case 4
                            txtLast_FCE.Text = "4"
                        Case 5
                            txtLast_FCE.Text = "5"
                        Case 6
                            txtLast_FCE.Text = "6"
                        Case 7
                            txtLast_FCE.Text = "7"
                        Case 8
                            txtLast_FCE.Text = "8"
                        Case 9
                            txtLast_FCE.Text = "9"
                        Case "A"
                            txtLast_FCE.Text = "10"
                        Case "B"
                            txtLast_FCE.Text = "11"
                        Case "C"
                            txtLast_FCE.Text = "12"
                        Case "D"
                            txtLast_FCE.Text = "13"
                        Case "E"
                            txtLast_FCE.Text = "14"
                        Case Else
                            txtLast_FCE.Text = ""
                    End Select
                End If
                If Mid(temp, 12, 1) <> "0" Then
                    temp2 = Mid(temp, 12, 1)
                    chbLast_Inspection_Date.Checked = True
                    Select Case temp2
                        Case 1
                            txtLast_Inspection_Date.Text = "1"
                        Case 2
                            txtLast_Inspection_Date.Text = "2"
                        Case 3
                            txtLast_Inspection_Date.Text = "3"
                        Case 4
                            txtLast_Inspection_Date.Text = "4"
                        Case 5
                            txtLast_Inspection_Date.Text = "5"
                        Case 6
                            txtLast_Inspection_Date.Text = "6"
                        Case 7
                            txtLast_Inspection_Date.Text = "7"
                        Case 8
                            txtLast_Inspection_Date.Text = "8"
                        Case 9
                            txtLast_Inspection_Date.Text = "9"
                        Case "A"
                            txtLast_Inspection_Date.Text = "10"
                        Case "B"
                            txtLast_Inspection_Date.Text = "11"
                        Case "C"
                            txtLast_Inspection_Date.Text = "12"
                        Case "D"
                            txtLast_Inspection_Date.Text = "13"
                        Case "E"
                            txtLast_Inspection_Date.Text = "14"
                        Case Else
                            txtLast_Inspection_Date.Text = ""
                    End Select
                End If
                If Mid(temp, 13, 1) <> "0" Then
                    temp2 = Mid(temp, 13, 1)
                    chbOperational_Status.Checked = True
                    Select Case temp2
                        Case 1
                            txtOperational_Status.Text = "1"
                        Case 2
                            txtOperational_Status.Text = "2"
                        Case 3
                            txtOperational_Status.Text = "3"
                        Case 4
                            txtOperational_Status.Text = "4"
                        Case 5
                            txtOperational_Status.Text = "5"
                        Case 6
                            txtOperational_Status.Text = "6"
                        Case 7
                            txtOperational_Status.Text = "7"
                        Case 8
                            txtOperational_Status.Text = "8"
                        Case 9
                            txtOperational_Status.Text = "9"
                        Case "A"
                            txtOperational_Status.Text = "10"
                        Case "B"
                            txtOperational_Status.Text = "11"
                        Case "C"
                            txtOperational_Status.Text = "12"
                        Case "D"
                            txtOperational_Status.Text = "13"
                        Case "E"
                            txtOperational_Status.Text = "14"
                        Case Else
                            txtOperational_Status.Text = ""
                    End Select
                End If
                If Mid(temp, 14, 1) <> "0" Then
                    temp2 = Mid(temp, 14, 1)
                    chbSIC_Code.Checked = True
                    Select Case temp2
                        Case 1
                            txtSIC_Code.Text = "1"
                        Case 2
                            txtSIC_Code.Text = "2"
                        Case 3
                            txtSIC_Code.Text = "3"
                        Case 4
                            txtSIC_Code.Text = "4"
                        Case 5
                            txtSIC_Code.Text = "5"
                        Case 6
                            txtSIC_Code.Text = "6"
                        Case 7
                            txtSIC_Code.Text = "7"
                        Case 8
                            txtSIC_Code.Text = "8"
                        Case 9
                            txtSIC_Code.Text = "9"
                        Case "A"
                            txtSIC_Code.Text = "10"
                        Case "B"
                            txtSIC_Code.Text = "11"
                        Case "C"
                            txtSIC_Code.Text = "12"
                        Case "D"
                            txtSIC_Code.Text = "13"
                        Case "E"
                            txtSIC_Code.Text = "14"
                        Case Else
                            txtSIC_Code.Text = ""
                    End Select
                End If
                If Mid(temp, 15, 1) <> "0" Then
                    temp2 = Mid(temp, 15, 1)
                    chbSSCP_Unit.Checked = True
                    Select Case temp2
                        Case 1
                            txtSSCP_Unit.Text = "1"
                        Case 2
                            txtSSCP_Unit.Text = "2"
                        Case 3
                            txtSSCP_Unit.Text = "3"
                        Case 4
                            txtSSCP_Unit.Text = "4"
                        Case 5
                            txtSSCP_Unit.Text = "5"
                        Case 6
                            txtSSCP_Unit.Text = "6"
                        Case 7
                            txtSSCP_Unit.Text = "7"
                        Case 8
                            txtSSCP_Unit.Text = "8"
                        Case 9
                            txtSSCP_Unit.Text = "9"
                        Case "A"
                            txtSSCP_Unit.Text = "10"
                        Case "B"
                            txtSSCP_Unit.Text = "11"
                        Case "C"
                            txtSSCP_Unit.Text = "12"
                        Case "D"
                            txtSSCP_Unit.Text = "13"
                        Case "E"
                            txtSSCP_Unit.Text = "14"
                        Case Else
                            txtSSCP_Unit.Text = ""
                    End Select

                    temp2 = Mid(temp, 1, 1)
                    Select Case temp2
                        Case "A"
                            txtCheckBox_Count.Text = "10"
                        Case "B"
                            txtCheckBox_Count.Text = 11
                        Case "C"
                            txtCheckBox_Count.Text = 12
                        Case "D"
                            txtCheckBox_Count.Text = 13
                        Case "E"
                            txtCheckBox_Count.Text = 14
                        Case Else
                            txtCheckBox_Count.Text = Mid(temp, 1, 1)
                    End Select
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.Load_UC_Facility_Assignment_Profile")
        Finally
            
        End Try
        Cursor = Cursors.Default
    End Sub
#End Region
#Region "Update Functions"
    Private Sub Update_User_Profile()
        Dim DefaultsText As String = ""
        Dim LogInID As String = ""
        Dim Password As String = ""
        Dim LogInCount As String = ""
        Dim temp As String = ""

        Try
            Cursor = Cursors.WaitCursor
            If txtNew_User_First_Name.Text = "" Then
                txtNew_User_First_Name.Text = txtUser_First_Name.Text
            End If
            If txtNew_User_Last_Name.Text = "" Then
                txtNew_User_Last_Name.Text = txtUser_Last_Name.Text
            End If
            If txtNew_User_Log_In.Text = "" Then
                txtNew_User_Log_In.Text = txtUser_Log_In.Text
            End If
            If txtNew_User_Password.Text = "" Then
                txtNew_User_Password.Text = txtUser_Password.Text
            End If

            'If NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT" Then
            SQL = "Update " & connNameSpace & ".APBUsers set strUserLogID = '" & txtNew_User_Log_In.Text & "', " & _
            "strUserFirstName = '" & Replace(txtNew_User_First_Name.Text, "'", "''") & "', " & _
            "strUserLastName = '" & Replace(txtNew_User_Last_Name.Text, "'", "''") & "', " & _
            "strUserpassword = '" & Replace(EncryptDecrypt.EncryptText(txtNew_User_Password.Text), "'", "''") & "' " & _
            "where strUserLogID = '" & txtUser_Log_In.Text & "'"
            'Else
            'SQL = "Update " & connNameSpace & ".APBUsers set strUserLogID = '" & txtNew_User_Log_In.Text & "', " & _
            '"strUserFirstName = '" & Replace(txtNew_User_First_Name.Text, "'", "''") & "', " & _
            '"strUserLastName = '" & Replace(txtNew_User_Last_Name.Text, "'", "''") & "', " & _
            '"strUserpassword = '" & Replace(txtNew_User_Password.Text, "'", "''") & "' " & _
            '"where strUserLogID = '" & txtUser_Log_In.Text & "'"
            'End If
            cmd = New OracleCommand(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Try
                Cursor = Cursors.WaitCursor
                dr = cmd.ExecuteReader

            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try

            UserName = txtNew_User_First_Name.Text & " " & txtNew_User_Last_Name.Text

            Dim result As DialogResult
            result = MessageBox.Show("Your Information has been UPDATED" + vbCrLf + "Do you want to clear this form?" + vbCrLf, "Profile UPDATE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = Windows.Forms.DialogResult.Yes Then
                txtUser_First_Name.Text = txtNew_User_First_Name.Text
                txtUser_Last_Name.Text = txtNew_User_Last_Name.Text
                txtUser_Log_In.Text = txtNew_User_Log_In.Text
                txtUser_Password.Text = txtNew_User_Password.Text

                txtNew_User_First_Name.Text = ""
                txtNew_User_Last_Name.Text = ""
                txtNew_User_Log_In.Text = ""
                txtNew_User_Password.Text = ""
            Else
                txtUser_First_Name.Text = txtNew_User_First_Name.Text
                txtUser_Last_Name.Text = txtNew_User_Last_Name.Text
                txtUser_Log_In.Text = txtNew_User_Log_In.Text
                txtUser_Password.Text = txtNew_User_Password.Text

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.Update_User_Profile")
        Finally

        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub Update_UC_Facility_Assignment_Profile()
        Dim SQL As String
        Dim SQL_Key As String

        Try
            Cursor = Cursors.WaitCursor

            Select Case txtCheckBox_Count.Text
                Case "0"
                    SQL_Key = 0
                Case "1"
                    SQL_Key = 1
                Case "2"
                    SQL_Key = 2
                Case "3"
                    SQL_Key = 3
                Case "4"
                    SQL_Key = 4
                Case "5"
                    SQL_Key = 5
                Case "6"
                    SQL_Key = 6
                Case "7"
                    SQL_Key = 7
                Case "8"
                    SQL_Key = 8
                Case "9"
                    SQL_Key = 9
                Case "10"
                    SQL_Key = "A"
                Case "11"
                    SQL_Key = "B"
                Case "12"
                    SQL_Key = "C"
                Case "13"
                    SQL_Key = "D"
                Case "14"
                    SQL_Key = "E"
                Case Else
                    SQL_Key = 0
            End Select

            Select Case txtAIRS_Number.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtCity.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtClassification.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtCounty.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtDistrict.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtDistrict_Engineer.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtDistrictResponsible.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtEngineer.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtFacility_Name.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtLast_FCE.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtLast_Inspection_Date.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtOperational_Status.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtSIC_Code.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            Select Case txtSSCP_Unit.Text
                Case 1
                    SQL_Key = SQL_Key & 1
                Case 2
                    SQL_Key = SQL_Key & 2
                Case 3
                    SQL_Key = SQL_Key & 3
                Case 4
                    SQL_Key = SQL_Key & 4
                Case 5
                    SQL_Key = SQL_Key & 5
                Case 6
                    SQL_Key = SQL_Key & 6
                Case 7
                    SQL_Key = SQL_Key & 7
                Case 8
                    SQL_Key = SQL_Key & 8
                Case 9
                    SQL_Key = SQL_Key & 9
                Case 10
                    SQL_Key = SQL_Key & "A"
                Case 11
                    SQL_Key = SQL_Key & "B"
                Case 12
                    SQL_Key = SQL_Key & "C"
                Case 13
                    SQL_Key = SQL_Key & "D"
                Case 14
                    SQL_Key = SQL_Key & "E"
                Case Else
                    SQL_Key = SQL_Key & 0
            End Select

            SQL_Key = SQL_Key

            SQL = "Update " & connNameSpace & ".APBUsers set strSSCPFacilityAssignment = '" & SQL_Key & "' " & _
            "where strUserGCode = '" & UserGCode & "'"

            Dim cmd As New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim dr As OracleDataReader = cmd.ExecuteReader
            

            MsgBox("done", MsgBoxStyle.Information, "Profile Tool")

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.Update_UC_Facility_Assignment_Profile")
        Finally
         
        End Try
        Cursor = Cursors.Default

    End Sub

#End Region
#Region "tbSSCP_UC_Facility_Assignment_Profile Subs"
    Private Sub Click_Actions()
        Try
            Cursor = Cursors.WaitCursor
            chbAIRS_Number.Checked = False
            chbCity.Checked = False
            chbClass.Checked = False
            chbCounty.Checked = False
            chbDistrict.Checked = False
            chbDistrict_Engineer.Checked = False
            chbEngineer.Checked = False
            chbFacility_Name.Checked = False
            chbLast_FCE.Checked = False
            chbLast_Inspection_Date.Checked = False
            chbOperational_Status.Checked = False
            chbSIC_Code.Checked = False
            chbSSCP_Unit.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.Click_Actions")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
#End Region
    Private Sub MmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiSave.Click
        Try
            Cursor = Cursors.WaitCursor
            If tbUser_Profile.Visible Then
                Update_User_Profile()
            End If
            If tbSSCP_UC_Facility_Assignment_Profile.Visible Then
                Update_UC_Facility_Assignment_Profile()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.MmiSave_Click")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub ClearUser_profile()
        Try
            Cursor = Cursors.WaitCursor
            txtNew_User_First_Name.Clear()
            txtNew_User_Last_Name.Clear()
            txtNew_User_Log_In.Clear()
            txtNew_User_Password.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.ClearUser_profile")
        Finally
        
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub ClearSSCP_UC_Fac_Assignemnt()
        Try
            Cursor = Cursors.WaitCursor
            Me.chbAIRS_Number.Checked = True
            txtAIRS_Number.Text = "1"
            Me.chbCity.Checked = False
            txtCity.Text = ""
            Me.chbClass.Checked = False
            txtClassification.Text = ""
            Me.chbCounty.Checked = False
            txtCounty.Text = ""
            Me.chbDistrict.Checked = False
            txtDistrict.Text = ""
            Me.chbDistrict_Engineer.Checked = False
            txtDistrict_Engineer.Text = ""
            Me.chbDistrictResponsible.Checked = False
            txtDistrictResponsible.Text = ""
            Me.chbEngineer.Checked = False
            txtEngineer.Text = ""
            Me.chbFacility_Name.Checked = False
            txtFacility_Name.Text = ""
            Me.chbLast_FCE.Checked = False
            txtLast_FCE.Text = ""
            Me.chbLast_Inspection_Date.Checked = False
            txtLast_Inspection_Date.Text = ""
            Me.chbOperational_Status.Checked = False
            txtOperational_Status.Text = ""
            Me.chbSIC_Code.Checked = False
            txtSIC_Code.Text = ""
            Me.chbSSCP_Unit.Checked = False
            txtCheckBox_Count.Text = "1"

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.ClearSSCP_UC_Fac_Assignemnt")
        Finally
         
        End Try
        Cursor = Cursors.Default
    End Sub
#Region "Click Actions for UC Facility Assignment Tab"
    Private Sub Click_Action_Remove(ByVal temp As Integer)
        'Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor

            Dim temp2 As Integer

            If txtAIRS_Number.Text <> "" Then
                temp2 = txtAIRS_Number.Text
                If temp2 >= temp Then
                    txtAIRS_Number.Text = txtAIRS_Number.Text - 1
                End If
            End If
            If txtCity.Text <> "" Then
                temp2 = txtCity.Text
                If temp2 >= temp Then
                    txtCity.Text = txtCity.Text - 1
                End If
            End If
            If txtClassification.Text <> "" Then
                temp2 = txtClassification.Text
                If temp2 >= temp Then
                    txtClassification.Text = txtClassification.Text - 1
                End If
            End If
            If txtCounty.Text <> "" Then
                temp2 = txtCounty.Text
                If temp2 >= temp Then
                    txtCounty.Text = txtCounty.Text - 1
                End If
            End If
            If txtDistrict.Text <> "" Then
                temp2 = txtDistrict.Text
                If temp2 >= temp Then
                    txtDistrict.Text = txtDistrict.Text - 1
                End If
            End If
            If txtDistrict_Engineer.Text <> "" Then
                temp2 = txtDistrict_Engineer.Text
                If temp2 >= temp Then
                    txtDistrict_Engineer.Text = txtDistrict_Engineer.Text - 1
                End If
            End If
            If txtDistrictResponsible.Text <> "" Then
                temp2 = txtDistrictResponsible.Text
                If temp2 >= temp Then
                    txtDistrictResponsible.Text = txtDistrictResponsible.Text - 1
                End If
            End If
            If txtEngineer.Text <> "" Then
                temp2 = txtEngineer.Text
                If temp2 >= temp Then
                    txtEngineer.Text = txtEngineer.Text - 1
                End If
            End If
            If txtFacility_Name.Text <> "" Then
                temp2 = txtFacility_Name.Text
                If temp2 >= temp Then
                    txtFacility_Name.Text = txtFacility_Name.Text - 1
                End If
            End If
            If txtLast_FCE.Text <> "" Then
                temp2 = txtLast_FCE.Text
                If temp2 >= temp Then
                    txtLast_FCE.Text = txtLast_FCE.Text - 1
                End If
            End If
            If txtLast_Inspection_Date.Text <> "" Then
                temp2 = txtLast_Inspection_Date.Text
                If temp2 >= temp Then
                    txtLast_Inspection_Date.Text = txtLast_Inspection_Date.Text - 1
                End If
            End If
            If txtOperational_Status.Text <> "" Then
                temp2 = txtOperational_Status.Text
                If temp2 >= temp Then
                    txtOperational_Status.Text = txtOperational_Status.Text - 1
                End If
            End If
            If txtSIC_Code.Text <> "" Then
                temp2 = txtSIC_Code.Text
                If temp2 >= temp Then
                    txtSIC_Code.Text = txtSIC_Code.Text - 1
                End If
            End If
            If txtSSCP_Unit.Text <> "" Then
                temp2 = txtSSCP_Unit.Text
                If temp2 >= temp Then
                    txtSSCP_Unit.Text = txtSSCP_Unit.Text - 1
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.Click_Action_Remove")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbAIRS_Number_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbAIRS_Number.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor

            If chbAIRS_Number.Checked = True Then
                Select Case temp
                    Case "0"
                        txtAIRS_Number.Text = 1
                    Case "1"
                        txtAIRS_Number.Text = 2
                    Case "2"
                        txtAIRS_Number.Text = 3
                    Case "3"
                        txtAIRS_Number.Text = 4
                    Case "4"
                        txtAIRS_Number.Text = 5
                    Case "5"
                        txtAIRS_Number.Text = 6
                    Case "6"
                        txtAIRS_Number.Text = 7
                    Case "7"
                        txtAIRS_Number.Text = 8
                    Case "8"
                        txtAIRS_Number.Text = 9
                    Case "9"
                        txtAIRS_Number.Text = 10
                    Case "10"
                        txtAIRS_Number.Text = 11
                    Case "11"
                        txtAIRS_Number.Text = 12
                    Case "12"
                        txtAIRS_Number.Text = 13
                    Case "13"
                        txtAIRS_Number.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "13"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "12"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "11"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "10"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "9"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "8"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "7"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "6"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "5"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "4"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "3"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "2"
                        Click_Action_Remove(txtAIRS_Number.Text)
                        txtAIRS_Number.Text = ""
                    Case "1"
                        txtAIRS_Number.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbAIRS_Number_CheckedChanged")
        Finally
        
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbCity_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbCity.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbCity.Checked = True Then
                Select Case temp
                    Case "0"
                        txtCity.Text = 1
                    Case "1"
                        txtCity.Text = 2
                    Case "2"
                        txtCity.Text = 3
                    Case "3"
                        txtCity.Text = 4
                    Case "4"
                        txtCity.Text = 5
                    Case "5"
                        txtCity.Text = 6
                    Case "6"
                        txtCity.Text = 7
                    Case "7"
                        txtCity.Text = 8
                    Case "8"
                        txtCity.Text = 9
                    Case "9"
                        txtCity.Text = 10
                    Case "10"
                        txtCity.Text = 11
                    Case "11"
                        txtCity.Text = 12
                    Case "12"
                        txtCity.Text = 13
                    Case "13"
                        txtCity.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "13"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "12"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "11"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "10"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "9"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "8"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "7"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "6"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "5"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "4"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "3"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "2"
                        Click_Action_Remove(txtCity.Text)
                        txtCity.Text = ""
                    Case "1"
                        txtCity.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbCity_CheckedChanged")
        Finally
         
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbClass_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbClass.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbClass.Checked = True Then
                Select Case temp
                    Case "0"
                        txtClassification.Text = 1
                    Case "1"
                        txtClassification.Text = 2
                    Case "2"
                        txtClassification.Text = 3
                    Case "3"
                        txtClassification.Text = 4
                    Case "4"
                        txtClassification.Text = 5
                    Case "5"
                        txtClassification.Text = 6
                    Case "6"
                        txtClassification.Text = 7
                    Case "7"
                        txtClassification.Text = 8
                    Case "8"
                        txtClassification.Text = 9
                    Case "9"
                        txtClassification.Text = 10
                    Case "10"
                        txtClassification.Text = 11
                    Case "11"
                        txtClassification.Text = 12
                    Case "12"
                        txtClassification.Text = 13
                    Case "13"
                        txtClassification.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "13"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "12"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "11"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "10"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "9"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "8"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "7"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "6"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "5"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "4"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "3"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "2"
                        Click_Action_Remove(txtClassification.Text)
                        txtClassification.Text = ""
                    Case "1"
                        txtClassification.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbClass_CheckedChanged")
        Finally
          
        End Try
        Cursor = Cursors.Default

    End Sub
    Private Sub chbCounty_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbCounty.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbCounty.Checked = True Then
                Select Case temp
                    Case "0"
                        txtCounty.Text = 1
                    Case "1"
                        txtCounty.Text = 2
                    Case "2"
                        txtCounty.Text = 3
                    Case "3"
                        txtCounty.Text = 4
                    Case "4"
                        txtCounty.Text = 5
                    Case "5"
                        txtCounty.Text = 6
                    Case "6"
                        txtCounty.Text = 7
                    Case "7"
                        txtCounty.Text = 8
                    Case "8"
                        txtCounty.Text = 9
                    Case "9"
                        txtCounty.Text = 10
                    Case "10"
                        txtCounty.Text = 11
                    Case "11"
                        txtCounty.Text = 12
                    Case "12"
                        txtCounty.Text = 13
                    Case "13"
                        txtCounty.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "13"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "12"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "11"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "10"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "9"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "8"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "7"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "6"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "5"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "4"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "3"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "2"
                        Click_Action_Remove(txtCounty.Text)
                        txtCounty.Text = ""
                    Case "1"
                        txtCounty.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbCounty_CheckedChanged")
        Finally
         
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbDistrict_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbDistrict.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbDistrict.Checked = True Then
                Select Case temp
                    Case "0"
                        txtDistrict.Text = 1
                    Case "1"
                        txtDistrict.Text = 2
                    Case "2"
                        txtDistrict.Text = 3
                    Case "3"
                        txtDistrict.Text = 4
                    Case "4"
                        txtDistrict.Text = 5
                    Case "5"
                        txtDistrict.Text = 6
                    Case "6"
                        txtDistrict.Text = 7
                    Case "7"
                        txtDistrict.Text = 8
                    Case "8"
                        txtDistrict.Text = 9
                    Case "9"
                        txtDistrict.Text = 10
                    Case "10"
                        txtDistrict.Text = 11
                    Case "11"
                        txtDistrict.Text = 12
                    Case "12"
                        txtDistrict.Text = 13
                    Case "13"
                        txtDistrict.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "13"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "12"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "11"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "10"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "9"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "8"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "7"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "6"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "5"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "4"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "3"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "2"
                        Click_Action_Remove(txtDistrict.Text)
                        txtDistrict.Text = ""
                    Case "1"
                        txtDistrict.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbDistrict_CheckedChanged")
        Finally
         
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbDistrict_Engineer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbDistrict_Engineer.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbDistrict_Engineer.Checked = True Then
                Select Case temp
                    Case "0"
                        txtDistrict_Engineer.Text = 1
                    Case "1"
                        txtDistrict_Engineer.Text = 2
                    Case "2"
                        txtDistrict_Engineer.Text = 3
                    Case "3"
                        txtDistrict_Engineer.Text = 4
                    Case "4"
                        txtDistrict_Engineer.Text = 5
                    Case "5"
                        txtDistrict_Engineer.Text = 6
                    Case "6"
                        txtDistrict_Engineer.Text = 7
                    Case "7"
                        txtDistrict_Engineer.Text = 8
                    Case "8"
                        txtDistrict_Engineer.Text = 9
                    Case "9"
                        txtDistrict_Engineer.Text = 10
                    Case "10"
                        txtDistrict_Engineer.Text = 11
                    Case "11"
                        txtDistrict_Engineer.Text = 12
                    Case "12"
                        txtDistrict_Engineer.Text = 13
                    Case "13"
                        txtDistrict_Engineer.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "13"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "12"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "11"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "10"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "9"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "8"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "7"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "6"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "5"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "4"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "3"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "2"
                        Click_Action_Remove(txtDistrict_Engineer.Text)
                        txtDistrict_Engineer.Text = ""
                    Case "1"
                        txtDistrict_Engineer.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbDistrict_Engineer_CheckedChanged")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbDistrictResponsible_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbDistrictResponsible.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbDistrictResponsible.Checked = True Then
                Select Case temp
                    Case "0"
                        txtDistrictResponsible.Text = 1
                    Case "1"
                        txtDistrictResponsible.Text = 2
                    Case "2"
                        txtDistrictResponsible.Text = 3
                    Case "3"
                        txtDistrictResponsible.Text = 4
                    Case "4"
                        txtDistrictResponsible.Text = 5
                    Case "5"
                        txtDistrictResponsible.Text = 6
                    Case "6"
                        txtDistrictResponsible.Text = 7
                    Case "7"
                        txtDistrictResponsible.Text = 8
                    Case "8"
                        txtDistrictResponsible.Text = 9
                    Case "9"
                        txtDistrictResponsible.Text = 10
                    Case "10"
                        txtDistrictResponsible.Text = 11
                    Case "11"
                        txtDistrictResponsible.Text = 12
                    Case "12"
                        txtDistrictResponsible.Text = 13
                    Case "13"
                        txtDistrictResponsible.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "13"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "12"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "11"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "10"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "9"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "8"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "7"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "6"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "5"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "4"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "3"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "2"
                        Click_Action_Remove(txtDistrictResponsible.Text)
                        txtDistrictResponsible.Text = ""
                    Case "1"
                        txtDistrictResponsible.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbDistrictResponsible_CheckedChanged")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbEngineer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbEngineer.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbEngineer.Checked = True Then
                Select Case temp
                    Case "0"
                        txtEngineer.Text = 1
                    Case "1"
                        txtEngineer.Text = 2
                    Case "2"
                        txtEngineer.Text = 3
                    Case "3"
                        txtEngineer.Text = 4
                    Case "4"
                        txtEngineer.Text = 5
                    Case "5"
                        txtEngineer.Text = 6
                    Case "6"
                        txtEngineer.Text = 7
                    Case "7"
                        txtEngineer.Text = 8
                    Case "8"
                        txtEngineer.Text = 9
                    Case "9"
                        txtEngineer.Text = 10
                    Case "10"
                        txtEngineer.Text = 11
                    Case "11"
                        txtEngineer.Text = 12
                    Case "12"
                        txtEngineer.Text = 13
                    Case "13"
                        txtEngineer.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "13"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "12"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "11"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "10"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "9"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "8"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "7"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "6"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "5"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "4"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "3"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "2"
                        Click_Action_Remove(txtEngineer.Text)
                        txtEngineer.Text = ""
                    Case "1"
                        txtEngineer.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbEngineer_CheckedChanged")
        Finally
         
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbFacility_Name_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbFacility_Name.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbFacility_Name.Checked = True Then
                Select Case temp
                    Case "0"
                        txtFacility_Name.Text = 1
                    Case "1"
                        txtFacility_Name.Text = 2
                    Case "2"
                        txtFacility_Name.Text = 3
                    Case "3"
                        txtFacility_Name.Text = 4
                    Case "4"
                        txtFacility_Name.Text = 5
                    Case "5"
                        txtFacility_Name.Text = 6
                    Case "6"
                        txtFacility_Name.Text = 7
                    Case "7"
                        txtFacility_Name.Text = 8
                    Case "8"
                        txtFacility_Name.Text = 9
                    Case "9"
                        txtFacility_Name.Text = 10
                    Case "10"
                        txtFacility_Name.Text = 11
                    Case "11"
                        txtFacility_Name.Text = 12
                    Case "12"
                        txtFacility_Name.Text = 13
                    Case "13"
                        txtFacility_Name.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "13"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "12"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "11"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "10"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "9"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "8"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "7"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "6"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "5"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "4"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "3"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "2"
                        Click_Action_Remove(txtFacility_Name.Text)
                        txtFacility_Name.Text = ""
                    Case "1"
                        txtFacility_Name.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbFacility_Name_CheckedChanged")
        Finally
            
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbLast_FCE_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbLast_FCE.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbLast_FCE.Checked = True Then
                Select Case temp
                    Case "0"
                        txtLast_FCE.Text = 1
                    Case "1"
                        txtLast_FCE.Text = 2
                    Case "2"
                        txtLast_FCE.Text = 3
                    Case "3"
                        txtLast_FCE.Text = 4
                    Case "4"
                        txtLast_FCE.Text = 5
                    Case "5"
                        txtLast_FCE.Text = 6
                    Case "6"
                        txtLast_FCE.Text = 7
                    Case "7"
                        txtLast_FCE.Text = 8
                    Case "8"
                        txtLast_FCE.Text = 9
                    Case "9"
                        txtLast_FCE.Text = 10
                    Case "10"
                        txtLast_FCE.Text = 11
                    Case "11"
                        txtLast_FCE.Text = 12
                    Case "12"
                        txtLast_FCE.Text = 13
                    Case "13"
                        txtLast_FCE.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "13"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "12"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "11"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "10"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "9"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "8"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "7"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "6"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "5"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "4"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "3"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "2"
                        Click_Action_Remove(txtLast_FCE.Text)
                        txtLast_FCE.Text = ""
                    Case "1"
                        txtLast_FCE.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbLast_FCE_CheckedChanged")
        Finally
          
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbLast_Inspection_Date_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbLast_Inspection_Date.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbLast_Inspection_Date.Checked = True Then
                Select Case temp
                    Case "0"
                        txtLast_Inspection_Date.Text = 1
                    Case "1"
                        txtLast_Inspection_Date.Text = 2
                    Case "2"
                        txtLast_Inspection_Date.Text = 3
                    Case "3"
                        txtLast_Inspection_Date.Text = 4
                    Case "4"
                        txtLast_Inspection_Date.Text = 5
                    Case "5"
                        txtLast_Inspection_Date.Text = 6
                    Case "6"
                        txtLast_Inspection_Date.Text = 7
                    Case "7"
                        txtLast_Inspection_Date.Text = 8
                    Case "8"
                        txtLast_Inspection_Date.Text = 9
                    Case "9"
                        txtLast_Inspection_Date.Text = 10
                    Case "10"
                        txtLast_Inspection_Date.Text = 11
                    Case "11"
                        txtLast_Inspection_Date.Text = 12
                    Case "12"
                        txtLast_Inspection_Date.Text = 13
                    Case "13"
                        txtLast_Inspection_Date.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "13"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "12"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "11"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "10"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "9"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "8"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "7"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "6"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "5"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "4"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "3"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "2"
                        Click_Action_Remove(txtLast_Inspection_Date.Text)
                        txtLast_Inspection_Date.Text = ""
                    Case "1"
                        txtLast_Inspection_Date.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbLast_Inspection_Date_CheckedChanged")
        Finally
        
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbOperational_Status_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbOperational_Status.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbOperational_Status.Checked = True Then
                Select Case temp
                    Case "0"
                        txtOperational_Status.Text = 1
                    Case "1"
                        txtOperational_Status.Text = 2
                    Case "2"
                        txtOperational_Status.Text = 3
                    Case "3"
                        txtOperational_Status.Text = 4
                    Case "4"
                        txtOperational_Status.Text = 5
                    Case "5"
                        txtOperational_Status.Text = 6
                    Case "6"
                        txtOperational_Status.Text = 7
                    Case "7"
                        txtOperational_Status.Text = 8
                    Case "8"
                        txtOperational_Status.Text = 9
                    Case "9"
                        txtOperational_Status.Text = 10
                    Case "10"
                        txtOperational_Status.Text = 11
                    Case "11"
                        txtOperational_Status.Text = 12
                    Case "12"
                        txtOperational_Status.Text = 13
                    Case "13"
                        txtOperational_Status.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "13"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "12"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "11"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "10"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "9"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "8"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "7"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "6"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "5"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "4"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "3"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "2"
                        Click_Action_Remove(txtOperational_Status.Text)
                        txtOperational_Status.Text = ""
                    Case "1"
                        txtOperational_Status.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbOperational_Status_CheckedChanged")
        Finally
           
        End Try
        Cursor = Cursors.Default

    End Sub
    Private Sub chbSIC_Code_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbSIC_Code.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor

            If chbSIC_Code.Checked = True Then
                Select Case temp
                    Case "0"
                        txtSIC_Code.Text = 1
                    Case "1"
                        txtSIC_Code.Text = 2
                    Case "2"
                        txtSIC_Code.Text = 3
                    Case "3"
                        txtSIC_Code.Text = 4
                    Case "4"
                        txtSIC_Code.Text = 5
                    Case "5"
                        txtSIC_Code.Text = 6
                    Case "6"
                        txtSIC_Code.Text = 7
                    Case "7"
                        txtSIC_Code.Text = 8
                    Case "8"
                        txtSIC_Code.Text = 9
                    Case "9"
                        txtSIC_Code.Text = 10
                    Case "10"
                        txtSIC_Code.Text = 11
                    Case "11"
                        txtSIC_Code.Text = 12
                    Case "12"
                        txtSIC_Code.Text = 13
                    Case "13"
                        txtSIC_Code.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "13"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "12"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "11"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "10"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "9"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "8"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "7"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "6"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "5"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "4"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "3"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "2"
                        Click_Action_Remove(txtSIC_Code.Text)
                        txtSIC_Code.Text = ""
                    Case "1"
                        txtSIC_Code.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbSIC_Code_CheckedChanged")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub chbSSCP_Unit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbSSCP_Unit.CheckedChanged
        Dim temp As Integer = txtCheckBox_Count.Text

        Try
            Cursor = Cursors.WaitCursor
            If chbSSCP_Unit.Checked = True Then
                Select Case temp
                    Case "0"
                        txtSSCP_Unit.Text = 1
                    Case "1"
                        txtSSCP_Unit.Text = 2
                    Case "2"
                        txtSSCP_Unit.Text = 3
                    Case "3"
                        txtSSCP_Unit.Text = 4
                    Case "4"
                        txtSSCP_Unit.Text = 5
                    Case "5"
                        txtSSCP_Unit.Text = 6
                    Case "6"
                        txtSSCP_Unit.Text = 7
                    Case "7"
                        txtSSCP_Unit.Text = 8
                    Case "8"
                        txtSSCP_Unit.Text = 9
                    Case "9"
                        txtSSCP_Unit.Text = 10
                    Case "10"
                        txtSSCP_Unit.Text = 11
                    Case "11"
                        txtSSCP_Unit.Text = 12
                    Case "12"
                        txtSSCP_Unit.Text = 13
                    Case "13"
                        txtSSCP_Unit.Text = 14
                End Select

                temp = temp + 1
                txtCheckBox_Count.Text = temp
            Else
                Select Case temp
                    Case "14"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "13"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "12"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "11"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "10"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "9"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "8"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "7"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "6"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "5"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "4"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "3"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "2"
                        Click_Action_Remove(txtSSCP_Unit.Text)
                        txtSSCP_Unit.Text = ""
                    Case "1"
                        txtSSCP_Unit.Text = ""
                End Select

                temp = temp - 1
                txtCheckBox_Count.Text = temp
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.chbSSCP_Unit_CheckedChanged")
        Finally
          
        End Try
        Cursor = Cursors.Default

    End Sub
#End Region
    Private Sub TBUserProfileTool_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBUserProfileTool.ButtonClick
        Try
            Cursor = Cursors.WaitCursor
            Select Case TBUserProfileTool.Buttons.IndexOf(e.Button)
                Case 0
                    If tbUser_Profile.Visible Then
                        Update_User_Profile()
                    End If
                    If tbSSCP_UC_Facility_Assignment_Profile.Visible Then
                        Update_UC_Facility_Assignment_Profile()
                    End If
                Case 1
                    If tbUser_Profile.Visible Then
                        ClearUser_profile()
                    End If
                    If tbSSCP_UC_Facility_Assignment_Profile.Visible Then
                        ClearSSCP_UC_Fac_Assignemnt()
                    End If


                Case 2
                    Me.Close()
                Case 3
                    Me.Close()
                Case Else

            End Select

        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.TBUserProfileTool_ButtonClick")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Me.Close()
    End Sub
    Private Sub MmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiExit.Click
        Me.Close()
    End Sub
    Private Sub MmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiCut.Click
        Try
            Cursor = Cursors.WaitCursor
            SendKeys.Send("^(x)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.MmiCut_Click")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub MmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiCopy.Click
        Try
            Cursor = Cursors.WaitCursor
            SendKeys.Send("^(c)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.MmiCopy_Click")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub MmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiPaste.Click
        Try
            Cursor = Cursors.WaitCursor
            SendKeys.Send("^(v)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.MmiPaste_Click")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub MmiClearPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiClearPage.Click
        Try
            Cursor = Cursors.WaitCursor
            If tbUser_Profile.Visible Then
                ClearUser_profile()
            End If
            If tbSSCP_UC_Facility_Assignment_Profile.Visible Then
                ClearSSCP_UC_Fac_Assignemnt()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.MmiClearPage_Click")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub MmiToolbar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiToolbar.Click
        Try
            Cursor = Cursors.WaitCursor
            If TBUserProfileTool.Visible = True Then
                TBUserProfileTool.Visible = False
            Else
                TBUserProfileTool.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.MmiToolbar_Click")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Private Sub MmiFacilityHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiFacilityHelp.Click
        Try
            Cursor = Cursors.WaitCursor
            MsgBox("When the Help manual is written it will appear here.", MsgBoxStyle.Information, "Profile Tool")
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.MmiFacilityHelp_Click")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub
    Sub RunAndExportReport(ByVal EngineerGcode As String)
        Dim WordText As String
        Dim WordApp As New Word.ApplicationClass
        Dim wordDoc As Word.DocumentClass

        Dim DateBias As String = ""

        Dim Staff As String = ""
        Dim DateStatement As String = ""
        Dim ReceivedByDate As String = "X"
        Dim OpenByDate As String = "X"
        Dim ClosedByDate As String = "X"
        Dim WitnessedByDate As String = "X"
        Dim OpenWitnessedByDate As String = "X"
        Dim CloseWitnessedByDate As String = "X"
        Dim GreaterByDate As String = "X"
        Dim OpenGreaterByDate As String = "X"
        Dim CloseGreaterByDate As String = "X"
        Dim ComplianceByDate As String = "X"
        Dim OpenComplianceByDate As String = "X"
        Dim CloseComplianceByDate As String = "X"
        Dim OpenMedianByDate As String = "X"
        Dim CloseMedianByDate As String = "X"
        Dim OpenPercentileByDate As String = "X"
        Dim ClosePercentileByDate As String = "X"

        Dim ReceivedTotal As String = "X"
        Dim OpenTotal As String = "X"
        Dim OpenWitnessedTotal As String = "X"
        Dim OpenComplianceTotal As String = "X"
        Dim OpenGreaterTotal As String = "X"
        Dim OpenGrearerDaysTotal As String = "X"
        Dim OpenMedianTotal As String = "X"
        Dim PercentileOpenTotalDay As String = "X"
        Dim ClosedTotal As String = "X"
        Dim ClosedWitnessedTotal As String = "X"
        Dim ClosedComplianceTotal As String = "X"
        Dim ClosedGreaterTotal As String = "X"
        Dim ClosedGreaterDaysTotal As String = "X"
        Dim ClosedMedianTotal As String = "X"
        Dim PercentileClosedTotalDay As String = "X"
        Dim Statement As String = ""

        Dim i As Integer = 0
        Dim MedianArrayByDateOpen(i) As Decimal
        Dim j As Integer = 0
        Dim MedianArrayByDateClose(j) As Decimal
        Dim n As Integer = 0
        Dim MedianArrayOpen(n) As Decimal
        Dim o As Integer = 0
        Dim MedianArrayClosed(o) As Decimal

        Try
            Cursor = Cursors.WaitCursor

            If rdbUnitDateTestStarted.Checked = True Then
                DateBias = "datTestDateStart between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Tests Conducted between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateReceived.Checked = True Then
                DateBias = "datReceivedDate between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Test Reports Received between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitDateCompleted.Checked = True Then
                DateBias = "datCompleteDate between '" & DTPUnitStart.Text & "' " & _
                "and '" & DTPUnitEnd.Text & "'"
                DateStatement = "For all Test Reports Completed between (" & DTPUnitStart.Text & ") and (" & DTPUnitEnd.Text & ") there were:"
            End If
            If rdbUnitStatsAll.Checked = True Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were: "
            End If
            If DateBias = "" Then
                DateBias = "datReceivedDate between '04-Jul-1776' " & _
                "and '09-Sep-9998'"
                DateStatement = "For all Test Reports in the database there were:"
            End If

            If EngineerGcode = "" Then

            Else
                SQL = "select " & _
                "distinct(strUserFirstName|| ' ' ||strUserLastName) as Staff,  " & _
                "case " & _
                "	when ReceivedByDate is NULL then 0  " & _
                "	Else ReceivedByDate " & _
                "End as ReceivedByDate,  " & _
                "Case  " & _
                "	when OpenByDate is Null then 0  " & _
                "	Else OpenByDate  " & _
                "End as OpenByDate,  " & _
                "Case  " & _
                "	WHEN CloseByDate is Null then 0  " & _
                "	Else CloseByDate " & _
                "End as CloseByDate,  " & _
                "Case  " & _
                "	when WitnessedByDate is Null then 0  " & _
                "	Else WitnessedByDate  " & _
                "End as WitnessedByDate, " & _
                "case  " & _
                "	when OpenWitnessedByDate is NULL then 0  " & _
                "	Else OpenWitnessedByDate  " & _
                "End as OpenWitnessedByDate,  " & _
                "case  " & _
                "	when CloseWitnessedByDate is NULL then 0  " & _
                "	Else CloseWitnessedByDate  " & _
                "End as CloseWitnessedByDate,  " & _
                "Case " & _
                "   when GreaterByDate is NUll then 0 " & _
                "   Else GreaterByDate " & _
                "End as GreaterByDate, " & _
                "case  " & _
                "	when OpenGreaterByDate is NULL then 0  " & _
                "	Else OpenGreaterByDate " & _
                "end as OpenGreaterByDate,    " & _
                "case  " & _
                "	When CloseGreaterByDate is NULL then 0  " & _
                "	Else CloseGreaterByDate  " & _
                "End as CloseGreaterByDate,  " & _
                "Case " & _
                "   when ComplianceByDate is NULL then 0 " & _
                "   Else ComplianceByDate " & _
                "End as ComplianceByDate, " & _
                "Case  " & _
                "	when OpenComplianceByDate is NULL then 0  " & _
                "	Else OpenComplianceByDate " & _
                "End as OpenComplianceByDate,  " & _
                "Case  " & _
                "	When CloseComplianceByDate is NULL then 0  " & _
                "	Else CloseComplianceByDate " & _
                "End as CloseComplianceByDate  " & _
                "from " & connNameSpace & ".APBUsers, " & connNameSpace & ".ISMPReportInformation,  " & _
                "(Select strReviewingEngineer,  count(*) as ReceivedByDate   " & _
                "from " & connNameSpace & ".ISMPReportInformation   " & _
                "where strDelete is NULL " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) ReceivedByDates,  " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as OpenByDate  " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'False'  " & _
                "and strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) OpenByDates,  " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as CloseByDate  " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'True'  " & _
                "and StrDelete is NULL  " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) CloseByDates,  " & _
                "(Select strWitnessingEngineer,  " & _
                "count(*) as WitnessedByDate  " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "group by strWitnessingEngineer) WitnessedByDates,  " & _
                "(Select strWitnessingEngineer,  " & _
                "count(*) as OpenWitnessedByDate   " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'False'  " & _
                 "and " & DateBias & " " & _
                "group by strWitnessingEngineer) OpenWitnessedByDates,  " & _
                "(select strWitnessingEngineer,  " & _
                "count(*) as CloseWitnessedByDate   " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'True' " & _
                "and " & DateBias & " " & _
                "group by strwitnessingEngineer) CloseWitnessedByDates,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as GreaterByDate " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and datReceivedDate < Decode(strClosed, 'False', (trunc(sysdate) - 50), " & _
                "                                        'True', (-50 + datCompleteDate)) " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) GreaterByDates,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as OpenGreaterByDate " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'False'  " & _
                "and datReceivedDate < (trunc(sysdate) - 50)  " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) OpenGreaterByDates,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as CloseGreaterByDate " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'True'  " & _
                "and datReceivedDate < (-50 + datCompleteDate) " & _
                "and " & DateBias & " " & _
                "Group by strReviewingEngineer) CloseGreaterByDates,  " & _
                "(select strReviewingEngineer, " & _
                "count(*) as ComplianceByDate " & _
                "from " & connNameSpace & ".ISMPReportInformation " & _
                "where strComplianceStatus = '05' " & _
                "and strDelete is NULL " & _
                "and " & DateBias & " " & _
                "group by strReviewingEngineer) ComplianceByDates, " & _
                "(select strReviewingEngineer,   " & _
                "count(*) as OpenComplianceByDate  " & _
                "from " & connNameSpace & ".ISMPReportInformation   " & _
                "where strComplianceStatus = '05'  " & _
                "and strClosed = 'False'  " & _
                "and strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "group by strReviewingEngineer) OpenComplianceByDates,   " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as CloseComplianceByDate  " & _
                "from " & connNameSpace & ".ISMPReportInformation   " & _
                "where strComplianceStatus = '05'  " & _
                "and strClosed = 'True'  " & _
                "and strDelete is NULL  " & _
                "and " & DateBias & " " & _
                "group by strReviewingEngineer) CloseComplianceByDates   " & _
                "where " & connNameSpace & ".APBUsers.strUserGCOde = " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = ReceivedByDates.strReviewingEngineer (+) " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenBYDates.strReviewingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = CloseByDates.strReviewingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = WitnessedByDates.strWitnessingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenwitnessedByDates.strWitnessingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = CloseWitnessedByDates.strWitnessingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = GreaterByDates.strReviewingEngineer (+) " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenGreaterByDates.strReviewingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = CloseGreaterByDates.strReviewingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = ComplianceByDates.strReviewingEngineer (+) " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenComplianceByDates.strReviewingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strREviewingEngineer = CloseComplianceByDates.strReviewingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = '" & EngineerGcode & "' "

                SQL2 = "Select " & _
                "(StrUserFirstName|| ' ' ||strUSerLastName) as Staff, " & _
                "(trunc(sysdate) - datReceivedDate) as DaysOpenByDate " & _
                "from " & connNameSpace & ".APBUsers, " & connNameSpace & ".ISMPReportInformation " & _
                "where " & connNameSpace & ".APBUsers.strUserGcode = " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'False' " & _
                "and strDelete is NULL " & _
                "and " & DateBias & " " & _
                "and strReviewingEngineer = '" & EngineerGcode & "' " & _
                "order by DaysOpenByDate ASC "

                SQL3 = "Select " & _
                "(StrUserFirstName|| ' ' ||strUSerLastName) as Staff, " & _
                "(datCompleteDate - datReceivedDate) as DaysCloseByDate " & _
                "from " & connNameSpace & ".APBUsers, " & connNameSpace & ".ISMPReportInformation " & _
                "where " & connNameSpace & ".APBUsers.strUserGcode = " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'True' " & _
                "and strDelete is NULL " & _
                "and " & DateBias & " " & _
                "and strReviewingEngineer = '" & EngineerGcode & "' " & _
                "order by DaysCloseByDate ASC "

                SQL4 = "Select " & _
                "distinct(StrUserFirstName|| ' ' ||strUSerLastName) as Staff,  " & _
                "case  " & _
                "	when ReceivedTotal is NULL then 0  " & _
                "	Else ReceivedTotal  " & _
                "end as ReceivedTotal,  " & _
                "case  " & _
                "	when OpenTotal is NULL then 0  " & _
                "	Else OpenTotal  " & _
                "End as OpenTotal,  " & _
                "Case  " & _
                "	when OpenWitnessedTotal is NULL then 0  " & _
                "	Else OpenWitnessedTotal  " & _
                "End as OpenWitnessedTotal,  " & _
                "Case  " & _
                "	When OpenComplianceTotal is NULL then 0  " & _
                "	Else OpenComplianceTotal  " & _
                "End as OpenComplianceTotal,  " & _
                "Case  " & _
                "	when CloseTotal is NULL then 0  " & _
                "	else CloseTotal  " & _
                "End as CloseTotal,  " & _
                "Case  " & _
                "	when ClosedWitnessedTotal is NULL then 0  " & _
                "	Else ClosedWitnessedTotal  " & _
                "End as ClosedWitnessedTotal,  " & _
                "Case  " & _
                "	when ClosedComplianceTotal is NULL then 0  " & _
                "	Else ClosedComplianceTotal " & _
                "End as ClosedComplianceTotal,  " & _
                "Case  " & _
                "when OpenGreaterTotal is NULL then 0   " & _
                "Else OpenGreaterTotal   " & _
                "End as OpenGreaterTotal, " & _
                "Case  " & _
                "when ClosedGreaterTotal is NULL then 0   " & _
                "Else ClosedGreaterTotal   " & _
                "End as ClosedGreaterTotal   " & _
                "from " & connNameSpace & ".APBUsers, " & connNameSpace & ".ISMPReportInformation, " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as ReceivedTotal  " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "Group by strReviewingEngineer) ReceivedTotals,  " & _
                "(Select strReviewingEngineer,  " & _
                "count(*) as OpenTotal " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'False' " & _
                "and strDelete is NULL  " & _
                "Group by strReviewingEngineer) OpenTotals,  " & _
                "(select strWitnessingEngineer,  " & _
                "count(*) as OpenWitnessedTotal  " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'False' " & _
                "and strDelete is Null " & _
                "group by strWitnessingEngineer) OpenWitnessedTotals,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as OpenComplianceTotal  " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strComplianceStatus = '05' " & _
                "and strClosed = 'False' " & _
                "and strDelete is NULL " & _
                "group by strReviewingEngineer) OpenComplianceTotals,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as CloseTotal  " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'True'  " & _
                "and strDelete is NULL " & _
                "Group by strReviewingEngineer) CloseTotals,  " & _
                "(select strWitnessingEngineer,  " & _
                "count(*) as ClosedWitnessedTotal  " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strClosed = 'True' " & _
                "and strDelete is NULL  " & _
                "group by strWitnessingEngineer) ClosedWitnessedTotals,  " & _
                "(select strReviewingEngineer,  " & _
                "count(*) as ClosedComplianceTotal  " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strComplianceStatus = '05' " & _
                "and strClosed = 'True' " & _
                "and strDelete is NULL " & _
                "group by strReviewingEngineer) ClosedComplianceTotals, " & _
                "(select strReviewingEngineer, count(*) as OpenGreaterTotal " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'False'  " & _
                "and datReceivedDate < (trunc(sysdate) - 50)  " & _
                "Group by strReviewingEngineer) OpenGreaterTotals, " & _
                "(select strReviewingEngineer, count(*) as ClosedGreaterTotal " & _
                "from " & connNameSpace & ".ISMPReportInformation  " & _
                "where strDelete is NULL  " & _
                "and strClosed = 'True'  " & _
                "and datReceivedDate < (-50 + datCompleteDate)  " & _
                "Group by strReviewingEngineer) ClosedGreaterTotals " & _
                "where " & connNameSpace & ".APBUsers.strUserGcode = " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = ReceivedTotals.strReviewingEngineer (+) " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenTotals.strReviewingEngineer (+) " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenWitnessedTotals.strWitnessingEngineer (+) " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenComplianceTotals.strReviewingEngineer (+) " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = CloseTotals.strReviewingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = ClosedWitnessedTotals.strWitnessingEngineer (+)  " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = ClosedCompliancetotals.strReviewingEngineer (+) " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = OpenGreaterTotals.strReviewingEngineer (+) " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = ClosedGreaterTotals.strReviewingEngineer (+)   " & _
                "and " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer = '" & EngineerGcode & "' "

                SQL5 = "Select " & _
                "(StrUserFirstName|| ' ' ||strUSerLastName) as Staff, " & _
                "(trunc(sysdate) - datReceivedDate) as DaysOpen " & _
                "from " & connNameSpace & ".APBUsers, " & connNameSpace & ".ISMPReportInformation " & _
                "where " & connNameSpace & ".APBUsers.strUserGcode = " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'False' " & _
                "and strDelete is NULL " & _
                "and strReviewingEngineer = '" & EngineerGcode & "' " & _
                "order by DaysOpen ASC "

                SQL6 = "Select " & _
                "(StrUserFirstName|| ' ' ||strUSerLastName) as Staff, " & _
                "(datCompleteDate -datReceivedDate) as DaysClosed " & _
                "from " & connNameSpace & ".APBUsers, " & connNameSpace & ".ISMPReportInformation " & _
                "where " & connNameSpace & ".APBUsers.strUserGcode = " & connNameSpace & ".ISMPReportInformation.strReviewingEngineer  " & _
                "and strClosed = 'True' " & _
                "and strDelete is NULL " & _
                "and strReviewingEngineer = '" & EngineerGcode & "' " & _
                "order by DaysClosed ASC "

                cmd = New OracleCommand(SQL, conn)
                cmd2 = New OracleCommand(SQL2, conn)
                cmd3 = New OracleCommand(SQL3, conn)
                cmd4 = New OracleCommand(SQL4, conn)
                cmd5 = New OracleCommand(SQL5, conn)
                cmd6 = New OracleCommand(SQL6, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                Try
                    Cursor = Cursors.WaitCursor
                    dr = cmd.ExecuteReader

                    While dr.Read
                        Staff = dr.Item("Staff")
                        ReceivedByDate = dr.Item("ReceivedByDate")
                        OpenByDate = dr.Item("OpenbyDate")
                        ClosedByDate = dr.Item("CLoseByDate")
                        WitnessedByDate = dr.Item("WitnessedByDate")
                        OpenWitnessedByDate = dr.Item("OpenWitnessedByDate")
                        CloseWitnessedByDate = dr.Item("Closewitnessedbydate")
                        GreaterByDate = dr.Item("GreaterByDate")
                        OpenGreaterByDate = dr.Item("OpenGreaterByDate")
                        CloseGreaterByDate = dr.Item("CloseGreaterByDate")
                        ComplianceByDate = dr.Item("ComplianceByDate")
                        OpenComplianceByDate = dr.Item("OpenComplianceByDate")
                        CloseComplianceByDate = dr.Item("CloseComplianceByDate")
                    End While

                    dr2 = cmd2.ExecuteReader
                    While dr2.Read
                        ReDim Preserve MedianArrayByDateOpen(i)
                        MedianArrayByDateOpen(i) = CInt(dr2.Item("DaysOpenByDate"))
                        i += 1
                    End While

                    dr3 = cmd3.ExecuteReader
                    While dr3.Read
                        ReDim Preserve MedianArrayByDateClose(j)
                        MedianArrayByDateClose(j) = CInt(dr3.Item("DaysCloseByDate"))
                        j += 1
                    End While

                    dr4 = cmd4.ExecuteReader
                    While dr4.Read
                        ReceivedTotal = dr4.Item("ReceivedTotal")
                        OpenTotal = dr4.Item("OpenTotal")
                        OpenWitnessedTotal = dr4.Item("OpenWitnessedTotal")
                        OpenComplianceTotal = dr4.Item("OpenComplianceTotal")
                        OpenGreaterTotal = dr4.Item("OpenGreaterTotal")
                        ClosedTotal = dr4.Item("CloseTotal")
                        ClosedWitnessedTotal = dr4.Item("ClosedWitnessedTotal")
                        ClosedComplianceTotal = dr4.Item("ClosedComplianceTotal")
                        ClosedGreaterTotal = dr4.Item("ClosedGreaterTotal")
                    End While

                    dr5 = cmd5.ExecuteReader
                    While dr5.Read
                        ReDim Preserve MedianArrayOpen(n)
                        MedianArrayOpen(n) = CInt(dr5.Item("DaysOpen"))
                        n += 1
                    End While

                    dr6 = cmd6.ExecuteReader
                    While dr6.Read
                        ReDim Preserve MedianArrayClosed(o)
                        MedianArrayClosed(o) = CInt(dr6.Item("DaysClosed"))
                        o += 1
                    End While

                Catch ex As Exception
                    MsgBox(ex.ToString())
                End Try

                If MedianArrayByDateOpen.GetLength(0) Mod 2 = 0 Then
                    OpenMedianByDate = (MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) / 2) - 1) + MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) / 2))) / 2
                    If MedianArrayOpen.GetLength(0) <= 2 Then
                        OpenPercentileByDate = "Unavailable"
                    Else
                        OpenPercentileByDate = (MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) * 0.8) - 1) + MedianArrayByDateOpen((MedianArrayByDateOpen.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    OpenMedianByDate = MedianArrayByDateOpen(MedianArrayByDateOpen.GetLength(0) / 2)
                    If MedianArrayByDateOpen.GetLength(0) <= 2 Then
                        OpenPercentileByDate = "Unavailable"
                    Else
                        OpenPercentileByDate = MedianArrayOpen(MedianArrayByDateOpen.GetLength(0) * 0.8)
                    End If
                End If

                If MedianArrayByDateClose.GetLength(0) Mod 2 = 0 Then
                    CloseMedianByDate = (MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) / 2) - 1) + MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) / 2))) / 2
                    If MedianArrayOpen.GetLength(0) <= 2 Then
                        ClosePercentileByDate = "Unavailable"
                    Else
                        ClosePercentileByDate = (MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) * 0.8) - 1) + MedianArrayByDateClose((MedianArrayByDateClose.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    CloseMedianByDate = MedianArrayByDateClose(MedianArrayByDateClose.GetLength(0) / 2)
                    If MedianArrayByDateClose.GetLength(0) <= 2 Then
                        ClosePercentileByDate = "Unavailable"
                    Else
                        ClosePercentileByDate = MedianArrayByDateClose(MedianArrayByDateClose.GetLength(0) * 0.8)
                    End If
                End If

                If MedianArrayOpen.GetLength(0) Mod 2 = 0 Then
                    OpenMedianTotal = (MedianArrayOpen((MedianArrayOpen.GetLength(0) / 2) - 1) + MedianArrayOpen((MedianArrayOpen.GetLength(0) / 2))) / 2
                    If MedianArrayOpen.GetLength(0) <= 2 Then
                        PercentileOpenTotalDay = "Unavailable"
                    Else
                        PercentileOpenTotalDay = (MedianArrayOpen((MedianArrayOpen.GetLength(0) * 0.8) - 1) + MedianArrayOpen((MedianArrayOpen.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    OpenMedianTotal = MedianArrayOpen(MedianArrayOpen.GetLength(0) / 2)
                    If MedianArrayOpen.GetLength(0) <= 2 Then
                        PercentileOpenTotalDay = "Unavailable"
                    Else
                        PercentileOpenTotalDay = MedianArrayOpen(MedianArrayOpen.GetLength(0) * 0.8)
                    End If
                End If

                If MedianArrayClosed.GetLength(0) Mod 2 = 0 Then
                    ClosedMedianTotal = (MedianArrayClosed((MedianArrayClosed.GetLength(0) / 2) - 1) + MedianArrayClosed((MedianArrayClosed.GetLength(0) / 2))) / 2
                    If MedianArrayOpen.GetLength(0) <= 2 Then
                        PercentileClosedTotalDay = "Unavailable"
                    Else
                        PercentileClosedTotalDay = (MedianArrayClosed((MedianArrayClosed.GetLength(0) * 0.8) - 1) + MedianArrayClosed((MedianArrayClosed.GetLength(0) * 0.8))) / 2
                    End If
                Else
                    ClosedMedianTotal = MedianArrayClosed(MedianArrayClosed.GetLength(0) / 2)
                    If MedianArrayClosed.GetLength(0) <= 2 Then
                        PercentileClosedTotalDay = "Unavailable"
                    Else
                        PercentileClosedTotalDay = MedianArrayClosed(MedianArrayClosed.GetLength(0) * 0.8)
                    End If
                End If

            End If

            Statement = Statement & _
            "For the Staff member: " & Staff & vbCrLf & _
            vbTab & DateStatement & vbCrLf & vbCrLf & _
            "1. " & ReceivedByDate & " Test Reports Received " & vbCrLf & _
            "2. " & OpenByDate & " of these " & ReceivedByDate & " Test Reports are currently open" & vbCrLf & _
            "3. " & ClosedByDate & " of these " & ReceivedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf & _
            "4. " & WitnessedByDate & " of these " & ReceivedByDate & " Test Reports were witnessed by " & Staff & vbCrLf & _
            "5. " & OpenWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are still open " & vbCrLf & _
            "6. " & CloseWitnessedByDate & " of these " & WitnessedByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf & _
            "7. " & GreaterByDate & " of these " & ReceivedByDate & " Test Reports have been open for more than 50-days" & vbCrLf & _
            "8. " & OpenGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more than 50-days are still open " & vbCrLf & _
            "9. " & CloseGreaterByDate & " of these " & GreaterByDate & " Test Reports open for more then 50-days are currently closed " & vbCrLf & vbCrLf & _
            "10. " & ComplianceByDate & " of these " & ReceivedByDate & " Test Reports were out of compliance" & vbCrLf & _
            "11. " & OpenComplianceByDate & " of these " & ComplianceByDate & " Test Reports are still open " & vbCrLf & _
            "12. " & CloseComplianceByDate & " of these " & ComplianceByDate & " Test Reports are currently closed " & vbCrLf & vbCrLf & _
            "13. The median time taken to complete those " & ClosedByDate & " Closed Test Reports was " & CloseMedianByDate & "-days" & vbCrLf & _
            "14. The 80% Percentile Time taken to complete those " & ClosedByDate & " Closed Test Reports was " & ClosePercentileByDate & "-days" & vbCrLf & _
            "15. The median time of the " & OpenByDate & " Open Test Reports is " & OpenMedianByDate & "-days" & vbCrLf & _
            "16. The 80% Percentile Time of the " & OpenByDate & " Open Test Reports is " & OpenPercentileByDate & "-days" & vbCrLf & vbCrLf & _
            "17. Overall " & Staff & " has received " & ReceivedTotal & " Test Reports" & vbCrLf & vbCrLf & _
            "18. " & OpenTotal & " of " & ReceivedTotal & " Test Reports are currently open" & vbCrLf & _
            "19. " & OpenWitnessedTotal & " of these " & OpenTotal & " Test Reports have been witnessed" & vbCrLf & _
            "20. " & OpenComplianceTotal & " of these " & OpenTotal & " Test Reports are currently out of compliance " & vbCrLf & _
            "21. " & OpenGreaterTotal & " of these " & OpenTotal & " Test Reports have been open for more than 50-days" & vbCrLf & _
            "22. The median time of the " & OpenTotal & " Open Test Reports is " & OpenMedianTotal & "-days" & vbCrLf & _
            "23. The 80% Percentile Time of the " & OpenTotal & " Open Test Reports is " & PercentileOpenTotalDay & "-days" & vbCrLf & vbCrLf & _
            "24. " & ClosedTotal & " of " & ReceivedTotal & " Test Reports are currently closed " & vbCrLf & _
            "25. " & ClosedWitnessedTotal & " of these " & ClosedTotal & " Test Reports have been witnessed" & vbCrLf & _
            "26. " & ClosedComplianceTotal & " of these " & ClosedTotal & " Test Reports are out of compliance " & vbCrLf & _
            "27. " & ClosedGreaterTotal & " of these " & ClosedTotal & " Test Reports were open for more than 50-days" & vbCrLf & _
            "28. The median time of the " & ClosedTotal & " Closed Test Reports was " & ClosedMedianTotal & "-days" & vbCrLf & _
            "29. The 80% Percentile Time of the " & ClosedTotal & " Closed Test Reports was " & PercentileClosedTotalDay & "-days" & vbCrLf & vbCrLf & vbCrLf




            WordText = Statement

            wordDoc = WordApp.Documents.Add()
            wordDoc.Activate()
            WordApp.Selection.TypeText(WordText)
            WordApp.Visible = True

         Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.RunAndExportReport")
        Finally
           
        End Try
        Cursor = Cursors.Default


    End Sub
    Private Sub llbRunEngineerStatReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbRunEngineerStatReport.LinkClicked

        Try
            Cursor = Cursors.WaitCursor
            RunAndExportReport(UserGCode)
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.llbRunEngineerStatReport_LinkClicked")
        Finally
         
        End Try
        Cursor = Cursors.Default

    End Sub

    Private Sub User_Profile_Tool_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Cursor = Cursors.WaitCursor
            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            APB_User_Profile_Tool = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), "ProfileTool.User_Profile_Tool_Closing")
        Finally
           
        End Try
        Cursor = Cursors.Default
    End Sub


End Class
