namespace WebsiteDocTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChapterNumberToStories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stories", "ChapterNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stories", "ChapterNumber");
        }
    }
}
