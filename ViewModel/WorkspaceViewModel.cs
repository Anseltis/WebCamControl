using System;
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
    public class WorkspaceViewModel: BaseViewModel, IWebcamCaptureHolder
    {
        private readonly CameraProvider _cameraProvider;
        private readonly ICommandFactory _commandFactory;

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


        public ICommand StartCaptureCommand { get; }
        public ICommand StopCaptureCommand { get; }

        /// <summary>
        /// Gets a active camers instance.
        /// </summary>
        public CameraViewModel CameraSelectedItem => CameraSelectedIndex >= 0 && CameraSelectedIndex < Cameras.Count
            ? Cameras[CameraSelectedIndex] : null;

        /// <summary>
        /// Gets Refresh cameras action
        /// </summary>
        public ICommand RefreshCameraCommand { get; }


        /// <summary>
        ///  Initializes a new instance of the <see cref="WorkspaceViewModel"/> class.
        /// </summary>
        /// <param name="cameraProvider">A camera list discovery</param>
        /// <param name="commandFactory">A factory to create command instance</param>
        public WorkspaceViewModel(CameraProvider cameraProvider, ICommandFactory commandFactory)
        {
            _cameraProvider = cameraProvider;
            _commandFactory = commandFactory;
            RefreshCameraCommand = commandFactory.CreateCommand(RefreshCameras);

            this
                .SetPropertyChanged(nameof(CameraSelectedIndex), () => OnPropertyChanged(nameof(CameraSelectedItem)))
                .SetPropertyChanged(nameof(CameraSelectedItem), () => 
                {
                    foreach (var camera in _cameras)
                    {
                        camera.Capture = false;
                    }
                });

            StartCaptureCommand = commandFactory.CreateCommand<CameraViewModel>(StartCapture);
            StopCaptureCommand = commandFactory.CreateCommand<CameraViewModel>(StopCapture);
        }

        /// <summary>
        /// Refreshes the camera list
        /// </summary>
        public void RefreshCameras()
        {
            if (CameraSelectedItem?.Capture == true)
            {
                CameraSelectedItem.Capture = false;    
            }

            Cameras = NoCameras;
            Cameras = _cameraProvider.GetList()
                .Select(camera => new CameraViewModel(camera, _cameraProvider, _commandFactory))
                .ToList()
                .AsReadOnly();

            foreach (var camera in Cameras)
            {
                camera.SetPropertyChanged(nameof(camera.Capture), () =>
                {
                    if (camera.Capture)
                    {
                        StartCapture(camera);
                    }
                    else
                    {
                        StopCapture(camera);
                    }
                });
            }

            if (Cameras.Any())
            {
                CameraSelectedIndex = 0;
            }
        }

        public void StartCapture(CameraViewModel camera)
        {
            OnStartCapture?.Invoke(this, new WebcamCaptureEventArg(camera.Name));
        }

        public void StopCapture(CameraViewModel camera)
        {
            OnStopCapture?.Invoke(this, new WebcamCaptureEventArg(camera.Name));
        }

        public event EventHandler<WebcamCaptureEventArg> OnStopCapture;
        public event EventHandler<WebcamCaptureEventArg> OnStartCapture;
    }
}
