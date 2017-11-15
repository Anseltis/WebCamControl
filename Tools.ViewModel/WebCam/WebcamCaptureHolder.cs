using System;

namespace ESystems.WebCamControl.Tools.ViewModel.WebCam
{
    public interface IWebcamCaptureHolder
    {
        event EventHandler<WebcamCaptureEventArg> OnStopCapture;

        event EventHandler<WebcamCaptureEventArg> OnStartCapture;
    }
}
