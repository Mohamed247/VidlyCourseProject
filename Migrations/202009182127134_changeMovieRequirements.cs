namespace VidlyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeMovieRequirements : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "DateAdded", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
