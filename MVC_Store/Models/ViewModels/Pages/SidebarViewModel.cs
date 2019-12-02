using MVC_Store.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC_Store.Models.ViewModels.Pages
{
    public class SidebarViewModel
    {
        public SidebarViewModel()
        {
        }

        public SidebarViewModel(SidebarDTO sidebar)
        {
            Id = sidebar.Id;
            Body = sidebar.Body;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: int.MaxValue, MinimumLength = 3)]
        [AllowHtml]
        public string Body { get; set; }
    }
}