' Sources:
' FormatProvider for file sizes: http://stackoverflow.com/a/128683
' Nicer method for converting raw byte size to string: http://stackoverflow.com/a/4975942
' Getting FormatProvider to work on datagridview cells: http://stackoverflow.com/a/3576329

Public Class FileSizeFormatProvider
    Implements IFormatProvider
    Implements ICustomFormatter

    Public Function GetFormat(formatType As Type) As Object Implements System.IFormatProvider.GetFormat
        If formatType Is GetType(ICustomFormatter) Then
            Return Me
        End If
        Return Nothing
    End Function

    Private Const fileSizeFormat As String = "fs"
    Private Const OneKiloByte As Decimal = 1024D
    Private Const OneMegaByte As Decimal = OneKiloByte * 1024D
    Private Const OneGigaByte As Decimal = OneMegaByte * 1024D

    Public Function Format(format__1 As String, arg As Object, formatProvider As IFormatProvider) As String Implements System.ICustomFormatter.Format
        If format__1 Is Nothing OrElse Not format__1.StartsWith(fileSizeFormat) Then
            Return defaultFormat(format__1, arg, formatProvider)
        End If

        If TypeOf arg Is String Then
            Return defaultFormat(format__1, arg, formatProvider)
        End If

        Dim size As Decimal

        Try
            size = Convert.ToDecimal(arg)
        Catch generatedExceptionName As InvalidCastException
            Return defaultFormat(format__1, arg, formatProvider)
        End Try

        Dim suffix As String
        Dim suffixes As String() = {"B", "kB", "MB", "GB", "TB", "PB", "EB"} 'Longs run out around EB

        If size = 0 Then
            suffix = suffixes(0)
        Else
            Dim bytes As Long = Math.Abs(size)
            Dim place As Integer = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)))
            Dim num As Double = Math.Round(bytes / Math.Pow(1024, place), 1)
            size = (Math.Sign(size) * num)
            suffix = suffixes(place)
        End If

        Dim precision As String = format__1.Substring(3)
        If String.IsNullOrEmpty(precision) Then
            precision = "0"
        End If
        Return String.Format("{0:N" & Convert.ToString(precision) & "} {1}", size, suffix)

    End Function

    Private Shared Function defaultFormat(format As String, arg As Object, formatProvider As IFormatProvider) As String
        Dim formattableArg As IFormattable = TryCast(arg, IFormattable)
        If formattableArg IsNot Nothing Then
            Return formattableArg.ToString(format, formatProvider)
        End If
        Return arg.ToString()
    End Function

End Class