using ESystems.WebCamControl.Tools.Model;

namespace ESystems.WebCamControl.Model
{
    public class CameraControl
    {
        public string CameraName { get; }
        public CameraPropertyType CameraProperty { get; }
        public CameraAction CameraAction { get; }
        public Shortcut Shortcut { get; }

        public CameraControl(string cameraName, CameraPropertyType cameraProperty, CameraAction cameraAction, Shortcut shortcut)
        {
            CameraName = cameraName;
            CameraAction = cameraAction;
            CameraProperty = cameraProperty;
            Shortcut = shortcut;
        }
    }
}
