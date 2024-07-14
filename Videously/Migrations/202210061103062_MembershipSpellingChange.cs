namespace Videously.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembershipSpellingChange : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customers", name: "MembershiptType_Id", newName: "MembershipType_Id");
            RenameIndex(table: "dbo.Customers", name: "IX_MembershiptType_Id", newName: "IX_MembershipType_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Customers", name: "IX_MembershipType_Id", newName: "IX_MembershiptType_Id");
            RenameColumn(table: "dbo.Customers", name: "MembershipType_Id", newName: "MembershiptType_Id");
        }
    }
}
