using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using ESystems.WebCamControl.Model;
using ESystems.WebCamControl.Tools.Model;

namespace ESystems.WebCamControl.ViewModel.StoreManager
{
    public class CameraOptionStoreManager
    {
        public string FileName => "Controls.config";

        public IEnumerable<CameraControl> GetState()
        {
            try
            {
                var document = XDocument.Load(FileName);
                return document.XPathSelectElements("//Control")
                    .Select(element => new CameraControl(
                        cameraName: (string) element.Element("CameraName"),
                        cameraProperty: (CameraPropertyType) Enum.Parse(
                            typeof(CameraPropertyType),
                            (string) element.Element("Property")),
                        cameraAction: (CameraAction) Enum.Parse(
                            typeof(CameraAction),
                            (string) element.Element("Action")),
                        shortcut: new Shortcut(
                            keyCode: (string) element.XPathSelectElement("Shortcut/KeyCode"),
                            alt: element.XPathSelectElements("Shortcut/Alt").Any(),
                            shift: element.XPathSelectElements("Shortcut/Shift").Any(),
                            ctrl: element.XPathSelectElements("Shortcut/Ctrl").Any())));
            }
            catch
            {
                return Enumerable.Empty<CameraControl>();
            }
        }
    }
}
