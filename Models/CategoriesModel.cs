﻿namespace ApiEstoque.Models
{
    public class CategoriesModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string? imageUrl { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;

    }
}
