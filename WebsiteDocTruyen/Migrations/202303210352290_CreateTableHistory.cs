namespace WebsiteDocTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        HistoryID = c.Int(nullable: false, identity: true),
                        ChapterID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        DateRead = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryID)
                .ForeignKey("dbo.Chapters", t => t.ChapterID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.ChapterID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Histories", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Histories", "ChapterID", "dbo.Chapters");
            DropIndex("dbo.Histories", new[] { "UserID" });
            DropIndex("dbo.Histories", new[] { "ChapterID" });
            DropTable("dbo.Histories");
        }
    }
}
