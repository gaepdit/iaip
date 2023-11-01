Imports System.Data.SqlClient
Imports GaEpd
Imports Iaip.Apb

Namespace DAL
    Module ContactData

        Public Enum ContactKey
            None = 0
            Monitoring = 10
            Compliance = 20
            Permitting = 30
            Fees = 40
            EmissionInventory = 41
            <Obsolete("Emissions Statement contact no longer used", True)>
            EmissionStatement = 42
            <Obsolete("Ambient contact no longer used", True)>
            AmbientMonitoring = 50
            <Obsolete("Planning and Support contact no longer used", True)>
            PlanningAndSupport = 60
            <Obsolete("District Office contact no longer used", True)>
            DistrictOffices = 70
        End Enum

        Public Function GetCurrentContact(airsNumber As Apb.ApbFacilityId, key As ContactKey) As Contact
            If key = ContactKey.None OrElse Not [Enum].IsDefined(GetType(ContactKey), key) Then Return Nothing

            Const query As String = " SELECT strContactFirstName, 
                  strContactLastName, 
                  strContactPrefix, 
                  strContactSuffix, 
                  strContactCompanyName, 
                  strContactAddress1, 
                  strContactAddress2, 
                  strContactCity, 
                  strContactstate, 
                  strContactZipCode, 
                  STRCONTACTTITLE, 
                  STRCONTACTPHONENUMBER1, 
                  strContactEmail 
                 FROM APBContactInformation 
                 WHERE strAIRSNumber = @airsnumber 
                 AND strKey          = @key "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@airsnumber", airsNumber.DbFormattedString),
                New SqlParameter("@key", key.ToString("D"))
            }

            Dim dataTable As DataTable = DB.GetDataTable(query, parameters)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Return ContactFromDataRow(dataTable.Rows(0))
        End Function

        Private Function ContactFromDataRow(row As DataRow) As Contact
            Dim contact As New Contact
            Dim address As New Address

            With address
                .Street = DBUtilities.GetNullable(Of String)(row("strContactAddress1"))
                .Street2 = DBUtilities.GetNullable(Of String)(row("strContactAddress2"))
                .City = DBUtilities.GetNullable(Of String)(row("strContactCity"))
                .PostalCode = DBUtilities.GetNullable(Of String)(row("strContactZipCode"))
                .State = DBUtilities.GetNullable(Of String)(row("strContactstate"))
            End With

            With contact
                .FirstName = DBUtilities.GetNullable(Of String)(row("strContactFirstName"))
                .LastName = DBUtilities.GetNullable(Of String)(row("strContactLastName"))
                .Prefix = DBUtilities.GetNullable(Of String)(row("strContactPrefix"))
                .Suffix = DBUtilities.GetNullable(Of String)(row("strContactSuffix"))
                .CompanyName = DBUtilities.GetNullable(Of String)(row("strContactCompanyName"))
                .MailingAddress = address
                .EmailAddress = DBUtilities.GetNullable(Of String)(row("strContactEmail"))
                .Title = DBUtilities.GetNullable(Of String)(row("STRCONTACTTITLE"))
                .PhoneNumber = DBUtilities.GetNullable(Of String)(row("STRCONTACTPHONENUMBER1"))
            End With

            Return contact
        End Function

        Public Function ContactKeyExists(reKey As ContactKey, airsNumber As ApbFacilityId) As Boolean
            Dim query As String = "select convert(bit, count(*)) from APBCONTACTINFORMATION 
                where STRKEY = @key and STRAIRSNUMBER = @airs"

            Dim params As SqlParameter() = {
                New SqlParameter("@key", reKey.ToString("D")),
                New SqlParameter("@airs", airsNumber.DbFormattedString)
            }

            Return DB.GetBoolean(query, params)
        End Function

    End Module
End Namespace
