namespace UserManagement.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserPersonRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PersonId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PersonId");
        }
    }
}
