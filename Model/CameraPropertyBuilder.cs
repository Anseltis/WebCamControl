namespace ESystems.WebCamControl.Model
{
    public class CameraPropertyBuilder
    {
        public CameraPropertyType PropertyType { get; set; }
        public int Minimum { get; set; }
        public int Maximun { get; set; }
        public int Value { get; set; }
        public int Step { get; set; }
        public int Default { get; set; }
        public bool Auto { get; set; }
        public bool AutoEnabled { get; set; }
        public bool Enabled { get; set; }
    }
}
