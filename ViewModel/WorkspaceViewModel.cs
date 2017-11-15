using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ESystems.WebCamControl.Model;
using ESystems.WebCamControl.Tools.ViewModel;
using ESystems.WebCamControl.Tools.ViewModel.KeyEvent;
using ESystems.WebCamControl.Tools.ViewModel.WebCam;

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
        private bool _showVideo;

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
        /// Gets or sets information about video box visibility.
        /// </summary>
        public bool ShowVideo
        {
            get => _showVideo;
            set => SetField(ref _showVideo, value);
        }

        public ICommand StartCaptureCommand { get; }
        public ICommand StopCaptureCommand { get; }

        public event EventHandler<WebcamCaptureEventArg> OnStopCapture;
        public event EventHandler<WebcamCaptureEventArg> OnStartCapture;

        public CameraPropertyViewModel Focus => CameraSelectedItem?.Focus;
        public CameraPropertyViewModel Exposure => CameraSelectedItem?.Exposure;
        public CameraPropertyViewModel Iris => CameraSelectedItem?.Iris;
        public CameraPropertyViewModel Pan => CameraSelectedItem?.Pan;
        public CameraPropertyViewModel Roll => CameraSelectedItem?.Roll;
        public CameraPropertyViewModel Tilt => CameraSelectedItem?.Tilt;
        public CameraPropertyViewModel Zoom => CameraSelectedItem?.Zoom;

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
        /// Gets global keydown action
        /// </summary>
        public ICommand KeyDownCommand { get; }


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
            KeyDownCommand = commandFactory.CreateCommand<KeyEventParameter>(GlobalKeyDown);

            this
                .SetPropertyChanged(nameof(CameraSelectedIndex), () => OnPropertyChanged(nameof(CameraSelectedItem)))
                .SetPropertyChanged(nameof(CameraSelectedItem), () =>
                {
                    foreach (var camera in _cameras)
                    {
                        camera.Capture = false;
                    }
                })
                .SetPropertyChanged(nameof(ShowVideo), () =>
                {
                    if (CameraSelectedItem != null)
                    {
                        CameraSelectedItem.Capture = ShowVideo;
                    }
                });

            StartCaptureCommand = commandFactory.CreateCommand<CameraViewModel>(
                camera => camera.Capture = true, 
                camera => camera?.Capture == false);
            StopCaptureCommand = commandFactory.CreateCommand<CameraViewModel>(
                camera => camera.Capture = false,
                camera => camera?.Capture == true);
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

            CameraSelectedIndex = -1;
            Cameras = NoCameras;            
            Cameras = _cameraProvider.GetList()
                .Select(camera => new CameraViewModel(camera, _cameraProvider, _commandFactory, new CameraStoreManager(camera)))
                .ToList()
                .AsReadOnly();

            foreach (var camera in Cameras)
            {
                camera.SetPropertyChanged(nameof(camera.Capture), () =>
                {
                    if (camera.Capture)
                    {
                        OnStartCapture?.Invoke(this, new WebcamCaptureEventArg(camera.Name));
                    }
                    else
                    {
                        OnStopCapture?.Invoke(this, new WebcamCaptureEventArg(camera.Name));
                    }
                });
            }

            if (Cameras.Any())
            {
                CameraSelectedIndex = 0;
            }
        }

        private void GlobalKeyDown(KeyEventParameter keyEventParameter)
        {
            if (CameraSelectedItem == null || !CameraSelectedItem.Focus.Enabled)
            {
                return;
            }

            var property = CameraSelectedItem.Properties
                .FirstOrDefault(item => item.Name == keyEventParameter.PropertyName);
            if (property == null)
            {
                return;
            }

            try
            {
                ChangeProperty(property, keyEventParameter.KeyCode);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }

        }

        private static void ChangeProperty(CameraPropertyViewModel property, string keyCode)
        {
            switch (keyCode)
            {
                case "Add":
                    property.Auto = false;
                    property.Value++;
                    break;
                case "Subtract":
                    property.Auto = false;
                    property.Value--;
                    break;
            }
        }
    }
}
