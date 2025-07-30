using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeminiNLSearchPOC.Migrations
{
    /// <inheritdoc />
    public partial class insertnewrecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    EscrowOfficer = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "CreatedDate", "EscrowOfficer", "FileName", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc", "deed_001.pdf", "Property Deed" },
                    { 2, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "xyz", "mortgage_002.pdf", "Mortgage Agreement" },
                    { 3, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc", "insurance_003.pdf", "Title Insurance" },
                    { 4, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "xyz", "closing_004.pdf", "Closing Statement" },
                    { 5, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "pqr", "loan_est_005.pdf", "Loan Estimate" },
                    { 6, new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc", "promo_note_006.pdf", "Promissory Note" },
                    { 7, new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "xyz", "inspection_007.pdf", "Inspection Report" },
                    { 8, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "pqr", "appraisal_008.pdf", "Appraisal Report" },
                    { 9, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc", "hud_settlement_009.pdf", "HUD-1 Settlement" },
                    { 10, new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "xyz", "receipt_010.pdf", "Earnest Money Receipt" },
                    { 11, new DateTime(2025, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc", "addendum_A_011.pdf", "Addendum A" },
                    { 12, new DateTime(2025, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "pqr", "disclosures_012.pdf", "Final Disclosures" },
                    { 13, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "lmn", "purchase_agree_013.pdf", "Purchase Agreement" },
                    { 14, new DateTime(2025, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc", "revised_loan_014.pdf", "Revised Loan Statement" },
                    { 15, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "xyz", "ho_policy_015.pdf", "Homeowner's Insurance Policy" },
                    { 16, new DateTime(2023, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "pqr", "tax_2024_016.pdf", "Tax Statement 2024" },
                    { 17, new DateTime(2024, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "lmn", "water_rights_017.pdf", "Water Rights Agreement" },
                    { 18, new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc", "hoa_bylaws_018.pdf", "HOA Bylaws" },
                    { 19, new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "pqr", "survey_report_019.pdf", "Survey Report" },
                    { 20, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "xyz", "radon_report_020.pdf", "Radon Inspection Report" },
                    { 21, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lmn", "lease_agree_021.pdf", "Lease Agreement" },
                    { 22, new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc", "walkthrough_022.pdf", "Final Walkthrough Statement" },
                    { 23, new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "pqr", "repair_agree_023.pdf", "Repair Agreement" },
                    { 24, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "xyz", "contingency_024.pdf", "Contingency Removal Form" },
                    { 25, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "lmn", "poa_doc_025.pdf", "Power of Attorney Document" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
