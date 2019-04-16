
' This file is used by Code Analysis to maintain SuppressMessage 
' attributes that are applied to this project.
' Project-level suppressions either have no target or are given 
' a specific target and scoped to a namespace, type, member, etc.

<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification:="IAIP was designed around this pattern.")>
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Review unused parameters", Justification:="Parameter used in Release, but not Debug build", Scope:="member", Target:="~M:Iaip.ErrorReporting.ErrorReport(System.Exception,System.String,System.String,System.Boolean)")>
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Review unused parameters", Justification:="Parameter used in Release, but not Debug build", Scope:="member", Target:="~M:Iaip.IaipExceptionManager.HandleException(System.Exception,System.String)")>