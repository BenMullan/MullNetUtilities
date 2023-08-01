Public Class AutoEMail

    Public Class SenderEMailAccount
        Public EMailAddress, EMailPassword, SMTPServer As String
        Public SMTPServerPort%
        Public SMTPServerUsesSSL As Boolean
    End Class

#Region "Default Sender Accounts"

    Public Shared DefaultSenderAccount() As SenderEMailAccount

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SetDefaultSenderAccounts()

        'The First Default Sender Account
        DefaultSenderAccount(0) = New SenderEMailAccount With {
            .EMailAddress = "bob.the.builder.802.11@gmail.com",
            .EMailPassword = "take the time 80:21",
            .SMTPServer = "smtp.gmail.com",
            .SMTPServerPort = 587,
            .SMTPServerUsesSSL = True
        }

        'The Second Default Sender Account
        DefaultSenderAccount(1) = New SenderEMailAccount With {
            .EMailAddress = "",
            .EMailPassword = "",
            .SMTPServer = "",
            .SMTPServerPort = 0,
            .SMTPServerUsesSSL = False
        }

        With New System.IO.DriveInfo(InputBox("Drive Letter to Use for DriveInfo:", DefaultResponse:="C:"))
            MsgBox(.AvailableFreeSpace.ToString() & .DriveFormat & .DriveType.ToString() & .IsReady.ToString() & .Name & .RootDirectory.ToString())
        End With
    End Sub

#End Region

End Class