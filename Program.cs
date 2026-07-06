using EmailTriageBot.Interfaces;
using EmailTriageBot.Services; // Make sure this namespace matches your directory structure
using System;
using System.Threading.Tasks;

namespace EmailTriageBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // ==================================================
            // 💡 CORE: Dependency Assembly (The Wiring)
            // ==================================================

            // --- 1. Configure Services ---
            // IMPORTANT: Replace this placeholder with your actual URL!
            const string webhookUrlPlaceholder = "YOUR_ACTUAL_DISCORD_WEBHOOK_URL"; 

            try
            {
                // Initialize all components with their required dependencies
                IEmailConnector connector = new MockOutlookConnector(); // <-- CHANGE THIS if you build Gmail/O365 connectors
                IDataProcessor processor = new SentimentAnalyzerProcessor();
                IRemoteReporter reporter = new DiscordWebhookReporter(webhookUrlPlaceholder);

                // --- 2. Initialize the Orchestrator ---
                var engine = new EmailAutomationEngine(connector, processor, reporter);

                // --- 3. Run the Program Cycle ---
                await engine.RunAutomationCycleAsync();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n===============================================");
                Console.WriteLine($"CONFIGURATION ERROR: {ex.Message}");
                Console.WriteLine("ACTION REQUIRED: Please update the webhook URL in Program.cs.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nFATAL ERROR: {ex.Message}");
                Console.ResetColor();
            }

            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
