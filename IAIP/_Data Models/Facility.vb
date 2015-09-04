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
        Public Property FacilityName() As String

        ''' <summary>
        ''' Facility Location is where the facility is actually located, distinct 
        ''' from a mailing address. Facility Location may not have a real 
        ''' postal address, but will have some of the elements of a postal address
        ''' </summary>
        Public Property FacilityLocation() As Location

        Public Property HeaderData() As FacilityHeaderData

        Public ReadOnly Property SubjectToNsps() As Boolean?
            Get
                If HeaderData Is Nothing Then
                    Return Nothing
                Else
                    Return Convert.ToBoolean((Me.HeaderData.AirPrograms And AirProgram.NSPS) > 0)
                End If
            End Get
        End Property

        Public ReadOnly Property SubjectToPart70() As Boolean?
            Get
                If HeaderData Is Nothing Then
                    Return Nothing
                Else
                    Return Convert.ToBoolean((Me.HeaderData.AirPrograms And AirProgram.TitleV) > 0)
                End If
            End Get
        End Property

        Public Property Comment() As String

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

        Public Property ApprovedByApb() As Boolean?
        Public Property DistrictOfficeLocation() As String
        Public Property DistrictResponsible() As Boolean?
        Public Property ComplianceStatusList() As List(Of PollutantComplianceStatus)

        Public ReadOnly Property ControllingComplianceStatus() As PollutantComplianceStatus
            Get
                If _ComplianceStatusList Is Nothing OrElse _ComplianceStatusList.Count = 0 Then
                    Return PollutantComplianceStatus.NoValue
                Else
                    Return ComplianceStatusList.Max
                End If
            End Get
        End Property


#End Region

#Region " Methods "

        Public Sub RetrieveHeaderData()
            If Me IsNot Nothing Then
                HeaderData = DAL.GetFacilityHeaderData(Me.AirsNumber)
            End If
        End Sub

        Public Sub RetrieveComplianceStatusList()
            If Me IsNot Nothing Then
                ComplianceStatusList = DAL.FacilityModule.GetComplianceStatusList(Me.AirsNumber)
            End If
        End Sub

#End Region

#Region " Public shared functions "

        Public Shared Function SanitizeFacilityNameForDb(ByVal name As String) As String
            If String.IsNullOrEmpty(name) Then
                Return Nothing
            End If

            Dim sanitizedName As New System.Text.StringBuilder(name)
            sanitizedName.Replace("[", "(").Replace("]", ")")

            Return sanitizedName.ToString
        End Function

        Public Shared Function GetAirProgramDbKey(ByVal selectedAirProgram As AirProgram) As String
            Select Case selectedAirProgram
                Case AirProgram.AcidPrecipitation
                    Return "A"
                Case AirProgram.CfcTracking
                    Return "4"
                Case AirProgram.FederalSIP
                    Return "1"
                Case AirProgram.FESOP
                    Return "F"
                Case AirProgram.MACT
                    Return "M"
                Case AirProgram.NativeAmerican
                    Return "I"
                Case AirProgram.NESHAP
                    Return "8"
                Case AirProgram.NonFederalSIP
                    Return "3"
                Case AirProgram.NSPS
                    Return "9"
                Case AirProgram.NSR
                    Return "7"
                Case AirProgram.PSD
                    Return "6"
                Case AirProgram.RMP
                    Return "R"
                Case AirProgram.SIP
                    Return "0"
                Case AirProgram.TitleV
                    Return "V"
                Case Else
                    Return ""
            End Select
        End Function

#End Region

    End Class

End Namespace
