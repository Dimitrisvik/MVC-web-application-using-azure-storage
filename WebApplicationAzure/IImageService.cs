﻿using System.Collections.Generic;

namespace WebApplicationAzure
{
    /// <summary>
    /// Service responsible managing all Images operations
    /// </summary>
    public interface IImagesService
    {
        /// <summary>
        /// Returns all images 
        /// </summary>
        /// <returns></returns>
        List<TableImage> GetImages();

        /// <summary>
        /// Adds the supplied <paramref name="image"/> to the system and returns the Id.
        /// Part of the operation is to store the Image in the blob storage.
        /// </summary>
        int AddNewImage(WebApplicationAzure.Models.Image image);

        /// <summary>
        /// Deletes the Image with the supplied <paramref name="id"/> from the system 
        /// and deletes the file from the blob storage as well.
        /// </summary>
        void DeleteImage(int id);
        
    }
}
