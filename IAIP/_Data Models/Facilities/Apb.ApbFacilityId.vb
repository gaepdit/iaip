Namespace Apb

    Public Class ApbFacilityId
        Implements IEquatable(Of ApbFacilityId)

#Region " Constructor "

        Private _value As String

        Public Sub New(ByVal input As String)
            ' Parse and save or throw exception
            If IsValidAirsNumberFormat(input) Then
                _value = GetNormalizedAirsNumber(input)
            Else
                _value = Nothing
                Throw New InvalidAirsNumberException(String.Format("{0} is not a valid AIRS number.", input))
            End If
        End Sub

#End Region

#Region " Properties "

        ''' <summary>
        ''' Displays APB facility ID as an eight-character string in the form "00000000"
        ''' </summary>
        ''' <remarks>Equivalent to ShortString property</remarks>
        Public Overrides Function ToString() As String
            Return _value
        End Function

        ''' <summary>
        ''' Displays APB facility ID as an eight-character string in the form "00000000"
        ''' </summary>
        ''' <remarks>Equivalent to ToString method</remarks>
        Public ReadOnly Property ShortString() As String
            Get
                Return _value
            End Get
        End Property

        ''' <summary>
        ''' Displays APB facility ID as a nine-character string in the form "000-00000"
        ''' </summary>
        Public ReadOnly Property FormattedString() As String
            Get
                Return Mid(_value, 1, 3) & "-" & Mid(_value, 4, 5)
            End Get
        End Property

        ''' <summary>
        ''' Displays APB facility ID as a 12-character string in the form "041300000000"
        ''' </summary>
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

#Region " IEquatable Interface implementation "

        Public Overloads Function Equals(ByVal other As ApbFacilityId) As Boolean _
        Implements IEquatable(Of ApbFacilityId).Equals
            If other Is Nothing Then Return False
            Return Me.ToString.Equals(other.ToString)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If TypeOf obj Is ApbFacilityId Then Return Equals(DirectCast(obj, ApbFacilityId))
            Return False
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Me.ToString.GetHashCode()
        End Function

#End Region

#Region " Validate/normalize AIRS number "

        ''' <summary>
        ''' Determines whether a string is in the format of a valid AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The string to test</param>
        ''' <returns>True if airsNumber is valid; otherwise, False.</returns>
        ''' <remarks>Valid AIRS numbers are in the form 000-00000 or 04-13-000-0000 (with or without the dashes)</remarks>
        <DebuggerStepThrough()> _
        Public Shared Function IsValidAirsNumberFormat(ByVal airsNumber As String) As Boolean
            If airsNumber Is Nothing Then Return False
            Return System.Text.RegularExpressions.Regex.IsMatch(airsNumber, AirsNumberPattern)
        End Function

        ''' <summary>
        ''' Converts a string representation of an AIRS number to the "00000000" form.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to convert.</param>
        ''' <returns>A string representation of an AIRS number in the form "00000000".</returns>
        <DebuggerStepThrough()> _
        Private Function GetNormalizedAirsNumber(ByVal airsNumber As String) As String
            ' Converts a string representation of an AIRS number to the "00000000" form 
            ' (eight numerals, no dashes).
            '
            ' Remove spaces, dashes, and leading '0413'
            airsNumber = airsNumber.Replace("-", "").Replace(" ", "")
            If airsNumber.Length = 12 Then airsNumber = airsNumber.Remove(0, 4)

            Return airsNumber
        End Function

#End Region

    End Class

#Region " Invalid AIRS number exception "

    <Serializable>
    Public Class InvalidAirsNumberException
        Inherits Exception

        Private Const invalidAirsNumberMessage As String = "The AIRS number is not valid."

        Public Sub New()
            MyBase.New(invalidAirsNumberMessage)
        End Sub

        Public Sub New(ByVal auxMessage As String)
            MyBase.New(String.Format("{0} - {1}", invalidAirsNumberMessage, auxMessage))
        End Sub

        Public Sub New(ByVal auxMessage As String, ByVal inner As Exception)
            MyBase.New(String.Format("{0} - {1}", invalidAirsNumberMessage, auxMessage), inner)
        End Sub
    End Class

#End Region

End Namespace