using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentShop_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkTimeStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    WorkTimeEnd = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceMinute = table.Column<float>(type: "real", nullable: false),
                    MaxSpeed = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxWeight = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transports_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grand = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransportAvailables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountTransport = table.Column<int>(type: "int", nullable: false),
                    TransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportAvailables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportAvailables_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportAvailables_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderShop",
                columns: table => new
                {
                    OrdersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShop", x => new { x.OrdersId, x.ShopsId });
                    table.ForeignKey(
                        name: "FK_OrderShop_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderShop_Shops_ShopsId",
                        column: x => x.ShopsId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTransport",
                columns: table => new
                {
                    OrdersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTransport", x => new { x.OrdersId, x.TransportsId });
                    table.ForeignKey(
                        name: "FK_OrderTransport_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTransport_Transports_TransportsId",
                        column: x => x.TransportsId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sum = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Results = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogTransactions_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name_Category" },
                values: new object[,]
                {
                    { new Guid("03b13ea3-1c23-48bc-9100-3f8447e729db"), "Scooter" },
                    { new Guid("979a107c-3aaf-4501-bb0f-eba5635a8081"), "Motorbike" },
                    { new Guid("b0a72c41-77ed-4a70-bf0d-3419d4df0f5c"), "Bike" }
                });

            migrationBuilder.InsertData(
                table: "LogTransactions",
                columns: new[] { "Id", "Results", "TransactionId" },
                values: new object[,]
                {
                    { new Guid("846b5455-f0e2-4781-b0d5-b8c965bbaae1"), true, null },
                    { new Guid("942352ea-532a-49ab-b82b-38c2ceb29a64"), true, null },
                    { new Guid("b2dc3263-878a-4931-9c13-1918f7fb395b"), false, null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "DateFrom", "DateTo", "Price", "UserId" },
                values: new object[,]
                {
                    { new Guid("17bddc87-3b52-4dea-9ced-3f2d1bf076bb"), new DateTime(2023, 10, 28, 11, 25, 53, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 28, 23, 46, 8, 584, DateTimeKind.Local).AddTicks(9970), 155f, null },
                    { new Guid("ae295cdb-37b4-4777-b694-664bfb02811b"), new DateTime(2023, 10, 28, 12, 55, 3, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 28, 23, 46, 8, 584, DateTimeKind.Local).AddTicks(9946), 55f, null },
                    { new Guid("c6daabeb-e90c-43b2-8a63-8308838335f4"), new DateTime(2023, 10, 28, 12, 33, 13, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 28, 23, 46, 8, 584, DateTimeKind.Local).AddTicks(9961), 35f, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Comment", "CreatedAt", "Grand", "TransportId", "UserId" },
                values: new object[,]
                {
                    { new Guid("a3f2f78b-9d65-4875-8071-462bf4d09e36"), "Bad", new DateTime(2023, 10, 28, 20, 46, 8, 585, DateTimeKind.Utc).AddTicks(212), 2, null, null },
                    { new Guid("b74f6e70-c8e1-43f2-a24b-85207bcfa853"), "Nice", new DateTime(2023, 10, 28, 20, 46, 8, 585, DateTimeKind.Utc).AddTicks(218), 4, null, null },
                    { new Guid("d45f9e9e-35d3-4014-ab9a-153bfd5ff020"), "Good", new DateTime(2023, 10, 28, 20, 46, 8, 585, DateTimeKind.Utc).AddTicks(215), 5, null, null }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Address", "WorkTimeEnd", "WorkTimeStart" },
                values: new object[,]
                {
                    { new Guid("170bfc2f-5b3d-41df-946e-2f84ac3d5316"), "Street Livikovicha 15", new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("2c247366-ff1a-4692-9015-412ff684de4d"), "Street Victory 5", new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("88ee7a78-94e1-4fcd-ae1d-432d554c42b0"), "Street Chresatic 55", new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Date", "OrderId", "Sum" },
                values: new object[,]
                {
                    { new Guid("0e5d35d5-559a-4b41-b2d2-5c275fec3970"), new DateTime(2023, 10, 28, 23, 46, 8, 585, DateTimeKind.Local).AddTicks(185), null, 25f },
                    { new Guid("972da729-279c-44ac-b683-fe8d1e412a27"), new DateTime(2023, 10, 28, 23, 46, 8, 585, DateTimeKind.Local).AddTicks(180), null, 125f },
                    { new Guid("efcb97e4-cd92-4613-b001-f1574dde3bb6"), new DateTime(2023, 10, 28, 23, 46, 8, 585, DateTimeKind.Local).AddTicks(189), null, 120f }
                });

            migrationBuilder.InsertData(
                table: "TransportAvailables",
                columns: new[] { "Id", "CountTransport", "ShopId", "TransportId" },
                values: new object[,]
                {
                    { new Guid("5da20ea5-1073-4693-a1aa-9305a39d04fa"), 15, null, null },
                    { new Guid("dd31d57f-493d-43e6-ae27-882824a9c1bd"), 14, null, null },
                    { new Guid("f22b84d2-f3b8-4948-8e7f-30cf11e7e73b"), 22, null, null }
                });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "CategoryId", "ImgUrl", "Mark", "MaxSpeed", "MaxWeight", "Model", "PriceMinute" },
                values: new object[,]
                {
                    { new Guid("13e130ba-4766-4b3b-8aca-3a5ac268bea4"), null, "http://...", "Volva", 30, 105, "Speed", 3f },
                    { new Guid("35705a50-fa2b-453a-833c-8dee0be68c53"), null, "http://...", "Tesla", 45, 115, "Skod", 4.5f },
                    { new Guid("ddcd9353-b811-485a-b9a7-9e886a2d9871"), null, "http://...", "Honda", 35, 125, "V3", 3.5f }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "LastName", "Name", "Password", "Phone", "Role" },
                values: new object[,]
                {
                    { new Guid("16d81047-a3a4-4e33-bf6f-f2f68a4d1bcb"), new DateTime(2001, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "lebid@gmail.com", "Lebid", "Vanya", "1920202", "+380737303277", "User" },
                    { new Guid("4f990671-5a3f-4e77-a745-745446513434"), new DateTime(2004, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "shabaltas@gmail.com", "Shabaltas", "Vlad", "648577", "+380991833277", "Admin" },
                    { new Guid("5af1e215-7722-4114-89ea-2b9e5aebac29"), new DateTime(2002, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "khalin2002@gmail.com", "Khalin", "Andrew", "10122002", "+380737303288", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogTransactions_TransactionId",
                table: "LogTransactions",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShop_ShopsId",
                table: "OrderShop",
                column: "ShopsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTransport_TransportsId",
                table: "OrderTransport",
                column: "TransportsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_TransportId",
                table: "Ratings",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OrderId",
                table: "Transactions",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TransportAvailables_ShopId",
                table: "TransportAvailables",
                column: "ShopId",
                unique: true,
                filter: "[ShopId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TransportAvailables_TransportId",
                table: "TransportAvailables",
                column: "TransportId",
                unique: true,
                filter: "[TransportId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_CategoryId",
                table: "Transports",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogTransactions");

            migrationBuilder.DropTable(
                name: "OrderShop");

            migrationBuilder.DropTable(
                name: "OrderTransport");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "TransportAvailables");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
