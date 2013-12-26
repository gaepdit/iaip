Imports Oracle.DataAccess.Client

Namespace DAL
    Module CrSmokeSchoolRoster

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

            Dim query As String = " SELECT STRTITLE AS Title, STRCOMPANYNAME AS CompanyName, " & _
                " STRADDRESS1 AS Address1, STRADDRESS2 AS Address2, STRCITY AS City, " & _
                " STRSTATE AS State, STRZIP AS Zip, STRPHONENUMBER AS Phone, STRFAX AS Fax, " & _
                " STREMAIL AS Email, STRCONFIRMATIONNBR AS ConfirmationNumber, " & _
                " STRLOCATIONDATE AS LocationDate, STRLECTUREYESNO AS LectureYesNo, " & _
                " STRFIRSTNAME AS FirstName, STRLASTNAME AS LastName, " & _
                " TO_CHAR(NUMUSERID) AS StudentID " & _
                " FROM " & DBNameSpace & ".SMOKESCHOOLRESERVATION " & _
                " WHERE STRLOCATIONDATE = :pId " & _
                " ORDER BY LastName, FirstName, StudentID "
            Dim parameter As New OracleParameter("pId", id)
            Dim table As DataTable = DB.GetDataTable(query, parameter)

            For Each row As DataRow In table.Rows
                row("Phone") = FormatStringAsPhoneNumber(DB.GetNullable(Of String)(row("Phone")))
                row("Fax") = FormatStringAsPhoneNumber(DB.GetNullable(Of String)(row("Fax")))
            Next

            Return table
        End Function

    End Module
End Namespace