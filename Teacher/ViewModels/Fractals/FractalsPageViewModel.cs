using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Fractals;
using CGTeacherShared.Fractals.Abstract;
using Microsoft.Graphics.Canvas;
using Teacher.ViewModels.AffinisTransformations;

namespace Teacher.ViewModels.Fractals
{
    public class FractalsPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FractalViewModel> Fractals { get; }

        private FractalViewModel _currentFractal;
        private readonly ResourceLoader _resourceLoader;
        private CancellationTokenSource _cancellationTokenSource;

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
            _cancellationTokenSource = new CancellationTokenSource();
            Transformation = new TransformationViewModel(new Transformation());

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

            Transformation.PropertyChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Transformation));
            };
        }

        public TransformationViewModel Transformation { get; }

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
            CancelRendering();
            _cancellationTokenSource = new CancellationTokenSource();
            _currentFractal.StartRendering(Transformation, width, height, Dpi, _cancellationTokenSource.Token);
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

        public void CancelRendering()
        {
            if (IsRendering)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                IsRendering = false;
            }
        }
    }
}
