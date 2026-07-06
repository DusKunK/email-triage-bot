using EmailTriageBot.Interfaces;
using EmailTriageBot.Models;
using System;
using System.Linq;

namespace EmailTriageBot.Services
{
    public class SentimentAnalyzerProcessor : IDataProcessor
    {
        public void Analyze(EmailDataModel email)
        {
            // --- LOGIC MODULE: Keyword-based Scoring (Simple MVP) ---
            double score = 0.5;

            if (email.BodyContentPreview.Contains("urgent") || email.Subject.Contains("OVERDUE"))
            {
                score += 0.3; // Boost for high-priority keywords
            }
            else if (email.BodyContentPreview.Contains("update") && !email.Subject.Contains("Budget"))
            {
                // Lower score boost for simple updates
                score = Math.Min(1.0, score + 0.1); 
            }

            // Cap the score at 1.0 (Max Urgency)
            email.UrgencyScore = Math.Min(1.0, score);

            // Rule: If urgency is high AND it's not just a status update, flag action required
            email.RequiresAction = email.UrgencyScore > 0.6 && !email.Subject.Contains("status");
        }
    }
}
