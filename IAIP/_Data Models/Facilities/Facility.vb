Namespace Apb.Facilities

    ''' <summary>
    ''' Basic information about a stationary source.
    ''' </summary>
    Public Class Facility

#Region " Constructors "
        Public Sub New()
        End Sub

        Public Sub New(airsNumber As ApbFacilityId)
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
        Public Property FacilityLocation As Location
        Public Property HeaderData As FacilityHeaderData
        Public Property Comment As String
        Public Property ApprovedByApb As Boolean
        Public Property DistrictOfficeLocation As String
        Public Property DistrictResponsible As Boolean?

#End Region

#Region " Methods "

        Public Function RetrieveHeaderData() As Facility
            If Me IsNot Nothing Then
                HeaderData = DAL.GetFacilityHeaderData(AirsNumber)
            End If
            Return Me
        End Function

#End Region

#Region " Public shared functions "

        ''' <summary>
        ''' Sanitizes a string for use as a facility name by replacing brackets with parentheses
        ''' </summary>
        ''' <param name="name">The string to sanitize.</param>
        ''' <returns>A sanitized string.</returns>
        ''' <remarks>This is required because of a quirk in the current database setup.</remarks>
        Public Shared Function SanitizeFacilityNameForDb(name As String) As String
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
                    Return HeaderData.AirPrograms.HasFlag(AirPrograms.NSPS)
                End If
            End Get
        End Property

        Public ReadOnly Property SubjectToPart70() As Boolean?
            Get
                If HeaderData Is Nothing Then
                    Return Nothing
                Else
                    Return HeaderData.AirPrograms.HasFlag(AirPrograms.TitleV)
                End If
            End Get
        End Property

        Public ReadOnly Property LongDisplay() As String
            Get
                Dim ld As String = AirsNumber.FormattedString &
                vbNewLine &
                FacilityName.ToString & vbNewLine

                If FacilityLocation IsNot Nothing Then
                    ld = ld & FacilityLocation.Address.ToString & vbNewLine &
                    FacilityLocation.County.ToString & " county"
                End If

                If HeaderData IsNot Nothing Then
                    ld = ld & vbNewLine & vbNewLine &
                    "Classification: " & HeaderData.Classification.GetDescription & vbNewLine &
                    "Status: " & HeaderData.OperationalStatus.GetDescription
                End If

                Return ld
            End Get
        End Property

        Public ReadOnly Property DisplayFacilityAddress As String
            Get
                Return FacilityLocation.Address.ToString
            End Get
        End Property
        Public ReadOnly Property DisplayCity As String
            Get
                Return FacilityLocation.Address.City
            End Get
        End Property
        Public ReadOnly Property DisplayCounty As String
            Get
                Return FacilityLocation.County
            End Get
        End Property
        Public ReadOnly Property DisplayClassification As String
            Get
                If HeaderData Is Nothing Then Return Nothing
                Return HeaderData.ClassificationDescription
            End Get
        End Property
        Public ReadOnly Property DisplayAirPrograms As String
            Get
                If HeaderData Is Nothing Then Return Nothing
                Return String.Join(", ", HeaderData.AirPrograms.GetUniqueFlagDescriptions)
            End Get
        End Property

#End Region

    End Class

End Namespace
