'This class was written by Michael Floyd for EPD on the 7th of October 2009
'As you can see below by written I mean I copied that dudes code
'I liked his comments and keep them for future reference
' This class was written by Andrew Bowden for CIM on the 24th of May 2006
' This class is pretty specific, it is designed to send an email!  It is also pretty stolen
' (when I say stolen, I mean borrowed) from http://www.ipdg3.com/sourcecode.php
' The title of this article was "Send GroupWise Email in .NET".  I'm sorry that I haven't put a
' real link in here but I lost it - just found it again ;0
' http://www.ipdg3.com/sourcecoderesults.php?option=search_sourcecode&sc=NET_&ss=groupwise&match=cp&offset=0
' Anyway, I've embedded this into a class so that if our company ever gets off it's lazy
' ass and joins the rest of the IT community in the 21st century and install a SMTP server, I'll only
' need to modify this class to make everything work - God Bless OOP!
' Last Modified: Andrew Bowden
' Last Modified on: 24th May 2006
' Last Modification: Added all of these lovely comments, so I guess you must be happy!
'use this on the page event to send email.
'Dim etest As email
'etest = New email
'etest.sendEmail(EmailAddress, Subject, Body)
'btnEmailAcknowledgmentLetter.Enabled = False
'MessageBox.Show("Email has been sent.")
Imports System

Public Class email
    Private objMessages As Object
    Private objMessage As Object
    Private objMailBox As Object
    Private objRecipients As Object
    Private objRecipient As Object
    Private objMessageSent As Object

    Private objGroupWise As Object
    Private objAccount As Object

    'constructor
    Public Sub New()
        ' Set some of the variables
        objGroupWise = CreateObject("NovellGroupWareSession")
        objAccount = objGroupWise.Login
    End Sub
    ' This is a wrapper, so that if we ever change to a 'real' mail server
    ' (for example, SMTP) all that needs to be done is to write a SMTP method
    ' and redirect this part!
    Public Function sendEmail(ByVal emailTo As String, ByVal subject As String, ByVal message As String) As Boolean
        If emailTo.Length < 1 Or emailTo.IndexOf("@") < 0 Then
            Return False
        ElseIf subject.Length < 1 And message.Length < 1 Then
            Return False
        End If
        Return sendGroupWiseEmail(emailTo, subject, message)
    End Function
    ' Send an email via groupwise.  Hopefully, it will return true for a successful send and False for a
    ' unsuccessful send.
    Private Function sendGroupWiseEmail(ByVal emailTo, ByVal subject, ByVal message) As Boolean
        Try
            objMailBox = objAccount.mailbox
            objMessages = objMailBox.Messages
            objMessage = objMessages.Add("GW.MESSAGE.MAIL", "Draft")
            objRecipients = objMessage.Recipients
            objRecipient = objRecipients.Add(emailTo)
            objMessage.Subject = subject
            objMessage.BodyText = message
            objMessageSent = objMessage.Send

            Return True
        Catch ex As Exception
            ' MessageBox.Show(ex.ToString)
            Return False
        Finally
            Beep()
        End Try

    End Function
End Class
