using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PickGroup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_number = table.Column<string>(type: "text", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: true),
                    manager_name = table.Column<string>(type: "text", nullable: false),
                    manager_phone_number = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    delivery_address = table.Column<string>(type: "text", nullable: false),
                    delivery_start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    delivery_end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    delivery_shift = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.CheckConstraint("ck_orders_version_positive", "version > 0");
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: true),
                    storage_location_code = table.Column<string>(type: "text", nullable: false),
                    product_name = table.Column<string>(type: "text", nullable: false),
                    requires_manufacture_date = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    required_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    unit = table.Column<string>(type: "text", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.CheckConstraint("ck_order_items_required_quantity_positive", "required_quantity > 0");
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
