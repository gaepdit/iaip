Imports Oracle.DataAccess.Client

Public Class IAIPProfileUpdate

    Private Sub IAIPProfileUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
    End Sub

    Private Sub btnUpdateEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateEmail.Click
        Try
            Dim myInput As String = txtEmailAddress.Text.Trim()

            Dim pattern As String = "^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$"

            Dim myRegEx As New System.Text.RegularExpressions.Regex(pattern)

            If myRegEx.IsMatch(myInput) Then
                ' MessageBox.Show("E-Mail is valid")

                SQL = "Update " & DBNameSpace & ".EPDUserProfiles set " & _
                "strEmailAddress = '" & txtEmailAddress.Text & "' " & _
                "where numUserID = '" & UserGCode & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                txtEmailAddress.Clear()
                txtEmailAddress.BackColor = Color.White

            Else
                MessageBox.Show("Please enter a valid E-Mail address!!")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdatePhoneNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePhoneNumber.Click
        Try
            Dim myInput As String = mtbPhoneNumber.Text.Trim()
            Dim pattern As String = "^1?\s*-?\s*(\d{3}|\(\s*\d{3}\s*\))\s*-?\s*\d{3}\s*-?\s*\d{4}$"
            Dim myRegEx As New System.Text.RegularExpressions.Regex(pattern)
            If myRegEx.IsMatch(myInput) Then
                SQL = "Update " & DBNameSpace & ".EPDUserProfiles set " & _
                "strPhone = '" & mtbPhoneNumber.Text & "' " & _
                "where numuserID = '" & UserGCode & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                mtbPhoneNumber.Clear()
                mtbPhoneNumber.BackColor = Color.White
            Else
                MessageBox.Show("Please enter a valid Phone Number!!")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdatePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePassword.Click
        Try
            If txtUserPassword.Text <> "" And txtConfirmPassword.Text <> "" Then
                If txtUserPassword.Text = txtConfirmPassword.Text Then
                    SQL = "Update " & DBNameSpace & ".EPDUsers set " & _
                    "strPassword = '" & Replace(EncryptDecrypt.EncryptText(txtUserPassword.Text), "'", "''") & "' " & _
                    "where numUserId = '" & UserGCode & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    txtUserPassword.Clear()
                    txtConfirmPassword.Clear()
                    txtUserPassword.BackColor = Color.White
                    txtConfirmPassword.BackColor = Color.White
                Else
                    MessageBox.Show("Passwords must match")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateAllData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateAllData.Click
        Try
            Dim myInput As String = txtEmailAddress.Text.Trim()
            Dim pattern As String = "^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$"
            Dim myRegEx As New System.Text.RegularExpressions.Regex(pattern)

            If myRegEx.IsMatch(myInput) Then
                ' MessageBox.Show("E-Mail is valid")

                SQL = "Update " & DBNameSpace & ".EPDUserProfiles set " & _
                "strEmailAddress = '" & txtEmailAddress.Text & "' " & _
                "where numUserID = '" & UserGCode & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                'txtEmailAddress.Clear()
                txtEmailAddress.BackColor = Color.White
            Else
                MessageBox.Show("Please enter a valid E-Mail address!!" & vbCrLf & _
                                "All data not saved.", Me.Text, MessageBoxButtons.OK)
                Exit Sub
            End If

            Dim myInput2 As String = mtbPhoneNumber.Text.Trim()
            Dim pattern2 As String = "^1?\s*-?\s*(\d{3}|\(\s*\d{3}\s*\))\s*-?\s*\d{3}\s*-?\s*\d{4}$"
            Dim myRegEx2 As New System.Text.RegularExpressions.Regex(pattern)
            If myRegEx2.IsMatch(myInput) Then
                SQL = "Update " & DBNameSpace & ".EPDUserProfiles set " & _
                "strPhone = '" & mtbPhoneNumber.Text & "' " & _
                "where numuserID = '" & UserGCode & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                ' mtbPhoneNumber.Clear()
                mtbPhoneNumber.BackColor = Color.White
            Else
                MessageBox.Show("Please enter a valid Phone Number!!" & vbCrLf & _
                                "All data not saved.", Me.Text, MessageBoxButtons.OK)
                Exit Sub
            End If

            If txtUserPassword.Text <> "" And txtConfirmPassword.Text <> "" Then
                If txtUserPassword.Text = txtConfirmPassword.Text Then
                    SQL = "Update " & DBNameSpace & ".EPDUsers set " & _
                    "strPassword = '" & Replace(EncryptDecrypt.EncryptText(txtUserPassword.Text), "'", "''") & "' " & _
                    "where numUserId = '" & UserGCode & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    ' txtUserPassword.Clear()
                    ' txtConfirmPassword.Clear()
                    txtUserPassword.BackColor = Color.White
                    txtConfirmPassword.BackColor = Color.White
                Else
                    MessageBox.Show("Passwords must match" & vbCrLf & _
                                "All data not saved.", Me.Text, MessageBoxButtons.OK)
                    Exit Sub
                End If
            Else
                If txtUserPassword.BackColor = Color.Red Then
                    MessageBox.Show("Please update password." & vbCrLf & _
                                          "All data not saved.", Me.Text, MessageBoxButtons.OK)
                    Exit Sub
                End If
            End If

            MessageBox.Show("All data saved", Me.Text, MessageBoxButtons.OK)
            ProfileUpdate.Close()
            ProfileUpdate = Nothing

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class