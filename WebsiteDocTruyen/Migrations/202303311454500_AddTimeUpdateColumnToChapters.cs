namespace WebsiteDocTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeUpdateColumnToChapters : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapters", "TimeUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chapters", "TimeUpdate");
        }
    }
}
