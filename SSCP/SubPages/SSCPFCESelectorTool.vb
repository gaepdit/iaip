Imports Oracle.DataAccess.Client


Public Class SSCPFCESelectorTool
    Inherits DefaultForm

    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean

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
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mmiSearch As System.Windows.Forms.MenuItem
    Friend WithEvents tbbSearch As System.Windows.Forms.ToolBarButton
    Friend WithEvents llbOpenFCE As System.Windows.Forms.LinkLabel
    Friend WithEvents tbbSelect As System.Windows.Forms.ToolBarButton
    Friend WithEvents mmiOpenFCE As System.Windows.Forms.MenuItem
    Friend WithEvents TBSSCPFCESelector As System.Windows.Forms.ToolBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSCPFCESelectorTool))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mmiSearch = New System.Windows.Forms.MenuItem
        Me.mmiOpenFCE = New System.Windows.Forms.MenuItem
        Me.mmiHelp = New System.Windows.Forms.MenuItem
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBSSCPFCESelector = New System.Windows.Forms.ToolBar
        Me.tbbSearch = New System.Windows.Forms.ToolBarButton
        Me.tbbSelect = New System.Windows.Forms.ToolBarButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.llbOpenFCE = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiSearch, Me.mmiOpenFCE, Me.mmiHelp})
        '
        'mmiSearch
        '
        Me.mmiSearch.Index = 0
        Me.mmiSearch.Text = "Search"
        '
        'mmiOpenFCE
        '
        Me.mmiOpenFCE.Index = 1
        Me.mmiOpenFCE.Text = "Open FCE"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 2
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
        'TBSSCPFCESelector
        '
        Me.TBSSCPFCESelector.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSearch, Me.tbbSelect})
        Me.TBSSCPFCESelector.DropDownArrows = True
        Me.TBSSCPFCESelector.ImageList = Me.Image_List_All
        Me.TBSSCPFCESelector.Location = New System.Drawing.Point(0, 0)
        Me.TBSSCPFCESelector.Name = "TBSSCPFCESelector"
        Me.TBSSCPFCESelector.ShowToolTips = True
        Me.TBSSCPFCESelector.Size = New System.Drawing.Size(384, 28)
        Me.TBSSCPFCESelector.TabIndex = 142
        '
        'tbbSearch
        '
        Me.tbbSearch.ImageIndex = 3
        Me.tbbSearch.Name = "tbbSearch"
        '
        'tbbSelect
        '
        Me.tbbSelect.ImageIndex = 54
        Me.tbbSelect.Name = "tbbSelect"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 143
        Me.Label1.Text = "Facility Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 144
        Me.Label2.Text = "AIRS Number:"
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(96, 40)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(248, 20)
        Me.txtFacilityName.TabIndex = 145
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(96, 64)
        Me.txtAIRSNumber.MaxLength = 8
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.Size = New System.Drawing.Size(248, 20)
        Me.txtAIRSNumber.TabIndex = 146
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(96, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 12)
        Me.Label3.TabIndex = 147
        Me.Label3.Text = "(Eight Digit Number)"
        '
        'llbOpenFCE
        '
        Me.llbOpenFCE.AutoSize = True
        Me.llbOpenFCE.Location = New System.Drawing.Point(96, 112)
        Me.llbOpenFCE.Name = "llbOpenFCE"
        Me.llbOpenFCE.Size = New System.Drawing.Size(163, 13)
        Me.llbOpenFCE.TabIndex = 148
        Me.llbOpenFCE.TabStop = True
        Me.llbOpenFCE.Text = "Open Full Compliance Evaluation"
        '
        'SSCPFCESelectorTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(384, 153)
        Me.Controls.Add(Me.llbOpenFCE)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtAIRSNumber)
        Me.Controls.Add(Me.txtFacilityName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TBSSCPFCESelector)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPFCESelectorTool"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Compliance FCE Selector Tool"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub SSCPFCESelectorTool_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
    End Sub

    Private Sub SSCPFCESelectorTool_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

            txtAIRSNumber.Focus()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub


#Region "Main Menu"
    Private Sub mmiSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSearch.Click
        Try

            OpenFacilitySearchTool()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiOpenFCE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOpenFCE.Click
        Try

            OpenFCETool()
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

            Help.ShowHelp(Label1, HELP_URL)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub


#End Region

