Imports System.Data.SqlClient

Public Class SSPP_FeeContact
    Dim SQL As String
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader

    Private Sub SSPP_FeeContact_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Sub LoadCurrentFeeContact()
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

            SQL = "Select " &
            "strContactKey, " &
            "strContactFirstName, strContactLastName, " &
            "strContactPrefix, strContactSuffix, " &
            "strContactTitle, strContactCompanyName, " &
            "strContactPhoneNumber1, strContactPhoneNumber2, " &
            "strContactFaxNumber, strContactEmail, " &
            "strContactAddress1, strContactCity, " &
            "strContactState, strContactZipCode, " &
            "strContactDescription " &
            "from AIRBRANCH.APBContactInformation " &
            "where strContactKey = '0413" & txtAIRSNumber.Text & "40' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                dr = cmd.ExecuteReader
                While dr.Read
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
                End While
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtAIRSNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAIRSNumber.TextChanged
        Try
            LoadCurrentFeeContact()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnLoadDefaultDescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadDefaultDescription.Click
        Try
            txtDescription.Clear()
            txtDescription.Text = "Fee Contact. " & vbCrLf &
            "Modified by: " & CurrentUser.AlphaName & vbCrLf &
            "Modified on: " & OracleDate & vbCrLf &
            "Via Application # " & txtApplicationNumber.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveFeeContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFeeContact.Click
        Try
            If txtAIRSNumber.Text <> "" Then
                SQL = "Select " &
                "strContactKey " &
                "from AIRBRANCH.APBContactInformation " &
                "where strContactKey = '0413" & txtAIRSNumber.Text & "40' " &
                "and strContactDescription = '" & txtDescription.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
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

                SQL = "Update AIRBRANCH.APBContactInformation set " &
                "strContactFirstName = '" & Replace(txtFirstName.Text, "'", "''") & "', " &
                "strContactLastName = '" & Replace(txtLastName.Text, "'", "''") & "', " &
                "strContactPrefix = '" & Replace(txtSocialTitle.Text, "'", "''") & "', " &
                "strContactSuffix = '" & Replace(txtPedigree.Text, "'", "''") & "', " &
                "strContactCompanyName = '" & Replace(txtCompany.Text, "'", "''") & "', " &
                "strContactTitle = '" & Replace(txtTitle.Text, "'", "''") & "', " &
                "strContactPhoneNumber1 = '" & Replace(mtbPhoneNumber1.Text, "'", "''") & "', " &
                "strContactPhoneNumber2 = '" & Replace(mtbPhoneNumber2.Text, "'", "''") & "', " &
                "strContactFaxNumber = '" & Replace(mtbFaxNumber.Text, "'", "''") & "', " &
                "strContactEmail = '" & Replace(txtEmailAddress.Text.Trim, "'", "''") & "', " &
                "strContactAddress1 = '" & Replace(txtAddress.Text, "'", "''") & "', " &
                "strContactAddress2 = '', " &
                "strContactCity = '" & Replace(txtCity.Text, "'", "''") & "', " &
                "strContactState = '" & Replace(txtState.Text, "'", "''") & "', " &
                "strContactZipCode = '" & Replace(mtbZipCode.Text, "'", "''") & "', " &
                "strModifingPerson = '" & CurrentUser.UserID & "', " &
                "datModifingDate = '" & OracleDate & "', " &
                "strContactDescription = '" & Replace(txtDescription.Text, "'", "''") & "' " &
                "where strContactKey = '0413" & txtAIRSNumber.Text & "40' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                MsgBox("Contact Saved", MsgBoxStyle.Information, "Fee Contact Update")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


End Class