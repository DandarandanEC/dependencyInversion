using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace interfaceSegregation
{

    class Program
    {

        static void Main(string[] args)
        {
            var msOutlookProcessor = new MSOutlookProcessor("Dan");
            var googleMailProcessor = new GoogleMailProcessor("Edward");
            var salesAndMarketingInstance = new SalesAndMarketing(msOutlookProcessor);
            salesAndMarketingInstance.blastEmail(
                new List<string> { "Bok", "Justine", "Francis" },
                "Good Day!"
                );
        }
    }

    public interface IEmailProcessor
    {
        void sendEmailToMultipleRecipient(List<string> recipients, string message);
    }

    class SalesAndMarketing
    {
        IEmailProcessor emailProcessor;
        public SalesAndMarketing(IEmailProcessor emailProcessor)
        {
            this.emailProcessor = emailProcessor;
        }

        public void blastEmail(List<string> recipient, string message)
        {
            this.emailProcessor.sendEmailToMultipleRecipient(recipient, message);
        }
    }

    class MicrosoftOutlookAPI
    {
        public string user { get; set; }
        public MicrosoftOutlookAPI(string user)
        {
            this.user = user;
        }

        public void sendEmailToMultipleRecipient(List<string> recipients, string message)
        {
            foreach (string recipient in recipients)
            {
                Console.WriteLine(new
                {
                    recipient = recipient,
                    sender = this.user,
                    message = message
                });
            }
        }
    }

    class MSOutlookProcessor : IEmailProcessor
    {
        string user;
        public MicrosoftOutlookAPI outlookAPI;

        public MSOutlookProcessor(string user)
        {
            this.user = user;
            this.outlookAPI = new MicrosoftOutlookAPI(user);
        }

        public void sendEmailToMultipleRecipient(List<string> recipients, string message)
        {
            this.outlookAPI.sendEmailToMultipleRecipient(recipients, message);
        }
    }

    class GoogleMailAPI
    {
        public void sendEmailToMultipleRecipient(string user, List<string> recipients, string message)
        {
            foreach (string recipient in recipients)
            {
                Console.WriteLine(new
                {
                    recipient = recipient,
                    sender = user,
                    message = message
                });
            }
        }
    }

    class GoogleMailProcessor : IEmailProcessor
    {
        string user;
        public GoogleMailAPI googleMailAPI;

        public GoogleMailProcessor(string user)
        {
            this.user = user;
            this.googleMailAPI = new GoogleMailAPI();
        }

        public void sendEmailToMultipleRecipient(List<string> recipients, string message)
        {
            this.googleMailAPI.sendEmailToMultipleRecipient(this.user, recipients, message);
        }
    }
}
