using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TaskMobileApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Models.TaskManager _taskManager = TaskMobileApp.App._taskManager;

       public MainPage()
        {
            this.InitializeComponent();
            _taskManager.RefreshTasksAsync();
            FlyoutAddTaskUserControl.TaskAdded += FlyoutAddTaskUserControl_TaskAdded;
        }

        private void FlyoutAddTaskUserControl_TaskAdded(object sender, Controls.TaskAddedEventArgs e)
        {
            _taskManager.AddTaskAsync(e.AddedTask);
        }

        private void AddTaskAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox s = sender as CheckBox;
            var taskId = s.Tag as int?;
            if(taskId >= 0)
            {
                _taskManager.CompleteTaskAsync(taskId.Value);
            }

        }
    }
}
