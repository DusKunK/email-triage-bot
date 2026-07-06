using System;

namespace EmailTriageBot.Models
{
    public class EmailDataModel
    {
        public DateTime ReceivedDate { get; set; } = DateTime.Now;
        public string From { get; set; } = "Unknown";
        public string Subject { get; set; } = "No Subject";
        public string BodyContentPreview { get; set; } = "";
        public double UrgencyScore { get; set; } = 0.5; // Range 0.0 (Low) to 1.0 (Critical)
        public bool RequiresAction { get; set; } = false;
        public string RawBodyText { get; set; } = "";
    }

    // Payload specifically for Discord embedding structure
    public class ProcessedReportPayload
    {
        public string Content { get; set; }
        public object Embeds { get; set; } 
    }
}
