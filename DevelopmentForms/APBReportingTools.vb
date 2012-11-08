Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports System.Xml
Imports System.Windows.Forms.RichTextBox


''' The extended Richtextbox was found on codeproject.com. It is a widely available .dll done by Microsoft. 
''' 
''' <summary>   
''' Rich Text Editor
''' Project demonstrates using an extended version of the rich text box control
''' to manipulate, store, recover, and print rich text, normal text, and html files.
''' </summary>
''' <remarks>The extended rich text box control was developed by Microsoft; it is
''' included with this project in the separate class library</remarks>

Public Class APBReportingTools
    Dim myFont As Font
    Private currentFile As String
    Private checkPrint As Integer
    Private Sub APBBranchTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

        Catch ex As Exception

        End Try
    End Sub


#Region "Extended Richtextbox code"

#Region "Menu Methods"
    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        Try
            If rtbOrgChart.Modified Then

                Dim answer As Integer
                answer = MessageBox.Show("The current document has not been saved, would you like to continue without saving?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If answer = Windows.Forms.DialogResult.Yes Then
                    rtbOrgChart.Clear()
                Else
                    Exit Sub
                End If

            Else

                rtbOrgChart.Clear()

            End If

            currentFile = ""
            Me.Text = "Editor: New Document"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click

        If rtbOrgChart.Modified Then

            Dim answer As Integer
            answer = MessageBox.Show("The current document has not been saved, would you like to continue without saving?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If answer = Windows.Forms.DialogResult.No Then
                Exit Sub

            Else

                OpenFile()

            End If

        Else

            OpenFile()

        End If

    End Sub
    Private Sub OpenFile()

        OpenFileDialog1.Title = "RTE - Open File"
        OpenFileDialog1.DefaultExt = "rtf"
        OpenFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.ShowDialog()

        If OpenFileDialog1.FileName = "" Then Exit Sub

        Dim strExt As String
        strExt = System.IO.Path.GetExtension(OpenFileDialog1.FileName)
        strExt = strExt.ToUpper()

        Select Case strExt
            Case ".RTF"
                rtbOrgChart.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.RichText)
            Case Else
                Dim txtReader As System.IO.StreamReader
                txtReader = New System.IO.StreamReader(OpenFileDialog1.FileName)
                rtbOrgChart.Text = txtReader.ReadToEnd
                txtReader.Close()
                txtReader = Nothing
                rtbOrgChart.SelectionStart = 0
                rtbOrgChart.SelectionLength = 0
        End Select

        currentFile = OpenFileDialog1.FileName
        rtbOrgChart.Modified = False
        Me.Text = "Editor: " & currentFile.ToString()

    End Sub
    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            If currentFile = "" Then
                SaveAsToolStripMenuItem_Click(Me, e)
                Exit Sub
            End If

            Dim strExt As String
            strExt = System.IO.Path.GetExtension(currentFile)
            strExt = strExt.ToUpper()

            Select Case strExt
                Case ".RTF"
                    rtbOrgChart.SaveFile(currentFile)
                Case Else
                    ' to save as plain text
                    Dim txtWriter As System.IO.StreamWriter
                    txtWriter = New System.IO.StreamWriter(currentFile)
                    txtWriter.Write(rtbOrgChart.Text)
                    txtWriter.Close()
                    txtWriter = Nothing
                    rtbOrgChart.SelectionStart = 0
                    rtbOrgChart.SelectionLength = 0
                    rtbOrgChart.Modified = False
            End Select

            Me.Text = "Editor: " & currentFile.ToString()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        Try
            SaveFileDialog1.Title = "RTE - Save File"
            SaveFileDialog1.DefaultExt = "rtf"
            SaveFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*"
            SaveFileDialog1.FilterIndex = 1
            SaveFileDialog1.ShowDialog()

            If SaveFileDialog1.FileName = "" Then Exit Sub

            Dim strExt As String
            strExt = System.IO.Path.GetExtension(SaveFileDialog1.FileName)
            strExt = strExt.ToUpper()

            Select Case strExt
                Case ".RTF"
                    rtbOrgChart.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText)
                Case Else
                    Dim txtWriter As System.IO.StreamWriter
                    txtWriter = New System.IO.StreamWriter(SaveFileDialog1.FileName)
                    txtWriter.Write(rtbOrgChart.Text)
                    txtWriter.Close()
                    txtWriter = Nothing
                    rtbOrgChart.SelectionStart = 0
                    rtbOrgChart.SelectionLength = 0
            End Select

            currentFile = SaveFileDialog1.FileName
            rtbOrgChart.Modified = False
            Me.Text = "Editor: " & currentFile.ToString()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Try
            If rtbOrgChart.Modified Then

                Dim answer As Integer
                answer = MessageBox.Show("The current document has not been saved, would you like to continue without saving?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If answer = Windows.Forms.DialogResult.No Then
                    Exit Sub
                Else
                    ReportingTools = Nothing
                    Me.Close()
                End If
            Else
                ReportingTools = Nothing
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        Try

            rtbOrgChart.SelectAll()

        Catch exc As Exception

            MessageBox.Show("Unable to select all document content.", "RTE - Select", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click

        Try

            rtbOrgChart.Copy()

        Catch exc As Exception

            MessageBox.Show("Unable to copy document content.", "RTE - Copy", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click

        Try

            rtbOrgChart.Cut()

        Catch exc As Exception

            MessageBox.Show("Unable to cut document content.", "RTE - Cut", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click

        Try
            rtbOrgChart.Paste()

        Catch exc As Exception

            MessageBox.Show("Unable to copy clipboard content to document.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    Private Sub SelectFontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectFontToolStripMenuItem.Click
        Try
            If Not rtbOrgChart.SelectionFont Is Nothing Then
                FontDialog1.Font = rtbOrgChart.SelectionFont
            Else
                FontDialog1.Font = Nothing
            End If

            FontDialog1.ShowApply = True

            If FontDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                rtbOrgChart.SelectionFont = FontDialog1.Font
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FontColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontColorToolStripMenuItem.Click
        Try
            ColorDialog1.Color = rtbOrgChart.ForeColor

            If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                rtbOrgChart.SelectionColor = ColorDialog1.Color
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub BoldToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BoldToolStripMenuItem.Click
        Try
            ItalicToolStripMenuItem.Checked = False
            UnderlineToolStripMenuItem.Checked = False

            If BoldToolStripMenuItem.Checked = False Then
                tbrBold.Checked = False
            Else
                tbrBold.Checked = True
            End If

            If Not rtbOrgChart.SelectionFont Is Nothing Then

                Dim currentFont As System.Drawing.Font = rtbOrgChart.SelectionFont
                Dim newFontStyle As System.Drawing.FontStyle

                If rtbOrgChart.SelectionFont.Bold = True Then
                    newFontStyle = FontStyle.Regular
                Else
                    newFontStyle = FontStyle.Bold
                End If

                rtbOrgChart.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub ItalicToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItalicToolStripMenuItem.Click
        Try
            BoldToolStripMenuItem.Checked = False
            UnderlineToolStripMenuItem.Checked = False

            If ItalicToolStripMenuItem.Checked = False Then
                tbrItalic.Checked = False
            Else
                tbrItalic.Checked = True
            End If

            If Not rtbOrgChart.SelectionFont Is Nothing Then

                Dim currentFont As System.Drawing.Font = rtbOrgChart.SelectionFont
                Dim newFontStyle As System.Drawing.FontStyle

                If rtbOrgChart.SelectionFont.Italic = True Then
                    newFontStyle = FontStyle.Regular
                Else
                    newFontStyle = FontStyle.Italic
                End If

                rtbOrgChart.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub UnderlineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnderlineToolStripMenuItem.Click
        Try
            BoldToolStripMenuItem.Checked = False
            ItalicToolStripMenuItem.Checked = False

            If UnderlineToolStripMenuItem.Checked = False Then
                tbrUnderline.Checked = False
            Else
                tbrUnderline.Checked = True

            End If
            If Not rtbOrgChart.SelectionFont Is Nothing Then

                Dim currentFont As System.Drawing.Font = rtbOrgChart.SelectionFont
                Dim newFontStyle As System.Drawing.FontStyle

                If rtbOrgChart.SelectionFont.Underline = True Then
                    newFontStyle = FontStyle.Regular
                Else
                    newFontStyle = FontStyle.Underline
                End If

                rtbOrgChart.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub NormalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NormalToolStripMenuItem.Click

        Try
            BoldToolStripMenuItem.Checked = False
            ItalicToolStripMenuItem.Checked = False
            UnderlineToolStripMenuItem.Checked = False

            If Not rtbOrgChart.SelectionFont Is Nothing Then

                Dim currentFont As System.Drawing.Font = rtbOrgChart.SelectionFont
                Dim newFontStyle As System.Drawing.FontStyle
                newFontStyle = FontStyle.Regular

                rtbOrgChart.SelectionFont = New Font(currentFont.FontFamily, currentFont.Size, newFontStyle)

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub PageColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageColorToolStripMenuItem.Click
        Try
            ColorDialog1.Color = rtbOrgChart.BackColor

            If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                rtbOrgChart.BackColor = ColorDialog1.Color
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mnuUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUndo.Click
        Try
            If rtbOrgChart.CanUndo Then rtbOrgChart.Undo()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mnuRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRedo.Click
        Try
            If rtbOrgChart.CanRedo Then rtbOrgChart.Redo()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub LeftToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftToolStripMenuItem.Click
        Try
            tbrCenter.Checked = False
            tbrRight.Checked = False
            CenterToolStripMenuItem.Checked = False
            RightToolStripMenuItem.Checked = False

            If LeftToolStripMenuItem.Checked = False Then
                tbrLeft.Checked = False
            Else
                tbrLeft.Checked = True
            End If

            rtbOrgChart.SelectionAlignment = HorizontalAlignment.Left
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub CenterToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CenterToolStripMenuItem.Click
        Try
            tbrLeft.Checked = False
            tbrRight.Checked = False
            LeftToolStripMenuItem.Checked = False
            RightToolStripMenuItem.Checked = False

            If CenterToolStripMenuItem.Checked = False Then
                tbrCenter.Checked = False
            Else
                tbrCenter.Checked = True
            End If

            rtbOrgChart.SelectionAlignment = HorizontalAlignment.Center
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub RightToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightToolStripMenuItem.Click
        Try
            tbrLeft.Checked = False
            tbrCenter.Checked = False
            LeftToolStripMenuItem.Checked = False
            CenterToolStripMenuItem.Checked = False

            If RightToolStripMenuItem.Checked = False Then
                tbrRight.Checked = False
            Else
                tbrRight.Checked = True
            End If

            rtbOrgChart.SelectionAlignment = HorizontalAlignment.Right
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub AddBulletsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddBulletsToolStripMenuItem.Click
        Try
            rtbOrgChart.BulletIndent = 10
            rtbOrgChart.SelectionBullet = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub RemoveBulletsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveBulletsToolStripMenuItem.Click
        Try
            rtbOrgChart.SelectionBullet = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mnuIndent0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIndent0.Click
        Try
            rtbOrgChart.SelectionIndent = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mnuIndent5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIndent5.Click
        Try
            rtbOrgChart.SelectionIndent = 5
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mnuIndent10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIndent10.Click
        Try
            rtbOrgChart.SelectionIndent = 10
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mnuIndent15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIndent15.Click
        Try
            rtbOrgChart.SelectionIndent = 15
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mnuIndent20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIndent20.Click
        Try
            rtbOrgChart.SelectionIndent = 20
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FindToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindToolStripMenuItem.Click
        Try
            Dim f As New frmFind()
            f.Show()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FindAndReplaceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindAndReplaceToolStripMenuItem.Click
        Try
            Dim f As New frmReplace()
            f.Show()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub PreviewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviewToolStripMenuItem.Click
        Try
            PrintPreviewDialog1.Document = PrintDocument1
            PrintPreviewDialog1.ShowDialog()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Try
            PrintDialog1.Document = PrintDocument1

            If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                PrintDocument1.Print()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mnuPageSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPageSetup.Click
        Try
            PageSetupDialog1.Document = PrintDocument1
            PageSetupDialog1.ShowDialog()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub InsertImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertImageToolStripMenuItem.Click

        OpenFileDialog1.Title = "RTE - Insert Image File"
        OpenFileDialog1.DefaultExt = "rtf"
        OpenFileDialog1.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.ShowDialog()

        If OpenFileDialog1.FileName = "" Then Exit Sub

        Try
            Dim strImagePath As String = OpenFileDialog1.FileName
            Dim img As Image
            img = Image.FromFile(strImagePath)
            Clipboard.SetDataObject(img)
            Dim df As DataFormats.Format
            df = DataFormats.GetFormat(DataFormats.Bitmap)
            If Me.rtbOrgChart.CanPaste(df) Then
                Me.rtbOrgChart.Paste(df)
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to insert image format selected.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
#End Region

#Region "Toolbar Methods"
    Private Sub tbrSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrSave.Click
        Try
            SaveToolStripMenuItem_Click(Me, e)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrOpen.Click
        Try
            OpenToolStripMenuItem_Click(Me, e)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrNew.Click
        Try
            NewToolStripMenuItem_Click(Me, e)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrBold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrBold.Click
        Try
            tbrItalic.Checked = False
            tbrUnderline.Checked = False

            If tbrBold.Checked = False Then
                BoldToolStripMenuItem.Checked = False
            Else
                BoldToolStripMenuItem.Checked = True
            End If

            BoldToolStripMenuItem_Click(Me, e)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrItalic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrItalic.Click
        Try
            tbrBold.Checked = False
            tbrUnderline.Checked = False

            If tbrItalic.Checked = False Then
                ItalicToolStripMenuItem.Checked = False
            Else
                ItalicToolStripMenuItem.Checked = True
            End If

            ItalicToolStripMenuItem_Click(Me, e)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrUnderline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrUnderline.Click
        Try
            tbrBold.Checked = False
            tbrItalic.Checked = False

            If tbrUnderline.Checked = False Then
                UnderlineToolStripMenuItem.Checked = False
            Else
                UnderlineToolStripMenuItem.Checked = True
            End If

            UnderlineToolStripMenuItem_Click(Me, e)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrFont.Click
        Try
            SelectFontToolStripMenuItem_Click(Me, e)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrLeft.Click
        Try
            tbrCenter.Checked = False
            tbrRight.Checked = False
            CenterToolStripMenuItem.Checked = False
            RightToolStripMenuItem.Checked = False

            If tbrLeft.Checked = False Then
                LeftToolStripMenuItem.Checked = False
            Else
                LeftToolStripMenuItem.Checked = True
            End If

            rtbOrgChart.SelectionAlignment = HorizontalAlignment.Left
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrCenter.Click
        Try
            tbrLeft.Checked = False
            tbrRight.Checked = False
            LeftToolStripMenuItem.Checked = False
            RightToolStripMenuItem.Checked = False

            If tbrCenter.Checked = False Then
                CenterToolStripMenuItem.Checked = False
            Else
                CenterToolStripMenuItem.Checked = True
            End If

            rtbOrgChart.SelectionAlignment = HorizontalAlignment.Center
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrRight.Click
        Try
            tbrLeft.Checked = False
            tbrCenter.Checked = False
            LeftToolStripMenuItem.Checked = False
            CenterToolStripMenuItem.Checked = False

            If tbrRight.Checked = False Then
                RightToolStripMenuItem.Checked = False
            Else
                RightToolStripMenuItem.Checked = True
            End If

            rtbOrgChart.SelectionAlignment = HorizontalAlignment.Right
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tbrFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrFind.Click
        Try
            Dim f As New frmFind()
            f.Show()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

#Region "Printing"
    Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        Try
            ' Adapted from Microsoft's example for extended richtextbox control
            '
            checkPrint = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            ' Adapted from Microsoft's example for extended richtextbox control
            '
            ' Print the content of the RichTextBox. Store the last character printed.
            checkPrint = rtbOrgChart.Print(checkPrint, rtbOrgChart.TextLength, e)

            ' Look for more pages
            If checkPrint < rtbOrgChart.TextLength Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

#End Region







    Private Sub btnGenerateOrgChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateOrgChart.Click
        Try
            Dim Postion As Int16 = 0
            'Dim myFont As New Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point)
            'Dim myFont2 As New Font("Arial", 13, FontStyle.Bold, GraphicsUnit.Point)
            'Dim myFont3 As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
            'Dim myFont4 As New Font("Arial", 8.5, FontStyle.Underline, GraphicsUnit.Point)
            'Dim myFont5 As New Font("Arial", 8.5, FontStyle.Bold, GraphicsUnit.Point)
            'Dim myFont6 As New Font("Arial", 8.5, FontStyle.Regular, GraphicsUnit.Point)

            rtbOrgChart.Text = "AIR PROTECTION BRANCH"
            'rtbOrgChart.Select(Postion, 21)
            rtbOrgChart.Select(0, 21)
            rtbOrgChart.SelectionFont = New Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point)
            'rtbOrgChart.Select(Postion, 21)
            rtbOrgChart.Select(0, 21)
            rtbOrgChart.SelectionAlignment = HorizontalAlignment.Center
            Postion = rtbOrgChart.Text.Length

            rtbOrgChart.Text = rtbOrgChart.Text & vbCrLf & "Heather Abrams, Branch Chief"
            rtbOrgChart.Select(23, 28)
            rtbOrgChart.SelectionFont = New Font("Arial", 13, FontStyle.Bold, GraphicsUnit.Point)
            rtbOrgChart.Select(23, 28)
            rtbOrgChart.SelectionAlignment = HorizontalAlignment.Center
            Postion = rtbOrgChart.Text.Length

            rtbOrgChart.Text = rtbOrgChart.Text & vbCrLf & "Lou Ann Holder, Administrative Operations Manager"
            rtbOrgChart.Select(51, 49)
            rtbOrgChart.SelectionFont = New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
            rtbOrgChart.Select(51, 49)
            rtbOrgChart.SelectionAlignment = HorizontalAlignment.Center
            Postion = rtbOrgChart.Text.Length

         




        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rtbOrgChart_KeyDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtbOrgChart.KeyDown
        pnl1.Text = e.KeyData.ToString
    End Sub


End Class