using ImageLikes.Data;

namespace ImageLikes.Web.Models
{
    public class ImageViewModel
    {
        public Image Image { get; set; }
        public bool CanNotLike { get; set; }
    }
}
