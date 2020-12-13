using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using CGTeacherShared.Annotations;
using Microsoft.Toolkit.Extensions;
using Microsoft.Toolkit.Uwp.Helpers;
using Teacher.ViewModels.Controls.AdvancedColorPicker;
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
                _imageCopy = _image?.Clone();
                OnPropertyChanged(nameof(Image));
            }
        }

        public ObservableCollection<ColorModelViewModelBase> ColorModels { get; set; }

        public ColorModelPageViewModel()
        {
            _resourceLoader = ResourceLoader.GetForCurrentView();

            ColorModels = new ObservableCollection<ColorModelViewModelBase>
            {
                new RgbViewModel(),
                new XyzViewModel()
            };
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

            var file = await filePicker.PickSaveFileAsync();

            if (file != null)
            {
                using var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                await _image.ToStream(stream, BitmapEncoder.PngEncoderId);
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
            if (_image == null) return;
            _oldColorInt = color.ToInt();
            using var context = _image.GetBitmapContext();
            _chosenPixels = context.Pixels
                .Select((x, ind) => new Pixel(x, ind, context.Width))
                .Where(pixel => cropRect.Contains(pixel.Point) && pixel.Color == _oldColorInt)
                .Select(x => x.Index)
                .ToArray();
        }

        public void ClearChose()
        {
            _chosenPixels = null;
        }

        public void ChangeColorOfChosePixels(Color color)
        {
            if(_image == null) return;
            if(_chosenPixels == null) return;
            var colorInt = color.ToInt();
            using var context = _image.GetBitmapContext();
            foreach (var pixel in _chosenPixels)
            {
                _image.SetPixeli(pixel, colorInt);
            }
        }

        public void AdjustBrightness(int value, Rect cropRect)
        {
            if (_image == null) return;
            if (_imageCopy == null) return;
            using var context = _image.GetBitmapContext();
            var croppedBitmap = _imageCopy.Crop(cropRect);

            croppedBitmap = croppedBitmap.AdjustBrightness(value);
            _image.Blit(cropRect, croppedBitmap, new Rect(0, 0, cropRect.Width, cropRect.Height));
        }

        public void RevertChanges()
        {
            Image = _imageCopy;
            _chosenPixels = null;
        }

        public void ClearAll()
        {
            Image = null;
            _chosenPixels = null;
            _oldColorInt = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

