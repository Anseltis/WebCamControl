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
    }
}
