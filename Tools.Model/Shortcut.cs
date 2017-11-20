namespace ESystems.WebCamControl.Tools.Model
{
    public class Shortcut
    {
        private readonly (string KeyCode, bool Shift, bool Alt, bool Ctrl) _tuple;

        public bool Shift => _tuple.Shift;
        public bool Alt => _tuple.Alt;
        public bool Ctrl => _tuple.Ctrl;
        public string KeyCode => _tuple.KeyCode;

        public Shortcut(string keyCode, bool shift = false, bool alt = false, bool ctrl = false)
        {
            _tuple = (KeyCode: keyCode, Shift: shift, Alt: alt, Ctrl: ctrl);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var shortcut = obj as Shortcut;
            if (shortcut == null)
            {
                return false;
            }

            return _tuple.Equals(shortcut._tuple);
        }

        public override int GetHashCode() => _tuple.GetHashCode();
    }
}
