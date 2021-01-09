using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HStore.Migrations
{
    public partial class _1st : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(maxLength: 50, nullable: true),
                    TotalPaid = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    TotalRemaining = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 350, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    TodayPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, defaultValueSql: "((0))"),
                    Quantity = table.Column<decimal>(type: "decimal(18, 0)", nullable: true, defaultValueSql: "((0))"),
                    StorePrice = table.Column<decimal>(type: "decimal(37, 2)", nullable: true, computedColumnSql: "(isnull([TodayPrice],(0))*isnull([Quantity],(0)))"),
                    ProductionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(maxLength: 50, nullable: true),
                    TotalPaid = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    TotalRemaining = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientsPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentValue = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaymentComment = table.Column<string>(maxLength: 2000, nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientsPayments_Clients",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientsPayments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(maxLength: 50, nullable: true),
                    SellDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Paid = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, defaultValueSql: "((0))"),
                    Remaining = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, defaultValueSql: "((0))"),
                    TotalAmount = table.Column<decimal>(type: "decimal(19, 2)", nullable: true, computedColumnSql: "(isnull([Paid],(0))+isnull([Remaining],(0)))"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellRequest_Clients",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellRequest_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(maxLength: 50, nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SupplierId = table.Column<int>(nullable: false),
                    Paid = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, defaultValueSql: "((0))"),
                    Remaining = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, defaultValueSql: "((0))"),
                    TotalAmount = table.Column<decimal>(type: "decimal(19, 2)", nullable: true, computedColumnSql: "(isnull([Paid],(0))+isnull([Remaining],(0)))"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseRequest_Suppliers",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseRequest_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuppliersPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentValue = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaymentComment = table.Column<string>(maxLength: 2000, nullable: true),
                    SupplierId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuppliersPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuppliersPayments_Suppliers",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuppliersPayments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellRequestId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, defaultValueSql: "((0))"),
                    SellQuantity = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    SellTotalAmount = table.Column<decimal>(type: "decimal(29, 2)", nullable: true, computedColumnSql: "(isnull([SellPrice],(0))*isnull([SellQuantity],(0)))"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellRequestDetails_StoreItems",
                        column: x => x.ItemId,
                        principalTable: "StoreItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellRequestDetails_SellRequest",
                        column: x => x.SellRequestId,
                        principalTable: "SellRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellRequestDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseRequestId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, defaultValueSql: "((0))"),
                    PurchaseQuantity = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    PurchaseTotalAmount = table.Column<decimal>(type: "decimal(29, 2)", nullable: true, computedColumnSql: "(isnull([PurchasePrice],(0))*isnull([PurchaseQuantity],(0)))"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestDetails_StoreItems",
                        column: x => x.ItemId,
                        principalTable: "StoreItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestDetails_PurchaseRequest",
                        column: x => x.PurchaseRequestId,
                        principalTable: "PurchaseRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsPayments_ClientId",
                table: "ClientsPayments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsPayments_UserId",
                table: "ClientsPayments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequest_SupplierId",
                table: "PurchaseRequest",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequest_UserId",
                table: "PurchaseRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestDetails_ItemId",
                table: "PurchaseRequestDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestDetails_PurchaseRequestId",
                table: "PurchaseRequestDetails",
                column: "PurchaseRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestDetails_UserId",
                table: "PurchaseRequestDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SellRequest_ClientId",
                table: "SellRequest",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SellRequest_UserId",
                table: "SellRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SellRequestDetails_ItemId",
                table: "SellRequestDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SellRequestDetails_SellRequestId",
                table: "SellRequestDetails",
                column: "SellRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SellRequestDetails_UserId",
                table: "SellRequestDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItems_UserId",
                table: "StoreItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UserId",
                table: "Suppliers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuppliersPayments_SupplierId",
                table: "SuppliersPayments",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SuppliersPayments_UserId",
                table: "SuppliersPayments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClientsPayments");

            migrationBuilder.DropTable(
                name: "PurchaseRequestDetails");

            migrationBuilder.DropTable(
                name: "SellRequestDetails");

            migrationBuilder.DropTable(
                name: "SuppliersPayments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PurchaseRequest");

            migrationBuilder.DropTable(
                name: "StoreItems");

            migrationBuilder.DropTable(
                name: "SellRequest");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
