namespace VidlyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMovieRentals : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Rentals", newName: "MovieRentals");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MovieRentals", newName: "Rentals");
        }
    }
}
