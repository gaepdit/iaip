Imports System.Collections.Generic

Public Class SharedData
    Private Shared _initLock As Object = New Object()
    Private Shared _tDictionary As Dictionary(Of SharedTable, DataTable)
    Private Shared _dsDictionary As Dictionary(Of SharedDataSet, DataSet)
    Private Shared _kvlDictionary As Dictionary(Of SharedKeyValueList, List(Of KeyValuePair(Of Integer, String)))

#Region " Enums: Available shared data "

    ''' <summary>
    ''' Enum delineating all the available shared Tables in the SharedData service
    ''' </summary>
    Public Enum SharedTable
        Pollutants
        ViolationTypes
        AllComplianceStaff
        IaipAccountRoles
        AllFeeFacilities
    End Enum

    ''' <summary>
    ''' Enum delineating all the available shared DataSets in the SharedData service
    ''' </summary>
    Public Enum SharedDataSet
        EpdOrganization
    End Enum

    ''' <summary>
    ''' Enum delineating all the available shared Lists of KeyValuePairs in the SharedData service
    ''' </summary>
    Public Enum SharedKeyValueList
        ActiveUsers
    End Enum

#End Region

#Region " Initialize shared data "

    ' The InitializeData procedures define how the shared data is initially populated

    Private Shared Sub InitializeData(table As SharedTable)
        SyncLock _initLock

            Dim dt As New DataTable

            Select Case table

                Case SharedTable.ViolationTypes
                    dt = DAL.Sscp.GetViolationTypes()
                    dt.PrimaryKey = New DataColumn() {dt.Columns("AIRVIOLATIONTYPECODE")}

                Case SharedTable.AllComplianceStaff
                    dt = DAL.StaffData.GetComplianceStaff()

                Case SharedTable.Pollutants
                    dt = DAL.CommonData.GetPollutantsTable()
                    dt.PrimaryKey = New DataColumn() {dt.Columns("Pollutant Code")}

                Case SharedTable.IaipAccountRoles
                    dt = DAL.GetIaipAccountRoles
                    dt.PrimaryKey = New DataColumn() {dt.Columns("RoleCode")}

                Case SharedTable.AllFeeFacilities
                    dt = DAL.GetAllFeeFacilities
                    dt.PrimaryKey = New DataColumn() {dt.Columns("STRAIRSNUMBER")}

            End Select

            dt.TableName = table.ToString

            If _tDictionary.ContainsKey(table) Then
                _tDictionary.Remove(table)
            End If
            _tDictionary.Add(table, dt)

        End SyncLock
    End Sub

    Private Shared Sub InitializeData(dataSet As SharedDataSet)
        SyncLock _initLock

            Dim ds As New DataSet(dataSet.ToString)

            Select Case dataSet

                Case SharedDataSet.EpdOrganization

                    ds.Tables.Add(DAL.GetEpdBranchesAsDataTable)
                    ds.Tables(0).TableName = "Branches"
                    ds.Tables(0).Rows.Add({0})

                    ds.Tables.Add(DAL.GetEpdProgramsAsDataTable)
                    ds.Tables(1).TableName = "Programs"
                    ds.Tables(1).Rows.Add({0})

                    ds.Tables.Add(DAL.GetEpdUnitsAsDataTable)
                    ds.Tables(2).TableName = "Units"
                    ds.Tables(2).Rows.Add({0})

            End Select

            If _dsDictionary.ContainsKey(dataSet) Then
                _dsDictionary.Remove(dataSet)
            End If
            _dsDictionary.Add(dataSet, ds)

        End SyncLock
    End Sub

    Private Shared Sub InitializeData(keyValueList As SharedKeyValueList)
        SyncLock _initLock

            Dim kvl As New List(Of KeyValuePair(Of Integer, String))

            Select Case keyValueList

                Case SharedKeyValueList.ActiveUsers
                    kvl = DAL.GetActiveUsers()

            End Select

            If _kvlDictionary.ContainsKey(keyValueList) Then
                _kvlDictionary.Remove(keyValueList)
            End If
            _kvlDictionary.Add(keyValueList, kvl)

        End SyncLock
    End Sub

#End Region

#Region " Public functions for using shared data "

    ' The GetSharedData functions make the shared data available to other procedures

    ''' <summary>
    ''' Returns data from the shared data service. If data has not been intialized, 
    ''' first retrieves the data from the database. Data is only retrieved the first
    ''' time it is used when the IAIP is run.
    ''' </summary>
    ''' <param name="table">The shared Table to return.</param>
    ''' <returns>Table of data from the shared data service.</returns>
    Public Shared Function GetSharedData(table As SharedTable) As DataTable
        If _tDictionary Is Nothing Then
            _tDictionary = New Dictionary(Of SharedTable, DataTable)
        End If

        If Not _tDictionary.ContainsKey(table) OrElse _tDictionary(table) Is Nothing Then
            InitializeData(table)
        End If

        Return _tDictionary(table)
    End Function

    ''' <summary>
    ''' Returns data from the shared data service. If data has not been intialized, 
    ''' first retrieves the data from the database. Data is only retrieved the first
    ''' time it is used when the IAIP is run.
    ''' </summary>
    ''' <param name="dataSet">The shared DataSet to return.</param>
    ''' <returns>DataSet from the shared data service.</returns>
    Public Shared Function GetSharedData(dataSet As SharedDataSet) As DataSet
        If _dsDictionary Is Nothing Then
            _dsDictionary = New Dictionary(Of SharedDataSet, DataSet)
        End If

        If Not _dsDictionary.ContainsKey(dataSet) OrElse _dsDictionary(dataSet) Is Nothing Then
            InitializeData(dataSet)
        End If

        Return _dsDictionary(dataSet)
    End Function

    ''' <summary>
    ''' Returns data from the shared data service. If data has not been intialized, 
    ''' first retrieves the data from the database. Data is only retrieved the first
    ''' time it is used when the IAIP is run.
    ''' <param name="keyValueList">The shared List of KeyValuePairs to return.</param>
    ''' <returns>List of integer-indexed KeyValuePairs from the shared data service.</returns>
    Public Shared Function GetSharedData(keyValueList As SharedKeyValueList) As List(Of KeyValuePair(Of Integer, String))
        If _kvlDictionary Is Nothing Then
            _kvlDictionary = New Dictionary(Of SharedKeyValueList, List(Of KeyValuePair(Of Integer, String)))
        End If

        If Not _kvlDictionary.ContainsKey(keyValueList) OrElse _kvlDictionary(keyValueList) Is Nothing Then
            InitializeData(keyValueList)
        End If

        Return _kvlDictionary(keyValueList)
    End Function

#End Region

End Class