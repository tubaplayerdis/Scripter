using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;


namespace Scripter.Views
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public string? CurrentFile;
        public EditorWindow()
        {
            InitializeComponent();
            CurrentFile = null;
        }
        
        private void Execute_Button_Click(object sender, RoutedEventArgs e)
        {
            Engine_Work.Startup.Run(f.StringFromRichTextBox(EditorBox));
        }

        private void Go_To_Start_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void EditorBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int lineNumber;
            EditorBox.CaretPosition.GetLineStartPosition(-int.MaxValue, out lineNumber);
            int columnNumber = EditorBox.CaretPosition.GetLineStartPosition(0).GetOffsetToPosition(EditorBox.CaretPosition);
            if (lineNumber == 0)
                columnNumber--;

            
            Linecharbox.Text = $"Line: {-lineNumber + 1}, Char: {columnNumber + 1}";
            ISFileSavedView.IsChecked = false;
        }

        private void grid_Initialized(object sender, EventArgs e)
        {
            FileNameBlock.Text = "No File Slelected";
            Linecharbox.Text = "Line: 1, Char: 1";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditorBox.Cut();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            EditorBox.Copy();
        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            EditorBox.Paste();
        }

        private void NewFileButton_Click(object sender, RoutedEventArgs e)
        {
            if(ISFileSavedView.IsChecked == false)
            {
                if(MessageBox.Show("You have unsaved work, continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    EditorBox.Document.Blocks.Clear();
                    ISFileSavedView.IsChecked = false;
                }
            }
            
        }

        private void SaveCurrentFile()
        {
            
            try
            {
                if(CurrentFile == null)
                {
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.Filter = "JavaScript Files|*.js";
                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, f.StringFromRichTextBox(EditorBox));
                        try
                        {
                            //Data_Management.Data.FileLocations.Add(saveFileDialog.FileName);
                        } catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        
                        ISFileSavedView.IsChecked = true;
                    }
                }
                else
                {
                    File.WriteAllText(CurrentFile, f.StringFromRichTextBox(EditorBox));
                    ISFileSavedView.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                Engine_Work.Dialogs.ErrorBoxShow(ex.Message);
                ISFileSavedView.IsChecked = false;
            }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (ISFileSavedView.IsChecked == false)
            {
                if (MessageBox.Show("You have unsaved work, continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                    openFileDialog.Filter = "JavaScript Files|*.js";
                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        EditorBox.Document.Blocks.Clear();
                        EditorBox.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(openFileDialog.FileName))));
                        
                        ISFileSavedView.IsChecked = false;
                    }
                }
            }
            else
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "JavaScript Files|*.js";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    EditorBox.Document.Blocks.Clear();
                    EditorBox.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(openFileDialog.FileName))));
                    
                    ISFileSavedView.IsChecked = false;
                }
            }
           
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentFile();
        }
    }


    static class f
    {
        public static void SetStringNULLRichTextBox(RichTextBox richTextBox)
        {
            TextRange txt = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            txt.Text = "";
        }
        public static string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }
    }
    
}
