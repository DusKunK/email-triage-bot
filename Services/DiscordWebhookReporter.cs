using EmailTriageBot.Interfaces;
using EmailTriageBot.Models;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EmailTriageBot.Services
{
    public class DiscordWebhookReporter : IRemoteReporter
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _webhookUrl;

        // The constructor enforces that the URL must be provided immediately
        public DiscordWebhookReporter(string webhookUrl)
        {
            if (string.IsNullOrWhiteSpace(webhookUrl))
            {
                throw new ArgumentException("Discord Webhook URL cannot be empty.");
            }
            _webhookUrl = webhookUrl;
        }

        public async Task ReportAsync(EmailDataModel data, string context = null)
        {
            // Discord requires specific formatting for embeds. We build it to match the desired output look.
            var payloadObject = new 
            {
                content = $"📥 **Triage Alert:** {data.Subject}",
                embeds = new[]
                {
                    new {
                        title = "📧 Email Snapshot",
                        description = $"**Urgency Score:** {(data.UrgencyScore * 100):F0}% | **Action Needed:** {(data.RequiresAction ? "🚨 ACTION REQUIRED" : "✅ INFO/FYI")}",
                        color = data.RequiresAction ? 16711935 : 3447003, // Red vs Green hex codes for visual impact
                        fields = new[]
                        {
                            new { name = "Sender", value = data.From, inline = true },
                            new { name = "Received Date", value = data.ReceivedDate.ToString("MM/dd HH:mm"), inline = true },
                            new { name = "Context Provided", value = context ?? "N/A", inline = false }
                        }
                    }
                }
            };

            string jsonPayload = JsonConvert.SerializeObject(payloadObject);
            var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

            try
            {
                Console.WriteLine($"[REPORTER]: Sending payload to Discord webhook...");
                HttpResponseMessage response = await _httpClient.PostAsync(_webhookUrl, content);
                response.EnsureSuccessStatusCode();
                // NOTE: In a real app, check the body for confirmation details.
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[ERROR]: Failed to reach Discord webhook: {ex.Message}");
            }
        }
    }
}
