Imports System.Deployment.Application

Public Module AppVersion

    Private CurrentVersion As Version

    Public Function GetCurrentVersion() As Version
        ' This is the currently installed (running) version

        If CurrentVersion Is Nothing Then
            If ApplicationDeployment.IsNetworkDeployed Then
                CurrentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion
            Else
                CurrentVersion = New Version(Application.ProductVersion)
            End If
        End If

        Return CurrentVersion
    End Function

    Public Function GetCurrentVersionAsMajorMinorBuild() As Version
        Return GetVersionAsMajorMinorBuild(GetCurrentVersion)
    End Function

    Friend Function GetVersionAsMajorMinorBuild(v As Version) As Version
        ' This converts a Version from four components to three
        If v.Revision = -1 Then Return v ' (A version with fewer than four components gets returned as-is)
        Return New Version(v.Major, v.Minor, v.Build)
    End Function

End Module
