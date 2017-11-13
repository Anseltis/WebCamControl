using ESystems.WebCamControl.Model;
using ESystems.WebCamControl.Tools.ViewModel;

namespace ESystems.WebCamControl.ViewModel
{
    /// <summary>
    /// Class with information about a camera.
    /// </summary>
    public class CameraViewModel : BaseViewModel
    {
        private bool _capture = false;
        private readonly Camera _camera;

        /// <summary> 
        /// Get camera friendly name 
        /// </summary>
        public string Name => _camera.Name;

        /// <summary>
        /// Get hardware camera name
        /// </summary>
        public string DevicePath => _camera.DevicePath;

        public bool Capture
        {
            get => _capture;
            set => SetField(ref _capture, value);
        }
        /// <summary> Initializes a new instance of the <see cref="CameraViewModel"/> class.
        /// </summary>
        /// <param name="camera">A camera model</param>
        public CameraViewModel(Camera camera)
        {
            _camera = camera;
        }
    }
}