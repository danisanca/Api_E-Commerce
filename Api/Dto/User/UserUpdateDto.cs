﻿using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.User
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public int id { get; set; }

        [StringLength(45, ErrorMessage = "O nome deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string name { get; set; }

        [StringLength(100, ErrorMessage = "Email deve ter no maximo {1} characters.")]
        [EmailAddress(ErrorMessage = "Formato do email é invalido.")]
        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        public string email { get; set; }
    }
}
