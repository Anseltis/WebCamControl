using System.Windows;
using ESystems.WebCamControl.Model;
using ESystems.WebCamControl.Tools.View;
using ESystems.WebCamControl.ViewModel;

namespace ESystems.WebCamControl.Bootstrap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Startup implement.
        /// </summary>
        /// <param name="e">Startup arguments (command line parameters). </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainView = new MainWindow();
            mainView.Show();
            var viewModel = new WorkspaceViewModel(new CommandFactory(), new CameraProvider());
            mainView.DataContext = viewModel;
            viewModel.RefreshCameras();
        }
    }
}
