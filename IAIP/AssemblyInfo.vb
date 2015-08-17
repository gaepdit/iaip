Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

#If BETA Then

<Assembly: AssemblyTitle("IAIP Horizon")> 
<Assembly: AssemblyDescription("GA Air Protection Branch integrated database system; beta testing")> 
<Assembly: AssemblyCompany("State of Georgia")> 
<Assembly: AssemblyProduct("IAIP Horizon")> 
<Assembly: AssemblyCopyright("")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: CLSCompliant(True)> 
<Assembly: Guid("4d6ff346-89e3-4388-9b75-0077e47a7e46")> 

#Else

<Assembly: AssemblyTitle("Integrated Air Information Platform")> 
<Assembly: AssemblyDescription("GA Air Protection Branch integrated database system; beta testing")> 
<Assembly: AssemblyCompany("State of Georgia")> 
<Assembly: AssemblyProduct("Integrated Air Information Platform")> 
<Assembly: AssemblyCopyright("")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: CLSCompliant(True)> 
<Assembly: Guid("9F83B6AF-EE06-4324-83E9-7AB1D5E8BE5F")> 

#End If

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'2.
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:

<Assembly: AssemblyVersion("4.0.3.0")> 

<Assembly: AssemblyFileVersionAttribute("4.0.3.0")> 

<Assembly: ComVisibleAttribute(False)> 