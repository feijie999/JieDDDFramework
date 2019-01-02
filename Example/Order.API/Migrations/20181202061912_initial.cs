using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buyers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 64, nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orderstatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderstatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymenttypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymenttypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymentmethods",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 64, nullable: false),
                    FreeCode = table.Column<string>(maxLength: 32, nullable: true),
                    PaymentTypeId = table.Column<int>(nullable: true),
                    BuyerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentmethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paymentmethods_buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_paymentmethods_paymenttypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "paymenttypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 64, nullable: false),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_State = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true),
                    Address_ZipCode = table.Column<string>(nullable: true),
                    OrderStatusId = table.Column<int>(nullable: true),
                    BuyerId = table.Column<string>(nullable: true),
                    PaymentMethodId = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DeletedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_orderstatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "orderstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_paymentmethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "paymentmethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 64, nullable: false),
                    ProductId = table.Column<string>(maxLength: 32, nullable: false),
                    ProductName = table.Column<string>(maxLength: 32, nullable: false),
                    ProductCount = table.Column<int>(nullable: false),
                    ProductPrice = table.Column<decimal>(nullable: false),
                    OrderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderItems_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_OrderId",
                table: "orderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_BuyerId",
                table: "orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_OrderStatusId",
                table: "orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_PaymentMethodId",
                table: "orders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_paymentmethods_BuyerId",
                table: "paymentmethods",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_paymentmethods_PaymentTypeId",
                table: "paymentmethods",
                column: "PaymentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "orderstatus");

            migrationBuilder.DropTable(
                name: "paymentmethods");

            migrationBuilder.DropTable(
                name: "buyers");

            migrationBuilder.DropTable(
                name: "paymenttypes");
        }
    }
}
