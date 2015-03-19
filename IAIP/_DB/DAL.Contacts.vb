Imports Oracle.DataAccess.Client

Namespace DAL
    Module Contacts

        Public Enum ContactKey
            None = 0
            IndustrialSourceMonitoring = 10
            StationarySourceCompliance = 20
            StationarySourcePermitting = 30
            Fees = 40
            EmissionInventory = 41
            EmissionStatement = 42
            AmbientMonitoring = 50
            PlanningAndSupport = 60
            DistrictOffices = 70
        End Enum

        Public Function GetCurrentContact(ByVal airsNumber As Apb.ApbFacilityId, ByVal key As ContactKey) As Contact
            If key = ContactKey.None OrElse Not [Enum].IsDefined(GetType(ContactKey), key) Then Return Nothing

            Dim contact As New Contact

            Dim query As String = " SELECT strContactFirstName, " & _
                "  strContactLastName, " & _
                "  strContactPrefix, " & _
                "  strContactSuffix, " & _
                "  strContactCompanyName, " & _
                "  strContactAddress1, " & _
                "  strContactAddress2, " & _
                "  strContactCity, " & _
                "  strContactstate, " & _
                "  strContactZipCode, " & _
                "  STRCONTACTTITLE, " & _
                "  STRCONTACTPHONENUMBER1, " & _
                "  strContactEmail " & _
                " FROM AIRBRANCH.APBContactInformation " & _
                " WHERE strAIRSNumber = :airsnumber " & _
                " AND strKey          = :key "

            Dim parameters As OracleParameter() = New OracleParameter() { _
                New OracleParameter("airsnumber", airsNumber.DbFormattedString), _
                New OracleParameter("key", key.ToString("D")) _
            }

            Dim dataTable As DataTable = DB.GetDataTable(query, parameters)
            If dataTable Is Nothing Then Return Nothing

            FillContactFromDataRow(dataTable.Rows(0), contact)

            Return contact
        End Function

        Private Sub FillContactFromDataRow(ByVal row As DataRow, ByRef contact As Contact)
            Dim address As New Address
            With address
                .Street = DB.GetNullable(Of String)(row("strContactAddress1"))
                .Street2 = DB.GetNullable(Of String)(row("strContactAddress2"))
                .City = DB.GetNullable(Of String)(row("strContactCity"))
                .PostalCode = DB.GetNullable(Of String)(row("strContactZipCode"))
                .State = DB.GetNullable(Of String)(row("strContactstate"))
            End With
            With contact
                .FirstName = DB.GetNullable(Of String)(row("strContactFirstName"))
                .LastName = DB.GetNullable(Of String)(row("strContactLastName"))
                .Prefix = DB.GetNullable(Of String)(row("strContactPrefix"))
                .Suffix = DB.GetNullable(Of String)(row("strContactSuffix"))
                .CompanyName = DB.GetNullable(Of String)(row("strContactCompanyName"))
                .MailingAddress = address
                .EmailAddress = DB.GetNullable(Of String)(row("strContactEmail"))
                .Title = DB.GetNullable(Of String)(row("STRCONTACTTITLE"))
                .PhoneNumber = DB.GetNullable(Of String)(row("STRCONTACTPHONENUMBER1"))
            End With
        End Sub

    End Module
End Namespace
