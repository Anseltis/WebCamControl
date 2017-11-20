using System;

namespace ESystems.WebCamControl.Tools.Model
{
    public class Shortcut
    {
        private readonly Tuple<string, bool, bool, bool> _tuple;
        public bool Shift { get; }
        public bool Alt { get; }
        public bool Ctrl { get; }
        public string KeyCode { get; }

        public Shortcut(string keyCode, bool shift = false, bool alt = false, bool ctrl = false)
        {
            KeyCode = keyCode;
            Shift = shift;
            Alt = alt;
            Ctrl = ctrl;
            _tuple = Tuple.Create(keyCode, shift, alt, ctrl);
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
