Imports System.Collections.Generic
Imports System.Linq

Namespace Apb.Facilities

    ''' <summary>
    ''' Basic information about a stationary source.
    ''' </summary>
    Public Class Facility

#Region " Constructors "
        Public Sub New()
        End Sub

        Public Sub New(ByVal airsNumber As ApbFacilityId)
            Me.AirsNumber = airsNumber
        End Sub
#End Region

#Region " Properties "

        Public Property AirsNumber() As ApbFacilityId
        Private _FacilityName As String
        Public Property FacilityName() As String
            Get
                Return _FacilityName
            End Get
            Set(value As String)
                _FacilityName = SanitizeFacilityNameForDb(value)
            End Set
        End Property

        ''' <summary>
        ''' Facility Location is where the facility is actually located, distinct 
        ''' from a mailing address. Facility Location may not have a real 
        ''' postal address, but will have some of the elements of a postal address
        ''' </summary>
        Public Property FacilityLocation() As Location
        Public Property HeaderData() As FacilityHeaderData
        Public Property Comment() As String
        Public Property ApprovedByApb() As Boolean?
        Public Property DistrictOfficeLocation() As String
        Public Property DistrictResponsible() As Boolean?
        Public Property ComplianceStatusList() As List(Of PollutantComplianceStatus)

#End Region

#Region " Methods "

        Public Sub RetrieveHeaderData()
            If Me IsNot Nothing Then
                HeaderData = DAL.GetFacilityHeaderData(Me.AirsNumber)
            End If
        End Sub

        Public Sub RetrieveComplianceStatusList()
            If Me IsNot Nothing Then
                ComplianceStatusList = DAL.FacilityData.GetComplianceStatusList(Me.AirsNumber)
            End If
        End Sub

#End Region

#Region " Public shared functions "

        ''' <summary>
        ''' Sanitizes a string for use as a facility name by replacing brackets with parentheses
        ''' </summary>
        ''' <param name="name">The string to sanitize.</param>
        ''' <returns>A sanitized string.</returns>
        ''' <remarks>This is required because of a quirk in the current database setup.</remarks>
        Public Shared Function SanitizeFacilityNameForDb(ByVal name As String) As String
            If String.IsNullOrEmpty(name) Then
                Return Nothing
            End If

            Dim sanitizedName As New System.Text.StringBuilder(name)
            sanitizedName.Replace("[", "(").Replace("]", ")")

            Return sanitizedName.ToString
        End Function

#End Region

#Region " Read-only convenience properties"

        Public ReadOnly Property SubjectToNsps() As Boolean?
            Get
                If HeaderData Is Nothing Then
                    Return Nothing
                Else
                    Return HeaderData.AirPrograms.HasFlag(AirProgram.NSPS)
                End If
            End Get
        End Property

        Public ReadOnly Property SubjectToPart70() As Boolean?
            Get
                If HeaderData Is Nothing Then
                    Return Nothing
                Else
                    Return HeaderData.AirPrograms.HasFlag(AirProgram.TitleV)
                End If
            End Get
        End Property

        Public ReadOnly Property LongDisplay() As String
            Get
                Dim ld As String = Me.AirsNumber.FormattedString & _
                vbNewLine & _
                Me.FacilityName.ToString & vbNewLine

                If Me.FacilityLocation IsNot Nothing Then
                    ld = ld & Me.FacilityLocation.Address.ToString & vbNewLine & _
                    Me.FacilityLocation.County.ToString & " county"
                End If

                If Me.HeaderData IsNot Nothing Then
                    ld = ld & vbNewLine & vbNewLine & _
                    "Classification: " & Me.HeaderData.Classification.GetDescription & vbNewLine & _
                    "Status: " & Me.HeaderData.OperationalStatus.GetDescription
                End If

                Return ld
            End Get
        End Property

        Public ReadOnly Property ControllingComplianceStatus() As PollutantComplianceStatus
            Get
                If ComplianceStatusList Is Nothing OrElse ComplianceStatusList.Count = 0 Then
                    Return PollutantComplianceStatus.NoValue
                Else
                    Return ComplianceStatusList.Max
                End If
            End Get
        End Property

#End Region

    End Class

End Namespace
