Imports System.Reflection
Imports System.Runtime.InteropServices

#If SqlServer Then

<Assembly: AssemblyTitle("IAIP for SQL Server")>
<Assembly: AssemblyDescription("GA Air Protection Branch integrated database system; SQL Server Edition")>
<Assembly: AssemblyProduct("IAIP for SQL Server")> 
<Assembly: Guid("3de1d3bd-80ec-462f-84ba-749620c13b3f")> 

#ElseIf UAT Then

<Assembly: AssemblyTitle("IAIP Horizon")>
<Assembly: AssemblyDescription("GA Air Protection Branch integrated database system; user acceptance testing version")>
<Assembly: AssemblyProduct("IAIP Horizon")> 
<Assembly: Guid("4d6ff346-89e3-4388-9b75-0077e47a7e46")> 

#Else

<Assembly: AssemblyTitle("Integrated Air Information Platform")>
<Assembly: AssemblyDescription("GA Air Protection Branch integrated database system")>
<Assembly: AssemblyProduct("Integrated Air Information Platform")>
<Assembly: Guid("9F83B6AF-EE06-4324-83E9-7AB1D5E8BE5F")>

#End If

#If SqlServer Then

<Assembly: AssemblyVersion("0.1.0.0")>
<Assembly: AssemblyFileVersion("0.1.0.0")>

#Else

<Assembly: AssemblyVersion("4.3.3.0")>
<Assembly: AssemblyFileVersion("4.3.3.0")>

#End If

<Assembly: AssemblyCompany("State of Georgia")>
<Assembly: AssemblyCopyright("")>
<Assembly: AssemblyTrademark("")>
<Assembly: CLSCompliant(True)>
<Assembly: ComVisible(False)>
