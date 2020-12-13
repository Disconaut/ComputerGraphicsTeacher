using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Teacher.ViewModels.ColorModels
{
    class ColorModelPageViewModel
    {
        private readonly ResourceLoader _resourceLoader;


        public async void SaveImage()
        {
            var filePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                DefaultFileExtension = ".png",
                SuggestedFileName = _resourceLoader.GetString("SuggestedImageFileName")
            };

            filePicker.FileTypeChoices.Add("PNG", new List<string> { ".png" });
            filePicker.FileTypeChoices.Add("JPEG", new List<string> { ".jpeg" });
            filePicker.FileTypeChoices.Add("JPG", new List<string> { ".jpg" });

            var file = await filePicker.PickSaveFileAsync();

            if (file != null)
            {
                using var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                //await _renderTarget.SaveAsync(stream, CanvasBitmapFileFormat.Png, 1);
            }
        }

        public async void OpenImage()
        {
            var filePivker = new FileOpenPicker();
            filePivker.ViewMode = PickerViewMode.Thumbnail;
            filePivker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            filePivker.FileTypeFilter.Add(".jpg");
            filePivker.FileTypeFilter.Add(".jpeg");
            filePivker.FileTypeFilter.Add(".png");

            StorageFile file = await filePivker.PickSingleFileAsync();
            await file.CopyAsync(ApplicationData.Current.LocalFolder, "frameImage");
        }
    }
}

