namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserNotifications", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Notifications", "ApplicationUser_Id");
            DropColumn("dbo.UserNotifications", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserNotifications", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Notifications", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserNotifications", "ApplicationUser_Id");
            CreateIndex("dbo.Notifications", "ApplicationUser_Id");
            AddForeignKey("dbo.UserNotifications", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Notifications", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
