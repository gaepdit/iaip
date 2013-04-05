'Imports System.DateTime
Imports System.Data.OracleClient


Public Class IAIPDistrictSourceTool
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsCounty As DataSet
    Dim daCounty As OracleDataAdapter
    Dim dsDistrict As DataSet
    Dim daDistrict As OracleDataAdapter
    Dim dsDistrictStaff As DataSet
    Dim daDistrictStaff As OracleDataAdapter


    Private Sub IAIPDistrictSourcesTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            LoadDistrictListBox()
            LoadDistrictStaff()
            LoadCountyListBox("PageLoad")


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

#Region "Page Load"
    Sub LoadCountyListBox(ByVal LoadSource As String)
        Dim SQLLine As String = ""

        Try

            clbCounties.Items.Clear()

            Select Case LoadSource
                Case "PageLoad"
                    SQL = "Select strCountyName, strCountyCode " & _
                    "from " & connNameSpace & ".LookUpCountyInformation " & _
                    "Order by strCountyName"
                Case "District"
                    For x As Integer = 0 To clbDistricts.Items.Count - 1
                        If clbDistricts.GetItemChecked(x) = True Then
                            clbDistricts.SelectedIndex = x
                            SQLLine = SQLLine & "strDistrictCode = '" & clbDistricts.SelectedValue & "' OR "
                        End If
                    Next
                    Select Case SQLLine
                        Case ""
                            SQLLine = ""
                        Case Else
                            SQLLine = "AND (" & Mid(SQLLine, 1, Len(SQLLine) - 4) & ") "
                    End Select

                    SQL = "Select strCountyName, strCountyCode " & _
                    "from " & connNameSpace & ".LookUpCountyInformation, " & connNameSpace & ".LookUpDistrictInformation " & _
                    "Where strCountyCode = strDistrictCounty " & _
                    " " & SQLLine & " Order by strCountyName "

            End Select

            cmd = New OracleCommand(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                clbCounties.Items.Add(dr.Item("strCountyname"))
            End While

            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub LoadDistrictListBox()
        Dim dtDistrict As New DataTable
        Dim dtDistrict2 As New DataTable
        Dim dtDistrict3 As New DataTable
        Dim dtDistrict4 As New DataTable
        Dim drDSRow As DataRow
        Dim drDSRow2 As DataRow
        Dim drDSRow3 As DataRow
        Dim drDSRow4 As DataRow
        Dim drNewRow As DataRow
        Dim drNewRow2 As DataRow
        Dim drNewRow3 As DataRow
        Dim drNewRow4 As DataRow

        Try

            SQL = "Select strDistrictName, strDistrictCode " & _
                   "from " & connNameSpace & ".LookUPDistricts " & _
                   "order by StrDistrictName"

            dsDistrict = New DataSet
            daDistrict = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daDistrict.Fill(dsDistrict, "District")
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            dtDistrict.Columns.Add("strDistrictName", GetType(System.String))
            dtDistrict.Columns.Add("strDistrictCode", GetType(System.String))
            dtDistrict2.Columns.Add("strDistrictName", GetType(System.String))
            dtDistrict2.Columns.Add("strDistrictCode", GetType(System.String))
            dtDistrict3.Columns.Add("strDistrictName", GetType(System.String))
            dtDistrict3.Columns.Add("strDistrictCode", GetType(System.String))
            dtDistrict4.Columns.Add("strDistrictName", GetType(System.String))
            dtDistrict4.Columns.Add("strDistrictCode", GetType(System.String))

            drNewRow = dtDistrict.NewRow()
            drNewRow("strDistrictName") = " "
            drNewRow("strDistrictCode") = " "
            dtDistrict.Rows.Add(drNewRow)

            drNewRow3 = dtDistrict3.NewRow()
            drNewRow3("strDistrictName") = " "
            drNewRow3("strDistrictCode") = " "
            dtDistrict3.Rows.Add(drNewRow3)

            For Each drDSRow In dsDistrict.Tables("District").Rows()
                drNewRow = dtDistrict.NewRow()
                drNewRow("strDistrictName") = drDSRow("strDistrictName")
                drNewRow("strDistrictCode") = drDSRow("strDistrictCode")
                dtDistrict.Rows.Add(drNewRow)
            Next
            For Each drDSRow2 In dsDistrict.Tables("District").Rows()
                drNewRow2 = dtDistrict2.NewRow()
                drNewRow2("strDistrictName") = drDSRow2("strDistrictName") & " - " & drDSRow2("strDistrictCode")
                drNewRow2("strDistrictCode") = drDSRow2("strDistrictCode")
                dtDistrict2.Rows.Add(drNewRow2)
            Next
            For Each drDSRow3 In dsDistrict.Tables("District").Rows()
                drNewRow3 = dtDistrict3.NewRow()
                drNewRow3("strDistrictName") = drDSRow3("strDistrictName")
                drNewRow3("strDistrictCode") = drDSRow3("strDistrictCode")
                dtDistrict3.Rows.Add(drNewRow3)
            Next
            For Each drDSRow4 In dsDistrict.Tables("District").Rows()
                drNewRow4 = dtDistrict4.NewRow()
                drNewRow4("strDistrictName") = drDSRow4("strDistrictName")
                drNewRow4("strDistrictCode") = drDSRow4("strDistrictCode")
                dtDistrict4.Rows.Add(drNewRow4)
            Next

            With cboDistricts
                .DataSource = dtDistrict
                .DisplayMember = "strDistrictName"
                .ValueMember = "strDistrictCode"
                .SelectedIndex = 0
            End With

            With lsbDistricts
                .DataSource = dtDistrict2
                .DisplayMember = "strDistrictName"
                .ValueMember = "strDistrictName"
                .SelectedIndex = 0
            End With

            With cboDistrictToRemove
                .DataSource = dtDistrict3
                .DisplayMember = "strDistrictName"
                .ValueMember = "strDistrictCode"
                .SelectedIndex = 0
            End With

            With clbDistricts
                .DataSource = dtDistrict4
                .DisplayMember = "strDistrictName"
                .ValueMember = "strDistrictCode"
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub LoadDistrictStaff()
        Try
            Dim dtDistrictStaff As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select  " & _
            "(strFirstName||' '||strLastName) as Username,   " & _
            "" & connNameSpace & ".EPDUserProfiles.numUserID,  " & _
            "striaipPermissions  " & _
            "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".IAIPPermissions     " & _
            "where " & connNameSpace & ".EPDUserProfiles.numUserID = " & connNameSpace & ".IAIPPermissions.numUserID  " & _
            "and ((numProgram = '4' and numUnit is null)  " & _
            "or strIAIPPermissions like '%(21)%'  " & _
            "or strIAIPPermissions like '%(23)%' " & _
            "or strIAIPPermissions like '%(25)%' " & _
            "or (numBranch = '5' and numProgram <> '35' and numEmployeeStatus = '1')) "

            dsDistrictStaff = New DataSet
            daDistrictStaff = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daDistrictStaff.Fill(dsDistrictStaff, "DistrictStaff")

            dtDistrictStaff.Columns.Add("Username", GetType(System.String))
            dtDistrictStaff.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtDistrictStaff.NewRow()
            drNewRow("Username") = " "
            drNewRow("numUserID") = " "
            dtDistrictStaff.Rows.Add(drNewRow)

            For Each drDSRow In dsDistrictStaff.Tables("DistrictStaff").Rows()
                drNewRow = dtDistrictStaff.NewRow()
                drNewRow("Username") = drDSRow("Username")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtDistrictStaff.Rows.Add(drNewRow)
            Next

            With cboDistrictManager
                .DataSource = dtDistrictStaff
                .DisplayMember = "Username"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region
#Region "Subs and Functions"
    Sub SaveDistrictList()
        Dim strObject As String

        Try

            If cboDistricts.SelectedIndex > 0 Then
                For Each strObject In clbCounties.CheckedItems
                    SQL = "Update " & connNameSpace & ".LookupDistrictInformation set " & _
                    "strDistrictCode = '" & cboDistricts.SelectedValue & "' " & _
                    "where strDistrictCounty = (select strCountyCode " & _
                    "from " & connNameSpace & ".LookUpCountyInformation where " & _
                    "strCountyName = '" & strObject & "') "

                    cmd = New OracleCommand(SQL, conn)

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                Next
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveNewDistricts()
        Try

            If txtNewDistrict.Text <> "" And txtNewDistrictCode.Text <> "" Then
                SQL = "Select strDistrictCode from " & connNameSpace & ".LookUPDistricts " & _
                "where strDistrictCode = '" & txtNewDistrictCode.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    SQL = "Update " & connNameSpace & ".LookUPDistricts set " & _
                    "strDistrictName = '" & txtNewDistrict.Text & "', " & _
                    "strDistrictManager = '" & cboDistrictManager.SelectedValue & "' " & _
                    "where strDistrictCode = '" & txtNewDistrictCode.Text & "' "
                Else
                    SQL = "Insert into " & connNameSpace & ".LookUPDistricts " & _
                    "(strDistrictCode, strDistrictName, " & _
                    "strDistrictManager) " & _
                    "values " & _
                    "('" & txtNewDistrictCode.Text & "', " & _
                    "'" & txtNewDistrict.Text & "', " & _
                    "'" & cboDistrictManager.SelectedValue & "') "
                End If

                cmd = New OracleCommand(SQL, conn)
                dr = cmd.ExecuteReader

                LoadDistrictListBox()

                lsbDistricts.SelectedValue = txtNewDistrict.Text & " - " & txtNewDistrictCode.Text

                If conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            End If

            If chbRemoveDistrict.Checked = True Then
                SQL = "Delete " & connNameSpace & ".LookUPDistricts " & _
                "where strDistrictCode = '" & Me.cboDistrictToRemove.SelectedValue & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                dr = cmd.ExecuteReader

                If conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
                LoadDistrictListBox()
                chbRemoveDistrict.Checked = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub Clear()
        Try

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub Back()
        Try

            IAIPDistrictTool = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region
#Region "Declaration"
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("^(X)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("^(C)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("^(V)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try

            Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
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
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub TBManagingDistricts_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBManagingDistricts.ButtonClick
        Try

            Select Case TBManagingDistricts.Buttons.IndexOf(e.Button)
                Case 0
                    Clear()
                Case 1
                    Back()
                Case Else

            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub llbViewDistricts_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewDistricts.LinkClicked
        Try

            Select Case clbDistricts.CheckedItems.Count
                Case 0
                    MsgBox("Please select a District from the right.", MsgBoxStyle.Information, "SSCP District Liasion")
                Case Else
                    LoadCountyListBox("District")
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnCheckAllCounties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckAllCounties.Click
        Dim i As Integer

        Try

            For i = 0 To clbCounties.Items.Count - 1
                clbCounties.SetItemChecked(i, True)
            Next
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnClearChecks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearChecks.Click
        Dim i As Integer
        Try

            For i = 0 To clbCounties.Items.Count - 1
                clbCounties.SetItemChecked(i, False)
            Next

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub IAIPDistrictSourcesTool_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub BtnSaveDistricts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveDistricts.Click
        Try

            SaveDistrictList()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnAddUpdateInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUpdateInfo.Click
        Try

            SaveNewDistricts()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub lsbDistricts_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lsbDistricts.MouseUp
        Try
            txtNewDistrictCode.Text = Mid(lsbDistricts.SelectedValue, ((lsbDistricts.SelectedValue).ToString.IndexOf("-") + 3))

            If txtNewDistrictCode.Text <> "" Then
                SQL = "Select " & _
                "strDistrictName, strDistrictManager " & _
                "from " & connNameSpace & ".LookUPDistricts " & _
                "where strDistrictCode = '" & txtNewDistrictCode.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strDistrictName")) Then
                        txtNewDistrict.Clear()
                    Else
                        txtNewDistrict.Text = dr.Item("strDistrictName")
                    End If
                    If IsDBNull(dr.Item("strDistrictManager")) Then
                        cboDistrictManager.Text = ""
                        cboDistrictManager.SelectedIndex = 0
                    Else
                        Dim temp As String = ""
                        temp = dr.Item("strDistrictManager")

                        cboDistrictManager.SelectedValue = dr.Item("strDistrictManager")
                    End If
                Else
                    txtNewDistrict.Clear()
                    cboDistrictManager.Text = ""
                    cboDistrictManager.SelectedIndex = 0
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region









    Private Sub btnViewDistrict_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewDistrict.Click
        Dim CountyName As String = ""
        Dim i As Integer = 0

        Try

            clbCounties.Items.Clear()

            SQL = "Select strCountyName, strCountyCode " & _
            "from " & connNameSpace & ".LookUpCountyInformation " & _
            "Order by strCountyName"

            cmd = New OracleCommand(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                CountyName = dr.Item("strCountyname")
                clbCounties.Items.Add(CountyName)

                SQL2 = "Select " & _
                "strCountyName " & _
                "from " & connNameSpace & ".LookUpCountyInformation, " & connNameSpace & ".LookUpDistrictInformation " & _
                "Where strCountyCode = strDistrictCounty " & _
                "and strDistrictCode = '" & clbDistricts.SelectedValue & "' " & _
                "and strCountyName = '" & CountyName & "' "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr2 = cmd.ExecuteReader

                recExist = dr2.Read
                If recExist = True Then
                    If dr2.Item("strCountyName") = CountyName Then
                        clbCounties.SetItemChecked(i, True)
                    Else
                        clbCounties.SetItemChecked(i, False)
                    End If
                End If
                dr2.Close()
                i += 1
            End While
            dr.Close()



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
End Class