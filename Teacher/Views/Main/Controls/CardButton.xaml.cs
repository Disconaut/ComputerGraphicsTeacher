using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Teacher.Controls.Enums;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Teacher.Views.Main.Controls
{
    public sealed partial class CardButton : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(CardButton), new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", typeof(ImageSource), typeof(CardButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource ImageSource
        {
            get => (ImageSource) GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty TextPositionProperty = DependencyProperty.Register(
            "TextPosition", typeof(CardButtonTextPosition), typeof(CardButton), new PropertyMetadata(default(CardButtonTextPosition)));

        public CardButtonTextPosition TextPosition
        {
            get => (CardButtonTextPosition) GetValue(TextPositionProperty);
            set => SetValue(TextPositionProperty, value);
        }

        public VerticalAlignment TextPositionCast 
            => (VerticalAlignment) TextPosition;

        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
            "TextAlignment", typeof(TextAlignment), typeof(CardButton), new PropertyMetadata(default(TextAlignment)));

        public TextAlignment TextAlignment
        {
            get => (TextAlignment) GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        public static readonly DependencyProperty TextBackgroundProperty = DependencyProperty.Register(
            "TextBackground", typeof(Color), typeof(CardButton), new PropertyMetadata(default(Color)));

        public Color TextBackground
        {
            get => (Color) GetValue(TextBackgroundProperty);
            set => SetValue(TextBackgroundProperty, value);
        }

        public CardButton()
        {
            this.InitializeComponent();
            RegisterPropertyChangedCallback(TextAlignmentProperty, CaptionChangedCallback);
            RegisterPropertyChangedCallback(TextProperty, CaptionChangedCallback);
            RegisterPropertyChangedCallback(TextBackgroundProperty, CaptionChangedCallback);
        }

        private void CaptionChangedCallback(DependencyObject sender, DependencyProperty dp)
        {
            var gradientFill = 0.27;
            var textAlign = TextAlignment;

            var fillBrush = new LinearGradientBrush();
            if (textAlign == TextAlignment.Center)
            {
                fillBrush.StartPoint = new Point(0, 0.5);
                fillBrush.EndPoint = new Point(1, 0.5);
                var transparentZone = 1 - gradientFill;
                fillBrush.GradientStops.Add(
                    new GradientStop
                    {
                        Color = Colors.Transparent,
                        Offset = transparentZone / 2
                    });
                fillBrush.GradientStops.Add(new GradientStop
                {
                    Color = TextBackground,
                    Offset = 0.5
                });
                fillBrush.GradientStops.Add(new GradientStop
                {
                    Color = Colors.Transparent,
                    Offset = 1 - transparentZone / 2
                });
            }
            else
            {
                if (textAlign == TextAlignment.Right)
                {
                    fillBrush.StartPoint = new Point(1 - gradientFill, 0.5);
                    fillBrush.EndPoint = new Point(0, 0.5);
                }
                else
                {
                    fillBrush.StartPoint = new Point(gradientFill, 0.5);
                    fillBrush.EndPoint = new Point(1, 0.5);
                }

                fillBrush.GradientStops.Add(new GradientStop
                {
                    Color = TextBackground,
                    Offset = 0
                });
                fillBrush.GradientStops.Add(new GradientStop
                {
                    Offset = 1
                });
            }

            CaptionBackground.Background = fillBrush;
        }
    }
}
