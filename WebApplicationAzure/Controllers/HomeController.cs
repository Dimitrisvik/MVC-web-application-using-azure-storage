using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationAzure.Controllers
{
    public class HomeController : Controller
    {
        BlobServices _blobServices = new BlobServices();

        ImagesService _imageService = new ImagesService();

        DataClassesImageDataContext db = new DataClassesImageDataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Upload()
        {

            ViewBag.MyList = _imageService.GetImages();

            return View();
        }

        [HttpPost]
        public ActionResult Upload(WebApplicationAzure.Models.Image image)
        {

            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                if (file.ContentLength == 0)
                    continue;

                if (file.ContentLength > 0)
                {
                    image.Id = new Random().Next();
                    image.Name = image.Id.ToString() + file.FileName;
                    CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(image.Name);
                    blob.UploadFromStream(file.InputStream);

                    image.ImagePath = blob.Uri.AbsoluteUri;
                }

                int newImageFlag = _imageService.AddNewImage(image);
            }

            return RedirectToAction("Upload");
        }

        [HttpPost]
        public string DeleteImg(int Id)
        {
            TableImage image = db.TableImages.Single(TableImage => TableImage.Id == Id);

            Uri uri = new Uri(image.ImagePath);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);
            CloudBlobContainer blobContainer = _blobServices.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);
            blob.Delete();
            _imageService.DeleteImage(Id);
            return "File Successfully Deleted";

        }
    }

}