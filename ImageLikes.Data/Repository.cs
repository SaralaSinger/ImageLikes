using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLikes.Data
{
    public class Repository
    {
        private string _connectionString;
        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(Image img)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Images.Add(img);
            context.SaveChanges();
        }
        public List<Image> GetAll()
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.ToList();
        }
        public void UpdateLikes(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            var img = context.Images.FirstOrDefault(i => i.Id == id);
            if(img != null)
            {
                img.Likes++;
                context.SaveChanges();
            }
            
        }
        public Image GetImageById(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }
    }
}
