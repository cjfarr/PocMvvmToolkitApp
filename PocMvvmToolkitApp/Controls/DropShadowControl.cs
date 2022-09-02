namespace PocMvvmToolkitApp.Controls
{
    using System.Xml.Linq;
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Hosting;
    using Windows.UI.Composition;

    [TemplatePart(Name = "PART_DropShadowCanvas", Type = typeof(Canvas))]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    public class DropShadowControl :ContentControl
    {
        public bool LightShadow { get; set; }


        private Canvas canvas;
        private ContentPresenter contentPresenter;
        private FrameworkElement content;
        public DropShadowControl()
        {
            Style = Application.Current.Resources["DropShadowStyle"] as Style;

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            canvas = GetTemplateChild("PART_DropShadowCanvas") as Canvas;
            contentPresenter = GetTemplateChild("PART_ContentPresenter") as ContentPresenter;
            content = contentPresenter.Content as FrameworkElement;

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (content != null)
            {
                content.SizeChanged += Content_SizeChanged;
            }
            ResetDropShadow();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (content != null)
            {
                content.SizeChanged -= Content_SizeChanged;
            }
        }

        private void Content_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResetDropShadow();
        }

        private void ResetDropShadow()
        {
            if (canvas == null) return;

            if (content == null) return;

            var _compositor = ElementCompositionPreview.GetElementVisual(canvas).Compositor;


            float width = (float)content.ActualWidth;
            float height = (float)content.ActualHeight;

            var myVisual = _compositor.CreateSpriteVisual();
            myVisual.Offset = new System.Numerics.Vector3(width / -2, height / -2, 0);
            myVisual.Brush = _compositor.CreateColorBrush(Windows.UI.Colors.Transparent);
            myVisual.Size = new System.Numerics.Vector2(width, height);

            var shadow = _compositor.CreateDropShadow();
            shadow.BlurRadius = 15;
            shadow.Color = LightShadow ? Windows.UI.Color.FromArgb(100, 80, 80, 80) : Windows.UI.Color.FromArgb(200, 20, 20, 20);

            myVisual.Shadow = shadow;

            ElementCompositionPreview.SetElementChildVisual(canvas, myVisual);
        }
    }

    public class LightDropShadowControl : DropShadowControl
    {
        public LightDropShadowControl()
        {
            this.LightShadow = true;
        }
    }
}
