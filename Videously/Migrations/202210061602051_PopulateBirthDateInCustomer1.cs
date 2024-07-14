namespace Videously.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBirthDateInCustomer1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET BirthDate = '2/3/1980' WHERE Id = 7");
            Sql("UPDATE Customers SET BirthDate = '6/7/1980' WHERE Id = 4");
            Sql("UPDATE Customers SET BirthDate = '9/10/1980' WHERE Id = 2");
            Sql("UPDATE Customers SET BirthDate = '12/11/1980' WHERE Id = 1");
        }
        
        public override void Down()
        {
        }
    }
}
