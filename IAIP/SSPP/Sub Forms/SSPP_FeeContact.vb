Imports System.Data.SqlClient

Public Class SSPP_FeeContact

    Private Sub LoadCurrentFeeContact()
        Try
            txtSocialTitle.Clear()
            txtFirstName.Clear()
            txtLastName.Clear()
            txtPedigree.Clear()
            txtTitle.Clear()
            txtCompany.Clear()
            txtAddress.Clear()
            txtCity.Clear()
            txtState.Clear()
            mtbZipCode.Clear()
            mtbPhoneNumber1.Clear()
            mtbPhoneNumber2.Clear()
            mtbFaxNumber.Clear()
            txtEmailAddress.Clear()
            txtDescription.Clear()

            Dim SQL As String = "Select " &
            "strContactKey, " &
            "strContactFirstName, strContactLastName, " &
            "strContactPrefix, strContactSuffix, " &
            "strContactTitle, strContactCompanyName, " &
            "strContactPhoneNumber1, strContactPhoneNumber2, " &
            "strContactFaxNumber, strContactEmail, " &
            "strContactAddress1, strContactCity, " &
            "strContactState, strContactZipCode, " &
            "strContactDescription " &
            "from APBContactInformation " &
            "where strContactKey = @airs "

            Dim p As New SqlParameter("@airs", "0413" & txtAIRSNumber.Text & "40")

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtSocialTitle.Clear()
                Else
                    txtSocialTitle.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtFirstName.Clear()
                Else
                    txtFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtLastName.Clear()
                Else
                    txtLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtPedigree.Clear()
                Else
                    txtPedigree.Text = dr.Item("strContactSuffix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtTitle.Clear()
                Else
                    txtTitle.Text = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtCompany.Clear()
                Else
                    txtCompany.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                    mtbPhoneNumber1.Clear()
                Else
                    mtbPhoneNumber1.Text = dr.Item("strContactPhoneNumber1")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber2")) Then
                    mtbPhoneNumber2.Clear()
                Else
                    mtbPhoneNumber2.Text = dr.Item("strContactPhoneNumber2")
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    mtbFaxNumber.Clear()
                Else
                    mtbFaxNumber.Text = dr.Item("strContactFaxNumber")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEmailAddress.Clear()
                Else
                    txtEmailAddress.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtAddress.Clear()
                Else
                    txtAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtCity.Clear()
                Else
                    txtCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtState.Clear()
                Else
                    txtState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    mtbZipCode.Clear()
                Else
                    mtbZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactDescription")) Then
                    txtDescription.Clear()
                Else
                    txtDescription.Text = dr.Item("strContactDescription")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtAIRSNumber_TextChanged(sender As Object, e As EventArgs) Handles txtAIRSNumber.TextChanged
        Try
            LoadCurrentFeeContact()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            txtSocialTitle.Clear()
            txtFirstName.Clear()
            txtLastName.Clear()
            txtPedigree.Clear()
            txtTitle.Clear()
            txtCompany.Clear()
            txtAddress.Clear()
            txtCity.Clear()
            txtState.Clear()
            mtbZipCode.Clear()
            mtbPhoneNumber1.Clear()
            mtbPhoneNumber2.Clear()
            mtbFaxNumber.Clear()
            txtEmailAddress.Clear()
            txtDescription.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnLoadDefaultDescription_Click(sender As Object, e As EventArgs) Handles btnLoadDefaultDescription.Click
        Try
            txtDescription.Clear()
            txtDescription.Text = "Fee Contact. " & vbCrLf &
            "Modified by: " & CurrentUser.AlphaName & vbCrLf &
            "Modified on: " & TodayFormatted & vbCrLf &
            "Via Application # " & txtApplicationNumber.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveFeeContact_Click(sender As Object, e As EventArgs) Handles btnSaveFeeContact.Click
        Try
            If txtAIRSNumber.Text <> "" Then
                Dim SQL As String = "Select " &
                "strContactKey " &
                "from APBContactInformation " &
                "where strContactKey = @key " &
                "and strContactDescription = @desc "

                Dim p As SqlParameter() = {
                    New SqlParameter("@key", "0413" & txtAIRSNumber.Text & "40"),
                    New SqlParameter("@desc", txtDescription.Text)
                }

                If DB.ValueExists(SQL, p) Then
                    MsgBox("The Contact Description is exactly the same are a record on file." & vbCrLf &
                           "Please enter a unique description before saving.", MsgBoxStyle.Exclamation,
                           "Fee Contact Updtate")
                    Exit Sub
                End If

                If Not IsValidEmailAddress(txtEmailAddress.Text.Trim) Then
                    MsgBox("Invalid Email Address" & vbCrLf &
                           "Please enter a valid Email Address", MsgBoxStyle.Exclamation, "Fee Contact Update")
                    Exit Sub
                End If

                SQL = "Update APBContactInformation set " &
                    "strContactFirstName = @strContactFirstName, " &
                    "strContactLastName = @strContactLastName, " &
                    "strContactPrefix = @strContactPrefix, " &
                    "strContactSuffix = @strContactSuffix, " &
                    "strContactCompanyName = @strContactCompanyName, " &
                    "strContactTitle = @strContactTitle, " &
                    "strContactPhoneNumber1 = @strContactPhoneNumber1, " &
                    "strContactPhoneNumber2 = @strContactPhoneNumber2, " &
                    "strContactFaxNumber = @strContactFaxNumber, " &
                    "strContactEmail = @strContactEmail, " &
                    "strContactAddress1 = @strContactAddress1, " &
                    "strContactAddress2 = null, " &
                    "strContactCity = @strContactCity, " &
                    "strContactState = @strContactState, " &
                    "strContactZipCode = @strContactZipCode, " &
                    "strModifingPerson = @strModifingPerson, " &
                    "datModifingDate =  GETDATE() , " &
                    "strContactDescription = @strContactDescription " &
                    "where strContactKey = @strContactKey "

                Dim p2 As SqlParameter() = {
                    New SqlParameter("@strContactFirstName", txtFirstName.Text),
                    New SqlParameter("@strContactLastName", txtLastName.Text),
                    New SqlParameter("@strContactPrefix", txtSocialTitle.Text),
                    New SqlParameter("@strContactSuffix", txtPedigree.Text),
                    New SqlParameter("@strContactCompanyName", txtCompany.Text),
                    New SqlParameter("@strContactTitle", txtTitle.Text),
                    New SqlParameter("@strContactPhoneNumber1", mtbPhoneNumber1.Text),
                    New SqlParameter("@strContactPhoneNumber2", mtbPhoneNumber2.Text),
                    New SqlParameter("@strContactFaxNumber", mtbFaxNumber.Text),
                    New SqlParameter("@strContactEmail", txtEmailAddress.Text),
                    New SqlParameter("@strContactAddress1", txtAddress.Text),
                    New SqlParameter("@strContactCity", txtCity.Text),
                    New SqlParameter("@strContactState", txtState.Text),
                    New SqlParameter("@strContactZipCode", mtbZipCode.Text),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@strContactDescription", txtDescription.Text),
                    New SqlParameter("@strContactKey", "0413" & txtAIRSNumber.Text & "40")
                 }

                DB.RunCommand(SQL, p2)
                MsgBox("Contact Saved", MsgBoxStyle.Information, "Fee Contact Update")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class