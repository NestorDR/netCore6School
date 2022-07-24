using Microsoft.AspNetCore.Http;
using MimeKit;

namespace EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; } = new();
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection? Attachments { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="to">Tuple&lt;string, string&gt; enumerable, where the 1st string is the recipient's friendly name and the 2nd is the recipient's email address.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="content">Email content.</param>
        /// <param name="attachments">Collection of email attachments.</param>
        public Message(IEnumerable<Tuple<string, string>> to, string subject, string content, IFormFileCollection? attachments)
        {
            To.AddRange(to.Select(recipient => new MailboxAddress(recipient.Item1, recipient.Item2)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
