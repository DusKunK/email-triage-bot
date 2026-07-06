# 📧 Email Triage Bot: Intelligent Workflow Automation

**Project Description:**
This application automates the process of monitoring an email client, extracting key metadata, analyzing the content's intent (urgency/sentiment), and reporting actionable summaries to a centralized communication channel (Discord). It moves beyond simple scraping by implementing a full **Pipeline Architecture**.

**Core Goal:**
To transform raw, unstructured email data into structured, high-priority alerts automatically.

## ✨ Key Features & Design Principles
1.  **Modular Pipeline:** Utilizes the Strategy Pattern via C# interfaces (`IEmailConnector`, `IDataProcessor`, `IRemoteReporter`) allowing any part of the system to be swapped out without rewriting the core logic.
2.  **Intelligence Layer:** Instead of just copying raw content, it *scores* the importance and required action level based on heuristic rules or machine learning (simulated).
3.  **Robust Reporting:** Uses Discord's advanced embed structure to present rich, easily digestible information at a glance.

## 🛠️ Setup & Dependencies
1.  **.NET Target:** .NET 6.0+ recommended.
2.  **Dependencies:** This project requires `Newtonsoft.Json`. (Install via NuGet).
3.  **API Connection:** The current implementation uses `MockOutlookConnector`. To connect to a real service:
    *   **For Microsoft Outlook:** You must install the necessary COM Interop libraries for C#.
    *   **For Gmail/O365:** Update `MockOutlookConnector` to use the appropriate SDK (e.g., Google.Apis.Gmail).

## 🚀 How to Run It
1.  **Update Configuration:** In `Program.cs`, locate the `DiscordWebhookReporter` constructor and replace `"YOUR_ACTUAL_DISCORD_WEBHOOK_URL"` with your real URL.
2.  **Run:** Execute the project (`dotnet run`).

## ⚙️ Extending the Bot (For Developers)
*   **Add New Sources:** Implement a new class that inherits from `IEmailConnector` (e.g., `SlackConnector`) and pass it to the Engine constructor.
*   **Improve Analysis:** Replace `SentimentAnalyzerProcessor` with one wrapping an external ML service endpoint for real-time sentiment analysis.
