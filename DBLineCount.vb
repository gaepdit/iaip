Imports System
Imports System.Collections
Imports System.IO

Namespace LineCount
    Public Class DBLineCount
        'FileNames holds the names of files in the project directories
        Protected FileNames As New ArrayList(400)
        Public Sub New()
        End Sub 'New
        '/ it returns filenames in the project
        '/ </summary>
        Public ReadOnly Property FilesInProject() As ArrayList
            Get
                Return FileNames
            End Get
        End Property
        '/ <summary>
        '/ this function returns the count of code lines
        '/ </summary>
        '/ <returns></returns>
        Public Function GetLineCount() As Integer
            Dim LineCount As Integer = 0
            ' this array holds file types, you can add more file types if you want
            'Dim myFileArray(7) As [String] = {"*.cs", "*.aspx", "*.ascx", "*.xml", "*.asax", "*.config", "*.js"}
            Dim myFileArray() As String = {"*.cs", "*.vb", "*.ascx", "*.xml", "*.asax", "*.config", "*.js"}
            ' this array holds directories where your project files resides
            'Dim myDirectoryArray(2) As [String] = {"c:\inetpub\wwwroot\supplynet\", "d:\Net Projects\SpNetComponents\"}
            Dim myDirectoryArray() As String = {"C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\ISMP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\ISMP\DMU", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\ISMP\SMU", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\ISMP\SMU\SubPages", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\Clases", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\DataSet", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\DevelopmentForms", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 9.2\APB", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 9.2\DMU", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 9.2\ISMP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 9.2\PASP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 9.2\SSCP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 9.2\SSPP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 10.1", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 10.1\APB", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 10.1\APB\SubReports", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 10.1\DMU", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 10.1\PASP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 10.1\SmokeSchool", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\IAIP-Crystal Reports\Oracle 10.1\SSPP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\MASP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\My Project", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\PASP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\SmokeSchool", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\SSCP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\SSCP\SubPages", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\SSPP", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\SSPP\Sub Forms", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\SubMain", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\Universial Forms", "C:\Program Files\Visual Studio 2008\Projects\JohnGaltProject\Universial Forms\Sub Forms"}
            'this loops directories
            Dim sd As [String]
            For Each sd In myDirectoryArray
                Dim dir As New DirectoryInfo(sd)
                ' this loops file types
                Dim sFileType As [String]
                For Each sFileType In myFileArray
                    ' this loops files
                    Dim file As FileInfo
                    For Each file In dir.GetFiles(sFileType)
                        ' add the file name to FileNames ArrayList
                        FileNames.Add(file.FullName)
                        ' open files for streamreader
                        'File.OpenText(file.FullName)
                        Dim sr As StreamReader = New StreamReader(file.FullName)
                        'file.OpenText(file.FullName)
                        'loop through file
                        While Not (sr.ReadLine() Is Nothing)
                            LineCount += 1
                        End While
                        'close the streamreader
                        sr.Close()
                    Next file
                Next sFileType
            Next sd
            Return LineCount
        End Function 'GetLineCount
    End Class
End Namespace 'LineCount

