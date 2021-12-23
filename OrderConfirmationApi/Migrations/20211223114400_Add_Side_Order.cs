﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderConfirmationApi.Migrations
{
    public partial class Add_Side_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Side",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Side",
                table: "Orders");
        }
    }
}
