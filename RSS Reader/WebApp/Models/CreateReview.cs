using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class CreateReview
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Review is required")]
        [Length(7, 1000, ErrorMessage = "Review is too short")]
        public string MainBody { get; set; }
    }
}
