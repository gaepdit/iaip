Imports System.Resources
Imports System.Reflection
Imports System.Runtime.InteropServices

<Assembly: AssemblyVersion("7.0.0.0")>
<Assembly: AssemblyCompany("State of Georgia")>
<Assembly: AssemblyCopyright("Copyright © 2025")>
<Assembly: CLSCompliant(True)>
<Assembly: ComVisible(False)>
<Assembly: NeutralResourcesLanguage("en-US")>

#If UAT Then

<Assembly: AssemblyTitle("IAIP Horizon")>
<Assembly: AssemblyDescription("GA Air Protection Branch integrated information system; user acceptance testing version")>
<Assembly: AssemblyProduct("IAIP Horizon")> 
<Assembly: Guid("4d6ff346-89e3-4388-9b75-0077e47a7e46")>

#Else

<Assembly: AssemblyTitle("Integrated Air Information Platform")>
<Assembly: AssemblyDescription("GA Air Protection Branch integrated information system")>
<Assembly: AssemblyProduct("Integrated Air Information Platform")>
<Assembly: Guid("9F83B6AF-EE06-4324-83E9-7AB1D5E8BE5F")>

#End If
