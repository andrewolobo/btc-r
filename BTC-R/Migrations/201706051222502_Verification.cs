namespace BTC_R.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Verification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "verified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "verified");
        }
    }
}
