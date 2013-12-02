Imports System.IO
Imports Oracle.DataAccess.Types

Namespace Apb

    Namespace SSCP

        Public Class EnforcementDocument
            Inherits Document

            Private _enforcementNumber As String
            Public Property EnforcementNumber() As String
                Get
                    Return _enforcementNumber
                End Get
                Set(ByVal value As String)
                    _enforcementNumber = value
                End Set
            End Property

        End Class

    End Namespace

End Namespace
