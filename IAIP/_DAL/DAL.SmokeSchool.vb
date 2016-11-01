Imports Oracle.ManagedDataAccess.Client

Namespace DAL
    Module SmokeSchoolData

        Public Function GetSmokeSchoolRosterAsDataTable(ByVal id As String) As DataTable
            ' "STRTITLE" VARCHAR2(50 BYTE), 
            ' "STRCOMPANYNAME" VARCHAR2(50 BYTE), 
            ' "STRADDRESS1" VARCHAR2(50 BYTE), 
            ' "STRADDRESS2" VARCHAR2(50 BYTE), 
            ' "STRCITY" VARCHAR2(50 BYTE), 
            ' "STRSTATE" VARCHAR2(2 BYTE), 
            ' "STRZIP" VARCHAR2(9 BYTE), 
            ' "STRPHONENUMBER" VARCHAR2(25 BYTE), 
            ' "STRFAX" VARCHAR2(25 BYTE), 
            ' "STREMAIL" VARCHAR2(50 BYTE), 
            ' "STRCONFIRMATIONNBR" VARCHAR2(25 BYTE), 
            ' "STRLOCATIONDATE" VARCHAR2(70 BYTE), 
            ' "STRIPADDRESS" VARCHAR2(15 BYTE), 
            ' "STRLECTUREYESNO" VARCHAR2(3 BYTE), 
            ' "DATTRANSACTIONDATE" DATE, 
            ' "STRFIRSTNAME" VARCHAR2(30 BYTE), 
            ' "STRLASTNAME" VARCHAR2(30 BYTE), 
            ' "NUMUSERID" NUMBER(10,0), 
            ' "STRSALUTATION" VARCHAR2(5 BYTE), 
            ' "STRYEARUSERTERMLOC" VARCHAR2(40 BYTE)

            Dim query As String = " SELECT STRTITLE AS Title, STRCOMPANYNAME AS CompanyName, " &
                " STRADDRESS1 AS Address1, STRADDRESS2 AS Address2, STRCITY AS City, " &
                " STRSTATE AS State, STRZIP AS Zip, STRPHONENUMBER AS Phone, STRFAX AS Fax, " &
                " STREMAIL AS Email, STRCONFIRMATIONNBR AS ConfirmationNumber, " &
                " STRLOCATIONDATE AS LocationDate, STRLECTUREYESNO AS LectureYesNo, " &
                " STRFIRSTNAME AS FirstName, STRLASTNAME AS LastName, " &
                " TO_CHAR(NUMUSERID) AS StudentID " &
                " FROM AIRBRANCH.SMOKESCHOOLRESERVATION " &
                " WHERE STRLOCATIONDATE = :pId " &
                " ORDER BY LastName, FirstName, StudentID "
            Dim parameter As New OracleParameter("pId", id)
            Dim table As DataTable = DB.GetDataTable(query, parameter)

            For Each row As DataRow In table.Rows
                row("Phone") = FormatDigitsAsPhoneNumber(DB.GetNullable(Of String)(row("Phone")))
                row("Fax") = FormatDigitsAsPhoneNumber(DB.GetNullable(Of String)(row("Fax")))
            Next

            Return table
        End Function

    End Module
End Namespace