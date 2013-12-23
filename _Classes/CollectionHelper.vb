﻿Imports System.ComponentModel

Public Class CollectionHelper

    ' this is the method I have been using
    Public Shared Function ConvertToDataTable(Of T)(ByVal list As IList) As DataTable
        Dim table As DataTable = CreateTable(Of T)()
        Dim entityType As Type = GetType(T)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)

        For Each item As T In list
            Dim row As DataRow = table.NewRow()

            For Each prop As PropertyDescriptor In properties
                row(prop.Name) = If(prop.GetValue(item), DBNull.Value)
            Next

            table.Rows.Add(row)
        Next

        Return table
    End Function

    Public Shared Function CreateTable(Of T)() As DataTable
        Dim entityType As Type = GetType(T)
        Dim table As New DataTable(entityType.Name)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)

        For Each prop As PropertyDescriptor In properties
            table.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
        Next

        Return table
    End Function

End Class
