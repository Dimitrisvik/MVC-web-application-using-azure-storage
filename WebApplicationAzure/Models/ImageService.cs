using System.Collections.Generic;
using System.Linq;

namespace WebApplicationAzure
{

    public class ImagesService : IImagesService
    {
        public DataClassesImageDataContext DataImage
        {
            get
            {
                return new DataClassesImageDataContext();
            }
        }

        public int AddNewImage(WebApplicationAzure.Models.Image image)
        {
            using (DataClassesImageDataContext db = new DataClassesImageDataContext())
            {
                TableImage newImage = new TableImage();
                newImage.Id = image.Id;
                newImage.Name = image.Name;
                newImage.Description = image.Description;
                newImage.ImagePath = image.ImagePath;

                db.TableImages.InsertOnSubmit(newImage);
                db.SubmitChanges();
            }
            return image.Id;
        }

        public void DeleteImage(int id)
        {
            using (DataClassesImageDataContext db = new DataClassesImageDataContext())
            {
                TableImage image = db.TableImages.Single(TableImage => TableImage.Id == id);

                db.TableImages.DeleteOnSubmit(image);
                db.SubmitChanges();
            }
        }

        public List<TableImage> GetImages()
        {
            return DataImage.TableImages.ToList();
        }
    }
}
