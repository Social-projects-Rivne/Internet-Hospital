using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace InternetHospital.BusinessLogic.Validation
{
    public static class ImageValidation
    {
        //signatures of different types
        private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
        private static readonly byte[] JPG = { 255, 216, 255 };
        private static readonly byte AMOUNT_OF_BYTES_FOR_CHECKING = 16;

        /// <summary>
        /// Method for checking is file an image. Works faster than GetImageFromFile(). 
        /// If there is no need of validating width and height of image, it will be better to use this method.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsImage(IFormFile file)
        {
            bool isImage = false;
            if (file != null)
            {
                using (var fs = file.OpenReadStream())
                {
                    byte[] fileArr = new byte[AMOUNT_OF_BYTES_FOR_CHECKING];
                    fs.Read(fileArr, 0, AMOUNT_OF_BYTES_FOR_CHECKING);
                    if (fileArr.Take(JPG.Length).SequenceEqual(JPG)
                        || fileArr.Take(PNG.Length).SequenceEqual(PNG))
                    {
                        isImage = true;
                    }
                }
            }
            return isImage;
        }
        /// <summary>
        /// Check is file an image anf if yes - returns it, else returns null.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Image GetImageFromFile(IFormFile file)
        {
            Image img = null;
            try
            {
                using (var imgFromFile = Image.FromStream(file.OpenReadStream()))
                {
                    img = (Image)imgFromFile.Clone();
                    return img;
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool HasImageValidSize(Image img, int minHeight, int maxHeight, int minWidth, int maxWidth)
        {
            bool isValid = false;
            if (img != null)
            {
                bool isValidHeight = minHeight <= img.Height && img.Height <= maxHeight;
                bool isValidWidth = minWidth <= img.Width && img.Width <= maxWidth;
                isValid = isValidHeight && isValidWidth;
            }
            return isValid;
        }

        public static bool IsValidImageFile(IFormFile file, int minHeight, int maxHeight, int minWidth, int maxWidth)
        {
            bool isValid = false;
            Image img = GetImageFromFile(file);
            if (img != null)
            {
                isValid = HasImageValidSize(img, minHeight, maxHeight, minWidth, maxWidth);
            }
            return isValid;
        }
    }
}
