using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMaster.Migrations
{
    /// <inheritdoc />
    public partial class addedisdisabledtoallfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTask_Notes_NotesId",
                table: "TagTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagTask",
                table: "TagTask");

            migrationBuilder.DropIndex(
                name: "IX_TagTask_TagsId",
                table: "TagTask");

            migrationBuilder.RenameColumn(
                name: "NotesId",
                table: "TagTask",
                newName: "TasksId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Tags",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Notes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagTask",
                table: "TagTask",
                columns: new[] { "TagsId", "TasksId" });

            migrationBuilder.CreateIndex(
                name: "IX_TagTask_TasksId",
                table: "TagTask",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagTask_Notes_TasksId",
                table: "TagTask",
                column: "TasksId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTask_Notes_TasksId",
                table: "TagTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagTask",
                table: "TagTask");

            migrationBuilder.DropIndex(
                name: "IX_TagTask_TasksId",
                table: "TagTask");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "TasksId",
                table: "TagTask",
                newName: "NotesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagTask",
                table: "TagTask",
                columns: new[] { "NotesId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_TagTask_TagsId",
                table: "TagTask",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagTask_Notes_NotesId",
                table: "TagTask",
                column: "NotesId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
