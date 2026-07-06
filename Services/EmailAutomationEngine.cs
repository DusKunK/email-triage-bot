using EmailTriageBot.Interfaces;
using EmailTriageBot.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTriageBot.Services
{
    public class EmailAutomationEngine
    {
        private readonly IEmailConnector _connector;
        private readonly IDataProcessor _processor;
        private readonly IRemoteReporter _reporter;

        // Dependency Injection Constructor: This is the key pattern!
        public EmailAutomationEngine(IEmailConnector connector, IDataProcessor processor, IRemoteReporter reporter)
        {
            _connector = connector ?? throw new ArgumentNullException(nameof(connector));
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
            _reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        public async Task RunAutomationCycleAsync()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("🚀 Starting Email Triage Automation Cycle");
            Console.WriteLine("==========================================================");

            try
            {
                // STEP 1: FETCH DATA (Connector)
                var emails = await _connector.FetchNewEmailsAsync();

                if (emails == null || !emails.Any())
                {
                    Console.WriteLine("\n✅ No new emails found to process.");
                    return;
                }

                // We process records in a list that we can pass around for state tracking
                var processedRecords = new List<EmailDataModel>();

                // STEP 2 &amp; 3: PROCESS (Analyzer) AND REPORT (Reporter) concurrently/sequentially
                foreach (var email in emails)
                {
                    // Run Analysis first to boost the data object's intelligence
                    _processor.Analyze(email); 
                    processedRecords.Add(email);

                    // Report immediately on processed items
                    await _reporter.ReportAsync(email, context: "Directly analyzed snapshot.");
                }

                // OPTIONAL: Add logic here to save the entire 'processedRecords' list locally if needed for audit trail.

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ CRITICAL FAILURE during automation cycle: {ex.Message}");
            }
        }
    }
}