#Region "Subs and Functions"
    Sub OpenFacilitySearchTool()
        Try

            If FacilityLookUpTool Is Nothing Then
                If FacilityLookUpTool Is Nothing Then FacilityLookUpTool = New IAIPFacilityLookUpTool
                FacilityLookUpTool.Show()
            Else
                FacilityLookUpTool.Dispose()
                FacilityLookUpTool = New IAIPFacilityLookUpTool
                If FacilityLookUpTool Is Nothing Then FacilityLookUpTool = New IAIPFacilityLookUpTool
                FacilityLookUpTool.Show()
            End If
            FacilityLookUpTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub AddAirProgramCodes(ByRef AirProgramCode As String)
        Dim AirList As String = ""
        Dim i As Integer

        Try

            If AirProgramCode = "" Then
                AirProgramCode = "000000000000000"
            End If
            If Len(AirProgramCode) <> 15 Then
                For i = Len(AirProgramCode) To 15
                    AirProgramCode = AirProgramCode & "0"
                Next
            End If

            If Mid(AirProgramCode, 1, 1) = 1 Then
                AirList = "0 - SIP" & vbCrLf
            End If
            If Mid(AirProgramCode, 2, 1) = 1 Then
                AirList = AirList & "1 - Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCode, 3, 1) = 1 Then
                AirList = AirList & "3 - Non-Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCode, 4, 1) = 1 Then
                AirList = AirList & "4 - CFC Tracking" & vbCrLf
            End If
            If Mid(AirProgramCode, 5, 1) = 1 Then
                AirList = AirList & "6 - PSD" & vbCrLf
            End If
            If Mid(AirProgramCode, 6, 1) = 1 Then
                AirList = AirList & "7 - NSR" & vbCrLf
            End If
            If Mid(AirProgramCode, 7, 1) = 1 Then
                AirList = AirList & "8 - NESHAP" & vbCrLf
            End If
            If Mid(AirProgramCode, 8, 1) = 1 Then
                AirList = AirList & "9 - NSPS" & vbCrLf
            End If
            If Mid(AirProgramCode, 9, 1) = 1 Then
                AirList = AirList & "F - FESOP" & vbCrLf
            End If
            If Mid(AirProgramCode, 10, 1) = 1 Then
                AirList = AirList & "A - Acid Precipitation" & vbCrLf
            End If
            If Mid(AirProgramCode, 11, 1) = 1 Then
                AirList = AirList & "I - Native American" & vbCrLf
            End If
            If Mid(AirProgramCode, 12, 1) = 1 Then
                AirList = AirList & "M - MACT" & vbCrLf
            End If
            If Mid(AirProgramCode, 13, 1) = 1 Then
                AirList = AirList & "V - Title V Permit" & vbCrLf
            End If
            If AirList = "" Then
                AirList = "No Air Program Codes available" & vbCrLf
            End If
            AirList = Mid(AirList, 1, (Len(AirList) - 2))
            AirProgramCode = AirList
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub OpenFCETool()
        'Dim Street As String = ""
        'Dim City As String = ""
        'Dim County As String = ""
        'Dim Classification As String = ""
        Dim AIRProgramCode As String = ""

        Try

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            SQL = "Select strFacilityStreet1, strFacilityCity " & _
            "from " & DBNameSpace & ".APBFacilityInformation " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            dr = cmd.ExecuteReader
            While dr.Read
                'Street = dr.Item("strFacilityStreet1")
                'City = dr.Item("strFacilityCity")
            End While

            SQL = "Select strAIRProgramCodes, strClass " & _
            "from " & DBNameSpace & ".APBHeaderData " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            dr = cmd.ExecuteReader
            While dr.Read
                AIRProgramCode = dr.Item("strAIRProgramCodes")
                'Classification = dr.Item("strClass")
            End While

            'SQL = "Select strCountyName " & _
            '"from " & DBNameSpace & ".LookUpCountyInformation " & _
            '"where strCountyCode = '" & Mid(txtAIRSNumber.Text, 1, 3) & "' "

            'cmd = New OracleCommand(SQL, conn)
            'dr = cmd.ExecuteReader
            'While dr.Read
            '    County = dr.Item("strCountyName")
            'End While

            AddAirProgramCodes(AIRProgramCode)
            'AIRProgramCode = AIRProgramCode

            If txtAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Or txtFacilityName.Text = "Invalid AIRS Number" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary Warning")
                Else
                    SSCPFCE = Nothing
                    If SSCPFCE Is Nothing Then SSCPFCE = New SSCPFCEWork
                    SSCPFCE.txtAirsNumber.Text = Me.txtAIRSNumber.Text
                    SSCPFCE.txtFacilityInformation.Text = txtAIRSNumber.Text
                    SSCPFCE.txtOrigin.Text = "FCE Selector Tool"
                    SSCPFCE.Show()
                    SSCPFCE.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

                    SSCPFCESelector = Nothing
                    Me.Dispose()

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
#End Region

    Private Sub TBSSCPFCESelector_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBSSCPFCESelector.ButtonClick
        Try

            Select Case TBSSCPFCESelector.Buttons.IndexOf(e.Button)
                Case 0
                    OpenFacilitySearchTool()
                Case 1
                    OpenFCETool()
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub llbOpenFCE_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbOpenFCE.LinkClicked
        Try

            OpenFCETool()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            txtAIRSNumber.Text = Value
        End Set
    End Property

    Private Sub txtAIRSNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAIRSNumber.TextChanged
        Try

            If txtAIRSNumber.Text.Length = 8 Then
                SQL = "Select strFacilityName " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    txtFacilityName.Text = dr.Item("strFacilityName")
                Else
                    txtFacilityName.Text = "Invalid AIRS Number"
                End If

                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            Else
                txtFacilityName.Text = ""
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
End Class
