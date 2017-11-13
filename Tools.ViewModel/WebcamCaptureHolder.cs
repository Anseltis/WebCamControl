using System;

namespace ESystems.WebCamControl.Tools.ViewModel
{
    public interface IWebcamCaptureHolder
    {
        event EventHandler<WebcamCaptureEventArg> OnStopCapture;

        event EventHandler<WebcamCaptureEventArg> OnStartCapture;
    }
}
