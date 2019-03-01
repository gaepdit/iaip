﻿Namespace Apb

    Public Class ApbFacilityId
        Implements IEquatable(Of ApbFacilityId)

        ''' <summary>
        ''' APB facility ID as an eight-character string in the form "00000000"
        ''' </summary>
        Public ReadOnly Property ShortString() As String

        Public Sub New(airsNumber As String)
            If Not IsValidAirsNumberFormat(airsNumber) Then
                Throw New ArgumentException(String.Format("{0} is not a valid AIRS number.", airsNumber))
            End If

            ShortString = GetNormalizedAirsNumber(airsNumber)
        End Sub

        ''' <summary>
        ''' Returns APB facility ID as an eight-character string in the form "00000000"
        ''' </summary>
        Public Overrides Function ToString() As String
            Return ShortString
        End Function

        ''' <summary>
        ''' Returns APB facility ID as a nine-character string in the form "000-00000"
        ''' </summary>
        Public ReadOnly Property FormattedString() As String
            Get
                Return CountySubstring & "-" & Mid(ShortString, 4, 5)
            End Get
        End Property

        ''' <summary>
        ''' Returns APB facility ID as an eight-character string in the form "000-0000"
        ''' </summary>
        Public ReadOnly Property PermitFormattedString() As String
            Get
                Return CountySubstring & "-" & Mid(ShortString, 5, 4)
            End Get
        End Property

        ''' <summary>
        ''' Returns APB facility ID as an integer
        ''' </summary>
        Public Function ToInt() As Integer
            Return Convert.ToInt32(ShortString)
        End Function

        ''' <summary>
        ''' Returns APB facility ID as a 12-character string in the form "041300000000"
        ''' </summary>
        Public ReadOnly Property DbFormattedString() As String
            Get
                Return GA_EPA_REGION_CODE & GA_STATE_NUMERIC_CODE & ShortString
            End Get
        End Property

        ''' <summary>
        ''' Returns county substring as a 3-character string 
        ''' </summary>
        Public ReadOnly Property CountySubstring() As String
            Get
                Return Mid(ShortString, 1, 3)
            End Get
        End Property

        ''' <summary>
        ''' Displays the facility identifier used by EPA's ICIS-Air system
        ''' </summary>
        Public ReadOnly Property EpaFacilityIdentifier() As String
            Get
                Return GA_STATE_CODE & "000000" & GA_STATE_NUMERIC_CODE & ShortString
            End Get
        End Property

#Region " Operators "

        Public Shared Narrowing Operator CType(airsNumber As String) As ApbFacilityId
            Return New ApbFacilityId(airsNumber)
        End Operator

        Public Shared Widening Operator CType(airsNumber As ApbFacilityId) As String
            Return airsNumber.ToString
        End Operator

        Public Shared Operator =(airs1 As ApbFacilityId, airs2 As ApbFacilityId) As Boolean
            If airs1 Is Nothing AndAlso airs2 Is Nothing Then
                Return True
            End If

            If airs1 IsNot Nothing AndAlso airs2 IsNot Nothing Then
                Return airs1.ShortString.Equals(airs2.ShortString)
            End If

            Return False
        End Operator

        Public Shared Operator <>(airs1 As ApbFacilityId, airs2 As ApbFacilityId) As Boolean
            Return Not (airs1 = airs2)
        End Operator

#End Region

#Region " IEquatable Interface implementation "

        Public Overloads Function Equals(other As ApbFacilityId) As Boolean _
        Implements IEquatable(Of ApbFacilityId).Equals
            If other Is Nothing Then Return False
            Return ToString().Equals(other.ToString())
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If TypeOf obj Is ApbFacilityId Then Return Equals(DirectCast(obj, ApbFacilityId))
            Return False
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return ToString().GetHashCode()
        End Function

#End Region

#Region " Validate/normalize AIRS number "

        ''' <summary>
        ''' Determines whether a string is in the format of a valid AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The string to test</param>
        ''' <returns>True if airsNumber is valid; otherwise, False.</returns>
        ''' <remarks>Valid AIRS numbers are in the form 000-00000 or 04-13-000-0000 (with or without the dashes)</remarks>
        <DebuggerStepThrough()>
        Public Shared Function IsValidAirsNumberFormat(airsNumber As String) As Boolean
            If airsNumber Is Nothing Then Return False

            Return Text.RegularExpressions.Regex.IsMatch(airsNumber, AirsNumberPattern)
        End Function

        ''' <summary>
        ''' Converts a string representation of an AIRS number to the "00000000" form.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to convert.</param>
        ''' <returns>A string representation of an AIRS number in the form "00000000".</returns>
        <DebuggerStepThrough()>
        Private Shared Function GetNormalizedAirsNumber(airsNumber As String) As String
            ' Converts a string representation of an AIRS number to the "00000000" form 
            ' (eight numerals, no dashes).

            ' Remove spaces, dashes, and leading '0413'
            airsNumber = airsNumber.Replace("-", "").Replace(" ", "")

            If airsNumber.Length = 12 Then airsNumber = airsNumber.Remove(0, 4)

            Return airsNumber
        End Function

        Public Shared Function TryCastApbFacilityId(airsNumber As String) As ApbFacilityId
            If String.IsNullOrEmpty(airsNumber) OrElse Not IsValidAirsNumberFormat(airsNumber) Then
                Return Nothing
            End If

            Return New ApbFacilityId(airsNumber)
        End Function

        Public Shared Function TryCastApbFacilityId(airsNumber As Object) As ApbFacilityId
            If airsNumber Is Nothing OrElse IsDBNull(airsNumber) Then
                Return Nothing
            End If

            Return TryCastApbFacilityId(CStr(airsNumber))
        End Function

#End Region

    End Class

End Namespace