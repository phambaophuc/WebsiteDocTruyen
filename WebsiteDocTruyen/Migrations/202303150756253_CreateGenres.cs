namespace WebsiteDocTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (1, N'Tiên hiệp')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (2, N'Kiếm hiệp')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (3, N'Ngôn tình')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (4, N'Đô thị')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (5, N'Xuyên không')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (6, N'Trọng sinh')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (7, N'Tiểu thuyết')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (8, N'Huyền huyễn')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (9, N'Lịch sử')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (10, N'Cổ đại')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (11, N'Mạt thế')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (12, N'Thám hiểm')");
            Sql("INSERT INTO GENRES (GENREID, NAME) VALUES (13, N'Văn học Việt')");
        }

        public override void Down()
        {
        }
    }
}
