namespace ArtistsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedTheImagesOfAlbums : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Albums SET Thumbnail = 'na_image.jpg'");
        }
        
        public override void Down()
        {
            Sql("UPDATE dbo.Albums SET Thumbnail = ''");
        }
    }
}
