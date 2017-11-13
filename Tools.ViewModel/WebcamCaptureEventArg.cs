namespace ESystems.WebCamControl.Tools.ViewModel
{
    public class WebcamCaptureEventArg
    {
        public string DeviceName { get; }

        public WebcamCaptureEventArg(string deviceName)
        {
            DeviceName = deviceName;
        }
    }
}
