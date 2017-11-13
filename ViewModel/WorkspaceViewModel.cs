using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ESystems.WebCamControl.Model;
using ESystems.WebCamControl.Tools.ViewModel;

namespace ESystems.WebCamControl.ViewModel
{
    /// <summary> 
    /// A class of camera list control.
    /// </summary>
    public class WorkspaceViewModel: BaseViewModel
    {
        private readonly CameraProvider _cameraProvider;

        private IReadOnlyList<CameraViewModel> _cameras = NoCameras;
        private static readonly IReadOnlyList<CameraViewModel> NoCameras = new List<CameraViewModel>().AsReadOnly();
        private int _cameraSelectedIndex;

        /// <summary> 
        /// Gets or sets cameras in the system. 
        /// </summary>
        public IReadOnlyList<CameraViewModel> Cameras
        {
            get => _cameras;
            private set => SetField(ref _cameras, value);
        }

        /// <summary>
        /// Gets or sets index of the current camera. 
        /// </summary>
        public int CameraSelectedIndex
        {
            get => _cameraSelectedIndex;
            set => SetField(ref _cameraSelectedIndex, value);
        }

        /// <summary>
        /// Gets a active camers instance.
        /// </summary>
        public CameraViewModel CameraViewModelSelectedItem => Cameras[CameraSelectedIndex];

        /// <summary>
        /// Gets Refresh cameras action
        /// </summary>
        public ICommand RefreshCameraCommand { get; }


        /// <summary>
        ///  Initializes a new instance of the <see cref="WorkspaceViewModel"/> class.
        /// </summary>
        public WorkspaceViewModel(ICommandFactory commandFactory, CameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            RefreshCameraCommand = commandFactory.CreateCommand(RefreshCameras);
        }
        
        /// <summary>
        /// Refreshes the camera list
        /// </summary>
        public void RefreshCameras()
        {
            Cameras = NoCameras;
            Cameras = _cameraProvider.GetList()
                .Select(camera => new CameraViewModel(camera))
                .ToList()
                .AsReadOnly();

            if (Cameras.Any())
            {
                CameraSelectedIndex = 0;
            }
        }
    }
}
