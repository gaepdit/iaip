Namespace Apb.Ismp

    Public Class ReferenceNumber
        Implements IEquatable(Of ReferenceNumber)

#Region " Constructor "

        Private _referenceNumber As String

        Public Sub New(ByVal input As String)
            ' Parse and save or throw exception
            If IsValidReferenceNumberFormat(input) Then
                _referenceNumber = input
            Else
                _referenceNumber = Nothing
                Throw New InvalidReferenceNumberException(String.Format("{0} is not a valid ISMP Reference Number.", input))
            End If
        End Sub

#End Region

#Region " Operators "

        Public Shared Narrowing Operator CType(ByVal referenceNumber As String) As ReferenceNumber
            Return New ReferenceNumber(referenceNumber)
        End Operator

#End Region

#Region " IEquatable Interface implementation "

        Public Overloads Function Equals(ByVal other As ReferenceNumber) As Boolean _
        Implements IEquatable(Of ReferenceNumber).Equals
            If other Is Nothing Then Return False
            Return Me.ToString.Equals(other.ToString)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If TypeOf obj Is ReferenceNumber Then Return Equals(DirectCast(obj, ReferenceNumber))
            Return False
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Me.ToString.GetHashCode()
        End Function

#End Region

#Region " Validate Reference Number "

        ''' <summary>
        ''' Determines whether a string is in the format of a valid Reference Number.
        ''' </summary>
        ''' <param name="referenceNumber">The string to test</param>
        ''' <returns>True if referenceNumber is valid; otherwise, False.</returns>
        ''' <remarks>Valid Reference numbers are in the form </remarks>
        <DebuggerStepThrough()> _
        Public Shared Function IsValidReferenceNumberFormat(ByVal referenceNumber As String) As Boolean
            ' Temporary test until I can figure out Reference Number formatting
            If referenceNumber Is Nothing _
                OrElse String.IsNullOrWhiteSpace(referenceNumber) _
                OrElse Not Integer.TryParse(referenceNumber, Nothing) _
                Then
                Return False
            Else
                Return True
            End If


            ' Valid Reference numbers are in one of two forms:
            ' * yyyyxxxxx -- where yyyy is a year greater than or equal to 2004, 
            '                and xxxxx is a five-digit sequence number
            ' * ???
            'Dim rgx As New System.Text.RegularExpressions.Regex("")
            'Return rgx.IsMatch(referenceNumber)
        End Function

#End Region

    End Class

#Region " Invalid Reference number exception "

    <Serializable>
    Public Class InvalidReferenceNumberException
        Inherits Exception

        Private Const invalidReferenceNumberMessage As String = "The Reference Number is not valid."

        Public Sub New()
            MyBase.New(invalidReferenceNumberMessage)
        End Sub

        Public Sub New(ByVal auxMessage As String)
            MyBase.New(String.Format("{0} - {1}", invalidReferenceNumberMessage, auxMessage))
        End Sub

        Public Sub New(ByVal auxMessage As String, ByVal inner As Exception)
            MyBase.New(String.Format("{0} - {1}", invalidReferenceNumberMessage, auxMessage), inner)
        End Sub
    End Class

#End Region

End Namespace