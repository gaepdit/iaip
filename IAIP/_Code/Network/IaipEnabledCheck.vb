Imports System.Threading.Tasks

Module IaipEnabledCheck

    Public Async Function IsIaipEnabledAsync() As Task(Of Boolean)
        Return Await Task.Run(
            Function()
                Return DAL.AppIsEnabled()
            End Function
            ).ConfigureAwait(False)
    End Function

End Module
