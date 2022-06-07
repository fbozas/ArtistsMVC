namespace ArtistsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAlbumsDescriptions : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Albums SET Description = 'An Awesome Album 1' WHERE ID = 1");
            Sql("UPDATE Albums SET Description = 'An Awesome Album 2' WHERE ID = 2");
            Sql("UPDATE Albums SET Description = 'An Awesome Album 3' WHERE ID = 3");
        }
        
        public override void Down()
        {
            Sql("UPDATE Albums SET Description = 'Desc 1' WHERE ID = 1");
            Sql("UPDATE Albums SET Description = 'DESC 2' WHERE ID = 2");
            Sql("UPDATE Albums SET Description = 'DESC 3' WHERE ID = 3");
        }
    }
}
