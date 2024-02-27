using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMaster.Migrations
{
    /// <inheritdoc />
    public partial class Renamedtaskstonotesagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteTag_Notes_TasksId",
                table: "NoteTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteTag",
                table: "NoteTag");

            migrationBuilder.DropIndex(
                name: "IX_NoteTag_TasksId",
                table: "NoteTag");

            migrationBuilder.RenameColumn(
                name: "TasksId",
                table: "NoteTag",
                newName: "NotesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteTag",
                table: "NoteTag",
                columns: new[] { "NotesId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_NoteTag_TagsId",
                table: "NoteTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTag_Notes_NotesId",
                table: "NoteTag",
                column: "NotesId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteTag_Notes_NotesId",
                table: "NoteTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteTag",
                table: "NoteTag");

            migrationBuilder.DropIndex(
                name: "IX_NoteTag_TagsId",
                table: "NoteTag");

            migrationBuilder.RenameColumn(
                name: "NotesId",
                table: "NoteTag",
                newName: "TasksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteTag",
                table: "NoteTag",
                columns: new[] { "TagsId", "TasksId" });

            migrationBuilder.CreateIndex(
                name: "IX_NoteTag_TasksId",
                table: "NoteTag",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTag_Notes_TasksId",
                table: "NoteTag",
                column: "TasksId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
