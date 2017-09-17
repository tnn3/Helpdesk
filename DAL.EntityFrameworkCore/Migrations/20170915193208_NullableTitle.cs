using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.EntityFrameworkCore.Migrations
{
    public partial class NullableTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TitleId",
                table: "AspNetUsers",
                type: "int4",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TitleId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4",
                oldNullable: true);
        }
    }
}
