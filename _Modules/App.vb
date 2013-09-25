Imports System.Reflection
Imports Oracle.DataAccess.Client
Imports System.Collections.Generic
Imports EQATEC.Analytics.Monitor
Imports System.Web
Imports System.IO

Module App

#Region "Application Analytics Monitoring"
    ' Using EQATEC.Analytics.Monitor Library
    Friend monitor As IAnalyticsMonitor
    Friend monitorSettings As IAnalyticsMonitorSettings = AnalyticsMonitorFactory.CreateSettings(AnalyticsApiKey)
    Friend monitorInstallationInfo As New Dictionary(Of String, String)
    ' Don't create the monitor yet. Change settings in MyApplication_Startup, 
    ' then create & start the monitor there
#End Region

#Region "URL handling"
    Public Sub SendEmail(ByVal address As String, _
                         Optional ByVal subject As String = Nothing, _
                         Optional ByVal body As String = Nothing, _
                         Optional ByVal sender As Object = Nothing)
        monitor.TrackFeature("Url.SendEmail")

        If subject IsNot Nothing Then subject = Uri.EscapeDataString(subject)
        If body IsNot Nothing Then body = Uri.EscapeDataString(body)

        Dim emailUrl As String = String.Format("mailto:{0}?subject={1}&body={2}", address, subject, body)

        OpenUrl(emailUrl, sender)
    End Sub
    Public Sub OpenHelpUrl(Optional ByVal sender As Object = Nothing)
        monitor.TrackFeature("Url.OpenHelp")
        OpenUrl(HelpUrl, sender)
    End Sub
    Public Sub OpenDownloadUrl(Optional ByVal sender As Object = Nothing)
        monitor.TrackFeature("Url.OpenDownload")
        OpenUrl(DownloadUrl, sender)
    End Sub
    Public Sub OpenAboutUrl(Optional ByVal sender As Object = Nothing)
        monitor.TrackFeature("Url.OpenAbout")
        OpenUrl(AboutUrl, sender)
    End Sub
    Private Sub OpenUrl(ByVal url As String, Optional ByVal sender As Object = Nothing)
        ' Reference: http://code.logos.com/blog/2008/01/using_processstart_to_link_to.html
        If url Is Nothing Then Exit Sub

        If sender IsNot Nothing Then
            sender.Cursor = Cursors.AppStarting
        End If
        Try
            Process.Start(url)
        Catch ee As Exception When _
        TypeOf ee Is System.ComponentModel.Win32Exception OrElse _
        TypeOf ee Is System.ObjectDisposedException OrElse _
        TypeOf ee Is System.IO.FileNotFoundException
        Finally
            If sender IsNot Nothing Then
                sender.Cursor = Nothing
            End If
        End Try
    End Sub
    Public Function IsValidEmail(ByVal email As String) As Boolean
        If String.IsNullOrEmpty(email) Then Return False
        Try
            Dim testEmail As Net.Mail.MailAddress = New Net.Mail.MailAddress(email)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
#End Region

#Region "Versioning Info"
    Public Function GetCurrentVersion() As Version
        ' This is the currently installed (running) version

        If CurrentVersion Is Nothing Then
            Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
            Dim fileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(thisAssembly.Location)
            CurrentVersion = New Version(fileVersionInfo.FileVersion)
        End If

        Return CurrentVersion
    End Function
    Public Function GetPublishedVersion(Optional ByVal appName As String = AppName) As Version
        ' This is the latest available version as listed in the database
        ' (The database has to be updated by hand by an administrator)

        If PublishedVersion Is Nothing OrElse PublishedVersion.Equals(New Version("0.0.0.0")) Then
            Dim publishedVersionString As String = ""

            ' Hit up the database for a version string
            Dim query As String = "Select strVersionNumber " & _
                "from " & DBNameSpace & ".APBMasterApp " & _
                "where strApplicationName = :pAppName"
            Using connection As New OracleConnection(CurrentConnString)
                Using command As New OracleCommand(query, connection)
                    command.CommandType = CommandType.Text
                    command.Parameters.Add(":pAppName", OracleDbType.Varchar2).Value = appName

                    Try
                        connection.Open()
                        Dim reader As OracleDataReader = command.ExecuteReader
                        While reader.Read
                            If Not IsDBNull(reader.Item("strVersionNumber")) Then
                                publishedVersionString = reader.Item("strVersionNumber")
                            End If
                        End While
                    Catch ee As OracleException
                        'MessageBox.Show("Could not connect to the database.")
                        publishedVersionString = "0.0.0.0"
                    End Try
                End Using
            End Using

            Try
                PublishedVersion = New Version(publishedVersionString)
            Catch ee As Exception When _
            TypeOf ee Is ArgumentException OrElse _
            TypeOf ee Is ArgumentNullException OrElse _
            TypeOf ee Is ArgumentOutOfRangeException OrElse _
            TypeOf ee Is FormatException OrElse _
            TypeOf ee Is OverflowException
                MessageBox.Show("The database version string contains an error. Please inform the Data Management Unit. Thank you.")
                PublishedVersion = New Version("0.0.0.0")
            End Try
        End If

        Return PublishedVersion
    End Function
    Public Function IsUpdateAvailable() As Boolean
        ' If Version has increased, update is available
        Dim currentVersion As Version = GetCurrentVersion()
        Dim publishedVersion As Version = GetPublishedVersion()

        ' If database has an error, published version will be 0.0.0.0. This will return false.
        Return currentVersion.CompareTo(publishedVersion) < 0
    End Function
    Public Function IsUpdateMandatory() As Boolean
        ' If Version has increased beyond just the Revision number, then update is mandatory
        Dim currentVersion As Version = GetCurrentVersion()
        Dim publishedVersion As Version = GetPublishedVersion()

        ' If database has an error, published version will be 0.0.0.0. This will return false.
        Return GetVersionAsMajorMinor(currentVersion).CompareTo(GetVersionAsMajorMinor(publishedVersion)) < 0
    End Function
    Private Function GetVersionAsBuild(ByVal v As Version) As Version
        ' This converts a Version from four components to three
        If v.Revision = -1 Then Return v ' (A version with fewer than four components gets returned as-is)
        Return New Version(v.Major, v.Minor, v.Build)
    End Function
    Private Function GetVersionAsMajorMinor(ByVal v As Version) As Version
        ' This converts a Version from four components to three
        If v.Build = -1 Then Return v ' (A version with fewer than three components gets returned as-is)
        Return New Version(v.Major, v.Minor)
    End Function
#End Region

End Module
