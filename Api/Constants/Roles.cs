namespace ApiEstoque.Constants
{
    public class Roles
    {
        public enum TypeUserRole
        {
            Admin,
            User
        }
        public enum AccessRole
        {
            Standard,
            Seller
        }
        public static class RoleHelper
        {
            public static string GetRoleName(TypeUserRole role) => role.ToString();
            public static string GetRoleName(AccessRole role) => role.ToString();

            // Ou uma forma geral:
            public static string GetRoleName<TEnum>(TEnum role) where TEnum : Enum
            {
                return role.ToString();
            }
        }
    }
}
