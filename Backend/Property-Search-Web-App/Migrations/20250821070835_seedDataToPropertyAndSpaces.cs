using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Search_Web_App.Migrations
{
    public partial class seedDataToPropertyAndSpaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "Description", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "123 Main St, City, State", "A beautiful family house", 300000m, "house" },
                    { 2, "456 Oak St, City, State", "Cozy apartment in the city center", 150000m, "apartment" },
                    { 3, "789 Pine St, City, State", "Modern condo with great amenities", 250000m, "condo" },
                    { 4, "101 Maple Ave, City, State", "Spacious house with a large yard", 400000m, "house" },
                    { 5, "202 Birch Rd, City, State", "Affordable apartment with great location", 120000m, "apartment" },
                    { 6, "303 Cedar Dr, City, State", "Nice condo near the beach", 220000m, "condo" },
                    { 7, "404 Elm St, City, State", "House with open-plan living space", 350000m, "house" },
                    { 8, "505 Walnut Ln, City, State", "Bright apartment with excellent views", 170000m, "apartment" },
                    { 9, "606 Ash Blvd, City, State", "Luxury condo with pool and gym", 280000m, "condo" },
                    { 10, "707 Maple Dr, City, State", "Large family house with 5 bedrooms", 320000m, "house" }
                });

            migrationBuilder.InsertData(
                table: "Spaces",
                columns: new[] { "Id", "Description", "PropertyId", "Size", "Type" },
                values: new object[,]
                {
                    { 1, "Master bedroom", 1, 150m, "bedroom" },
                    { 2, "Modern kitchen", 1, 100m, "kitchen" },
                    { 3, "Spacious living room", 1, 200m, "living room" },
                    { 4, "Cozy bedroom", 2, 120m, "bedroom" },
                    { 5, "Full bathroom", 2, 60m, "bathroom" },
                    { 6, "Luxury bedroom with ensuite", 3, 140m, "bedroom" },
                    { 7, "State-of-the-art kitchen", 3, 110m, "kitchen" },
                    { 8, "Open-plan living room", 4, 220m, "living room" },
                    { 9, "Large bedroom", 4, 130m, "bedroom" },
                    { 10, "Compact kitchen with modern appliances", 5, 90m, "kitchen" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Spaces",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
