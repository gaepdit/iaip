Imports System.Data.SqlClient
Imports EpdIt

Public Class IAIPEditFacilityLocation

    Public Property AirsNumber As Apb.ApbFacilityId
    Private Property drCurrentData As DataRow

    Private Sub IAIPEditFacilityLocation_Load(sender As Object, e As EventArgs) Handles Me.Load
        ParseParameters()

        If AirsNumber IsNot Nothing Then
            LoadCurrentFacilityInformation()
            LoadHistoricalFacilityInformation()
        End If
    End Sub

    Private Sub ParseParameters()
        If Parameters IsNot Nothing AndAlso Parameters.ContainsKey(FormParameter.AirsNumber) Then
            Try
                Me.AirsNumber = Parameters(FormParameter.AirsNumber)
                lblAirsNumber.Text = Me.AirsNumber.FormattedString
                Me.Text = Me.Text & " - " & Me.AirsNumber.FormattedString
            Catch ex As Exception
                Me.AirsNumber = Nothing
            End Try
        End If
    End Sub

    Private Sub LoadCurrentFacilityInformation()
        Dim query As String = "Select * " &
            "from VW_APBFacilityLocation " &
            "where strAIRSNumber = @strAIRSNumber "

        Dim param As New SqlParameter("@strAIRSNumber", AirsNumber.DbFormattedString)

        drCurrentData = DB.GetDataRow(query, param)

        If drCurrentData IsNot Nothing Then
            Dim ModifingPerson As String
            Dim ModifingDate As String
            Dim ModifingLocation As String

            txtFacilityName.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("STRFACILITYNAME"))
            txtStreetAddress.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("STRFACILITYSTREET1"))
            txtStreetAddress2.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("STRFACILITYSTREET2"))
            txtFacilityCity.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("STRFACILITYCITY"))
            txtFacilityState.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("STRFACILITYSTATE"))
            mtbFacilityZipCode.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("STRFACILITYZIPCODE"))
            txtFacilityLatitude.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("NUMFACILITYLATITUDE"))
            txtFacilityLongitude.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("NUMFACILITYLONGITUDE"))
            txtComments.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("STRCOMMENTS"))

            ModifingPerson = If(DBUtilities.GetNullable(Of String)(drCurrentData.Item("USERNAME")), "Unknown")
            ModifingDate = If(DBUtilities.GetNullable(Of String)(drCurrentData.Item("MODIFINGDATE")), "Unknown Date")
            ModifingLocation = If(DBUtilities.GetNullable(Of String)(drCurrentData.Item("STRMODIFINGLOCATION")), "Unknown Location")
            lblModifiedNote.Text = "Modified on " & ModifingDate & " by " & ModifingPerson & " from " & ModifingLocation
        End If
    End Sub

    Private Sub LoadHistoricalFacilityInformation()
        Dim query As String = "Select * " &
            "from VW_HB_APBFacilityLocation " &
            "where strAIRSNumber = @strAIRSNumber " &
            "Order by strKey DESC "

        Dim param As New SqlParameter("@strAIRSNumber", AirsNumber.DbFormattedString)

        Dim dtHistoricalData As DataTable = DB.GetDataTable(query, param)

        If dtHistoricalData IsNot Nothing Then
            dgvFacilityInformationHistory.DataSource = dtHistoricalData
            dgvFacilityInformationHistory.Columns("strKey").HeaderText = "Key"
            dgvFacilityInformationHistory.Columns("strKey").DisplayIndex = 0
            dgvFacilityInformationHistory.Columns("strKey").Visible = False
            dgvFacilityInformationHistory.Columns("ModifingDate").HeaderText = "Date Modified"
            dgvFacilityInformationHistory.Columns("ModifingDate").DisplayIndex = 1
            dgvFacilityInformationHistory.Columns("UserName").HeaderText = "Modifying Person"
            dgvFacilityInformationHistory.Columns("UserName").DisplayIndex = 2
            dgvFacilityInformationHistory.Columns("strModifingLocation").HeaderText = "Modifying Location"
            dgvFacilityInformationHistory.Columns("strModifingLocation").DisplayIndex = 3
            dgvFacilityInformationHistory.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFacilityInformationHistory.Columns("strFacilityName").DisplayIndex = 4
            dgvFacilityInformationHistory.Columns("strFacilityStreet1").HeaderText = "Facility Address"
            dgvFacilityInformationHistory.Columns("strFacilityStreet1").DisplayIndex = 5
            dgvFacilityInformationHistory.Columns("strFacilityStreet2").HeaderText = "Facility Address 2"
            dgvFacilityInformationHistory.Columns("strFacilityStreet2").DisplayIndex = 6
            dgvFacilityInformationHistory.Columns("strFacilityCity").HeaderText = "City"
            dgvFacilityInformationHistory.Columns("strFacilityCity").DisplayIndex = 7
            dgvFacilityInformationHistory.Columns("strFacilityState").HeaderText = "State"
            dgvFacilityInformationHistory.Columns("strFacilityState").DisplayIndex = 8
            dgvFacilityInformationHistory.Columns("strFacilityZipCode").HeaderText = "Zip Code"
            dgvFacilityInformationHistory.Columns("strFacilityZipCode").DisplayIndex = 9
            dgvFacilityInformationHistory.Columns("numFacilityLongitude").HeaderText = "Longitude"
            dgvFacilityInformationHistory.Columns("numFacilityLongitude").DisplayIndex = 10
            dgvFacilityInformationHistory.Columns("numFacilityLatitude").HeaderText = "Latitude"
            dgvFacilityInformationHistory.Columns("numFacilityLatitude").DisplayIndex = 11
            dgvFacilityInformationHistory.Columns("strComments").HeaderText = "Comments"
            dgvFacilityInformationHistory.Columns("strComments").DisplayIndex = 12
            dgvFacilityInformationHistory.Columns("strAIRSNumber").HeaderText = "AIRS Number"
            dgvFacilityInformationHistory.Columns("strAIRSNumber").DisplayIndex = 13
            dgvFacilityInformationHistory.Columns("strAIRSNumber").Visible = False
            dgvFacilityInformationHistory.SanelyResizeColumns
        End If
    End Sub

    Private Sub Save()
        If Not (AccountFormAccess(28, 2) = "1" OrElse AccountFormAccess(28, 3) = "1" OrElse AccountFormAccess(28, 4) = "1") Then
            MessageBox.Show("You do not have permissions to change facility location information.")
            Return
        End If

        Dim FacilityName As String
        Dim Street1 As String
        Dim Street2 As String
        Dim City As String
        Dim State As String
        Dim ZipCode As String
        Dim Longitude As String
        Dim Latitude As String
        Dim Comments As String

        txtFacilityName.BackColor = Color.Empty
        txtStreetAddress.BackColor = Color.Empty
        txtStreetAddress2.BackColor = Color.Empty
        txtFacilityCity.BackColor = Color.Empty
        txtFacilityState.BackColor = Color.Empty
        mtbFacilityZipCode.BackColor = Color.Empty
        txtFacilityLatitude.BackColor = Color.Empty
        txtFacilityLongitude.BackColor = Color.Empty
        txtComments.BackColor = Color.Empty

        If Not ValidateData() Then
            MessageBox.Show("Please correct the missing data.")
            Return
        End If

        txtFacilityName.Text = Apb.Facilities.Facility.SanitizeFacilityNameForDb(txtFacilityName.Text)

        If txtFacilityName.Text <> drCurrentData.Item("STRFACILITYNAME") Then
            FacilityName = txtFacilityName.Text
        Else
            FacilityName = ""
        End If

        If txtStreetAddress.Text <> drCurrentData.Item("STRFACILITYSTREET1") Then
            Street1 = txtStreetAddress.Text
        Else
            Street1 = ""
        End If

        If txtStreetAddress2.Text <> drCurrentData.Item("STRFACILITYSTREET2") Then
            Street2 = txtStreetAddress2.Text
        Else
            Street2 = ""
        End If

        If txtFacilityCity.Text <> drCurrentData.Item("STRFACILITYCITY") Then
            City = txtFacilityCity.Text
        Else
            City = ""
        End If

        If txtFacilityState.Text <> drCurrentData.Item("STRFACILITYSTATE") Then
            State = txtFacilityState.Text
        Else
            State = ""
        End If

        If Replace(mtbFacilityZipCode.Text, "-", "") <> drCurrentData.Item("STRFACILITYZIPCODE") Then
            ZipCode = Replace(mtbFacilityZipCode.Text, "-", "")
        Else
            ZipCode = ""
        End If

        If txtFacilityLongitude.Text <> drCurrentData.Item("NUMFACILITYLONGITUDE") Then
            Longitude = txtFacilityLongitude.Text
        Else
            Longitude = ""
        End If

        If txtFacilityLatitude.Text <> drCurrentData.Item("NUMFACILITYLATITUDE") Then
            Latitude = txtFacilityLatitude.Text
        Else
            Latitude = ""
        End If

        Comments = txtComments.Text

        If String.Join("", {FacilityName, Street1, Street2, City, State, ZipCode, Longitude, Latitude}) = "" Then
            MessageBox.Show("No data has been modified")
            Return
        End If

        Dim query1 As String = "Update APBFacilityInformation set "

        If FacilityName <> "" Then query1 = query1 & " strFacilityName = @strFacilityName, "
        If Street1 <> "" Then query1 = query1 & " strFacilityStreet1 = @strFacilityStreet1, "
        If Street2 <> "" Then query1 = query1 & " strFacilityStreet2 = @strFacilityStreet2, "
        If City <> "" Then query1 = query1 & " strFacilityCity = @strFacilityCity, "
        If State <> "" Then query1 = query1 & " strFacilityState = @strFacilityState, "
        If ZipCode <> "" Then query1 = query1 & " strFacilityZipCode = @strFacilityZipCode, "
        If Longitude <> "" Then query1 = query1 & " numFacilityLongitude = @numFacilityLongitude, "
        If Latitude <> "" Then query1 = query1 & " numFacilityLatitude = @numFacilityLatitude, "
        If Comments <> "" Then query1 = query1 & " strComments = @strComments, "

        query1 = query1 & " strModifingPerson = @strModifingPerson, " &
            " datModifingDate = getdate(), " &
            " strModifingLocation = '2' " &
            " where strAIRSNumber = @strAIRSNumber "

        Dim params1 As SqlParameter() = {
            New SqlParameter("@strFacilityName", FacilityName),
            New SqlParameter("@strFacilityStreet1", Street1),
            New SqlParameter("@strFacilityStreet2", Street2),
            New SqlParameter("@strFacilityCity", City),
            New SqlParameter("@strFacilityState", State),
            New SqlParameter("@strFacilityZipCode", ZipCode),
            New SqlParameter("@numFacilityLongitude", Longitude),
            New SqlParameter("@numFacilityLatitude", Latitude),
            New SqlParameter("@strComments", Comments),
            New SqlParameter("@strModifingPerson", CurrentUser.UserID),
            New SqlParameter("@strAIRSNumber", AirsNumber.DbFormattedString)
        }

        Dim queryList As New Generic.List(Of String) From {
            query1
        }

        Dim paramsList As New Generic.List(Of SqlParameter()) From {
            params1
        }

        If FacilityName <> "" Then
            Dim query3 As String = "Update EIS_FacilitySite set " &
                "strFacilitySiteName = @strFacilitySiteName, " &
                "strFacilitySiteComment = 'Facility Name updated.', " &
                "UpdateUSer = @UpdateUSer, " &
                "updateDateTime = getdate() " &
                "where facilitySiteID = @facilitySiteID "
            Dim params3 As SqlParameter() = {
                New SqlParameter("@strFacilitySiteName", FacilityName),
                New SqlParameter("@UpdateUSer", CurrentUser.AlphaName),
                New SqlParameter("@facilitySiteID", AirsNumber.DbFormattedString)
            }

            queryList.Add(query3)
            paramsList.Add(params3)
        End If

        If DB.RunCommand(queryList, paramsList) Then
            LoadCurrentFacilityInformation()
            LoadHistoricalFacilityInformation()

            MessageBox.Show("Data updated successfully")
        Else
            MessageBox.Show("An unknown error occurred. Please try again.")
        End If
    End Sub

    Private Function ValidateData() As Boolean
        Dim DataIsValid As Boolean = True

        If txtFacilityName.Text = "" Then
            DataIsValid = False
            txtFacilityName.BackColor = Color.Yellow
        End If

        If txtStreetAddress.Text = "" Then
            DataIsValid = False
            txtStreetAddress.BackColor = Color.Yellow
        End If

        If txtStreetAddress2.Text = "" Then
            DataIsValid = False
            txtStreetAddress2.BackColor = Color.Yellow
        End If

        If txtFacilityCity.Text = "" Then
            DataIsValid = False
            txtFacilityCity.BackColor = Color.Yellow
        End If

        If txtFacilityState.Text = "" Then
            DataIsValid = False
            txtFacilityState.BackColor = Color.Yellow
        End If

        If mtbFacilityZipCode.Text = "" Then
            DataIsValid = False
            mtbFacilityZipCode.BackColor = Color.Yellow
        End If

        If Not (txtFacilityLatitude.Text = "" OrElse IsNumeric(txtFacilityLatitude.Text)) Then
            DataIsValid = False
            txtFacilityLatitude.BackColor = Color.Yellow
        End If

        If Not (txtFacilityLongitude.Text = "" OrElse IsNumeric(txtFacilityLongitude.Text)) Then
            DataIsValid = False
            txtFacilityLongitude.BackColor = Color.Yellow
        End If

        If txtComments.Text = "" OrElse txtComments.Text = DBUtilities.GetNullable(Of String)(drCurrentData.Item("STRCOMMENTS")) Then
            DataIsValid = False
            txtComments.BackColor = Color.Yellow
            MessageBox.Show("Since this is a direct change to the data, please make a unique comment" &
                            " so future users will know why the data was changed.")
        End If

        Return DataIsValid
    End Function

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Save()
    End Sub

End Class