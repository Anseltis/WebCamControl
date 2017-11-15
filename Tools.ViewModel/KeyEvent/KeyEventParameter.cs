namespace ESystems.WebCamControl.Tools.ViewModel.KeyEvent
{
    public class KeyEventParameter
    {
        public string PropertyName { get; }
        public string KeyCode { get; }

        public KeyEventParameter(string propertyName, string keyCode)
        {
            PropertyName = propertyName;
            KeyCode = keyCode;
        }
    }
}
