# 🤖 Natural Language Database Search with Google Gemini AI

A Proof of Concept (POC) demonstrating how to use Google Gemini AI to convert natural language into SQL queries, allowing users to search a database using conversational language.

-----

## 📜 Overview

This project showcases the power of combining a Large Language Model (LLM) with a traditional backend application. It allows users to search database records using simple, conversational English instead of writing complex SQL syntax.

-----

## ⚙️ How It Works

### 1\. User Interaction Flow

The process is straightforward, moving from user input to a filtered UI display.

`User Input` → `AI Processing` → `SQL Generation` → `Database Query` → `Filtered Results` → `UI Display`

  * **User Input**: A user types a natural language query like "Show me all records from November 2025".
  * **AI Processing**: The query is sent to the Google Gemini AI API.
  * **SQL Generation**: Gemini understands the intent and converts the text into a valid SQLite query.
  * **Database Query**: The backend executes the generated SQL against the local database.
  * **Filtered Results**: The query returns only the records that match the user's intent.
  * **UI Display**: The filtered results are displayed back to the user in a clean table format.

### 2\. Technical Architecture

```text
┌─────────────┐    ┌──────────────┐    ┌──────────────┐    ┌─────────────┐
│   User      │────│  Web UI      │────│  Backend     │────│  Database   │
│             │    │  (Razor      │    │  (ASP.NET    │    │  (SQLite)   │
│ Natural     │    │   Pages)     │    │   Core)      │    │             │
│ Language    │    │              │    │              │    │             │
└─────────────┘    └──────────────┘    └──────────────┘    └─────────────┘
                            │                    │
                            │            ┌──────────────┐
                            │            │  Gemini AI   │
                            │            │   Service    │
                            │            └──────────────┘
                            │                    │
                            └────────────────────┘
```

-----

## ✨ Features

  * **Natural Language Support**: Handles various query types including dates, names, and relative timeframes (e.g., "last 30 days").
  * **Security Features**: Includes basic validation to ensure only `SELECT` statements are executed, preventing malicious `UPDATE` or `DELETE` commands.
  * **Transparent UX**: The user interface is clean, responsive, and shows the generated SQL for transparency.
  * **Sample Data**: Comes pre-loaded with sample data for immediate testing.

-----

## 📋 Prerequisites

