Imports System.DateTime
Imports System.Data.OracleClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Data.Odbc


Public Class DMU_TITLEV_PROJECT
    Public GATVConn As Object
    Public GATVcmd As Object
    Public GATVcmd2 As Object
    Public GATVdr As Object
    Public GATVdr2 As Object
    Public count As Integer

    Private Sub DMU_TITLEV_PROJECT_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
        monitor.TrackFeature("Dev." & Me.Name)
    End Sub
    Private Sub btn_PFW_WAREHOUSEADMIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_PFW_WAREHOUSEADMIN.Click
        Try
            PopulatePFW_WareHouseAdmin()
        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFW_WareHouseAdmin()
        Try
            Dim ApplicationID As String = ""
            Dim ProjectID As String = ""
            Dim AIRSNumber As String = ""
            Dim datStatus As String = ""

            count = 0
            SQL = "SELECT " & _
            "tbl_ProjectManagement.ProjectIdentifier, " & _
            "tbl_ProjectManagement.FacilityID, " & _
            "tbl_Projectmanagement.ApplicationNumber, " & _
            "tbl_ProjectManagement.ProjectDate " & _
            "from tbl_ProjectManagement " & _
            "ORDER BY tbl_ProjectManagement.ProjectIdentifier "

            SQL = "SELECT " & _
             "tbl_ProjectManagement.ProjectIdentifier, " & _
             "tbl_ProjectManagement.FacilityID, " & _
             "tbl_Projectmanagement.ApplicationNumber, " & _
             "tbl_ProjectManagement.ProjectDate " & _
             "from tbl_ProjectManagement, " & _
             "(select facilityid, max(projectidentifier) as maxprojectiD " & _
             "from tbl_projectmanagement " & _
             "group by facilityid ) MaxList " & _
             "where maxlist.maxprojectiD = tbl_ProjectManagement.ProjectIdentifier "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader

            While GATVdr.Read
                If IsDBNull(GATVdr.Item("ProjectIdentifier")) Then
                Else
                    ProjectID = GATVdr.Item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.Item("FacilityID")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = "04" & Mid(GATVdr.Item("FacilityID"), 1, 5) & "0" & Mid(GATVdr.Item("FacilityID"), 6)
                End If
                If IsDBNull(GATVdr.Item("ApplicationNumber")) Then
                    ApplicationID = ""
                Else
                    ApplicationID = GATVdr.Item("ApplicationNumber")
                End If
                If IsDBNull(GATVdr.Item("ProjectDate")) Then
                    datStatus = ""
                Else
                    datStatus = GATVdr.Item("ProjectDate")
                End If

                temp = "No"

                SQL = "select strAIRSNumber " & _
                "from Airbranch.APBMasterAIRS " & _
                "where strairsnumber = '" & AIRSNumber & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strAIRSNumber")) Then
                        temp = "No"
                    Else
                        temp = "Yes"
                    End If
                End While
                dr.Close()

                If temp = "Yes" Then
                    SQL = "Insert into AIRTVAPPLICATION.PFW_WAREHOUSEADMIN " & _
                    "values " & _
                    "(AIRTVAPPLICATION.seq_pfw_gsid.nextval, '" & ApplicationID & "', " & _
                    "'" & ProjectID & "', '" & AIRSNumber & "', " & _
                    "'1', sysdate, " & _
                    "'mfloyd', sysdate, " & _
                    "'test populate', '1', " & _
                    "'1') "

                    'TVApplication
                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    count += 1
                    dr.Close()
                End If
            End While
            GATVdr.Close()
            MsgBox(count.ToString())

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnPFW_PERMITADMIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFW_PERMITADMIN.Click
        Try
            PopulatePFW_PermitAdmin()
        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFW_PermitAdmin()
        Try
            Dim ProjectId As String = ""
            Dim StartDate As String = ""
            Dim ConfidentialData As String = ""
            Dim GSID As String = ""
            count = 0

            SQL = "SELECT " & _
            "tbl_ProjectManagement.ProjectIdentifier, " & _
            "tbl_ProjectManagement.Projectdate " & _
            "from tbl_ProjectManagement " & _
            "ORDER BY tbl_ProjectManagement.ProjectIdentifier "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("Projectdate")) Then
                    StartDate = ""
                Else
                    StartDate = Format(GATVdr.item("Projectdate"), "dd-MMM-yyyy")
                End If

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_PERMITADMIN " & _
                        "(GSID, DatProjectStart, " & _
                        "NUMCONFIDENTIALDATA, strProjectDescription, " & _
                        "comments, Active) " & _
                        "select  " & _
                        "'" & GSID & "', '" & StartDate & "', " & _
                        "'0', '', " & _
                        "'test Populate', '1' " & _
                        "from dual " & _
                        "where not exists (select * from pfw_permitAdmin " & _
                        "where gsid = '" & GSID & "') "

                        cmd = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

            SQL = "SELECT " & _
            "tblConfInfoData.ProjectIdentifier  " & _
            "from tblConfInfoData "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ConfidentialData = ""
                Else
                    ConfidentialData = GATVdr.item("ProjectIdentifier")
                End If

                If ConfidentialData <> "" Then
                    SQL = "select GSID " & _
                  "from airTVApplication.PFW_WareHouseAdmin " & _
                  "where ProjectID = '" & ConfidentialData & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "update AIRTVAPPLICATION.PFW_PERMITADMIN set " & _
                        "NUMCONFIDENTIALDATA = '1' " & _
                        "where GSID = '" & GSID & "' "

                        cmd = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

            MsgBox(count.ToString())

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnPFW_ApplicationReason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFW_ApplicationReason.Click
        Try
            PopulatePFW_ApplicationReason()
        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFW_ApplicationReason()
        Dim ProjectId As String = ""
        Dim ProjectName As String = ""
        Dim GSID As String = ""
        count = 0

        Try
            SQL = "SELECT " & _
         "tbl_ProjectManagement.ProjectIdentifier, " & _
         "tbl_ProjectManagement.ProjectName " & _
         "from tbl_ProjectManagement "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("ProjectName")) Then
                    ProjectName = ""
                Else
                    ProjectName = GATVdr.item("ProjectName")
                End If

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        If ProjectName.ToUpper.Contains("RENEWAL") Then
                            ProjectName = "202"
                        Else
                            ProjectName = "201"
                        End If

                        SQL = "Insert into AIRTVAPPLICATION.PFW_APPLICATIONREASON " & _
                        "(APPLICATIONREASONID, GSID, " & _
                        "APPLICATIONREASON, " & _
                        "comments, Active) " & _
                        "select  " & _
                        "AIRTVproject.SEQ_GAP_APPLICATIONREASONID.nextval, '" & GSID & "',  " & _
                        "'" & ProjectName & "', " & _
                        "'test Populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

            MsgBox(count.ToString())


        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnPFW_PERMIT_CONDITION_MOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFW_PERMIT_CONDITION_MOD.Click
        Try
            PopulatePFW_Permit_Condition_Mod()

        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFW_Permit_Condition_Mod()
        Try

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnPFW_FACILITYINFORMATION_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFW_FACILITYINFORMATION.Click
        Try
            PopulatePFW_FacilityInformation()
        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFW_FacilityInformation()
        Dim ProjectId As String = ""
        Dim HoldingCompanyName As String = ""
        Dim Facilityname As String = ""
        Dim facilitystreet1 As String = ""
        Dim facilitycity As String = ""
        Dim FacilityState As String = ""
        Dim FacilityZipCode As String = ""
        Dim longitude As Integer
        Dim latitude As Integer
        Dim UTMEasting As String = ""
        Dim UTMNorthing As String = ""
        Dim UTMZone As String = ""
        Dim SmallBusiness As String = ""
        Dim SIC As String = ""
        Dim DunnBradstreetNo As String = ""
        Dim GSID As String = ""
        count = 0

        Try
            SQL = "SELECT " & _
         "* " & _
         "from tblFacilityInformation_1_10 "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("ParentCompanyName")) Then
                    HoldingCompanyName = ""
                Else
                    HoldingCompanyName = GATVdr.item("ParentCompanyName")
                End If
                If IsDBNull(GATVdr.item("FacilityName")) Then
                    Facilityname = ""
                Else
                    Facilityname = GATVdr.item("FacilityName")
                End If
                If IsDBNull(GATVdr.item("FacilityLocationStreet1")) Then
                    facilitystreet1 = ""
                Else
                    facilitystreet1 = GATVdr.item("FacilityLocationStreet1")
                End If
                If IsDBNull(GATVdr.item("FacilityLocationCity")) Then
                    facilitycity = ""
                Else
                    facilitycity = GATVdr.item("FacilityLocationCity")
                End If

                FacilityState = "GA"

                If IsDBNull(GATVdr.item("FacilityLocationZip")) Then
                    FacilityZipCode = ""
                Else
                    FacilityZipCode = GATVdr.item("FacilityLocationZip")
                End If
                If IsDBNull(GATVdr.item("LongitudeDeg")) Or IsDBNull(GATVdr.item("LongitudeMin")) _
                       Or IsDBNull(GATVdr.item("LongitudeSec")) Then
                    longitude = Nothing
                Else
                    longitude = GATVdr.item("LongitudeDeg") + GATVdr.item("LongitudeMin") / 60 + GATVdr.item("LongitudeSec") / 3600
                End If
                If longitude > 99 Then
                    longitude = Nothing
                End If
                If IsDBNull(GATVdr.item("LatitudeDeg")) Or IsDBNull(GATVdr.item("LatitudeMin")) _
                          Or IsDBNull(GATVdr.item("LatitudeSec")) Then
                    latitude = Nothing
                Else
                    latitude = GATVdr.item("LatitudeDeg") + GATVdr.item("LatitudeMin") / 60 + GATVdr.item("LatitudeSec") / 3600
                End If
                If latitude > 99 Then
                    latitude = Nothing
                End If
                If IsDBNull(GATVdr.item("UTMHorizontalMeters")) Then
                    UTMEasting = ""
                Else
                    UTMEasting = GATVdr.item("UTMHorizontalMeters")
                End If
                If IsDBNull(GATVdr.item("UTMVerticalMeters")) Then
                    UTMNorthing = ""
                Else
                    UTMNorthing = GATVdr.item("UTMVerticalMeters")
                End If
                If IsDBNull(GATVdr.item("UTMZone")) Then
                    UTMZone = ""
                Else
                    UTMZone = GATVdr.item("UTMZone")
                End If

                SmallBusiness = "0"

                If IsDBNull(GATVdr.item("SIC1")) Then
                    SIC = ""
                Else
                    SIC = GATVdr.item("SIC1")
                End If

                If IsDBNull(GATVdr.item("DunnBradstreetNo")) Then
                    DunnBradstreetNo = ""
                Else
                    DunnBradstreetNo = GATVdr.item("DunnBradstreetNo")
                End If
                If IsNumeric(DunnBradstreetNo) Then
                Else
                    DunnBradstreetNo = ""
                End If

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_FS_FACILITYINFORMATION " & _
                        "(FACILITYINFORMATIONID, " & _
                        "GSID, STRHOLDINGCOMPANYNAME, " & _
                        "STRFACILITYNAME, STRFACILITYSTREET1, " & _
                        "STRFACILITYCITY, STRFACILITYSTATE, " & _
                        "STRFACILITYZIPCODE, NUMFACILITYLONGITUDE," & _
                        "NUMFACILITYLATITUDE, NUMUTMEASTING, " & _
                        "NUMUTMNORTHING, NUMUTMZONE,  " & _
                        "NUMSMALLBUSINESS, SIC, " & _
                        "NAICS, NUMDUNANDBRADSTREET, " & _
                        "STRFACILITYDESCRIPTION, COMMENTS, " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "airtvproject.SEQ_GAP_FS_FACILITYINFOID.nextval, " & _
                        "'" & GSID & "', '" & Replace(HoldingCompanyName, "'", "''") & "',  " & _
                        "'" & Replace(Facilityname, "'", "''") & "', '" & Replace(facilitystreet1, "'", "''") & "', " & _
                        "'" & Replace(facilitycity, "'", "''") & "', '" & Replace(FacilityState, "'", "''") & "', " & _
                        "'" & Replace(Replace(FacilityZipCode, "'", "''"), "-", "") & "', '" & longitude & "', " & _
                        "'" & latitude & "', '" & UTMEasting & "', " & _
                        "'" & UTMNorthing & "', '" & UTMZone & "', " & _
                        "'" & SmallBusiness & "', '" & Mid(SIC, 1, 4) & "', " & _
                        "'', '" & Replace(Replace(DunnBradstreetNo, "-", ""), "N/A", "") & "', " & _
                        "'test population', 'test populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') " & _
                        "and exists (select * from PFWLK_SIC " & _
                        "where SICCode = '" & SIC & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

            MsgBox(count.ToString())



        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnPopSICtable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopSICtable.Click
        Try
            PopulateSICtable()
        Catch ex As Exception

        End Try
    End Sub
    Sub PopulateSICtable()
        Try
            Dim SIC As String = ""
            Dim SICDesc As String = ""

            SQL = "Select * from airbranch.LookUpSICCodes "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strSICCode")) Then
                    SIC = ""
                Else
                    SIC = dr.Item("strSICCode")
                End If
                If IsDBNull(dr.Item("strSICDesc")) Then
                    SICDesc = ""
                Else
                    SICDesc = dr.Item("strSICDesc")
                End If

                If SIC <> "" Then
                    SQL2 = "Insert into AIRTVApplication.PFWLK_SIC " & _
                    "(SICCode, Strdesc, " & _
                    "Active, UpdateUSer, " & _
                    "updateDate, CreateDate ) " & _
                    "select " & _
                    "'" & SIC & "', '" & Replace(SICDesc, "'", "''") & "', '1', 'MFloyd', sysdate, sysdate from dual " & _
                    "where not exists (select * from AIRTVApplication.PFWLK_SIC Where sicCode = '" & SIC & "')"

                    cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                End If

            End While

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnPFW_EMISSIONUNITMASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFW_EMISSIONUNITMASTER.Click
        Try
            PopulatePFW_EmissionUnitMaster()
        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFW_EmissionUnitMaster()
        Try
            PopulatePFWEUBoiler()
            PopulatePFWEUCoating()
            PopulatePFWEUCrushing()
            PopulatePFWEUDryCleaner()
            PopulatePFWEUDryingEquipment()
            PopulatePFWEUElectroplating()
            PopulatePFWEUFRB()
            PopulatePFWEUICEandTurbine()
            PopulatePFWEULandfill()
            PopulatePFWEULiquidStorage()
            PopulatePFWEUMisc()
            PopulatePFWEUNRBulkMixing()
            PopulatePFWEUOven()
            PopulatePFWEUPrinting()
            PopulatePFWEUReactorVessel()
            PopulatePFWEUSeparationProcess()
            PopulatePFWEUSolidWaste()
            PopulatePFWEUSolventCleaning()
            PopulatePFWEUTextileCoater()

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Sub Populate_EU_Boiler()
        Try


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDeleteEUMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEUMaster.Click
        Try
            DeletePFW_EUMaster()
        Catch ex As Exception

        End Try
    End Sub
    Sub DeletePFW_EUMaster()
        Try
            SQL = "Delete AIRTVApplication.PFW_EUS_FuelBurning "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EUS_FuelSupplier "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EUS_LandFillDeposit "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EUS_LandFillPermit "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EUS_Material "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvApplication.pfw_EU_Boiler "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_Coating "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_Crushing "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_DryCleaner "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_DryingEquipment "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_ElectroPlating "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_FiberReinforceplastic "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_IceAndTurbine "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_LandFill "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_LiquidStorage "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_NRBulkMixing "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_Oven "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_Printing_HAP "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_Printing "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_ReactorVessel "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_SeparationProcess "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_SolidWaste "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_SolventCleaning "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.PFW_EU_TextileCoater "
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()


            SQL = "Delete AIRTVApplication.PFW_EmissionUnitHeader"
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.pfw_emissionunitid"
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete AIRTVApplication.pfw_emissionunitmaster"
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done")
        End Try
    End Sub
    Private Sub btnDeleteFacInformation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFacInformation.Click
        Try
            DeletePFWFacInformation()
        Catch ex As Exception

        End Try
    End Sub
    Sub DeletePFWFacInformation()
        Try
            SQL = "delete airtvapplication.PFW_FS_FACILITYINFORMATION"
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnDeletePermitConditionMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePermitConditionMod.Click
        Try
            DeletePermitConMod()
        Catch ex As Exception

        End Try
    End Sub
    Sub DeletePermitConMod()
        Try
            'SQL = "delete airtvapplication.PFW_FS_FACILITYINFORMATION"
            'cmd = New OracleCommand(SQL, connTVApplication)
            'If connTVApplication.State = ConnectionState.Closed Then
            '    connTVApplication.Open()
            'End If
            'dr = cmd.ExecuteReader
            'dr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnDeletePFWApplicationReason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePFWApplicationReason.Click
        Try
            DeletePFWAppReason()
        Catch ex As Exception

        End Try
    End Sub
    Sub DeletePFWAppReason()
        Try
            SQL = "delete airtvapplication.PFW_APPLICATIONREASON"
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnDeletePermitAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePermitAdmin.Click
        Try
            deletePermitAdmin()
        Catch ex As Exception

        End Try
    End Sub
    Sub deletePermitAdmin()
        Try
            SQL = "delete airtvapplication.PFW_PERMITADMIN"
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnDeleteWarehouseAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteWarehouseAdmin.Click
        Try
            DeletePFWWarehouseAdmin()
        Catch ex As Exception

        End Try
    End Sub
    Sub DeletePFWWarehouseAdmin()
        Try
            DeleteEUBoiler()
            DeletePFW_EUMaster()
            DeletePFWFacInformation()
            DeletePermitConMod()
            DeletePFWAppReason()
            deletePermitAdmin()


            SQL = "Delete AIRTVApplication.PFW_WAREHOUSEADMIN"
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnPopulateLookUps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopulateLookUps.Click
        Try
            SQL = "Insert All " & _
            "into AIRTVProject.GAPLK_ProjectStatus (ProjectStatusCode, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "   values ('1', 'Open', '1', 'Floyd, Michael-153', sysdate, sysdate)  " & _
            "Select * from dual where not exists (select * from AIRTVProject.GAPLK_ProjectStatus " & _
            "  where ProjectStatusCode = '1' )  "

            cmd = New OracleCommand(SQL, ConnTVProject)
            If ConnTVProject.State = ConnectionState.Closed Then
                ConnTVProject.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert All " & _
            "into AIRTVProject.GAPLK_ProjectStatus (ProjectStatusCode, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "   values ('2', 'Pending', '1', 'Floyd, Michael-153', sysdate, sysdate)  " & _
            "Select * from dual where not exists (select * from AIRTVProject.GAPLK_ProjectStatus " & _
            "  where ProjectStatusCode = '2' )  "

            cmd = New OracleCommand(SQL, ConnTVProject)
            If ConnTVProject.State = ConnectionState.Closed Then
                ConnTVProject.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()


            SQL = "Insert All " & _
            "into AIRTVProject.GAPLK_ProjectStatus (ProjectStatusCode, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "   values ('3', 'Submitted', '1', 'Floyd, Michael-153', sysdate, sysdate)  " & _
            "Select * from dual where not exists (select * from AIRTVProject.GAPLK_ProjectStatus " & _
            "  where ProjectStatusCode = '3' )  "

            cmd = New OracleCommand(SQL, ConnTVProject)
            If ConnTVProject.State = ConnectionState.Closed Then
                ConnTVProject.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert All " & _
            "into AIRTVProject.GAPLK_ProjectStatus (ProjectStatusCode, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "   values ('4', 'Deleted', '1', 'Floyd, Michael-153', sysdate, sysdate)  " & _
            "Select * from dual where not exists (select * from AIRTVProject.GAPLK_ProjectStatus " & _
            "  where ProjectStatusCode = '4' )  "

            cmd = New OracleCommand(SQL, ConnTVProject)
            If ConnTVProject.State = ConnectionState.Closed Then
                ConnTVProject.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVProject.GAPLK_ApplicationReason " & _
            "(ApplicationReasonCode, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "Select '100', 'SIP-Initial', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from AIRTVProject.GAPLK_ApplicationReason where ApplicationReasonCode = '100' ) "

            cmd = New OracleCommand(SQL, ConnTVProject)
            If ConnTVProject.State = ConnectionState.Closed Then
                ConnTVProject.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVProject.GAPLK_ApplicationReason " & _
            "(ApplicationReasonCode, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "Select '201', 'TV-Initial', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from AIRTVProject.GAPLK_ApplicationReason where ApplicationReasonCode = '201' ) "

            cmd = New OracleCommand(SQL, ConnTVProject)
            If ConnTVProject.State = ConnectionState.Closed Then
                ConnTVProject.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVProject.GAPLK_ApplicationReason " & _
            "(ApplicationReasonCode, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "Select '202', 'TV-Renewal', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from AIRTVProject.GAPLK_ApplicationReason where ApplicationReasonCode = '202' ) "

            cmd = New OracleCommand(SQL, ConnTVProject)
            If ConnTVProject.State = ConnectionState.Closed Then
                ConnTVProject.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS  " & _
            "(OPERATINGSTATUScODE, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "Select 'O', 'Operating', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS where OPERATINGSTATUScODE = 'O' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS  " & _
            "(OPERATINGSTATUScODE, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "Select 'P', 'Planned', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS where OPERATINGSTATUScODE = 'P' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS  " & _
           "(OPERATINGSTATUScODE, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
           "Select 'C', 'Under Construction', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
           "where not exists (select * from AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS where OPERATINGSTATUScODE = 'C' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS  " & _
           "(OPERATINGSTATUScODE, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
           "Select 'T', 'Temporarily Closed', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
           "where not exists (select * from AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS where OPERATINGSTATUScODE = 'T' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS  " & _
           "(OPERATINGSTATUScODE, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
           "Select 'X', 'Closed/Dismantled', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
           "where not exists (select * from AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS where OPERATINGSTATUScODE = 'X' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS  " & _
           "(OPERATINGSTATUScODE, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
           "Select 'I', 'Seasonal Operation', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
           "where not exists (select * from AIRTVAPPLICATION.PFWLK_OPERATINGSTATUS where OPERATINGSTATUScODE = 'I' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
            "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "Select '1', 'Boiler', '1', 'Boiler', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '1' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
            "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
            "Select '2', 'Chrome Electroplating', '1', 'Chrome Electroplating', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '2' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '3', 'Crushing, Milling, Grinding', '1', 'Crushing, Milling, Grinding', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '3' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '4', 'Dryers, Calciners, & Kilns', '1', 'Dryers, Calciners, & Kilns', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '4' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '5', 'Ovens', '1', 'Ovens', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '5' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '6', 'Fiber Reinforced Plastics', '1', 'Fiber Reinforced Plastics', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '6' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '7', 'Internal Combustion Engines', '1', 'Internal Combustion Engines', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '7' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()


            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '8', 'Landfills', '1', 'Landfills', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '8' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()


            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '9', 'Liquid Storage Vessels', '1', 'Liquid Storage Vessels', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '9' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '10', 'Non-reactive Bulk mixing', '1', 'Non-reactive Bulk mixing', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '10' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '11', 'Coating', '1', 'Coating', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '11' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '12', 'Percholrate Dry Cleaners', '1', 'Percholrate Dry Cleaners', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '12' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '13', 'Printing Operations', '1', 'Printing Operations', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '13' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '14', 'Reactor Vessels', '1', 'Reactor Vessels', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '14' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '15', 'Separation Process', '1', 'Separation Process', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '15' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '16', 'Solid Waste Incinerators', '1', 'Solid Waste Incinerators', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '16' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '17', 'Solvent Cleaning', '1', 'Solvent Cleaning', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '17' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '18', 'Textile Coating', '1', 'Textile Coating', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '18' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE  " & _
 "(EMISSIONUNITTYPECODE, EMISSIONUNITTYPENAME, EMISSIONUNITAPPTYPE, strdesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
 "Select '19', 'Miscellaneous', '1', 'Miscellaneous', '1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
 "where not exists (select * from AIRTVAPPLICATION.PFWLK_EMISSIONUNITTYPE where EMISSIONUNITTYPECODE = '19' ) "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()



            Dim SubPart As String
            Dim Subpartdesc As String

            SQL = "Select * from Airbranch.LookupSubpartSIP " & _
            "order by strSubPart "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                SubPart = ""
                Subpartdesc = ""
                If IsDBNull(dr.Item("strSubPart")) Then
                    SubPart = ""
                Else
                    SubPart = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    Subpartdesc = ""
                Else
                    Subpartdesc = dr.Item("strDescription")
                End If

                If SubPart <> "" Then
                    SQL = "insert into airtvapplication.pfwlk_regulatoryStandard " & _
                    "(regulatoryStandardCode, RegulatoryStandardType, REgulatoryStandardName, " & _
                    "strDesc, Active, UpdateUser, UpdateDate, Createdate) " & _
                    "select " & _
                    "airtvapplication.seq_pfwlk_regulatoryStandard.nextval, 'SIP', " & _
                    "'" & SubPart & "', '" & Replace(Subpartdesc, "'", "''") & "', '1', " & _
                    "'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                    "where not exists (select * from airtvApplication.PFWLK_RegulatoryStandard where RegulatoryStandardType = 'SIP' " & _
                    "and REgulatoryStandardName = '" & SubPart & "' ) "

                    cmd2 = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                    count += 1
                End If
            End While
            dr.Close()



            SQL = "Select * from Airbranch.LookupSubpart60 " & _
            "order by strSubPart "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                SubPart = ""
                Subpartdesc = ""
                If IsDBNull(dr.Item("strSubPart")) Then
                    SubPart = ""
                Else
                    SubPart = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    Subpartdesc = ""
                Else
                    Subpartdesc = dr.Item("strDescription")
                End If

                If SubPart <> "" Then
                    SQL = "insert into airtvapplication.pfwlk_regulatoryStandard " & _
                    "(regulatoryStandardCode, RegulatoryStandardType, REgulatoryStandardName, " & _
                    "strDesc, Active, UpdateUser, UpdateDate, Createdate) " & _
                    "select " & _
                    "airtvapplication.seq_pfwlk_regulatoryStandard.nextval, 'Part 60', " & _
                    "'" & SubPart & "', '" & Replace(Subpartdesc, "'", "''") & "', '1', " & _
                    "'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                    "where not exists (select * from airtvApplication.PFWLK_RegulatoryStandard where RegulatoryStandardType = 'Part 60' " & _
                    "and REgulatoryStandardName = '" & SubPart & "' ) "

                    cmd2 = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                    count += 1
                End If
            End While
            dr.Close()

            SQL = "Select * from Airbranch.LookupSubpart61 " & _
            "order by strSubPart "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                SubPart = ""
                Subpartdesc = ""
                If IsDBNull(dr.Item("strSubPart")) Then
                    SubPart = ""
                Else
                    SubPart = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    Subpartdesc = ""
                Else
                    Subpartdesc = dr.Item("strDescription")
                End If

                If SubPart <> "" Then
                    SQL = "insert into airtvapplication.pfwlk_regulatoryStandard " & _
                    "(regulatoryStandardCode, RegulatoryStandardType, REgulatoryStandardName, " & _
                    "strDesc, Active, UpdateUser, UpdateDate, Createdate) " & _
                    "select " & _
                    "airtvapplication.seq_pfwlk_regulatoryStandard.nextval, 'Part 61', " & _
                    "'" & SubPart & "', '" & Replace(Subpartdesc, "'", "''") & "', '1', " & _
                    "'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                    "where not exists (select * from airtvApplication.PFWLK_RegulatoryStandard where RegulatoryStandardType = 'Part 61' " & _
                    "and REgulatoryStandardName = '" & SubPart & "' ) "

                    cmd2 = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                    count += 1
                End If
            End While
            dr.Close()


            SQL = "Select * from Airbranch.LookupSubpart63 " & _
            "order by strSubPart "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                SubPart = ""
                Subpartdesc = ""
                If IsDBNull(dr.Item("strSubPart")) Then
                    SubPart = ""
                Else
                    SubPart = dr.Item("strSubpart")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    Subpartdesc = ""
                Else
                    Subpartdesc = dr.Item("strDescription")
                End If

                If SubPart <> "" Then
                    SQL = "insert into airtvapplication.pfwlk_regulatoryStandard " & _
                    "(regulatoryStandardCode, RegulatoryStandardType, REgulatoryStandardName, " & _
                    "strDesc, Active, UpdateUser, UpdateDate, Createdate) " & _
                    "select " & _
                    "airtvapplication.seq_pfwlk_regulatoryStandard.nextval, 'Part 63', " & _
                    "'" & SubPart & "', '" & Replace(Subpartdesc, "'", "''") & "', '1', " & _
                    "'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                    "where not exists (select * from airtvApplication.PFWLK_RegulatoryStandard where RegulatoryStandardType = 'Part 63' " & _
                    "and REgulatoryStandardName = '" & SubPart & "' ) "

                    cmd2 = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                    count += 1
                End If
            End While
            dr.Close()

            Dim PollutantCode As String
            Dim PollutantDesc As String
            Dim PollutantType As String
            Dim pollutantStatus As String

            SQL = "Select * from airbranch.EISLK_PollutantCode " & _
            "order by strPollutantType, pollutantCode "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                PollutantCode = ""
                PollutantDesc = ""
                PollutantType = ""
                pollutantStatus = ""

                If IsDBNull(dr.Item("PollutantCode")) Then
                    PollutantCode = ""
                Else
                    PollutantCode = dr.Item("pollutantCode")
                End If
                If IsDBNull(dr.Item("strDesc")) Then
                    PollutantDesc = ""
                Else
                    PollutantDesc = dr.Item("strDesc")
                End If
                If IsDBNull(dr.Item("strPollutantType")) Then
                    PollutantType = ""
                Else
                    PollutantType = dr.Item("strPollutantType")
                End If
                If IsDBNull(dr.Item("Active")) Then
                    pollutantStatus = "0"
                Else
                    pollutantStatus = dr.Item("Active")
                End If

                If PollutantCode <> "" And PollutantDesc <> "" And PollutantType <> "" Then
                    SQL = "Insert into airTVApplication.PFWLK_Airpollutant " & _
                    "(AIRPollutantCode, AIRPollutantType, strDesc, Active, UpdateUser, UpdateDate, CreateDate) " & _
                    "select '" & Replace(PollutantCode, "'", "''") & "', '" & Replace(PollutantType, "'", "''") & "', " & _
                    "'" & Replace(PollutantDesc, "'", "''") & "', '" & pollutantStatus & "', " & _
                    "'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                    "where not exists (select * from airtvApplication.PFWLK_Airpollutant where AIRPollutantCode = '" & PollutantCode & "') "

                    cmd2 = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "insert into airtvapplication.pfwlk_controlUnitType  " & _
                  "(ControlUnitTypeCode, ControlUnitTypeName, strDesc, " & _
                  "Active, UpdateUser, UpdateDate, Createdate) " & _
                  "select " & _
                  "'1', 'Adsorbers and Carbon Drums', 'Adsorbers and Carbon Drums', " & _
                  "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                  "where not exists (select * from airtvApplication.pfwlk_controlUnitType where ControlUnitTypeCode = '1') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.pfwlk_controlUnitType  " & _
                  "(ControlUnitTypeCode, ControlUnitTypeName, strDesc, " & _
                  "Active, UpdateUser, UpdateDate, Createdate) " & _
                  "select " & _
                  "'2', 'Biofiltration', 'Biofiltration', " & _
                  "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                  "where not exists (select * from airtvApplication.pfwlk_controlUnitType where ControlUnitTypeCode = '2') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.pfwlk_controlUnitType  " & _
                  "(ControlUnitTypeCode, ControlUnitTypeName, strDesc, " & _
                  "Active, UpdateUser, UpdateDate, Createdate) " & _
                  "select " & _
                  "'3', 'Condensers and Refrigeration Units', 'Condensers and Refrigeration Units', " & _
                  "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                  "where not exists (select * from airtvApplication.pfwlk_controlUnitType where ControlUnitTypeCode = '3') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.pfwlk_controlUnitType  " & _
                "(ControlUnitTypeCode, ControlUnitTypeName, strDesc, " & _
                "Active, UpdateUser, UpdateDate, Createdate) " & _
                "select " & _
                "'4', 'Cyclones and Settling Chambers', 'Cyclones and Settling Chambers', " & _
                "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                "where not exists (select * from airtvApplication.pfwlk_controlUnitType where ControlUnitTypeCode = '4') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.pfwlk_controlUnitType  " & _
                "(ControlUnitTypeCode, ControlUnitTypeName, strDesc, " & _
                "Active, UpdateUser, UpdateDate, Createdate) " & _
                "select " & _
                "'5', 'Electrostatic Percipitatiors', 'Electrostatic Percipitatiors', " & _
                "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                "where not exists (select * from airtvApplication.pfwlk_controlUnitType where ControlUnitTypeCode = '5') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.pfwlk_controlUnitType  " & _
                "(ControlUnitTypeCode, ControlUnitTypeName, strDesc, " & _
                "Active, UpdateUser, UpdateDate, Createdate) " & _
                "select " & _
                "'6', 'Filter Media', 'Filter Media', " & _
                "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
                "where not exists (select * from airtvApplication.pfwlk_controlUnitType where ControlUnitTypeCode = '6') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.pfwlk_controlUnitType  " & _
            "(ControlUnitTypeCode, ControlUnitTypeName, strDesc, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'7', 'Miscellaneous', 'Miscellaneous', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.pfwlk_controlUnitType where ControlUnitTypeCode = '7') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.pfwlk_controlUnitType  " & _
            "(ControlUnitTypeCode, ControlUnitTypeName, strDesc, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'8', 'Oxidizer', 'Oxidizer', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.pfwlk_controlUnitType where ControlUnitTypeCode = '8') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.pfwlk_controlUnitType  " & _
            "(ControlUnitTypeCode, ControlUnitTypeName, strDesc, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'9', 'Scrubbers', 'Scrubbers', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.pfwlk_controlUnitType where ControlUnitTypeCode = '9') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'0', 'Unknown', 'gas', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '0') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'1', 'Natural Gas', 'gas', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '1') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'2', 'Landfill Gas', 'gas', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '2') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'3', 'Propane', 'gas', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '3') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'4', 'Ultra-low Sulfur Diesel', 'liquid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '4') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'5', 'No. 2 Fuel Oil', 'liquid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '5') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'6', 'No. 6 Fuel Oil', 'liquid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '6') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'7', 'Black Liquor Soilds', 'liquid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '7') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'8', 'Biofuel', 'liquid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '8') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'9', 'Coal', 'solid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '9') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'10', 'Tire Derived Fuel (TDF)', 'solid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '10') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'11', 'Wood Products', 'solid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '11') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'12', 'Other Solid', 'solid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '12') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'13', 'Other Liquid', 'liquid', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '13') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            SQL = "insert into airtvapplication.PFWLK_FUELTYPE  " & _
            "(FUELTYPECODE, STRDESC, FUELSTATE, " & _
            "Active, UpdateUser, UpdateDate, Createdate) " & _
            "select " & _
            "'14', 'Other gas', 'gas', " & _
            "'1', 'Floyd, Michael-153', sysdate, sysdate from Dual " & _
            "where not exists (select * from airtvApplication.PFWLK_FUELTYPE where FUELTYPECODE = '14') "

            cmd2 = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr2 = cmd2.ExecuteReader
            dr2.Close()

            MsgBox("Done", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnAddGapProjectAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGapProjectAdmin.Click
        Try
            SQL = "Insert into airtvproject.GAP_ProjectAdmin " & _
            "(projectid, strairsnumber, " & _
            "ProjectStatus, datStatus, " & _
            "strSubmitBy, datSubmitDate, " & _
            "Comments, " & _
            "CreateDate, CreateUser, " & _
            "UpdateDate, UpdateUser, " & _
            "Valid, Active) " & _
            "select " & _
            "AIRTVProject.seq_GAP_Projectid.nextval, '041305100008', " & _
            "'1', sysdate, " & _
            "'Floyd, Michael-153', sysdate, " & _
            "'Test Data', " & _
            "sysdate, 'Floyd, Michael-153', " & _
            "sysdate, 'Floyd, Michael-153', " & _
            "'0', '1' " & _
            "from dual "

            cmd = New OracleCommand(SQL, ConnTVProject)
            If ConnTVProject.State = ConnectionState.Closed Then
                ConnTVProject.Open()
            End If
            dr = cmd.ExecuteReader

            SQL = "Insert into AIRTVproject.GAP_FS_FACILITYINFORMATION " & _
            "(FACILITYINFORMATIONID, " & _
            "ProjectID, STRHOLDINGCOMPANYNAME, " & _
            "STRFACILITYNAME, STRFACILITYSTREET1, " & _
            "STRFACILITYCITY, STRFACILITYSTATE, " & _
            "STRFACILITYZIPCODE, NUMFACILITYLONGITUDE," & _
            "NUMFACILITYLATITUDE, NUMHORIZONTALACCURACY, " & _
            "HORIZONTALCOLLECTIONMETHOD, HORIZONTALREFERENCEDATUM, " & _
            "NUMUTMEASTING, " & _
            "NUMUTMNORTHING, NUMUTMZONE,  " & _
            "NUMSMALLBUSINESS, SIC, " & _
            "NAICS, NUMDUNANDBRADSTREET, " & _
            "STRFACILITYDESCRIPTION, COMMENTS, " & _
            "CREATEDATE, UPDATEDATE, " & _
            "UPDATEUSER, " & _
            "VALID, ACTIVE) " & _
            "select  " & _
            "airtvproject.SEQ_GAP_FS_FACILITYINFOID.nextval, " & _
            "AIRTVProject.seq_GAP_Projectid.currval, 'Holding Co. Test', " & _
            "'Facility Name', '123 Any Street', " & _
            "'Town', 'GA', " & _
            "'12345', '34.000', " & _
            "'-84.99', '', " & _
            "'', '', " & _
            "'', " & _
            "'', '', " & _
            "'50', '2021', " & _
            "'7221', '', " & _
            "'test description', 'Test Comment', " & _
            "sysdate, sysdate, " & _
            "'Floyd, Michael-153', " & _
            "'0', '1' " & _
            "from dual "

            cmd = New OracleCommand(SQL, ConnTVProject)
            If ConnTVProject.State = ConnectionState.Closed Then
                ConnTVProject.Open()
            End If
            dr = cmd.ExecuteReader

            MsgBox("done")

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnTestGapFacilityInformation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestGapFacilityInformation.Click
        Try





            MsgBox("done")

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub btnDeleteEUBoiler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEUBoiler.Click
        Try
            DeleteEUBoiler()

        Catch ex As Exception

        End Try
    End Sub
    Sub DeleteEUBoiler()
        Try
            SQL = "delete airtvapplication.PFW_EU_Boiler"
            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Done")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnEUBoiler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUBoiler.Click
        Try

            PopulatePFWEUBoiler()

        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFWEUBoiler()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim FuelConfiguration As String = ""
            Dim HeatInputCAP As String = ""
            Dim LastModDate As String = ""
            Dim LastMod As String = ""
            Dim DraftType As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""
            Dim TVEquipmentID As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitBoilersFurnacesOtherIndirectContact.EquipmentID,  " & _
            "tblEmissionUnitBoilersFurnacesOtherIndirectContact.Description, " & _
            "tblEmissionUnitBoilersFurnacesOtherIndirectContact.HeatInputCapacity, " & _
            "tblEmissionUnitBoilersFurnacesOtherIndirectContact.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitBoilersFurnacesOtherIndirectContact.Comments, " & _
            "tblEmissionUnitBoilersFurnacesOtherIndirectContact.Manufacturer, " & _
            "tblEmissionUnitBoilersFurnacesOtherIndirectContact.ModelNumber, " & _
            "tblEmissionUnitBoilersFurnacesOtherIndirectContact.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitBoilersFurnacesOtherIndirectContact.InstallationDate " & _
            "FROM tblEmissionUnitBoilersFurnacesOtherIndirectContact INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitBoilersFurnacesOtherIndirectContact.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitBoilersFurnacesOtherIndirectContact.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                'If IsDBNull(GATVdr.ite("EquipmentID")) Then
                '    TVEquipmentID = ""
                'Else
                '    TVEquipmentID = GATVdr.item("EquipmentID")
                'End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Boilers, Furnaces & Other Indirect Contact Heat Generating Equipment" Then
                    EUType = "1"
                Else
                    EUType = "19"
                End If
                If IsDBNull(GATVdr.item("Description")) Then
                    FuelConfiguration = ""
                Else
                    FuelConfiguration = GATVdr.item("Description")
                End If
                If IsDBNull(GATVdr.item("HeatInputCapacity")) Then
                    HeatInputCAP = ""
                Else
                    HeatInputCAP = GATVdr.item("HeatInputCapacity")
                End If
                DraftType = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    LastModDate = ""
                Else
                    LastModDate = GATVdr.item("DateManufactured_Reconstructed")
                End If
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    LastMod = ""
                Else
                    LastMod = GATVdr.item("DateManufactured_Reconstructed")
                End If
                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsNumeric(LastModDate) And LastModDate.Length = 4 Then
                    LastModDate = "01-Jan-" & LastModDate
                End If
                If IsDate(LastModDate) Then
                    LastModDate = Format(CDate(LastModDate), "dd-MMM-yyyy")
                Else
                    LastModDate = ""
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If

                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_Boiler " & _
                        "(pfw_eu_boilerid, " & _
                        "emissionunitid, gsid, " & _
                        "strFuelBurningConfiguration, numHeatInputCapacity, " & _
                        "strDraftType, " & _
                        "datLastModification, strLastModification, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_BOILERID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(FuelConfiguration, "'", "''") & "', '" & Replace(HeatInputCAP, "'", "''") & "', " & _
                        "'', " & _
                        "'" & LastModDate & "', '" & Replace(LastMod, "'", "''") & "', " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SaveFuelBurning(oldID, GSID, temp)


                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnEUCoating_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUCoating.Click
        Try
            PopulatePFWEUCoating()
        Catch ex As Exception
          
        End Try
    End Sub
    Sub PopulatePFWEUCoating()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim COATINGTYPE As String = ""
            Dim STRCOATINGOTHER As String = ""
            Dim NUMVOCPOTENTIAL As String = ""
            Dim NUMMAXVOC As String = ""
            Dim STRITEMCOATED As String = ""
            Dim NUMHOURSOPERATED As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitPaintingandCoating.TypeOfCoatingDone, " & _
            "tblEmissionUnitPaintingandCoating.DescriptionofOther,  " & _
            "tblEmissionUnitPaintingandCoating.VOCPotentialToEmit, " & _
            "tblEmissionUnitPaintingandCoating.MaximumActualDailyVOCEmissions, " & _
            "tblEmissionUnitPaintingandCoating.Manufacturer, " & _
            "tblEmissionUnitPaintingandCoating.ModelNumber, " & _
            "tblEmissionUnitPaintingandCoating.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitPaintingandCoating.InstallationDate " & _
            "FROM tblEmissionUnitPaintingandCoating INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitPaintingandCoating.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitPaintingandCoating.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If

                If EUType = "Painting & Coating Operations" Then
                    EUType = "11"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("TypeOfCoatingDone")) Then
                    COATINGTYPE = ""
                Else
                    COATINGTYPE = GATVdr.item("TypeOfCoatingDone")
                End If
                If IsDBNull(GATVdr.item("DescriptionofOther")) Then
                    STRCOATINGOTHER = ""
                Else
                    STRCOATINGOTHER = GATVdr.item("DescriptionofOther")
                End If
                If IsDBNull(GATVdr.item("VOCPotentialToEmit")) Then
                    NUMVOCPOTENTIAL = "-1"
                Else
                    NUMVOCPOTENTIAL = GATVdr.item("VOCPotentialToEmit")
                End If
                If IsDBNull(GATVdr.item("MaximumActualDailyVOCEmissions")) Then
                    NUMMAXVOC = ""
                Else
                    NUMMAXVOC = GATVdr.item("MaximumActualDailyVOCEmissions")
                End If
                STRITEMCOATED = ""
                NUMHOURSOPERATED = ""

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_COATING " & _
                        "(PFW_EU_COATINGID, " & _
                        "emissionunitid, gsid, " & _
                        "COATINGTYPE, STRCOATINGOTHER, " & _
                        "NUMVOCPOTENTIAL, NUMMAXVOC, " & _
                        "STRITEMCOATED, NUMHOURSOPERATED, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_COATINGID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(COATINGTYPE, "'", "''") & "', '" & Replace(STRCOATINGOTHER, "'", "''") & "', " & _
                        "'" & NUMVOCPOTENTIAL & "', '" & NUMMAXVOC & "',  " & _
                        "'" & STRITEMCOATED & "', '" & Replace(NUMHOURSOPERATED, "'", "''") & "', " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUCrushing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUCrushing.Click
        Try
            PopulatePFWEUCrushing()
        Catch ex As Exception
            
        End Try
    End Sub
    Sub PopulatePFWEUCrushing()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim FuelConfiguration As String = ""
            Dim HeatInputCAP As String = ""
            Dim LastModDate As String = ""
            Dim LastMod As String = ""
            Dim DraftType As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""
            Dim UnitType As String = ""
            Dim UnitTypeOther As String = ""
            Dim Heated As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitCrushingMillingGrinding.Description, " & _
            "tblEmissionUnitCrushingMillingGrinding.TypeofUnit, " & _
            "tblEmissionUnitCrushingMillingGrinding.UnitHeated, " & _
            "tblEmissionUnitCrushingMillingGrinding.DescriptionofOther, " & _
            "tblEmissionUnitCrushingMillingGrinding.Manufacturer, " & _
            "tblEmissionUnitCrushingMillingGrinding.ModelNumber, " & _
            "tblEmissionUnitCrushingMillingGrinding.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitCrushingMillingGrinding.InstallationDate " & _
            "FROM tblEmissionUnitCrushingMillingGrinding INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitCrushingMillingGrinding.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitCrushingMillingGrinding.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Crushing, Milling & Grinding" Then
                    EUType = "3"
                Else
                    EUType = "19"
                End If
                If IsDBNull(GATVdr.item("TypeofUnit")) Then
                    UnitType = ""
                Else
                    UnitType = GATVdr.item("TypeofUnit")
                End If
                If IsDBNull(GATVdr.item("DescriptionofOther")) Then
                    UnitTypeOther = ""
                Else
                    UnitTypeOther = GATVdr.item("DescriptionofOther")
                End If
                If IsDBNull(GATVdr.item("UnitHeated")) Then
                    Heated = ""
                Else
                    Heated = GATVdr.item("UnitHeated")
                End If
                If Heated = "True" Then
                    Heated = "1"
                Else
                    If Heated = "False" Then
                        Heated = "0"
                    End If
                End If
                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_Crushing " & _
                        "(pfw_eu_CrushingID, " & _
                        "emissionunitid, gsid, " & _
                        "CrushingUnitType, strOtherType, " & _
                        "numUnitHeated, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_CrushingID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(UnitType, "'", "''") & "', '" & Replace(Mid(UnitTypeOther, 1, 400), "'", "''") & "', " & _
                        "'" & Replace(Heated, "'", "''") & "', " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)
                        SaveMaterials(oldID, GSID, temp, "Crusher")


                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()



        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUDryCleaner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUDryCleaner.Click
        Try
            PopulatePFWEUDryCleaner()
        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFWEUDryCleaner()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim MachineType As String = ""
            Dim Max12MonthUse As String = ""
            Dim InitialNotification As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitPerchloroethyleneDryCleaners.MachineType, " & _
            "tblEmissionUnitPerchloroethyleneDryCleaners.MaxUsed12Mo, " & _
            "tblEmissionUnitPerchloroethyleneDryCleaners.InititalNotification, " & _
            "tblEmissionUnitPerchloroethyleneDryCleaners.Manufacturer, " & _
            "tblEmissionUnitPerchloroethyleneDryCleaners.ModelNumber, " & _
            "tblEmissionUnitPerchloroethyleneDryCleaners.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitPerchloroethyleneDryCleaners.InstallationDate " & _
            "FROM tblEmissionUnitPerchloroethyleneDryCleaners INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitPerchloroethyleneDryCleaners.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitPerchloroethyleneDryCleaners.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If

                If EUType = "Perchloroethylene Dry Cleaners" Then
                    EUType = "12"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("MachineType")) Then
                    MachineType = ""
                Else
                    MachineType = GATVdr.item("MachineType")
                End If
                If IsDBNull(GATVdr.item("MaxUsed12Mo")) Then
                    Max12MonthUse = ""
                Else
                    Max12MonthUse = GATVdr.item("MaxUsed12Mo")
                End If
                If IsDBNull(GATVdr.item("InititalNotification")) Then
                    InitialNotification = ""
                Else
                    InitialNotification = GATVdr.item("InititalNotification")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_DryCleaner " & _
                      "(pfw_eu_DryCleanerID, " & _
                      "emissionunitid, gsid, " & _
                      "NUMMACHINETYPE, NUMMAX12MONTHUSE, " & _
                      "NUMINITIALNOTIFICATION, " & _
                      "comments, Active) " & _
                      "Select " & _
                      "airtvProject.SEQ_GAP_EU_DryCleanerID.nextval,  " & _
                      "'" & temp & "', '" & GSID & "', " & _
                      "'" & MachineType & "', '" & Max12MonthUse & "', " & _
                      "'" & InitialNotification & "', " & _
                      "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                      "from dual " & _
                      "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                      "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUDryingEquipment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUDryingEquipment.Click
        Try
            PopulatePFWEUDryingEquipment()
        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFWEUDryingEquipment()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""
            Dim EquipmentType As String = ""
            Dim DryingEquipment As String = ""
            Dim OtherType As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.TypeofUnit,  " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.SpecificType,  " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.DescriptionofOther,  " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.Manufacturer, " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.ModelNumber, " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.InstallationDate " & _
            "FROM tblEmissionUnitDryersCalcinersKilnsOvens INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitDryersCalcinersKilnsOvens.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitDryersCalcinersKilnsOvens.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) " & _
            "where TypeOfUnit <> 4 "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Dryers, Calciners, Kilns & Ovens" Then
                    EUType = "4"
                Else
                    EUType = "19"
                End If
                If IsDBNull(GATVdr.item("TypeofUnit")) Then
                    EquipmentType = ""
                Else
                    EquipmentType = GATVdr.item("TypeofUnit")
                End If
                If IsDBNull(GATVdr.item("SpecificType")) Then
                    DryingEquipment = ""
                Else
                    DryingEquipment = GATVdr.item("SpecificType")
                End If
                If IsDBNull(GATVdr.item("DescriptionofOther")) Then
                    OtherType = ""
                Else
                    OtherType = GATVdr.item("DescriptionofOther")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_DryingEquipment " & _
                        "(pfw_eu_DryingEquipmentID, " & _
                        "emissionunitid, gsid, " & _
                        "strTypeofEquipment, DryingEquipmentType, " & _
                        "strOtherType, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_DryingEquipmentID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(EquipmentType, "'", "''") & "', '" & Replace(DryingEquipment, "'", "''") & "', " & _
                        "'" & Replace(Mid(OtherType, 1, 400), "'", "''") & "', " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)
                        SaveMaterials(oldID, GSID, temp, "Drying Equipment")

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUElectroplating_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUElectroplating.Click
        Try
            PopulatePFWEUElectroplating()
        Catch ex As Exception
          
        End Try
    End Sub
    Sub PopulatePFWEUElectroplating()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim FuelConfiguration As String = ""
            Dim HeatInputCAP As String = ""
            Dim LastModDate As String = ""
            Dim LastMod As String = ""
            Dim DraftType As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""
            Dim WettingAgent As String = ""
            Dim Part63Notification As String = ""
            Dim ChromicAcidUsed As String = ""
            Dim TankType As String = ""
            Dim HardElectrPlating As String = ""
            Dim PartsPlated As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitChromeElectroplating.TrivalentWettingAgent, " & _
            "tblEmissionUnitChromeElectroplating.InitialNotification,  " & _
            "tblEmissionUnitChromeElectroplating.UseChromic,  " & _
            "tblEmissionUnitChromeElectroplating.TankType,  " & _
            "tblEmissionUnitChromeElectroplating.TankUsageOption, " & _
            "tblEmissionUnitChromeElectroplating.PartsPlatedDescription, " & _
            "tblEmissionUnitChromeElectroplating.Manufacturer, " & _
            "tblEmissionUnitChromeElectroplating.ModelNumber, " & _
            "tblEmissionUnitChromeElectroplating.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitChromeElectroplating.InstallationDate " & _
            "FROM tblEmissionUnitChromeElectroplating INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitChromeElectroplating.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitChromeElectroplating.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Chromium Electroplating & Chromium Anodizing Tanks" Then
                    EUType = "2"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("TrivalentWettingAgent")) Then
                    WettingAgent = ""
                Else
                    WettingAgent = GATVdr.item("TrivalentWettingAgent")
                End If
                If IsDBNull(GATVdr.item("InitialNotification")) Then
                    Part63Notification = ""
                Else
                    Part63Notification = GATVdr.item("InitialNotification")
                End If
                If IsDBNull(GATVdr.item("UseChromic")) Then
                    ChromicAcidUsed = ""
                Else
                    ChromicAcidUsed = GATVdr.item("UseChromic")
                End If
                If IsDBNull(GATVdr.item("TankType")) Then
                    TankType = ""
                Else
                    TankType = GATVdr.item("TankType")
                End If
                If IsDBNull(GATVdr.item("TankUsageOption")) Then
                    HardElectrPlating = ""
                Else
                    HardElectrPlating = GATVdr.item("TankUsageOption")
                End If
                If IsDBNull(GATVdr.item("PartsPlatedDescription")) Then
                    PartsPlated = ""
                Else
                    PartsPlated = GATVdr.item("PartsPlatedDescription")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_ELECTROPLATING " & _
                        "(pfw_eu_electroplatingID, " & _
                        "emissionunitid, gsid, " & _
                        "STRWETTINGAGENT, NUMINITIALPART63NOTIFICATION, " & _
                        "NUMCHROMICACIDUSE, " & _
                        "NUMTANKTYPE, NUMHARDELECTROPLATING, " & _
                        "STRPARTSPLATEDDESC, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_ELECTROPLATINGID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(WettingAgent, "'", "''") & "', '" & Replace(Part63Notification, "'", "''") & "', " & _
                        "'" & ChromicAcidUsed & "', " & _
                        "'" & TankType & "', '" & Replace(HardElectrPlating, "'", "''") & "', " & _
                        "'" & Replace(PartsPlated, "'", "''") & "', " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUFRB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUFRB.Click
        Try
            PopulatePFWEUFRB()
        Catch ex As Exception
         
        End Try
    End Sub
    Sub PopulatePFWEUFRB()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim FRBType As String = ""
            Dim OtherType As String = ""
            Dim ProductDesc As String = ""
            Dim VaporSupressant As String = ""
            Dim GunModDistance As String = ""
            Dim OverSprayed As String = ""
            Dim Thickness As String = ""
            Dim CupGelTime As String = ""
            Dim AppRate As String = ""
            Dim AirTemp As String = ""
            Dim AirVelocity As String = ""
            Dim HAPProductionResin As String = ""
            Dim HAPProductionResinUnits As String = ""
            Dim HAPToolResin As String = ""
            Dim HAPToolResinUnits As String = ""
            Dim OtherResin As String = ""
            Dim OtherResinUnits As String = ""
            Dim HAPPigmentGelCoat As String = ""
            Dim HAPPigmentGelCoatUnits As String = ""
            Dim HAPToolGelCoat As String = ""
            Dim HAPToolGelCoatUnits As String = ""
            Dim HAPClearGelCoat As String = ""
            Dim HAPClearGelCoatUnits As String = ""
            Dim HAPBaseGelCoat As String = ""
            Dim HAPBaseGelCoatUnits As String = ""
            Dim HAPOtherGelCoat As String = ""
            Dim HAPOtherGelCoatUnits As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitFiberPlastic.FRPProcessType, " & _
            "tblEmissionUnitFiberPlastic.FRPProcessTypeOther, " & _
            "tblEmissionUnitFiberPlastic.ProductDescription,  " & _
            "tblEmissionUnitFiberPlastic.VaporSupressant, " & _
            "tblEmissionUnitFiberPlastic.GunToMold, " & _
            "tblEmissionUnitFiberPlastic.Overpsrayed, " & _
            "tblEmissionUnitFiberPlastic.Thickness, " & _
            "tblEmissionUnitFiberPlastic.CupGelTime, " & _
            "tblEmissionUnitFiberPlastic.ApplicationRate, " & _
            "tblEmissionUnitFiberPlastic.AirTempF, " & _
            "tblEmissionUnitFiberPlastic.AirVeloOverMold, " & _
            "tblEmissionUnitFiberPlastic.HAPProductionResin, " & _
            "tblEmissionUnitFiberPlastic.HAPProductionResinUnits, " & _
            "tblEmissionUnitFiberPlastic.HAPToolingResin, " & _
            "tblEmissionUnitFiberPlastic.HAPToolingResinUnits, " & _
            "tblEmissionUnitFiberPlastic.HAPOtherResin, " & _
            "tblEmissionUnitFiberPlastic.HAPOtherResinUnits, " & _
            "tblEmissionUnitFiberPlastic.HAPPigmentedGelCoat, " & _
            "tblEmissionUnitFiberPlastic.HAPPigmentedGelCoatUnits, " & _
            "tblEmissionUnitFiberPlastic.HAPToolingGelCoat, " & _
            "tblEmissionUnitFiberPlastic.HAPToolingGelCoatUnits, " & _
            "tblEmissionUnitFiberPlastic.HAPClearGelCoat, " & _
            "tblEmissionUnitFiberPlastic.HAPClearGelCoatUnits, " & _
            "tblEmissionUnitFiberPlastic.HAPBasecoatGelCoat, " & _
            "tblEmissionUnitFiberPlastic.HAPBasecoatGelCoatUnits, " & _
            "tblEmissionUnitFiberPlastic.HAPOtherGelCoat, " & _
            "tblEmissionUnitFiberPlastic.HAPOtherGelCoatUnits, " & _
            "tblEmissionUnitFiberPlastic.Manufacturer, " & _
            "tblEmissionUnitFiberPlastic.ModelNumber, " & _
            "tblEmissionUnitFiberPlastic.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitFiberPlastic.InstallationDate " & _
            "FROM tblEmissionUnitFiberPlastic INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitFiberPlastic.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitFiberPlastic.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Fiber Reinforced Plastic Manufacturing Operations" Then
                    EUType = "6"
                Else
                    EUType = "19"
                End If
                If IsDBNull(GATVdr.item("FRPProcessType")) Then
                    FRBType = ""
                Else
                    FRBType = GATVdr.item("FRPProcessType")
                End If
                If IsDBNull(GATVdr.item("FRPProcessTypeOther")) Then
                    OtherType = ""
                Else
                    OtherType = GATVdr.item("FRPProcessTypeOther")
                End If
                If IsDBNull(GATVdr.item("ProductDescription")) Then
                    ProductDesc = ""
                Else
                    ProductDesc = GATVdr.item("ProductDescription")
                End If
                If IsDBNull(GATVdr.item("VaporSupressant")) Then
                    VaporSupressant = ""
                Else
                    VaporSupressant = GATVdr.item("VaporSupressant")
                End If
                Select Case VaporSupressant
                    Case True
                        VaporSupressant = "1"
                    Case False
                        VaporSupressant = "0"
                    Case Else
                        VaporSupressant = "0"
                End Select
                If IsDBNull(GATVdr.item("GunToMold")) Then
                    GunModDistance = ""
                Else
                    GunModDistance = GATVdr.item("GunToMold")
                End If
                If IsDBNull(GATVdr.item("Overpsrayed")) Then
                    OverSprayed = ""
                Else
                    OverSprayed = GATVdr.item("Overpsrayed")
                End If
                If IsDBNull(GATVdr.item("Thickness")) Then
                    Thickness = ""
                Else
                    Thickness = GATVdr.item("Thickness")
                End If
                If IsDBNull(GATVdr.item("CupGelTime")) Then
                    CupGelTime = ""
                Else
                    CupGelTime = GATVdr.item("CupGelTime")
                End If
                If IsDBNull(GATVdr.item("ApplicationRate")) Then
                    AppRate = ""
                Else
                    AppRate = GATVdr.item("ApplicationRate")
                End If
                If IsDBNull(GATVdr.item("AirTempF")) Then
                    AirTemp = ""
                Else
                    AirTemp = GATVdr.item("AirTempF")
                End If
                If IsDBNull(GATVdr.item("AirVeloOverMold")) Then
                    AirVelocity = ""
                Else
                    AirVelocity = GATVdr.item("AirVeloOverMold")
                End If
                If IsDBNull(GATVdr.item("HAPProductionResin")) Then
                    HAPProductionResin = ""
                Else
                    HAPProductionResin = GATVdr.item("HAPProductionResin")
                End If
                If IsDBNull(GATVdr.item("HAPProductionResinUnits")) Then
                    HAPProductionResinUnits = ""
                Else
                    HAPProductionResinUnits = GATVdr.item("HAPProductionResinUnits")
                End If
                HAPProductionResinUnits = ""

                If IsDBNull(GATVdr.item("HAPToolingResin")) Then
                    HAPToolResin = ""
                Else
                    HAPToolResin = GATVdr.item("HAPToolingResin")
                End If
                If IsDBNull(GATVdr.item("HAPToolingResinUnits")) Then
                    HAPToolResinUnits = ""
                Else
                    HAPToolResinUnits = GATVdr.item("HAPToolingResinUnits")
                End If
                HAPToolResinUnits = ""

                If IsDBNull(GATVdr.item("HAPOtherResin")) Then
                    OtherResin = ""
                Else
                    OtherResin = GATVdr.item("HAPOtherResin")
                End If
                If IsDBNull(GATVdr.item("HAPOtherResinUnits")) Then
                    OtherResinUnits = ""
                Else
                    OtherResinUnits = GATVdr.item("HAPOtherResinUnits")
                End If
                OtherResinUnits = ""

                If IsDBNull(GATVdr.item("HAPPigmentedGelCoat")) Then
                    HAPPigmentGelCoat = ""
                Else
                    HAPPigmentGelCoat = GATVdr.item("HAPPigmentedGelCoat")
                End If
                If IsDBNull(GATVdr.item("HAPPigmentedGelCoatUnits")) Then
                    HAPPigmentGelCoatUnits = ""
                Else
                    HAPPigmentGelCoatUnits = GATVdr.item("HAPPigmentedGelCoatUnits")
                End If
                HAPPigmentGelCoatUnits = ""

                If IsDBNull(GATVdr.item("HAPToolingGelCoat")) Then
                    HAPToolGelCoat = ""
                Else
                    HAPToolGelCoat = GATVdr.item("HAPToolingGelCoat")
                End If
                If IsDBNull(GATVdr.item("HAPToolingGelCoatUnits")) Then
                    HAPToolGelCoatUnits = ""
                Else
                    HAPToolGelCoatUnits = GATVdr.item("HAPToolingGelCoatUnits")
                End If
                HAPToolGelCoatUnits = ""

                If IsDBNull(GATVdr.item("HAPClearGelCoat")) Then
                    HAPClearGelCoat = ""
                Else
                    HAPClearGelCoat = GATVdr.item("HAPClearGelCoat")
                End If
                If IsDBNull(GATVdr.item("HAPClearGelCoatUnits")) Then
                    HAPClearGelCoatUnits = ""
                Else
                    HAPClearGelCoatUnits = GATVdr.item("HAPClearGelCoatUnits")
                End If
                HAPClearGelCoatUnits = ""

                If IsDBNull(GATVdr.item("HAPBasecoatGelCoat")) Then
                    HAPBaseGelCoat = ""
                Else
                    HAPBaseGelCoat = GATVdr.item("HAPBasecoatGelCoat")
                End If
                If IsDBNull(GATVdr.item("HAPBasecoatGelCoatUnits")) Then
                    HAPBaseGelCoatUnits = ""
                Else
                    HAPBaseGelCoatUnits = GATVdr.item("HAPBasecoatGelCoatUnits")
                End If
                HAPBaseGelCoatUnits = ""

                If IsDBNull(GATVdr.item("HAPOtherGelCoat")) Then
                    HAPOtherGelCoat = ""
                Else
                    HAPOtherGelCoat = GATVdr.item("HAPOtherGelCoat")
                End If
                If IsDBNull(GATVdr.item("HAPOtherGelCoatUnits")) Then
                    HAPOtherGelCoatUnits = ""
                Else
                    HAPOtherGelCoatUnits = GATVdr.item("HAPOtherGelCoatUnits")
                End If
                HAPOtherGelCoatUnits = ""

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_FiberReinforcePlastic " & _
                        "(PFW_EU_FIBERREINFORCEPLASTICID, " & _
                        "emissionunitid, gsid, " & _
                        "FRBTYPE, STROTHERTYPE, " & _
                        "STRPRODUCTDESC, NUMVAPORSUPRESSANT, " & _
                        "NUMGUNMOLDDISTANCT, NUMOVERSPRAYED, " & _
                        "NUMTHICKNESS, NUMCUPGELTIME, " & _
                        "NUMAPPRATE, NUMAIRTEMP, " & _
                        "NUMAIRVELOCITY, NUMHAPPRODUCTIONRESIN, " & _
                        "NUMHAPPRODUCTIONRESINUNITS, NUMHAPTOOLRESIN, " & _
                        "NUMHAPTOOLRESINUNITS, NUMHAPOTHERRESIN, " & _
                        "NUMHAPOTHERRESINUNITS, NUMHAPPIGMENTGELCOAT, " & _
                        "NUMHAPPIGMENTGELCOATUNITS, NUMHAPTOOLGELCOAT, " & _
                        "NUMHAPTOOLGELCOATUNITS, NUMHAPCLEARGELCOAT, " & _
                        "NUMHAPCLEARGELCOATUNITS, NUMHAPBASEGELCOAT, " & _
                        "NUMHAPBASEGELCOATUNITS, NUMHAPOTHERGELCOAT, " & _
                        "NUMHAPOTHERGELCOATUNITS, " & _
                      "comments, Active) " & _
                      "Select " & _
                      "airtvProject.SEQ_GAP_EU_FIBERREINFORCEPID.nextval,  " & _
                      "'" & temp & "', '" & GSID & "', " & _
                     "'" & FRBType & "', '" & Replace(Mid(OtherType, 1, 400), "'", "''") & "',  " & _
                     "'" & Replace(Mid(ProductDesc, 1, 2000), "'", "''") & "',  '" & VaporSupressant & "', " & _
                     "'" & GunModDistance & "',  '" & OverSprayed & "', " & _
                     "'" & Thickness & "',  '" & CupGelTime & "', " & _
                     "'" & AppRate & "',  '" & AirTemp & "', " & _
                     "'" & AirVelocity & "',  '" & HAPProductionResin & "', " & _
                     "'" & HAPProductionResinUnits & "',  '" & HAPToolResin & "',  " & _
                     "'" & HAPToolResinUnits & "',  '" & OtherResin & "', " & _
                     "'" & OtherResinUnits & "',  '" & HAPPigmentGelCoat & "', " & _
                     "'" & HAPPigmentGelCoatUnits & "',  '" & HAPToolGelCoat & "', " & _
                     "'" & HAPToolGelCoatUnits & "',  '" & HAPClearGelCoat & "', " & _
                     "'" & HAPClearGelCoatUnits & "',  '" & HAPBaseGelCoat & "', " & _
                     "'" & HAPBaseGelCoatUnits & "',  '" & HAPOtherGelCoat & "', " & _
                     "'" & HAPOtherGelCoatUnits & "', " & _
                      "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                      "from dual " & _
                      "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                      "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUICEandTurbine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUICEandTurbine.Click
        Try
            PopulatePFWEUICEandTurbine()
        Catch ex As Exception
          
        End Try
    End Sub
    Sub PopulatePFWEUICEandTurbine()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim ICETYPE As String = ""
            Dim EPACERTIFICATION As String = "1"
            Dim EPACERTIFICATIONOTHER As String = ""
            Dim NUMPOWEROUTPUT As String = ""
            Dim NUMPOWEROUTPUTUNITS As String = ""
            Dim NUMMAXPOWEROUTPUT As String = ""
            Dim NUMMAXPOWEROUTPUTUNITS As String = ""
            Dim NUMMAXHOURSOPERATINGANNUAL As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitICCombustionTurbineEngines.EngineType, " & _
            "tblEmissionUnitICCombustionTurbineEngines.DesignPowerOutput, " & _
            "tblEmissionUnitICCombustionTurbineEngines.DesignPowerUnit, " & _
            "tblEmissionUnitICCombustionTurbineEngines.OperationalMaximumPowerOutput, " & _
            "tblEmissionUnitICCombustionTurbineEngines.OperarationalMaxPowerUnit, " & _
            "tblEmissionUnitICCombustionTurbineEngines.OperatingSchedule, " & _
            "tblEmissionUnitICCombustionTurbineEngines.HeatInputCapacity, " & _
            "tblEmissionUnitICCombustionTurbineEngines.Manufacturer, " & _
            "tblEmissionUnitICCombustionTurbineEngines.ModelNumber, " & _
            "tblEmissionUnitICCombustionTurbineEngines.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitICCombustionTurbineEngines.InstallationDate " & _
            "FROM tblEmissionUnitICCombustionTurbineEngines INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitICCombustionTurbineEngines.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitICCombustionTurbineEngines.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Internal Combustion Engines & Combustion Turbines" Then
                    EUType = "7"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("EngineType")) Then
                    ICETYPE = ""
                Else
                    ICETYPE = GATVdr.item("EngineType")
                End If
                Select Case ICETYPE
                    Case "Combustion Turbine"
                        ICETYPE = "1"
                    Case "Internal Combustion"
                        ICETYPE = "2"
                    Case Else
                        ICETYPE = ""
                End Select
                If IsDBNull(GATVdr.item("DesignPowerOutput")) Then
                    NUMPOWEROUTPUT = ""
                Else
                    NUMPOWEROUTPUT = GATVdr.item("DesignPowerOutput")
                End If
                If IsDBNull(GATVdr.item("DesignPowerUnit")) Then
                    NUMPOWEROUTPUTUNITS = ""
                Else
                    NUMPOWEROUTPUTUNITS = GATVdr.item("DesignPowerUnit")
                End If
                NUMPOWEROUTPUTUNITS = ""

                If IsDBNull(GATVdr.item("OperationalMaximumPowerOutput")) Then
                    NUMMAXPOWEROUTPUT = ""
                Else
                    NUMMAXPOWEROUTPUT = GATVdr.item("OperationalMaximumPowerOutput")
                End If
                If IsDBNull(GATVdr.item("OperarationalMaxPowerUnit")) Then
                    NUMMAXPOWEROUTPUTUNITS = ""
                Else
                    NUMMAXPOWEROUTPUTUNITS = GATVdr.item("OperarationalMaxPowerUnit")
                End If
                NUMMAXPOWEROUTPUTUNITS = ""

                If IsDBNull(GATVdr.item("OperatingSchedule")) Then
                    NUMMAXHOURSOPERATINGANNUAL = ""
                Else
                    NUMMAXHOURSOPERATINGANNUAL = GATVdr.item("OperatingSchedule")
                End If
              
                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_iceandturbine " & _
                        "(PFW_EU_ICEANDTURBINEID, " & _
                        "emissionunitid, gsid, " & _
                        "ICETYPE, EPACERTIFICATION, " & _
                        "EPACERTIFICATIONOTHER, NUMPOWEROUTPUT, " & _
                        "NUMPOWEROUTPUTUNITS, NUMMAXPOWEROUTPUT, " & _
                        "NUMMAXPOWEROUTPUTUNITS, NUMMAXHOURSOPERATINGANNUAL, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_ICEANDTURBINEID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & ICETYPE & "', '" & Replace(Mid(EPACERTIFICATION, 1, 400), "'", "''") & "',  " & _
                        "'" & Replace(Mid(EPACERTIFICATIONOTHER, 1, 2000), "'", "''") & "',  '" & NUMPOWEROUTPUT & "', " & _
                        "'" & NUMPOWEROUTPUTUNITS & "',  '" & NUMMAXPOWEROUTPUT & "', " & _
                        "'" & NUMMAXPOWEROUTPUTUNITS & "',  '" & NUMMAXHOURSOPERATINGANNUAL & "', " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEULandfill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEULandfill.Click
        Try
            PopulatePFWEULandfill()
        Catch ex As Exception
          
        End Try
    End Sub
    Sub PopulatePFWEULandfill()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim oldID As String = ""
            Dim desc As String = ""
            Dim EUType As String = ""
            Dim NUMCONSTRUCTIONYEAR As String = ""
            Dim NUMYEARWASTEFIRSTRECEIVED As String = ""
            Dim DATCLOSUREDATE As String = ""
            Dim NUMDESIGNCAPACITY As String = ""
            Dim NUMTIER1RESULTS As String = ""
            Dim NUMTIER2RESULTS As String = ""
            Dim NUMTIER3RESULTS As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            Dim STRWASTEHANDLINGPERMIT As String = ""
            Dim STRORIGINALISSUEDATE As String = ""
            Dim NUMMAXDESIGNCAPACITY As String = ""

            Dim NUMYEARRECEIVED As String = ""
            Dim NUMTONSACCEPTED As String = ""
            Dim NUMESTIMATEDVALUE As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitLandfills.InitialConstructionModificationDate, " & _
            "tblEmissionUnitLandfills.DateFirstReceivedWaste, " & _
            "tblEmissionUnitLandfills.ClosureDate, " & _
            "tblEmissionUnitLandfills.TotalDesignCapacity, " & _
            "tblEmissionUnitLandfills.Tier1TestResult, " & _
            "tblEmissionUnitLandfills.Tier2TestResult, " & _
            "tblEmissionUnitLandfills.Tier3TestResult " & _
            "FROM tblEmissionUnitLandfills INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitLandfills.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitLandfills.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Internal Combustion Engines & Combustion Turbines" Then
                    EUType = "7"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("InitialConstructionModificationDate")) Then
                    NUMCONSTRUCTIONYEAR = "1776"
                Else
                    NUMCONSTRUCTIONYEAR = GATVdr.item("InitialConstructionModificationDate")
                End If

                If IsNumeric(NUMCONSTRUCTIONYEAR) And NUMCONSTRUCTIONYEAR.Length = 4 Then
                    NUMCONSTRUCTIONYEAR = NUMCONSTRUCTIONYEAR
                Else
                    If NUMCONSTRUCTIONYEAR.Length > 4 Then
                        NUMCONSTRUCTIONYEAR = Mid(NUMCONSTRUCTIONYEAR, NUMCONSTRUCTIONYEAR.Length - 3)
                        If IsNumeric(NUMCONSTRUCTIONYEAR) Then
                            NUMCONSTRUCTIONYEAR = NUMCONSTRUCTIONYEAR
                        Else
                            NUMCONSTRUCTIONYEAR = "1776"
                        End If
                    Else
                        NUMCONSTRUCTIONYEAR = "1776"
                    End If
                End If

                If IsDBNull(GATVdr.item("DateFirstReceivedWaste")) Then
                    NUMYEARWASTEFIRSTRECEIVED = NUMCONSTRUCTIONYEAR
                Else
                    NUMYEARWASTEFIRSTRECEIVED = GATVdr.item("DateFirstReceivedWaste")
                End If

                If IsNumeric(NUMYEARWASTEFIRSTRECEIVED) And NUMYEARWASTEFIRSTRECEIVED.Length = 4 Then
                    NUMYEARWASTEFIRSTRECEIVED = NUMYEARWASTEFIRSTRECEIVED
                Else
                    If NUMYEARWASTEFIRSTRECEIVED.Length > 4 Then
                        NUMYEARWASTEFIRSTRECEIVED = Mid(NUMYEARWASTEFIRSTRECEIVED, NUMYEARWASTEFIRSTRECEIVED.Length - 4)
                        If IsNumeric(NUMYEARWASTEFIRSTRECEIVED) Then
                            NUMYEARWASTEFIRSTRECEIVED = NUMYEARWASTEFIRSTRECEIVED
                        Else
                            NUMYEARWASTEFIRSTRECEIVED = NUMCONSTRUCTIONYEAR
                        End If

                    Else
                        NUMYEARWASTEFIRSTRECEIVED = NUMCONSTRUCTIONYEAR
                    End If
                End If
                If NUMYEARWASTEFIRSTRECEIVED = "" Then
                    NUMYEARWASTEFIRSTRECEIVED = "1776"
                End If

                If IsDBNull(GATVdr.item("ClosureDate")) Then
                    DATCLOSUREDATE = ""
                Else
                    DATCLOSUREDATE = GATVdr.item("ClosureDate")
                End If
                If IsNumeric(DATCLOSUREDATE) And DATCLOSUREDATE.Length = 4 Then
                    DATCLOSUREDATE = "01-Jan-" & DATCLOSUREDATE
                End If
                If IsDate(DATCLOSUREDATE) Then
                    DATCLOSUREDATE = Format(CDate(DATCLOSUREDATE), "dd-MMM-yyyy")
                Else
                    DATCLOSUREDATE = ""
                End If

                If IsDBNull(GATVdr.item("TotalDesignCapacity")) Then
                    NUMDESIGNCAPACITY = ""
                Else
                    NUMDESIGNCAPACITY = GATVdr.item("TotalDesignCapacity")
                End If
                If IsDBNull(GATVdr.item("Tier1TestResult")) Then
                    NUMTIER1RESULTS = ""
                Else
                    NUMTIER1RESULTS = GATVdr.item("Tier1TestResult")
                End If
                If IsDBNull(GATVdr.item("Tier2TestResult")) Then
                    NUMTIER2RESULTS = ""
                Else
                    NUMTIER2RESULTS = GATVdr.item("Tier2TestResult")
                End If
                If IsDBNull(GATVdr.item("Tier3TestResult")) Then
                    NUMTIER3RESULTS = ""
                Else
                    NUMTIER3RESULTS = GATVdr.item("Tier3TestResult")
                End If
                Manufacturer = ""
                ModelNumber = ""
                Installation = ""
                DateManufactured = ""
                InstallationDate = ""

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_Landfill " & _
                        "(PFW_EU_LandfillID, " & _
                        "emissionunitid, gsid, " & _
                        "NUMCONSTRUCTIONYEAR, NUMYEARWASTEFIRSTRECEIVED, " & _
                        "DATCLOSUREDATE, NUMDESIGNCAPACITY, " & _
                        "NUMTIER1RESULTS, NUMTIER2RESULTS, " & _
                        "NUMTIER3RESULTS, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_ICEANDTURBINEID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & NUMCONSTRUCTIONYEAR & "', '" & Replace(Mid(NUMYEARWASTEFIRSTRECEIVED, 1, 400), "'", "''") & "',  " & _
                        "'" & Replace(Mid(DATCLOSUREDATE, 1, 2000), "'", "''") & "',  '" & NUMDESIGNCAPACITY & "', " & _
                        "'" & NUMTIER1RESULTS & "',  '" & NUMTIER2RESULTS & "', " & _
                        "'" & NUMTIER3RESULTS & "',  " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "select " & _
                        "SolidWasteHandlingPermitNumber, " & _
                        "OriginalIssueOrAmendmentDate, " & _
                        "MaximumPermittedDesignCapacity " & _
                        "from tblEmissionUnitLandfillPermits_sub " & _
                        "where EquipmentID = " & oldID & " " & _
                        "and ProjectIdentifier = " & ProjectId & " "

                        gatvcmd2 = New OleDbCommand(SQL, GATVConn)
                        If GATVConn.state = ConnectionState.Closed Then
                            GATVConn.open()
                        End If
                        GATVdr2 = GATVcmd2.ExecuteReader
                        While GATVdr2.Read
                            If IsDBNull(GATVdr2.item("SolidWasteHandlingPermitNumber")) Then
                                STRWASTEHANDLINGPERMIT = ""
                            Else
                                STRWASTEHANDLINGPERMIT = GATVdr2.item("SolidWasteHandlingPermitNumber")
                            End If
                            If IsDBNull(GATVdr2.item("OriginalIssueOrAmendmentDate")) Then
                                STRORIGINALISSUEDATE = ""
                            Else
                                STRORIGINALISSUEDATE = GATVdr2.item("OriginalIssueOrAmendmentDate")
                            End If
                            If IsDBNull(GATVdr2.item("MaximumPermittedDesignCapacity")) Then
                                NUMMAXDESIGNCAPACITY = ""
                            Else
                                NUMMAXDESIGNCAPACITY = GATVdr2.item("MaximumPermittedDesignCapacity")
                            End If

                            SQL = "Insert into AIRTVAPPLICATION.PFW_EUS_LANDFILLPERMIT " & _
                            "(PFW_EUS_LandfillPermitID, " & _
                            "EmissionUnitID, GSID, " & _
                            "STRWASTEHANDLINGPERMIT, STRORIGINALISSUEDATE, " & _
                            "NUMMAXDESIGNCAPACITY, " & _
                            "Comments, Active) " & _
                            "Select " & _
                            "airtvProject.SEQ_GAP_EUS_LANDFILLPERMITID.nextval,  " & _
                            "'" & temp & "', '" & GSID & "', " & _
                            "'" & Replace(Mid(STRWASTEHANDLINGPERMIT, 1, 400), "'", "''") & "', " & _
                            "'" & Replace(Mid(STRORIGINALISSUEDATE, 1, 400), "'", "''") & "',  " & _
                            "'" & NUMMAXDESIGNCAPACITY & "',  " & _
                            "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                            "from dual " & _
                            "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                            "where EMISSIONUNITID = " & temp & ") "

                            cmd2 = New OracleCommand(SQL, ConnTVApplication)
                            If ConnTVApplication.State = ConnectionState.Closed Then
                                ConnTVApplication.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            dr2.Close()
                        End While


                        SQL = "select " & _
                       "Year, " & _
                       "TonsSolidWasteAccepted, " & _
                       "IsEstimate " & _
                       "from tblEmissionUnitLandfillAnnualDepsoit_Sub " & _
                       "where EquipmentID = " & oldID & " " & _
                       "and ProjectIdentifier = " & ProjectId & " "


                        SQL = "SELECT " & _
                        "tblEmissionUnitLandfillAnnualDepsoit_Sub.Year, " & _
                        "tblEmissionUnitLandfillAnnualDepsoit_Sub.TonsSolidWasteAccepted, " & _
                        "tblEmissionUnitLandfillAnnualDepsoit_Sub.IsEstimate " & _
                        "FROM tblEmissionUnitLandfillAnnualDepsoit_Sub " & _
                        "where EquipmentID = " & oldID & " " & _
                        "and ProjectIdentifier = " & ProjectId & " "

                        GATVcmd2 = New OleDbCommand(SQL, GATVConn)
                        If GATVConn.state = ConnectionState.Closed Then
                            GATVConn.open()
                        End If
                        GATVdr2 = GATVcmd2.ExecuteReader
                        While GATVdr2.Read
                            If IsDBNull(GATVdr2.item("Year")) Then
                                NUMYEARRECEIVED = ""
                            Else
                                NUMYEARRECEIVED = GATVdr2.item("Year")
                            End If
                            If IsDBNull(GATVdr2.item("TonsSolidWasteAccepted")) Then
                                NUMTONSACCEPTED = ""
                            Else
                                NUMTONSACCEPTED = GATVdr2.item("TonsSolidWasteAccepted")
                            End If
                            If IsDBNull(GATVdr2.item("IsEstimate")) Then
                                NUMESTIMATEDVALUE = "0"
                            Else
                                NUMESTIMATEDVALUE = GATVdr2.item("IsEstimate")
                            End If

                            If NUMTONSACCEPTED = "" Then
                                NUMTONSACCEPTED = "0"
                            End If
                            If NUMESTIMATEDVALUE = True Then
                                NUMESTIMATEDVALUE = "1"
                            Else
                                NUMESTIMATEDVALUE = "0"
                            End If

                            If NUMYEARRECEIVED = "" Then
                            Else
                                SQL = "Insert into AIRTVAPPLICATION.PFW_EUS_LANDFILLDeposit " & _
                                "(PFW_EUS_LandfillDepositID, " & _
                                "EmissionUnitID, GSID, " & _
                                "NUMYEARRECEIVED, NUMTONSACCEPTED, " & _
                                "NUMESTIMATEDVALUE, " & _
                                "Comments, Active) " & _
                                "Select " & _
                                "airtvProject.SEQ_GAP_EUS_LANDFILLDepositID.nextval,  " & _
                                "'" & temp & "', '" & GSID & "', " & _
                                "'" & Replace(NUMYEARRECEIVED, " ", "") & "', " & _
                                "'" & NUMTONSACCEPTED & "',  " & _
                                "'" & NUMESTIMATEDVALUE & "',  " & _
                                "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                                "from dual " & _
                                "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                                "where EMISSIONUNITID = " & temp & ") "

                                cmd2 = New OracleCommand(SQL, ConnTVApplication)
                                If ConnTVApplication.State = ConnectionState.Closed Then
                                    ConnTVApplication.Open()
                                End If
                                dr2 = cmd2.ExecuteReader
                                dr2.Close()
                            End If
                        End While
                        SaveFuelBurning(oldID, GSID, temp)
                        LandFillSub(oldID, GSID, temp, "LandFill")

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEULiquidStorage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEULiquidStorage.Click
        Try
            PopulatePFWEULiquidStorage()
        Catch ex As Exception
           
        End Try
    End Sub
    Sub PopulatePFWEULiquidStorage()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim NUMABOVEGROUND As String = ""
            Dim LIQUIDSTORAGETANK As String = ""
            Dim STRTANKTYPEOTHER As String = ""
            Dim NUMSTORAGECAPACITY As String = ""
            Dim STRINSTALLATIONDATE As String = ""
            Dim STRROOFCOLOR As String = ""
            Dim STRSHELLCOLOR As String = ""
            Dim NUMPAINTCONDITION As String = ""
            Dim NUMCONSERVATIONVENT As String = ""
            Dim NUMMAXTRUEVAPORPRESSURE As String = ""
            Dim NUMMAXTRUEVAPORPRESSURETEMP As String = ""
            Dim NUMSTORAGETEMP As String = ""
            Dim STRFILLUPMETHOD As String = ""
            Dim LIQUIDSTORAGEROOFTYPE As String = ""
            Dim STRROOFTYPEOTHER As String = ""
            Dim LIQUIDSTORAGESEALTYPE As String = ""
            Dim STRSEALTYPEOTHER As String = ""
            Dim SECONDARYSEALTYPE As String = ""
            Dim STRSECONDARYSEALTYPE As String = ""
            Dim NUMPRESSURESETTING As String = ""
            Dim NUMVACUUMSETTING As String = ""
            Dim NUMHEATEDTANK As String = ""
            Dim NUMSUBMERGEDFILLPIPE As String = ""
            Dim STRMATERIALSINTANK As String = ""
            Dim NUMTOTALVAPORPRESSURE As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitLiquidStorageVessels.StorageCapacity, " & _
            "tblEmissionUnitLiquidStorageVessels.StorageCapacityUnits, " & _
            "tblEmissionUnitLiquidStorageVessels.AboveGroundTank, " & _
            "tblEmissionUnitLiquidStorageVessels.TankType, " & _
            "tblEmissionUnitLiquidStorageVessels.DescriptionOfOther, " & _
            "tblEmissionUnitLiquidStorageVessels.ColorOfTankRoof, " & _
            "tblEmissionUnitLiquidStorageVessels.ColorOfTankShell, " & _
            "tblEmissionUnitLiquidStorageVessels.PaintCondition, " & _
            "tblEmissionUnitLiquidStorageVessels.TankHeated, " & _
            "tblEmissionUnitLiquidStorageVessels.SubmergedFillPipe, " & _
            "tblEmissionUnitLiquidStorageVessels.Pressure_VacuumConservationVent, " & _
            "tblEmissionUnitLiquidStorageVessels.PressureSetting, " & _
            "tblEmissionUnitLiquidStorageVessels.VacuumSetting, " & _
            "tblEmissionUnitLiquidStorageVessels.MaterialStoredInTank, " & _
            "tblEmissionUnitLiquidStorageVessels.MaterialTotalVaporPresure, " & _
            "tblEmissionUnitLiquidStorageVessels.NonstandardMixture, " & _
            "tblEmissionUnitLiquidStorageVessels.InstallationDate " & _
            "FROM tblEmissionUnitLiquidStorageVessels INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitLiquidStorageVessels.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitLiquidStorageVessels.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If

                If EUType = "Non-Reactive Bulk Mixing" Then
                    EUType = "10"
                Else
                    EUType = "19"
                End If
                NUMABOVEGROUND = ""
                LIQUIDSTORAGETANK = ""
                STRTANKTYPEOTHER = ""
                NUMSTORAGECAPACITY = ""
                STRINSTALLATIONDATE = ""
                STRROOFCOLOR = ""
                STRSHELLCOLOR = ""
                NUMPAINTCONDITION = ""
                NUMCONSERVATIONVENT = ""
                NUMMAXTRUEVAPORPRESSURE = ""
                NUMMAXTRUEVAPORPRESSURETEMP = ""
                NUMSTORAGETEMP = ""
                STRFILLUPMETHOD = ""
                LIQUIDSTORAGEROOFTYPE = ""
                STRROOFTYPEOTHER = ""
                LIQUIDSTORAGESEALTYPE = ""
                STRSEALTYPEOTHER = ""
                SECONDARYSEALTYPE = ""
                STRSECONDARYSEALTYPE = ""
                NUMPRESSURESETTING = ""
                NUMVACUUMSETTING = ""
                NUMHEATEDTANK = ""
                NUMSUBMERGEDFILLPIPE = ""
                STRMATERIALSINTANK = ""
                NUMTOTALVAPORPRESSURE = ""

                If IsDBNull(GATVdr.item("AboveGroundTank")) Then
                    NUMABOVEGROUND = ""
                Else
                    NUMABOVEGROUND = GATVdr.item("AboveGroundTank")
                End If
                If NUMABOVEGROUND = True Then
                    NUMABOVEGROUND = 1
                Else
                    NUMABOVEGROUND = 0
                End If

                If IsDBNull(GATVdr.item("TankType")) Then
                    LIQUIDSTORAGETANK = ""
                Else
                    LIQUIDSTORAGETANK = GATVdr.item("TankType")
                End If
                If IsDBNull(GATVdr.item("DescriptionOfOther")) Then
                    STRTANKTYPEOTHER = ""
                Else
                    STRTANKTYPEOTHER = GATVdr.item("DescriptionOfOther")
                End If
                If IsDBNull(GATVdr.item("StorageCapacity")) Then
                    NUMSTORAGECAPACITY = ""
                Else
                    NUMSTORAGECAPACITY = GATVdr.item("StorageCapacity")
                End If
                If IsDBNull(GATVdr.item("Installationdate")) Then
                    STRINSTALLATIONDATE = ""
                Else
                    STRINSTALLATIONDATE = GATVdr.item("Installationdate")
                End If
                If IsDBNull(GATVdr.item("ColorOfTankRoof")) Then
                    STRROOFCOLOR = ""
                Else
                    STRROOFCOLOR = GATVdr.item("ColorOfTankRoof")
                End If
                If IsDBNull(GATVdr.item("ColorOfTankShell")) Then
                    STRSHELLCOLOR = ""
                Else
                    STRSHELLCOLOR = GATVdr.item("ColorOfTankShell")
                End If
                If IsDBNull(GATVdr.item("PaintCondition")) Then
                    NUMPAINTCONDITION = ""
                Else
                    NUMPAINTCONDITION = GATVdr.item("PaintCondition")
                End If

                If IsDBNull(GATVdr.item("Pressure_VacuumConservationVent")) Then
                    NUMCONSERVATIONVENT = ""
                Else
                    NUMCONSERVATIONVENT = GATVdr.item("Pressure_VacuumConservationVent")
                End If
                If NUMCONSERVATIONVENT = True Then
                    NUMCONSERVATIONVENT = 1
                Else
                    NUMCONSERVATIONVENT = 0
                End If
                If IsDBNull(GATVdr.item("PressureSetting")) Then
                    NUMMAXTRUEVAPORPRESSURE = ""
                Else
                    NUMMAXTRUEVAPORPRESSURE = GATVdr.item("PressureSetting")
                End If
                If IsDBNull(GATVdr.item("MaterialStoredInTank")) Then
                    STRMATERIALSINTANK = ""
                Else
                    STRMATERIALSINTANK = GATVdr.item("MaterialStoredInTank")
                End If
                If IsDBNull(GATVdr.item("MaterialTotalVaporPresure")) Then
                    NUMTOTALVAPORPRESSURE = ""
                Else
                    NUMTOTALVAPORPRESSURE = GATVdr.item("MaterialTotalVaporPresure")
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_LiquidStorage " & _
                        "(PFW_EU_LiquidStorageID, " & _
                        "emissionunitid, gsid, " & _
                        "NUMABOVEGROUND, LIQUIDSTORAGETANK, " & _
                        "STRTANKTYPEOTHER, NUMSTORAGECAPACITY, " & _
                        "STRINSTALLATIONDATE, STRROOFCOLOR, " & _
                        "STRSHELLCOLOR, NUMPAINTCONDITION, " & _
                        "NUMCONSERVATIONVENT, NUMMAXTRUEVAPORPRESSURE, " & _
                        "NUMMAXTRUEVAPORPRESSURETEMP, NUMSTORAGETEMP, " & _
                        "STRFILLUPMETHOD, LIQUIDSTORAGEROOFTYPE, " & _
                        "STRROOFTYPEOTHER, LIQUIDSTORAGESEALTYPE, " & _
                        "STRSEALTYPEOTHER, SECONDARYSEALTYPE, " & _
                        "STRSECONDARYSEALTYPE, NUMPRESSURESETTING, " & _
                        "NUMVACUUMSETTING, NUMHEATEDTANK, " & _
                        "NUMSUBMERGEDFILLPIPE, STRMATERIALSINTANK, " & _
                        "NUMTOTALVAPORPRESSURE, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_LIQUIDSTORAGETANKID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                         "'" & NUMABOVEGROUND & "', '" & LIQUIDSTORAGETANK & "', " & _
                        "'" & Replace(Mid(STRTANKTYPEOTHER, 1, 400), "'", "''") & "', '" & NUMSTORAGECAPACITY & "', " & _
                        "'" & Replace(Mid(STRINSTALLATIONDATE, 1, 400), "'", "''") & "', '" & STRROOFCOLOR & "', " & _
                        "'" & STRSHELLCOLOR & "', '" & NUMPAINTCONDITION & "', " & _
                        "'" & NUMCONSERVATIONVENT & "', '" & NUMMAXTRUEVAPORPRESSURE & "', " & _
                        "'" & NUMMAXTRUEVAPORPRESSURETEMP & "', '" & NUMSTORAGETEMP & "', " & _
                        "'" & STRFILLUPMETHOD & "', '" & LIQUIDSTORAGEROOFTYPE & "', " & _
                        "'" & STRROOFTYPEOTHER & "', '" & LIQUIDSTORAGESEALTYPE & "', " & _
                        "'" & STRSEALTYPEOTHER & "', '" & SECONDARYSEALTYPE & "', " & _
                        "'" & STRSECONDARYSEALTYPE & "', '" & NUMPRESSURESETTING & "', " & _
                        "'" & NUMVACUUMSETTING & "', '" & NUMHEATEDTANK & "', " & _
                        "'" & NUMSUBMERGEDFILLPIPE & "', '" & STRMATERIALSINTANK & "', " & _
                        "'" & NUMTOTALVAPORPRESSURE & "', " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)
                        SaveMaterials(oldID, GSID, temp, "Liquid Storage")

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()


        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnPFWEUMisc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWEUMisc.Click
        Try
            PopulatePFWEUMisc()

        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFWEUMisc()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitMiscellaneous.Manufacturer, " & _
            "tblEmissionUnitMiscellaneous.ModelNumber, " & _
            "tblEmissionUnitMiscellaneous.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitMiscellaneous.InstallationDate " & _
            "FROM tblEmissionUnitMiscellaneous INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitMiscellaneous.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitMiscellaneous.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If

                If EUType = "Miscellaneous" Then
                    EUType = "19"
                Else
                    EUType = "19"
                End If
                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)
                        SaveMaterials(oldID, GSID, temp, "Miscellaneous")

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()


        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUNRBulkMixing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUNRBulkMixing.Click
        Try
            PopulatePFWEUNRBulkMixing()
        Catch ex As Exception

        End Try
    End Sub
    Sub PopulatePFWEUNRBulkMixing()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim OperatingTemp As String = ""
            Dim CoveredDurningOp As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitNonReactiveBulkMixing.OperatingTemperatureF, " & _
            "tblEmissionUnitNonReactiveBulkMixing.CoveredDuringOperation, " & _
            "tblEmissionUnitNonReactiveBulkMixing.Manufacturer, " & _
            "tblEmissionUnitNonReactiveBulkMixing.ModelNumber, " & _
            "tblEmissionUnitNonReactiveBulkMixing.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitNonReactiveBulkMixing.InstallationDate " & _
            "FROM tblEmissionUnitNonReactiveBulkMixing INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitNonReactiveBulkMixing.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitNonReactiveBulkMixing.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If

                If EUType = "Non-Reactive Bulk Mixing" Then
                    EUType = "10"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("OperatingTemperatureF")) Then
                    OperatingTemp = ""
                Else
                    OperatingTemp = GATVdr.item("OperatingTemperatureF")
                End If
                If IsDBNull(GATVdr.item("CoveredDuringOperation")) Then
                    CoveredDurningOp = ""
                Else
                    CoveredDurningOp = GATVdr.item("CoveredDuringOperation")
                End If
                Select Case CoveredDurningOp
                    Case True
                        CoveredDurningOp = 1
                    Case False
                        CoveredDurningOp = 0
                    Case Else
                        CoveredDurningOp = 0
                End Select

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_NRBulkMixing " & _
                        "(PFW_EU_NRBulkMixingID, " & _
                        "emissionunitid, gsid, " & _
                        "NUMOPERATINGTEMP, NUMCOVEREDDURINGOP, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_NRBULKMIXINGID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & OperatingTemp & "', '" & CoveredDurningOp & "',  " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)
                        SaveMaterials(oldID, GSID, temp, "NonReactive")

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUOven_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUOven.Click
        Try
            PopulatePFWEUOven()
        Catch ex As Exception
           
        End Try
    End Sub
    Sub PopulatePFWEUOven()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""
            Dim OvenType As String = ""
            Dim OvenComment As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.TypeofUnit,  " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.SpecificType,  " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.DescriptionofOther,  " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.Manufacturer, " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.ModelNumber, " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitDryersCalcinersKilnsOvens.InstallationDate " & _
            "FROM tblEmissionUnitDryersCalcinersKilnsOvens INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitDryersCalcinersKilnsOvens.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitDryersCalcinersKilnsOvens.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) " & _
            "where TypeOfUnit = 4 "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Dryers, Calciners, Kilns & Ovens" Then
                    EUType = "4"
                Else
                    EUType = "19"
                End If
                If IsDBNull(GATVdr.item("TypeofUnit")) Then
                    OvenType = ""
                Else
                    OvenType = GATVdr.item("TypeofUnit")
                End If
              
                If IsDBNull(GATVdr.item("DescriptionofOther")) Then
                    OvenComment = ""
                Else
                    OvenComment = GATVdr.item("DescriptionofOther")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_Oven " & _
                        "(pfw_eu_OvenID, " & _
                        "emissionunitid, gsid, " & _
                        "OvenType, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_OvenID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(OvenType, "'", "''") & "',  " & _
                        "'" & Replace(Mid(OvenComment, 1, 2000), "'", "''") & "', " & _
                        "'1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)
                        SaveMaterials(oldID, GSID, temp, "Drying Equipment")

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUPrinting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUPrinting.Click
        Try
            PopulatePFWEUPrinting()
        Catch ex As Exception
           
        End Try
    End Sub
    Sub PopulatePFWEUPrinting()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim PrintingType As String = ""
            Dim NumberOfUnits As String = ""
            Dim Substrate As String = ""
            Dim TotalHAPPotential As String = ""

            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitPrintingOperations.HasFlexographic, " & _
            "tblEmissionUnitPrintingOperations.NumFlexographic, " & _
            "tblEmissionUnitPrintingOperations.SubstrateFlexographic, " & _
            "tblEmissionUnitPrintingOperations.VOCPTEFlexographic, " & _
            "tblEmissionUnitPrintingOperations.HasPackRoto, " & _
            "tblEmissionUnitPrintingOperations.NumPackRoto, " & _
            "tblEmissionUnitPrintingOperations.SubstratePackRoto, " & _
            "tblEmissionUnitPrintingOperations.VOCPTEPackRoto, " & _
            "tblEmissionUnitPrintingOperations.HasPubRoto, " & _
            "tblEmissionUnitPrintingOperations.NumPubRoto, " & _
            "tblEmissionUnitPrintingOperations.SubstratePubRoto, " & _
            "tblEmissionUnitPrintingOperations.VOCPTEPubRoto, " & _
            "tblEmissionUnitPrintingOperations.HasOther, " & _
            "tblEmissionUnitPrintingOperations.NumOther, " & _
            "tblEmissionUnitPrintingOperations.DescriptionOther, " & _
            "tblEmissionUnitPrintingOperations.VOCPTEOther, " & _
            "tblEmissionUnitPrintingOperations.HasScreen, " & _
            "tblEmissionUnitPrintingOperations.NumScreen, " & _
            "tblEmissionUnitPrintingOperations.SubstrateScreen, " & _
            "tblEmissionUnitPrintingOperations.VOCPTEScreen, " & _
            "tblEmissionUnitPrintingOperations.HasLithOffHeat, " & _
            "tblEmissionUnitPrintingOperations.NumLithOffHeat, " & _
            "tblEmissionUnitPrintingOperations.SubstrateLithOffHeat, " & _
            "tblEmissionUnitPrintingOperations.VOCPTELithOffHeat, " & _
            "tblEmissionUnitPrintingOperations.HasLithOffNonHeat, " & _
            "tblEmissionUnitPrintingOperations.NumLithOffNonHeat, " & _
            "tblEmissionUnitPrintingOperations.SubstrateLithOffNonHeat, " & _
            "tblEmissionUnitPrintingOperations.VOCPTELithOffNonHeat, " & _
            "tblEmissionUnitPrintingOperations.Manufacturer, " & _
            "tblEmissionUnitPrintingOperations.ModelNumber, " & _
            "tblEmissionUnitPrintingOperations.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitPrintingOperations.InstallationDate " & _
            "FROM tblEmissionUnitPrintingOperations INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitPrintingOperations.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitPrintingOperations.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If

                If EUType = "Printing Operations" Then
                    EUType = "13"
                Else
                    EUType = "19"
                End If



                PrintingType = ""
                If IsDBNull(GATVdr.item("HasFlexographic")) Then
                    PrintingType = ""
                Else
                    PrintingType = GATVdr.item("HasFlexographic")
                End If
                If PrintingType = True Then
                    PrintingType = "1"
                End If
                If PrintingType = "1" Then
                Else
                    If IsDBNull(GATVdr.item("HasPackRoto")) Then
                        PrintingType = ""
                    Else
                        PrintingType = GATVdr.item("HasPackRoto")
                    End If
                    If PrintingType = True Then
                        PrintingType = "2"
                    End If
                    If PrintingType = "2" Then
                    Else
                        If IsDBNull(GATVdr.item("HasPubRoto")) Then
                            PrintingType = ""
                        Else
                            PrintingType = GATVdr.item("HasPubRoto")
                        End If
                        If PrintingType = True Then
                            PrintingType = "3"
                        End If
                        If PrintingType = "3" Then
                        Else
                            If IsDBNull(GATVdr.item("HasScreen")) Then
                                PrintingType = ""
                            Else
                                PrintingType = GATVdr.item("HasScreen")
                            End If
                            If PrintingType = True Then
                                PrintingType = "4"
                            End If
                            If PrintingType = "4" Then
                            Else
                                If IsDBNull(GATVdr.item("HasLithOffHeat")) Then
                                    PrintingType = ""
                                Else
                                    PrintingType = GATVdr.item("HasLithOffHeat")
                                End If
                                If PrintingType = True Then
                                    PrintingType = "5"
                                End If
                                If PrintingType = "5" Then
                                Else
                                    If IsDBNull(GATVdr.item("HasLithOffNonHeat")) Then
                                        PrintingType = ""
                                    Else
                                        PrintingType = GATVdr.item("HasLithOffNonHeat")
                                    End If
                                    If PrintingType = True Then
                                        PrintingType = "6"
                                    End If
                                    If PrintingType = "6" Then
                                    Else
                                        If IsDBNull(GATVdr.item("HasOther")) Then
                                            PrintingType = ""
                                        Else
                                            PrintingType = GATVdr.item("HasOther")
                                        End If
                                        If PrintingType = True Then
                                            PrintingType = "7"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                Select Case PrintingType
                    Case "1"
                        If IsDBNull(GATVdr.item("NumFlexographic")) Then
                            NumberOfUnits = ""
                        Else
                            NumberOfUnits = GATVdr.item("NumFlexographic")
                        End If
                        If IsDBNull(GATVdr.item("SubstrateFlexographic")) Then
                            Substrate = ""
                        Else
                            Substrate = GATVdr.item("SubstrateFlexographic")
                        End If
                        If IsDBNull(GATVdr.item("VOCPTEFlexographic")) Then
                            TotalHAPPotential = ""
                        Else
                            TotalHAPPotential = GATVdr.item("VOCPTEFlexographic")
                        End If

                    Case "2"
                        If IsDBNull(GATVdr.item("NumPackRoto")) Then
                            NumberOfUnits = ""
                        Else
                            NumberOfUnits = GATVdr.item("NumPackRoto")
                        End If
                        If IsDBNull(GATVdr.item("SubstratePackRoto")) Then
                            Substrate = ""
                        Else
                            Substrate = GATVdr.item("SubstratePackRoto")
                        End If
                        If IsDBNull(GATVdr.item("VOCPTEPackRoto")) Then
                            TotalHAPPotential = ""
                        Else
                            TotalHAPPotential = GATVdr.item("VOCPTEPackRoto")
                        End If
                    Case "3"
                        If IsDBNull(GATVdr.item("NumPubRoto")) Then
                            NumberOfUnits = ""
                        Else
                            NumberOfUnits = GATVdr.item("NumPubRoto")
                        End If
                        If IsDBNull(GATVdr.item("SubstratePubRoto")) Then
                            Substrate = ""
                        Else
                            Substrate = GATVdr.item("SubstratePubRoto")
                        End If
                        If IsDBNull(GATVdr.item("VOCPTEPubRoto")) Then
                            TotalHAPPotential = ""
                        Else
                            TotalHAPPotential = GATVdr.item("VOCPTEPubRoto")
                        End If
                    Case "4"
                        If IsDBNull(GATVdr.item("NumScreen")) Then
                            NumberOfUnits = ""
                        Else
                            NumberOfUnits = GATVdr.item("NumScreen")
                        End If
                        If IsDBNull(GATVdr.item("SubstrateScreen")) Then
                            Substrate = ""
                        Else
                            Substrate = GATVdr.item("SubstrateScreen")
                        End If
                        If IsDBNull(GATVdr.item("VOCPTEScreen")) Then
                            TotalHAPPotential = ""
                        Else
                            TotalHAPPotential = GATVdr.item("VOCPTEScreen")
                        End If
                    Case "5"
                        If IsDBNull(GATVdr.item("NumLithOffHeat")) Then
                            NumberOfUnits = ""
                        Else
                            NumberOfUnits = GATVdr.item("NumLithOffHeat")
                        End If
                        If IsDBNull(GATVdr.item("SubstrateLithOffHeat")) Then
                            Substrate = ""
                        Else
                            Substrate = GATVdr.item("SubstrateLithOffHeat")
                        End If
                        If IsDBNull(GATVdr.item("VOCPTELithOffHeat")) Then
                            TotalHAPPotential = ""
                        Else
                            TotalHAPPotential = GATVdr.item("VOCPTELithOffHeat")
                        End If
                    Case "6"
                        If IsDBNull(GATVdr.item("NumLithOffNonHeat")) Then
                            NumberOfUnits = ""
                        Else
                            NumberOfUnits = GATVdr.item("NumLithOffNonHeat")
                        End If
                        If IsDBNull(GATVdr.item("SubstrateLithOffNonHeat")) Then
                            Substrate = ""
                        Else
                            Substrate = GATVdr.item("SubstrateLithOffNonHeat")
                        End If
                        If IsDBNull(GATVdr.item("VOCPTELithOffNonHeat")) Then
                            TotalHAPPotential = ""
                        Else
                            TotalHAPPotential = GATVdr.item("VOCPTELithOffNonHeat")
                        End If
                    Case Else
                        If IsDBNull(GATVdr.item("NumOther")) Then
                            NumberOfUnits = ""
                        Else
                            NumberOfUnits = GATVdr.item("NumOther")
                        End If
                        If IsDBNull(GATVdr.item("DescriptionOther")) Then
                            Substrate = ""
                        Else
                            Substrate = GATVdr.item("DescriptionOther")
                        End If
                        If IsDBNull(GATVdr.item("VOCPTEOther")) Then
                            TotalHAPPotential = ""
                        Else
                            TotalHAPPotential = GATVdr.item("VOCPTEOther")
                        End If
                        PrintingType = "7"
                End Select

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_Printing " & _
                      "(PFW_EU_PrintingID, " & _
                      "emissionunitid, gsid, " & _
                      "PrintingType, numNumberofUnits, " & _
                      "strSubstrate, numTotalHAPPotential, " & _
                      "comments, Active) " & _
                      "Select " & _
                      "airtvProject.SEQ_GAP_EU_PrintingID.nextval,  " & _
                      "'" & temp & "', '" & GSID & "', " & _
                      "'" & PrintingType & "', '" & NumberOfUnits & "', " & _
                      "'" & Replace(Mid(Substrate, 1, 400), "'", "''") & "', '" & TotalHAPPotential & "', " & _
                      "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                      "from dual " & _
                      "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                      "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUReactorVessel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUReactorVessel.Click
        Try
            PopulatePFWEUReactorVessel()
        Catch ex As Exception
            
        End Try
    End Sub
    Sub PopulatePFWEUReactorVessel()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim REACTORVESSELTYPE As String = ""
            Dim STRREACTORTYPEOTHER As String = ""
            Dim NUMCATALYSTUSED As String = ""
            Dim STRCATALYSTDESC As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitReactorVessel.ReactorType, " & _
            "tblEmissionUnitReactorVessel.DescReatorTypeOther, " & _
            "tblEmissionUnitReactorVessel.UseCatalyst, " & _
            "tblEmissionUnitReactorVessel.CatalystDesc, " & _
            "tblEmissionUnitReactorVessel.Manufacturer, " & _
            "tblEmissionUnitReactorVessel.ModelNumber, " & _
            "tblEmissionUnitReactorVessel.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitReactorVessel.InstallationDate " & _
            "FROM tblEmissionUnitReactorVessel INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitReactorVessel.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitReactorVessel.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If

                If EUType = "Reactor Vessel" Then
                    EUType = "14"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("ReactorType")) Then
                    REACTORVESSELTYPE = ""
                Else
                    REACTORVESSELTYPE = GATVdr.item("ReactorType")
                End If
                Select Case REACTORVESSELTYPE
                    Case "1"
                        REACTORVESSELTYPE = "1"
                    Case "2"
                        REACTORVESSELTYPE = "2"
                    Case "3"
                        REACTORVESSELTYPE = "3"
                    Case "4"
                        REACTORVESSELTYPE = "4"
                    Case "5"
                        REACTORVESSELTYPE = "5"
                    Case "6"
                        REACTORVESSELTYPE = "6"
                    Case "7"
                        REACTORVESSELTYPE = "7"
                    Case Else
                        REACTORVESSELTYPE = "7"
                End Select

                If IsDBNull(GATVdr.item("DescReatorTypeOther")) Then
                    STRREACTORTYPEOTHER = ""
                Else
                    STRREACTORTYPEOTHER = GATVdr.item("DescReatorTypeOther")
                End If
                If IsDBNull(GATVdr.item("UseCatalyst")) Then
                    NUMCATALYSTUSED = ""
                Else
                    NUMCATALYSTUSED = GATVdr.item("UseCatalyst")
                End If
                If NUMCATALYSTUSED = True Then
                    NUMCATALYSTUSED = 1
                Else
                    NUMCATALYSTUSED = 0
                End If
                If IsDBNull(GATVdr.item("CatalystDesc")) Then
                    STRCATALYSTDESC = ""
                Else
                    STRCATALYSTDESC = GATVdr.item("CatalystDesc")
                End If
                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_ReactorVessel " & _
                        "(PFW_EU_ReactorVesselID, " & _
                        "emissionunitid, gsid, " & _
                        "REACTORVESSELTYPE, STRREACTORTYPEOTHER, " & _
                        "NUMCATALYSTUSED, STRCATALYSTDESC, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_ReactorVesselID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & REACTORVESSELTYPE & "', '" & Replace(Mid(STRREACTORTYPEOTHER, 1, 400), "'", "''") & "', " & _
                        "'" & NUMCATALYSTUSED & "', '" & Replace(Mid(STRCATALYSTDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)
                        SaveMaterials(oldID, GSID, temp, "ReactorVessel")

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()


        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUSeparationProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUSeparationProcess.Click
        Try
            PopulatePFWEUSeparationProcess()
        Catch ex As Exception
            
        End Try
    End Sub
    Sub PopulatePFWEUSeparationProcess()
        Try

            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim SEPARATIONPROCESSTYPE As String = ""
            Dim STRSEPARATORTYPEOTHER As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""


            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitSeparationProcess.SeparatorType, " & _
            "tblEmissionUnitSeparationProcess.DescSepTypeOther, " & _
                "tblEmissionUnitSeparationProcess.Manufacturer, " & _
                "tblEmissionUnitSeparationProcess.ModelNumber, " & _
                "tblEmissionUnitSeparationProcess.DateManufactured_Reconstructed, " & _
                "tblEmissionUnitSeparationProcess.InstallationDate " & _
                "FROM tblEmissionUnitSeparationProcess INNER JOIN tblEmissionUnitMaster ON " & _
                "(tblEmissionUnitSeparationProcess.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
                "(tblEmissionUnitSeparationProcess.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If

                If EUType = "Separation Process" Then
                    EUType = "15"
                Else
                    EUType = "19"
                End If
                If IsDBNull(GATVdr.item("SeparatorType")) Then
                    SEPARATIONPROCESSTYPE = ""
                Else
                    SEPARATIONPROCESSTYPE = GATVdr.item("SeparatorType")
                End If
                If IsDBNull(GATVdr.item("DescSepTypeOther")) Then
                    STRSEPARATORTYPEOTHER = ""
                Else
                    STRSEPARATORTYPEOTHER = GATVdr.item("DescSepTypeOther")
                End If
                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_SeparationProcess " & _
                     "(PFW_EU_SeparationProcessID, " & _
                     "emissionunitid, gsid, " & _
                     "SEPARATIONPROCESSTYPE, STRSEPARATORTYPEOTHER, " & _
                     "comments, Active) " & _
                     "Select " & _
                     "airtvProject.SEQ_GAP_EU_SeparationProcessID.nextval,  " & _
                     "'" & temp & "', '" & GSID & "', " & _
                     "'" & SEPARATIONPROCESSTYPE & "', '" & Replace(Mid(STRSEPARATORTYPEOTHER, 1, 400), "'", "''") & "', " & _
                     "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                     "from dual " & _
                     "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                     "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)
                        SaveMaterials(oldID, GSID, temp, "Separation")

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()




        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUSolidWaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUSolidWaste.Click
        Try
            PopulatePFWEUSolidWaste()
        Catch ex As Exception
           
        End Try
    End Sub
    Sub PopulatePFWEUSolidWaste()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim STRHEARTHGRATE As String = ""
            Dim NUMFEEDTYPE As String = ""
            Dim NUMMAXCHARGINGRATE As String = ""
            Dim NUMAVERAGECHARGINGRATE As String = ""
            Dim NUMPRIMARYBURNERCAP As String = ""
            Dim PRIMARYFUELTYPE As String = ""
            Dim NUMSECONDARYBURNERCAP As String = ""
            Dim SECONDARYFUELTYPE As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.TypeOfHearth,  " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.FeedType, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.MaximumChargingRate, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.NormalChargingRate, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.PrimaryChamberBurnerCapacity, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.PrimaryChamberBurnerFuelType, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.SecondaryChamberBurnerCapacity, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.SecondaryChamberBurnerFuelType, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.Manufacturer, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.ModelNumber, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitSolidLiquidWasteIncineration.InstallationDate " & _
            "FROM tblEmissionUnitSolidLiquidWasteIncineration INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitSolidLiquidWasteIncineration.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitSolidLiquidWasteIncineration.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If

                If EUType = "Separation Process" Then
                    EUType = "15"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("TypeOfHearth")) Then
                    STRHEARTHGRATE = ""
                Else
                    STRHEARTHGRATE = GATVdr.item("TypeOfHearth")
                End If
                If IsDBNull(GATVdr.item("FeedType")) Then
                    NUMFEEDTYPE = ""
                Else
                    NUMFEEDTYPE = GATVdr.item("FeedType")
                End If
                If IsDBNull(GATVdr.item("MaximumChargingRate")) Then
                    NUMMAXCHARGINGRATE = ""
                Else
                    NUMMAXCHARGINGRATE = GATVdr.item("MaximumChargingRate")
                End If
                If IsDBNull(GATVdr.item("NormalChargingRate")) Then
                    NUMAVERAGECHARGINGRATE = ""
                Else
                    NUMAVERAGECHARGINGRATE = GATVdr.item("NormalChargingRate")
                End If
                If IsDBNull(GATVdr.item("PrimaryChamberBurnerCapacity")) Then
                    NUMPRIMARYBURNERCAP = ""
                Else
                    NUMPRIMARYBURNERCAP = GATVdr.item("PrimaryChamberBurnerCapacity")
                End If
                If IsDBNull(GATVdr.item("PrimaryChamberBurnerFuelType")) Then
                    PRIMARYFUELTYPE = ""
                Else
                    PRIMARYFUELTYPE = GATVdr.item("PrimaryChamberBurnerFuelType")
                End If
                Select Case PRIMARYFUELTYPE
                    Case "Natural gas"
                        PRIMARYFUELTYPE = "1"
                        SECONDARYFUELTYPE = ""
                    Case "Natural gas/propane"
                        PRIMARYFUELTYPE = "1"
                        SECONDARYFUELTYPE = "3"
                    Case "Natural gas or No. 2 Fuel Oil"
                        PRIMARYFUELTYPE = "1"
                        SECONDARYFUELTYPE = "5"
                    Case "Natural Gas or Digester Gas"
                        PRIMARYFUELTYPE = "1"
                        SECONDARYFUELTYPE = "2"
                    Case "Natural Gas and/or Digester Gas"
                        PRIMARYFUELTYPE = "1"
                        SECONDARYFUELTYPE = "2"
                    Case "Natural or Digester Gas"
                        PRIMARYFUELTYPE = "1"
                        SECONDARYFUELTYPE = "2"
                    Case Else
                        PRIMARYFUELTYPE = "0"
                        SECONDARYFUELTYPE = ""
                End Select

                If IsDBNull(GATVdr.item("SecondaryChamberBurnerCapacity")) Then
                    NUMSECONDARYBURNERCAP = ""
                Else
                    NUMSECONDARYBURNERCAP = GATVdr.item("SecondaryChamberBurnerCapacity")
                End If
                'If IsDBNull(GATVdr.item("SecondaryChamberBurnerFuelType")) Then
                '    SECONDARYFUELTYPE = ""
                'Else
                '    SECONDARYFUELTYPE = GATVdr.item("SecondaryChamberBurnerFuelType")
                'End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_SolidWaste " & _
                        "(PFW_EU_SolidWasteID, " & _
                        "emissionunitid, gsid, " & _
                        "STRHEARTHGRATE, NUMFEEDTYPE, " & _
                        "NUMMAXCHARGINGRATE, NUMAVERAGECHARGINGRATE, " & _
                        "NUMPRIMARYBURNERCAP, PRIMARYFUELTYPE, " & _
                        "NUMSECONDARYBURNERCAP, SECONDARYFUELTYPE, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_SolidWasteID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(Mid(STRHEARTHGRATE, 1, 400), "'", "''") & "', '" & NUMFEEDTYPE & "', " & _
                        "'" & NUMMAXCHARGINGRATE & "', '" & NUMAVERAGECHARGINGRATE & "', " & _
                        "'" & NUMPRIMARYBURNERCAP & "', '" & PRIMARYFUELTYPE & "', " & _
                        "'" & NUMSECONDARYBURNERCAP & "', '" & SECONDARYFUELTYPE & "', " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)
                        SaveMaterials(oldID, GSID, temp, "SolidWaste")

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()


        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUSolventCleaning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUSolventCleaning.Click
        Try
            PopulatePFWEUSolventCleaning()
        Catch ex As Exception
        
        End Try
    End Sub
    Sub PopulatePFWEUSolventCleaning()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""
            Dim SOLVENTMACHINETYPE As String = ""
            Dim SOLVENTTYPE As String = ""
            Dim NUMSOLVENTINTERFACEAREA As String = ""
            Dim NUMSOLVENTCONSUMPTION As String = ""
            Dim NUMSOLVENTCAPCITY As String = ""
            Dim NUMFREEBOARDREFRIGERATION As String = ""
            Dim NUMFREEBOARDRATIO070 As String = ""
            Dim NUMFREEBOARDRATIO075 As String = ""
            Dim NUMFREEBOARDRATIO100 As String = ""
            Dim NUMSUMPSHUTOFFLIQ As String = ""
            Dim NUMSUMPSHUTOFFVAPOR As String = ""
            Dim NUMIDLECOVERS As String = ""
            Dim NUMREDUCEDDRAFT As String = ""
            Dim NUMAUTOPARTSHANDLE As String = ""
            Dim NUMWORKINGCOVERS As String = ""
            Dim NUMSUPERHEATVAPOR As String = ""
            Dim NUMPRIMARYCONDENSER As String = ""
            Dim NUMDWELL As String = ""
            Dim NUMCARBONADSORBER As String = ""
            Dim NUMOTHERCONTROL As String = ""
            Dim STROTHERCONTROLDESC As String = ""
            Dim NUMTIGHTCOVER As String = ""
            Dim NUMWATERLAYER As String = ""
            Dim NUMCOLDFREEBOARDRATIO075 As String = ""


            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitSolventCleaningMachines.SolventUnsed, " & _
            "tblEmissionUnitSolventCleaningMachines.MachineType, " & _
            "tblEmissionUnitSolventCleaningMachines.IsNotification, " & _
            "tblEmissionUnitSolventCleaningMachines.SolventAirInterfaceArea, " & _
            "tblEmissionUnitSolventCleaningMachines.SolventAirInterfaceAreaUnits, " & _
            "tblEmissionUnitSolventCleaningMachines.HAPConsumption, " & _
            "tblEmissionUnitSolventCleaningMachines.HAPCunsumptionUnits, " & _
            "tblEmissionUnitSolventCleaningMachines.CleaningCapacity, " & _
            "tblEmissionUnitSolventCleaningMachines.CleaningCapacityUnits, " & _
            "tblEmissionUnitSolventCleaningMachines.FreeboardRefrig, " & _
            "tblEmissionUnitSolventCleaningMachines.FreeboardRatio070, " & _
            "tblEmissionUnitSolventCleaningMachines.FreeboardRatio075, " & _
            "tblEmissionUnitSolventCleaningMachines.FreeboardRatio10, " & _
            "tblEmissionUnitSolventCleaningMachines.SumpShutOffLiquid, " & _
            "tblEmissionUnitSolventCleaningMachines.SumpShutOffVapor, " & _
            "tblEmissionUnitSolventCleaningMachines.IdleCovers, " & _
            "tblEmissionUnitSolventCleaningMachines.ReducedDraft, " & _
            "tblEmissionUnitSolventCleaningMachines.AutomatedPartsHandle, " & _
            "tblEmissionUnitSolventCleaningMachines.WorkingCovers, " & _
            "tblEmissionUnitSolventCleaningMachines.SuperheatVapor, " & _
            "tblEmissionUnitSolventCleaningMachines.PrimaryCond, " & _
            "tblEmissionUnitSolventCleaningMachines.Dwell, " & _
            "tblEmissionUnitSolventCleaningMachines.CarbonAdsorb, " & _
            "tblEmissionUnitSolventCleaningMachines.OtherControl, " & _
            "tblEmissionUnitSolventCleaningMachines.DescriptionOfOtherControl, " & _
            "tblEmissionUnitSolventCleaningMachines.TypeOfBatchCold, " & _
            "tblEmissionUnitSolventCleaningMachines.TightCover, " & _
            "tblEmissionUnitSolventCleaningMachines.WaterLayer, " & _
            "tblEmissionUnitSolventCleaningMachines.CoolFreeboardRatio075, " & _
            "tblEmissionUnitSolventCleaningMachines.Manufacturer, " & _
            "tblEmissionUnitSolventCleaningMachines.ModelNumber, " & _
            "tblEmissionUnitSolventCleaningMachines.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitSolventCleaningMachines.InstallationDate " & _
            "FROM tblEmissionUnitSolventCleaningMachines INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitSolventCleaningMachines.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitSolventCleaningMachines.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Solvent Cleaning Machines" Then
                    EUType = "17"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("MachineType")) Then
                    SOLVENTMACHINETYPE = ""
                Else
                    SOLVENTMACHINETYPE = GATVdr.item("MachineType")
                End If
                If IsDBNull(GATVdr.item("SolventUnsed")) Then
                    SOLVENTTYPE = ""
                Else
                    SOLVENTTYPE = GATVdr.item("SolventUnsed")
                End If
                If IsDBNull(GATVdr.item("SolventAirInterfaceArea")) Then
                    NUMSOLVENTINTERFACEAREA = ""
                Else
                    NUMSOLVENTINTERFACEAREA = GATVdr.item("SolventAirInterfaceArea")
                End If
                If NUMSOLVENTINTERFACEAREA = True Then
                    NUMSOLVENTINTERFACEAREA = 1
                Else
                    NUMSOLVENTINTERFACEAREA = 0
                End If
                If IsDBNull(GATVdr.item("HAPConsumption")) Then
                    NUMSOLVENTCONSUMPTION = ""
                Else
                    NUMSOLVENTCONSUMPTION = GATVdr.item("HAPConsumption")
                End If
                If IsDBNull(GATVdr.item("CleaningCapacity")) Then
                    NUMSOLVENTCAPCITY = ""
                Else
                    NUMSOLVENTCAPCITY = GATVdr.item("CleaningCapacity")
                End If

                If IsDBNull(GATVdr.item("FreeboardRefrig")) Then
                    NUMFREEBOARDREFRIGERATION = ""
                Else
                    NUMFREEBOARDREFRIGERATION = GATVdr.item("FreeboardRefrig")
                End If
                If NUMFREEBOARDREFRIGERATION = True Then
                    NUMFREEBOARDREFRIGERATION = 1
                Else
                    NUMFREEBOARDREFRIGERATION = 0
                End If

                If IsDBNull(GATVdr.item("FreeboardRatio070")) Then
                    NUMFREEBOARDRATIO070 = ""
                Else
                    NUMFREEBOARDRATIO070 = GATVdr.item("FreeboardRatio070")
                End If
                If NUMFREEBOARDRATIO070 = True Then
                    NUMFREEBOARDRATIO070 = 1
                Else
                    NUMFREEBOARDRATIO070 = 0
                End If
                If IsDBNull(GATVdr.item("FreeboardRatio075")) Then
                    NUMFREEBOARDRATIO075 = ""
                Else
                    NUMFREEBOARDRATIO075 = GATVdr.item("FreeboardRatio075")
                End If
                If NUMFREEBOARDRATIO075 = True Then
                    NUMFREEBOARDRATIO075 = 1
                Else
                    NUMFREEBOARDRATIO075 = 0
                End If
                If IsDBNull(GATVdr.item("FreeboardRatio10")) Then
                    NUMFREEBOARDRATIO100 = ""
                Else
                    NUMFREEBOARDRATIO100 = GATVdr.item("FreeboardRatio10")
                End If
                If NUMFREEBOARDRATIO100 = True Then
                    NUMFREEBOARDRATIO100 = 1
                Else
                    NUMFREEBOARDRATIO100 = 0
                End If
                If IsDBNull(GATVdr.item("SumpShutOffLiquid")) Then
                    NUMSUMPSHUTOFFLIQ = ""
                Else
                    NUMSUMPSHUTOFFLIQ = GATVdr.item("SumpShutOffLiquid")
                End If
                If NUMSUMPSHUTOFFLIQ = True Then
                    NUMSUMPSHUTOFFLIQ = 1
                Else
                    NUMSUMPSHUTOFFLIQ = 0
                End If
                If IsDBNull(GATVdr.item("SumpShutOffVapor")) Then
                    NUMSUMPSHUTOFFVAPOR = ""
                Else
                    NUMSUMPSHUTOFFVAPOR = GATVdr.item("SumpShutOffVapor")
                End If
                If NUMSUMPSHUTOFFVAPOR = True Then
                    NUMSUMPSHUTOFFVAPOR = 1
                Else
                    NUMSUMPSHUTOFFVAPOR = 0
                End If
                If IsDBNull(GATVdr.item("IdleCovers")) Then
                    NUMIDLECOVERS = ""
                Else
                    NUMIDLECOVERS = GATVdr.item("IdleCovers")
                End If
                If NUMIDLECOVERS = True Then
                    NUMIDLECOVERS = 1
                Else
                    NUMIDLECOVERS = 0
                End If
                If IsDBNull(GATVdr.item("ReducedDraft")) Then
                    NUMREDUCEDDRAFT = ""
                Else
                    NUMREDUCEDDRAFT = GATVdr.item("ReducedDraft")
                End If
                If NUMREDUCEDDRAFT = True Then
                    NUMREDUCEDDRAFT = 1
                Else
                    NUMREDUCEDDRAFT = 0
                End If
                If IsDBNull(GATVdr.item("AutomatedPartsHandle")) Then
                    NUMAUTOPARTSHANDLE = ""
                Else
                    NUMAUTOPARTSHANDLE = GATVdr.item("AutomatedPartsHandle")
                End If
                If NUMAUTOPARTSHANDLE = True Then
                    NUMAUTOPARTSHANDLE = 1
                Else
                    NUMAUTOPARTSHANDLE = 0
                End If
                If IsDBNull(GATVdr.item("WorkingCovers")) Then
                    NUMWORKINGCOVERS = ""
                Else
                    NUMWORKINGCOVERS = GATVdr.item("WorkingCovers")
                End If
                If NUMWORKINGCOVERS = True Then
                    NUMWORKINGCOVERS = 1
                Else
                    NUMWORKINGCOVERS = 0
                End If
                If IsDBNull(GATVdr.item("SuperheatVapor")) Then
                    NUMSUPERHEATVAPOR = ""
                Else
                    NUMSUPERHEATVAPOR = GATVdr.item("SuperheatVapor")
                End If
                If NUMSUPERHEATVAPOR = True Then
                    NUMSUPERHEATVAPOR = 1
                Else
                    NUMSUPERHEATVAPOR = 0
                End If
                If IsDBNull(GATVdr.item("PrimaryCond")) Then
                    NUMPRIMARYCONDENSER = ""
                Else
                    NUMPRIMARYCONDENSER = GATVdr.item("PrimaryCond")
                End If
                If NUMPRIMARYCONDENSER = True Then
                    NUMPRIMARYCONDENSER = 1
                Else
                    NUMPRIMARYCONDENSER = 0
                End If
                If IsDBNull(GATVdr.item("Dwell")) Then
                    NUMDWELL = ""
                Else
                    NUMDWELL = GATVdr.item("Dwell")
                End If
                If NUMDWELL = True Then
                    NUMDWELL = 1
                Else
                    NUMDWELL = 0
                End If
                If IsDBNull(GATVdr.item("CarbonAdsorb")) Then
                    NUMCARBONADSORBER = ""
                Else
                    NUMCARBONADSORBER = GATVdr.item("CarbonAdsorb")
                End If
                If NUMCARBONADSORBER = True Then
                    NUMCARBONADSORBER = 1
                Else
                    NUMCARBONADSORBER = 0
                End If
                If IsDBNull(GATVdr.item("OtherControl")) Then
                    NUMOTHERCONTROL = ""
                Else
                    NUMOTHERCONTROL = GATVdr.item("OtherControl")
                End If
                If NUMOTHERCONTROL = True Then
                    NUMOTHERCONTROL = 1
                Else
                    NUMOTHERCONTROL = 0
                End If
                If IsDBNull(GATVdr.item("DescriptionOfOtherControl")) Then
                    STROTHERCONTROLDESC = ""
                Else
                    STROTHERCONTROLDESC = GATVdr.item("DescriptionOfOtherControl")
                End If
                If IsDBNull(GATVdr.item("TightCover")) Then
                    NUMTIGHTCOVER = ""
                Else
                    NUMTIGHTCOVER = GATVdr.item("TightCover")
                End If
                If NUMTIGHTCOVER = True Then
                    NUMTIGHTCOVER = 1
                Else
                    NUMTIGHTCOVER = 0
                End If
                If IsDBNull(GATVdr.item("WaterLayer")) Then
                    NUMWATERLAYER = ""
                Else
                    NUMWATERLAYER = GATVdr.item("WaterLayer")
                End If
                If NUMWATERLAYER = True Then
                    NUMWATERLAYER = 1
                Else
                    NUMWATERLAYER = 0
                End If
                If IsDBNull(GATVdr.item("CoolFreeboardRatio075")) Then
                    NUMCOLDFREEBOARDRATIO075 = ""
                Else
                    NUMCOLDFREEBOARDRATIO075 = GATVdr.item("CoolFreeboardRatio075")
                End If
                If NUMCOLDFREEBOARDRATIO075 = True Then
                    NUMCOLDFREEBOARDRATIO075 = 1
                Else
                    NUMCOLDFREEBOARDRATIO075 = 0
                End If
                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_SolventCleaning " & _
                        "(pfw_eu_SolventCleaningID, " & _
                        "emissionunitid, gsid, " & _
                        "SOLVENTMACHINETYPE, SOLVENTTYPE, " & _
                        "NUMSOLVENTINTERFACEAREA, NUMSOLVENTCONSUMPTION, " & _
                        "NUMSOLVENTCAPCITY, NUMFREEBOARDREFRIGERATION, " & _
                        "NUMFREEBOARDRATIO070, NUMFREEBOARDRATIO075, " & _
                        "NUMFREEBOARDRATIO100, NUMSUMPSHUTOFFLIQ, " & _
                        "NUMSUMPSHUTOFFVAPOR, NUMIDLECOVERS, " & _
                        "NUMREDUCEDDRAFT, NUMAUTOPARTSHANDLE, " & _
                        "NUMWORKINGCOVERS, NUMSUPERHEATVAPOR, " & _
                        "NUMPRIMARYCONDENSER, NUMDWELL, " & _
                        "NUMCARBONADSORBER, NUMOTHERCONTROL, " & _
                        "STROTHERCONTROLDESC, NUMTIGHTCOVER, " & _
                        "NUMWATERLAYER, NUMCOLDFREEBOARDRATIO075, " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_SolventCleaningID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(SOLVENTMACHINETYPE, "'", "''") & "', '" & Replace(SOLVENTTYPE, "'", "''") & "', " & _
                        "'" & Replace(NUMSOLVENTINTERFACEAREA, "'", "''") & "', '" & Replace(NUMSOLVENTCONSUMPTION, "'", "''") & "', " & _
                        "'" & Replace(NUMSOLVENTCAPCITY, "'", "''") & "', '" & Replace(NUMFREEBOARDREFRIGERATION, "'", "''") & "', " & _
                        "'" & Replace(NUMFREEBOARDRATIO070, "'", "''") & "', '" & Replace(NUMFREEBOARDRATIO075, "'", "''") & "', " & _
                        "'" & Replace(NUMFREEBOARDRATIO100, "'", "''") & "', '" & Replace(NUMSUMPSHUTOFFLIQ, "'", "''") & "', " & _
                        "'" & Replace(NUMSUMPSHUTOFFVAPOR, "'", "''") & "', '" & Replace(NUMIDLECOVERS, "'", "''") & "', " & _
                        "'" & Replace(NUMREDUCEDDRAFT, "'", "''") & "', '" & Replace(NUMAUTOPARTSHANDLE, "'", "''") & "', " & _
                        "'" & Replace(NUMWORKINGCOVERS, "'", "''") & "', '" & Replace(NUMSUPERHEATVAPOR, "'", "''") & "', " & _
                        "'" & Replace(NUMPRIMARYCONDENSER, "'", "''") & "', '" & Replace(NUMDWELL, "'", "''") & "', " & _
                        "'" & Replace(NUMCARBONADSORBER, "'", "''") & "', '" & Replace(NUMOTHERCONTROL, "'", "''") & "', " & _
                        "'" & Replace(STROTHERCONTROLDESC, "'", "''") & "', '" & Replace(NUMTIGHTCOVER, "'", "''") & "', " & _
                        "'" & Replace(NUMWATERLAYER, "'", "''") & "', '" & Replace(NUMCOLDFREEBOARDRATIO075, "'", "''") & "', " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', " & _
                        "'1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Private Sub btnEUTextileCoater_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEUTextileCoater.Click
        Try
            PopulatePFWEUTextileCoater()
        Catch ex As Exception
           
        End Try
    End Sub
    Sub PopulatePFWEUTextileCoater()
        Try
            Dim ProjectId As String = ""
            Dim GSID As String = ""
            Dim EUName As String = ""
            Dim EUID As String = ""
            Dim desc As String = ""
            Dim oldID As String = ""
            Dim EUType As String = ""
            Dim Comments As String = ""
            Dim Manufacturer As String = ""
            Dim ModelNumber As String = ""
            Dim DateManufactured As String = ""
            Dim InstallationDate As String = ""
            Dim Installation As String = ""
            Dim STRAPPLICATORTYPE As String = ""
            Dim STRAPPLICATORTYPEOTHERDESC As String = ""
            Dim NUMPOLYMERICCOATING As String = ""
            Dim NUMWATERCONTENT As String = ""
            Dim NUMVOCCONTENT As String = ""
            Dim STRHEATSETTYPE As String = ""
            Dim STRHEATSETTYPEOTHERDESC As String = ""
            Dim STRSUBSTRATETYPE As String = ""
            Dim STRSUBSTRATETYPEOTHERDESC As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblEmissionUnitTextileCoater.CoatingApplicatorType, " & _
            "tblEmissionUnitTextileCoater.DescApplicatorTypeOther, " & _
            "tblEmissionUnitTextileCoater.PolymericCoatingUsed, " & _
            "tblEmissionUnitTextileCoater.HeatsetType, " & _
            "tblEmissionUnitTextileCoater.DescHeatsetOther, " & _
            "tblEmissionUnitTextileCoater.Substrate, " & _
            "tblEmissionUnitTextileCoater.DescSubstrateOther, " & _
            "tblEmissionUnitTextileCoater.WaterContent, " & _
            "tblEmissionUnitTextileCoater.VOCContent, " & _
            "tblEmissionUnitTextileCoater.Manufacturer, " & _
            "tblEmissionUnitTextileCoater.ModelNumber, " & _
            "tblEmissionUnitTextileCoater.DateManufactured_Reconstructed, " & _
            "tblEmissionUnitTextileCoater.InstallationDate " & _
            "FROM tblEmissionUnitTextileCoater INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblEmissionUnitTextileCoater.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblEmissionUnitTextileCoater.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectIdentifier")) Then
                    ProjectId = ""
                Else
                    ProjectId = GATVdr.item("ProjectIdentifier")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    EUName = ""
                Else
                    EUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    desc = "None given"
                Else
                    desc = GATVdr.item("EquipmentDescription")
                End If
                If IsDBNull(GATVdr.item("EquipmentType")) Then
                    EUType = ""
                Else
                    EUType = GATVdr.item("EquipmentType")
                End If
                If EUType = "Textile Coater" Then
                    EUType = "18"
                Else
                    EUType = "19"
                End If

                If IsDBNull(GATVdr.item("CoatingApplicatorType")) Then
                    STRAPPLICATORTYPE = ""
                Else
                    STRAPPLICATORTYPE = GATVdr.item("CoatingApplicatorType")
                End If
                If IsDBNull(GATVdr.item("DescApplicatorTypeOther")) Then
                    STRAPPLICATORTYPEOTHERDESC = ""
                Else
                    STRAPPLICATORTYPEOTHERDESC = GATVdr.item("DescApplicatorTypeOther")
                End If
                If IsDBNull(GATVdr.item("PolymericCoatingUsed")) Then
                    NUMPOLYMERICCOATING = ""
                Else
                    NUMPOLYMERICCOATING = GATVdr.item("PolymericCoatingUsed")
                End If
                If NUMPOLYMERICCOATING = True Then
                    NUMPOLYMERICCOATING = 1
                Else
                    NUMPOLYMERICCOATING = 0
                End If
                If IsDBNull(GATVdr.item("WaterContent")) Then
                    NUMWATERCONTENT = ""
                Else
                    NUMWATERCONTENT = GATVdr.item("WaterContent")
                End If
                If IsDBNull(GATVdr.item("VOCContent")) Then
                    NUMVOCCONTENT = ""
                Else
                    NUMVOCCONTENT = GATVdr.item("VOCContent")
                End If
                If IsDBNull(GATVdr.item("HeatsetType")) Then
                    STRHEATSETTYPE = ""
                Else
                    STRHEATSETTYPE = GATVdr.item("HeatsetType")
                End If
                If IsDBNull(GATVdr.item("DescHeatsetOther")) Then
                    STRHEATSETTYPEOTHERDESC = ""
                Else
                    STRHEATSETTYPEOTHERDESC = GATVdr.item("DescHeatsetOther")
                End If
                If IsDBNull(GATVdr.item("Substrate")) Then
                    STRSUBSTRATETYPE = ""
                Else
                    STRSUBSTRATETYPE = GATVdr.item("Substrate")
                End If
                If IsDBNull(GATVdr.item("DescSubstrateOther")) Then
                    STRSUBSTRATETYPEOTHERDESC = ""
                Else
                    STRSUBSTRATETYPEOTHERDESC = GATVdr.item("DescSubstrateOther")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    Manufacturer = ""
                Else
                    Manufacturer = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    ModelNumber = ""
                Else
                    ModelNumber = GATVdr.item("ModelNumber")
                End If
                Installation = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DateManufactured = ""
                    Installation = ""
                Else
                    DateManufactured = GATVdr.item("DateManufactured_Reconstructed")
                    Installation = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DateManufactured) And DateManufactured.Length = 4 Then
                    DateManufactured = "01-Jan-" & DateManufactured
                End If
                If IsDate(DateManufactured) Then
                    DateManufactured = Format(CDate(DateManufactured), "dd-MMM-yyyy")
                Else
                    DateManufactured = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    InstallationDate = ""
                    Installation = Installation
                Else
                    InstallationDate = GATVdr.item("InstallationDate")
                    Installation = Installation & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(InstallationDate) And InstallationDate.Length = 4 Then
                    InstallationDate = "01-Jan-" & InstallationDate
                End If
                If IsDate(InstallationDate) Then
                    InstallationDate = Format(CDate(InstallationDate), "dd-MMM-yyyy")
                Else
                    InstallationDate = ""
                End If
                Installation = Mid(Installation, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITMASTER " & _
                        "(EMISSIONUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_EMISSIONUNITID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITID " & _
                       "(EMISSIONUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(EUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                       "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EMISSIONUNITHEADER " & _
                        "(EMISSIONUNITHEADERID, EMISSIONUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, EMISSIONUNITTYPE , " & _
                        "UNITTYPE, COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_EMISSIONUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(desc, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(Manufacturer, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(ModelNumber, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DateManufactured, "'", "''") & "', '" & Replace(InstallationDate, "'", "''") & "', " & _
                        "'" & Replace(Installation, "'", "''") & "', 'O', " & _
                        "sysdate, '" & EUType & "', " & _
                        "'100', '" & EUID & "', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_EU_TextileCoater " & _
                        "(pfw_eu_TextileCoaterID, " & _
                        "emissionunitid, gsid, " & _
                        "STRAPPLICATORTYPE, STRAPPLICATORTYPEOTHERDESC, " & _
                        "NUMPOLYMERICCOATING, NUMWATERCONTENT, " & _
                        "NUMVOCCONTENT, STRHEATSETTYPE, " & _
                        "STRHEATSETTYPEOTHERDESC, STRSUBSTRATETYPE, " & _
                        "STRSUBSTRATETYPEOTHERDESC,  " & _
                        "comments, Active) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_EU_TextileCoaterID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(STRAPPLICATORTYPE, "'", "''") & "', '" & Replace(STRAPPLICATORTYPEOTHERDESC, "'", "''") & "', " & _
                        "'" & NUMPOLYMERICCOATING & "', '" & NUMWATERCONTENT & "', " & _
                        "'" & NUMVOCCONTENT & "' , '" & Replace(STRHEATSETTYPE, "'", "''") & "',  " & _
                        "'" & Replace(STRHEATSETTYPEOTHERDESC, "'", "''") & "',  '" & Replace(STRSUBSTRATETYPE, "'", "''") & "',  " & _
                        "'" & Replace(STRSUBSTRATETYPEOTHERDESC, "'", "''") & "',   " & _
                        "'" & Replace(Mid(Comments, 1, 2000), "'", "''") & "', " & _
                        "'1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        SaveFuelBurning(oldID, GSID, temp)

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("done")
        End Try
    End Sub
    Sub SaveFuelBurning(ByVal oldID As String, ByVal GSID As String, ByVal EUID As String)
        Try
            Dim EUName As String = ""
            Dim desc As String = ""
            Dim FuelType As String = ""
            Dim strFuelTypeOther As String = ""
            Dim NUMPOTENTIALFUELCONSUMPTION As String = ""
            Dim NUMMAXCONSUMPTION As String = ""
            Dim NUMAVGCONSUMPTION As String = ""
            Dim NUMMAXANNUALCONSUMPTION As String = ""
            Dim NUMPERCENTOZONESEASON As String = ""
            Dim NUMMAXHEATINGVALUE As String = ""
            Dim NUMMAXHEATINGVALUEUNITS As String = ""
            Dim NUMMAXHEATINPUT As String = ""
            Dim NUMMINHEATINPUT As String = ""
            Dim NUMAVGHEATINPUT As String = ""
            Dim NUMMAXALLOWABLESULFURPERCENT As String = ""
            Dim NUMMAXPROPOSEDSULFURPERCENT As String = ""
            Dim NUMAVGPROPOSEDSULFURPERCENT As String = ""
            Dim NUMMAXPROPOSEDASHCONTENT As String = ""
            Dim NUMAVGPROPOSEDASHCONTENT As String = ""

            SQL = "Select " & _
            "tblEmissionUnitFuels_Sub.FuelTypeandGrade, " & _
            "tblEmissionUnitFuels_Sub.MaxHourlyConsumption, " & _
            "tblEmissionUnitFuels_Sub.HourlyConsumptionUnits, " & _
            "tblEmissionUnitFuels_Sub.MaxHeatInput, " & _
            "tblEmissionUnitFuels_Sub.HeatInputUnits, " & _
            "tblEmissionUnitFuels_Sub.MaxAnnualFuelConsumption, " & _
            "tblEmissionUnitFuels_Sub.AnnualConsumptionUnits, " & _
            "tblEmissionUnitFuels_Sub.MaximumHeatingValue, " & _
            "tblEmissionUnitFuels_Sub.MaxHeatingValueUnits, " & _
            "tblEmissionUnitFuels_Sub.MaximumAllowableSulfurPercent " & _
            "from tblEmissionUnitFuels_Sub " & _
            "where EmissionUnitID = " & oldID & " "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr2 = GATVcmd.ExecuteReader
            While GATVdr2.Read
                FuelType = ""
                If IsDBNull(GATVdr2.item("FuelTypeandGrade")) Then
                    FuelType = ""
                Else
                    FuelType = GATVdr2.item("FuelTypeandGrade")
                End If
                FuelType = FuelType.ToString

                Select Case FuelType
                    Case "Natural Gas", "Pipeline quality natural gas", "Natural Gas ", "Pipeline grade natural gas", _
                          "Pipeline Natural Gas", "Natural Gas (Primary)", "Natural Gas (burners combined)", "Natural  Gas", _
                          "*Natural Gas", "Fuel: Natural Gas", "Natural Gas - pilot", "Natual Gas", "Narural gas", _
                          "natrual gas", "NAT. GAS", "Natrual Gas (Primary)", "Natral Gas", "NAURAL GAS"
                        FuelType = "1"

                    Case "Landfill Gas", "Landfill Gas (Methane)", "Landfill Gas (LFG)", "LANDFILL/ANAEROBIC DIGESTER BIOGAS", _
                        "Fuel: Landfill Gas", "pre-treated landfill gas", "LFG"
                        FuelType = "2"

                    Case "Propane (backup)", "Propane", "Propane-Air", "Propane (LPG)", _
                            "Liquid Propane Gas", "Liquid Propane Gas (LPG)", "Propane (see footnote document)", _
                            "Propane HD-5", "PROPANE GAS", "Propane/Air mixture", "Vaporized Propane and Air Mixture", _
                            "Propane *", "liquid propane", "BUTANE/PROPANE", "*Propane", _
                            "Propane (Back-up)", "LPG (Propane)", "Propane for light off only", "Fuel: Propane", _
                            "LPG(propane)"
                        FuelType = "3"

                    Case "Ultra Low Sulfur Diesel", "Ultra Low Sulfur Diesel Fuel", "Diesel Fuel", "#2 Diesel Fuel", _
                            "No 2 Diesel", "No 2 Diesel Fuel", "Diesel", "No. 2 Diesel Fuel", _
                            "Diesel #2", "Diesel Oil", "#2 Diesel", "#2 DIESEL FUEL OIL", _
                            "Diesel ", "No. 2 Diesel", "Diesel Fuel ", "#2 Fuel Oil (Diesel)", _
                            "Low Sulfur Diesel Fuel", "Ultra-Low Sulfur Diesel", "Diesel Fuel Oil", "Diesel fuel Number 2"
                        FuelType = "4"

                    Case "Distillate Oil No. 2", "Distillate Fuel Oil No. 2", "No. 2 Fuel Oil", "No 2 Fuel Oil", _
                        "#2 Fuel Oil", "#2 fuel oil (see footnote document)", "No. 2 fuel oil (see footnote document)", _
                        "Fuel Oil (#2)", "No. 2 Distillate Fuel Oil", "Fuel Oil - #2", _
                        "Distillate Fuel Oil (Fuel Oil #2)", "Fuell Oil #2", "Fuel Oil #2", _
                        "No. 2 Oil", "No. 2 Oil (calciner)  (4) (5)", "No. 2 Fuel Oil (Boiler #1)", _
                        "No. 2 Fuel Oil (Boiler #2)", "No. 2 Fuel Oil (#1 boiler)", "No. 2 Fuel Oil (#2 boiler)", _
                        "Waste gas-H2", "Fuel Oil No. 2", "fuel oil no.2, 0.5% sulfur content", _
                        "# 2 Fuel Oil", "#2 distillate Fuel oil", "No. 2 Fuel Oil ", "No.2 Fuel Oil", "Number 2 Oil", _
                        "No2. Fuel Oil", "No. 6 Fuel Oil/ No. 2 Fuel Oil", "#2 Fuel Oil containing < 0.5 wt. % Sulfur", _
                        "# 2 Fuel Oil (standby use only)", "Fuel Oil # 2", "Distillate #2 fuel oil", "Bio-Diesel 20", _
                        "#2 FUEL OIL ", "LOW SULFUR NO. 2 FUEL OIL", "Number 2 Fuel Oil", "#2 Fuel Oil (backup)", _
                        "No. 2 Fuel Oil (Predryer burner) (b)", "No. 2 Fuel Oil (Burner 4A-YD) (c)", "No. 2. fuel oil", _
                        "#2 low sulfur fuel oil", "# 2 low sulfur fuel oil", "No. 2 Fuel (emergency start-up only)", _
                        "#2 Fuel Oul", "Distillate Oil #2", "Distillate Oil#2", "#6 Fuel Oil (or #2/#6 mix)", "N. 2 fuel oil", _
                        "Fuel Oil - Nos. 2 & 4", "No. 2 Distillate Oil", "No. 2", "Fuel Oils 1, 2, 4, 5, and 6", _
                        "Fuel Oil 1, 2, 4, 5 and 6", "Fuel Oil No 2", "Fuel: #2 Fuel Oil", "No.2 Distillate Fuel oil", _
                        "No.2 Distillate Oil", "Oil (numbers 2 through 6)"
                        FuelType = "5"

                    Case "#6 Fuel oil", "#6 fuel oil (see footnote document)", "No. 6 Oil", _
                        "No. 6 Fuel Oil", "#6 FUELOIL", "No. 6 Fuel Oil/Used Oil", "No.6 fuel oil ", _
                        "Fuel Oil No. 6", "Fuel Oil #6", "#6 Fuel Oil (Secondary)", "Fuel Oil #6 (Secondary)", _
                        "Oil #6", "Used Oil (blended with No. 6 fuel oil)", "No.6 Fuel Oil", "No. 6 residual oil", _
                        "# 6 fuel oil", "Fuel Oil, No.6", "No. 6 Fuel Oil / Used Oil", "No. 6 Fuel Oil ", _
                        "Fuel: #6 Fuel Oil", "#6 fuel oil - residual oil", "Digester Gas (App. 65% methane)"
                        FuelType = "6"

                    Case "Black liquor", "Black Liquor Solids", "Weak Black Liquor", "Black Liquor Solids (dry)"
                        FuelType = "7"

                    Case "Biodiesel", "Biogas", "Biomass (wet)", "Biomass (dry)", "Biomass (hog fuel)", _
                        "Bio-Diesel", "Biomass", "Wood waste and various biomass species", _
                        "Bark/ Wood Waste/ Tobacco stalks and other biomass", "Diesel or Biodiesel (see footnote document)", _
                        "Biomass (see footnote document)", "Biomass/Agricultural Wastes (wet)", "Biomass/Agricultural Wastes (dry)", _
                        "Agricultural Biogenic Materials", "Cellulosic Biomass- Untreated", "Cellulosic Biomass- Treated"
                        FuelType = "8"

                    Case "coal- bituminous (see footnote document)", "coal - bituminous", "Coal, Pulverized", _
                        "coal-bituminous (see footnote document)", "coal - bituminous (see footnote document)", _
                        "Coal", "Bituminous Coal", "Coal (kiln coal burner)", "Coal (calciner) (4)", _
                        "Coal- subbituminous (see footnote document)", "Coal- bitruminous (see footnote document)", _
                        "Pulverized coal", "Pulveized Coal", "Coal,Bituminous", "bituminus coal", "Coal/ Synfuel", _
                        "Coal-subbituminous", "Coal, Bituminous"
                        FuelType = "9"

                    Case "Scrap Tires (3)", "Tire Derived Fuel (TDF)", "Tire-derived fuel", "Tire derived fuel", _
                        "Scrap Tires", "Waste Tires", "Tires"
                        FuelType = "10"

                    Case "wood flour", "Wood Fuel", "Woodwaste", "wood", "Wood waste", "Wood Residue", _
                        "Woodwaste (based on green material)", "Bark and Woodwaste", "wood shavings", "Other Wood Waste", _
                        "Bark/Woodwaste", "Bark/wood waste", "Pine bark & wood waste", "wood and bark", _
                        "Wood Waste (Based on green material)", "Wood Waste Design Blend", "Highest Btu Wood Waste", _
                        "Wood Bark, Fines, and Sanderdust/Sawdust", "Wood/Cotton Waste", "Wood and allowable waste materials", _
                        "Dry Scrap Construction Wood", "wood waste (shavings)", "Woodwaste (green sawdust)", _
                        "Wood fuel, sawdust and bark", "Wood chips", "Wood Residue Fuel", "wood waste (sawdust and bark)", _
                        "Green wood saw dust", "Dry wood sawdust", "Bark/ Wood Waste", "Wood Bark ", "Wood Residuals", _
                        "Dried Wood Shavings", "WASTE WOOD CHIPS AND BARK", "Woodwaste/Bark ", "Wood Fuel, sawdust", _
                        "Green Wood Residuals (Bark, Sawdust, etc.)", "Dry Wood Residuals (Plytrim, Sanderdust, etc.)", _
                        "Dry wood shavings", "Syngas from gasified carpet and wood flour"
                        FuelType = "11"

                    Case Else
                        strFuelTypeOther = FuelType
                        FuelType = "0"

                End Select

                If IsDBNull(GATVdr2.item("MaxHourlyConsumption")) Then
                    NUMMAXCONSUMPTION = ""
                Else
                    NUMMAXCONSUMPTION = GATVdr2.item("MaxHourlyConsumption")
                End If
                If IsDBNull(GATVdr2.item("HourlyConsumptionUnits")) Then
                    NUMAVGCONSUMPTION = ""
                Else
                    NUMAVGCONSUMPTION = GATVdr2.item("HourlyConsumptionUnits")
                    NUMAVGCONSUMPTION = "0"
                End If
                If IsDBNull(GATVdr2.item("MaxHeatInput")) Then
                    NUMMAXHEATINPUT = ""
                Else
                    NUMMAXHEATINPUT = GATVdr2.item("MaxHeatInput")
                End If
                'If IsDBNull(GATVdr.item("HeatInputUnits")) Then
                '    FuelType = ""
                'Else
                '    FuelType = GATVdr.item("HeatInputUnits")
                'End If
                If IsDBNull(GATVdr2.item("MaxAnnualFuelConsumption")) Then
                    NUMMAXANNUALCONSUMPTION = ""
                Else
                    NUMMAXANNUALCONSUMPTION = GATVdr2.item("MaxAnnualFuelConsumption")
                End If
                'If IsDBNull(GATVdr.item("AnnualConsumptionUnits")) Then
                '    FuelType = ""
                'Else
                '    FuelType = GATVdr.item("AnnualConsumptionUnits")
                'End If
                If IsDBNull(GATVdr2.item("MaximumHeatingValue")) Then
                    NUMMAXHEATINGVALUE = ""
                Else
                    NUMMAXHEATINGVALUE = GATVdr2.item("MaximumHeatingValue")
                End If
                If IsDBNull(GATVdr2.item("MaxHeatingValueUnits")) Then
                    NUMMAXHEATINGVALUEUNITS = ""
                Else
                    NUMMAXHEATINGVALUEUNITS = GATVdr2.item("MaxHeatingValueUnits")
                    NUMMAXHEATINGVALUEUNITS = "0"
                End If
                If IsDBNull(GATVdr2.item("MaximumAllowableSulfurPercent")) Then
                    NUMMAXALLOWABLESULFURPERCENT = ""
                Else
                    NUMMAXALLOWABLESULFURPERCENT = GATVdr2.item("MaximumAllowableSulfurPercent")
                End If

                SQL = "Insert into AIRTVApplication.PFW_EUS_FuelBurning " & _
                "(PFW_EUS_FUELBURNINGID, EMISSIONUNITID, " & _
                "GSID, " & _
                "FUELTYPE, STRFUELTYPEOTHER, " & _
                "NUMPOTENTIALFUELCONSUMPTION, NUMMAXCONSUMPTION, " & _
                "NUMAVGCONSUMPTION, NUMMAXANNUALCONSUMPTION, " & _
                "NUMPERCENTOZONESEASON, NUMMAXHEATINGVALUE, " & _
                "NUMMAXHEATINGVALUEUNITS, NUMMAXHEATINPUT, " & _
                "NUMMINHEATINPUT, NUMAVGHEATINPUT, " & _
                "NUMMAXALLOWABLESULFURPERCENT, NUMMAXPROPOSEDSULFURPERCENT, " & _
                "NUMAVGPROPOSEDSULFURPERCENT, NUMMAXPROPOSEDASHCONTENT, " & _
                "NUMAVGPROPOSEDASHCONTENT, COMMENTS, " & _
                "ACTIVE) " & _
                "select " & _
                "AIRTVProject.SEQ_GAP_EUS_FUELBURNINGID.nextval, '" & EUID & "', " & _
                "'" & GSID & "', " & _
                "'" & FuelType & "', '" & Replace(strFuelTypeOther, "'", "''") & "', " & _
                "'" & NUMPOTENTIALFUELCONSUMPTION & "', '" & NUMMAXCONSUMPTION & "', " & _
                "'" & NUMAVGCONSUMPTION & "', '" & NUMMAXANNUALCONSUMPTION & "', " & _
                "'" & NUMPERCENTOZONESEASON & "', '" & NUMMAXHEATINGVALUE & "', " & _
                "'" & NUMMAXHEATINGVALUEUNITS & "', '" & NUMMAXHEATINPUT & "', " & _
                "'" & NUMMINHEATINPUT & "', '" & NUMAVGHEATINPUT & "', " & _
                "'" & NUMMAXALLOWABLESULFURPERCENT & "', '" & NUMMAXPROPOSEDSULFURPERCENT & "', " & _
                "'" & NUMAVGPROPOSEDSULFURPERCENT & "', '" & NUMMAXPROPOSEDASHCONTENT & "', " & _
                "'" & NUMAVGPROPOSEDASHCONTENT & "', 'population event', " & _
                "'1' " & _
                "from dual " & _
                "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                "where EMISSIONUNITID = " & EUID & ") "

                cmd2 = New OracleCommand(SQL, ConnTVApplication)
                If ConnTVApplication.State = ConnectionState.Closed Then
                    ConnTVApplication.Open()
                End If
                dr2 = cmd2.ExecuteReader
                dr2.Close()

            End While
            GATVdr2.close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            '            MsgBox("done")
        End Try
    End Sub
    Sub SaveMaterials(ByVal oldID As String, ByVal GSID As String, ByVal EUID As String, ByVal EUType As String)
        Try
            Dim EUName As String = ""
            Dim desc As String = ""
            Dim STRMATERIALNAME As String = ""
            Dim STRMATERIALORIGIN As String = ""
            Dim NUMMATERIALTYPE As String = ""
            Dim NUMHOURLYRATE As String = ""
            Dim NUMHOURLYRATEUNITS As String = ""
            Dim NUMMAXHOURLYRATE As String = ""
            Dim NUMMAXHOURLYRATEUNITS As String = ""
            Dim NUMMAXANNUALINPUT As String = ""
            Dim NUMMAXANNUALINPUTUNITS As String = ""
            Dim STRCASNUMBER As String = ""
            Dim NUMPRECENTOFTOTAL As String = ""
            Dim NUMVAPORPRESSURE As String = ""
            Dim NUMMOISTURECONTENT As String = ""
            Dim NUMVESSELFLOW As String = ""

            Select Case EUType
                Case "Crusher"
                    SQL = "Select " & _
                    "tblEmissionUnitCrushedMaterialsProcessed_Sub.EmissionUnitID, " & _
                    "tblEmissionUnitCrushedMaterialsProcessed_Sub.CrushedMaterial, " & _
                    "tblEmissionUnitCrushedMaterialsProcessed_Sub.MaxHourlyRate, " & _
                    "tblEmissionUnitCrushedMaterialsProcessed_Sub.MaxHourlyRateUnits, " & _
                    "tblEmissionUnitCrushedMaterialsProcessed_Sub.AvgFreeMoisture " & _
                    "from tblEmissionUnitCrushedMaterialsProcessed_Sub " & _
                    "where EmissionUnitID = " & oldID & " "

                    GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                    GATVcmd = New OleDbCommand(SQL, GATVConn)
                    If GATVConn.State = ConnectionState.Closed Then
                        GATVConn.Open()
                    End If
                    GATVdr2 = GATVcmd.ExecuteReader
                    While GATVdr2.Read

                        STRMATERIALNAME = ""
                        STRMATERIALORIGIN = ""
                        NUMMATERIALTYPE = "1"
                        NUMHOURLYRATE = ""
                        NUMHOURLYRATEUNITS = ""
                        NUMMAXHOURLYRATE = ""
                        NUMMAXHOURLYRATEUNITS = ""
                        NUMMAXANNUALINPUT = ""
                        NUMMAXANNUALINPUTUNITS = ""
                        STRCASNUMBER = ""
                        NUMPRECENTOFTOTAL = ""
                        NUMVAPORPRESSURE = ""
                        NUMMOISTURECONTENT = ""
                        NUMVESSELFLOW = ""

                        If IsDBNull(GATVdr2.item("CrushedMaterial")) Then
                            STRMATERIALNAME = "No Name"
                        Else
                            STRMATERIALNAME = GATVdr2.item("CrushedMaterial")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyRate")) Then
                            NUMMAXHOURLYRATE = ""
                        Else
                            NUMMAXHOURLYRATE = GATVdr2.item("MaxHourlyRate")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyRateUnits")) Then
                            NUMMAXHOURLYRATEUNITS = ""
                        Else
                            NUMMAXHOURLYRATEUNITS = GATVdr2.item("MaxHourlyRateUnits")
                        End If
                        NUMMAXHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("AvgFreeMoisture")) Then
                            NUMMOISTURECONTENT = ""
                        Else
                            NUMMOISTURECONTENT = GATVdr2.item("AvgFreeMoisture")
                        End If

                        SQL = "Insert into AIRTVApplication.PFW_EUS_Material " & _
                        "(PFW_EUS_MATERIALID, EMISSIONUNITID, " & _
                        "GSID, " & _
                        "STRMATERIALNAME, STRMATERIALORIGIN, " & _
                        "NUMMATERIALTYPE, NUMHOURLYRATE, " & _
                        "NUMHOURLYRATEUNITS, NUMMAXHOURLYRATE, " & _
                        "NUMMAXHOURLYRATEUNITS, NUMMAXANNUALINPUT, " & _
                        "NUMMAXANNUALINPUTUNITS, STRCASNUMBER, " & _
                        "NUMPRECENTOFTOTAL, NUMVAPORPRESSURE, " & _
                        "NUMMOISTURECONTENT, NUMVESSELFLOW, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "AIRTVProject.SEQ_GAP_EUS_MATERIALID.nextVal, '" & EUID & "', " & _
                        "'" & GSID & "', " & _
                        "'" & Replace(STRMATERIALNAME, "'", "''") & "', '" & Replace(STRMATERIALORIGIN, "'", "''") & "', " & _
                        "'" & NUMMATERIALTYPE & "', '" & NUMHOURLYRATE & "', " & _
                        "'" & NUMHOURLYRATEUNITS & "', '" & NUMMAXHOURLYRATE & "', " & _
                        "'" & NUMMAXHOURLYRATEUNITS & "', '" & NUMMAXANNUALINPUT & "', " & _
                        "'" & NUMMAXANNUALINPUTUNITS & "', '" & Replace(STRCASNUMBER, "'", "''") & "', " & _
                        "'" & NUMPRECENTOFTOTAL & "', '" & NUMVAPORPRESSURE & "', " & _
                        "'" & NUMMOISTURECONTENT & "', '" & NUMVESSELFLOW & "', " & _
                        "'population event', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & EUID & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While
                    GATVdr2.close()


                Case "Drying Equipment"
                    SQL = "Select " & _
                   "tblEmissionUnitMaterialsProcessed_sub.EquipmentID, " & _
                   "tblEmissionUnitMaterialsProcessed_sub.Material, " & _
                   "tblEmissionUnitMaterialsProcessed_sub.MaxHourlyInputRate, " & _
                   "tblEmissionUnitMaterialsProcessed_sub.MaxHourlyRateUnits, " & _
                   "tblEmissionUnitMaterialsProcessed_sub.AverageFreeMoistureContent " & _
                   "from tblEmissionUnitMaterialsProcessed_sub " & _
                   "where EquipmentID = " & oldID & " "

                    GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                    GATVcmd = New OleDbCommand(SQL, GATVConn)
                    If GATVConn.State = ConnectionState.Closed Then
                        GATVConn.Open()
                    End If
                    GATVdr2 = GATVcmd.ExecuteReader
                    While GATVdr2.Read
                        STRMATERIALNAME = ""
                        STRMATERIALORIGIN = ""
                        NUMMATERIALTYPE = "1"
                        NUMHOURLYRATE = ""
                        NUMHOURLYRATEUNITS = ""
                        NUMMAXHOURLYRATE = ""
                        NUMMAXHOURLYRATEUNITS = ""
                        NUMMAXANNUALINPUT = ""
                        NUMMAXANNUALINPUTUNITS = ""
                        STRCASNUMBER = ""
                        NUMPRECENTOFTOTAL = ""
                        NUMVAPORPRESSURE = ""
                        NUMMOISTURECONTENT = ""
                        NUMVESSELFLOW = ""

                        If IsDBNull(GATVdr2.item("Material")) Then
                            STRMATERIALNAME = "No Name"
                        Else
                            STRMATERIALNAME = GATVdr2.item("Material")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyInputRate")) Then
                            NUMMAXHOURLYRATE = ""
                        Else
                            NUMMAXHOURLYRATE = GATVdr2.item("MaxHourlyInputRate")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyRateUnits")) Then
                            NUMMAXHOURLYRATEUNITS = ""
                        Else
                            NUMMAXHOURLYRATEUNITS = GATVdr2.item("MaxHourlyRateUnits")
                        End If
                        NUMMAXHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("AverageFreeMoistureContent")) Then
                            NUMMOISTURECONTENT = ""
                        Else
                            NUMMOISTURECONTENT = GATVdr2.item("AverageFreeMoistureContent")
                        End If

                        SQL = "Insert into AIRTVApplication.PFW_EUS_Material " & _
                        "(PFW_EUS_MATERIALID, EMISSIONUNITID, " & _
                        "GSID, " & _
                        "STRMATERIALNAME, STRMATERIALORIGIN, " & _
                        "NUMMATERIALTYPE, NUMHOURLYRATE, " & _
                        "NUMHOURLYRATEUNITS, NUMMAXHOURLYRATE, " & _
                        "NUMMAXHOURLYRATEUNITS, NUMMAXANNUALINPUT, " & _
                        "NUMMAXANNUALINPUTUNITS, STRCASNUMBER, " & _
                        "NUMPRECENTOFTOTAL, NUMVAPORPRESSURE, " & _
                        "NUMMOISTURECONTENT, NUMVESSELFLOW, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "AIRTVProject.SEQ_GAP_EUS_MATERIALID.nextVal, '" & EUID & "', " & _
                        "'" & GSID & "', " & _
                        "'" & Replace(STRMATERIALNAME, "'", "''") & "', '" & Replace(STRMATERIALORIGIN, "'", "''") & "', " & _
                        "'" & NUMMATERIALTYPE & "', '" & NUMHOURLYRATE & "', " & _
                        "'" & NUMHOURLYRATEUNITS & "', '" & NUMMAXHOURLYRATE & "', " & _
                        "'" & NUMMAXHOURLYRATEUNITS & "', '" & NUMMAXANNUALINPUT & "', " & _
                        "'" & NUMMAXANNUALINPUTUNITS & "', '" & Replace(STRCASNUMBER, "'", "''") & "', " & _
                        "'" & NUMPRECENTOFTOTAL & "', '" & NUMVAPORPRESSURE & "', " & _
                        "'" & NUMMOISTURECONTENT & "', '" & NUMVESSELFLOW & "', " & _
                        "'population event', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & EUID & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While
                    GATVdr2.close()
                Case "Liquid Storage"
                    SQL = "Select " & _
                    "tblEmissionUnitLiqStorageVesselComponent_sub.EquipmentID, " & _
                    "tblEmissionUnitLiqStorageVesselComponent_sub.Component, " & _
                    "tblEmissionUnitLiqStorageVesselComponent_sub.PercentOfMixture, " & _
                    "tblEmissionUnitLiqStorageVesselComponent_sub.ComponentCASNumber, " & _
                    "tblEmissionUnitLiqStorageVesselComponent_sub.ComponentTrueVaporPressure " & _
                    "from tblEmissionUnitLiqStorageVesselComponent_sub " & _
                    "where EquipmentID = " & oldID & " "

                    GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                    GATVcmd = New OleDbCommand(SQL, GATVConn)
                    If GATVConn.State = ConnectionState.Closed Then
                        GATVConn.Open()
                    End If
                    GATVdr2 = GATVcmd.ExecuteReader
                    While GATVdr2.Read
                        STRMATERIALNAME = ""
                        STRMATERIALORIGIN = ""
                        NUMMATERIALTYPE = "1"
                        NUMHOURLYRATE = ""
                        NUMHOURLYRATEUNITS = ""
                        NUMMAXHOURLYRATE = ""
                        NUMMAXHOURLYRATEUNITS = ""
                        NUMMAXANNUALINPUT = ""
                        NUMMAXANNUALINPUTUNITS = ""
                        STRCASNUMBER = ""
                        NUMPRECENTOFTOTAL = ""
                        NUMVAPORPRESSURE = ""
                        NUMMOISTURECONTENT = ""
                        NUMVESSELFLOW = ""

                        If IsDBNull(GATVdr2.item("Component")) Then
                            STRMATERIALNAME = "No Name"
                        Else
                            STRMATERIALNAME = GATVdr2.item("Component")
                        End If
                        If IsDBNull(GATVdr2.item("PercentOfMixture")) Then
                            NUMPRECENTOFTOTAL = ""
                        Else
                            NUMPRECENTOFTOTAL = GATVdr2.item("PercentOfMixture")
                        End If
                        If IsDBNull(GATVdr2.item("ComponentCASNumber")) Then
                            STRCASNUMBER = ""
                        Else
                            STRCASNUMBER = GATVdr2.item("ComponentCASNumber")
                        End If
                        NUMMAXHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("ComponentTrueVaporPressure")) Then
                            NUMVAPORPRESSURE = ""
                        Else
                            NUMVAPORPRESSURE = GATVdr2.item("ComponentTrueVaporPressure")
                        End If

                        SQL = "Insert into AIRTVApplication.PFW_EUS_Material " & _
                        "(PFW_EUS_MATERIALID, EMISSIONUNITID, " & _
                        "GSID, " & _
                        "STRMATERIALNAME, STRMATERIALORIGIN, " & _
                        "NUMMATERIALTYPE, NUMHOURLYRATE, " & _
                        "NUMHOURLYRATEUNITS, NUMMAXHOURLYRATE, " & _
                        "NUMMAXHOURLYRATEUNITS, NUMMAXANNUALINPUT, " & _
                        "NUMMAXANNUALINPUTUNITS, STRCASNUMBER, " & _
                        "NUMPRECENTOFTOTAL, NUMVAPORPRESSURE, " & _
                        "NUMMOISTURECONTENT, NUMVESSELFLOW, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "AIRTVProject.SEQ_GAP_EUS_MATERIALID.nextVal, '" & EUID & "', " & _
                        "'" & GSID & "', " & _
                        "'" & Replace(STRMATERIALNAME, "'", "''") & "', '" & Replace(STRMATERIALORIGIN, "'", "''") & "', " & _
                        "'" & NUMMATERIALTYPE & "', '" & NUMHOURLYRATE & "', " & _
                        "'" & NUMHOURLYRATEUNITS & "', '" & NUMMAXHOURLYRATE & "', " & _
                        "'" & NUMMAXHOURLYRATEUNITS & "', '" & NUMMAXANNUALINPUT & "', " & _
                        "'" & NUMMAXANNUALINPUTUNITS & "', '" & Replace(STRCASNUMBER, "'", "''") & "', " & _
                        "'" & NUMPRECENTOFTOTAL & "', '" & NUMVAPORPRESSURE & "', " & _
                        "'" & NUMMOISTURECONTENT & "', '" & NUMVESSELFLOW & "', " & _
                        "'population event', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & EUID & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While
                    GATVdr2.close()


                Case "Miscellaneous"
                    SQL = "Select " & _
                    "tblEmissionUnitMiscInput_sub.EquipmentID, " & _
                    "tblEmissionUnitMiscInput_sub.Material, " & _
                    "tblEmissionUnitMiscInput_sub.MaxHourlyInputRate, " & _
                    "tblEmissionUnitMiscInput_sub.MaxHourlyRateUnits, " & _
                    "tblEmissionUnitMiscInput_sub.CASNumber,  " & _
                    "tblEmissionUnitMiscInput_sub.MaxAnnualInput,  " & _
                    "tblEmissionUnitMiscInput_sub.MoistureContent  " & _
                    "from tblEmissionUnitMiscInput_sub " & _
                    "where EquipmentID = " & oldID & " "

                    GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                    GATVcmd = New OleDbCommand(SQL, GATVConn)
                    If GATVConn.State = ConnectionState.Closed Then
                        GATVConn.Open()
                    End If
                    GATVdr2 = GATVcmd.ExecuteReader
                    While GATVdr2.Read
                        STRMATERIALNAME = ""
                        STRMATERIALORIGIN = ""
                        NUMMATERIALTYPE = "1"
                        NUMHOURLYRATE = ""
                        NUMHOURLYRATEUNITS = ""
                        NUMMAXHOURLYRATE = ""
                        NUMMAXHOURLYRATEUNITS = ""
                        NUMMAXANNUALINPUT = ""
                        NUMMAXANNUALINPUTUNITS = ""
                        STRCASNUMBER = ""
                        NUMPRECENTOFTOTAL = ""
                        NUMVAPORPRESSURE = ""
                        NUMMOISTURECONTENT = ""
                        NUMVESSELFLOW = ""

                        If IsDBNull(GATVdr2.item("Material")) Then
                            STRMATERIALNAME = "No Name"
                        Else
                            STRMATERIALNAME = GATVdr2.item("Material")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyInputRate")) Then
                            NUMMAXHOURLYRATE = ""
                        Else
                            NUMMAXHOURLYRATE = GATVdr2.item("MaxHourlyInputRate")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyRateUnits")) Then
                            NUMMAXHOURLYRATEUNITS = ""
                        Else
                            NUMMAXHOURLYRATEUNITS = GATVdr2.item("MaxHourlyRateUnits")
                        End If
                        NUMMAXHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("CASNumber")) Then
                            STRCASNUMBER = ""
                        Else
                            STRCASNUMBER = GATVdr2.item("CASNumber")
                        End If
                        If IsDBNull(GATVdr2.item("MaxAnnualInput")) Then
                            NUMMAXANNUALINPUT = ""
                        Else
                            NUMMAXANNUALINPUT = GATVdr2.item("MaxAnnualInput")
                        End If
                        If IsDBNull(GATVdr2.item("MoistureContent")) Then
                            NUMMOISTURECONTENT = ""
                        Else
                            NUMMOISTURECONTENT = GATVdr2.item("MoistureContent")
                        End If

                        SQL = "Insert into AIRTVApplication.PFW_EUS_Material " & _
                        "(PFW_EUS_MATERIALID, EMISSIONUNITID, " & _
                        "GSID, " & _
                        "STRMATERIALNAME, STRMATERIALORIGIN, " & _
                        "NUMMATERIALTYPE, NUMHOURLYRATE, " & _
                        "NUMHOURLYRATEUNITS, NUMMAXHOURLYRATE, " & _
                        "NUMMAXHOURLYRATEUNITS, NUMMAXANNUALINPUT, " & _
                        "NUMMAXANNUALINPUTUNITS, STRCASNUMBER, " & _
                        "NUMPRECENTOFTOTAL, NUMVAPORPRESSURE, " & _
                        "NUMMOISTURECONTENT, NUMVESSELFLOW, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "AIRTVProject.SEQ_GAP_EUS_MATERIALID.nextVal, '" & EUID & "', " & _
                        "'" & GSID & "', " & _
                        "'" & Replace(STRMATERIALNAME, "'", "''") & "', '" & Replace(STRMATERIALORIGIN, "'", "''") & "', " & _
                        "'" & NUMMATERIALTYPE & "', '" & NUMHOURLYRATE & "', " & _
                        "'" & NUMHOURLYRATEUNITS & "', '" & NUMMAXHOURLYRATE & "', " & _
                        "'" & NUMMAXHOURLYRATEUNITS & "', '" & NUMMAXANNUALINPUT & "', " & _
                        "'" & NUMMAXANNUALINPUTUNITS & "', '" & Replace(STRCASNUMBER, "'", "''") & "', " & _
                        "'" & NUMPRECENTOFTOTAL & "', '" & NUMVAPORPRESSURE & "', " & _
                        "'" & NUMMOISTURECONTENT & "', '" & NUMVESSELFLOW & "', " & _
                        "'population event', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & EUID & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While
                    GATVdr2.close()

                    SQL = "Select " & _
                    "tblEmissionUnitMiscOutput_sub.EquipmentID, " & _
                    "tblEmissionUnitMiscOutput_sub.Material, " & _
                    "tblEmissionUnitMiscOutput_sub.MaxHourlyInputRate, " & _
                    "tblEmissionUnitMiscOutput_sub.MaxHourlyRateUnits, " & _
                    "tblEmissionUnitMiscOutput_sub.CASNumber,  " & _
                    "tblEmissionUnitMiscOutput_sub.MaxAnnualInput " & _
                    "from tblEmissionUnitMiscOutput_sub " & _
                    "where EquipmentID = " & oldID & " "

                    'GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                    GATVcmd = New OleDbCommand(SQL, GATVConn)
                    'If GATVConn.State = ConnectionState.Closed Then
                    '    GATVConn.Open()
                    'End If
                    GATVdr2 = GATVcmd.ExecuteReader
                    While GATVdr2.Read
                        STRMATERIALNAME = ""
                        STRMATERIALORIGIN = ""
                        NUMMATERIALTYPE = "2"
                        NUMHOURLYRATE = ""
                        NUMHOURLYRATEUNITS = ""
                        NUMMAXHOURLYRATE = ""
                        NUMMAXHOURLYRATEUNITS = ""
                        NUMMAXANNUALINPUT = ""
                        NUMMAXANNUALINPUTUNITS = ""
                        STRCASNUMBER = ""
                        NUMPRECENTOFTOTAL = ""
                        NUMVAPORPRESSURE = ""
                        NUMMOISTURECONTENT = ""
                        NUMVESSELFLOW = ""

                        If IsDBNull(GATVdr2.item("Material")) Then
                            STRMATERIALNAME = "No Name"
                        Else
                            STRMATERIALNAME = GATVdr2.item("Material")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyInputRate")) Then
                            NUMMAXHOURLYRATE = ""
                        Else
                            NUMMAXHOURLYRATE = GATVdr2.item("MaxHourlyInputRate")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyRateUnits")) Then
                            NUMMAXHOURLYRATEUNITS = ""
                        Else
                            NUMMAXHOURLYRATEUNITS = GATVdr2.item("MaxHourlyRateUnits")
                        End If
                        NUMMAXHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("CASNumber")) Then
                            STRCASNUMBER = ""
                        Else
                            STRCASNUMBER = GATVdr2.item("CASNumber")
                        End If
                        If IsDBNull(GATVdr2.item("MaxAnnualInput")) Then
                            NUMMAXANNUALINPUT = ""
                        Else
                            NUMMAXANNUALINPUT = GATVdr2.item("MaxAnnualInput")
                        End If

                        SQL = "Insert into AIRTVApplication.PFW_EUS_Material " & _
                        "(PFW_EUS_MATERIALID, EMISSIONUNITID, " & _
                        "GSID, " & _
                        "STRMATERIALNAME, STRMATERIALORIGIN, " & _
                        "NUMMATERIALTYPE, NUMHOURLYRATE, " & _
                        "NUMHOURLYRATEUNITS, NUMMAXHOURLYRATE, " & _
                        "NUMMAXHOURLYRATEUNITS, NUMMAXANNUALINPUT, " & _
                        "NUMMAXANNUALINPUTUNITS, STRCASNUMBER, " & _
                        "NUMPRECENTOFTOTAL, NUMVAPORPRESSURE, " & _
                        "NUMMOISTURECONTENT, NUMVESSELFLOW, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "AIRTVProject.SEQ_GAP_EUS_MATERIALID.nextVal, '" & EUID & "', " & _
                        "'" & GSID & "', " & _
                        "'" & Replace(STRMATERIALNAME, "'", "''") & "', '" & Replace(STRMATERIALORIGIN, "'", "''") & "', " & _
                        "'" & NUMMATERIALTYPE & "', '" & NUMHOURLYRATE & "', " & _
                        "'" & NUMHOURLYRATEUNITS & "', '" & NUMMAXHOURLYRATE & "', " & _
                        "'" & NUMMAXHOURLYRATEUNITS & "', '" & NUMMAXANNUALINPUT & "', " & _
                        "'" & NUMMAXANNUALINPUTUNITS & "', '" & Replace(STRCASNUMBER, "'", "''") & "', " & _
                        "'" & NUMPRECENTOFTOTAL & "', '" & NUMVAPORPRESSURE & "', " & _
                        "'" & NUMMOISTURECONTENT & "', '" & NUMVESSELFLOW & "', " & _
                        "'population event', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & EUID & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While
                    GATVdr2.close()

                Case "NonReactive"
                    SQL = "Select " & _
                  "tblEmissionUnitMaterialProcBulkMixing_sub.EquipmentID, " & _
                  "tblEmissionUnitMaterialProcBulkMixing_sub.Material, " & _
                  "tblEmissionUnitMaterialProcBulkMixing_sub.MaxHourlyInputRate, " & _
                  "tblEmissionUnitMaterialProcBulkMixing_sub.MaxHourlyRateUnits, " & _
                  "tblEmissionUnitMaterialProcBulkMixing_sub.CASNumber,  " & _
                  "tblEmissionUnitMaterialProcBulkMixing_sub.MaxAnnualInput " & _
                  "from tblEmissionUnitMaterialProcBulkMixing_sub " & _
                  "where EquipmentID = " & oldID & " "

                    GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                    GATVcmd = New OleDbCommand(SQL, GATVConn)
                    If GATVConn.State = ConnectionState.Closed Then
                        GATVConn.Open()
                    End If
                    GATVdr2 = GATVcmd.ExecuteReader
                    While GATVdr2.Read
                        STRMATERIALNAME = ""
                        STRMATERIALORIGIN = ""
                        NUMMATERIALTYPE = "1"
                        NUMHOURLYRATE = ""
                        NUMHOURLYRATEUNITS = ""
                        NUMMAXHOURLYRATE = ""
                        NUMMAXHOURLYRATEUNITS = ""
                        NUMMAXANNUALINPUT = ""
                        NUMMAXANNUALINPUTUNITS = ""
                        STRCASNUMBER = ""
                        NUMPRECENTOFTOTAL = ""
                        NUMVAPORPRESSURE = ""
                        NUMMOISTURECONTENT = ""
                        NUMVESSELFLOW = ""

                        If IsDBNull(GATVdr2.item("Material")) Then
                            STRMATERIALNAME = "No Name"
                        Else
                            STRMATERIALNAME = GATVdr2.item("Material")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyInputRate")) Then
                            NUMMAXHOURLYRATE = ""
                        Else
                            NUMMAXHOURLYRATE = GATVdr2.item("MaxHourlyInputRate")
                        End If
                        If IsDBNull(GATVdr2.item("MaxHourlyRateUnits")) Then
                            NUMMAXHOURLYRATEUNITS = ""
                        Else
                            NUMMAXHOURLYRATEUNITS = GATVdr2.item("MaxHourlyRateUnits")
                        End If
                        NUMMAXHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("CASNumber")) Then
                            STRCASNUMBER = ""
                        Else
                            STRCASNUMBER = GATVdr2.item("CASNumber")
                        End If
                        If IsDBNull(GATVdr2.item("MaxAnnualInput")) Then
                            NUMMAXANNUALINPUT = ""
                        Else
                            NUMMAXANNUALINPUT = GATVdr2.item("MaxAnnualInput")
                        End If

                        SQL = "Insert into AIRTVApplication.PFW_EUS_Material " & _
                        "(PFW_EUS_MATERIALID, EMISSIONUNITID, " & _
                        "GSID, " & _
                        "STRMATERIALNAME, STRMATERIALORIGIN, " & _
                        "NUMMATERIALTYPE, NUMHOURLYRATE, " & _
                        "NUMHOURLYRATEUNITS, NUMMAXHOURLYRATE, " & _
                        "NUMMAXHOURLYRATEUNITS, NUMMAXANNUALINPUT, " & _
                        "NUMMAXANNUALINPUTUNITS, STRCASNUMBER, " & _
                        "NUMPRECENTOFTOTAL, NUMVAPORPRESSURE, " & _
                        "NUMMOISTURECONTENT, NUMVESSELFLOW, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "AIRTVProject.SEQ_GAP_EUS_MATERIALID.nextVal, '" & EUID & "', " & _
                        "'" & GSID & "', " & _
                        "'" & Replace(STRMATERIALNAME, "'", "''") & "', '" & Replace(STRMATERIALORIGIN, "'", "''") & "', " & _
                        "'" & NUMMATERIALTYPE & "', '" & NUMHOURLYRATE & "', " & _
                        "'" & NUMHOURLYRATEUNITS & "', '" & NUMMAXHOURLYRATE & "', " & _
                        "'" & NUMMAXHOURLYRATEUNITS & "', '" & NUMMAXANNUALINPUT & "', " & _
                        "'" & NUMMAXANNUALINPUTUNITS & "', '" & Replace(STRCASNUMBER, "'", "''") & "', " & _
                        "'" & NUMPRECENTOFTOTAL & "', '" & NUMVAPORPRESSURE & "', " & _
                        "'" & NUMMOISTURECONTENT & "', '" & NUMVESSELFLOW & "', " & _
                        "'population event', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & EUID & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While
                    GATVdr2.close()

                Case "ReactorVessel"
                    SQL = "Select " & _
                   "tblEmissionUnitReactSepInOut.EquipmentID, " & _
                   "tblEmissionUnitReactSepInOut.ReactOrSep, " & _
                   "tblEmissionUnitReactSepInOut.IsInput, " & _
                   "tblEmissionUnitReactSepInOut.NameOfMaterial, " & _
                   "tblEmissionUnitReactSepInOut.CASNumber,  " & _
                   "tblEmissionUnitReactSepInOut.MaterialRate,  " & _
                   "tblEmissionUnitReactSepInOut.MaterialRateUnits  " & _
                   "from tblEmissionUnitReactSepInOut " & _
                   "where EquipmentID = " & oldID & " "


                    GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                    GATVcmd = New OleDbCommand(SQL, GATVConn)
                    If GATVConn.State = ConnectionState.Closed Then
                        GATVConn.Open()
                    End If
                    GATVdr2 = GATVcmd.ExecuteReader
                    While GATVdr2.Read
                        STRMATERIALNAME = ""
                        STRMATERIALORIGIN = ""
                        NUMMATERIALTYPE = "1"
                        NUMHOURLYRATE = ""
                        NUMHOURLYRATEUNITS = ""
                        NUMMAXHOURLYRATE = ""
                        NUMMAXHOURLYRATEUNITS = ""
                        NUMMAXANNUALINPUT = ""
                        NUMMAXANNUALINPUTUNITS = ""
                        STRCASNUMBER = ""
                        NUMPRECENTOFTOTAL = ""
                        NUMVAPORPRESSURE = ""
                        NUMMOISTURECONTENT = ""
                        NUMVESSELFLOW = ""

                        If IsDBNull(GATVdr2.item("IsInput")) Then
                            NUMMATERIALTYPE = "1"
                        Else
                            NUMMATERIALTYPE = GATVdr2.item("IsInput")
                        End If
                        If NUMMATERIALTYPE = False Then
                            NUMMATERIALTYPE = "2"
                        Else
                            NUMMATERIALTYPE = "1"
                        End If

                        If IsDBNull(GATVdr2.item("NameOfMaterial")) Then
                            STRMATERIALNAME = "No Name"
                        Else
                            STRMATERIALNAME = GATVdr2.item("NameOfMaterial")
                        End If
                        If IsDBNull(GATVdr2.item("CASNumber")) Then
                            STRCASNUMBER = ""
                        Else
                            STRCASNUMBER = GATVdr2.item("CASNumber")
                        End If
                        If IsDBNull(GATVdr2.item("MaterialRate")) Then
                            NUMHOURLYRATE = ""
                        Else
                            NUMHOURLYRATE = GATVdr2.item("MaterialRate")
                        End If
                        NUMMAXHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("MaterialRateUnits")) Then
                            NUMHOURLYRATEUNITS = ""
                        Else
                            NUMHOURLYRATEUNITS = GATVdr2.item("MaterialRateUnits")
                        End If
                        NUMHOURLYRATEUNITS = ""

                        SQL = "Insert into AIRTVApplication.PFW_EUS_Material " & _
                        "(PFW_EUS_MATERIALID, EMISSIONUNITID, " & _
                        "GSID, " & _
                        "STRMATERIALNAME, STRMATERIALORIGIN, " & _
                        "NUMMATERIALTYPE, NUMHOURLYRATE, " & _
                        "NUMHOURLYRATEUNITS, NUMMAXHOURLYRATE, " & _
                        "NUMMAXHOURLYRATEUNITS, NUMMAXANNUALINPUT, " & _
                        "NUMMAXANNUALINPUTUNITS, STRCASNUMBER, " & _
                        "NUMPRECENTOFTOTAL, NUMVAPORPRESSURE, " & _
                        "NUMMOISTURECONTENT, NUMVESSELFLOW, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "AIRTVProject.SEQ_GAP_EUS_MATERIALID.nextVal, '" & EUID & "', " & _
                        "'" & GSID & "', " & _
                        "'" & Replace(STRMATERIALNAME, "'", "''") & "', '" & Replace(STRMATERIALORIGIN, "'", "''") & "', " & _
                        "'" & NUMMATERIALTYPE & "', '" & NUMHOURLYRATE & "', " & _
                        "'" & NUMHOURLYRATEUNITS & "', '" & NUMMAXHOURLYRATE & "', " & _
                        "'" & NUMMAXHOURLYRATEUNITS & "', '" & NUMMAXANNUALINPUT & "', " & _
                        "'" & NUMMAXANNUALINPUTUNITS & "', '" & Replace(STRCASNUMBER, "'", "''") & "', " & _
                        "'" & NUMPRECENTOFTOTAL & "', '" & NUMVAPORPRESSURE & "', " & _
                        "'" & NUMMOISTURECONTENT & "', '" & NUMVESSELFLOW & "', " & _
                        "'population event', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & EUID & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While
                    GATVdr2.close()

                Case "Separation"
                    SQL = "Select " & _
                 "tblEmissionUnitReactSepInOut.EquipmentID, " & _
                 "tblEmissionUnitReactSepInOut.ReactOrSep, " & _
                 "tblEmissionUnitReactSepInOut.IsInput, " & _
                 "tblEmissionUnitReactSepInOut.NameOfMaterial, " & _
                 "tblEmissionUnitReactSepInOut.CASNumber,  " & _
                 "tblEmissionUnitReactSepInOut.MaterialRate,  " & _
                 "tblEmissionUnitReactSepInOut.MaterialRateUnits  " & _
                 "from tblEmissionUnitReactSepInOut " & _
                 "where EquipmentID = " & oldID & " "


                    GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                    GATVcmd = New OleDbCommand(SQL, GATVConn)
                    If GATVConn.State = ConnectionState.Closed Then
                        GATVConn.Open()
                    End If
                    GATVdr2 = GATVcmd.ExecuteReader
                    While GATVdr2.Read
                        STRMATERIALNAME = ""
                        STRMATERIALORIGIN = ""
                        NUMMATERIALTYPE = "1"
                        NUMHOURLYRATE = ""
                        NUMHOURLYRATEUNITS = ""
                        NUMMAXHOURLYRATE = ""
                        NUMMAXHOURLYRATEUNITS = ""
                        NUMMAXANNUALINPUT = ""
                        NUMMAXANNUALINPUTUNITS = ""
                        STRCASNUMBER = ""
                        NUMPRECENTOFTOTAL = ""
                        NUMVAPORPRESSURE = ""
                        NUMMOISTURECONTENT = ""
                        NUMVESSELFLOW = ""

                        If IsDBNull(GATVdr2.item("IsInput")) Then
                            NUMMATERIALTYPE = "1"
                        Else
                            NUMMATERIALTYPE = GATVdr2.item("IsInput")
                        End If
                        If NUMMATERIALTYPE = False Then
                            NUMMATERIALTYPE = "2"
                        Else
                            NUMMATERIALTYPE = "1"
                        End If

                        If IsDBNull(GATVdr2.item("NameOfMaterial")) Then
                            STRMATERIALNAME = "No Name"
                        Else
                            STRMATERIALNAME = GATVdr2.item("NameOfMaterial")
                        End If
                        If IsDBNull(GATVdr2.item("CASNumber")) Then
                            STRCASNUMBER = ""
                        Else
                            STRCASNUMBER = GATVdr2.item("CASNumber")
                        End If
                        If IsDBNull(GATVdr2.item("MaterialRate")) Then
                            NUMHOURLYRATE = ""
                        Else
                            NUMHOURLYRATE = GATVdr2.item("MaterialRate")
                        End If
                        NUMMAXHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("MaterialRateUnits")) Then
                            NUMHOURLYRATEUNITS = ""
                        Else
                            NUMHOURLYRATEUNITS = GATVdr2.item("MaterialRateUnits")
                        End If
                        NUMHOURLYRATEUNITS = ""

                        SQL = "Insert into AIRTVApplication.PFW_EUS_Material " & _
                        "(PFW_EUS_MATERIALID, EMISSIONUNITID, " & _
                        "GSID, " & _
                        "STRMATERIALNAME, STRMATERIALORIGIN, " & _
                        "NUMMATERIALTYPE, NUMHOURLYRATE, " & _
                        "NUMHOURLYRATEUNITS, NUMMAXHOURLYRATE, " & _
                        "NUMMAXHOURLYRATEUNITS, NUMMAXANNUALINPUT, " & _
                        "NUMMAXANNUALINPUTUNITS, STRCASNUMBER, " & _
                        "NUMPRECENTOFTOTAL, NUMVAPORPRESSURE, " & _
                        "NUMMOISTURECONTENT, NUMVESSELFLOW, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "AIRTVProject.SEQ_GAP_EUS_MATERIALID.nextVal, '" & EUID & "', " & _
                        "'" & GSID & "', " & _
                        "'" & Replace(STRMATERIALNAME, "'", "''") & "', '" & Replace(STRMATERIALORIGIN, "'", "''") & "', " & _
                        "'" & NUMMATERIALTYPE & "', '" & NUMHOURLYRATE & "', " & _
                        "'" & NUMHOURLYRATEUNITS & "', '" & NUMMAXHOURLYRATE & "', " & _
                        "'" & NUMMAXHOURLYRATEUNITS & "', '" & NUMMAXANNUALINPUT & "', " & _
                        "'" & NUMMAXANNUALINPUTUNITS & "', '" & Replace(STRCASNUMBER, "'", "''") & "', " & _
                        "'" & NUMPRECENTOFTOTAL & "', '" & NUMVAPORPRESSURE & "', " & _
                        "'" & NUMMOISTURECONTENT & "', '" & NUMVESSELFLOW & "', " & _
                        "'population event', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & EUID & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While
                    GATVdr2.close()

                Case "SolidWaste"
                    SQL = "Select " & _
              "tblEmissionUnitSolidLiquidIncinerationMaterials_sub.MaterialBurned, " & _
              "tblEmissionUnitSolidLiquidIncinerationMaterials_sub.MaterialOrigin, " & _
              "tblEmissionUnitSolidLiquidIncinerationMaterials_sub.AverageWeightPercent, " & _
              "tblEmissionUnitSolidLiquidIncinerationMaterials_sub.AverageHeatingValue, " & _
              "tblEmissionUnitSolidLiquidIncinerationMaterials_sub.HeatValueUnit,  " & _
              "tblEmissionUnitSolidLiquidIncinerationMaterials_sub.MaterialType " & _
              "from tblEmissionUnitSolidLiquidIncinerationMaterials_sub " & _
              "where EquipmentID = " & oldID & " "


                    GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
                    GATVcmd = New OleDbCommand(SQL, GATVConn)
                    If GATVConn.State = ConnectionState.Closed Then
                        GATVConn.Open()
                    End If
                    GATVdr2 = GATVcmd.ExecuteReader
                    While GATVdr2.Read
                        STRMATERIALNAME = ""
                        STRMATERIALORIGIN = ""
                        NUMMATERIALTYPE = "1"
                        NUMHOURLYRATE = ""
                        NUMHOURLYRATEUNITS = ""
                        NUMMAXHOURLYRATE = ""
                        NUMMAXHOURLYRATEUNITS = ""
                        NUMMAXANNUALINPUT = ""
                        NUMMAXANNUALINPUTUNITS = ""
                        STRCASNUMBER = ""
                        NUMPRECENTOFTOTAL = ""
                        NUMVAPORPRESSURE = ""
                        NUMMOISTURECONTENT = ""
                        NUMVESSELFLOW = ""

                        If IsDBNull(GATVdr2.item("MaterialBurned")) Then
                            STRMATERIALNAME = "No Name"
                        Else
                            STRMATERIALNAME = GATVdr2.item("MaterialBurned")
                        End If
                        If IsDBNull(GATVdr2.item("MaterialOrigin")) Then
                            STRMATERIALORIGIN = ""
                        Else
                            STRMATERIALORIGIN = GATVdr2.item("MaterialOrigin")
                        End If
                        If IsDBNull(GATVdr2.item("AverageWeightPercent")) Then
                            NUMPRECENTOFTOTAL = ""
                        Else
                            NUMPRECENTOFTOTAL = GATVdr2.item("AverageWeightPercent")
                        End If
                        NUMMAXHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("AverageHeatingValue")) Then
                            NUMHOURLYRATEUNITS = ""
                        Else
                            NUMHOURLYRATEUNITS = GATVdr2.item("AverageHeatingValue")
                        End If
                        NUMHOURLYRATEUNITS = ""
                        If IsDBNull(GATVdr2.item("HeatValueUnit")) Then
                            NUMHOURLYRATEUNITS = ""
                        Else
                            NUMHOURLYRATEUNITS = GATVdr2.item("HeatValueUnit")
                        End If
                        NUMHOURLYRATEUNITS = ""

                        'If IsDBNull(GATVdr2.item("MaterialType")) Then
                        '    NUMHOURLYRATEUNITS = ""
                        'Else
                        '    NUMHOURLYRATEUNITS = GATVdr2.item("MaterialType")
                        'End If

                        SQL = "Insert into AIRTVApplication.PFW_EUS_Material " & _
                        "(PFW_EUS_MATERIALID, EMISSIONUNITID, " & _
                        "GSID, " & _
                        "STRMATERIALNAME, STRMATERIALORIGIN, " & _
                        "NUMMATERIALTYPE, NUMHOURLYRATE, " & _
                        "NUMHOURLYRATEUNITS, NUMMAXHOURLYRATE, " & _
                        "NUMMAXHOURLYRATEUNITS, NUMMAXANNUALINPUT, " & _
                        "NUMMAXANNUALINPUTUNITS, STRCASNUMBER, " & _
                        "NUMPRECENTOFTOTAL, NUMVAPORPRESSURE, " & _
                        "NUMMOISTURECONTENT, NUMVESSELFLOW, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "AIRTVProject.SEQ_GAP_EUS_MATERIALID.nextVal, '" & EUID & "', " & _
                        "'" & GSID & "', " & _
                        "'" & Replace(STRMATERIALNAME, "'", "''") & "', '" & Replace(STRMATERIALORIGIN, "'", "''") & "', " & _
                        "'" & NUMMATERIALTYPE & "', '" & NUMHOURLYRATE & "', " & _
                        "'" & NUMHOURLYRATEUNITS & "', '" & NUMMAXHOURLYRATE & "', " & _
                        "'" & NUMMAXHOURLYRATEUNITS & "', '" & NUMMAXANNUALINPUT & "', " & _
                        "'" & NUMMAXANNUALINPUTUNITS & "', '" & Replace(STRCASNUMBER, "'", "''") & "', " & _
                        "'" & NUMPRECENTOFTOTAL & "', '" & NUMVAPORPRESSURE & "', " & _
                        "'" & NUMMOISTURECONTENT & "', '" & NUMVESSELFLOW & "', " & _
                        "'population event', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                        "where EMISSIONUNITID = " & EUID & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While
                    GATVdr2.close()


                Case Else

            End Select
            

          

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            '            MsgBox("done")
        End Try
    End Sub
    Sub LandFillSub(ByVal oldID As String, ByVal GSID As String, ByVal EUID As String, ByVal EUType As String)
        Try
            Dim NUMYEARRECEIVED As String = ""
            Dim NUMTONSACCEPTED As String = ""
            Dim NUMESTIMATEDVALUE As String = ""
            Dim STRWASTEHANDLINGPERMIT As String = ""
            Dim STRORIGINALISSUEDATE As String = ""
            Dim NUMMAXDESIGNCAPACITY As String = ""

            SQL = "Select " & _
            "tblEmissionUnitLandFillPermits_Sub.SolidWasteHandlingPermitNumber, " & _
            "tblEmissionUnitLandFillPermits_Sub.OriginalIssueOrAmendmentDate, " & _
            "tblEmissionUnitLandFillPermits_Sub.MaximumPermittedDesignCapacity " & _
            "from tblEmissionUnitLandFillPermits_Sub " & _
             "where EquipmentID = " & oldID & " "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr2 = GATVcmd.ExecuteReader
            While GATVdr2.Read
                STRWASTEHANDLINGPERMIT = ""
                STRORIGINALISSUEDATE = ""
                NUMMAXDESIGNCAPACITY = ""

                If IsDBNull(GATVdr2.item("SolidWasteHandlingPermitNumber")) Then
                    STRWASTEHANDLINGPERMIT = ""
                Else
                    STRWASTEHANDLINGPERMIT = GATVdr2.item("SolidWasteHandlingPermitNumber")
                End If
                If IsDBNull(GATVdr2.item("OriginalIssueOrAmendmentDate")) Then
                    STRORIGINALISSUEDATE = ""
                Else
                    STRORIGINALISSUEDATE = GATVdr2.item("OriginalIssueOrAmendmentDate")
                End If
                If IsDBNull(GATVdr2.item("MaximumPermittedDesignCapacity")) Then
                    NUMMAXDESIGNCAPACITY = ""
                Else
                    NUMMAXDESIGNCAPACITY = GATVdr2.item("MaximumPermittedDesignCapacity")
                End If

                SQL = "Insert into AIRTVApplication.PFW_EUS_LandFillPermit " & _
                "(PFW_EUS_LANDFILLPERMITID, EMISSIONUNITID, " & _
                "GSID, " & _
                "STRWASTEHANDLINGPERMIT, STRORIGINALISSUEDATE, " & _
                "NUMMAXDESIGNCAPACITY, " & _
                "COMMENTS, ACTIVE) " & _
                "Select " & _
                "AIRTVProject.SEQ_GAP_EUS_LANDFILLPERMITID.nextVal, '" & EUID & "', " & _
                "'" & GSID & "', " & _
                "'" & Replace(STRWASTEHANDLINGPERMIT, "'", "''") & "', '" & Replace(STRORIGINALISSUEDATE, "'", "''") & "', " & _
                "'" & NUMMAXDESIGNCAPACITY & "',  " & _
                "'population event', '1' " & _
                "from dual " & _
                "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                "where EMISSIONUNITID = " & EUID & ") "

                cmd2 = New OracleCommand(SQL, ConnTVApplication)
                If ConnTVApplication.State = ConnectionState.Closed Then
                    ConnTVApplication.Open()
                End If
                dr2 = cmd2.ExecuteReader
                dr2.Close()
            End While
            GATVdr2.close()

            SQL = "Select " & _
         "tblEmissionUnitLandFillAnnualDepsoit_sub.Year, " & _
         "tblEmissionUnitLandFillAnnualDepsoit_sub.TonsSolidWasteAccepted, " & _
         "tblEmissionUnitLandFillAnnualDepsoit_sub.IsEstimate " & _
         "from tblEmissionUnitLandFillAnnualDepsoit_sub " & _
          "where EquipmentID = " & oldID & " "

            'GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            'If GATVConn.State = ConnectionState.Closed Then
            '    GATVConn.Open()
            'End If
            GATVdr2 = GATVcmd.ExecuteReader
            While GATVdr2.Read
                NUMYEARRECEIVED = ""
                NUMTONSACCEPTED = ""
                NUMESTIMATEDVALUE = ""

                If IsDBNull(GATVdr2.item("Year")) Then
                    NUMYEARRECEIVED = ""
                Else
                    NUMYEARRECEIVED = GATVdr2.item("Year")
                End If
                NUMYEARRECEIVED = Replace(NUMYEARRECEIVED, " ", "")

                If IsDBNull(GATVdr2.item("TonsSolidWasteAccepted")) Then
                    NUMTONSACCEPTED = ""
                Else
                    NUMTONSACCEPTED = GATVdr2.item("TonsSolidWasteAccepted")
                End If
                If IsDBNull(GATVdr2.item("IsEstimate")) Then
                    NUMESTIMATEDVALUE = ""
                Else
                    NUMESTIMATEDVALUE = GATVdr2.item("IsEstimate")
                End If
                If NUMESTIMATEDVALUE = True Then
                    NUMESTIMATEDVALUE = "1"
                Else
                    NUMESTIMATEDVALUE = "0"
                End If

                If NUMYEARRECEIVED <> "" And NUMTONSACCEPTED <> "" And NUMESTIMATEDVALUE <> "" Then
                    SQL = "Insert into AIRTVApplication.PFW_EUS_LandFilldeposit " & _
                    "(PFW_EUS_LANDFILLdepositID, EMISSIONUNITID, " & _
                    "GSID, " & _
                    "NUMYEARRECEIVED, NUMTONSACCEPTED, " & _
                    "NUMESTIMATEDVALUE, " & _
                    "COMMENTS, ACTIVE) " & _
                    "Select " & _
                    "AIRTVProject.SEQ_GAP_EUS_LANDFILLDepositID.nextVal, '" & EUID & "', " & _
                    "'" & GSID & "', " & _
                    "'" & Replace(NUMYEARRECEIVED, "'", "''") & "', '" & Replace(NUMTONSACCEPTED, "'", "''") & "', " & _
                    "'" & NUMESTIMATEDVALUE & "',  " & _
                    "'population event', '1' " & _
                    "from dual " & _
                    "where exists (select * from PFW_EMISSIONUNITMASTER " & _
                    "where EMISSIONUNITID = " & EUID & ") "

                    cmd2 = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            GATVdr2.close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            '            MsgBox("done")
        End Try

    End Sub

    
    Private Sub btnPFWCDAdsorber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWCDAdsorber.Click
        PopulatePFWCDAdsorber()
    End Sub
    Sub PopulatePFWCDAdsorber()
        Try
            Dim ProjectID As String = ""
            Dim oldID As String = ""
            Dim EUID As String = ""
            Dim GSID As String = ""
            Dim CUName As String = ""
            Dim Comments As String = ""

            Dim STRDESC As String = ""
            Dim STRMANUFACTURER As String = ""
            Dim STRMODELNUMBER As String = ""
            Dim DATMANUFACTURE As String = ""
            Dim DATINSTALLATION As String = ""
            Dim STRINSTALLATION As String = ""
            Dim OPERATINGSTATUS As String = "O"
            Dim DATOPERATINGSTATUS As String = ""
            Dim CONTROLUNITTYPE As String = "1"
            Dim OPERATINGREASON As String = ""
            Dim STROPERATINGREASONOTHER As String = ""
            Dim STRPARAMETERSMONITORED As String = ""
           
            Dim NUMRESIDENCETIME As String = ""
            Dim ADSORBENT As String = ""
            Dim STRADSORBENTOTHER As String = ""
            Dim NUMREGENERATIONCYCLE As String = ""
            Dim NUMREGENERATIONUNITS As String = ""
            Dim REGENERATIONTYPE As String = ""
            Dim STRREGENERATIONTYPEOTHER As String = ""
            Dim NUMBREAKTHROUGHMONITOR As String = ""
            Dim NUMMONITORSETTING As String = ""
            Dim NUMMONITORCONCENTRATION As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblControlAdsorbers.ProjectIdentifier as ProjectID,  " & _
            "tblControlAdsorbers.EquipmentID,  " & _
            "tblControlAdsorbers.Description, " & _
            "tblControlAdsorbers.Manufacturer, " & _
            "tblControlAdsorbers.ModelNumber, " & _
            "tblControlAdsorbers.DateManufactured_Reconstructed, " & _
            "tblControlAdsorbers.InstallationDate, " & _
            "tblControlAdsorbers.PollutantControlledID, " & _
            "tblControlAdsorbers.OverallControlEff, " & _
            "tblControlAdsorbers.ResidenceTime, " & _
            "tblControlAdsorbers.BreakthroughMonitor , " & _
            "tblControlAdsorbers.SettingForBreakMon , " & _
            "tblControlAdsorbers.SettingUnits , " & _
            "tblControlAdsorbers.BreakthroughConc , " & _
            "tblControlAdsorbers.BreakthroughConcUnits , " & _
            "tblControlAdsorbers.AdsorbentUsed , " & _
            "tblControlAdsorbers.AdsorbentUsedOther , " & _
            "tblControlAdsorbers.RegenerationType , " & _
            "tblControlAdsorbers.RegenerationTypeOther , " & _
            "tblControlAdsorbers.RegenerationCycle , " & _
            "tblControlAdsorbers.RegenerationCycleUnits , " & _
            "tblControlAdsorbers.ParametersMonitored , " & _
            "tblControlAdsorbers.ControlReason , " & _
            "tblControlAdsorbers.ControlReasonOther , " & _
            "tblControlAdsorbers.Comments " & _
            "FROM tblControlAdsorbers INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblControlAdsorbers.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblControlAdsorbers.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectId = ""
                If IsDBNull(GATVdr.item("ProjectID")) Then
                    ProjectID = ""
                Else
                    ProjectID = GATVdr.item("ProjectID")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If

                If IsDBNull(GATVdr.item("ResidenceTime")) Then
                    NUMRESIDENCETIME = ""
                Else
                    NUMRESIDENCETIME = GATVdr.item("ResidenceTime")
                End If
                If IsDBNull(GATVdr.item("AdsorbentUsed")) Then
                    ADSORBENT = ""
                Else
                    ADSORBENT = GATVdr.item("AdsorbentUsed")
                End If
               
                If IsDBNull(GATVdr.item("AdsorbentUsedOther")) Then
                    STRADSORBENTOTHER = ""
                Else
                    STRADSORBENTOTHER = GATVdr.item("AdsorbentUsedOther")
                End If
                If IsDBNull(GATVdr.item("RegenerationCycle")) Then
                    NUMREGENERATIONCYCLE = ""
                Else
                    NUMREGENERATIONCYCLE = GATVdr.item("RegenerationCycle")
                End If
                If IsDBNull(GATVdr.item("RegenerationCycleUnits")) Then
                    NUMREGENERATIONUNITS = ""
                Else
                    NUMREGENERATIONUNITS = GATVdr.item("RegenerationCycleUnits")
                End If
                NUMREGENERATIONUNITS = ""
                If IsDBNull(GATVdr.item("RegenerationType")) Then
                    REGENERATIONTYPE = ""
                Else
                    REGENERATIONTYPE = GATVdr.item("RegenerationType")
                End If
                If IsDBNull(GATVdr.item("RegenerationTypeOther")) Then
                    STRREGENERATIONTYPEOTHER = ""
                Else
                    STRREGENERATIONTYPEOTHER = GATVdr.item("RegenerationTypeOther")
                End If
                If IsDBNull(GATVdr.item("BreakthroughMonitor")) Then
                    NUMBREAKTHROUGHMONITOR = ""
                Else
                    NUMBREAKTHROUGHMONITOR = GATVdr.item("BreakthroughMonitor")
                End If
                If NUMBREAKTHROUGHMONITOR = False Then
                    NUMBREAKTHROUGHMONITOR = "0"
                Else
                    NUMBREAKTHROUGHMONITOR = "1"
                End If
                If IsDBNull(GATVdr.item("BreakthroughMonitor")) Then
                    NUMMONITORSETTING = ""
                Else
                    NUMMONITORSETTING = GATVdr.item("BreakthroughMonitor")
                End If
                If NUMMONITORSETTING = False Then
                    NUMMONITORSETTING = "0"
                Else
                    NUMMONITORSETTING = "1"
                End If
                If IsDBNull(GATVdr.item("BreakthroughConc")) Then
                    NUMMONITORCONCENTRATION = ""
                Else
                    NUMMONITORCONCENTRATION = GATVdr.item("BreakthroughConc")
                End If
                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    CUName = ""
                Else
                    CUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    STRDESC = "None given"
                Else
                    STRDESC = GATVdr.item("EquipmentDescription")
                End If

                If IsDBNull(GATVdr.item("ControlReason")) Then
                    OPERATINGREASON = "None given"
                Else
                    OPERATINGREASON = GATVdr.item("ControlReason")
                End If
                If IsDBNull(GATVdr.item("ControlReasonOther")) Then
                    STROPERATINGREASONOTHER = "None given"
                Else
                    STROPERATINGREASONOTHER = GATVdr.item("ControlReasonOther")
                End If

                If IsDBNull(GATVdr.item("ParametersMonitored")) Then
                    STRPARAMETERSMONITORED = "None given"
                Else
                    STRPARAMETERSMONITORED = GATVdr.item("ParametersMonitored")
                End If

                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    STRMANUFACTURER = ""
                Else
                    STRMANUFACTURER = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    STRMODELNUMBER = ""
                Else
                    STRMODELNUMBER = GATVdr.item("ModelNumber")
                End If
                STRINSTALLATION = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DATMANUFACTURE = ""
                    STRINSTALLATION = ""
                Else
                    DATMANUFACTURE = GATVdr.item("DateManufactured_Reconstructed")
                    STRINSTALLATION = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DATMANUFACTURE) And DATMANUFACTURE.Length = 4 Then
                    DATMANUFACTURE = "01-Jan-" & DATMANUFACTURE
                End If

                If IsDate(DATMANUFACTURE) Then
                    DATMANUFACTURE = Format(CDate(DATMANUFACTURE), "dd-MMM-yyyy")
                Else
                    DATMANUFACTURE = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    DATINSTALLATION = ""
                    STRINSTALLATION = STRINSTALLATION
                Else
                    DATINSTALLATION = GATVdr.item("InstallationDate")
                    STRINSTALLATION = STRINSTALLATION & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(DATINSTALLATION) And DATINSTALLATION.Length = 4 Then
                    DATINSTALLATION = "01-Jan-" & DATINSTALLATION
                End If
                If IsDate(DATINSTALLATION) Then
                    DATINSTALLATION = Format(CDate(DATINSTALLATION), "dd-MMM-yyyy")
                Else
                    DATINSTALLATION = ""
                End If
                STRINSTALLATION = Mid(STRINSTALLATION, 1, 400)

                If ProjectId <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectId & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUnitMaster " & _
                        "(CONTROLUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUnitID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_ControlUnitID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITID " & _
                       "(CONTROLUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(CUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_ControlUNITMASTER " & _
                       "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITHEADER " & _
                        "(ControlUNITHEADERID, ControlUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, ControlUNITTYPE , " & _
                        "OperatingReason, strOperatingReasonOther, " & _
                        "STRPARAMETERSMONITORED, " & _
                        "COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(STRDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(STRMANUFACTURER, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(STRMODELNUMBER, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DATMANUFACTURE, "'", "''") & "', '" & Replace(DATINSTALLATION, "'", "''") & "', " & _
                        "'" & Replace(STRINSTALLATION, "'", "''") & "', 'O', " & _
                        "sysdate, '1', " & _
                        "'" & OPERATINGREASON & "', '" & Replace(STROPERATINGREASONOTHER, "'", "''") & "', " & _
                        "'" & Replace(STRPARAMETERSMONITORED, "'", "''") & "', " & _
                        "'Initail Populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_CD_Adsorber " & _
                        "(PFW_CD_ADSORBERID, CONTROLUNITID, " & _
                        "GSID, NUMRESIDENCETIME, " & _
                        "ADSORBENT, STRADSORBENTOTHER, " & _
                        "NUMREGENERATIONCYCLE, NUMREGENERATIONUNITS, " & _
                        "REGENERATIONTYPE, STRREGENERATIONTYPEOTHER, " & _
                        "NUMBREAKTHROUGHMONITOR, NUMMONITORSETTING, " & _
                        "NUMMONITORCONCENTRATION, COMMENTS, " & _
                        "ACTIVE) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_CD_AdsorberID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(NUMRESIDENCETIME, "'", "''") & "', '" & Replace(ADSORBENT, "'", "''") & "', " & _
                        "'" & Replace(STRADSORBENTOTHER, "'", "''") & "', '" & Replace(NUMREGENERATIONCYCLE, "'", "''") & "', " & _
                        "'" & NUMREGENERATIONUNITS & "', " & _
                        "'" & Replace(REGENERATIONTYPE, "'", "''") & "', '" & Replace(STRREGENERATIONTYPEOTHER, "'", "''") & "', " & _
                        "'" & NUMBREAKTHROUGHMONITOR & "', '" & NUMMONITORSETTING & "', " & _
                        "'" & NUMMONITORCONCENTRATION & "', " & _
                         "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnPFWCDBioFiltration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWCDBioFiltration.Click
        PopulatePFWCDBioFiltration()
    End Sub
    Sub PopulatePFWCDBioFiltration()
        Try
            Dim ProjectID As String = ""
            Dim oldID As String = ""
            Dim EUID As String = ""
            Dim GSID As String = ""
            Dim CUName As String = ""
            Dim Comments As String = ""

            Dim STRDESC As String = ""
            Dim STRMANUFACTURER As String = ""
            Dim STRMODELNUMBER As String = ""
            Dim DATMANUFACTURE As String = ""
            Dim DATINSTALLATION As String = ""
            Dim STRINSTALLATION As String = ""
            Dim OPERATINGSTATUS As String = "O"
            Dim DATOPERATINGSTATUS As String = ""
            Dim CONTROLUNITTYPE As String = "1"
            Dim OPERATINGREASON As String = ""
            Dim STROPERATINGREASONOTHER As String = ""
            Dim STRPARAMETERSMONITORED As String = ""

            Dim NUMINLETLOADING As String = ""
            Dim NUMINLETLOADINGUNITS As String = ""
            Dim NUMEXITLOADING As String = ""
            Dim NUMEXITLOADINGUNITS As String = ""
            Dim NUMINLETGASTEMP As String = ""
            Dim NUMINLETGASTEMPUNITS As String = ""
            Dim NUMWATERFLOWRATE As Integer
            Dim NUMWATERFLOWRATEUNITS As String = ""
            Dim NUMMEDIABEDTEMP As String = ""
            Dim NUMMEDIABEDTEMPUNITS As String = ""
            Dim NUMMEDIABEDHUMIDITY As String = ""
            Dim NUMINLETRELATIVEHUMIDITY As String = ""
            Dim NUMPRESSUREDROP As String = ""
            Dim NUMMEDIABEDPH As String = ""
            Dim STRNUTRIENTDESC As String = ""
            Dim NUMNUTRIENTADDRATE As String = ""
            Dim NUMNUTRIENTADDRATEUNITS As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblControlBioFilters.ProjectIdentifier as ProjectID,  " & _
            "tblControlBioFilters.EquipmentID,  " & _
            "tblControlBioFilters.Description, " & _
            "tblControlBioFilters.Manufacturer, " & _
            "tblControlBioFilters.ModelNumber, " & _
            "tblControlBioFilters.DateManufactured_Reconstructed, " & _
            "tblControlBioFilters.InstallationDate, " & _
            "tblControlBioFilters.PollutantControlledID, " & _
            "tblControlBioFilters.OverallControlEff, " & _
            "tblControlBioFilters.PressureDrop, " & _
            "tblControlBioFilters.InletLoading , " & _
            "tblControlBioFilters.InletLoadingUnits , " & _
            "tblControlBioFilters.ExitLoading , " & _
            "tblControlBioFilters.ExitLoadingUnits , " & _
            "tblControlBioFilters.InletGasTemp , " & _
            "tblControlBioFilters.InletGasTempUnits , " & _
            "tblControlBioFilters.WaterFlow , " & _
            "tblControlBioFilters.WaterFlowUnits , " & _
            "tblControlBioFilters.BedTemp , " & _
            "tblControlBioFilters.[Bed Temp Units] as BedTempUnits, " & _
            "tblControlBioFilters.BedHumidity , " & _
            "tblControlBioFilters.InRelativeHumidity , " & _
            "tblControlBioFilters.NutrientDesc , " & _
            "tblControlBioFilters.NutrientAddRate , " & _
            "tblControlBioFilters.NutrientAddRateUnits , " & _
            "tblControlBioFilters.BedpH , " & _
            "tblControlBioFilters.ParametersMonitored , " & _
            "tblControlBioFilters.ControlReason, " & _
            "tblControlBioFilters.ControlReasonOther, " & _
            "tblControlBioFilters.Comments " & _
            "FROM tblControlBioFilters INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblControlBioFilters.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblControlBioFilters.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectID = ""
                If IsDBNull(GATVdr.item("ProjectID")) Then
                    ProjectID = ""
                Else
                    ProjectID = GATVdr.item("ProjectID")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If

                If IsDBNull(GATVdr.item("InletLoading")) Then
                    NUMINLETLOADING = ""
                Else
                    NUMINLETLOADING = GATVdr.item("InletLoading")
                End If
                If IsNumeric(NUMINLETLOADING) Then
                Else
                    NUMINLETLOADING = ""
                End If
                If IsDBNull(GATVdr.item("InletLoadingUnits")) Then
                    NUMINLETLOADINGUNITS = ""
                Else
                    NUMINLETLOADINGUNITS = GATVdr.item("InletLoadingUnits")
                End If
                NUMINLETLOADINGUNITS = ""

                If IsDBNull(GATVdr.item("ExitLoading")) Then
                    NUMEXITLOADING = ""
                Else
                    NUMEXITLOADING = GATVdr.item("ExitLoading")
                End If
                If IsNumeric(NUMEXITLOADING) Then
                Else
                    NUMEXITLOADING = ""
                End If
                If IsDBNull(GATVdr.item("ExitLoadingUnits")) Then
                    NUMEXITLOADINGUNITS = ""
                Else
                    NUMEXITLOADINGUNITS = GATVdr.item("ExitLoadingUnits")
                End If
                NUMEXITLOADINGUNITS = ""

                If IsDBNull(GATVdr.item("InletGasTemp")) Then
                    NUMINLETGASTEMP = ""
                Else
                    NUMINLETGASTEMP = GATVdr.item("InletGasTemp")
                End If
                If IsNumeric(NUMINLETGASTEMP) Then
                Else
                    NUMINLETGASTEMP = ""
                End If

                If IsDBNull(GATVdr.item("InletGasTempUnits")) Then
                    NUMINLETGASTEMPUNITS = ""
                Else
                    NUMINLETGASTEMPUNITS = GATVdr.item("InletGasTempUnits")
                End If
                NUMINLETGASTEMPUNITS = ""

                If IsDBNull(GATVdr.item("WaterFlow")) Then
                    NUMWATERFLOWRATE = ""
                Else
                    NUMWATERFLOWRATE = GATVdr.item("WaterFlow")
                End If
                If IsNumeric(NUMWATERFLOWRATE) Then
                Else
                    NUMWATERFLOWRATE = ""
                End If
                If IsDBNull(GATVdr.item("WaterFlowUnits")) Then
                    NUMWATERFLOWRATEUNITS = ""
                Else
                    NUMWATERFLOWRATEUNITS = GATVdr.item("WaterFlowUnits")
                End If
                NUMWATERFLOWRATEUNITS = ""

                If IsDBNull(GATVdr.item("BedTemp")) Then
                    NUMMEDIABEDTEMP = ""
                Else
                    NUMMEDIABEDTEMP = GATVdr.item("BedTemp")
                End If
                If IsNumeric(NUMMEDIABEDTEMP) Then
                Else
                    NUMMEDIABEDTEMP = ""
                End If
                If IsDBNull(GATVdr.item("BedHumidity")) Then
                    NUMMEDIABEDHUMIDITY = ""
                Else
                    NUMMEDIABEDHUMIDITY = GATVdr.item("BedHumidity")
                End If
                If IsNumeric(NUMMEDIABEDHUMIDITY) Then
                Else
                    NUMMEDIABEDHUMIDITY = ""
                End If
                If IsDBNull(GATVdr.item("InRelativeHumidity")) Then
                    NUMINLETRELATIVEHUMIDITY = ""
                Else
                    NUMINLETRELATIVEHUMIDITY = GATVdr.item("InRelativeHumidity")
                End If
                If IsNumeric(NUMINLETRELATIVEHUMIDITY) Then
                Else
                    NUMINLETRELATIVEHUMIDITY = ""
                End If
                If IsDBNull(GATVdr.item("PressureDrop")) Then
                    NUMPRESSUREDROP = ""
                Else
                    NUMPRESSUREDROP = GATVdr.item("PressureDrop")
                End If
                If IsNumeric(NUMPRESSUREDROP) Then
                Else
                    NUMPRESSUREDROP = ""
                End If
                If IsDBNull(GATVdr.item("BedpH")) Then
                    NUMMEDIABEDPH = ""
                Else
                    NUMMEDIABEDPH = GATVdr.item("BedpH")
                End If
                If IsNumeric(NUMMEDIABEDPH) Then
                Else
                    NUMMEDIABEDPH = ""
                End If
                If IsDBNull(GATVdr.item("NutrientDesc")) Then
                    STRNUTRIENTDESC = ""
                Else
                    STRNUTRIENTDESC = GATVdr.item("NutrientDesc")
                End If
                If IsDBNull(GATVdr.item("NutrientAddRate")) Then
                    NUMNUTRIENTADDRATE = ""
                Else
                    NUMNUTRIENTADDRATE = GATVdr.item("NutrientAddRate")
                End If
                If IsNumeric(NUMNUTRIENTADDRATE) Then
                Else
                    NUMNUTRIENTADDRATE = ""
                End If
                If IsDBNull(GATVdr.item("NutrientAddRateUnits")) Then
                    NUMNUTRIENTADDRATEUNITS = ""
                Else
                    NUMNUTRIENTADDRATEUNITS = GATVdr.item("NutrientAddRateUnits")
                End If
                NUMNUTRIENTADDRATEUNITS = ""

                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    CUName = ""
                Else
                    CUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    STRDESC = "None given"
                Else
                    STRDESC = GATVdr.item("EquipmentDescription")
                End If

                If IsDBNull(GATVdr.item("ControlReason")) Then
                    OPERATINGREASON = "None given"
                Else
                    OPERATINGREASON = GATVdr.item("ControlReason")
                End If
                If IsDBNull(GATVdr.item("ControlReasonOther")) Then
                    STROPERATINGREASONOTHER = "None given"
                Else
                    STROPERATINGREASONOTHER = GATVdr.item("ControlReasonOther")
                End If

                If IsDBNull(GATVdr.item("ParametersMonitored")) Then
                    STRPARAMETERSMONITORED = "None given"
                Else
                    STRPARAMETERSMONITORED = GATVdr.item("ParametersMonitored")
                End If

                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    STRMANUFACTURER = ""
                Else
                    STRMANUFACTURER = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    STRMODELNUMBER = ""
                Else
                    STRMODELNUMBER = GATVdr.item("ModelNumber")
                End If
                STRINSTALLATION = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DATMANUFACTURE = ""
                    STRINSTALLATION = ""
                Else
                    DATMANUFACTURE = GATVdr.item("DateManufactured_Reconstructed")
                    STRINSTALLATION = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DATMANUFACTURE) And DATMANUFACTURE.Length = 4 Then
                    DATMANUFACTURE = "01-Jan-" & DATMANUFACTURE
                End If

                If IsDate(DATMANUFACTURE) Then
                    DATMANUFACTURE = Format(CDate(DATMANUFACTURE), "dd-MMM-yyyy")
                Else
                    DATMANUFACTURE = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    DATINSTALLATION = ""
                    STRINSTALLATION = STRINSTALLATION
                Else
                    DATINSTALLATION = GATVdr.item("InstallationDate")
                    STRINSTALLATION = STRINSTALLATION & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(DATINSTALLATION) And DATINSTALLATION.Length = 4 Then
                    DATINSTALLATION = "01-Jan-" & DATINSTALLATION
                End If
                If IsDate(DATINSTALLATION) Then
                    DATINSTALLATION = Format(CDate(DATINSTALLATION), "dd-MMM-yyyy")
                Else
                    DATINSTALLATION = ""
                End If
                STRINSTALLATION = Mid(STRINSTALLATION, 1, 400)

                If ProjectID <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectID & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUnitMaster " & _
                        "(CONTROLUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUnitID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_ControlUnitID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITID " & _
                       "(CONTROLUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(CUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_ControlUNITMASTER " & _
                       "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITHEADER " & _
                        "(ControlUNITHEADERID, ControlUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, ControlUNITTYPE , " & _
                        "OperatingReason, strOperatingReasonOther, " & _
                        "STRPARAMETERSMONITORED, " & _
                        "COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(STRDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(STRMANUFACTURER, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(STRMODELNUMBER, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DATMANUFACTURE, "'", "''") & "', '" & Replace(DATINSTALLATION, "'", "''") & "', " & _
                        "'" & Replace(STRINSTALLATION, "'", "''") & "', 'O', " & _
                        "sysdate, '2', " & _
                        "'" & OPERATINGREASON & "', '" & Replace(STROPERATINGREASONOTHER, "'", "''") & "', " & _
                        "'" & Replace(STRPARAMETERSMONITORED, "'", "''") & "', " & _
                        "'Initail Populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_CD_BioFiltration " & _
                        "(PFW_CD_BioFiltrationID, CONTROLUNITID, " & _
                        "GSID,  " & _
                        "NUMINLETLOADING, NUMINLETLOADINGUNITS, " & _
                        "NUMEXITLOADING, NUMEXITLOADINGUNITS, " & _
                        "NUMINLETGASTEMP, NUMINLETGASTEMPUNITS, " & _
                        "NUMWATERFLOWRATE, NUMWATERFLOWRATEUNITS, " & _
                        "NUMMEDIABEDTEMP, NUMMEDIABEDTEMPUNITS, " & _
                        "NUMMEDIABEDHUMIDITY, NUMINLETRELATIVEHUMIDITY, " & _
                        "NUMPRESSUREDROP, NUMMEDIABEDPH, " & _
                        "STRNUTRIENTDESC, NUMNUTRIENTADDRATE, " & _
                        "NUMNUTRIENTADDRATEUNITS, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_CD_BioFiltrationID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & NUMINLETLOADING & "', '" & NUMINLETLOADINGUNITS & "', " & _
                        "'" & NUMEXITLOADING & "', '" & NUMEXITLOADINGUNITS & "', " & _
                        "'" & NUMINLETGASTEMP & "', '" & NUMINLETGASTEMPUNITS & "', " & _
                        "'" & NUMWATERFLOWRATE & "', '" & NUMWATERFLOWRATEUNITS & "', " & _
                        "'" & NUMMEDIABEDTEMP & "', '" & NUMMEDIABEDTEMPUNITS & "', " & _
                        "'" & NUMMEDIABEDHUMIDITY & "', '" & NUMINLETRELATIVEHUMIDITY & "', " & _
                        "'" & NUMPRESSUREDROP & "', '" & NUMMEDIABEDPH & "', " & _
                        "'" & Replace(STRNUTRIENTDESC, "'", "''") & "', '" & NUMNUTRIENTADDRATE & "', " & _
                        "'" & NUMNUTRIENTADDRATEUNITS & "', " & _
                         "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnPFWCDCondenser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWCDCondenser.Click
        PopulatePFWCDCondenser()
    End Sub
    Sub PopulatePFWCDCondenser()
        Try
            Dim ProjectID As String = ""
            Dim oldID As String = ""
            Dim EUID As String = ""
            Dim GSID As String = ""
            Dim CUName As String = ""
            Dim Comments As String = ""

            Dim STRDESC As String = ""
            Dim STRMANUFACTURER As String = ""
            Dim STRMODELNUMBER As String = ""
            Dim DATMANUFACTURE As String = ""
            Dim DATINSTALLATION As String = ""
            Dim STRINSTALLATION As String = ""
            Dim OPERATINGSTATUS As String = "O"
            Dim DATOPERATINGSTATUS As String = ""
            Dim CONTROLUNITTYPE As String = "1"
            Dim OPERATINGREASON As String = ""
            Dim STROPERATINGREASONOTHER As String = ""
            Dim STRPARAMETERSMONITORED As String = ""

            Dim NUMEXITGASTEMP As String = ""
            Dim NUMEXITGASTEMPUNITS As String = ""
            Dim STRCOOLINGMEDIADESC As String = ""
            Dim NUMCOOLINGMEDIATEMPIN As String = ""
            Dim NUMCOOLINGMEDIATEMPINUNITS As String = ""
            Dim NUMCOOLINGMEDIATEMPOUT As String = ""
            Dim NUMCOOLINGMEDIATEMPOUTUNITS As String = ""
            Dim NUMCOOLINGMEDIAFLOWRATE As String = ""
            Dim NUMCOOLINGMEDIAFLOWRATEUNITS As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblControlCondenser.ProjectIdentifier as ProjectID,  " & _
            "tblControlCondenser.EquipmentID,  " & _
            "tblControlCondenser.Description, " & _
            "tblControlCondenser.Manufacturer, " & _
            "tblControlCondenser.ModelNumber, " & _
            "tblControlCondenser.DateManufactured_Reconstructed, " & _
            "tblControlCondenser.InstallationDate, " & _
            "tblControlCondenser.PollutantControlledID, " & _
            "tblControlCondenser.OverallControlEff, " & _
            "tblControlCondenser.ExitGasTemp, " & _
            "tblControlCondenser.ExitGasTempUnits , " & _
            "tblControlCondenser.CoolingMedia , " & _
            "tblControlCondenser.CoolMediaTempIn , " & _
            "tblControlCondenser.CoolMediaTempInUnits , " & _
            "tblControlCondenser.CoolMediaTempOut , " & _
            "tblControlCondenser.CoolMediaTempOutUnits , " & _
            "tblControlCondenser.CoolMediaFlow , " & _
            "tblControlCondenser.CoolMediaFlowUnits , " & _
            "tblControlCondenser.ParametersMonitored , " & _
            "tblControlCondenser.ControlReason, " & _
            "tblControlCondenser.ControlReasonOther, " & _
            "tblControlCondenser.Comments " & _
            "FROM tblControlCondenser INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblControlCondenser.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblControlCondenser.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectID = ""
                If IsDBNull(GATVdr.item("ProjectID")) Then
                    ProjectID = ""
                Else
                    ProjectID = GATVdr.item("ProjectID")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If

                If IsDBNull(GATVdr.item("ExitGasTemp")) Then
                    NUMEXITGASTEMP = ""
                Else
                    NUMEXITGASTEMP = GATVdr.item("ExitGasTemp")
                End If
                If IsDBNull(GATVdr.item("ExitGasTempUnits")) Then
                    NUMEXITGASTEMPUNITS = ""
                Else
                    NUMEXITGASTEMPUNITS = GATVdr.item("ExitGasTempUnits")
                End If
                NUMEXITGASTEMPUNITS = ""
                If IsDBNull(GATVdr.item("CoolingMedia")) Then
                    STRCOOLINGMEDIADESC = ""
                Else
                    STRCOOLINGMEDIADESC = GATVdr.item("CoolingMedia")
                End If
                If IsDBNull(GATVdr.item("CoolMediaTempIn")) Then
                    NUMCOOLINGMEDIATEMPIN = ""
                Else
                    NUMCOOLINGMEDIATEMPIN = GATVdr.item("CoolMediaTempIn")
                End If
                If IsDBNull(GATVdr.item("CoolMediaTempInUnits")) Then
                    NUMCOOLINGMEDIATEMPINUNITS = ""
                Else
                    NUMCOOLINGMEDIATEMPINUNITS = GATVdr.item("CoolMediaTempInUnits")
                End If
                NUMCOOLINGMEDIATEMPINUNITS = ""
                If IsDBNull(GATVdr.item("CoolMediaTempOut")) Then
                    NUMCOOLINGMEDIATEMPOUT = ""
                Else
                    NUMCOOLINGMEDIATEMPOUT = GATVdr.item("CoolMediaTempOut")
                End If
                If IsDBNull(GATVdr.item("CoolMediaTempOutUnits")) Then
                    NUMCOOLINGMEDIATEMPOUTUNITS = ""
                Else
                    NUMCOOLINGMEDIATEMPOUTUNITS = GATVdr.item("CoolMediaTempOutUnits")
                End If
                NUMCOOLINGMEDIATEMPOUTUNITS = ""
                If IsDBNull(GATVdr.item("CoolMediaFlow")) Then
                    NUMCOOLINGMEDIAFLOWRATE = ""
                Else
                    NUMCOOLINGMEDIAFLOWRATE = GATVdr.item("CoolMediaFlow")
                End If
                If IsDBNull(GATVdr.item("CoolMediaFlowUnits")) Then
                    NUMCOOLINGMEDIAFLOWRATEUNITS = ""
                Else
                    NUMCOOLINGMEDIAFLOWRATEUNITS = GATVdr.item("CoolMediaFlowUnits")
                End If
                NUMCOOLINGMEDIAFLOWRATEUNITS = ""

                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    CUName = ""
                Else
                    CUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    STRDESC = "None given"
                Else
                    STRDESC = GATVdr.item("EquipmentDescription")
                End If

                If IsDBNull(GATVdr.item("ControlReason")) Then
                    OPERATINGREASON = "None given"
                Else
                    OPERATINGREASON = GATVdr.item("ControlReason")
                End If
                If IsDBNull(GATVdr.item("ControlReasonOther")) Then
                    STROPERATINGREASONOTHER = "None given"
                Else
                    STROPERATINGREASONOTHER = GATVdr.item("ControlReasonOther")
                End If

                If IsDBNull(GATVdr.item("ParametersMonitored")) Then
                    STRPARAMETERSMONITORED = "None given"
                Else
                    STRPARAMETERSMONITORED = GATVdr.item("ParametersMonitored")
                End If

                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    STRMANUFACTURER = ""
                Else
                    STRMANUFACTURER = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    STRMODELNUMBER = ""
                Else
                    STRMODELNUMBER = GATVdr.item("ModelNumber")
                End If
                STRINSTALLATION = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DATMANUFACTURE = ""
                    STRINSTALLATION = ""
                Else
                    DATMANUFACTURE = GATVdr.item("DateManufactured_Reconstructed")
                    STRINSTALLATION = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DATMANUFACTURE) And DATMANUFACTURE.Length = 4 Then
                    DATMANUFACTURE = "01-Jan-" & DATMANUFACTURE
                End If

                If IsDate(DATMANUFACTURE) Then
                    DATMANUFACTURE = Format(CDate(DATMANUFACTURE), "dd-MMM-yyyy")
                Else
                    DATMANUFACTURE = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    DATINSTALLATION = ""
                    STRINSTALLATION = STRINSTALLATION
                Else
                    DATINSTALLATION = GATVdr.item("InstallationDate")
                    STRINSTALLATION = STRINSTALLATION & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(DATINSTALLATION) And DATINSTALLATION.Length = 4 Then
                    DATINSTALLATION = "01-Jan-" & DATINSTALLATION
                End If
                If IsDate(DATINSTALLATION) Then
                    DATINSTALLATION = Format(CDate(DATINSTALLATION), "dd-MMM-yyyy")
                Else
                    DATINSTALLATION = ""
                End If
                STRINSTALLATION = Mid(STRINSTALLATION, 1, 400)

                If ProjectID <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectID & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUnitMaster " & _
                        "(CONTROLUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUnitID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_ControlUnitID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITID " & _
                       "(CONTROLUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(CUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_ControlUNITMASTER " & _
                       "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITHEADER " & _
                        "(ControlUNITHEADERID, ControlUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, ControlUNITTYPE , " & _
                        "OperatingReason, strOperatingReasonOther, " & _
                        "STRPARAMETERSMONITORED, " & _
                        "COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(STRDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(STRMANUFACTURER, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(STRMODELNUMBER, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DATMANUFACTURE, "'", "''") & "', '" & Replace(DATINSTALLATION, "'", "''") & "', " & _
                        "'" & Replace(STRINSTALLATION, "'", "''") & "', 'O', " & _
                        "sysdate, '3', " & _
                        "'" & OPERATINGREASON & "', '" & Replace(STROPERATINGREASONOTHER, "'", "''") & "', " & _
                        "'" & Replace(STRPARAMETERSMONITORED, "'", "''") & "', " & _
                        "'Initail Populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_CD_condenser " & _
                        "(PFW_CD_CondenserID, CONTROLUNITID, " & _
                        "GSID,  " & _
                        "NUMEXITGASTEMP, NUMEXITGASTEMPUNITS, " & _
                        "STRCOOLINGMEDIADESC, NUMCOOLINGMEDIATEMPIN, " & _
                        "NUMCOOLINGMEDIATEMPINUNITS, NUMCOOLINGMEDIATEMPOUT, " & _
                        "NUMCOOLINGMEDIATEMPOUTUNITS, NUMCOOLINGMEDIAFLOWRATE, " & _
                        "NUMCOOLINGMEDIAFLOWRATEUNITS, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_CD_CondenserID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & NUMEXITGASTEMP & "', '" & NUMEXITGASTEMPUNITS & "', " & _
                        "'" & Replace(STRCOOLINGMEDIADESC, "'", "''") & "', '" & NUMCOOLINGMEDIATEMPIN & "', " & _
                        "'" & NUMCOOLINGMEDIATEMPINUNITS & "', '" & NUMCOOLINGMEDIATEMPOUT & "', " & _
                        "'" & NUMCOOLINGMEDIATEMPOUTUNITS & "', '" & NUMCOOLINGMEDIAFLOWRATE & "',  " & _
                        "'" & NUMCOOLINGMEDIAFLOWRATEUNITS & "', " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnPFWCDCyclone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWCDCyclone.Click
        PopulatePFWCDCyclone()
    End Sub
    Sub PopulatePFWCDCyclone()
        Try
            Dim ProjectID As String = ""
            Dim oldID As String = ""
            Dim EUID As String = ""
            Dim GSID As String = ""
            Dim CUName As String = ""
            Dim Comments As String = ""

            Dim STRDESC As String = ""
            Dim STRMANUFACTURER As String = ""
            Dim STRMODELNUMBER As String = ""
            Dim DATMANUFACTURE As String = ""
            Dim DATINSTALLATION As String = ""
            Dim STRINSTALLATION As String = ""
            Dim OPERATINGSTATUS As String = "O"
            Dim DATOPERATINGSTATUS As String = ""
            Dim CONTROLUNITTYPE As String = "1"
            Dim OPERATINGREASON As String = ""
            Dim STROPERATINGREASONOTHER As String = ""
            Dim STRPARAMETERSMONITORED As String = ""

            Dim NUMPRESSUREDROP As String = ""
            Dim NUMSIDESTREAMSEPARATOR As String = ""
            Dim NUMSIDESTREAMFLOWRATE As String = ""
            Dim NUMSIDESTREAMFLOWRATEUNITS As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblControlCyclone.ProjectIdentifier as ProjectID,  " & _
            "tblControlCyclone.EquipmentID,  " & _
            "tblControlCyclone.Description, " & _
            "tblControlCyclone.Manufacturer, " & _
            "tblControlCyclone.ModelNumber, " & _
            "tblControlCyclone.DateManufactured_Reconstructed, " & _
            "tblControlCyclone.InstallationDate, " & _
            "tblControlCyclone.PollutantControlledID, " & _
            "tblControlCyclone.OverallControlEff, " & _
            "tblControlCyclone.PressureDrop, " & _
            "tblControlCyclone.InGasVelo, " & _
            "tblControlCyclone.InGasVeloUnits, " & _
            "tblControlCyclone.SideStreamSep, " & _
            "tblControlCyclone.SideStreamVelo, " & _
            "tblControlCyclone.SideStreamVeloUnits, " & _
            "tblControlCyclone.ParametersMonitored , " & _
            "tblControlCyclone.ControlReason, " & _
            "tblControlCyclone.ControlReasonOther, " & _
            "tblControlCyclone.Comments " & _
            "FROM tblControlCyclone INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblControlCyclone.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblControlCyclone.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectID = ""
                If IsDBNull(GATVdr.item("ProjectID")) Then
                    ProjectID = ""
                Else
                    ProjectID = GATVdr.item("ProjectID")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If

                If IsDBNull(GATVdr.item("PressureDrop")) Then
                    NUMPRESSUREDROP = ""
                Else
                    NUMPRESSUREDROP = GATVdr.item("PressureDrop")
                End If
                If IsDBNull(GATVdr.item("SideStreamSep")) Then
                    NUMSIDESTREAMSEPARATOR = ""
                Else
                    NUMSIDESTREAMSEPARATOR = GATVdr.item("SideStreamSep")
                End If
                If NUMSIDESTREAMSEPARATOR = False Then
                    NUMSIDESTREAMSEPARATOR = "0"
                Else
                    NUMSIDESTREAMSEPARATOR = "1"
                End If
                If IsDBNull(GATVdr.item("SideStreamVelo")) Then
                    NUMSIDESTREAMFLOWRATE = ""
                Else
                    NUMSIDESTREAMFLOWRATE = GATVdr.item("SideStreamVelo")
                End If
                If IsDBNull(GATVdr.item("SideStreamVeloUnits")) Then
                    NUMSIDESTREAMFLOWRATEUNITS = ""
                Else
                    NUMSIDESTREAMFLOWRATEUNITS = GATVdr.item("SideStreamVeloUnits")
                End If
                NUMSIDESTREAMFLOWRATEUNITS = ""

                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    CUName = ""
                Else
                    CUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    STRDESC = "None given"
                Else
                    STRDESC = GATVdr.item("EquipmentDescription")
                End If

                If IsDBNull(GATVdr.item("ControlReason")) Then
                    OPERATINGREASON = "None given"
                Else
                    OPERATINGREASON = GATVdr.item("ControlReason")
                End If
                If IsDBNull(GATVdr.item("ControlReasonOther")) Then
                    STROPERATINGREASONOTHER = "None given"
                Else
                    STROPERATINGREASONOTHER = GATVdr.item("ControlReasonOther")
                End If

                If IsDBNull(GATVdr.item("ParametersMonitored")) Then
                    STRPARAMETERSMONITORED = "None given"
                Else
                    STRPARAMETERSMONITORED = GATVdr.item("ParametersMonitored")
                End If

                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    STRMANUFACTURER = ""
                Else
                    STRMANUFACTURER = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    STRMODELNUMBER = ""
                Else
                    STRMODELNUMBER = GATVdr.item("ModelNumber")
                End If
                STRINSTALLATION = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DATMANUFACTURE = ""
                    STRINSTALLATION = ""
                Else
                    DATMANUFACTURE = GATVdr.item("DateManufactured_Reconstructed")
                    STRINSTALLATION = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DATMANUFACTURE) And DATMANUFACTURE.Length = 4 Then
                    DATMANUFACTURE = "01-Jan-" & DATMANUFACTURE
                End If

                If IsDate(DATMANUFACTURE) Then
                    DATMANUFACTURE = Format(CDate(DATMANUFACTURE), "dd-MMM-yyyy")
                Else
                    DATMANUFACTURE = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    DATINSTALLATION = ""
                    STRINSTALLATION = STRINSTALLATION
                Else
                    DATINSTALLATION = GATVdr.item("InstallationDate")
                    STRINSTALLATION = STRINSTALLATION & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(DATINSTALLATION) And DATINSTALLATION.Length = 4 Then
                    DATINSTALLATION = "01-Jan-" & DATINSTALLATION
                End If
                If IsDate(DATINSTALLATION) Then
                    DATINSTALLATION = Format(CDate(DATINSTALLATION), "dd-MMM-yyyy")
                Else
                    DATINSTALLATION = ""
                End If
                STRINSTALLATION = Mid(STRINSTALLATION, 1, 400)

                If ProjectID <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectID & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUnitMaster " & _
                        "(CONTROLUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUnitID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_ControlUnitID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITID " & _
                       "(CONTROLUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(CUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_ControlUNITMASTER " & _
                       "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITHEADER " & _
                        "(ControlUNITHEADERID, ControlUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, ControlUNITTYPE , " & _
                        "OperatingReason, strOperatingReasonOther, " & _
                        "STRPARAMETERSMONITORED, " & _
                        "COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(STRDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(STRMANUFACTURER, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(STRMODELNUMBER, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DATMANUFACTURE, "'", "''") & "', '" & Replace(DATINSTALLATION, "'", "''") & "', " & _
                        "'" & Replace(STRINSTALLATION, "'", "''") & "', 'O', " & _
                        "sysdate, '4', " & _
                        "'" & OPERATINGREASON & "', '" & Replace(STROPERATINGREASONOTHER, "'", "''") & "', " & _
                        "'" & Replace(STRPARAMETERSMONITORED, "'", "''") & "', " & _
                        "'Initail Populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_CD_cyclone " & _
                        "(PFW_CD_CycloneID, CONTROLUNITID, " & _
                        "GSID,  " & _
                        "NUMPRESSUREDROP, NUMSIDESTREAMSEPARATOR, " & _
                        "NUMSIDESTREAMFLOWRATE, NUMSIDESTREAMFLOWRATEUNITS, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_CD_CycloneID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & NUMPRESSUREDROP & "', '" & NUMSIDESTREAMSEPARATOR & "', " & _
                        "'" & NUMSIDESTREAMFLOWRATE & "', '" & NUMSIDESTREAMFLOWRATEUNITS & "', " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnPFWCDESP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWCDESP.Click
        PopulatePFWCDESP()
    End Sub
    Sub PopulatePFWCDESP()
        Try
            Dim ProjectID As String = ""
            Dim oldID As String = ""
            Dim EUID As String = ""
            Dim GSID As String = ""
            Dim CUName As String = ""
            Dim Comments As String = ""

            Dim STRDESC As String = ""
            Dim STRMANUFACTURER As String = ""
            Dim STRMODELNUMBER As String = ""
            Dim DATMANUFACTURE As String = ""
            Dim DATINSTALLATION As String = ""
            Dim STRINSTALLATION As String = ""
            Dim OPERATINGSTATUS As String = "O"
            Dim DATOPERATINGSTATUS As String = ""
            Dim CONTROLUNITTYPE As String = "1"
            Dim OPERATINGREASON As String = ""
            Dim STROPERATINGREASONOTHER As String = ""
            Dim STRPARAMETERSMONITORED As String = ""

            Dim NUMTYPEOFESP As String = ""
            Dim NUMESPFIELDCOUNT As String = ""
            Dim NUMPRIMARYVOLTAGE As String = ""
            Dim NUMPRIMARYAMPERAGE As String = ""
            Dim NUMSECONDARYVOLTAGE As String = ""
            Dim NUMSECONDARYAMPERAGE As String = ""
            Dim NUMSPARKRATE As String = ""
            Dim NUMINLETGASVELOCITY As String = ""
            Dim NUMINLETGASVELOCITYUNITS As String = ""
            Dim NUMWATERFLOWRATE As String = ""
            Dim NUMWATERFLOWRATEUNITS As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblControlESP.ProjectIdentifier as ProjectID,  " & _
            "tblControlESP.EquipmentID,  " & _
            "tblControlESP.Description, " & _
            "tblControlESP.Manufacturer, " & _
            "tblControlESP.ModelNumber, " & _
            "tblControlESP.DateManufactured_Reconstructed, " & _
            "tblControlESP.InstallationDate, " & _
            "tblControlESP.PollutantControlledID, " & _
            "tblControlESP.OverallControlEff, " & _
            "tblControlESP.InletGasVelo, " & _
            "tblControlESP.InletGasVeloUnits, " & _
            "tblControlESP.TypeOfESP, " & _
            "tblControlESP.NumberFields, " & _
            "tblControlESP.PrimaryVoltage, " & _
            "tblControlESP.PrimaryAmp, " & _
            "tblControlESP.PreipVoltage , " & _
            "tblControlESP.PrecipAmp, " & _
            "tblControlESP.SparkRate, " & _
            "tblControlESP.WaterFlow, " & _
            "tblControlESP.WaterFlowUnits, " & _
            "tblControlESP.ParametersMonitored, " & _
            "tblControlESP.ControlReason, " & _
            "tblControlESP.ControlReasonOther, " & _
            "tblControlESP.Comments " & _
            "FROM tblControlESP INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblControlESP.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblControlESP.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectID = ""
                If IsDBNull(GATVdr.item("ProjectID")) Then
                    ProjectID = ""
                Else
                    ProjectID = GATVdr.item("ProjectID")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If

                If IsDBNull(GATVdr.item("TypeOfESP")) Then
                    NUMTYPEOFESP = ""
                Else
                    NUMTYPEOFESP = GATVdr.item("TypeOfESP")
                End If
                If IsDBNull(GATVdr.item("NumberFields")) Then
                    NUMESPFIELDCOUNT = ""
                Else
                    NUMESPFIELDCOUNT = GATVdr.item("NumberFields")
                End If
                If IsDBNull(GATVdr.item("PrimaryVoltage")) Then
                    NUMPRIMARYVOLTAGE = ""
                Else
                    NUMPRIMARYVOLTAGE = GATVdr.item("PrimaryVoltage")
                End If

                If IsDBNull(GATVdr.item("PrimaryAmp")) Then
                    NUMPRIMARYAMPERAGE = ""
                Else
                    NUMPRIMARYAMPERAGE = GATVdr.item("PrimaryAmp")
                End If
                If IsDBNull(GATVdr.item("PreipVoltage")) Then
                    NUMSECONDARYVOLTAGE = ""
                Else
                    NUMSECONDARYVOLTAGE = GATVdr.item("PreipVoltage")
                End If
                If IsDBNull(GATVdr.item("PrecipAmp")) Then
                    NUMSECONDARYAMPERAGE = ""
                Else
                    NUMSECONDARYAMPERAGE = GATVdr.item("PrecipAmp")
                End If

                If IsDBNull(GATVdr.item("SparkRate")) Then
                    NUMSPARKRATE = ""
                Else
                    NUMSPARKRATE = GATVdr.item("SparkRate")
                End If
                If IsDBNull(GATVdr.item("InletGasVelo")) Then
                    NUMINLETGASVELOCITY = ""
                Else
                    NUMINLETGASVELOCITY = GATVdr.item("InletGasVelo")
                End If
                If IsDBNull(GATVdr.item("InletGasVeloUnits")) Then
                    NUMINLETGASVELOCITYUNITS = ""
                Else
                    NUMINLETGASVELOCITYUNITS = GATVdr.item("InletGasVeloUnits")
                End If
                NUMINLETGASVELOCITYUNITS = ""

                If IsDBNull(GATVdr.item("WaterFlow")) Then
                    NUMWATERFLOWRATE = ""
                Else
                    NUMWATERFLOWRATE = GATVdr.item("WaterFlow")
                End If
                If IsDBNull(GATVdr.item("WaterFlowUnits")) Then
                    NUMWATERFLOWRATEUNITS = ""
                Else
                    NUMWATERFLOWRATEUNITS = GATVdr.item("WaterFlowUnits")
                End If
                NUMWATERFLOWRATEUNITS = ""

                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    CUName = ""
                Else
                    CUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    STRDESC = "None given"
                Else
                    STRDESC = GATVdr.item("EquipmentDescription")
                End If

                If IsDBNull(GATVdr.item("ControlReason")) Then
                    OPERATINGREASON = "None given"
                Else
                    OPERATINGREASON = GATVdr.item("ControlReason")
                End If
                If IsDBNull(GATVdr.item("ControlReasonOther")) Then
                    STROPERATINGREASONOTHER = "None given"
                Else
                    STROPERATINGREASONOTHER = GATVdr.item("ControlReasonOther")
                End If

                If IsDBNull(GATVdr.item("ParametersMonitored")) Then
                    STRPARAMETERSMONITORED = "None given"
                Else
                    STRPARAMETERSMONITORED = GATVdr.item("ParametersMonitored")
                End If

                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    STRMANUFACTURER = ""
                Else
                    STRMANUFACTURER = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    STRMODELNUMBER = ""
                Else
                    STRMODELNUMBER = GATVdr.item("ModelNumber")
                End If
                STRINSTALLATION = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DATMANUFACTURE = ""
                    STRINSTALLATION = ""
                Else
                    DATMANUFACTURE = GATVdr.item("DateManufactured_Reconstructed")
                    STRINSTALLATION = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DATMANUFACTURE) And DATMANUFACTURE.Length = 4 Then
                    DATMANUFACTURE = "01-Jan-" & DATMANUFACTURE
                End If

                If IsDate(DATMANUFACTURE) Then
                    DATMANUFACTURE = Format(CDate(DATMANUFACTURE), "dd-MMM-yyyy")
                Else
                    DATMANUFACTURE = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    DATINSTALLATION = ""
                    STRINSTALLATION = STRINSTALLATION
                Else
                    DATINSTALLATION = GATVdr.item("InstallationDate")
                    STRINSTALLATION = STRINSTALLATION & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(DATINSTALLATION) And DATINSTALLATION.Length = 4 Then
                    DATINSTALLATION = "01-Jan-" & DATINSTALLATION
                End If
                If IsDate(DATINSTALLATION) Then
                    DATINSTALLATION = Format(CDate(DATINSTALLATION), "dd-MMM-yyyy")
                Else
                    DATINSTALLATION = ""
                End If
                STRINSTALLATION = Mid(STRINSTALLATION, 1, 400)

                If ProjectID <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectID & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUnitMaster " & _
                        "(CONTROLUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUnitID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_ControlUnitID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITID " & _
                       "(CONTROLUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(CUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_ControlUNITMASTER " & _
                       "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITHEADER " & _
                        "(ControlUNITHEADERID, ControlUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, ControlUNITTYPE , " & _
                        "OperatingReason, strOperatingReasonOther, " & _
                        "STRPARAMETERSMONITORED, " & _
                        "COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(STRDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(STRMANUFACTURER, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(STRMODELNUMBER, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DATMANUFACTURE, "'", "''") & "', '" & Replace(DATINSTALLATION, "'", "''") & "', " & _
                        "'" & Replace(STRINSTALLATION, "'", "''") & "', 'O', " & _
                        "sysdate, '5', " & _
                        "'" & OPERATINGREASON & "', '" & Replace(STROPERATINGREASONOTHER, "'", "''") & "', " & _
                        "'" & Replace(STRPARAMETERSMONITORED, "'", "''") & "', " & _
                        "'Initail Populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_CD_ESP " & _
                        "(PFW_CD_ESPID, CONTROLUNITID, " & _
                        "GSID,  " & _
                        "NUMTYPEOFESP, NUMESPFIELDCOUNT, " & _
                        "NUMPRIMARYVOLTAGE, NUMPRIMARYAMPERAGE, " & _
                        "NUMSECONDARYVOLTAGE, NUMSECONDARYAMPERAGE, " & _
                        "NUMSPARKRATE, NUMINLETGASVELOCITY, " & _
                        "NUMINLETGASVELOCITYUNITS, NUMWATERFLOWRATE, " & _
                        "NUMWATERFLOWRATEUNITS, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_CD_ESPID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & NUMTYPEOFESP & "', '" & NUMESPFIELDCOUNT & " ', " & _
                        "'" & NUMPRIMARYVOLTAGE & "', '" & NUMPRIMARYAMPERAGE & "', " & _
                        "'" & NUMSECONDARYVOLTAGE & "', '" & NUMSECONDARYAMPERAGE & "', " & _
                        "'" & NUMSPARKRATE & "', '" & NUMINLETGASVELOCITY & "', " & _
                        "'" & NUMINLETGASVELOCITYUNITS & "', '" & NUMWATERFLOWRATE & "', " & _
                        "'" & NUMWATERFLOWRATEUNITS & "', " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnPFWCDFilterMedia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWCDFilterMedia.Click
        PopulatePFWCDFilterMedia()
    End Sub
    Sub PopulatePFWCDFilterMedia()
        Try
            Dim ProjectID As String = ""
            Dim oldID As String = ""
            Dim EUID As String = ""
            Dim GSID As String = ""
            Dim CUName As String = ""
            Dim Comments As String = ""

            Dim STRDESC As String = ""
            Dim STRMANUFACTURER As String = ""
            Dim STRMODELNUMBER As String = ""
            Dim DATMANUFACTURE As String = ""
            Dim DATINSTALLATION As String = ""
            Dim STRINSTALLATION As String = ""
            Dim OPERATINGSTATUS As String = "O"
            Dim DATOPERATINGSTATUS As String = ""
            Dim CONTROLUNITTYPE As String = "1"
            Dim OPERATINGREASON As String = ""
            Dim STROPERATINGREASONOTHER As String = ""
            Dim STRPARAMETERSMONITORED As String = ""

            Dim FILTERMEDIA As String = ""
            Dim STRFILTERMEDIAOTHER As String = ""
            Dim NUMDISPOSABLE As String = ""
            Dim NUMINLETDEWPOINTTEMP As String = ""
            Dim NUMINLETDEWPOINTTEMPUNITS As String = ""
            Dim NUMINLETGASTEMP As String = ""
            Dim NUMINLETGASTEMPUNITS As String = ""
            Dim NUMPRESSUREDROP As String = ""
            Dim NUMBAGCOUNT As String = ""
            Dim NUMFILTEROPLIFE As String = ""
            Dim NUMFILTEROPLIFEUNITS As String = ""
            Dim NUMFILTERAREA As String = ""
            Dim NUMFILTERREPLACESCHED As String = ""
            Dim NUMFILTERREPLACESCHEDUNITS As String = ""
            Dim CLEANINGMETHOD As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblControlFilters.ProjectIdentifier as ProjectID,  " & _
            "tblControlFilters.EquipmentID,  " & _
            "tblControlFilters.Description, " & _
            "tblControlFilters.Manufacturer, " & _
            "tblControlFilters.ModelNumber, " & _
            "tblControlFilters.DateManufactured_Reconstructed, " & _
            "tblControlFilters.InstallationDate, " & _
            "tblControlFilters.PollutantControlledID, " & _
            "tblControlFilters.OverallControlEff, " & _
            "tblControlFilters.PressureDrop, " & _
            "tblControlFilters.NumberOfBags, " & _
            "tblControlFilters.FilterOpLife, " & _
            "tblControlFilters.FilterOpLifeUnits, " & _
            "tblControlFilters.InletDewTemp, " & _
            "tblControlFilters.InletDewTempUnits, " & _
            "tblControlFilters.InletGasTemp, " & _
            "tblControlFilters.InletGasTempUnits, " & _
            "tblControlFilters.FilterCleaningMethod, " & _
            "tblControlFilters.FilterCleaningSchedule, " & _
            "tblControlFilters.FilterCleaningScheduleUnits, " & _
            "tblControlFilters.FilterReplacementDesc, " & _
            "tblControlFilters.FilterReplaceScheule, " & _
            "tblControlFilters.FilterReplaceScheuleUnits, " & _
            "tblControlFilters.FilterArea, " & _
            "tblControlFilters.Disposable, " & _
            "tblControlFilters.ParametersMonitored, " & _
            "tblControlFilters.ControlReason, " & _
            "tblControlFilters.ControlReasonOther, " & _
            "tblControlFilters.Comments " & _
            "FROM tblControlFilters INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblControlFilters.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblControlFilters.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectID = ""
                If IsDBNull(GATVdr.item("ProjectID")) Then
                    ProjectID = ""
                Else
                    ProjectID = GATVdr.item("ProjectID")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If

                FILTERMEDIA = "4"

                If IsDBNull(GATVdr.item("Disposable")) Then
                    NUMDISPOSABLE = ""
                Else
                    NUMDISPOSABLE = GATVdr.item("Disposable")
                End If
                If NUMDISPOSABLE = False Then
                    NUMDISPOSABLE = 0
                Else
                    NUMDISPOSABLE = 1
                End If
                If IsDBNull(GATVdr.item("InletDewTemp")) Then
                    NUMINLETDEWPOINTTEMP = ""
                Else
                    NUMINLETDEWPOINTTEMP = GATVdr.item("InletDewTemp")
                End If

                If IsDBNull(GATVdr.item("InletDewTempUnits")) Then
                    NUMINLETDEWPOINTTEMPUNITS = ""
                Else
                    NUMINLETDEWPOINTTEMPUNITS = GATVdr.item("InletDewTempUnits")
                End If
                NUMINLETDEWPOINTTEMPUNITS = ""

                If IsDBNull(GATVdr.item("InletGasTemp")) Then
                    NUMINLETGASTEMP = ""
                Else
                    NUMINLETGASTEMP = GATVdr.item("InletGasTemp")
                End If
                If IsDBNull(GATVdr.item("InletGasTempUnits")) Then
                    NUMINLETGASTEMPUNITS = ""
                Else
                    NUMINLETGASTEMPUNITS = GATVdr.item("InletGasTempUnits")
                End If
                NUMINLETGASTEMPUNITS = ""

                If IsDBNull(GATVdr.item("PressureDrop")) Then
                    NUMPRESSUREDROP = ""
                Else
                    NUMPRESSUREDROP = GATVdr.item("PressureDrop")
                End If
                If IsDBNull(GATVdr.item("NumberOfBags")) Then
                    NUMBAGCOUNT = ""
                Else
                    NUMBAGCOUNT = GATVdr.item("NumberOfBags")
                End If
                If IsDBNull(GATVdr.item("FilterOpLife")) Then
                    NUMFILTEROPLIFE = ""
                Else
                    NUMFILTEROPLIFE = GATVdr.item("FilterOpLife")
                End If
                If IsDBNull(GATVdr.item("FilterOpLifeUnits")) Then
                    NUMFILTEROPLIFEUNITS = ""
                Else
                    NUMFILTEROPLIFEUNITS = GATVdr.item("FilterOpLifeUnits")
                End If
                NUMFILTEROPLIFEUNITS = ""
                If IsDBNull(GATVdr.item("FilterArea")) Then
                    NUMFILTERAREA = ""
                Else
                    NUMFILTERAREA = GATVdr.item("FilterArea")
                End If
                If IsDBNull(GATVdr.item("FilterReplaceScheule")) Then
                    NUMFILTERREPLACESCHED = ""
                Else
                    NUMFILTERREPLACESCHED = GATVdr.item("FilterReplaceScheule")
                End If
                If IsDBNull(GATVdr.item("FilterReplaceScheuleUnits")) Then
                    NUMFILTERREPLACESCHEDUNITS = ""
                Else
                    NUMFILTERREPLACESCHEDUNITS = GATVdr.item("FilterReplaceScheuleUnits")
                End If
                NUMFILTERREPLACESCHEDUNITS = ""
                If IsDBNull(GATVdr.item("FilterCleaningMethod")) Then
                    CLEANINGMETHOD = ""
                Else
                    CLEANINGMETHOD = GATVdr.item("FilterCleaningMethod")
                End If
                CLEANINGMETHOD = ""

                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    CUName = ""
                Else
                    CUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    STRDESC = "None given"
                Else
                    STRDESC = GATVdr.item("EquipmentDescription")
                End If

                If IsDBNull(GATVdr.item("ControlReason")) Then
                    OPERATINGREASON = "None given"
                Else
                    OPERATINGREASON = GATVdr.item("ControlReason")
                End If
                If IsDBNull(GATVdr.item("ControlReasonOther")) Then
                    STROPERATINGREASONOTHER = "None given"
                Else
                    STROPERATINGREASONOTHER = GATVdr.item("ControlReasonOther")
                End If

                If IsDBNull(GATVdr.item("ParametersMonitored")) Then
                    STRPARAMETERSMONITORED = "None given"
                Else
                    STRPARAMETERSMONITORED = GATVdr.item("ParametersMonitored")
                End If

                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    STRMANUFACTURER = ""
                Else
                    STRMANUFACTURER = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    STRMODELNUMBER = ""
                Else
                    STRMODELNUMBER = GATVdr.item("ModelNumber")
                End If
                STRINSTALLATION = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DATMANUFACTURE = ""
                    STRINSTALLATION = ""
                Else
                    DATMANUFACTURE = GATVdr.item("DateManufactured_Reconstructed")
                    STRINSTALLATION = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DATMANUFACTURE) And DATMANUFACTURE.Length = 4 Then
                    DATMANUFACTURE = "01-Jan-" & DATMANUFACTURE
                End If

                If IsDate(DATMANUFACTURE) Then
                    DATMANUFACTURE = Format(CDate(DATMANUFACTURE), "dd-MMM-yyyy")
                Else
                    DATMANUFACTURE = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    DATINSTALLATION = ""
                    STRINSTALLATION = STRINSTALLATION
                Else
                    DATINSTALLATION = GATVdr.item("InstallationDate")
                    STRINSTALLATION = STRINSTALLATION & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(DATINSTALLATION) And DATINSTALLATION.Length = 4 Then
                    DATINSTALLATION = "01-Jan-" & DATINSTALLATION
                End If
                If IsDate(DATINSTALLATION) Then
                    DATINSTALLATION = Format(CDate(DATINSTALLATION), "dd-MMM-yyyy")
                Else
                    DATINSTALLATION = ""
                End If
                STRINSTALLATION = Mid(STRINSTALLATION, 1, 400)

                If ProjectID <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectID & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUnitMaster " & _
                        "(CONTROLUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUnitID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_ControlUnitID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITID " & _
                       "(CONTROLUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(CUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_ControlUNITMASTER " & _
                       "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITHEADER " & _
                        "(ControlUNITHEADERID, ControlUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, ControlUNITTYPE , " & _
                        "OperatingReason, strOperatingReasonOther, " & _
                        "STRPARAMETERSMONITORED, " & _
                        "COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(STRDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(STRMANUFACTURER, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(STRMODELNUMBER, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DATMANUFACTURE, "'", "''") & "', '" & Replace(DATINSTALLATION, "'", "''") & "', " & _
                        "'" & Replace(STRINSTALLATION, "'", "''") & "', 'O', " & _
                        "sysdate, '6', " & _
                        "'" & OPERATINGREASON & "', '" & Replace(STROPERATINGREASONOTHER, "'", "''") & "', " & _
                        "'" & Replace(STRPARAMETERSMONITORED, "'", "''") & "', " & _
                        "'Initail Populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_CD_FilterMedia " & _
                        "(PFW_CD_FilterMediaID, CONTROLUNITID, " & _
                        "GSID,  " & _
                        "FILTERMEDIA, STRFILTERMEDIAOTHER,  " & _
                        "NUMDISPOSABLE, NUMINLETDEWPOINTTEMP,  " & _
                        "NUMINLETDEWPOINTTEMPUNITS, NUMINLETGASTEMP,  " & _
                        "NUMINLETGASTEMPUNITS, NUMPRESSUREDROP,  " & _
                        "NUMBAGCOUNT, NUMFILTEROPLIFE,  " & _
                        "NUMFILTEROPLIFEUNITS, NUMFILTERAREA,  " & _
                        "NUMFILTERREPLACESCHED, NUMFILTERREPLACESCHEDUNITS,  " & _
                        "CLEANINGMETHOD, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_CD_FilterMediaID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & FILTERMEDIA & "', '" & Replace(STRFILTERMEDIAOTHER, "'", "''") & "',  " & _
                        "'" & NUMDISPOSABLE & "', '" & NUMINLETDEWPOINTTEMP & "',  " & _
                        "'" & NUMINLETDEWPOINTTEMPUNITS & "', '" & NUMINLETGASTEMP & "',  " & _
                        "'" & NUMINLETGASTEMPUNITS & "', '" & NUMPRESSUREDROP & "',  " & _
                        "'" & NUMBAGCOUNT & "', '" & NUMFILTEROPLIFE & "',  " & _
                        "'" & NUMFILTEROPLIFEUNITS & "', '" & NUMFILTERAREA & "',  " & _
                        "'" & NUMFILTERREPLACESCHED & "', '" & NUMFILTERREPLACESCHEDUNITS & "',  " & _
                        "'" & CLEANINGMETHOD & "', " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnPFWCDMiscellaneous_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWCDMiscellaneous.Click
        PopulatePFWCDMiscellaneous()
    End Sub
    Sub PopulatePFWCDMiscellaneous()
        Try
            Dim ProjectID As String = ""
            Dim oldID As String = ""
            Dim EUID As String = ""
            Dim GSID As String = ""
            Dim CUName As String = ""
            Dim Comments As String = ""

            Dim STRDESC As String = ""
            Dim STRMANUFACTURER As String = ""
            Dim STRMODELNUMBER As String = ""
            Dim DATMANUFACTURE As String = ""
            Dim DATINSTALLATION As String = ""
            Dim STRINSTALLATION As String = ""
            Dim OPERATINGSTATUS As String = "O"
            Dim DATOPERATINGSTATUS As String = ""
            Dim CONTROLUNITTYPE As String = "1"
            Dim OPERATINGREASON As String = ""
            Dim STROPERATINGREASONOTHER As String = ""
            Dim STRPARAMETERSMONITORED As String = ""

            Dim STRCONTROLDEVICESPECS As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblControlMisc.ProjectIdentifier as ProjectID,  " & _
            "tblControlMisc.EquipmentID,  " & _
            "tblControlMisc.Description, " & _
            "tblControlMisc.Manufacturer, " & _
            "tblControlMisc.ModelNumber, " & _
            "tblControlMisc.DateManufactured_Reconstructed, " & _
            "tblControlMisc.InstallationDate, " & _
            "tblControlMisc.PollutantControlledID, " & _
            "tblControlMisc.OverallControlEff, " & _
            "tblControlMisc.DeviceSpecs, " & _
            "tblControlMisc.ParametersMonitored, " & _
            "tblControlMisc.ControlReason, " & _
            "tblControlMisc.ControlReasonOther, " & _
            "tblControlMisc.Comments " & _
            "FROM tblControlMisc INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblControlMisc.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblControlMisc.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectID = ""
                If IsDBNull(GATVdr.item("ProjectID")) Then
                    ProjectID = ""
                Else
                    ProjectID = GATVdr.item("ProjectID")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If
                If IsDBNull(GATVdr.item("DeviceSpecs")) Then
                    STRCONTROLDEVICESPECS = ""
                Else
                    STRCONTROLDEVICESPECS = GATVdr.item("DeviceSpecs")
                End If

                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    CUName = ""
                Else
                    CUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    STRDESC = "None given"
                Else
                    STRDESC = GATVdr.item("EquipmentDescription")
                End If

                If IsDBNull(GATVdr.item("ControlReason")) Then
                    OPERATINGREASON = "None given"
                Else
                    OPERATINGREASON = GATVdr.item("ControlReason")
                End If
                If IsDBNull(GATVdr.item("ControlReasonOther")) Then
                    STROPERATINGREASONOTHER = "None given"
                Else
                    STROPERATINGREASONOTHER = GATVdr.item("ControlReasonOther")
                End If

                If IsDBNull(GATVdr.item("ParametersMonitored")) Then
                    STRPARAMETERSMONITORED = "None given"
                Else
                    STRPARAMETERSMONITORED = GATVdr.item("ParametersMonitored")
                End If

                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    STRMANUFACTURER = ""
                Else
                    STRMANUFACTURER = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    STRMODELNUMBER = ""
                Else
                    STRMODELNUMBER = GATVdr.item("ModelNumber")
                End If
                STRINSTALLATION = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DATMANUFACTURE = ""
                    STRINSTALLATION = ""
                Else
                    DATMANUFACTURE = GATVdr.item("DateManufactured_Reconstructed")
                    STRINSTALLATION = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DATMANUFACTURE) And DATMANUFACTURE.Length = 4 Then
                    DATMANUFACTURE = "01-Jan-" & DATMANUFACTURE
                End If

                If IsDate(DATMANUFACTURE) Then
                    DATMANUFACTURE = Format(CDate(DATMANUFACTURE), "dd-MMM-yyyy")
                Else
                    DATMANUFACTURE = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    DATINSTALLATION = ""
                    STRINSTALLATION = STRINSTALLATION
                Else
                    DATINSTALLATION = GATVdr.item("InstallationDate")
                    STRINSTALLATION = STRINSTALLATION & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(DATINSTALLATION) And DATINSTALLATION.Length = 4 Then
                    DATINSTALLATION = "01-Jan-" & DATINSTALLATION
                End If
                If IsDate(DATINSTALLATION) Then
                    DATINSTALLATION = Format(CDate(DATINSTALLATION), "dd-MMM-yyyy")
                Else
                    DATINSTALLATION = ""
                End If
                STRINSTALLATION = Mid(STRINSTALLATION, 1, 400)

                If ProjectID <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectID & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUnitMaster " & _
                        "(CONTROLUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUnitID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_ControlUnitID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITID " & _
                       "(CONTROLUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(CUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_ControlUNITMASTER " & _
                       "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITHEADER " & _
                        "(ControlUNITHEADERID, ControlUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, ControlUNITTYPE , " & _
                        "OperatingReason, strOperatingReasonOther, " & _
                        "STRPARAMETERSMONITORED, " & _
                        "COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(STRDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(STRMANUFACTURER, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(STRMODELNUMBER, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DATMANUFACTURE, "'", "''") & "', '" & Replace(DATINSTALLATION, "'", "''") & "', " & _
                        "'" & Replace(STRINSTALLATION, "'", "''") & "', 'O', " & _
                        "sysdate, '7', " & _
                        "'" & OPERATINGREASON & "', '" & Replace(STROPERATINGREASONOTHER, "'", "''") & "', " & _
                        "'" & Replace(STRPARAMETERSMONITORED, "'", "''") & "', " & _
                        "'Initail Populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_CD_Miscellaneous " & _
                        "(PFW_CD_MiscellaneousID, CONTROLUNITID, " & _
                        "GSID,  " & _
                        "STRCONTROLDEVICESPECS, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_CD_MiscellaneousID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & Replace(STRCONTROLDEVICESPECS, "'", "''") & "',   " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnPFWCDOxidizer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWCDOxidizer.Click
        PopulatePFWCDOxidizer()
    End Sub
    Sub PopulatePFWCDOxidizer()
        Try
            Dim ProjectID As String = ""
            Dim oldID As String = ""
            Dim EUID As String = ""
            Dim GSID As String = ""
            Dim CUName As String = ""
            Dim Comments As String = ""

            Dim STRDESC As String = ""
            Dim STRMANUFACTURER As String = ""
            Dim STRMODELNUMBER As String = ""
            Dim DATMANUFACTURE As String = ""
            Dim DATINSTALLATION As String = ""
            Dim STRINSTALLATION As String = ""
            Dim OPERATINGSTATUS As String = "O"
            Dim DATOPERATINGSTATUS As String = ""
            Dim CONTROLUNITTYPE As String = "1"
            Dim OPERATINGREASON As String = ""
            Dim STROPERATINGREASONOTHER As String = ""
            Dim STRPARAMETERSMONITORED As String = ""

            Dim OXIDIZERTYPE As String = ""
            Dim NUMMAXINLETGASFLOW As String = ""
            Dim NUMOPERATINGTEMP As String = ""
            Dim NUMOPERATINGTEMPUNITS As String = ""
            Dim NUMRESIDENCETIME As String = ""
            Dim NUMPRESSUREDROP As String = ""
            Dim NUMTEMPINLETSTREAM As String = ""
            Dim NUMTEMPINLETSTREAMUNITS As String = ""
            Dim NUMTEMPOUTLETSTREAM As String = ""
            Dim NUMTEMPOUTLETSTREAMUNITS As String = ""
            Dim NUMCATALYSTLIFE As String = ""
            Dim NUMCATALYSTLIFEUNITS As String = ""
            Dim NUMREGENERATIONCYCLE As String = ""
            Dim NUMREGENERATIONCYCLEUNITS As String = ""
            Dim STRCATALYSTDESC As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblControlOxidizers.ProjectIdentifier as ProjectID,  " & _
            "tblControlOxidizers.EquipmentID,  " & _
            "tblControlOxidizers.Description, " & _
            "tblControlOxidizers.Manufacturer, " & _
            "tblControlOxidizers.ModelNumber, " & _
            "tblControlOxidizers.DateManufactured_Reconstructed, " & _
            "tblControlOxidizers.InstallationDate, " & _
            "tblControlOxidizers.PollutantControlledID, " & _
            "tblControlOxidizers.OverallControlEff, " & _
            "tblControlOxidizers.InGasFlow, " & _
            "tblControlOxidizers.OxidizerType, " & _
            "tblControlOxidizers.CombustTemp, " & _
            "tblControlOxidizers.CombustTempUnits, " & _
            "tblControlOxidizers.ResidenceTime, " & _
            "tblControlOxidizers.InletTempCatBed, " & _
            "tblControlOxidizers.InletTempCatBedUnits, " & _
            "tblControlOxidizers.OutTempCatBed, " & _
            "tblControlOxidizers.OutTempCatBedUnits, " & _
            "tblControlOxidizers.DescriptionOfCatalyst, " & _
            "tblControlOxidizers.CatLifeExpect, " & _
            "tblControlOxidizers.CatLifeExpectUnits, " & _
            "tblControlOxidizers.CatRegenCycle, " & _
            "tblControlOxidizers.CatRegenCycleUnits, " & _
            "tblControlOxidizers.PressureDrop, " & _
            "tblControlOxidizers.ParametersMonitored, " & _
            "tblControlOxidizers.ControlReason, " & _
            "tblControlOxidizers.ControlReasonOther, " & _
            "tblControlOxidizers.Comments " & _
            "FROM tblControlOxidizers INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblControlOxidizers.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblControlOxidizers.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectID = ""
                If IsDBNull(GATVdr.item("ProjectID")) Then
                    ProjectID = ""
                Else
                    ProjectID = GATVdr.item("ProjectID")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If




                If IsDBNull(GATVdr.item("OxidizerType")) Then
                    OXIDIZERTYPE = ""
                Else
                    OXIDIZERTYPE = GATVdr.item("OxidizerType")
                End If
                If IsDBNull(GATVdr.item("InGasFlow")) Then
                    NUMMAXINLETGASFLOW = ""
                Else
                    NUMMAXINLETGASFLOW = GATVdr.item("InGasFlow")
                End If
                If IsDBNull(GATVdr.item("CombustTemp")) Then
                    NUMOPERATINGTEMP = ""
                Else
                    NUMOPERATINGTEMP = GATVdr.item("CombustTemp")
                End If




                If IsDBNull(GATVdr.item("CombustTempUnits")) Then
                    NUMOPERATINGTEMPUNITS = ""
                Else
                    NUMOPERATINGTEMPUNITS = GATVdr.item("CombustTempUnits")
                End If
                NUMOPERATINGTEMPUNITS = ""

                If IsDBNull(GATVdr.item("ResidenceTime")) Then
                    NUMRESIDENCETIME = ""
                Else
                    NUMRESIDENCETIME = GATVdr.item("ResidenceTime")
                End If
                If IsDBNull(GATVdr.item("PressureDrop")) Then
                    NUMPRESSUREDROP = ""
                Else
                    NUMPRESSUREDROP = GATVdr.item("PressureDrop")
                End If
                If IsDBNull(GATVdr.item("InletTempCatBed")) Then
                    NUMTEMPINLETSTREAM = ""
                Else
                    NUMTEMPINLETSTREAM = GATVdr.item("InletTempCatBed")
                End If
                If IsDBNull(GATVdr.item("InletTempCatBedUnits")) Then
                    NUMTEMPINLETSTREAMUNITS = ""
                Else
                    NUMTEMPINLETSTREAMUNITS = GATVdr.item("InletTempCatBedUnits")
                End If
                NUMTEMPINLETSTREAMUNITS = ""

                If IsDBNull(GATVdr.item("OutTempCatBed")) Then
                    NUMTEMPOUTLETSTREAM = ""
                Else
                    NUMTEMPOUTLETSTREAM = GATVdr.item("OutTempCatBed")
                End If
                If IsDBNull(GATVdr.item("OutTempCatBedUnits")) Then
                    NUMTEMPOUTLETSTREAMUNITS = ""
                Else
                    NUMTEMPOUTLETSTREAMUNITS = GATVdr.item("OutTempCatBedUnits")
                End If
                NUMTEMPOUTLETSTREAMUNITS = ""

                If IsDBNull(GATVdr.item("CatLifeExpect")) Then
                    NUMCATALYSTLIFE = ""
                Else
                    NUMCATALYSTLIFE = GATVdr.item("CatLifeExpect")
                End If
                If IsDBNull(GATVdr.item("CatLifeExpectUnits")) Then
                    NUMCATALYSTLIFEUNITS = ""
                Else
                    NUMCATALYSTLIFEUNITS = GATVdr.item("CatLifeExpectUnits")
                End If
                NUMCATALYSTLIFEUNITS = ""

                If IsDBNull(GATVdr.item("CatRegenCycle")) Then
                    NUMREGENERATIONCYCLE = ""
                Else
                    NUMREGENERATIONCYCLE = GATVdr.item("CatRegenCycle")
                End If
                If IsDBNull(GATVdr.item("CatRegenCycleUnits")) Then
                    NUMREGENERATIONCYCLEUNITS = ""
                Else
                    NUMREGENERATIONCYCLEUNITS = GATVdr.item("CatRegenCycleUnits")
                End If
                NUMREGENERATIONCYCLEUNITS = ""

                If IsDBNull(GATVdr.item("DescriptionOfCatalyst")) Then
                    STRCATALYSTDESC = ""
                Else
                    STRCATALYSTDESC = GATVdr.item("DescriptionOfCatalyst")
                End If

                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    CUName = ""
                Else
                    CUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    STRDESC = "None given"
                Else
                    STRDESC = GATVdr.item("EquipmentDescription")
                End If

                If IsDBNull(GATVdr.item("ControlReason")) Then
                    OPERATINGREASON = "None given"
                Else
                    OPERATINGREASON = GATVdr.item("ControlReason")
                End If
                If IsDBNull(GATVdr.item("ControlReasonOther")) Then
                    STROPERATINGREASONOTHER = "None given"
                Else
                    STROPERATINGREASONOTHER = GATVdr.item("ControlReasonOther")
                End If

                If IsDBNull(GATVdr.item("ParametersMonitored")) Then
                    STRPARAMETERSMONITORED = "None given"
                Else
                    STRPARAMETERSMONITORED = GATVdr.item("ParametersMonitored")
                End If

                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    STRMANUFACTURER = ""
                Else
                    STRMANUFACTURER = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    STRMODELNUMBER = ""
                Else
                    STRMODELNUMBER = GATVdr.item("ModelNumber")
                End If
                STRINSTALLATION = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DATMANUFACTURE = ""
                    STRINSTALLATION = ""
                Else
                    DATMANUFACTURE = GATVdr.item("DateManufactured_Reconstructed")
                    STRINSTALLATION = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DATMANUFACTURE) And DATMANUFACTURE.Length = 4 Then
                    DATMANUFACTURE = "01-Jan-" & DATMANUFACTURE
                End If

                If IsDate(DATMANUFACTURE) Then
                    DATMANUFACTURE = Format(CDate(DATMANUFACTURE), "dd-MMM-yyyy")
                Else
                    DATMANUFACTURE = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    DATINSTALLATION = ""
                    STRINSTALLATION = STRINSTALLATION
                Else
                    DATINSTALLATION = GATVdr.item("InstallationDate")
                    STRINSTALLATION = STRINSTALLATION & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(DATINSTALLATION) And DATINSTALLATION.Length = 4 Then
                    DATINSTALLATION = "01-Jan-" & DATINSTALLATION
                End If
                If IsDate(DATINSTALLATION) Then
                    DATINSTALLATION = Format(CDate(DATINSTALLATION), "dd-MMM-yyyy")
                Else
                    DATINSTALLATION = ""
                End If
                STRINSTALLATION = Mid(STRINSTALLATION, 1, 400)

                If ProjectID <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectID & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUnitMaster " & _
                        "(CONTROLUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUnitID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_ControlUnitID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITID " & _
                       "(CONTROLUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(CUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_ControlUNITMASTER " & _
                       "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITHEADER " & _
                        "(ControlUNITHEADERID, ControlUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, ControlUNITTYPE , " & _
                        "OperatingReason, strOperatingReasonOther, " & _
                        "STRPARAMETERSMONITORED, " & _
                        "COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(STRDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(STRMANUFACTURER, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(STRMODELNUMBER, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DATMANUFACTURE, "'", "''") & "', '" & Replace(DATINSTALLATION, "'", "''") & "', " & _
                        "'" & Replace(STRINSTALLATION, "'", "''") & "', 'O', " & _
                        "sysdate, '8', " & _
                        "'" & OPERATINGREASON & "', '" & Replace(STROPERATINGREASONOTHER, "'", "''") & "', " & _
                        "'" & Replace(STRPARAMETERSMONITORED, "'", "''") & "', " & _
                        "'Initail Populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_CD_Oxidizer " & _
                        "(PFW_CD_OxidizerID, CONTROLUNITID, " & _
                        "GSID,  " & _
                        "OXIDIZERTYPE, NUMMAXINLETGASFLOW, " & _
                        "NUMOPERATINGTEMP, NUMOPERATINGTEMPUNITS, " & _
                        "NUMRESIDENCETIME, NUMPRESSUREDROP, " & _
                        "NUMTEMPINLETSTREAM, NUMTEMPINLETSTREAMUNITS, " & _
                        "NUMTEMPOUTLETSTREAM, NUMTEMPOUTLETSTREAMUNITS, " & _
                        "NUMCATALYSTLIFE, NUMCATALYSTLIFEUNITS, " & _
                        "NUMREGENERATIONCYCLE, NUMREGENERATIONCYCLEUNITS, " & _
                        "STRCATALYSTDESC, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_CD_OxidizerID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & OXIDIZERTYPE & "', '" & NUMMAXINLETGASFLOW & "', " & _
                        "'" & NUMOPERATINGTEMP & "', '" & NUMOPERATINGTEMPUNITS & "', " & _
                        "'" & NUMRESIDENCETIME & "', '" & NUMPRESSUREDROP & "', " & _
                        "'" & NUMTEMPINLETSTREAM & "', '" & NUMTEMPINLETSTREAMUNITS & "', " & _
                        "'" & NUMTEMPOUTLETSTREAM & "', '" & NUMTEMPOUTLETSTREAMUNITS & "', " & _
                        "'" & NUMCATALYSTLIFE & "', '" & NUMCATALYSTLIFEUNITS & "', " & _
                        "'" & NUMREGENERATIONCYCLE & "', '" & NUMREGENERATIONCYCLEUNITS & "', " & _
                        "'" & Replace(STRCATALYSTDESC, "'", "''") & "', " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnPFWCDScrubber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPFWCDScrubber.Click
        PopulatePFWCDScrubber()
    End Sub
    Sub PopulatePFWCDScrubber()
        Try
            Dim ProjectID As String = ""
            Dim oldID As String = ""
            Dim EUID As String = ""
            Dim GSID As String = ""
            Dim CUName As String = ""
            Dim Comments As String = ""

            Dim STRDESC As String = ""
            Dim STRMANUFACTURER As String = ""
            Dim STRMODELNUMBER As String = ""
            Dim DATMANUFACTURE As String = ""
            Dim DATINSTALLATION As String = ""
            Dim STRINSTALLATION As String = ""
            Dim OPERATINGSTATUS As String = "O"
            Dim DATOPERATINGSTATUS As String = ""
            Dim CONTROLUNITTYPE As String = "1"
            Dim OPERATINGREASON As String = ""
            Dim STROPERATINGREASONOTHER As String = ""
            Dim STRPARAMETERSMONITORED As String = ""

            Dim SCRUBBERTYPE As String = ""
            Dim STRSCRUBBERTYPEOTHER As String = ""
            Dim NUMMAXINLETGASFLOWRATE As String = ""
            Dim STRSCRUBBERCHEMICALS As String = ""
            Dim NUMINLETGASTEMP As String = ""
            Dim NUMINLETGASTEMPUNITS As String = ""
            Dim NUMOUTLETGASTEMP As String = ""
            Dim NUMOUTLETGASTEMPUNITS As String = ""
            Dim NUMPRESSUREATNOZZELS As String = ""
            Dim NUMPRESSUREDROP As String = ""
            Dim NUMSCRUBBANTPH As String = ""
            Dim NUMSCRUBBANTFLOWRATE As String = ""
            Dim NUMSCRUBBANTFLOWRATEUNITS As String = ""
            Dim STRPACKINGUSEDDESC As String = ""

            SQL = "SELECT tblEmissionUnitMaster.*, " & _
            "tblControlScrubbers.ProjectIdentifier as ProjectID,  " & _
            "tblControlScrubbers.EquipmentID,  " & _
            "tblControlScrubbers.Description, " & _
            "tblControlScrubbers.Manufacturer, " & _
            "tblControlScrubbers.ModelNumber, " & _
            "tblControlScrubbers.DateManufactured_Reconstructed, " & _
            "tblControlScrubbers.InstallationDate, " & _
            "tblControlScrubbers.PollutantControlledID, " & _
            "tblControlScrubbers.OverallControlEff, " & _
            "tblControlScrubbers.InGasFlowRate, " & _
            "tblControlScrubbers.ScrubChemicals, " & _
            "tblControlScrubbers.ScrubberType, " & _
            "tblControlScrubbers.ScrubberTypeOther, " & _
            "tblControlScrubbers.InGasTemp, " & _
            "tblControlScrubbers.InGasTempUnits, " & _
            "tblControlScrubbers.OutGasTemp, " & _
            "tblControlScrubbers.OutGasTempUnits, " & _
            "tblControlScrubbers.PressureDrop, " & _
            "tblControlScrubbers.ScrubbantpH, " & _
            "tblControlScrubbers.SrubbantFlow, " & _
            "tblControlScrubbers.ScrubbantFlowUnits, " & _
            "tblControlScrubbers.SprayNozzelPress, " & _
            "tblControlScrubbers.OpLifePacking, " & _
            "tblControlScrubbers.OpLifePackingUnits, " & _
            "tblControlScrubbers.BedPacking, " & _
            "tblControlScrubbers.TankCapacity, " & _
            "tblControlScrubbers.ParametersMonitored, " & _
            "tblControlScrubbers.ControlReason, " & _
            "tblControlScrubbers.ControlReasonOther, " & _
            "tblControlScrubbers.Comments " & _
            "FROM tblControlScrubbers INNER JOIN tblEmissionUnitMaster ON " & _
            "(tblControlScrubbers.EquipmentID = tblEmissionUnitMaster.EquipmentID_DB) AND " & _
            "(tblControlScrubbers.ProjectIdentifier = tblEmissionUnitMaster.ProjectIdentifier) "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader
            While GATVdr.Read
                ProjectID = ""
                If IsDBNull(GATVdr.item("ProjectID")) Then
                    ProjectID = ""
                Else
                    ProjectID = GATVdr.item("ProjectID")
                End If
                If IsDBNull(GATVdr.item("EquipmentID_DB")) Then
                    oldID = ""
                Else
                    oldID = GATVdr.item("EquipmentID_DB")
                End If
                If IsDBNull(GATVdr.item("EmissionUnitID")) Then
                    EUID = ""
                Else
                    EUID = GATVdr.item("EmissionUnitID")
                End If

                If IsDBNull(GATVdr.item("ScrubberType")) Then
                    SCRUBBERTYPE = ""
                Else
                    SCRUBBERTYPE = GATVdr.item("ScrubberType")
                End If
                If IsDBNull(GATVdr.item("ScrubberTypeOther")) Then
                    STRSCRUBBERTYPEOTHER = ""
                Else
                    STRSCRUBBERTYPEOTHER = GATVdr.item("ScrubberTypeOther")
                End If
                If IsDBNull(GATVdr.item("InGasFlowRate")) Then
                    NUMMAXINLETGASFLOWRATE = ""
                Else
                    NUMMAXINLETGASFLOWRATE = GATVdr.item("InGasFlowRate")
                End If
                If IsDBNull(GATVdr.item("ScrubChemicals")) Then
                    STRSCRUBBERCHEMICALS = ""
                Else
                    STRSCRUBBERCHEMICALS = GATVdr.item("ScrubChemicals")
                End If
                If IsDBNull(GATVdr.item("InGasTemp")) Then
                    NUMINLETGASTEMP = ""
                Else
                    NUMINLETGASTEMP = GATVdr.item("InGasTemp")
                End If
                If IsDBNull(GATVdr.item("InGasTempUnits")) Then
                    NUMINLETGASTEMPUNITS = ""
                Else
                    NUMINLETGASTEMPUNITS = GATVdr.item("InGasTempUnits")
                End If
                NUMINLETGASTEMPUNITS = ""

                If IsDBNull(GATVdr.item("OutGasTemp")) Then
                    NUMOUTLETGASTEMP = ""
                Else
                    NUMOUTLETGASTEMP = GATVdr.item("OutGasTemp")
                End If
                If IsDBNull(GATVdr.item("OutGasTempUnits")) Then
                    NUMOUTLETGASTEMPUNITS = ""
                Else
                    NUMOUTLETGASTEMPUNITS = GATVdr.item("OutGasTempUnits")
                End If
                NUMOUTLETGASTEMPUNITS = ""

                If IsDBNull(GATVdr.item("SprayNozzelPress")) Then
                    NUMPRESSUREATNOZZELS = ""
                Else
                    NUMPRESSUREATNOZZELS = GATVdr.item("SprayNozzelPress")
                End If
                If IsDBNull(GATVdr.item("PressureDrop")) Then
                    NUMPRESSUREDROP = ""
                Else
                    NUMPRESSUREDROP = GATVdr.item("PressureDrop")
                End If
                If IsDBNull(GATVdr.item("ScrubbantpH")) Then
                    NUMSCRUBBANTPH = ""
                Else
                    NUMSCRUBBANTPH = GATVdr.item("ScrubbantpH")
                End If
                If IsDBNull(GATVdr.item("SrubbantFlow")) Then
                    NUMSCRUBBANTFLOWRATE = ""
                Else
                    NUMSCRUBBANTFLOWRATE = GATVdr.item("SrubbantFlow")
                End If
                If IsDBNull(GATVdr.item("ScrubbantFlowUnits")) Then
                    NUMSCRUBBANTFLOWRATEUNITS = ""
                Else
                    NUMSCRUBBANTFLOWRATEUNITS = GATVdr.item("ScrubbantFlowUnits")
                End If
                NUMSCRUBBANTFLOWRATEUNITS = ""
                If IsDBNull(GATVdr.item("BedPacking")) Then
                    STRPACKINGUSEDDESC = ""
                Else
                    STRPACKINGUSEDDESC = GATVdr.item("BedPacking")
                End If

                If IsDBNull(GATVdr.item("EquipmentName")) Then
                    CUName = ""
                Else
                    CUName = GATVdr.item("EquipmentName")
                End If
                If IsDBNull(GATVdr.item("EquipmentDescription")) Then
                    STRDESC = "None given"
                Else
                    STRDESC = GATVdr.item("EquipmentDescription")
                End If

                If IsDBNull(GATVdr.item("ControlReason")) Then
                    OPERATINGREASON = "None given"
                Else
                    OPERATINGREASON = GATVdr.item("ControlReason")
                End If
                If IsDBNull(GATVdr.item("ControlReasonOther")) Then
                    STROPERATINGREASONOTHER = "None given"
                Else
                    STROPERATINGREASONOTHER = GATVdr.item("ControlReasonOther")
                End If

                If IsDBNull(GATVdr.item("ParametersMonitored")) Then
                    STRPARAMETERSMONITORED = "None given"
                Else
                    STRPARAMETERSMONITORED = GATVdr.item("ParametersMonitored")
                End If

                If IsDBNull(GATVdr.item("Comments")) Then
                    Comments = ""
                Else
                    Comments = GATVdr.item("Comments")
                End If

                If IsDBNull(GATVdr.item("Manufacturer")) Then
                    STRMANUFACTURER = ""
                Else
                    STRMANUFACTURER = GATVdr.item("Manufacturer")
                End If
                If IsDBNull(GATVdr.item("ModelNumber")) Then
                    STRMODELNUMBER = ""
                Else
                    STRMODELNUMBER = GATVdr.item("ModelNumber")
                End If
                STRINSTALLATION = ""
                If IsDBNull(GATVdr.item("DateManufactured_Reconstructed")) Then
                    DATMANUFACTURE = ""
                    STRINSTALLATION = ""
                Else
                    DATMANUFACTURE = GATVdr.item("DateManufactured_Reconstructed")
                    STRINSTALLATION = "Original Manufacturing text from old system - " & GATVdr.item("DateManufactured_Reconstructed") & vbCrLf
                End If
                If IsNumeric(DATMANUFACTURE) And DATMANUFACTURE.Length = 4 Then
                    DATMANUFACTURE = "01-Jan-" & DATMANUFACTURE
                End If

                If IsDate(DATMANUFACTURE) Then
                    DATMANUFACTURE = Format(CDate(DATMANUFACTURE), "dd-MMM-yyyy")
                Else
                    DATMANUFACTURE = ""
                End If
                If IsDBNull(GATVdr.item("InstallationDate")) Then
                    DATINSTALLATION = ""
                    STRINSTALLATION = STRINSTALLATION
                Else
                    DATINSTALLATION = GATVdr.item("InstallationDate")
                    STRINSTALLATION = STRINSTALLATION & "Original Installation text from old system - " & GATVdr.item("InstallationDate")
                End If
                If IsNumeric(DATINSTALLATION) And DATINSTALLATION.Length = 4 Then
                    DATINSTALLATION = "01-Jan-" & DATINSTALLATION
                End If
                If IsDate(DATINSTALLATION) Then
                    DATINSTALLATION = Format(CDate(DATINSTALLATION), "dd-MMM-yyyy")
                Else
                    DATINSTALLATION = ""
                End If
                STRINSTALLATION = Mid(STRINSTALLATION, 1, 400)

                If ProjectID <> "" Then
                    SQL = "select GSID " & _
                    "from airTVApplication.PFW_WareHouseAdmin " & _
                    "where ProjectID = '" & ProjectID & "' "

                    cmd = New OracleCommand(SQL, ConnTVApplication)
                    If ConnTVApplication.State = ConnectionState.Closed Then
                        ConnTVApplication.Open()
                    End If
                    dr = cmd.ExecuteReader
                    GSID = ""
                    While dr.Read
                        If IsDBNull(dr.Item("GSID")) Then
                            GSID = ""
                        Else
                            GSID = dr.Item("GSID")
                        End If
                    End While
                    dr.Close()

                    If GSID <> "" Then
                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUnitMaster " & _
                        "(CONTROLUNITID, " & _
                        "GSID, COMMENTS, " & _
                        "ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUnitID.nextval, " & _
                        "'" & GSID & "', 'test populate', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_WAREHOUSEADMIN " & _
                        "where gsid = '" & GSID & "') "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL2 = "select AIRTVPROJECT.SEQ_GAP_ControlUnitID.currval from dual "
                        cmd2 = New OracleCommand(SQL2, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            temp = dr2.Item(0)
                        End While
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITID " & _
                       "(CONTROLUNITID, " & _
                       "GSID, STRNAME, " & _
                       "STRPROGRAM, DATEXPIRED, " & _
                       "COMMENTS, ACTIVE) " & _
                       "select  " & _
                       "" & temp & ", " & _
                       "'" & GSID & "', '" & Replace(Mid(CUName, 1, 6), "'", "''") & "', " & _
                       "'TV', '', " & _
                       "'Test Populate', '1' " & _
                       "from dual " & _
                       "where exists (select * from PFW_ControlUNITMASTER " & _
                       "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_ControlUNITHEADER " & _
                        "(ControlUNITHEADERID, ControlUNITID,  " & _
                        "GSID, STRDESC , " & _
                        "STRMANUFACTURER , STRMODELNUMBER ,     " & _
                        "DATMANUFACTURE , DATINSTALLATION ," & _
                        "STRINSTALLATION, OPERATINGSTATUS , " & _
                        "DATOPERATINGSTATUS, ControlUNITTYPE , " & _
                        "OperatingReason, strOperatingReasonOther, " & _
                        "STRPARAMETERSMONITORED, " & _
                        "COMMENTS , " & _
                        "VALID, ACTIVE) " & _
                        "select  " & _
                        "AIRTVPROJECT.SEQ_GAP_ControlUNITHEADERID.nextval, " & _
                        "" & temp & ", " & _
                        "'" & GSID & "', '" & Replace(Mid(STRDESC, 1, 400), "'", "''") & "', " & _
                        "'" & Mid(Replace(STRMANUFACTURER, "'", "''"), 1, 150) & "', " & _
                        "'" & Mid(Replace(STRMODELNUMBER, "'", "''"), 1, 400) & "', " & _
                        "'" & Replace(DATMANUFACTURE, "'", "''") & "', '" & Replace(DATINSTALLATION, "'", "''") & "', " & _
                        "'" & Replace(STRINSTALLATION, "'", "''") & "', 'O', " & _
                        "sysdate, '9', " & _
                        "'" & OPERATINGREASON & "', '" & Replace(STROPERATINGREASONOTHER, "'", "''") & "', " & _
                        "'" & Replace(STRPARAMETERSMONITORED, "'", "''") & "', " & _
                        "'Initail Populate', " & _
                        "'1', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        SQL = "Insert into AIRTVAPPLICATION.PFW_CD_Scrubber " & _
                        "(PFW_CD_ScrubberID, CONTROLUNITID, " & _
                        "GSID,  " & _
                        "SCRUBBERTYPE, STRSCRUBBERTYPEOTHER,  " & _
                        "NUMMAXINLETGASFLOWRATE, STRSCRUBBERCHEMICALS,  " & _
                        "NUMINLETGASTEMP, NUMINLETGASTEMPUNITS,  " & _
                        "NUMOUTLETGASTEMP, NUMOUTLETGASTEMPUNITS,  " & _
                        "NUMPRESSUREATNOZZELS, NUMPRESSUREDROP,  " & _
                        "NUMSCRUBBANTPH, NUMSCRUBBANTFLOWRATE,  " & _
                        "NUMSCRUBBANTFLOWRATEUNITS, STRPACKINGUSEDDESC, " & _
                        "COMMENTS, ACTIVE) " & _
                        "Select " & _
                        "airtvProject.SEQ_GAP_CD_ScrubberID.nextval,  " & _
                        "'" & temp & "', '" & GSID & "', " & _
                        "'" & SCRUBBERTYPE & "', '" & Replace(Mid(STRSCRUBBERTYPEOTHER, 1, 400), "'", "''") & "',  " & _
                        "'" & NUMMAXINLETGASFLOWRATE & "', '" & Replace(STRSCRUBBERCHEMICALS, "'", "''") & "',  " & _
                        "'" & NUMINLETGASTEMP & "', '" & NUMINLETGASTEMPUNITS & "',  " & _
                        "'" & NUMOUTLETGASTEMP & "', '" & NUMOUTLETGASTEMPUNITS & "',  " & _
                        "'" & NUMPRESSUREATNOZZELS & "', '" & NUMPRESSUREDROP & "',  " & _
                        "'" & NUMSCRUBBANTPH & "', '" & NUMSCRUBBANTFLOWRATE & "',  " & _
                        "'" & NUMSCRUBBANTFLOWRATEUNITS & "', '" & Replace(STRPACKINGUSEDDESC, "'", "''") & "', " & _
                        "'" & Replace(Comments, "'", "''") & "', '1' " & _
                        "from dual " & _
                        "where exists (select * from PFW_ControlUNITMASTER " & _
                        "where ControlUNITID = " & temp & ") "

                        cmd2 = New OracleCommand(SQL, ConnTVApplication)
                        If ConnTVApplication.State = ConnectionState.Closed Then
                            ConnTVApplication.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        count += 1
                    End If
                End If
            End While
            GATVdr.Close()

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
    Private Sub btnCDDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCDDelete.Click
        DeleteControlDeviceMaster()
    End Sub
    Sub DeleteControlDeviceMaster()
        Try
            SQL = "Delete airtvapplication.PFW_CDS_Pollutant "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_CD_Adsorber "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_CD_BioFiltration "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_CD_Condenser "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_CD_Cyclone "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_CD_ESP "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_CD_FilterMedia "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_CD_Miscellaneous "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_CD_Oxidizer "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_CD_Scrubber "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_ControlUnitHeader "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_ControlUnitID "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete airtvapplication.PFW_ControlUnitMaster "

            cmd = New OracleCommand(SQL, ConnTVApplication)
            If ConnTVApplication.State = ConnectionState.Closed Then
                ConnTVApplication.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()



        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            MsgBox("Done" & vbCrLf & count)
        End Try
    End Sub
End Class