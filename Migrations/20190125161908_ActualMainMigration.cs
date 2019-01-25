using Microsoft.EntityFrameworkCore.Migrations;

namespace BeltExam.Migrations
{
    public partial class ActualMainMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Users_CoordinatorId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Participation_Activity_ActivityId",
                table: "Participation");

            migrationBuilder.DropForeignKey(
                name: "FK_Participation_Users_UserId",
                table: "Participation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participation",
                table: "Participation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activity",
                table: "Activity");

            migrationBuilder.RenameTable(
                name: "Participation",
                newName: "Participations");

            migrationBuilder.RenameTable(
                name: "Activity",
                newName: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_Participation_UserId",
                table: "Participations",
                newName: "IX_Participations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Participation_ActivityId",
                table: "Participations",
                newName: "IX_Participations_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_CoordinatorId",
                table: "Activities",
                newName: "IX_Activities_CoordinatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participations",
                table: "Participations",
                column: "ParticipationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_CoordinatorId",
                table: "Activities",
                column: "CoordinatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Activities_ActivityId",
                table: "Participations",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Users_UserId",
                table: "Participations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_CoordinatorId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Activities_ActivityId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Users_UserId",
                table: "Participations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participations",
                table: "Participations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Participations",
                newName: "Participation");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "Activity");

            migrationBuilder.RenameIndex(
                name: "IX_Participations_UserId",
                table: "Participation",
                newName: "IX_Participation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Participations_ActivityId",
                table: "Participation",
                newName: "IX_Participation_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_CoordinatorId",
                table: "Activity",
                newName: "IX_Activity_CoordinatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participation",
                table: "Participation",
                column: "ParticipationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activity",
                table: "Activity",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Users_CoordinatorId",
                table: "Activity",
                column: "CoordinatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participation_Activity_ActivityId",
                table: "Participation",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participation_Users_UserId",
                table: "Participation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
