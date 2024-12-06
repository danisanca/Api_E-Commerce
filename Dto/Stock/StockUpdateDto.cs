﻿using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Stock
{
    public class StockUpdateDto
    {
        [Required(ErrorMessage = "Id do stock é um campo obrigatório.")]
        public int idStock { get; set; }

        [Required(ErrorMessage = "Quantidade é um campo obrigatório.")]
        public float amount { get; set; }

        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public int productId { get; set; }
    }
}