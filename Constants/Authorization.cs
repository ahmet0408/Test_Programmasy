namespace TestProgrammasy.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Mugallym,
            Okuwçy
        }


        public const string default_username = "Admin";
        public const string default_password = "Password1!";
        public const Roles default_role = Roles.Administrator;
    }
}
