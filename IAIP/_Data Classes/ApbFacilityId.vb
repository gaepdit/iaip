Namespace Apb

    Public Structure ApbFacilityId

#Region " Constructor "

        Private _value As String

        Public Sub New(ByVal input As String)
            ' Parse and save or throw exception
            If ValidAirsNumber(input) Then
                _value = GetNormalizedAirsNumber(input)
            Else
                _value = Nothing
                Throw New InvalidAirsNumberException(String.Format("The argument {0} is not a valid AIRS number.", input))
            End If
        End Sub

#End Region

#Region " Properties "

        Public Overrides Function ToString() As String
            Return _value
        End Function

        Public ReadOnly Property ShortString() As String
            Get
                Return _value
            End Get
        End Property

        Public ReadOnly Property FormattedString() As String
            Get
                Return Mid(_value, 1, 3) & "-" & Mid(_value, 4, 5)
            End Get
        End Property

        Public ReadOnly Property DbFormattedString() As String
            Get
                Return "0413" & Me.ShortString
            End Get
        End Property

#End Region

#Region " Operators "

        Public Shared Narrowing Operator CType(ByVal airsNumber As String) As ApbFacilityId
            Return New ApbFacilityId(airsNumber)
        End Operator

        'Public Shared Widening Operator CType(ByVal airsNumber As ApbFacilityId) As String
        '    Return airsNumber.ToString
        'End Operator

#End Region

#Region " Public Shared Functions "

        ''' <summary>
        ''' Determines whether a string is in the format of a valid AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The string to test</param>
        ''' <returns>True if airsNumber is valid; otherwise, false.</returns>
        ''' <remarks>Valid AIRS numbers are in the form 000-00000 or 04-13-000-0000 (with or without the dashes)</remarks>
        <DebuggerStepThrough()> _
        Public Shared Function ValidAirsNumber(ByVal airsNumber As String) As Boolean
            If airsNumber Is Nothing Then Return False
            ' Valid AIRS numbers are in the form 000-00000 or 04-13-000-0000
            ' (with or without the dashes)
            If airsNumber Is Nothing Then Return False
            Dim rgx As New System.Text.RegularExpressions.Regex("^(04-?13-?)?\d{3}-?\d{5}$")
            Return rgx.IsMatch(airsNumber)
        End Function

#End Region

#Region " Private Functions "

        ''' <summary>
        ''' Converts a string representation of an AIRS number to the "00000000" form. If 'expand' is True, then 
        ''' the AIRS number is expanded to the "041300000000" form.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to convert.</param>
        ''' <returns>A string representation of an AIRS number in the "00000000" or "041300000000" form.</returns>
        <DebuggerStepThrough()> _
        Private Function GetNormalizedAirsNumber(ByVal airsNumber As String) As String
            ' Converts a string representation of an AIRS number to the "00000000" form 
            ' (eight numerals, no dashes).
            '
            ' If 'expand' is True, then the AIRS number is expanded to the "041300000000"
            ' form (12 numerals, no dashes, beginning with "0413").

            ' Remove spaces, dashes, and leading '0413'
            airsNumber = airsNumber.Replace("-", "").Replace(" ", "")
            If airsNumber.Length = 12 Then airsNumber = airsNumber.Remove(0, 4)
            
            Return airsNumber
        End Function

#End Region

    End Structure

    Public Class InvalidAirsNumberException
        Inherits Exception

        Private Const invalidAirsNumberMessage As String = "The AIRS number is not valid."

        Public Sub New()
            MyBase.New(invalidAirsNumberMessage)
        End Sub

        Public Sub New(ByVal auxMessage As String)
            MyBase.New(String.Format("{0} - {1}", _
                invalidAirsNumberMessage, auxMessage))
        End Sub

        Public Sub New(ByVal auxMessage As String, ByVal inner As Exception)
            MyBase.New(String.Format("{0} - {1}", _
                invalidAirsNumberMessage, auxMessage), inner)
        End Sub

    End Class

End Namespace