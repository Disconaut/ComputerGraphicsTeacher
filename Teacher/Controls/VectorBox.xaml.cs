using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.NumberFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CGTeacherShared.Shared.Vector;
using Teacher.Annotations;
using Teacher.Controls.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Teacher.Controls
{
    public sealed partial class VectorBox : UserControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(string), typeof(VectorBox), new PropertyMetadata(default(string)));

        public string Header
        {
            get => (string) GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty VectorProperty = DependencyProperty.Register(
            "Vector", typeof(ObservableVector2), typeof(VectorBox), new PropertyMetadata(default));

        //private static void VectorChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (d is VectorBox vectorBox)
        //    {
        //        vectorBox.VectorViewModel = new Vector2ViewModel((Vector2) e.NewValue);
        //        vectorBox.VectorViewModel.PropertyChanged += vectorBox.VectorViewModelOnPropertyChanged;
        //    }
        //}

        //private void VectorViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(VectorViewModel.X) || e.PropertyName == nameof(VectorViewModel.Y))
        //    {
        //        Vector = new Vector2(VectorViewModel.X, VectorViewModel.Y);
        //    }
        //}

        public ObservableVector2 Vector
        {
            get => (ObservableVector2) GetValue(VectorProperty);
            set => SetValue(VectorProperty, value);
        }

        public Vector2ViewModel VectorViewModel { get; private set; }

        public VectorBox()
        {
            this.InitializeComponent();

            var numFormatter = new DecimalFormatter
            {
                FractionDigits = 0
            };

            XBox.NumberFormatter = numFormatter;
            YBox.NumberFormatter = numFormatter;
        }
    }
}
