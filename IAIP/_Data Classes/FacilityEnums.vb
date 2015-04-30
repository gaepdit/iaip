Imports System.ComponentModel

Namespace Apb.Facilities

#Region " Operation "

    ''' <summary>
    ''' The operational status of a facility.
    ''' </summary>
    ''' <remarks>Stored in database as a single-character string.</remarks>
    Public Enum FacilityOperationalStatus
        Unspecified
        <Description("Operational")> O
        <Description("Planned")> P
        <Description("Under Construction")> C
        <Description("Temporarily Closed")> T
        <Description("Closed/Dismantled")> X
        <Description("Seasonal Operation")> I
    End Enum

    ''' <summary>
    ''' The source classification of a facility (based on permit type).
    ''' </summary>
    ''' <remarks>Stored in database as a two-character string.</remarks>
    Public Enum FacilityClassification
        Unspecified
        <Description("Major source")> A
        <Description("Minor source")> B
        <Description("Synthetic minor")> SM
        <Description("Permit by rule")> PR
        <Description("Unclassified")> C
    End Enum

    ''' <summary>
    ''' The CMS classification of a facility 
    ''' </summary>
    ''' <remarks>Stored in database as a nullable one-character string.</remarks>
    Public Enum FacilityCmsMember
        Unspecified
        <Description("Major")> A
        <Description("SM")> S
        <Description("None")> X
    End Enum

#End Region

#Region " Nonattainment status "

    ''' <summary>
    ''' Specifies whether a facility is located within a one-hour ozone nonattainment area.
    ''' </summary>
    ''' <remarks>The value of each enumeration member is significant because the members are stored
    ''' and retrieved from the database in a coded string (along with EightHourNonattainmentStatus and
    ''' PMFineNonattainmentStatus.)</remarks>
    Public Enum OneHourOzoneNonattainmentStatus
        No = 0
        Yes = 1
        Contribute = 2
    End Enum

    ''' <summary>
    ''' Specifies whether a facility is located within an eight-hour ozone nonattainment area.
    ''' </summary>
    ''' <remarks>The value of each enumeration member is significant because the members are stored
    ''' and retrieved from the database in a coded string (along with OneHourNonattainmentStatus and
    ''' PMFineNonattainmentStatus.)</remarks>
    Public Enum EightHourOzoneNonattainmentStatus
        None = 0
        Atlanta = 1
        Macon = 2
    End Enum

    ''' <summary>
    ''' Specifies whether a facility is located within a PM Fine (PM 2.5) nonattainment area.
    ''' </summary>
    ''' <remarks>The value of each enumeration member is significant because the members are stored
    ''' and retrieved from the database in a coded string (along with EightHourNonattainmentStatus and
    ''' OneHourNonattainmentStatus.)</remarks>
    Public Enum PMFineNonattainmentStatus
        None = 0
        Atlanta = 1
        Chattanooga = 2
        Floyd = 3
        Macon = 4
    End Enum

#End Region

#Region " Program codes "

    ''' <summary>
    ''' Bitwise flag for enumerating which air programs apply to a facility.
    ''' </summary>
    ''' <remarks>The enum value of the flags is significant because the flags are stored 
    ''' in the database as a (reversed) bitwise string. The string is 15 characters, but 
    ''' only the first 14 are used.</remarks>
    <Flags()> Public Enum AirProgram
        None = 0
        <Description("SIP")> SIP = 1
        <Description("Federal SIP")> FederalSIP = 2
        <Description("Non-Federal SIP")> NonFederalSIP = 4
        <Description("CFC Tracking")> CfcTracking = 8
        <Description("PSD")> PSD = 16
        <Description("NSR")> NSR = 32
        <Description("NESHAP (Part 61)")> NESHAP = 64
        <Description("NSPS")> NSPS = 128
        <Description("FESOP")> FESOP = 256
        <Description("Acid Precipitation")> AcidPrecipitation = 512
        <Description("Native American")> NativeAmerican = 1024
        <Description("MACT (Part63)")> MACT = 2048
        <Description("Title V")> TitleV = 4096
        <Description("Risk Management Plan")> RMP = 8192
    End Enum

    ''' <summary>
    ''' Bitwise flag for enumerating which air program classifications apply to a facility.
    ''' </summary>
    ''' <remarks>The enum value of the flags is significant because the flags are stored 
    ''' in the database as a (reversed) bitwise string. The string is 5 characters, but 
    ''' only the first 2 are used.</remarks>
    <Flags()> Public Enum AirProgramClassifications
        None = 0
        <Description("NSR/PSD Major")> NsrMajor = 1
        <Description("HAPs Major")> HapMajor = 2
    End Enum

#End Region

#Region " Header data "

    ''' <summary>
    ''' The action or user interface that initiates a change in facility header data
    ''' </summary>
    ''' <remarks>Stored in database as a numeric key.</remarks>
    Public Enum HeaderDataModificationLocation
        Unspecified
        <Description("Permitting Action")> PermittingAction = 1
        <Description("Facility Header Editor")> HeaderDataEditor = 2
        <Description("SSCP Shutdown Notification")> SscpNotification = 3
        <Description("IAIP Facility Creation Tool")> FacilityCreationTool = 4
    End Enum

    ''' <summary>
    ''' Compliance Status codes. These are applied on a per-pollutant, per-rule basis. 
    ''' </summary>
    ''' <remarks>Stored in database as a one-character string. The enum values are 
    ''' significant as they are used to determine controlling compliance status.</remarks>
    Public Enum PollutantComplianceStatus
        <Description("In violation, procedural & emissions")> Status_B = 35
        <Description("In violation, no schedule")> Status_1 = 34
        <Description("In violation, not meeting schedule")> Status_6 = 33
        <Description("In violation, procedural")> Status_W = 32
        <Description("In violation, no applicable state reg")> Status_8 = 31
        <Description("Unknown compliance status")> Status_0 = 21
        <Description("Meeting compliance schedule")> Status_5 = 11
        <Description("In compliance, source test")> Status_2 = 6
        <Description("In compliance, inspection ")> Status_3 = 5
        <Description("In compliance, certification ")> Status_4 = 4
        <Description("In compliance, shut down")> Status_9 = 3
        <Description("In compliance, procedural")> Status_C = 2
        <Description("In compliance, CEMS data")> Status_M = 1
        <Description("No value")> NoValue = 0
    End Enum

#End Region


End Namespace