namespace BikeTrips.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "Subject_Id" });
            AlterColumn("dbo.Comments", "Author_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "Subject_Id", c => c.Int());
            CreateIndex("dbo.Comments", "Subject_Id");
            CreateIndex("dbo.Comments", "Author_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "Subject_Id" });
            AlterColumn("dbo.Comments", "Subject_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "Author_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Comments", "Subject_Id");
            CreateIndex("dbo.Comments", "Author_Id");
        }
    }
}
