using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using ESystems.WebCamControl.Tools.ViewModel;
using WebEye.Controls.Wpf;

namespace ESystems.WebCamControl.Tools.View.Behaviors
{
    public class WebcamCaptureBehavior : Behavior<WebCameraControl>
    {
        /// <summary>
        /// Gets or sets camera name
        /// </summary>
        public IWebcamCaptureHolder WebcamCaptureHolder
        {
            get => (IWebcamCaptureHolder)GetValue(WebcamCaptureHolderProperty);
            set => SetValue(WebcamCaptureHolderProperty, value);
        }

        /// <summary>
        /// Dependency property of <see cref="WebcamCaptureHolder"/>.
        /// </summary>
        public static readonly DependencyProperty WebcamCaptureHolderProperty =
            DependencyProperty.Register(
                nameof(WebcamCaptureHolder),
                typeof(IWebcamCaptureHolder),
                typeof(WebcamCaptureBehavior),
                new PropertyMetadata(
                    null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, 
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var webCameraControl = ((WebcamCaptureBehavior)dependencyObject).AssociatedObject;

            var webcamCaptureHolder = dependencyPropertyChangedEventArgs.NewValue as IWebcamCaptureHolder;
            if (webcamCaptureHolder != null)
            {
                webcamCaptureHolder.OnStartCapture += (sender, args) => StartCapture(webCameraControl, args);
                webcamCaptureHolder.OnStopCapture += (sender, args) => StopCapture(webCameraControl);
            }
        }

        /// <summary>
        /// Attach behavior.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            if (WebcamCaptureHolder != null)
            {
                WebcamCaptureHolder.OnStartCapture += (sender, args) => StartCapture(AssociatedObject, args);
                WebcamCaptureHolder.OnStopCapture += (sender, args) => StopCapture(AssociatedObject);
            }
        }

        private static void StartCapture(WebCameraControl webCameraControl, WebcamCaptureEventArg webcamCaptureEventArg)
        {
            if (webCameraControl.IsCapturing)
            {
                webCameraControl.StopCapture();
            }

            var webcam = webCameraControl.GetVideoCaptureDevices()
                .FirstOrDefault(item => item.Name == webcamCaptureEventArg.DeviceName);
            if (webcam == null)
            {
                return;
            }

            webCameraControl.StartCapture(webcam);
        }

        private static void StopCapture(WebCameraControl webCameraControl)
        {
            if (!webCameraControl.IsCapturing)
            {
                return;
            }

            webCameraControl.StopCapture();
        }
    }
}
