Imports System.Data.SqlClient

Public Class ISMPAddPollutants

    Private Sub ISMPAddPollutants_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            FormatdgrPollutants()
            LoadDataSet()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub LoadDataSet()
        Try

            Dim query As String = "Select strPollutantCode, strPOllutantDescription " &
                 "from LookUPPollutants " &
                 "Order by strPollutantDescription"

            Dim dt As DataTable = DB.GetDataTable(query)
            dt.TableName = "Pollutant"
            dgrPollutants.DataSource = dt

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub FormatdgrPollutants()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Pollutant"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strPollutantCode"
            objtextcol.HeaderText = "Pollutant Code"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strPOllutantDescription"
            objtextcol.HeaderText = "Pollutant Description"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 400
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrPollutants.TableStyles.Clear()
            dgrPollutants.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrPollutants.CaptionText = "Pollutants"
            dgrPollutants.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub Save()
        Try

            If String.IsNullOrWhiteSpace(txtPollutant.Text) OrElse String.IsNullOrWhiteSpace(txtPollutantCode.Text) Then
                MessageBox.Show("Enter a pollutant code and description before saving.")
                Exit Sub
            End If

            Dim query As String = "Select convert(bit,count(*)) " &
                "from LookUPPollutants " &
                "where strPollutantcode = @code "

            Dim p As SqlParameter() = {
                New SqlParameter("@code", txtPollutantCode.Text),
                New SqlParameter("@desc", txtPollutant.Text)
            }

            If DB.GetBoolean(query, p) Then
                query = "Update LookUPPollutants set " &
                        "strPollutantDescription = @desc " &
                        "where strPollutantCode = @code "
            Else
                query = "Insert into LookUPPollutants " &
                        "(strPollutantCode, strPollutantDescription) " &
                        "values " &
                        "(@code, @desc) "
            End If

            DB.RunCommand(query, p)

            LoadDataSet()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    Private Sub dgrPollutants_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrPollutants.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrPollutants.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrPollutants(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrPollutants(hti.Row, 1)) Then
                    Else
                        txtPollutantCode.Text = dgrPollutants(hti.Row, 0)
                        txtPollutant.Text = dgrPollutants(hti.Row, 1)
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub

End Class

