namespace ApiEstoque.Dto.User
{
    public class ChangePasswordDto
    {
        public int idUser { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
