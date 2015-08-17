namespace Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoursesAndClassrooms : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CourseStudents", newName: "StudentCourses");
            DropPrimaryKey("dbo.StudentCourses");
            CreateTable(
                "dbo.ClassRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingNumber = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            AddPrimaryKey("dbo.StudentCourses", new[] { "Student_Id", "Course_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassRooms", "Course_Id", "dbo.Courses");
            DropIndex("dbo.ClassRooms", new[] { "Course_Id" });
            DropPrimaryKey("dbo.StudentCourses");
            DropTable("dbo.ClassRooms");
            AddPrimaryKey("dbo.StudentCourses", new[] { "Course_Id", "Student_Id" });
            RenameTable(name: "dbo.StudentCourses", newName: "CourseStudents");
        }
    }
}
