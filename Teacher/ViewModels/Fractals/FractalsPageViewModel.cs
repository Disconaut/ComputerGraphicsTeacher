using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using CGTeacherShared.Fractals;
using CGTeacherShared.Fractals.Abstract;
using Microsoft.Graphics.Canvas;

namespace Teacher.ViewModels.Fractals
{
    public class FractalsPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FractalViewModel> Fractals { get; }

        private FractalViewModel _currentFractal;
        private ResourceLoader _resourceLoader;
        public FractalViewModel CurrentFractal
        {
            get => _currentFractal;
            set
            {
                if (_currentFractal == value) return;

                _currentFractal = value;
                OnPropertyChanged(nameof(CurrentFractal));
            }
        }

        private CanvasRenderTarget _renderTarget;
        private bool _isRendering;
        private float _offsetX;
        private float _offsetY;
        private float _widthScale;
        private float _heightScale;
        private float _rotateAngle;

        public CanvasRenderTarget RenderTarget
        {
            get => _renderTarget;
            set
            {
                if (_renderTarget == value) return;

                _renderTarget = value;
                OnPropertyChanged(nameof(RenderTarget));
            }
        }

        public bool IsRendering
        {
            get => _isRendering;
            set
            {
                if (_isRendering != value)
                {
                    _isRendering = value;
                    OnPropertyChanged(nameof(IsRendering));
                }
            }
        }

        public FractalsPageViewModel()
        {
            _resourceLoader = ResourceLoader.GetForCurrentView();

            Fractals = new ObservableCollection<FractalViewModel>
            {
                new FractalViewModel(new HHDragonFractal()),
                new FractalViewModel(new LeviFractal()),
                new FractalViewModel(new KohFractal()),
                new FractalViewModel(new MinkovskiFractal())
            };

            foreach (var fractal in Fractals)
            {
                fractal.RenderComplete += (sender, args) =>
                {
                    RenderTarget = args.RenderTarget;
                    IsRendering = false;
                };
            }
        }

        public float OffsetX
        {
            get => _offsetX;
            set
            {
                if (Math.Abs(_offsetX - value) < float.Epsilon) return;

                _offsetX = value;
                OnPropertyChanged(nameof(OffsetX));
            }
        }

        public float OffsetY
        {
            get => _offsetY;
            set
            {
                if (Math.Abs(_offsetY - value) < float.Epsilon) return;

                _offsetY = value;
                OnPropertyChanged(nameof(OffsetY));
            }
        }

        public float WidthScale
        {
            get => _widthScale;
            set
            {
                if (Math.Abs(_widthScale - value) < float.Epsilon) return;

                _widthScale = value;
                OnPropertyChanged(nameof(WidthScale));
            }
        }

        public float HeightScale
        {
            get => _heightScale;
            set
            {
                if (Math.Abs(_heightScale - value) < float.Epsilon) return;

                _heightScale = value;
                OnPropertyChanged(nameof(HeightScale));
            }
        }

        public float RotateAngle
        {
            get => _rotateAngle;
            set
            {
                if (Math.Abs(_rotateAngle - value) < float.Epsilon) return;

                _rotateAngle = value;
                OnPropertyChanged(nameof(RotateAngle));
            }
        }


        public float Dpi => 96;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            await CoreApplication
                .MainView
                .CoreWindow
                .Dispatcher
                .RunAsync(
                    CoreDispatcherPriority.Normal,
                    () => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }

        public void RenderCurrentFractal(float width, float height)
        {
            _currentFractal.StartRendering(OffsetX, OffsetY, WidthScale, HeightScale, width, height, Dpi, RotateAngle);
            IsRendering = true;
        }

        public async Task SaveFractalToImage()
        {
            var filePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                DefaultFileExtension = ".png",
                SuggestedFileName = _resourceLoader.GetString("SuggestedFractalFileName")
            };

            filePicker.FileTypeChoices.Add("PNG", new List<string> { ".png" });

            var file = await filePicker.PickSaveFileAsync();

            if (file != null)
            {
                using var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                await _renderTarget.SaveAsync(stream, CanvasBitmapFileFormat.Png, 1);
            }
        }
    }
}
