namespace Videously.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeMoreEntriesToGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Horror')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6, 'Bio-pic')");
        }
        
        public override void Down()
        {
        }
    }
}
