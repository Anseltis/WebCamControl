using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using ESystems.WebCamControl.Tools.Model;
using Gma.System.MouseKeyHook;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace ESystems.WebCamControl.Tools.View.Behaviors
{
    public class GlobalKeyEventBehavior : Behavior<Window>
    {
        /// <summary>
        /// Gets or sets command action
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Dependency property of <see cref="Command"/>.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(GlobalKeyEventBehavior),
                new PropertyMetadata(null));

        private IKeyboardMouseEvents _hook;

        protected override void OnAttached()
        {
            base.OnAttached();
            _hook = Hook.GlobalEvents();
            _hook.KeyDown += HookOnKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            _hook.KeyDown -= HookOnKeyDown;
        }

        private void HookOnKeyDown(object sender, KeyEventArgs args)
        {
            var parameter = new Shortcut(
                keyCode: args.KeyCode.ToString(),
                alt: args.Alt,
                ctrl: args.Control,
                shift: args.Shift);

            if (args.Control && args.Shift && Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }
        }
    }
}
