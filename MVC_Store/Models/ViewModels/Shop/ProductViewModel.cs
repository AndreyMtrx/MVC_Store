using MVC_Store.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Models.ViewModels.Shop
{
    public class ProductViewModel
    {
        public ProductViewModel() { }
        public ProductViewModel(ProductDTO productDTO) {
            Id = productDTO.Id;
            Name = productDTO.Name;
            Slug = productDTO.Slug;
            Description = productDTO.Description;
            Price = productDTO.Price;
            CategoryName = productDTO.CategoryName;
            CategoryId = productDTO.CategoryId;
            ImageName = productDTO.ImageName;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [DisplayName("Image name")]
        public string ImageName { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<string> GalleryImages { get; set; }
    }
}