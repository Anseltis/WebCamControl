namespace ESystems.WebCamControl.Model
{
    /// <summary>
    /// Class of the web cam property
    /// </summary>
    public class CameraProperty
    {
        /// <summary>
        /// Gets property type.
        /// </summary>
        public CameraPropertyType PropertyType { get; }

        /// <summary>
        /// Gets mminimum value.
        /// </summary>
        public int Minimum { get; }

        /// <summary>
        /// Gets maximum value.
        /// </summary>
        public int Maximun { get; }

        /// <summary>
        /// Gets current value of the property.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Gets value threshold.
        /// </summary>
        public int Step { get; }

        /// <summary>
        /// Gets default value.
        /// </summary>
        public int Default { get; }

        /// <summary>
        /// Gets auto mode
        /// </summary>
        public bool Auto { get; }

        /// <summary>
        /// Gets auto mode availability.
        /// </summary>
        public bool AutoEnabled { get; }

        /// <summary>
        /// Gets property availability
        /// </summary>
        public bool Enabled { get; }

        /// <summary>
        /// Initializes a new instance ofthe <see cref="CameraProperty"/>
        /// </summary>
        /// <param name="builder">A builder instance</param>
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

