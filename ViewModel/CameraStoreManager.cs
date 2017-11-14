using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using ESystems.WebCamControl.Model;

namespace ESystems.WebCamControl.ViewModel
{
    /// <summary>
    /// Class with store options responsibility.
    /// </summary>
    public class CameraStoreManager
    {
        private readonly Camera _camera;

        public string FileName => $"{_camera.Name}.xml";

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraPropertyViewModel"/>
        /// </summary>
        /// <param name="camera"></param>
        public CameraStoreManager(Camera camera)
        {
            _camera = camera;
        }

        /// <summary>
        /// Gets the current state
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public XElement GetState(CameraViewModel viewModel)
        {
            return new XElement("root",
                new XElement(nameof(_camera.Name), _camera.Name),
                new XElement(nameof(_camera.DevicePath), _camera.DevicePath),
                new XElement(nameof(viewModel.Properties)), viewModel.Properties.Select(property =>
                    new XElement("Property",
                        new XElement(nameof(property.Name), property.Name),
                        new XElement(nameof(property.Auto), property.Auto),
                        new XElement(nameof(property.Value), property.Value))));
        }

        /// <summary>
        /// Saves the current state into the file
        /// </summary>
        /// <param name="viewModel"></param>
        public void Save(CameraViewModel viewModel)
        {
            GetState(viewModel).Save(FileName);
        }

        /// <summary>
        /// Restores a state from the file
        /// </summary>
        /// <param name="viewModel"></param>
        public void Restore(CameraViewModel viewModel)
        {
            try
            {
                var document = XDocument.Load(FileName);
                foreach (var element in document.XPathSelectElements("//Property"))
                {
                    var name = (string)element.Element("Name");
                    var property = viewModel.Properties.Single(item => item.Name == name);

                    property.Auto = (bool)element.Element(nameof(property.Auto));
                    property.Value = (int)element.Element(nameof(property.Value));
                }
            }
            catch(Exception)
            {
                // Exception handling
            }
        }
    }
}
