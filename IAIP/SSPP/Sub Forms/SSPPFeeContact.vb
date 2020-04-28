Imports System.Data.SqlClient

Public Class SSPPFeeContact

    Private _airsId As Apb.ApbFacilityId
    Public Property AirsId As Apb.ApbFacilityId
        Get
            Return _airsId
        End Get
        Set(value As Apb.ApbFacilityId)
            ArgumentNotNull(value, NameOf(value))
            _airsId = value
            lblAirs.Text = "AIRS #" & _airsId.FormattedString
            LoadCurrentFeeContact()
        End Set
    End Property

    Private _appNum As Integer = 0
    Public Property AppNumber As Integer
        Get
            Return _appNum
        End Get
        Set(value As Integer)
            _appNum = value
            lblApp.Text = "Application " & _appNum.ToString
        End Set
    End Property

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
            txtPhoneNumber1.Clear()
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

            Dim p As New SqlParameter("@airs", AirsId.DbFormattedString & "40")

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtSocialTitle.Clear()
                Else
                    txtSocialTitle.Text = dr.Item("strContactPrefix").ToString
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtFirstName.Clear()
                Else
                    txtFirstName.Text = dr.Item("strContactFirstName").ToString
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtLastName.Clear()
                Else
                    txtLastName.Text = dr.Item("strContactLastName").ToString
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtPedigree.Clear()
                Else
                    txtPedigree.Text = dr.Item("strContactSuffix").ToString
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtTitle.Clear()
                Else
                    txtTitle.Text = dr.Item("strContactTitle").ToString
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtCompany.Clear()
                Else
                    txtCompany.Text = dr.Item("strContactCompanyName").ToString
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                    txtPhoneNumber1.Clear()
                Else
                    txtPhoneNumber1.Text = dr.Item("strContactPhoneNumber1").ToString
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber2")) Then
                    mtbPhoneNumber2.Clear()
                Else
                    mtbPhoneNumber2.Text = dr.Item("strContactPhoneNumber2").ToString
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    mtbFaxNumber.Clear()
                Else
                    mtbFaxNumber.Text = dr.Item("strContactFaxNumber").ToString
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEmailAddress.Clear()
                Else
                    txtEmailAddress.Text = dr.Item("strContactEmail").ToString
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtAddress.Clear()
                Else
                    txtAddress.Text = dr.Item("strContactAddress1").ToString
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtCity.Clear()
                Else
                    txtCity.Text = dr.Item("strContactCity").ToString
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtState.Clear()
                Else
                    txtState.Text = dr.Item("strContactState").ToString
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    mtbZipCode.Clear()
                Else
                    mtbZipCode.Text = dr.Item("strContactZipCode").ToString
                End If
                If IsDBNull(dr.Item("strContactDescription")) Then
                    txtDescription.Clear()
                Else
                    txtDescription.Text = dr.Item("strContactDescription").ToString
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
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
        txtPhoneNumber1.Clear()
        mtbPhoneNumber2.Clear()
        mtbFaxNumber.Clear()
        txtEmailAddress.Clear()
        txtDescription.Clear()
    End Sub

    Private Sub btnLoadDefaultDescription_Click(sender As Object, e As EventArgs) Handles btnLoadDefaultDescription.Click
        txtDescription.Text = "Fee Contact. " & vbCrLf &
        "Modified by: " & CurrentUser.AlphaName & vbCrLf &
        "Modified on: " & TodayFormatted & vbCrLf &
        "Via Application # " & AppNumber.ToString
    End Sub

    Private Sub btnSaveFeeContact_Click(sender As Object, e As EventArgs) Handles btnSaveFeeContact.Click
        If AirsId Is Nothing Then
            Return
        End If

        Try
            Dim SQL As String = "Select " &
            "strContactKey " &
            "from APBContactInformation " &
            "where strContactKey = @key " &
            "and strContactDescription = @desc "

            Dim p As SqlParameter() = {
                New SqlParameter("@key", AirsId.DbFormattedString & "40"),
                New SqlParameter("@desc", txtDescription.Text)
            }

            If DB.ValueExists(SQL, p) Then
                MsgBox("The Contact Description is exactly the same as a record on file." & vbCrLf &
                       "Please enter a unique description before saving.", MsgBoxStyle.Exclamation,
                       "Fee Contact Update")
                Return
            End If

            If Not IsValidEmailAddress(txtEmailAddress.Text.Trim) Then
                MsgBox("Invalid Email Address" & vbCrLf &
                       "Please enter a valid Email Address", MsgBoxStyle.Exclamation, "Fee Contact Update")
                Return
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
                New SqlParameter("@strContactPhoneNumber1", txtPhoneNumber1.Text),
                New SqlParameter("@strContactPhoneNumber2", mtbPhoneNumber2.Text),
                New SqlParameter("@strContactFaxNumber", mtbFaxNumber.Text),
                New SqlParameter("@strContactEmail", txtEmailAddress.Text),
                New SqlParameter("@strContactAddress1", txtAddress.Text),
                New SqlParameter("@strContactCity", txtCity.Text),
                New SqlParameter("@strContactState", txtState.Text),
                New SqlParameter("@strContactZipCode", mtbZipCode.Text),
                New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                New SqlParameter("@strContactDescription", txtDescription.Text),
                New SqlParameter("@strContactKey", AirsId.DbFormattedString & "40")
             }

            DB.RunCommand(SQL, p2)
            MsgBox("Contact Saved", MsgBoxStyle.Information, "Fee Contact Update")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class