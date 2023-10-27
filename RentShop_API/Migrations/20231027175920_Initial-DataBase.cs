using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentShop_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataBase : Migration
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
                    TransportsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    { new Guid("a7545ea0-d165-451b-891b-85c7ad800624"), "Scooter" },
                    { new Guid("f8262a42-2924-4bd0-a346-52de667eb1db"), "Bike" },
                    { new Guid("fd5121f7-75d6-4a56-b349-ea2f9f02f156"), "Motorbike" }
                });

            migrationBuilder.InsertData(
                table: "LogTransactions",
                columns: new[] { "Id", "Results", "TransactionId" },
                values: new object[,]
                {
                    { new Guid("9aafcda5-254a-4a83-835a-7c77713639ca"), true, null },
                    { new Guid("bde66180-80a6-4945-aee0-3e6bb2f555fb"), true, null },
                    { new Guid("e5c1dc46-bc95-4593-a8be-c5d104a1b841"), false, null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "DateFrom", "DateTo", "Price", "UserId" },
                values: new object[,]
                {
                    { new Guid("6a54f9de-b9c8-4008-87eb-f16c444cf728"), new DateTime(2023, 10, 27, 12, 33, 13, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 27, 20, 59, 20, 151, DateTimeKind.Local).AddTicks(3186), 35f, null },
                    { new Guid("869167b6-3754-4e3a-b137-a1bbbd779382"), new DateTime(2023, 10, 27, 11, 25, 53, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 27, 20, 59, 20, 151, DateTimeKind.Local).AddTicks(3197), 155f, null },
                    { new Guid("bb9276ec-07c3-444f-9089-deef9f880374"), new DateTime(2023, 10, 27, 12, 55, 3, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 27, 20, 59, 20, 151, DateTimeKind.Local).AddTicks(3168), 55f, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Comment", "CreatedAt", "Grand", "TransportId", "TransportsId", "UserId" },
                values: new object[,]
                {
                    { new Guid("3fbbb959-ad20-48ff-9d0e-100213ea21bd"), "Nice", new DateTime(2023, 10, 27, 17, 59, 20, 151, DateTimeKind.Utc).AddTicks(3566), 4, null, null, null },
                    { new Guid("89a72202-1e5a-4abc-a7ba-eeec075b13e6"), "Good", new DateTime(2023, 10, 27, 17, 59, 20, 151, DateTimeKind.Utc).AddTicks(3560), 5, null, null, null },
                    { new Guid("f82e1950-eade-4c4b-a843-d65c12c78525"), "Bad", new DateTime(2023, 10, 27, 17, 59, 20, 151, DateTimeKind.Utc).AddTicks(3556), 2, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Address", "WorkTimeEnd", "WorkTimeStart" },
                values: new object[,]
                {
                    { new Guid("4f9f82fe-bce2-4e12-b8bd-5f5d8d7f0665"), "Street Chresatic 55", new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("837d7d5b-78a3-4849-9299-266b2b9d8c7d"), "Street Livikovicha 15", new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("c5be65bf-7141-4549-855b-d708e1eafa57"), "Street Victory 5", new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Date", "OrderId", "Sum" },
                values: new object[,]
                {
                    { new Guid("d145335e-e8ae-42c5-adda-53b331eac4aa"), new DateTime(2023, 10, 27, 20, 59, 20, 151, DateTimeKind.Local).AddTicks(3508), null, 125f },
                    { new Guid("d56f0ac3-3f04-415d-8b84-3f108961dbc3"), new DateTime(2023, 10, 27, 20, 59, 20, 151, DateTimeKind.Local).AddTicks(3514), null, 25f },
                    { new Guid("f0a1b6c7-f2fb-4db3-bc27-53aea9ed6a9b"), new DateTime(2023, 10, 27, 20, 59, 20, 151, DateTimeKind.Local).AddTicks(3518), null, 120f }
                });

            migrationBuilder.InsertData(
                table: "TransportAvailables",
                columns: new[] { "Id", "CountTransport", "ShopId", "TransportId" },
                values: new object[,]
                {
                    { new Guid("817de609-0ca0-4c15-88c4-d93c0e31f21f"), 14, null, null },
                    { new Guid("da2bac03-3752-4fe9-8f5f-2cd4667ae7d2"), 22, null, null },
                    { new Guid("f5d3cc5a-239d-4c3a-95e8-c72feccb287f"), 15, null, null }
                });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "CategoryId", "ImgUrl", "Mark", "MaxSpeed", "MaxWeight", "Model", "PriceMinute" },
                values: new object[,]
                {
                    { new Guid("7e878d64-ade2-4d56-8cc1-4fecff92ef8a"), null, "http://...", "Honda", 35, 125, "V3", 3.5f },
                    { new Guid("925435be-2647-4a46-aeb1-d2dbceae5d76"), null, "http://...", "Volva", 30, 105, "Speed", 3f },
                    { new Guid("c2f5c199-097d-414a-b2fa-30c14b068f68"), null, "http://...", "Tesla", 45, 115, "Skod", 4.5f }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "LastName", "Name", "Password", "Phone", "Role" },
                values: new object[,]
                {
                    { new Guid("585a94d1-6e77-48e5-a5de-5d3fac9a15bb"), new DateTime(2002, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "khalin2002@gmail.com", "Khalin", "Andrew", "10122002", "+380737303288", "User" },
                    { new Guid("84b85103-b966-4e21-af94-5980b68d017a"), new DateTime(2004, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "shabaltas@gmail.com", "Shabaltas", "Vlad", "648577", "+380991833277", "Admin" },
                    { new Guid("d1e341d8-8a9a-408c-aeec-cdbdfea0332e"), new DateTime(2001, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "lebid@gmail.com", "Lebid", "Vanya", "1920202", "+380737303277", "User" }
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
