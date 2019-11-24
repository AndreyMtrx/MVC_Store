using MVC_Store.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace MVC_Store.Models.ViewModels.Pages
{
    public class PageViewModel
    {
        public PageViewModel()
        {
        }

        public PageViewModel(PagesDTO row)
        {
            Id = row.Id;
            Title = row.Title;
            Slug = row.Slug;
            Body = row.Body;
            Sorting = row.Sorting;
            HasSidebar = row.HasSidebar;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Title { get; set; }

        public string Slug { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Body { get; set; }

        public int Sorting { get; set; }
        public bool HasSidebar { get; set; }
    }
}