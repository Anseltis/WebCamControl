using System;
using System.Collections.Generic;
using System.Linq;
using AForge.Video.DirectShow;

namespace ESystems.WebCamControl.Model
{
    /// <summary>
    /// This class provides a list of the cameras.
    /// </summary>
    public class CameraProvider
    {
        /// <summary>
        /// Returns a camera list 
        /// </summary>
        /// <returns>A list of the cameras.</returns>
        public IEnumerable<Camera> GetList()
        {
            return new FilterInfoCollection(FilterCategory.VideoInputDevice)
                .Cast<FilterInfo>()
                .Select(filterInfo => new Camera(filterInfo.Name, filterInfo.MonikerString));
        }

        /// <summary>
        /// Gets camera property
        /// </summary>
        /// <param name="camera">Camera type</param>
        /// <param name="propertyType">Property type</param>
        /// <returns>Camera property type</returns>
        public CameraProperty GetProperty(Camera camera, CameraPropertyType propertyType)
        {
            var type = (CameraControlProperty) Enum.Parse(typeof(CameraControlProperty), propertyType.ToString());
            var videoSource = new VideoCaptureDevice(camera.DevicePath);
            videoSource.GetCameraProperty(type, out var value, out CameraControlFlags flag);
            videoSource.GetCameraPropertyRange(type, out var min, out var max, out var step, out var def, out var flagdef);

            var builder = new CameraPropertyBuilder
            {
                PropertyType = propertyType,
                Maximun = max,
                Minimum = min,
                Value = value,
                Step = step,
                Default = def,
                Enabled = flagdef.HasFlag(CameraControlFlags.Manual) || flagdef.HasFlag(CameraControlFlags.Auto),
                AutoEnabled = flagdef.HasFlag(CameraControlFlags.Auto),
                Auto = flag.HasFlag(CameraControlFlags.Auto)
            };
            return new CameraProperty(builder);
        }

        /// <summary>
        /// Sets the camera property
        /// </summary>
        /// <param name="camera">Camera type</param>
        /// <param name="propertyType">Property type</param>
        /// <param name="setter">Property setter</param>
        public void SetProperty(Camera camera, CameraPropertyType propertyType, CameraPropertySetter setter)
        {
            var property = GetProperty(camera, propertyType);
            if (!property.Enabled)
            {
                return;
            }

            if (setter.Auto && !property.AutoEnabled)
            {
                return;
            }

            if (setter.Auto && property.Auto)
            {
                return;
            }

            var type = (CameraControlProperty)Enum.Parse(typeof(CameraControlProperty), propertyType.ToString());
            var videoSource = new VideoCaptureDevice(camera.DevicePath);

            if (setter.Auto)
            {
                videoSource.SetCameraProperty(type, property.Default, CameraControlFlags.Auto);
                return;
            }

            var value = setter.Value;
            if (value < property.Minimum)
            {
                value = property.Minimum;
            }

            if (value > property.Maximun)
            {
                value = property.Maximun;
            }

            if (property.Step > 1)
            {
                value = value - value % property.Step;
            }

            videoSource.SetCameraProperty(type, value, CameraControlFlags.Manual);
        }
    }
}
