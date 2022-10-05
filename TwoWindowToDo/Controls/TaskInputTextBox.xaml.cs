using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TwoWindowToDo.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TwoWindowToDo.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskInputBox : UserControl
    {
        public MainPageViewModel ViewModel { get; }
        public TaskInputBox()
        {
            this.InitializeComponent();
            this.ViewModel = Ioc.Default.GetService<MainPageViewModel>();
        }

        private void SaveAndReset()
        {
            ViewModel.newTodoTitle = TaskInputTextBox.Text;
            ViewModel.AddTodo();
            TaskInputTextBox.Text = String.Empty;
        }

        private void TextBox_LosingFocus(UIElement sender, LosingFocusEventArgs args)
        {
            SaveAndReset();
        }

        private void TaskInputTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter) SaveAndReset();
            if (e.Key == VirtualKey.Escape) TaskInputTextBox.Text = String.Empty; 
        }

        private void TaskInputTextBox_FocusEngaged(Control sender, FocusEngagedEventArgs args)
        {
            //TaskInputTextBox.Opacity = 1;
        }



    }
}
