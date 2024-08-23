Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient
Imports System.Runtime.CompilerServices

Namespace DAL

    ''' <summary>
    ''' Make sure you don't add table-valued parameters if the table is empty or null
    ''' See https://stackoverflow.com/a/6107942/212978
    ''' </summary>
    Module TableValuedParameter

        ''' <summary>
        ''' Returns a Structured (table-valued) SQL parameter using the Integer values provided
        ''' </summary>
        ''' <param name="parameterName">The SqlParameter name.</param>
        ''' <param name="values">An Integer IEnumerable.</param>
        ''' <returns>A table-valued SqlParameter of Integers, containing the supplied values.</returns>
        <Extension>
        <DebuggerStepThrough>
        Public Function AsTvpSqlParameter(values As IEnumerable(Of Integer), parameterName As String) As SqlParameter
            Return GaEpd.DBUtilities.TvpSqlParameter(parameterName, values, "dbo.IntegerTableType", "Item")
        End Function

        ''' <summary>
        ''' Returns a Structured (table-valued) SQL parameter using the String values provided
        ''' </summary>
        ''' <param name="parameterName">The SqlParameter name.</param>
        ''' <param name="values">A String IEnumerable.</param>
        ''' <returns>A table-valued SqlParameter of Strings, containing the supplied values.</returns>
        <Extension>
        <DebuggerStepThrough>
        Public Function AsTvpSqlParameter(values As IEnumerable(Of String), parameterName As String) As SqlParameter
            Return GaEpd.DBUtilities.TvpSqlParameter(parameterName, values, "dbo.StringTableType", "Item")
        End Function

    End Module
End Namespace