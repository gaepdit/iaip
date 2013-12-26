Imports System.Reflection
Imports Oracle.DataAccess.Client
Imports System.Web
Imports System.IO

Module App

#Region "Startup and shutdown"

    ' MyApplication_Startup -> App.Init()
    Friend Sub Init()

#If DEBUG Then
        Console.WriteLine("Me.Startup")
        Console.WriteLine("Environment.MachineName: " & Environment.MachineName)
        Console.WriteLine("Environment.UserName: " & Environment.UserName)
#End If

        'Settings
        If My.Settings.CallUpgrade Then
            My.Settings.Upgrade()
            My.Settings.CallUpgrade = False
        End If

        ' EQATEC analytics monitor
        MonitorInit()

        ' Form settings
        AllFormSettings = GetAllFormSettings()

        '' Just for fun: sample code for a new way of handling login form
        '' http://visualstudiomagazine.com/articles/2008/08/01/customize-your-application-startup.aspx
        'Dim frm = New LoginForm
        'Dim result = frm.ShowDialog
        'If result <> DialogResult.OK Then
        '    e.Cancel = True
        '    Me.HideSplashScreen()
        'End If

    End Sub

    ' MyApplication_Shutdown -> App.Finish()
    Friend Sub Finish()

        ' Form settings
        SaveAllFormSettings()

        ' EQATEC analytics monitor
        MonitorStop()

    End Sub

#End Region

#Region "URL handling"

    Public Sub OpenHelpUrl(Optional ByVal objectSender As Object = Nothing)
        monitor.TrackFeature("Url.OpenHelp")
        OpenUrl(HelpUrl, objectSender)
    End Sub

    Public Sub OpenDownloadUrl(Optional ByVal objectSender As Object = Nothing)
        monitor.TrackFeature("Url.OpenDownload")
        OpenUrl(DownloadUrl, objectSender)
    End Sub

    Public Sub OpenAboutUrl(Optional ByVal objectSender As Object = Nothing)
        monitor.TrackFeature("Url.OpenAbout")

        CreateVersionFile()
        OpenUrl(AboutUrl, objectSender)
    End Sub

    Public Function OpenUrl(ByVal url As String, Optional ByVal objectSender As Object = Nothing) As Boolean
        ' Reference: http://code.logos.com/blog/2008/01/using_processstart_to_link_to.html
        If url Is Nothing Then Exit Function

        If objectSender IsNot Nothing Then
            objectSender.Cursor = Cursors.AppStarting
        End If
        Try
            Process.Start(url)
            Return True
        Catch ee As Exception When _
        TypeOf ee Is System.ComponentModel.Win32Exception OrElse _
        TypeOf ee Is System.ObjectDisposedException OrElse _
        TypeOf ee Is System.IO.FileNotFoundException
            Return False
        Finally
            If objectSender IsNot Nothing Then
                objectSender.Cursor = Nothing
            End If
        End Try
    End Function

#End Region

#Region "Validation"

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
    Friend PublishedVersion As Version = Nothing
    Friend CurrentVersion As Version = Nothing
    Friend ReleaseDate As New DateTime(1970, 1, 1, 0, 0, 0)
    Friend VersionFileUpdated As Boolean = False

    Private Sub CreateVersionFile()
        If Not VersionFileUpdated Then
            Dim ThisReleaseDate As String = RetrieveLinkerTimestamp(Application.ExecutablePath).ToString("MMMM d, yyyy")
            Dim ThisVersion As String = GetCurrentVersionAsBuild.ToString
            Dim VersionFilePath As String = Path.GetDirectoryName(Application.ExecutablePath) & "\docs\version.js"

            Dim FileContents As String = _
                "var version = {" & _
                    "'number' : '" & ThisVersion & "'," & _
                    "'releaseDate' : '" & ThisReleaseDate & "'" & _
                "}"

            Dim sw As StreamWriter = Nothing
            Try
                sw = File.CreateText(VersionFilePath)
                sw.WriteLine(FileContents)
                sw.Flush()
                sw.Close()
                VersionFileUpdated = True
            Finally
                If Not sw Is Nothing Then sw.Close()
            End Try
        End If
    End Sub

    Private Function RetrieveLinkerTimestamp(ByVal filePath As String) As DateTime
        If ReleaseDate = New DateTime(1970, 1, 1, 0, 0, 0) Then
            Const PeHeaderOffset As Integer = 60
            Const LinkerTimestampOffset As Integer = 8

            Dim b(2047) As Byte
            Dim s As Stream = Nothing
            Try
                s = New FileStream(filePath, FileMode.Open, FileAccess.Read)
                s.Read(b, 0, 2048)
            Finally
                If Not s Is Nothing Then s.Close()
            End Try

            Dim i As Integer = BitConverter.ToInt32(b, PeHeaderOffset)

            Dim SecondsSince1970 As Integer = BitConverter.ToInt32(b, i + LinkerTimestampOffset)
            ReleaseDate = ReleaseDate.AddSeconds(SecondsSince1970)
            ReleaseDate = ReleaseDate.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(ReleaseDate).Hours)
        End If
        Return ReleaseDate
    End Function

    Public Function GetCurrentVersion() As Version
        ' This is the currently installed (running) version

        If CurrentVersion Is Nothing Then
            Dim thisAssembly As Assembly = Assembly.GetExecutingAssembly()
            Dim fileVersionInfo As FileVersionInfo = fileVersionInfo.GetVersionInfo(thisAssembly.Location)
            CurrentVersion = New Version(fileVersionInfo.FileVersion)
        End If

        Return CurrentVersion
    End Function

    Public Function GetCurrentVersionAsBuild() As Version
        Return GetVersionAsBuild(GetCurrentVersion)
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

#Region "String functions"

    Public Function ConcatNonEmptyStrings(ByVal separator As String, ByVal value() As String) As String
        Return String.Join(separator, Array.FindAll(value, Function(s) Not String.IsNullOrEmpty(s)))
    End Function

    Public Function FormatStringAsPhoneNumber(ByVal p As String) As String
        If p Is Nothing Then Return p
        If Not System.Text.RegularExpressions.Regex.IsMatch(p, "^[0-9 ]+$") Then Return p
        If Not (p.Length = 7 Or p.Length >= 10) Then Return p

        If p.Length = 7 Then
            Return p.Substring(0, 3) & "-" & p.Substring(4, 4)
        ElseIf p.Length = 10 Then
            Return "(" & p.Substring(0, 3) & ") " & p.Substring(3, 3) & "-" & p.Substring(6, 4)
        Else
            Return "(" & p.Substring(0, 3) & ") " & p.Substring(3, 3) & "-" & p.Substring(6, 4) & " Ext. " & p.Substring(10, p.Length - 10)
        End If
    End Function

#End Region

#Region "Date functions"

    Public Function NormalizeDate(ByVal d As Date?) As Date?
        ' Converts a date to Nothing if date is equal to #7/4/1776#

        If d.Equals(CType(Nothing, Date)) Then Return d
        If Not IsDate(d) Then Return Nothing
        If d.Equals(New Date(1776, 7, 4)) Then Return Nothing
        Return d
    End Function

#End Region

End Module
