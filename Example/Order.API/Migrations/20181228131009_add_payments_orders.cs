using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.API.Migrations
{
    public partial class add_payments_orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_OrderStatus_OrderStatusId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_Payments_PaymentMethodId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_buyers_BuyerId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentTypes_PaymentTypeId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentTypes",
                table: "PaymentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "PaymentTypes",
                newName: "paymenttypes");

            migrationBuilder.RenameTable(
                name: "OrderStatus",
                newName: "orderstatus");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "orderItems");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "paymentmethods");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "orderItems",
                newName: "IX_orderItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PaymentTypeId",
                table: "paymentmethods",
                newName: "IX_paymentmethods_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BuyerId",
                table: "paymentmethods",
                newName: "IX_paymentmethods_BuyerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "paymenttypes",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "paymenttypes",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "orderstatus",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "orderstatus",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "orderItems",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "orderItems",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FreeCode",
                table: "paymentmethods",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "paymentmethods",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_paymenttypes",
                table: "paymenttypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderstatus",
                table: "orderstatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderItems",
                table: "orderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paymentmethods",
                table: "paymentmethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_orders_OrderId",
                table: "orderItems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_orderstatus_OrderStatusId",
                table: "orders",
                column: "OrderStatusId",
                principalTable: "orderstatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_paymentmethods_PaymentMethodId",
                table: "orders",
                column: "PaymentMethodId",
                principalTable: "paymentmethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_paymentmethods_buyers_BuyerId",
                table: "paymentmethods",
                column: "BuyerId",
                principalTable: "buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_paymentmethods_paymenttypes_PaymentTypeId",
                table: "paymentmethods",
                column: "PaymentTypeId",
                principalTable: "paymenttypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_orders_OrderId",
                table: "orderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_orderstatus_OrderStatusId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_paymentmethods_PaymentMethodId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_paymentmethods_buyers_BuyerId",
                table: "paymentmethods");

            migrationBuilder.DropForeignKey(
                name: "FK_paymentmethods_paymenttypes_PaymentTypeId",
                table: "paymentmethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paymenttypes",
                table: "paymenttypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderstatus",
                table: "orderstatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderItems",
                table: "orderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paymentmethods",
                table: "paymentmethods");

            migrationBuilder.RenameTable(
                name: "paymenttypes",
                newName: "PaymentTypes");

            migrationBuilder.RenameTable(
                name: "orderstatus",
                newName: "OrderStatus");

            migrationBuilder.RenameTable(
                name: "orderItems",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "paymentmethods",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_orderItems_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_paymentmethods_PaymentTypeId",
                table: "Payments",
                newName: "IX_Payments_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_paymentmethods_BuyerId",
                table: "Payments",
                newName: "IX_Payments_BuyerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PaymentTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PaymentTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 1)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderStatus",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OrderStatus",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 1)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "OrderItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "OrderItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "FreeCode",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentTypes",
                table: "PaymentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_OrderStatus_OrderStatusId",
                table: "orders",
                column: "OrderStatusId",
                principalTable: "OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Payments_PaymentMethodId",
                table: "orders",
                column: "PaymentMethodId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_buyers_BuyerId",
                table: "Payments",
                column: "BuyerId",
                principalTable: "buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentTypes_PaymentTypeId",
                table: "Payments",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
