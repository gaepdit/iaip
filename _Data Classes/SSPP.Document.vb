Imports System.IO
Imports Oracle.DataAccess.Types

Namespace Apb

    Namespace SSPP

        Public Class PermitDocument
            Inherits Document

            Private _applicationNumber As String
            Public Property ApplicationNumber() As String
                Get
                    Return _applicationNumber
                End Get
                Set(ByVal value As String)
                    _applicationNumber = value
                End Set
            End Property

        End Class

    End Namespace

End Namespace
