Imports System.Runtime.CompilerServices

Module SplitContainerExtensions

    ''' <summary>
    ''' Sets the SplitContainer.SplitterDistance property, while taking into account the SplitContainer's dimensions
    ''' </summary>
    ''' <param name="sc">The SplitContainer to modify</param>
    ''' <param name="dist">The desired SplitterDistance</param>
    ''' <remarks>If the desired SplitterDistance is incompatible with the dimension of the 
    ''' SplitContainer, nothing is changed and no error is returned</remarks>
    <Extension()>
    Public Sub SanelySetSplitterDistance(sc As SplitContainer, dist As Integer)
        Dim i As Integer = dist

        If sc.Orientation = Orientation.Horizontal Then

            ' It may not seem possible for the size of a SplitContainer to be smaller than
            ' the minimum sizes of its parts, but it can happen if the SplitContainer is
            ' docked in a Form that is itself resized until the SplitContainer is too small.
            ' In this situation, don't try to fix things, just bail.
            If (sc.Height < sc.Panel1MinSize + sc.Panel2MinSize) Then Return

            ' The order here shouldn't matter
            i = Math.Max(i, sc.Panel1MinSize)
            i = Math.Min(i, sc.Height - sc.Panel2MinSize)

        Else
            ' Same as above, except for vertical orientation
            If (sc.Width < sc.Panel1MinSize + sc.Panel2MinSize) Then Return
            i = Math.Max(i, sc.Panel1MinSize)
            i = Math.Min(i, sc.Width - sc.Panel2MinSize)
        End If

        sc.SplitterDistance = i
    End Sub

    ''' <summary>
    ''' Toggle SplitContainer.SplitterDistance between two given values
    ''' </summary>
    ''' <param name="sc">The SplitContainer to modify</param>
    ''' <param name="a">One of the values to toggle between</param>
    ''' <param name="b">One of the values to toggle between</param>
    ''' <remarks>The order of the parameters does not matter. If either parameter is incompatible with 
    ''' the dimension of the SplitContainer, nothing is changed and no error is returned.</remarks>
    <Extension()>
    Public Sub ToggleSplitterDistance(sc As SplitContainer, a As Integer, b As Integer)
        ' Bail if a or b are outside the allowable values for SplitterDistance
        If (a < sc.Panel1MinSize) OrElse (b < sc.Panel1MinSize) Then Return
        If (sc.Orientation = Orientation.Vertical) Then
            If (a > sc.Width - sc.Panel2MinSize) OrElse (b > sc.Width - sc.Panel2MinSize) Then Return
        Else
            If (a > sc.Height - sc.Panel2MinSize) OrElse (b > sc.Height - sc.Panel2MinSize) Then Return
        End If

        ' If current SplitterDistance is smaller than the average, set it to the larger value;
        ' otherwise, set it to the smaller value
        If (sc.SplitterDistance < (a + b) / 2) Then
            sc.SanelySetSplitterDistance(Math.Max(a, b))
        Else
            sc.SanelySetSplitterDistance(Math.Min(a, b))
        End If
    End Sub

End Module