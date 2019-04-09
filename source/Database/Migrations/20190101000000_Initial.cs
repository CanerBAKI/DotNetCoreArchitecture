using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DotNetCoreArchitecture.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("UsersLogs", "User");
            migrationBuilder.DropTable("Users", "User");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("User");

            migrationBuilder.CreateTable("Users", schema: "User",
                columns: table => new
                {
                    UserId = table.Column<long>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName_Name = table.Column<string>(maxLength: 100),
                    FullName_Surname = table.Column<string>(maxLength: 200),
                    Email = table.Column<string>(maxLength: 300),
                    SignIn_Login = table.Column<string>(maxLength: 100),
                    SignIn_Password = table.Column<string>(maxLength: 500),
                    Roles = table.Column<int>(),
                    Status = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable("UsersLogs", schema: "User",
                columns: table => new
                {
                    UserLogId = table.Column<long>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(),
                    LogType = table.Column<int>(),
                    DateTime = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLogs", x => x.UserLogId);
                    table.ForeignKey("FK_UsersLogs_Users_UserId",
                        x => x.UserId,
                        principalSchema: "User",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "User",
                table: "Users",
                columns: new[] { "UserId", "Email", "Roles", "Status", "FullName_Name", "FullName_Surname", "SignIn_Login", "SignIn_Password" },
                values: new object[] { 1L, "administrator@administrator.com", 3, 1, "Administrator", "Administrator", "admin", "1h0ATANFe6x7kMHo1PURE74WI0ayevUwfK/+Ie+IWX/xWrFWngcVUwL/ewryn38EMVMQBFaNo4SaVwgXaBWnDw==" });

            migrationBuilder.CreateIndex("IX_Users_SignIn_Login", "Users", "SignIn_Login", "User", true);
            migrationBuilder.CreateIndex("IX_Users_Email", "Users", "Email", "User", true);
            migrationBuilder.CreateIndex("IX_UsersLogs_UserId", "UsersLogs", "UserId", "User");
        }
    }
}
