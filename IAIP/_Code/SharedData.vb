Imports System.Collections.Generic
Imports Iaip.Apb.Facilities

Public Class SharedData
    Private Shared ReadOnly _initLock As Object = New Object()
    Private Shared _tDictionary As Dictionary(Of SharedTable, DataTable)
    Private Shared _dsDictionary As Dictionary(Of SharedDataSet, DataSet)
    Private Shared _dictDictionary As Dictionary(Of SharedLookupDictionary, Dictionary(Of Integer, String))
    Private Shared _objDictionary As Dictionary(Of SharedObject, Object)

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
        EpdManagers
        SscpNotificationTypes
        Counties
        DistrictOffices
        FacilityOwnershipTypes
        FeeYears
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
        Counties
    End Enum

    Public Enum SharedObject
        FeeRatesSchedule
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

                Case SharedTable.AllComplianceStaff
                    dt = DAL.GetComplianceStaff()

                Case SharedTable.Pollutants
                    dt = DAL.GetPollutantsTable()

                Case SharedTable.IaipAccountRoles
                    dt = DAL.GetIaipAccountRoles()

                Case SharedTable.AllFeeFacilities
                    dt = DAL.GetAllFeeFacilities()

                Case SharedTable.EpdManagers
                    dt = DAL.GetEpdManagersAsDataTable()

                Case SharedTable.SscpNotificationTypes
                    dt = DAL.Sscp.GetSscpNotificationTypes()

                Case SharedTable.Counties
                    dt = DAL.GetCountiesAsDataTable()

                Case SharedTable.DistrictOffices
                    dt = DAL.GetDistrictOffices()

                Case SharedTable.FacilityOwnershipTypes
                    dt = DAL.GetFacilityOwnershipTypes()

                Case SharedTable.FeeYears
                    dt = DAL.GetAllFeeYearsAsDataTable()

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
                    ds.Tables(0).Rows.Add({0, ""})

                    ds.Tables.Add(DAL.GetEpdProgramsAsDataTable)
                    ds.Tables(1).TableName = "Programs"
                    ds.Tables(1).Rows.Add({0, ""})

                    ds.Tables.Add(DAL.GetEpdUnitsAsDataTable)
                    ds.Tables(2).TableName = "Units"
                    ds.Tables(2).Rows.Add({0, ""})

                Case SharedDataSet.RuleSubparts

                    ds.Tables.Add(DAL.GetRuleSubpartsAsDataTable(RulePart.NSPS))
                    ds.Tables(0).TableName = RulePart.NSPS.ToString

                    ds.Tables.Add(DAL.GetRuleSubpartsAsDataTable(RulePart.NESHAP))
                    ds.Tables(1).TableName = RulePart.NESHAP.ToString

                    ds.Tables.Add(DAL.GetRuleSubpartsAsDataTable(RulePart.MACT))
                    ds.Tables(2).TableName = RulePart.MACT.ToString

                    ds.Tables.Add(DAL.GetRuleSubpartsAsDataTable(RulePart.SIP))
                    ds.Tables(3).TableName = RulePart.SIP.ToString

            End Select

            If _dsDictionary.ContainsKey(dataSet) Then
                _dsDictionary.Remove(dataSet)
            End If
            _dsDictionary.Add(dataSet, ds)

        End SyncLock
    End Sub

    Private Shared Sub InitializeData(lookupDictionary As SharedLookupDictionary)
        SyncLock _initLock

            Dim dict As New Dictionary(Of Integer, String)

            Select Case lookupDictionary

                Case SharedLookupDictionary.ActiveUsers
                    dict = DAL.GetActiveUsers()

                Case SharedLookupDictionary.Counties
                    dict = DAL.GetCountiesAsDictionary()

            End Select

            If _dictDictionary.ContainsKey(lookupDictionary) Then
                _dictDictionary.Remove(lookupDictionary)
            End If
            _dictDictionary.Add(lookupDictionary, dict)

        End SyncLock
    End Sub

    Private Shared Sub InitializeData(obj As SharedObject)
        SyncLock _initLock

            Dim myObj As New Object

            Select Case obj

                Case SharedObject.FeeRatesSchedule
                    myObj = DAL.Finance.GetFeeRatesScheduleFromDB()

            End Select

            If _objDictionary.ContainsKey(obj) Then
                _objDictionary.Remove(obj)
            End If
            _objDictionary.Add(obj, myObj)

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
    ''' </summary>
    ''' <param name="lookupDictionary">The shared List of KeyValuePairs to return.</param>
    ''' <returns>List of integer-indexed KeyValuePairs from the shared data service.</returns>
    Public Shared Function GetSharedData(lookupDictionary As SharedLookupDictionary) As Dictionary(Of Integer, String)
        If _dictDictionary Is Nothing Then
            _dictDictionary = New Dictionary(Of SharedLookupDictionary, Dictionary(Of Integer, String))
        End If

        If Not _dictDictionary.ContainsKey(lookupDictionary) OrElse _dictDictionary(lookupDictionary) Is Nothing Then
            InitializeData(lookupDictionary)
        End If

        Return _dictDictionary(lookupDictionary)
    End Function

    ''' <summary>
    ''' Returns data from the shared data service. If data has not been intialized, 
    ''' first retrieves the data from the database. Data is only retrieved the first
    ''' time it is used when the IAIP is run.
    ''' </summary>
    ''' <typeparam name="T">The Type of Object to return</typeparam>
    ''' <param name="obj">The shared Object to return.</param>
    ''' <returns>Object of Type T from the shared data service.</returns>
    Public Shared Function GetSharedObject(Of T)(obj As SharedObject) As T
        If _objDictionary Is Nothing Then
            _objDictionary = New Dictionary(Of SharedObject, Object)
        End If

        If Not _objDictionary.ContainsKey(obj) OrElse _objDictionary(obj) Is Nothing Then
            InitializeData(obj)
        End If

        Return CType(_objDictionary(obj), T)
    End Function


#End Region

#Region " Public functions for clearing shared data "

    Public Shared Sub ClearSharedData(table As SharedTable)
        If _tDictionary IsNot Nothing AndAlso _tDictionary.ContainsKey(table) Then
            _tDictionary.Remove(table)
        End If
    End Sub

    Public Shared Sub ClearSharedData(dataSet As SharedDataSet)
        If _dsDictionary IsNot Nothing AndAlso _dsDictionary.ContainsKey(dataSet) Then
            _dsDictionary.Remove(dataSet)
        End If
    End Sub

    Public Shared Sub ClearSharedData(lookupDictionary As SharedLookupDictionary)
        If _dictDictionary IsNot Nothing AndAlso _dictDictionary.ContainsKey(lookupDictionary) Then
            _dictDictionary.Remove(lookupDictionary)
        End If
    End Sub

    Public Shared Sub ClearSharedData(obj As SharedObject)
        If _objDictionary IsNot Nothing AndAlso _objDictionary.ContainsKey(obj) Then
            _objDictionary.Remove(obj)
        End If
    End Sub

#End Region

End Class