Imports System
Imports System.IO
Imports System.Xml
'Imports System.Text
'Imports System.Security.Cryptography
Imports System.Data
Imports Oracle.DataAccess.Client
Imports System.Drawing
'Imports System.Net.Mail

'Imports System.Security
Imports System.Drawing.Drawing2D
Imports System.Text.RegularExpressions

Module MRFunctions
    'Public mstAttachFile As String
    'Public Apath As String = System.Environment.CurrentDirectory

    Sub DisplayReport(ByVal crReport As Object, ByVal TabText As String)
        Try
            crReport.DisplayGroupTree = True
            crReport.DisplayToolbar = True
            crReport.showrefreshbutton = False
            crReport.visible = True
            crReport.DisplayGroupTree = True

            Dim I As Integer
            Do While I < crReport.Controls.Count
                If TypeOf (crReport.Controls(I)) Is CrystalDecisions.Windows.Forms.PageView Then
                    Dim J As Integer
                    Do While J < crReport.Controls(I).Controls.Count
                        If CType(crReport.Controls(I).Controls(J), System.Windows.Forms.TabControl).TabPages.Count > 0 Then
                            'Change the tab text..
                            CType(crReport.Controls(I).Controls(J), System.Windows.Forms.TabControl).TabPages.Item(0).Text = TabText
                            Exit Do
                        End If
                    Loop
                    Exit Do
                Else
                    crReport.Controls(I).Visible = False
                End If
            Loop
        Catch ex As Exception
            ErrorReport(ex, "MRFunctions.DisplayReport")
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Public Class ProgressStatus
        Inherits StatusBarPanel
        Private pb As PictureBox = New PictureBox
        Dim t As Timer = New Timer

        Dim mysb As StatusBar
        Sub New(ByVal sb As StatusBar)
            Try
                mysb = sb
                pb.Hide()

                'add control
                sb.Controls.Add(pb)
                t.Interval = 40 'you can speed it up, if you like!
                t.Enabled = True

                'add handlers
                AddHandler sb.DrawItem, AddressOf Reposition
                AddHandler pb.Paint, AddressOf pb_Paint
                AddHandler t.Tick, AddressOf t_Tick

                'now animate a init
                progress = -1
            Catch ex As Exception
                ErrorReport(ex, "MRFunctions.New(ByVal sb as StatusBar)")
            Finally
                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            End Try
        End Sub
        Public Sub Refresh()
            Me.pb.Refresh()
        End Sub
        Private Sub t_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
            pb.Refresh()
        End Sub

        Dim mybackcolor As Color = Color.Black
        Private Sub Reposition(ByVal sender As Object, ByVal sbdevent As System.Windows.Forms.StatusBarDrawItemEventArgs)
            Try
                pb.Location = New Point(sbdevent.Bounds.X, sbdevent.Bounds.Y)
                pb.Size = New Size(sbdevent.Bounds.Width, sbdevent.Bounds.Height)
                pb.Show()
                mybackcolor = sbdevent.BackColor
            Catch ex As Exception
                ErrorReport(ex, "MRFunctions.Reposition")
            Finally
                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            End Try
        End Sub

        Private Sub pb_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
            drawit(e.Graphics, mybackcolor, New RectangleF(0, 0, pb.Width, pb.Height))
        End Sub

        Dim intprogress As Single
        Public Property progress() As Single
            Get
                Return intprogress
            End Get
            Set(ByVal Value As Single)
                intprogress = Value
                pb.Refresh()
            End Set
        End Property
        Public Property Animspeed() As Integer
            Get
                Return t.Interval
            End Get
            Set(ByVal Value As Integer)
                t.Interval = Value
            End Set
        End Property

        Private Sub drawit(ByVal g As Graphics, ByVal backcolor As Color, ByVal bounds As RectangleF)
            Static tint As Integer
            Static tz As Integer

            Try
                Select Case progress
                    Case -1 'timer
                        t.Enabled = True
                        If tz = 0 Then
                            tint += 5
                        Else
                            tint -= 5
                        End If
                        If tint > 80 Then tz = 1
                        If tint < 20 Then tz = 0
                        Dim gb As LinearGradientBrush = New LinearGradientBrush(bounds, Color.Blue, Color.White, 0, False)
                        Dim cb As ColorBlend = New ColorBlend(2)

                        Dim p(2) As Single
                        Dim c(2) As Color

                        c(0) = backcolor 'sbdevent.BackColor
                        p(0) = 0

                        c(1) = Color.DarkBlue
                        p(1) = Math.Abs(tint) / 100

                        c(2) = backcolor 'sbdevent.BackColor
                        p(2) = 1

                        cb.Colors = c
                        cb.Positions = p
                        gb.InterpolationColors = cb
                        g.FillRectangle(gb, bounds)  'sbdevent.Bounds)

                    Case 0 'empty
                        t.Enabled = False
                        Dim sgb As SolidBrush = New SolidBrush(backcolor) 'sbdevent.BackColor)
                        g.FillRectangle(sgb, bounds) ' sbdevent.Bounds)

                    Case Else 'ok!
                        t.Enabled = False
                        Dim gb As LinearGradientBrush = New LinearGradientBrush(bounds, Color.Blue, Color.White, 0, False)
                        Dim cb As ColorBlend = New ColorBlend(3)

                        Dim p(3) As Single
                        Dim c(3) As Color

                        c(0) = Color.DarkBlue 'sbdevent.BackColor
                        p(0) = 0

                        c(1) = Color.DarkBlue
                        p(1) = Math.Abs(progress) / 100
                        c(2) = backcolor ' sbdevent.BackColor
                        p(2) = Math.Abs(progress + 10) / 100


                        c(3) = backcolor 'sbdevent.BackColor
                        p(3) = 1

                        cb.Colors = c
                        cb.Positions = p
                        gb.InterpolationColors = cb
                        g.FillRectangle(gb, bounds)  ' sbdevent.Bounds)
                End Select

            Catch ex As Exception
                ErrorReport(ex, "MRFunctions.drawit")
            Finally
                If Conn.State = ConnectionState.Open Then
                    'conn.close()
                End If
            End Try
        End Sub



    End Class

