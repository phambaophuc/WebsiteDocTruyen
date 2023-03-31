namespace WebsiteDocTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTimeToChapter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapters", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chapters", "DateTime");
        }
    }
}
