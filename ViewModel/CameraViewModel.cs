using System;
using System.Collections.Generic;
using System.Windows.Input;
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
        private readonly Lazy<IEnumerable<CameraPropertyViewModel>> _propertiesLazy;

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

        public IEnumerable<CameraPropertyViewModel> Properties => _propertiesLazy.Value;

        public ICommand RestoreCommand { get; }
        public ICommand SaveCommand { get; }

        /// <summary> Initializes a new instance of the <see cref="CameraViewModel"/> class.
        /// </summary>
        /// <param name="camera">A camera model</param>
        /// <param name="cameraProvider">A camera list discovery</param>
        /// <param name="commandFactory">A factory to create command instance</param>
        /// <param name="storeManager">Store manager to save data</param>
        public CameraViewModel(
            Camera camera, 
            CameraProvider cameraProvider, 
            ICommandFactory commandFactory,
            CameraStoreManager storeManager)
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

            _propertiesLazy = new Lazy<IEnumerable<CameraPropertyViewModel>>(
                () => new[] { Focus, Exposure, Iris, Pan, Roll, Tilt, Zoom });

            SaveCommand = commandFactory.CreateCommand(() => storeManager.Save(this));
            RestoreCommand = commandFactory.CreateCommand(() => storeManager.Restore(this));
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
    }
}