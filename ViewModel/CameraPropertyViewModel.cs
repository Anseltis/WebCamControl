using ESystems.WebCamControl.Model;
using ESystems.WebCamControl.Tools.ViewModel;

namespace ESystems.WebCamControl.ViewModel
{

    /// <summary>
    /// Camera property class (view model).
    /// </summary>
    public class CameraPropertyViewModel : BaseViewModel
    {
        private int _value;
        private bool _auto;

        public bool Auto
        {
            get => _auto;
            set => SetField(ref _auto, value);
        }

        public int Value
        {
            get => _value;
            set => SetField(ref _value, value);
        }

        public string Name { get; }
        public bool AutoEnabled { get; }
        public bool Enabled { get; }
        public int Maximum { get; }
        public int Minimum { get; }
        public int Step { get; }

        public bool ManualEnabled => !Auto;

        public CameraPropertyViewModel(CameraProperty cameraProperty)
        {
            Name = cameraProperty.PropertyType.ToString();
            Minimum = cameraProperty.Minimum;
            Maximum = cameraProperty.Maximun;
            Enabled = cameraProperty.Enabled;
            Step = cameraProperty.Step;
            AutoEnabled = cameraProperty.AutoEnabled;
            Value = cameraProperty.Value;
            Auto = cameraProperty.Auto;

            this.SetPropertyChanged(nameof(Auto), () => OnPropertyChanged(nameof(ManualEnabled)));
        }

        
    }
}
