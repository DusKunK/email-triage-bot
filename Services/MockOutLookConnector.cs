using EmailTriageBot.Interfaces;
using EmailTriageBot.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTriageBot.Services
{
    public class MockOutlookConnector : IEmailConnector
    {
        public async Task<IEnumerable<EmailDataModel>> FetchNewEmailsAsync()
        {
            Console.WriteLine("\n[CONNECTOR]: Attempting connection to Outlook/Exchange...");
            await Task.Delay(500); // Simulate network latency

            // --- IN REALITY: Implement MAPI/Graph SDK calls here ---
            Console.WriteLine("[CONNECTOR]: Connection successful. Fetching latest items...");

            return new List<EmailDataModel>
            {
                new EmailDataModel { From = "Boss Boss", Subject = "Q3 Budget Review Attached", BodyContentPreview = "Please find attached the finalized spreadsheets; action is required by end of day.", RequiresAction = true },
                new EmailDataModel { From = "Team Member X", Subject = "Quick status update on Task B", BodyContentPreview = "Everything is on track to meet the deadline, just checking in.", RequiresAction = false }
            };
        }
    }
}
