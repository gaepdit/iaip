﻿Imports System.Collections.Generic

Namespace DAL
    Module State

        Public Function GetCountiesAsDictionary() As Dictionary(Of Integer, String)
            Dim query As String = "SELECT CONVERT(int, STRCOUNTYCODE) AS CountyCode, 
                STRCOUNTYNAME AS County
                FROM LOOKUPCOUNTYINFORMATION
                ORDER BY STRCOUNTYNAME"
            Return DB.GetLookupDictionary(query)
        End Function

        Public Function GetCountiesAsDataTable() As DataTable
            Dim query As String = "SELECT STRCOUNTYCODE AS CountyCode, 
                STRCOUNTYNAME AS County
                FROM LOOKUPCOUNTYINFORMATION
                ORDER BY STRCOUNTYNAME"
            Return DB.GetDataTable(query)
        End Function

        Public Function GetDistrictOffices() As DataTable
            Dim query As String = "SELECT STRDISTRICTCODE AS DistrictCode, 
                STRDISTRICTNAME AS DistrictName, STRDISTRICTMANAGER AS Manager
                FROM LOOKUPDISTRICTS ORDER BY DistrictName"
            Return DB.GetDataTable(query)
        End Function

        Public Function GetDistrictCountyAssignments() As DataTable
            Dim query As String = "SELECT STRDISTRICTCOUNTY AS CountyCode, 
                STRDISTRICTCODE AS DistrictCode
                FROM LOOKUPDISTRICTINFORMATION"
            Return DB.GetDataTable(query)
        End Function

    End Module
End Namespace
