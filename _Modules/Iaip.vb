Imports System.Reflection
Imports System.Data.OracleClient

Module Iaip

#Region "Versioning Info"
    Public Function GetCurrentVersion() As Version
        ' This is the currently installed (running) version
        Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim fileVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(thisAssembly.Location)
        Return New Version(fileVersion.FileVersion)
    End Function
    Public Function GetPublishedVersion() As Version
        ' This is the latest available version as listed in the database
        ' (The database has to be updated by hand by an administrator)
        Dim publishedVersionString As String = ""

        ' Hit up the database for a version string
        Dim sql As String = "Select strVersionNumber from " & DBNameSpace & ".APBMasterApp where strApplicationName = 'IAIP'"
        Using dbConn As New OracleConnection(CurrentConnString)
            Using dbCommand As New OracleCommand(sql, dbConn)
                Try
                    dbConn.Open()
                    Dim reader As OracleDataReader = dbCommand.ExecuteReader
                    While reader.Read
                        If Not IsDBNull(reader.Item("strVersionNumber")) Then
                            publishedVersionString = reader.Item("strVersionNumber")
                        End If
                    End While
                Catch ee As OracleException
                    MessageBox.Show("The database is currently unavailable.")
                    publishedVersionString = "0.0.0.0"
                End Try
            End Using
        End Using

        Try
            Return New Version(publishedVersionString)
        Catch ee As Exception When _
        TypeOf ee Is ArgumentException OrElse _
        TypeOf ee Is ArgumentNullException OrElse _
        TypeOf ee Is ArgumentOutOfRangeException OrElse _
        TypeOf ee Is FormatException OrElse _
        TypeOf ee Is OverflowException
            MessageBox.Show("The database version string contains an error. Please inform the Data Management Unit. Thank you.")
            Return New Version("0.0.0.0")
        End Try
    End Function
    Public Function IsUpdateAvailable() As Boolean
        ' If Version has increased, update is available
        Dim currentVersion As Version = GetCurrentVersion()
        Dim publishedVersion As Version = GetPublishedVersion()

        ' If database has an error, published version will be 0.0.0.0. This will return false.
        If currentVersion.CompareTo(publishedVersion) < 0 Then Return True
        Return False
    End Function
    Public Function IsUpdateMandatory() As Boolean
        ' If Version has increased beyond just the Revision number, then update is mandatory
        Dim currentVersion As Version = GetCurrentVersion()
        Dim publishedVersion As Version = GetPublishedVersion()

        ' If database has an error, published version will be 0.0.0.0. This will return false.
        If GetVersionAsBuild(currentVersion).CompareTo(GetVersionAsBuild(publishedVersion)) < 0 Then Return True
        Return False
    End Function
    Private Function GetVersionAsBuild(ByVal v As Version) As Version
        ' This converst a Version from four components to three
        If v.Revision = -1 Then Return v
        ' (A version with fewer than four components gets returned as-is)
        Return New Version(v.Major, v.Minor, v.Build)
    End Function
#End Region
End Module
