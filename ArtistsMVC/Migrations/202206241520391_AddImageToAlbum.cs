namespace ArtistsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToAlbum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Thumbnail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Thumbnail");
        }
    }
}
