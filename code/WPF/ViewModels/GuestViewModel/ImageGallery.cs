using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class ImageGallery
    {
        #region Data
        /// <summary>
        /// Holds all accommodation images
        /// </summary>
        public List<string> Images { get; private set; } = new List<string>();
        /// <summary>
        /// Id of the 'AccommodationImages' element that is currently shown to the user
        /// </summary>
        public int ViewingImageId { get; private set; }
        #endregion

        public ImageGallery()
        {
        }

        public ImageGallery(List<string> images)
        {
            Images = images;
            ViewingImageId = 0;
        }

        /// <summary>
        /// Shows a message box on error
        /// </summary>
        /// <param name="imageGallery">The value of the name tag of the image in .xaml</param>
        public void RefreshFrontImage(System.Windows.Controls.Image imageGallery)
        {
            try
            {   //The OS can throw an exception if the file can't be loaded as a bitmap
                if (Images.Count > 0)
                {
                    var uploadedBitMapImage = new BitmapImage(new Uri(
                        Images[ViewingImageId]
                        , UriKind.RelativeOrAbsolute
                        )
                    );
                    imageGallery.Source = uploadedBitMapImage;
                }
                else
                {
                    //imageGallery.Source = new BitmapImage(new Uri(
                    //    "/Resources/Images/quit.png"
                    //    , UriKind.RelativeOrAbsolute)
                    //);
                    imageGallery.Source = null;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Exception occured:" + exception.Message);
            }
        }

        public void ShowPreviousImage(object imageControlObject)
        {
            System.Windows.Controls.Image img = (System.Windows.Controls.Image)imageControlObject;
            if (Images.Count > 0)
            {
                ViewingImageId += 1;
                ViewingImageId %= Images.Count;
            }
            RefreshFrontImage(img);
        }

        public void ShowNextImage(object imageControlObject)
        {
            System.Windows.Controls.Image img = (System.Windows.Controls.Image)imageControlObject;
            if (Images.Count > 0)
            {
                ViewingImageId -= 1;
                if (ViewingImageId < 0)
                {
                    ViewingImageId = Images.Count - 1 > 0 ? Images.Count - 1 : 0;
                }
            }
            RefreshFrontImage(img);
        }

        /// <summary>
        /// Shows a message box on exception. Does not refresh the front image.
        /// </summary>
        /// <param name="imagePath">relative or absolute</param>
        /// <returns> < 0 on error and 0 on success</returns>
        public int Add(string imagePath)
        {
            if (imagePath.Length <= 0)
            {
                return 0;
            }
            try
            {   //The OS can throw an exception if the file can't be loaded as a bitmap
                var uploadedBitMapImage = new BitmapImage(new Uri(
                        imagePath
                        , UriKind.RelativeOrAbsolute
                        )
                    );
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unable to add image to gallery due to exception:" + exception.Message);
                return -1;
            }
            Images.Add(imagePath);
            return 0;
        }

        public void RemoveAt(int id) 
        {
            if (Images.Count <= 0)
            {
                return;
            }
            Images.RemoveAt(id);
            if (ViewingImageId >= Images.Count )
            {
                ViewingImageId = Images.Count -1 < 0 ? 0 : Images.Count - 1;
            }
        }

        /// <summary>
        /// Refreshes the fron image if 'imageControl' is not null
        /// </summary>
        /// <param name="imageControl">null if not specified</param>
        public void RemoveViewingImage(object? imageControl = null)
        {
            if (Images.Count <= 0)
            {
                if (imageControl != null)
                    ((System.Windows.Controls.Image)imageControl).Source = null;
                return;
            }
            RemoveAt(ViewingImageId);
            if (imageControl != null)
                RefreshFrontImage((System.Windows.Controls.Image)imageControl);
        }
    }
}
