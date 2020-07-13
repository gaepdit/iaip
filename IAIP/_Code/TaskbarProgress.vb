Imports System.Runtime.InteropServices

Public Module TaskbarProgress
    Public Enum TaskbarState
        NoProgress = 0
        Indeterminate = &H1
        Normal = &H2
        [Error] = &H4
        Paused = &H8
    End Enum

    <ComImport()>
    <Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")>
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Private Interface ITaskbarList3
        ' ITaskbarList
        <PreserveSig>
        Sub HrInit()
        <PreserveSig>
        Sub AddTab(ByVal hwnd As IntPtr)
        <PreserveSig>
        Sub DeleteTab(ByVal hwnd As IntPtr)
        <PreserveSig>
        Sub ActivateTab(ByVal hwnd As IntPtr)
        <PreserveSig>
        Sub SetActiveAlt(ByVal hwnd As IntPtr)

        ' ITaskbarList2
        <PreserveSig>
        Sub MarkFullscreenWindow(ByVal hwnd As IntPtr,
        <MarshalAs(UnmanagedType.Bool)> ByVal fFullscreen As Boolean)

        ' ITaskbarList3
        <PreserveSig>
        Sub SetProgressValue(ByVal hwnd As IntPtr, ByVal ullCompleted As ULong, ByVal ullTotal As ULong)
        <PreserveSig>
        Sub SetProgressState(ByVal hwnd As IntPtr, ByVal state As TaskbarState)
    End Interface

    <ComImport()>
    <Guid("56fdf344-fd6d-11d0-958a-006097c9a090")>
    <ClassInterface(ClassInterfaceType.None)>
    Private Class TaskbarInstance
    End Class

    Private ReadOnly taskbarInstanceField As ITaskbarList3 = CType(New TaskbarInstance(), ITaskbarList3)
    Private ReadOnly taskbarSupported As Boolean = Environment.OSVersion.Version >= New Version(6, 1)

    Public Sub SetTaskbarProgressState(ByVal windowHandle As IntPtr, ByVal taskbarState As TaskbarState)
        If taskbarSupported Then taskbarInstanceField.SetProgressState(windowHandle, taskbarState)
    End Sub

    Public Sub SetTaskbarProgressValue(ByVal windowHandle As IntPtr, ByVal progressValue As Double, ByVal progressMax As Double)
        If taskbarSupported Then taskbarInstanceField.SetProgressValue(windowHandle, progressValue, progressMax)
    End Sub
End Module
