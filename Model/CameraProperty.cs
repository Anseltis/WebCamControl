namespace ESystems.WebCamControl.Model
{
    public class CameraProperty
    {
        public CameraPropertyType PropertyType { get; }
        public int Minimum { get; }
        public int Maximun { get; }
        public int Value { get; }
        public int Step { get; }
        public int Default { get; }
        public bool Auto { get; }
        public bool AutoEnabled { get; }
        public bool Enabled { get; }

        public CameraProperty(CameraPropertyBuilder builder)
        {
            PropertyType = builder.PropertyType;
            Minimum = builder.Minimum;
            Maximun = builder.Maximun;
            Value = builder.Value;
            Step = builder.Step;
            Default = builder.Default;
            Auto = builder.Auto;
            AutoEnabled = builder.AutoEnabled;
            Enabled = builder.Enabled;
        }

        
    }
}

