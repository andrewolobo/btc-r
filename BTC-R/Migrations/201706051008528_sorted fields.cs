namespace BTC_R.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sortedfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "display_name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "display_name");
        }
    }
}
