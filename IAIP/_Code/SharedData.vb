Imports System.Collections.Generic
Imports Iaip.Apb.Facilities
Imports Iaip.DAL

Public Module SharedData
    Private ReadOnly _initLock As New Object()
    Private _tDictionary As Dictionary(Of SharedTable, DataTable)
    Private _dsDictionary As Dictionary(Of SharedDataSet, DataSet)
    Private _dictDictionary As Dictionary(Of SharedLookupDictionary, Dictionary(Of Integer, String))
    Private _objDictionary As Dictionary(Of SharedObject, Object)

    ' Enums: Available shared data

    ''' <summary>
    ''' Enum delineating all the available shared Tables in the SharedData service
    ''' </summary>
    Public Enum SharedTable
        Pollutants
        ViolationTypes
        AllComplianceStaff
        IaipAccountRoles
        EpdManagers
        SscpNotificationTypes
        Counties
        DistrictOffices
        FacilityOwnershipTypes
        FeeYears
        AllFacilityCities
        SsppEngineersList
        PermitTypes
        ApplicationTypes
        SsppUnits
    End Enum

    ''' <summary>
    ''' Enum delineating all the available shared DataSets in the SharedData service
    ''' </summary>
    Public Enum SharedDataSet
        EpdOrganization
        RuleSubparts
    End Enum

    ''' <summary>
    ''' Enum delineating all the available shared Lists of KeyValuePairs in the SharedData service
    ''' </summary>
    Public Enum SharedLookupDictionary
        ActiveUsers
    End Enum

    ''' <summary>
    ''' Enum delineating all the available shared Objects in the SharedData service
    ''' </summary>
    Public Enum SharedObject
        FeeRatesSchedule
    End Enum

    ' Initialize Shared data
    ' The InitializeData procedures define how the shared data is initially populated

    Private Sub InitializeData(table As SharedTable)
        SyncLock _initLock

            Dim dt As New DataTable

            Select Case table

                Case SharedTable.ViolationTypes
                    dt = Sscp.GetViolationTypes()

                Case SharedTable.AllComplianceStaff
                    dt = GetComplianceStaff()

                Case SharedTable.Pollutants
                    dt = GetPollutantsTable()

                Case SharedTable.IaipAccountRoles
                    dt = GetIaipAccountRoles()

                Case SharedTable.EpdManagers
                    dt = GetEpdManagersAsDataTable()

                Case SharedTable.SscpNotificationTypes
                    dt = Sscp.GetSscpNotificationTypes()

                Case SharedTable.Counties
                    dt = GetCountiesAsDataTable()

                Case SharedTable.DistrictOffices
                    dt = GetDistrictOffices()

                Case SharedTable.FacilityOwnershipTypes
                    dt = GetFacilityOwnershipTypes()

                Case SharedTable.FeeYears
                    dt = AnnualFees.GetAllFeeYearsAsDataTable()

                Case SharedTable.AllFacilityCities
                    dt = GetFacilityCitiesAsDataTable()

                Case SharedTable.SsppEngineersList
                    dt = Sspp.GetEngineersList()

                Case SharedTable.PermitTypes
                    dt = Sspp.GetPermitTypes()

                Case SharedTable.ApplicationTypes
                    dt = Sspp.GetApplicationTypes()

                Case SharedTable.SsppUnits
                    dt = Sspp.GetSsppUnits()

            End Select

            dt.TableName = table.ToString

            _tDictionary.Remove(table)
            _tDictionary.Add(table, dt)

        End SyncLock
    End Sub

    Private Sub InitializeData(dataSet As SharedDataSet)
        SyncLock _initLock

            Dim ds As New DataSet(dataSet.ToString)

            Select Case dataSet

                Case SharedDataSet.EpdOrganization

                    ds.Tables.Add(GetEpdBranchesAsDataTable)
                    ds.Tables(0).TableName = "Branches"
                    ds.Tables(0).Rows.Add(0, "")

                    ds.Tables.Add(GetEpdProgramsAsDataTable)
                    ds.Tables(1).TableName = "Programs"
                    ds.Tables(1).Rows.Add(0, "")

                    ds.Tables.Add(GetEpdUnitsAsDataTable)
                    ds.Tables(2).TableName = "Units"
                    ds.Tables(2).Rows.Add(0, "")

                Case SharedDataSet.RuleSubparts

                    ds.Tables.Add(GetRuleSubpartsAsDataTable(RulePart.NSPS))
                    ds.Tables(0).TableName = RulePart.NSPS.ToString

                    ds.Tables.Add(GetRuleSubpartsAsDataTable(RulePart.NESHAP))
                    ds.Tables(1).TableName = RulePart.NESHAP.ToString

                    ds.Tables.Add(GetRuleSubpartsAsDataTable(RulePart.MACT))
                    ds.Tables(2).TableName = RulePart.MACT.ToString

                    ds.Tables.Add(GetRuleSubpartsAsDataTable(RulePart.SIP))
                    ds.Tables(3).TableName = RulePart.SIP.ToString

            End Select

            _dsDictionary.Remove(dataSet)
            _dsDictionary.Add(dataSet, ds)

        End SyncLock
    End Sub

    Private Sub InitializeData(lookupDictionary As SharedLookupDictionary)
        SyncLock _initLock

            Dim dict As New Dictionary(Of Integer, String)

            Select Case lookupDictionary

                Case SharedLookupDictionary.ActiveUsers
                    dict = GetActiveUsers()

            End Select

            _dictDictionary.Remove(lookupDictionary)
            _dictDictionary.Add(lookupDictionary, dict)

        End SyncLock
    End Sub

    Private Sub InitializeData(obj As SharedObject)
        SyncLock _initLock

            Dim myObj As New Object

            Select Case obj

                Case SharedObject.FeeRatesSchedule
                    myObj = ApplicationFees.LoadFeeRatesSchedule()

            End Select

            _objDictionary.Remove(obj)
            _objDictionary.Add(obj, myObj)

        End SyncLock
    End Sub

    ' Public functions for using shared data
    ' The GetSharedData functions make the shared data available to other procedures

    ''' <summary>
    ''' Returns data from the shared data service. If data has not been initialized, 
    ''' first retrieves the data from the database. Data is only retrieved the first
    ''' time it is used when the IAIP is run.
    ''' </summary>
    ''' <param name="table">The shared Table to return.</param>
    ''' <returns>Table of data from the shared data service.</returns>
    Public Function GetSharedData(table As SharedTable) As DataTable
        If _tDictionary Is Nothing Then
            _tDictionary = New Dictionary(Of SharedTable, DataTable)
        End If

        Dim value As DataTable = Nothing
        If Not _tDictionary.TryGetValue(table, value) OrElse value Is Nothing Then
            InitializeData(table)
        End If

        Return _tDictionary(table)
    End Function

    ''' <summary>
    ''' Returns data from the shared data service. If data has not been initialized, 
    ''' first retrieves the data from the database. Data is only retrieved the first
    ''' time it is used when the IAIP is run.
    ''' </summary>
    ''' <param name="dataSet">The shared DataSet to return.</param>
    ''' <returns>DataSet from the shared data service.</returns>
    Public Function GetSharedData(dataSet As SharedDataSet) As DataSet
        If _dsDictionary Is Nothing Then
            _dsDictionary = New Dictionary(Of SharedDataSet, DataSet)
        End If

        Dim value As DataSet = Nothing
        If Not _dsDictionary.TryGetValue(dataSet, value) OrElse value Is Nothing Then
            InitializeData(dataSet)
        End If

        Return _dsDictionary(dataSet)
    End Function

    ''' <summary>
    ''' Returns data from the shared data service. If data has not been initialized, 
    ''' first retrieves the data from the database. Data is only retrieved the first
    ''' time it is used when the IAIP is run.
    ''' </summary>
    ''' <param name="lookupDictionary">The shared List of KeyValuePairs to return.</param>
    ''' <returns>List of integer-indexed KeyValuePairs from the shared data service.</returns>
    Public Function GetSharedData(lookupDictionary As SharedLookupDictionary) As Dictionary(Of Integer, String)
        If _dictDictionary Is Nothing Then
            _dictDictionary = New Dictionary(Of SharedLookupDictionary, Dictionary(Of Integer, String))
        End If

        If Not _dictDictionary.ContainsKey(lookupDictionary) OrElse _dictDictionary(lookupDictionary) Is Nothing Then
            InitializeData(lookupDictionary)
        End If

        Return _dictDictionary(lookupDictionary)
    End Function

    ''' <summary>
    ''' Returns data from the shared data service. If data has not been initialized, 
    ''' first retrieves the data from the database. Data is only retrieved the first
    ''' time it is used when the IAIP is run.
    ''' </summary>
    ''' <typeparam name="T">The Type of value to return</typeparam>
    ''' <param name="value">The shared value to return.</param>
    ''' <returns>Object of Type T from the shared data service.</returns>
    Public Function GetSharedObject(Of T)(value As SharedObject) As T
        If _objDictionary Is Nothing Then
            _objDictionary = New Dictionary(Of SharedObject, Object)
        End If

        If Not _objDictionary.ContainsKey(value) OrElse _objDictionary(value) Is Nothing Then
            InitializeData(value)
        End If

        Return _objDictionary(value)
    End Function


    ' Public functions for clearing shared data
    ' Removes data from the cache

    Public Sub ClearSharedData(table As SharedTable)
        _tDictionary?.Remove(table)
    End Sub

    Public Sub ClearSharedData(dataSet As SharedDataSet)
        _dsDictionary?.Remove(dataSet)
    End Sub

    Public Sub ClearSharedData(lookupDictionary As SharedLookupDictionary)
        _dictDictionary?.Remove(lookupDictionary)
    End Sub

    Public Sub ClearSharedObject(value As SharedObject)
        _objDictionary?.Remove(value)
    End Sub

    ' Public functions for reloading shared data
    ' Clears then returns fresh copy of data

    Public Function ReloadSharedData(table As SharedTable) As DataTable
        ClearSharedData(table)
        Return GetSharedData(table)
    End Function

    Public Function ReloadSharedData(dataSet As SharedDataSet) As DataSet
        ClearSharedData(dataSet)
        Return GetSharedData(dataSet)
    End Function

    Public Function ReloadSharedData(lookupDictionary As SharedLookupDictionary) As Dictionary(Of Integer, String)
        ClearSharedData(lookupDictionary)
        Return GetSharedData(lookupDictionary)
    End Function

    Public Function ReloadSharedObject(Of T)(value As SharedObject) As T
        ClearSharedObject(value)
        Return GetSharedObject(Of T)(value)
    End Function

End Module