#Region "Obsolete code removed"

    'Public Class RandomPassword

    '    ' Define default min and max password lengths.
    '    Private Shared DEFAULT_MIN_PASSWORD_LENGTH As Integer = 6
    '    Private Shared DEFAULT_MAX_PASSWORD_LENGTH As Integer = 8

    '    ' Define supported password characters divided into groups.
    '    ' You can add (or remove) characters to (from) these groups.
    '    Private Shared PASSWORD_CHARS_LCASE As String = "abcdefgjkmnpqrstwxyz"
    '    Private Shared PASSWORD_CHARS_UCASE As String = "ABCDEFGHJKMNPQRSTWXYZ"
    '    Private Shared PASSWORD_CHARS_NUMERIC As String = "23456789"
    '    Private Shared PASSWORD_CHARS_SPECIAL As String = "*"

    '    ' <summary>
    '    ' Generates a random password.
    '    ' </summary>
    '    ' <returns>
    '    ' Randomly generated password.
    '    ' </returns>
    '    ' <remarks>
    '    ' The length of the generated password will be determined at
    '    ' random. It will be no shorter than the minimum default and
    '    ' no longer than maximum default.
    '    ' </remarks>
    '    Public Shared Function Generate() As String
    '        Try
    '            Generate = Generate(DEFAULT_MIN_PASSWORD_LENGTH, _
    '                                         DEFAULT_MAX_PASSWORD_LENGTH)
    '        Catch ex As Exception
    '            ErrorReport(ex, "MRFunctions.Generate(No varables)")
    '        Finally
    '            If conn.State = ConnectionState.Open Then
    '                'conn.close()
    '            End If
    '        End Try
    '    End Function

    '    ' <summary>
    '    ' Generates a random password of the exact length.
    '    ' </summary>
    '    ' <param name="length">
    '    ' Exact password length.
    '    ' </param>
    '    ' <returns>
    '    ' Randomly generated password.
    '    ' </returns>
    '    Public Shared Function Generate(ByVal length As Integer) As String
    '        Try
    '            Generate = Generate(length, length)
    '        Catch ex As Exception
    '            ErrorReport(ex, "MRFunctions.Generate(Length Varable)")
    '        Finally
    '            If conn.State = ConnectionState.Open Then
    '                'conn.close()
    '            End If
    '        End Try
    '    End Function

    '    ' <summary>
    '    ' Generates a random password.
    '    ' </summary>
    '    ' <param name="minLength">
    '    ' Minimum password length.
    '    ' </param>
    '    ' <param name="maxLength">
    '    ' Maximum password length.
    '    ' </param>
    '    ' <returns>
    '    ' Randomly generated password.
    '    ' </returns>
    '    ' <remarks>
    '    ' The length of the generated password will be determined at
    '    ' random and it will fall with the range determined by the
    '    ' function parameters.
    '    ' </remarks>
    '    Public Shared Function Generate(ByVal minLength As Integer, _
    '    ByVal maxLength As Integer) _
    '        As String

    '        Try
    '            ' Make sure that input parameters are valid.
    '            If (minLength <= 0 Or maxLength <= 0 Or minLength > maxLength) Then
    '                Generate = Nothing
    '            End If

    '            ' Create a local array containing supported password characters
    '            ' grouped by types. You can remove character groups from this
    '            ' array, but doing so will weaken the password strength.
    '            Dim charGroups As Char()() = New Char()() _
    '            { _
    '                PASSWORD_CHARS_LCASE.ToCharArray(), _
    '                PASSWORD_CHARS_UCASE.ToCharArray(), _
    '                PASSWORD_CHARS_NUMERIC.ToCharArray(), _
    '                PASSWORD_CHARS_SPECIAL.ToCharArray() _
    '            }

    '            ' Use this array to track the number of unused characters in each
    '            ' character group.
    '            Dim charsLeftInGroup As Integer() = New Integer(charGroups.Length - 1) {}

    '            ' Initially, all characters in each group are not used.
    '            Dim I As Integer
    '            For I = 0 To charsLeftInGroup.Length - 1
    '                charsLeftInGroup(I) = charGroups(I).Length
    '            Next

    '            ' Use this array to track (iterate through) unused character groups.
    '            Dim leftGroupsOrder As Integer() = New Integer(charGroups.Length - 1) {}

    '            ' Initially, all character groups are not used.
    '            For I = 0 To leftGroupsOrder.Length - 1
    '                leftGroupsOrder(I) = I
    '            Next

    '            ' Because we cannot use the default randomizer, which is based on the
    '            ' current time (it will produce the same "random" number within a
    '            ' second), we will use a random number generator to seed the
    '            ' randomizer.

    '            ' Use a 4-byte array to fill it with random bytes and convert it then
    '            ' to an integer value.
    '            Dim randomBytes As Byte() = New Byte(3) {}

    '            ' Generate 4 random bytes.
    '            Dim rng As RNGCryptoServiceProvider = New RNGCryptoServiceProvider

    '            rng.GetBytes(randomBytes)

    '            ' Convert 4 bytes into a 32-bit integer value.
    '            Dim seed As Integer = ((randomBytes(0) And &H7F) << 24 Or _
    '                                    randomBytes(1) << 16 Or _
    '                                    randomBytes(2) << 8 Or _
    '                                    randomBytes(3))

    '            ' Now, this is real randomization.
    '            Dim random As Random = New Random(seed)

    '            ' This array will hold password characters.
    '            Dim password As Char() = Nothing

    '            ' Allocate appropriate memory for the password.
    '            If (minLength < maxLength) Then
    '                password = New Char(random.Next(minLength - 1, maxLength)) {}
    '            Else
    '                password = New Char(minLength - 1) {}
    '            End If

    '            ' Index of the next character to be added to password.
    '            Dim nextCharIdx As Integer

    '            ' Index of the next character group to be processed.
    '            Dim nextGroupIdx As Integer

    '            ' Index which will be used to track not processed character groups.
    '            Dim nextLeftGroupsOrderIdx As Integer

    '            ' Index of the last non-processed character in a group.
    '            Dim lastCharIdx As Integer

    '            ' Index of the last non-processed group.
    '            Dim lastLeftGroupsOrderIdx As Integer = leftGroupsOrder.Length - 1

    '            ' Generate password characters one at a time.
    '            For I = 0 To password.Length - 1

    '                ' If only one character group remained unprocessed, process it;
    '                ' otherwise, pick a random character group from the unprocessed
    '                ' group list. To allow a special character to appear in the
    '                ' first position, increment the second parameter of the Next
    '                ' function call by one, i.e. lastLeftGroupsOrderIdx + 1.
    '                If (lastLeftGroupsOrderIdx = 0) Then
    '                    nextLeftGroupsOrderIdx = 0
    '                Else
    '                    nextLeftGroupsOrderIdx = random.Next(0, lastLeftGroupsOrderIdx)
    '                End If

    '                ' Get the actual index of the character group, from which we will
    '                ' pick the next character.
    '                nextGroupIdx = leftGroupsOrder(nextLeftGroupsOrderIdx)

    '                ' Get the index of the last unprocessed characters in this group.
    '                lastCharIdx = charsLeftInGroup(nextGroupIdx) - 1

    '                ' If only one unprocessed character is left, pick it; otherwise,
    '                ' get a random character from the unused character list.
    '                If (lastCharIdx = 0) Then
    '                    nextCharIdx = 0
    '                Else
    '                    nextCharIdx = random.Next(0, lastCharIdx + 1)
    '                End If

    '                ' Add this character to the password.
    '                password(I) = charGroups(nextGroupIdx)(nextCharIdx)

    '                ' If we processed the last character in this group, start over.
    '                If (lastCharIdx = 0) Then
    '                    charsLeftInGroup(nextGroupIdx) = _
    '                                    charGroups(nextGroupIdx).Length
    '                    ' There are more unprocessed characters left.
    '                Else
    '                    ' Swap processed character with the last unprocessed character
    '                    ' so that we don't pick it until we process all characters in
    '                    ' this group.
    '                    If (lastCharIdx <> nextCharIdx) Then
    '                        Dim temp As Char = charGroups(nextGroupIdx)(lastCharIdx)
    '                        charGroups(nextGroupIdx)(lastCharIdx) = _
    '                                    charGroups(nextGroupIdx)(nextCharIdx)
    '                        charGroups(nextGroupIdx)(nextCharIdx) = temp
    '                    End If

    '                    ' Decrement the number of unprocessed characters in
    '                    ' this group.
    '                    charsLeftInGroup(nextGroupIdx) = _
    '                               charsLeftInGroup(nextGroupIdx) - 1
    '                End If

    '                ' If we processed the last group, start all over.
    '                If (lastLeftGroupsOrderIdx = 0) Then
    '                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1
    '                    ' There are more unprocessed groups left.
    '                Else
    '                    ' Swap processed group with the last unprocessed group
    '                    ' so that we don't pick it until we process all groups.
    '                    If (lastLeftGroupsOrderIdx <> nextLeftGroupsOrderIdx) Then
    '                        Dim temp As Integer = _
    '                                    leftGroupsOrder(lastLeftGroupsOrderIdx)
    '                        leftGroupsOrder(lastLeftGroupsOrderIdx) = _
    '                                    leftGroupsOrder(nextLeftGroupsOrderIdx)
    '                        leftGroupsOrder(nextLeftGroupsOrderIdx) = temp
    '                    End If

    '                    ' Decrement the number of unprocessed groups.
    '                    lastLeftGroupsOrderIdx = lastLeftGroupsOrderIdx - 1
    '                End If
    '            Next

    '            ' Convert password characters into a string and return the result.
    '            Generate = New String(password)

    '        Catch ex As Exception
    '            ErrorReport(ex, "MRFunctions.Generate(Min/Max Varable")
    '        Finally
    '            If conn.State = ConnectionState.Open Then
    '                'conn.close()
    '            End If
    '        End Try
    '    End Function

    'End Class

    'Sub SendMail(ByVal myEmailTo As String, ByVal mySubject As String, ByVal myMessage As String)
    '    Dim objMessages As Object
    '    Dim objMessage As Object
    '    Dim objMailBox As Object
    '    Dim objRecipients As Object
    '    Dim objRecipient As Object
    '    Dim objMessageSent As Object

    '    Try
    '        objMailBox = objAccount.MailBox
    '        objMessages = objMailBox.Messages
    '        objMessage = objMessages.Add("GW.MESSAGE.MAIL", "Draft")
    '        objRecipients = objMessage.Recipients

    '        objRecipient = objRecipients.Add(myEmailTo)
    '        objMessage.Subject = mySubject
    '        objMessage.BodyText = myMessage

    '        objMessageSent = objMessage.Send

    '        MessageBox.Show("The new Password has been sent Successfully..." & vbCrLf & vbCrLf & _
    '                         "To : " & myEmailTo & vbCrLf & vbCrLf & _
    '                         "Subject : " & mySubject & _
    '                         "", "Message Sent Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Catch ex As Exception
    '        ErrorReport(ex, "MRFunctions.SendMail")
    '    Finally
    '        If Conn.State = ConnectionState.Open Then
    '            'conn.close()
    '        End If
    '        Beep()   ' Beep after error processing.
    '    End Try
    'End Sub

    'Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
    '    Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
    '    Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)

    '    If emailAddressMatch.Success Then
    '        EmailAddressCheck = True
    '    Else
    '        EmailAddressCheck = False
    '    End If
    'End Function

#End Region

End Module
