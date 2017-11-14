using System;
using System.Windows.Input;
using System.Xml.Linq;
using ESystems.WebCamControl.Model;
using ESystems.WebCamControl.Tools.ViewModel;

namespace ESystems.WebCamControl.ViewModel
{
    /// <summary>
    /// Class with information about a camera.
    /// </summary>
    public class CameraViewModel : BaseViewModel
    {
        private readonly Camera _camera;
        private readonly CameraProvider _cameraProvider;
        private readonly Lazy<CameraPropertyViewModel> _focusLazy;
        private readonly Lazy<CameraPropertyViewModel> _exposureLazy;
        private readonly Lazy<CameraPropertyViewModel> _irisLazy;
        private readonly Lazy<CameraPropertyViewModel> _panLazy;
        private readonly Lazy<CameraPropertyViewModel> _rollLazy;
        private readonly Lazy<CameraPropertyViewModel> _tiltLazy;
        private readonly Lazy<CameraPropertyViewModel> _zoomLazy;

        private bool _capture;

        /// <summary> 
        /// Get camera friendly name 
        /// </summary>
        public string Name => _camera.Name;

        /// <summary>
        /// Get hardware camera name
        /// </summary>
        public string DevicePath => _camera.DevicePath;

        /// <summary>
        /// Gets whenever camera is in capture video
        /// </summary>
        public bool Capture
        {
            get => _capture;
            set => SetField(ref _capture, value);
        }

        public CameraPropertyViewModel Focus => _focusLazy.Value;
        public CameraPropertyViewModel Exposure => _exposureLazy.Value;
        public CameraPropertyViewModel Iris => _irisLazy.Value;
        public CameraPropertyViewModel Pan => _panLazy.Value;
        public CameraPropertyViewModel Roll => _rollLazy.Value;
        public CameraPropertyViewModel Tilt => _tiltLazy.Value;
        public CameraPropertyViewModel Zoom => _zoomLazy.Value;

        public ICommand RestoreCommand { get; }
        public ICommand SaveCommand { get; }

        /// <summary> Initializes a new instance of the <see cref="CameraViewModel"/> class.
        /// </summary>
        /// <param name="camera">A camera model</param>
        /// <param name="cameraProvider">A camera list discovery</param>
        /// <param name="commandFactory">A factory to create command instance</param>
        public CameraViewModel(Camera camera, CameraProvider cameraProvider, ICommandFactory commandFactory)
        {
            _camera = camera;
            _cameraProvider = cameraProvider;
            _focusLazy = new Lazy<CameraPropertyViewModel>(() => CreateCameraProperty(CameraPropertyType.Focus));
            _exposureLazy = new Lazy<CameraPropertyViewModel>(() => CreateCameraProperty(CameraPropertyType.Exposure));
            _irisLazy = new Lazy<CameraPropertyViewModel>(() => CreateCameraProperty(CameraPropertyType.Iris));
            _panLazy = new Lazy<CameraPropertyViewModel>(() => CreateCameraProperty(CameraPropertyType.Pan));
            _rollLazy = new Lazy<CameraPropertyViewModel>(() => CreateCameraProperty(CameraPropertyType.Roll));
            _tiltLazy = new Lazy<CameraPropertyViewModel>(() => CreateCameraProperty(CameraPropertyType.Tilt));
            _zoomLazy = new Lazy<CameraPropertyViewModel>(() => CreateCameraProperty(CameraPropertyType.Zoom));

            SaveCommand = commandFactory.CreateCommand(Save);
            RestoreCommand = commandFactory.CreateCommand(Restore);
        }

        public XElement GetState()
        {
            return new XElement("root",
                new XElement("Name", _camera.Name),
                new XElement("DevicePath", _camera.DevicePath),
                new XElement("Properties"), 
                    new[]
                    {
                        GetPropertyState(Focus),
                        GetPropertyState(Iris),
                        GetPropertyState(Exposure),
                        GetPropertyState(Pan),
                        GetPropertyState(Roll),
                        GetPropertyState(Tilt),
                        GetPropertyState(Zoom)
                    });
        }

        public XElement GetPropertyState(CameraPropertyViewModel property)
        {
            return new XElement("Property",
                new XElement("Name", property.Name),
                new XElement("Auto", property.Auto),
                new XElement("Value", property.Value));
        }

        private CameraPropertyViewModel CreateCameraProperty(CameraPropertyType propertyType)
        {
            var property = _cameraProvider.GetProperty(_camera, propertyType);
            var propertyViewModel = new CameraPropertyViewModel(property);
            propertyViewModel.SetPropertyChanged(
                new[] { nameof(propertyViewModel.Value), nameof(propertyViewModel.Auto) },
                () =>
                {
                    var setter = new CameraPropertySetter(propertyViewModel.Value, propertyViewModel.Auto);
                    _cameraProvider.SetProperty(_camera, propertyType, setter);
                });
            return propertyViewModel;
        }

        public void Save()
        {
        }

        public void Restore()
        {
        }
    }
}