namespace ArtistsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToArtist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "Thumbnail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Thumbnail");
        }
    }
}
