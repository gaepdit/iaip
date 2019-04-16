Imports System.Collections.Generic
Imports System.ComponentModel

Namespace Apb.Facilities
    Public Module FacilityEnums

#Region " Operational Statuses "

        ''' <summary>
        ''' The operational status of a facility.
        ''' </summary>
        ''' <remarks>Stored in database as a single-character string.</remarks>
        Public Enum FacilityOperationalStatus
            <Description("Unspecified")> U
            <Description("Operational")> O
            <Description("Planned")> P
            <Description("Under Construction")> C
            <Description("Temporarily Closed")> T
            <Description("Closed/Dismantled")> X
            <Description("Seasonal Operation")> I
        End Enum

        Public FacilityOperationalStatusDescriptions As New Dictionary(Of FacilityOperationalStatus, String) From {
            {FacilityOperationalStatus.U, "Unspecified"},
            {FacilityOperationalStatus.O, "Operational"},
            {FacilityOperationalStatus.P, "Planned"},
            {FacilityOperationalStatus.C, "Under Construction"},
            {FacilityOperationalStatus.T, "Temporarily Closed"},
            {FacilityOperationalStatus.X, "Closed/Dismantled"},
            {FacilityOperationalStatus.I, "Seasonal Operation"}
        }

        Public Enum EisSiteStatus
            UNK
            OP
            PS
            TS
        End Enum

        Public EisSiteStatusCodeDescriptions As New Dictionary(Of EisSiteStatus, String) From {
            {EisSiteStatus.UNK, "Unknown"},
            {EisSiteStatus.OP, "Operational"},
            {EisSiteStatus.PS, "Permanently shut down"},
            {EisSiteStatus.TS, "Temporarily shut down"}
        }

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

        Public FacilityClassificationDescriptions As New Dictionary(Of FacilityClassification, String) From {
            {FacilityClassification.A, "Major source"},
            {FacilityClassification.B, "Minor source"},
            {FacilityClassification.SM, "Synthetic minor"},
            {FacilityClassification.PR, "Permit by rule"},
            {FacilityClassification.C, "Unclassified"}
        }

        ''' <summary>
        ''' The CMS classification of a facility 
        ''' </summary>
        ''' <remarks>Stored in database as a nullable one-character string.</remarks>
        Public Enum FacilityCmsMember
            Unspecified
            <Description("Major")> A
            <Description("SM")> S
            <Description("None")> X
            <Description("Mega-site")> M
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
        <Flags()>
        Public Enum AirPrograms
            None = 0
            <Description("SIP")> SIP = 1 ' 100000000000000
            <Description("Federal SIP")> FederalSIP = 2 ' 010000000000000
            <Description("Non-Federal SIP")> NonFederalSIP = 4 ' 001000000000000
            <Description("CFC Tracking")> CfcTracking = 8 ' 000100000000000
            <Description("PSD")> PSD = 16 ' 000010000000000
            <Description("NSR")> NSR = 32 ' 000001000000000
            <Description("NESHAP")> NESHAP = 64 ' 000000100000000
            <Description("NSPS")> NSPS = 128 ' 000000010000000
            <Description("FESOP")> FESOP = 256 ' 000000001000000
            <Description("Acid Precipitation")> AcidPrecipitation = 512 ' 000000000100000
            <Description("Native American")> NativeAmerican = 1024 ' 000000000010000
            <Description("MACT")> MACT = 2048 ' 000000000001000
            <Description("Title V")> TitleV = 4096 ' 000000000000100
            <Description("Risk Management Plan")> RMP = 8192 ' 000000000000010
        End Enum

        Public AirProgramBitPosition As New Dictionary(Of AirPrograms, Integer) From {
            {AirPrograms.None, 0},
            {AirPrograms.SIP, 1},
            {AirPrograms.FederalSIP, 2},
            {AirPrograms.NonFederalSIP, 3},
            {AirPrograms.CfcTracking, 4},
            {AirPrograms.PSD, 5},
            {AirPrograms.NSR, 6},
            {AirPrograms.NESHAP, 7},
            {AirPrograms.NSPS, 8},
            {AirPrograms.FESOP, 9},
            {AirPrograms.AcidPrecipitation, 10},
            {AirPrograms.NativeAmerican, 11},
            {AirPrograms.MACT, 12},
            {AirPrograms.TitleV, 13},
            {AirPrograms.RMP, 14}
        }

        ''' <summary>
        ''' Bitwise flag for enumerating which air program classifications apply to a facility.
        ''' </summary>
        ''' <remarks>The enum value of the flags is significant because the flags are stored 
        ''' in the database as a (reversed) bitwise string. The string is 5 characters, but 
        ''' only the first 2 are used.</remarks>
        <Flags()>
        Public Enum AirProgramClassifications
            None = 0
            <Description("NSR/PSD Major")> NsrMajor = 1
            <Description("HAPs Major")> HapMajor = 2
        End Enum

        ''' <summary>
        ''' Air Programs (Rules) that have subparts
        ''' </summary>
        Public Enum RulePart
            SIP
            NSPS
            NESHAP
            MACT
        End Enum

#End Region

#Region " Header data "

        ''' <summary>
        ''' The action or user interface that initiates a change in facility header data
        ''' </summary>
        ''' <remarks>Stored in database as a numeric key.</remarks>
        Public Enum HeaderDataModificationLocation
            Unspecified = 0
            <Description("Permitting Action")> PermittingAction = 1
            <Description("Facility Header Editor")> HeaderDataEditor = 2
            <Description("SSCP Shutdown Notification")> SscpNotification = 3
            <Description("IAIP Facility Creation Tool")> FacilityCreationTool = 4
        End Enum

#End Region

    End Module
End Namespace