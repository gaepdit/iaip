Namespace DMU

    ''' <summary>
    ''' An EDT Error Message category and description
    ''' </summary>
    Public Class EdtErrorMessage

        Public Property ErrorCode() As String
            Get
                Return m_ErrorCode
            End Get
            Set(ByVal value As String)
                m_ErrorCode = value
            End Set
        End Property
        Private m_ErrorCode As String
        Public Property ErrorMessage() As String
            Get
                Return m_ErrorMessage
            End Get
            Set(ByVal value As String)
                m_ErrorMessage = value
            End Set
        End Property
        Private m_ErrorMessage As String
        Public Property ErrorCategory() As String
            Get
                Return m_ErrorCategory
            End Get
            Set(ByVal value As String)
                m_ErrorCategory = value
            End Set
        End Property
        Private m_ErrorCategory As String
        Public Property BusinessRuleCode() As String
            Get
                Return m_BusinessRuleCode
            End Get
            Set(ByVal value As String)
                m_BusinessRuleCode = value
            End Set
        End Property
        Private m_BusinessRuleCode As String
        Public Property BusinessRuleMessage() As String
            Get
                Return m_BusinessRuleMessage
            End Get
            Set(ByVal value As String)
                m_BusinessRuleMessage = value
            End Set
        End Property
        Private m_BusinessRuleMessage As String

        Public Property DefaultUserID() As Integer
            Get
                Return m_DefaultUserID
            End Get
            Set(ByVal value As Integer)
                m_DefaultUserID = value
            End Set
        End Property
        Private m_DefaultUserID As Integer
        Public Property DefaultUserName() As String
            Get
                Return m_DefaultUserName
            End Get
            Set(ByVal value As String)
                m_DefaultUserName = value
            End Set
        End Property
        Private m_DefaultUserName As String

    End Class

    ''' <summary>
    ''' An individual record submitted to the ICIS-Air EDT process
    ''' </summary>
    Public Class EdtSubmission

        Public Property EdtTableName() As String
            Get
                Return m_EdtTableName
            End Get
            Set(ByVal value As String)
                m_EdtTableName = value
            End Set
        End Property
        Private m_EdtTableName As String

        Public Property EdtID() As String
            Get
                Return m_EdtID
            End Get
            Set(ByVal value As String)
                m_EdtID = value
            End Set
        End Property
        Private m_EdtID As String

        Public Property EdtForeignKeyID() As String
            Get
                Return m_EdtForeignKeyID
            End Get
            Set(ByVal value As String)
                m_EdtForeignKeyID = value
            End Set
        End Property
        Private m_EdtForeignKeyID As String

        Public Property EdtOperation() As String
            Get
                Return m_EdtOperation
            End Get
            Set(ByVal value As String)
                m_EdtOperation = value
            End Set
        End Property
        Private m_EdtOperation As String

        Public Property EdtStatus() As String
            Get
                Return m_EdtStatus
            End Get
            Set(ByVal value As String)
                m_EdtStatus = value
            End Set
        End Property
        Private m_EdtStatus As String

        Public Property EdtSubmitDate() As DateTime
            Get
                Return m_EdtSubmitDate
            End Get
            Set(ByVal value As DateTime)
                m_EdtSubmitDate = value
            End Set
        End Property
        Private m_EdtSubmitDate As DateTime

    End Class

    ''' <summary>
    ''' An individual error record recorded by the ICIS-Air EDT submission process
    ''' </summary>
    Public Class EdtError

        Public Property ErrorID() As Integer
            Get
                Return m_ErrorID
            End Get
            Set(ByVal value As Integer)
                m_ErrorID = value
            End Set
        End Property
        Private m_ErrorID As Integer

        Public Property ErrorMessage() As EdtErrorMessage
            Get
                Return m_ErrorMessage
            End Get
            Set(ByVal value As EdtErrorMessage)
                m_ErrorMessage = value
            End Set
        End Property
        Private m_ErrorMessage As EdtErrorMessage

        Public Property EdtErrorMessageDetail() As String
            Get
                Return m_EdtErrorMessageDetail
            End Get
            Set(ByVal value As String)
                m_EdtErrorMessageDetail = value
            End Set
        End Property
        Private m_EdtErrorMessageDetail As String

        Public Property EdtSubmission() As EdtSubmission
            Get
                Return m_EdtSubmission
            End Get
            Set(ByVal value As EdtSubmission)
                m_EdtSubmission = value
            End Set
        End Property
        Private m_EdtSubmission As EdtSubmission

        Public Property AssignedToUserID() As Integer
            Get
                Return m_AssignedToUserID
            End Get
            Set(ByVal value As Integer)
                m_AssignedToUserID = value
            End Set
        End Property
        Private m_AssignedToUserID As Integer

        Public Property Resolved() As Boolean
            Get
                Return m_Resolved
            End Get
            Set(ByVal value As Boolean)
                m_Resolved = value
            End Set
        End Property
        Private m_Resolved As Boolean

        Public Property ResolvedDate() As DateTime?
            Get
                Return m_ResolvedDate
            End Get
            Set(ByVal value As DateTime?)
                m_ResolvedDate = value
            End Set
        End Property
        Private m_ResolvedDate As DateTime?

        Public Property ResolvedByUserID() As Integer
            Get
                Return m_ResolvedByUserID
            End Get
            Set(ByVal value As Integer)
                m_ResolvedByUserID = value
            End Set
        End Property
        Private m_ResolvedByUserID As Integer

        Public Property ResolvedByUserName() As String
            Get
                Return m_ResolvedByUserName
            End Get
            Set(ByVal value As String)
                m_ResolvedByUserName = value
            End Set
        End Property
        Private m_ResolvedByUserName As String

    End Class

End Namespace
