using Microsoft.EntityFrameworkCore.Migrations;

namespace Honfoglalo.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "DBSequenceHiLo",
                incrementBy: 10);

            migrationBuilder.CreateSequence<int>(
                name: "QuestionHiLo",
                startValue: 100L,
                incrementBy: 2);

            migrationBuilder.CreateTable(
                name: "DifficultyTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    FieldId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DifficultyTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    FieldLocation_Country = table.Column<string>(nullable: true),
                    FieldLocation_Region = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.UniqueConstraint("AK_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "HistoryTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwningTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    FieldName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwningTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Question1 = table.Column<string>(nullable: true),
                    Answer1 = table.Column<string>(nullable: true),
                    Answer2 = table.Column<string>(nullable: true),
                    Answer3 = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    Difficulty = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NumOfFields = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTable", x => x.Id);
                    table.UniqueConstraint("AK_User", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Fields",
                columns: new[] { "Id", "Name", "Rank", "FieldLocation_Country", "FieldLocation_Region" },
                values: new object[,]
                {
                    { 1, "f11", 5, "Hungary", "R10" },
                    { 20, "f120", 5, "Germany", "R119" },
                    { 19, "f119", 5, "Hungary", "R118" },
                    { 18, "f118", 1, "Germany", "R117" },
                    { 17, "f117", 3, "Germany", "R116" },
                    { 15, "f115", 1, "Germany", "R114" },
                    { 14, "f114", 5, "Germany", "R113" },
                    { 13, "f113", 1, "Hungary", "R112" },
                    { 12, "f112", 5, "Germany", "R111" },
                    { 11, "f111", 1, "Germany", "R110" },
                    { 16, "f116", 1, "Hungary", "R115" },
                    { 9, "f19", 5, "Germany", "R18" },
                    { 8, "f18", 4, "Germany", "R17" },
                    { 7, "f17", 5, "Hungary", "R16" },
                    { 6, "f16", 3, "Germany", "R15" },
                    { 5, "f15", 3, "Germany", "R14" },
                    { 4, "f14", 1, "Hungary", "R13" },
                    { 3, "f13", 1, "Germany", "R12" },
                    { 2, "f12", 4, "Germany", "R11" },
                    { 10, "f110", 2, "Hungary", "R19" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectAnswer", "Difficulty", "IsDeleted", "Question1", "Answer1", "Answer2", "Answer3" },
                values: new object[,]
                {
                    { 35, "Correct Answer35", 1, false, "Question 35", "Answer1_35", "Answer2_35", "Answer3_35" },
                    { 34, "Correct Answer34", 2, false, "Question 34", "Answer1_34", "Answer2_34", "Answer3_34" },
                    { 33, "Correct Answer33", 3, false, "Question 33", "Answer1_33", "Answer2_33", "Answer3_33" },
                    { 32, "Correct Answer32", 3, false, "Question 32", "Answer1_32", "Answer2_32", "Answer3_32" },
                    { 27, "Correct Answer27", 1, false, "Question 27", "Answer1_27", "Answer2_27", "Answer3_27" },
                    { 30, "Correct Answer30", 1, false, "Question 30", "Answer1_30", "Answer2_30", "Answer3_30" },
                    { 29, "Correct Answer29", 1, false, "Question 29", "Answer1_29", "Answer2_29", "Answer3_29" },
                    { 28, "Correct Answer28", 5, false, "Question 28", "Answer1_28", "Answer2_28", "Answer3_28" },
                    { 36, "Correct Answer36", 5, false, "Question 36", "Answer1_36", "Answer2_36", "Answer3_36" },
                    { 31, "Correct Answer31", 4, false, "Question 31", "Answer1_31", "Answer2_31", "Answer3_31" },
                    { 37, "Correct Answer37", 1, false, "Question 37", "Answer1_37", "Answer2_37", "Answer3_37" },
                    { 47, "Correct Answer47", 3, false, "Question 47", "Answer1_47", "Answer2_47", "Answer3_47" },
                    { 39, "Correct Answer39", 1, false, "Question 39", "Answer1_39", "Answer2_39", "Answer3_39" },
                    { 40, "Correct Answer40", 5, false, "Question 40", "Answer1_40", "Answer2_40", "Answer3_40" },
                    { 41, "Correct Answer41", 2, false, "Question 41", "Answer1_41", "Answer2_41", "Answer3_41" },
                    { 42, "Correct Answer42", 2, false, "Question 42", "Answer1_42", "Answer2_42", "Answer3_42" },
                    { 43, "Correct Answer43", 1, false, "Question 43", "Answer1_43", "Answer2_43", "Answer3_43" },
                    { 44, "Correct Answer44", 4, false, "Question 44", "Answer1_44", "Answer2_44", "Answer3_44" },
                    { 45, "Correct Answer45", 5, false, "Question 45", "Answer1_45", "Answer2_45", "Answer3_45" },
                    { 46, "Correct Answer46", 2, false, "Question 46", "Answer1_46", "Answer2_46", "Answer3_46" },
                    { 26, "Correct Answer26", 3, false, "Question 26", "Answer1_26", "Answer2_26", "Answer3_26" },
                    { 48, "Correct Answer48", 2, false, "Question 48", "Answer1_48", "Answer2_48", "Answer3_48" },
                    { 38, "Correct Answer38", 1, false, "Question 38", "Answer1_38", "Answer2_38", "Answer3_38" },
                    { 25, "Correct Answer25", 3, false, "Question 25", "Answer1_25", "Answer2_25", "Answer3_25" },
                    { 15, "Correct Answer15", 1, false, "Question 15", "Answer1_15", "Answer2_15", "Answer3_15" },
                    { 23, "Correct Answer23", 1, false, "Question 23", "Answer1_23", "Answer2_23", "Answer3_23" },
                    { 1, "Correct Answer1", 2, false, "Question 1", "Answer1_1", "Answer2_1", "Answer3_1" },
                    { 2, "Correct Answer2", 2, false, "Question 2", "Answer1_2", "Answer2_2", "Answer3_2" },
                    { 3, "Correct Answer3", 2, false, "Question 3", "Answer1_3", "Answer2_3", "Answer3_3" },
                    { 4, "Correct Answer4", 4, false, "Question 4", "Answer1_4", "Answer2_4", "Answer3_4" },
                    { 5, "Correct Answer5", 1, false, "Question 5", "Answer1_5", "Answer2_5", "Answer3_5" },
                    { 6, "Correct Answer6", 3, false, "Question 6", "Answer1_6", "Answer2_6", "Answer3_6" },
                    { 7, "Correct Answer7", 1, false, "Question 7", "Answer1_7", "Answer2_7", "Answer3_7" },
                    { 8, "Correct Answer8", 3, false, "Question 8", "Answer1_8", "Answer2_8", "Answer3_8" },
                    { 9, "Correct Answer9", 2, false, "Question 9", "Answer1_9", "Answer2_9", "Answer3_9" },
                    { 10, "Correct Answer10", 5, false, "Question 10", "Answer1_10", "Answer2_10", "Answer3_10" },
                    { 24, "Correct Answer24", 2, false, "Question 24", "Answer1_24", "Answer2_24", "Answer3_24" },
                    { 11, "Correct Answer11", 5, false, "Question 11", "Answer1_11", "Answer2_11", "Answer3_11" },
                    { 13, "Correct Answer13", 4, false, "Question 13", "Answer1_13", "Answer2_13", "Answer3_13" },
                    { 14, "Correct Answer14", 2, false, "Question 14", "Answer1_14", "Answer2_14", "Answer3_14" },
                    { 49, "Correct Answer49", 4, false, "Question 49", "Answer1_49", "Answer2_49", "Answer3_49" },
                    { 16, "Correct Answer16", 2, false, "Question 16", "Answer1_16", "Answer2_16", "Answer3_16" },
                    { 17, "Correct Answer17", 1, false, "Question 17", "Answer1_17", "Answer2_17", "Answer3_17" },
                    { 18, "Correct Answer18", 2, false, "Question 18", "Answer1_18", "Answer2_18", "Answer3_18" },
                    { 19, "Correct Answer19", 4, false, "Question 19", "Answer1_19", "Answer2_19", "Answer3_19" },
                    { 20, "Correct Answer20", 5, false, "Question 20", "Answer1_20", "Answer2_20", "Answer3_20" },
                    { 21, "Correct Answer21", 1, false, "Question 21", "Answer1_21", "Answer2_21", "Answer3_21" },
                    { 22, "Correct Answer22", 3, false, "Question 22", "Answer1_22", "Answer2_22", "Answer3_22" },
                    { 12, "Correct Answer12", 2, false, "Question 12", "Answer1_12", "Answer2_12", "Answer3_12" },
                    { 50, "Correct Answer50", 1, false, "Question 50", "Answer1_50", "Answer2_50", "Answer3_50" }
                });

            migrationBuilder.CreateIndex(
                name: "Index_Rank",
                table: "Fields",
                column: "Rank");

            migrationBuilder.CreateIndex(
                name: "Index_Difficulty",
                table: "Questions",
                column: "Difficulty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DifficultyTable");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "HistoryTable");

            migrationBuilder.DropTable(
                name: "OwningTable");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "UsersTable");

            migrationBuilder.DropSequence(
                name: "DBSequenceHiLo");

            migrationBuilder.DropSequence(
                name: "QuestionHiLo");
        }
    }
}
