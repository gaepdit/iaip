Imports System.Drawing.Drawing2D

Public Class ProgressStatus
    Private pb As PictureBox = New PictureBox
    Dim t As Timer = New Timer

    Sub New(ByVal sb As StatusBar)
        Try
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
        End Try
    End Sub

End Class