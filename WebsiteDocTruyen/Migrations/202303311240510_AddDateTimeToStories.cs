namespace WebsiteDocTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTimeToStories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stories", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stories", "DateTime");
        }
    }
}
