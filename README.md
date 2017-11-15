# WebCam Control

This program allows you to modify native options of your webcam. This program is based on DirectX.Directshow extensions and doesn't have any driver-specific code. Changing options affect all applications which use the current webcam.

* [Camera options](#camera-options)
* [Program modules](#program-modules)
* [View](#view)
* [Used Tools](#used-tools)

# Camera options
| Option | Description |
| ------ | ------ |
| Focus | Focal distances supported by the camera |
| Pan | Panning angles supported by the camera |
| Iris | Aperture settings supported by the camera |
| Roll | Roll angles supported by the camera |
| Tilt | Tilt angles supported by the camera |
| Zoom | Zoom levels supported by the camera |
| Exposure | Exposure times supported by the camera |

# Program modules

| Module name | Description |
| ------ | ------ |
| Boostrap | Running module of this MVVM WPF aplication |
| View | Xaml-controls (presentation view) |
| ViewModel | Presenter layer module |
| Model | Domain logics |
| Tools.ViewModel | Resuable presenter logics |
| Tools.View | Resuable view logics |

# View

![Collapsed Preview](/Collapsed.png)
![Preview](/Preview.png)

| Part | Description |
| ------ | ------ |
| Combobox | List of accessable cameras |
| Refresh button | Allows you to refresh list of accessable cameras  |
| Show Toggle Button | Expands/collapses camera preview video |
| Start Button | Plays the camera video |
| Stop Button | Stops the camera video |
| Save Button | Saves camera options into a local xml file |
| Restore Button | Restores camera options into a local xml file |
| Ctrl + Shift + Numpad+ | Increases focus value |
| Ctrl + Shift + Numpad- | Decreases focus value |
| Restore Button | Restores camera options from a local xml file |

# Used Tools
| Tool | Description |
| ------ | ------ |
| Visual Studio 2017 | Lastest version of Visual Studio |
| .Net Framework 4.6.1 | Lastes version of framework with the support of WPF |
| Windows Presentation Foundation | Windows Desktop Presentation Framework |
| WebEye.Controls.Wpf.WebCameraControl | WPF control to webcam video capture |
| AForge.Video.DirectShow | Library to work with the DirectX.DirectShow|
| MouseKeyHook | Library to intercept global key events |

