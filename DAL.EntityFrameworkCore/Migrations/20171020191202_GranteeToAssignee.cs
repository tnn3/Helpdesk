using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.EntityFrameworkCore.Migrations
{
    public partial class GranteeToAssignee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_AspNetUsers_GranteeId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_GranteeId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "GranteeId",
                table: "ProjectTasks");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeId",
                table: "ProjectTasks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_AssigneeId",
                table: "ProjectTasks",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_AspNetUsers_AssigneeId",
                table: "ProjectTasks",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_AspNetUsers_AssigneeId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_AssigneeId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "ProjectTasks");

            migrationBuilder.AddColumn<string>(
                name: "GranteeId",
                table: "ProjectTasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_GranteeId",
                table: "ProjectTasks",
                column: "GranteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_AspNetUsers_GranteeId",
                table: "ProjectTasks",
                column: "GranteeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
