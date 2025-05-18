using ApiEstoque.Helpers;
using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class UserModel : BaseEntity
    {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string typeAccount { get; set; }

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
