namespace Chilaqueria_API.Routers
{
    public class ApiRoutes
    {
        public static class Users
        {
            public static string GetAllActiveUsers => "Account/GetAllActiveUsers";
            public static string GetUserByID(int id) => $"Account/GetUserByID/{id}";

        }
    }
}
