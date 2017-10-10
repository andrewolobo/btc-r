namespace BTC_R.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        user_id = c.Guid(nullable: false),
                        action = c.String(),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
