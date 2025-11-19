using ApiEstoque.Constants;
using Microsoft.AspNetCore.Identity;
using SharedBase.Models;

namespace ApiEstoque.Models
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; } = FilterGetRoutes.Ativo.ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }

        public IEnumerable<HistoryMovimentModel> historyMoviments { get; set; }
    }
}
