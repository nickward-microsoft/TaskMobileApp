using System;
using System.Collections.Generic;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TaskMobileApp.Controls
{
    public sealed partial class AddTaskUserControl : UserControl
    {
        public event EventHandler<TaskAddedEventArgs> TaskAdded;

        public AddTaskUserControl()
        {
            this.InitializeComponent();
        }

        private void AddTaskButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TaskAddedEventArgs args = new TaskAddedEventArgs();

            args.AddedTask = new Models.Task();
            args.AddedTask.TaskId = 0;
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                args.AddedTask.Name = "New Task";
            } else {
                args.AddedTask.Name = NameTextBox.Text;
            }
            args.AddedTask.Complete = false;

            if (TaskAdded != null)
            {
                TaskAdded(this, args);
            }
        }
    }

    public class TaskAddedEventArgs : EventArgs
    {
        public Models.Task AddedTask { get; set; }
    }
}
