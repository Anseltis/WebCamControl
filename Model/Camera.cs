namespace ESystems.WebCamControl.Model
{
    /// <summary>
    /// Class with information about a camera.
    /// </summary>
    public class Camera
    {
        /// <summary> 
        /// Get camera friendly name 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get hardware camera name
        /// </summary>
        public string DevicePath { get; }

        /// <summary> Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="name">Camera friendly name</param>
        /// <param name="devicePath">Camera hardware name</param>
        public Camera(string name, string devicePath)
        {
            Name = name;
            DevicePath = devicePath;
        }
    }
}