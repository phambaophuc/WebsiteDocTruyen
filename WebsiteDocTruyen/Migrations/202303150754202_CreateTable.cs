namespace WebsiteDocTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chapters",
                c => new
                    {
                        ChapterID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        StoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChapterID)
                .ForeignKey("dbo.Stories", t => t.StoryID, cascadeDelete: true)
                .Index(t => t.StoryID);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        StoryID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Author = c.String(),
                        Description = c.String(),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.StoryID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoryId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stories", t => t.StoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.StoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GenreStories",
                c => new
                    {
                        Genre_GenreID = c.Int(nullable: false),
                        Story_StoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Story_StoryID })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Stories", t => t.Story_StoryID, cascadeDelete: true)
                .Index(t => t.Genre_GenreID)
                .Index(t => t.Story_StoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Favorites", "StoryId", "dbo.Stories");
            DropForeignKey("dbo.GenreStories", "Story_StoryID", "dbo.Stories");
            DropForeignKey("dbo.GenreStories", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.Chapters", "StoryID", "dbo.Stories");
            DropIndex("dbo.GenreStories", new[] { "Story_StoryID" });
            DropIndex("dbo.GenreStories", new[] { "Genre_GenreID" });
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.Favorites", new[] { "StoryId" });
            DropIndex("dbo.Chapters", new[] { "StoryID" });
            DropTable("dbo.GenreStories");
            DropTable("dbo.Favorites");
            DropTable("dbo.Genres");
            DropTable("dbo.Stories");
            DropTable("dbo.Chapters");
        }
    }
}
