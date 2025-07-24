Of course. Here is the provided content converted into a well-structured `README.md` file format.

-----

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