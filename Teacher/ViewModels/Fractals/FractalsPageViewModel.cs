using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
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

        public CanvasRenderTarget RenderTarget
        {
            get => _renderTarget;
            set
            {
                if(_renderTarget == value) return;

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
            _currentFractal.StartRendering(OffsetX, OffsetY, WidthScale, HeightScale, width, height, Dpi);
            IsRendering = true;
        }
    }
}
