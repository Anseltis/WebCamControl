using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ESystems.WebCamControl.Tools.View.Behaviors
{
    public class AutoFitInWindowBehavior: Behavior<ToggleButton>
    {
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }

        /// <summary>
        /// Attach behavior.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObjectOnClick;
        }

        /// <summary>
        /// Dettach behavior.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= AssociatedObjectOnClick;
        }

        DependencyObject GetTopLevelControl(DependencyObject control)
        {
            DependencyObject tmp = control;
            DependencyObject parent = null;
            while ((tmp = VisualTreeHelper.GetParent(tmp)) != null)
            {
                parent = tmp;
            }
            return parent;
        }

        private void AssociatedObjectOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var window = (Window)GetTopLevelControl(AssociatedObject);
            if (AssociatedObject.IsChecked == true)
            {
                window.SizeToContent = SizeToContent.Manual;
                window.Width = MaxWidth;
            }
            else
            {
                window.Width = MinWidth;
                window.SizeToContent = SizeToContent.Width;
            }
        }

    }
}