### Required Software

  * [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
  * [Git](https://git-scm.com/downloads)
  * A text editor or IDE like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/).

### Google AI Setup

1.  Navigate to [Google AI Studio](https://aistudio.google.com/).
2.  Sign in and generate a new API key.
3.  Copy the API key to use in the project configuration.

-----

## 🚀 Getting Started

### 1\. Clone the Repository

```bash
git clone <repository-url>
cd GeminiNLSearchPOC
```

### 2\. Configure Google AI API Key

Create a file named `appsettings.Development.json` in the project's root directory and add your API key.

```json
{
  "Gemini": {
    "ApiKey": "YOUR_GOOGLE_AI_STUDIO_API_KEY_HERE"
  }
}
```

### 3\. Restore Dependencies & Run

Open your terminal in the project directory and run the following commands:

```bash
# Restore .NET packages
dotnet restore

# Run the application
dotnet run
```

The application will be available at `https://localhost:5001`.

-----

## 🧪 Testing the Application

### Sample Queries to Try

  * **Date-based**:
      * `Show me all records from November 2025`
      * `Find documents created in November 2025`
  * **Officer-based**:
      * `Find all records created by escrow officer abc`
      * `Show me records by xyz`
  * **Time-based**:
      * `Show me records created last month`
      * `List files from the last 30 days`
  * **General**:
      * `Show all documents`
      * `Find agreements in the title`

### Expected Behavior

When a user enters a query like `"Show me all records from November 2025"`, the system will generate the corresponding SQL (`SELECT * FROM Documents WHERE strftime('%Y-%m', CreatedDate) = '2025-11';`), execute it, and display only the matching records.

-----

## 📁 Project Structure

```
GeminiNLSearchPOC/
├── Pages/
│   ├── Index.cshtml          # Main UI page
│   └── Index.cshtml.cs       # Page logic and AI integration
├── Services/
│   └── GeminiService.cs      # Service for SQL generation via Gemini
├── Data/
│   └── AppDbContext.cs       # Database context and seeding
├── Models/
│   └── Document.cs           # Data model
├── Properties/
│   └── launchSettings.json   # Port configuration
├── Program.cs                # Application startup & configuration
├── appsettings.json          # Base configuration
└── GeminiNLSearchPOC.csproj  # Project file
```

-----

## 🛡️ Security Considerations

This project is a Proof of Concept and includes basic security measures.

  * **Current Implementation**:
      * Removes markdown formatting from the AI response.
      * Validates that the generated command is a `SELECT` statement.
      * Uses EF Core's `FromSqlRaw` for safe execution against the database.
  * **Production Recommendations**:
      * Implement parameterized queries instead of raw SQL execution.
      * Add comprehensive SQL injection protection layers.
      * Incorporate rate limiting for API calls and user authentication.
      * Set up detailed logging and monitoring.

-----

## 🔧 Troubleshooting

  * **API Key Error**: Ensure `appsettings.Development.json` exists in the project root and contains your valid API key.
  * **Database Connection Error**: Check for write permissions in the project directory. If the `documents.db` file gets corrupted, you can safely delete it; the application will recreate it on the next run.
  * **SQL Syntax Errors**: Check the generated SQL displayed in the UI. Ensure the Gemini API is returning valid SQLite syntax.

-----

## 💡 Extending the POC

This project can be extended with more advanced features:

  * **Complex Queries**: Add support for `JOIN` operations and aggregation functions.
  * **UI Improvements**: Implement query history, saved searches, and data export functionality.
  * **AI Enhancements**: Refine the prompt engineering for better accuracy and suggest query optimizations to the user.

-----

## 🛠️ Technologies Used

  * **Backend**: ASP.NET Core 8
  * **Database**: SQLite with Entity Framework Core
  * **AI**: Google Gemini Pro
  * **Frontend**: Razor Pages, HTML, CSS

<img width="1357" height="707" alt="image" src="https://github.com/user-attachments/assets/c7dfd871-5b64-4457-9e65-daafd26a6959" />
<img width="1097" height="517" alt="image" src="https://github.com/user-attachments/assets/88077673-08d7-4384-958b-5378bb80e5e7" />

<img width="1045" height="633" alt="image" src="https://github.com/user-attachments/assets/d6fa4f88-ec30-4cbf-b3c0-83337654b69e" />
<img width="1376" height="666" alt="image" src="https://github.com/user-attachments/assets/7b61884e-ab3f-40f9-a88c-d19fc98efee9" />

<img width="1410" height="636" alt="image" src="https://github.com/user-attachments/assets/07d90024-5529-4d2b-a00f-29deb354c4eb" />

<img width="1413" height="602" alt="image" src="https://github.com/user-attachments/assets/0daa9f7c-4e47-4179-a775-0f94c0da16e4" />

<img width="1377" height="611" alt="image" src="https://github.com/user-attachments/assets/412b32d9-9928-4f03-834c-6bb92b36c55a" />





<img width="1108" height="586" alt="Screenshot 2025-07-25 013421" src="https://github.com/user-attachments/assets/e231bf41-5915-4379-95bb-fb95c1fe6332" />
<img width="1138" height="570" alt="Screenshot 2025-07-25 013405" src="https://github.com/user-attachments/assets/bc56acdf-817c-47f6-8d67-0d8c112ae569" />
<img width="818" height="480" alt="Screenshot 2025-07-25 013350" src="https://github.com/user-attachments/assets/69cc6577-aa8d-4a28-a45d-90ec7e965c05" />
<img width="895" height="657" alt="Screenshot 2025-07-25 012109" src="https://github.com/user-attachments/assets/1570c446-eba2-4059-8d28-5f4de5e27392" />
<img width="1121" height="582" alt="Screenshot 2025-07-25 013444" src="https://github.com/user-attachments/assets/368a0bf8-a71c-4da7-8de9-7e30719f4c2b" />

