namespace ESystems.WebCamControl.Model
{
    public class CameraPropertySetter
    {
        public int Value { get; }
        public bool Auto { get; }

        public CameraPropertySetter(int value, bool auto)
        {
            Value = value;
            Auto = auto;
        }
    }
}
