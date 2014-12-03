Imports System.Collections.Generic
Imports Oracle.DataAccess.Client

Namespace DAL
    Module Fees

        Public Function Update_FS_Admin_Status(ByVal feeYear As String, ByVal airsNumber As String) As Boolean
            If Not Apb.ApbFacilityId.ValidAirsNumberFormat(airsNumber) Then Return False
            Dim aN As Apb.ApbFacilityId = airsNumber

            Dim feeYearDecimal As Decimal
            If Not Decimal.TryParse(feeYear, feeYearDecimal) Then Return False

            Dim sp As String = "AIRBRANCH.PD_FEE_STATUS"

            Dim parameters As OracleParameter() = New OracleParameter() { _
                New OracleParameter("FEEYEAR", OracleDbType.Decimal, feeYear, ParameterDirection.Input), _
                New OracleParameter("AIRSNUMBER", aN.DbFormattedString) _
            }

            Try
                DB.ExecuteStoredProcedure(sp, parameters)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetAllFeeYears() As List(Of String)
            Dim list As New List(Of String)
            Dim dataTable As DataTable = GetAllFeeYearsAsDataTable()
            For Each row As DataRow In dataTable.Rows
                list.Add(row("FEEYEAR"))
            Next
            Return list
        End Function

        Private Function GetAllFeeYearsAsDataTable() As DataTable
            Dim query As String = "SELECT DISTINCT(NUMFEEYEAR) AS FEEYEAR " & _
                "FROM " & DBNameSpace & ".FSLK_NSPSREASONYEAR " & _
                "ORDER BY FEEYEAR DESC"
            Return DB.GetDataTable(query)
        End Function

        Public Function FeeMailoutEntryExists(ByVal airsNumber As Apb.ApbFacilityId, ByVal feeYear As String) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM " & DBNameSpace & ".FS_Mailout " & _
                " WHERE RowNum = 1 " & _
                " AND strAIRSnumber = :airsnumber " & _
                " AND numFeeYear = :feeyear "

            Dim parameters As OracleParameter() = New OracleParameter() { _
                New OracleParameter("airsnumber", airsNumber.DbFormattedString), _
                New OracleParameter("feeyear", feeYear) _
            }

            Dim result As String = DB.GetSingleValue(Of String)(query, parameters)
            Return Convert.ToBoolean(result)
        End Function

        Public Function UpdateFeeMailoutContact(ByVal contact As Contact, ByVal airsNumber As String, ByVal feeYear As String) As Boolean

            Dim query As String = "UPDATE " & DBNameSpace & ".FS_MAILOUT " & _
                " SET STRFIRSTNAME       = :v2, " & _
                "  STRLASTNAME          = :v3, " & _
                "  STRPREFIX            = :v4, " & _
                "  STRCONTACTSUFFIX     = :v28, " & _
                "  STRTITLE             = :v5, " & _
                "  STRCONTACTCONAME     = :v6, " & _
                "  STRCONTACTADDRESS1   = :v7, " & _
                "  STRCONTACTADDRESS2   = :v8, " & _
                "  STRCONTACTCITY       = :v9, " & _
                "  STRCONTACTSTATE      = :v10, " & _
                "  STRCONTACTZIPCODE    = :v11, " & _
                "  STRGECOUSEREMAIL     = :v12, " & _
                "  UPDATEUSER           = :v25, " & _
                "  UPDATEDATETIME       = :v26 " & _
                " WHERE STRAIRSNUMBER   = :airsnumber " & _
                " AND NUMFEEYEAR        = :feeyear "

            Dim parameters As OracleParameter() = { _
                New OracleParameter("v2", contact.FirstName), _
                New OracleParameter("v3", contact.LastName), _
                New OracleParameter("v4", contact.Prefix), _
                New OracleParameter("v28", contact.Suffix), _
                New OracleParameter("v5", contact.Title), _
                New OracleParameter("v6", contact.CompanyName), _
                New OracleParameter("v7", contact.MailingAddress.Street), _
                New OracleParameter("v8", contact.MailingAddress.Street2), _
                New OracleParameter("v9", contact.MailingAddress.City), _
                New OracleParameter("v10", contact.MailingAddress.State), _
                New OracleParameter("v11", contact.MailingAddress.PostalCode), _
                New OracleParameter("v12", contact.EmailAddress), _
                New OracleParameter("v25", UserGCode), _
                New OracleParameter("v26", OracleDate), _
                New OracleParameter("airsnumber", airsNumber), _
                New OracleParameter("feeyear", feeYear) _
            }

            Return DB.RunCommand(query, parameters)

        End Function

        Public Function UpdateFeeMailoutFacility(ByVal facility As Apb.Facility, ByVal airsNumber As String, ByVal feeYear As String) As Boolean
            Try

                Dim query As String = "UPDATE " & DBNameSpace & ".FS_MAILOUT " & _
                    " SET STROPERATIONALSTATUS = :v13, " & _
                    "  STRCLASS             = :v14, " & _
                    "  STRNSPS              = :v15, " & _
                    "  STRPART70            = :v16, " & _
                    "  DATSHUTDOWNDATE      = :v17, " & _
                    "  STRFACILITYNAME      = :v18, " & _
                    "  STRFACILITYADDRESS1  = :v19, " & _
                    "  STRFACILITYADDRESS2  = :v20, " & _
                    "  STRFACILITYCITY      = :v21, " & _
                    "  STRFACILITYZIPCODE   = :v22, " & _
                    "  STRCOMMENT           = :v23, " & _
                    "  UPDATEUSER           = :v25, " & _
                    "  UPDATEDATETIME       = :v26 " & _
                    " WHERE STRAIRSNUMBER   = :airsnumber " & _
                    " AND NUMFEEYEAR        = :feeyear "

                Dim parameters As OracleParameter() = { _
                    New OracleParameter("v13", facility.HeaderData.OperationalStatusCode), _
                    New OracleParameter("v14", facility.HeaderData.ClassificationCode), _
                    New OracleParameter("v15", If(facility.SubjectToNsps, "1", "0")), _
                    New OracleParameter("v16", If(facility.SubjectToPart70, "1", "0")), _
                    New OracleParameter("v17", facility.HeaderData.ShutdownDate), _
                    New OracleParameter("v18", facility.FacilityName), _
                    New OracleParameter("v19", facility.MailingAddress.Street), _
                    New OracleParameter("v20", facility.MailingAddress.Street2), _
                    New OracleParameter("v21", facility.MailingAddress.City), _
                    New OracleParameter("v22", facility.MailingAddress.PostalCode), _
                    New OracleParameter("v23", facility.Comment), _
                    New OracleParameter("v25", UserGCode), _
                    New OracleParameter("v26", OracleDate), _
                    New OracleParameter("airsnumber", airsNumber), _
                    New OracleParameter("feeyear", feeYear) _
                }

                Return DB.RunCommand(query, parameters)
            Catch ex As Exception

            End Try

        End Function

    End Module
End Namespace