namespace Videously.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyMembershipTypeToCUstomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        SignUpFee = c.Short(nullable: false),
                        DurationInMonths = c.Byte(nullable: false),
                        DiscountRate = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "MembershiptType_Id", c => c.Byte());
            CreateIndex("dbo.Customers", "MembershiptType_Id");
            AddForeignKey("dbo.Customers", "MembershiptType_Id", "dbo.MembershipTypes", "Id");
            DropColumn("dbo.Customers", "MembershiptTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "MembershiptTypeId", c => c.Byte(nullable: false));
            DropForeignKey("dbo.Customers", "MembershiptType_Id", "dbo.MembershipTypes");
            DropIndex("dbo.Customers", new[] { "MembershiptType_Id" });
            DropColumn("dbo.Customers", "MembershiptType_Id");
            DropTable("dbo.MembershipTypes");
        }
    }
}
