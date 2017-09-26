using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.EntityFrameworkCore.Migrations
{
    public partial class RemoveDuplicateChangerChangeSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeSets_AspNetUsers_ChangerId1",
                table: "ChangeSets");

            migrationBuilder.DropIndex(
                name: "IX_ChangeSets_ChangerId1",
                table: "ChangeSets");

            migrationBuilder.DropColumn(
                name: "ChangerId",
                table: "ChangeSets");

            migrationBuilder.DropColumn(
                name: "ChangerId1",
                table: "ChangeSets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangerId",
                table: "ChangeSets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangerId1",
                table: "ChangeSets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeSets_ChangerId1",
                table: "ChangeSets",
                column: "ChangerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeSets_AspNetUsers_ChangerId1",
                table: "ChangeSets",
                column: "ChangerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
