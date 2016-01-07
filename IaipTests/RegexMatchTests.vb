Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting


<TestClass()> Public Class RegexMatchTests

    <TestMethod()> Public Sub TestEmailAddresses()
        Dim s As String

        ' Good addresses
        s = "user.name@dnr.ga.gov"
        StringAssert.Matches(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "User.Name@dnr.ga.gov"
        StringAssert.Matches(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "user.name@dnr.state.ga.us"
        StringAssert.Matches(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "user.name@gadnr.org"
        StringAssert.Matches(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "user.name@gaepd.com"
        StringAssert.Matches(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "user.name@gaepd.org"
        StringAssert.Matches(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "user.name@georgiaepd.com"
        StringAssert.Matches(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "user.name@georgiaepd.org"
        StringAssert.Matches(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))


        ' Bad addresses
        s = ""
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "@dnr.ga.gov"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "dnr.ga.gov"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "@dnr.state.ga.us"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "dnr.state.ga.us"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "username@dnr.ga.gov"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "username@dnr.state.ga.us"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "username@mail.dnr.state.ga.us"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "user.name@mail.dnr.state.ga.us"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "username@example.com"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "username@test.co.uk"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "user.name@example.com"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))
        s = "user.name@test.co.uk"
        StringAssert.DoesNotMatch(s, New RegularExpressions.Regex(Iaip.DnrEmailPattern))

    End Sub

End Class