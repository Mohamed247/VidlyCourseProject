namespace VidlyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'39210068-f012-4c88-95bf-c691f289120f', N'admin@vidly.com', 0, N'AINyyJO+FrmtoCBjWFHAqFgpLBgrN6c8q81kl4G5LihRNCUkjFM7Z25vWz3EChYcog==', N'35287983-e7d1-4cb0-abf1-6f6a58325a43', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'496dc405-2ae9-4e43-91e1-ad43da9843c1', N'guest@vidly.com', 0, N'AJYlb6/RLYXX3n22IZy7furdh19uK4KNgA8UgHtB+0EvTDHIibDa/XuM6IlXo/zR7A==', N'3ebf0252-0bd3-42b4-adab-58edf9a5ea07', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b920ff5c-0618-4e4f-b34a-e856edcd3015', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'39210068-f012-4c88-95bf-c691f289120f', N'b920ff5c-0618-4e4f-b34a-e856edcd3015')
");
        }
        
        public override void Down()
        {
        }
    }
}
