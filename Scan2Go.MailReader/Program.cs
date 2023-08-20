using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main(string[] args)
    {
        string email = "d2demo@outlook.com";
        string password = "d2eco!!!";
        string imapServer = "outlook.office365.com"; // IMAP server address
        int port = 993; // IMAP port
        bool useSsl = true;

        using (var client = new ImapClient())
        {
            client.Connect(imapServer, port, useSsl);
            client.Authenticate(email, password);

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            // Retrieve unread messages from the inbox
            var messages = inbox.Search(SearchQuery.NotSeen);

            foreach (var uid in messages)
            {
                var message = inbox.GetMessage(uid);

                Console.WriteLine($"Subject: {message.Subject}");
                Console.WriteLine($"From: {message.From}");
                Console.WriteLine($"Date: {message.Date}");

                foreach (var attachment in message.Attachments)
                {
                    if (attachment is MimePart mimePart && mimePart.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            mimePart.Content.DecodeTo(memoryStream);

                            byte[] pdfBytes = memoryStream.ToArray();
                            string base64Pdf = Convert.ToBase64String(pdfBytes);

                            // Do something with the base64 PDF data
                            Console.WriteLine("Attachment is a PDF:");
                            Console.WriteLine(base64Pdf);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Attachment is not a PDF.");
                    }
                }
            }

            client.Disconnect(true);
        }
    }
}
