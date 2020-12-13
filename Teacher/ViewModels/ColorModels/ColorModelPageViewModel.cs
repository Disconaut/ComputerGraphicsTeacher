using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
using CGTeacherShared.Annotations;
using Microsoft.Toolkit.Extensions;
using Microsoft.Toolkit.Uwp.Helpers;
using Teacher.ViewModels.Shared;
using ColorHelper = Microsoft.Toolkit.Uwp.Helpers.ColorHelper;

namespace Teacher.ViewModels.ColorModels
{
    public class ColorModelPageViewModel: INotifyPropertyChanged
    {
        private readonly ResourceLoader _resourceLoader;

        private WriteableBitmap _image;
        private WriteableBitmap _imageCopy;
        private IEnumerable<int> _chosenPixels;
        private int _oldColorInt;

        public WriteableBitmap Image
        {
            get => _image;
            set
            {
                _image = value;
                _imageCopy = _image.Clone();
                OnPropertyChanged(nameof(Image));
            }
        }

        public ColorModelPageViewModel()
        {
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

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

        public async Task OpenImage()
        {
            var fileOpenPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail, 
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".jpeg");
            fileOpenPicker.FileTypeFilter.Add(".png");

            StorageFile file = await fileOpenPicker.PickSingleFileAsync();
            if (file != null)
            {
                using var stream = await file.OpenReadAsync();
                var bitmap = await BitmapFactory.FromStream(stream);
                Image = bitmap;
            }
        }

        public void ChoosePixelWithColor(Color color, Rect cropRect)
        {
            _oldColorInt = color.ToInt();
            using var context = Image.GetBitmapContext();
            _chosenPixels = context.Pixels
                .Select((x, ind) => new Pixel(x, ind, context.Width))
                .Where(pixel => cropRect.Contains(pixel.Point) && pixel.Color == _oldColorInt)
                .Select(x => x.Index)
                .ToArray();
        }

        public void ChangeColorOfChosePixels(Color color)
        {
            var colorInt = color.ToInt();
            using var context = Image.GetBitmapContext();
            foreach (var pixel in _chosenPixels)
            {
                Image.SetPixeli(pixel, colorInt);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

