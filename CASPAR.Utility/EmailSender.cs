using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;

namespace CASPAR.Utility
{
	public class EmailSender : IEmailSender
	{
		public string SendGridSecret { get; set; }

		public EmailSender(IConfiguration _config)
		{
			SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
		}

		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var emailToSend = new MimeMessage();
			emailToSend.From.Add(MailboxAddress.Parse("jrutherford@mail.weber.edu"));
			emailToSend.To.Add(MailboxAddress.Parse(email));
			emailToSend.Subject = subject;
			emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

			////send email with GMAIL ACCOUNT
			//using (var emailClient = new SmtpClient())
			//{
			//    emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
			//    emailClient.Authenticate("richfry@gmail.com", "Test123$");
			//    emailClient.Send(emailToSend);
			//    emailClient.Disconnect(true);
			//}

			//return Task.CompletedTask;

			var client = new SendGridClient(SendGridSecret);
			//THIS MUST MATCH A VERIFIED SENDGRID E-MAIL ADDRESS!!
			var from = new EmailAddress("jrutherford@mail.weber.edu", "CASPAR - Bit Bugs");
			var to = new EmailAddress(email);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
			return client.SendEmailAsync(msg);

		}
	}
}

