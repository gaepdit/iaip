Public NotInheritable Class IaipColors
    Public Shared ReadOnly Property ControlColorPair As ColorPair
        Get
            Return New ColorPair With {.ForegroundColor = SystemColors.ControlText, .BackgroundColor = SystemColors.Control}
        End Get
    End Property

    Public Shared ReadOnly Property ErrorForeColor As Color
        Get
            Return ColorTranslator.FromHtml("#800000")
        End Get
    End Property
    Public Shared ReadOnly Property ErrorBackColor As Color
        Get
            Return Color.Pink
        End Get
    End Property
    Public Shared ReadOnly Property ErrorColorPair As ColorPair
        Get
            Return New ColorPair With {.ForegroundColor = ErrorForeColor, .BackgroundColor = ErrorBackColor}
        End Get
    End Property

    Public Shared ReadOnly Property WarningForeColor As Color
        Get
            Return ColorTranslator.FromHtml("#6A3611")
        End Get
    End Property
    Public Shared ReadOnly Property WarningBackColor As Color
        Get
            Return Color.Bisque
        End Get
    End Property
    Public Shared ReadOnly Property WarningColorPair As ColorPair
        Get
            Return New ColorPair With {.ForegroundColor = WarningForeColor, .BackgroundColor = WarningBackColor}
        End Get
    End Property

    Public Shared ReadOnly Property SuccessForeColor As Color
        Get
            Return ColorTranslator.FromHtml("#004400")
        End Get
    End Property
    Public Shared ReadOnly Property SuccessBackColor As Color
        Get
            Return Color.LightGreen
        End Get
    End Property
    Public Shared ReadOnly Property SuccessColorPair As ColorPair
        Get
            Return New ColorPair With {.ForegroundColor = SuccessForeColor, .BackgroundColor = SuccessBackColor}
        End Get
    End Property

    Public Shared ReadOnly Property InfoForeColor As Color
        Get
            Return SystemColors.InfoText
        End Get
    End Property
    Public Shared ReadOnly Property InfoBackColor As Color
        Get
            Return SystemColors.Info
        End Get
    End Property
    Public Shared ReadOnly Property InfoColorPair As ColorPair
        Get
            Return New ColorPair With {.ForegroundColor = InfoForeColor, .BackgroundColor = InfoBackColor}
        End Get
    End Property

    Public Shared ReadOnly Property GridHoverForeColor As Color
        Get
            Return Color.Blue
        End Get
    End Property

    Public Shared ReadOnly Property GridHoverBackColor As Color
        Get
            Return ColorTranslator.FromHtml("#FDF2FD")
        End Get
    End Property

    Public Shared ReadOnly Property GridSelectionHoverForeColor As Color
        Get
            Return SystemColors.HighlightText
        End Get
    End Property

    Public Shared ReadOnly Property GridSelectionHoverBackColor As Color
        Get
            Return SystemColors.HotTrack
        End Get
    End Property
End Class

Public Class ColorPair
    Public Property ForegroundColor As Color
    Public Property BackgroundColor As Color
End Class
