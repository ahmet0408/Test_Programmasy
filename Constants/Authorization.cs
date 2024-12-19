namespace TestProgrammasy.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Admin,
            Teacher,
            Student
        }


        public const string default_username = "Admin";
        public const string default_password = "Password1!";
        public const Roles default_role = Roles.Admin;
    }
}
