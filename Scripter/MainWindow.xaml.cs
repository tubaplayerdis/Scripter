using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Data_Management;

namespace Scripter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //LocalData.InitRuntimeData();
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            Engine_Work.Startup.Init();
        }

        private void Single_File_Mode_Button_Click(object sender, RoutedEventArgs e)
        {
            Views.EditorWindow editorWindow = new Views.EditorWindow();
            editorWindow.Show();
            this.Close();
        }
    }
}
