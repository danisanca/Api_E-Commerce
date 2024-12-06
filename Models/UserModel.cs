using ApiEstoque.Helpers;

namespace ApiEstoque.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string typeAccout { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;

        public IEnumerable<HistoryMovimentModel> historyMoviments { get; set; }
        public IEnumerable<HistoryPurchaseModel> historyPurchases { get; set; }
        public IEnumerable<ScoreProductModel> scoreProducts { get; set; }
        public IEnumerable<EvidenceModel> evidences { get; set; }

        public void SetPasswordHash()
        {
            password = password.CreateHash();
        }
    }
}
