Public Module SavedSessions

    Public Sub UpdateSession(save As Boolean)
        If save Then
            SaveSession()
        Else
            RemoveLocalSession()
        End If
    End Sub

    Public Function ValidateSession() As Integer
        Dim sc As Specialized.StringCollection = My.Settings.SavedSession

        If sc IsNot Nothing AndAlso sc.Count = 2 Then
            Dim userId As Integer
            If Integer.TryParse(sc(0), userId) Then
                monitor.TrackFeatureStart("Startup.ValidatingSession")
                Dim newToken As String = DAL.ValidateSession(userId, sc(1))
                If Not String.IsNullOrEmpty(newToken) Then
                    SaveLocalSession(userId, newToken)
                    Return userId
                End If
            End If
        End If

        RemoveLocalSession()
        Return -1
    End Function

    Private Sub SaveSession()
        Dim token As String = DAL.SaveSession(CurrentUser.UserID)

        If Not String.IsNullOrEmpty(token) Then
            SaveLocalSession(CurrentUser.UserID, token)
        Else
            RemoveLocalSession()
        End If
    End Sub

    Private Sub SaveLocalSession(userId As Integer, token As String)
        Dim sc As New Specialized.StringCollection()
        sc.Insert(0, userId)
        sc.Insert(1, token)
        My.Settings.SavedSession = sc
    End Sub

    Private Sub RemoveLocalSession()
        If CurrentUser IsNot Nothing Then
            DAL.RevokeSession(CurrentUser.UserID)
        End If
        My.Settings.SavedSession = New Specialized.StringCollection()
    End Sub

End Module
