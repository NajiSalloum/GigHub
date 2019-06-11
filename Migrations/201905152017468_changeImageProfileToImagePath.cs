namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeImageProfileToImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImagePath", c => c.String());
            DropColumn("dbo.AspNetUsers", "ProfileImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ProfileImage", c => c.String());
            DropColumn("dbo.AspNetUsers", "ImagePath");
        }
    }
}
