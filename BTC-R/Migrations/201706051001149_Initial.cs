namespace BTC_R.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BtcAddresses",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        user_id = c.Guid(nullable: false),
                        address = c.String(),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        first_name = c.String(),
                        last_name = c.String(),
                        email_address = c.String(),
                        password = c.Binary(),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.BtcAddresses");
        }
    }
}
