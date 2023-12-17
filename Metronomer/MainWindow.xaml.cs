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

namespace Metronomer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void changeMusicNote(string note)
        {
            collapseAllNotes();

            switch (note)
            {
                case "1/4th Notes":
                    QuaterNote.Visibility = Visibility.Visible;
                    break;
                case "1/8th Notes":
                    EighthNote.Visibility = Visibility.Visible;
                    break;
                case "1/16th Notes":
                    SixTeenthNote.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void collapseAllNotes()
        {
            QuaterNote.Visibility = Visibility.Collapsed;
            EighthNote.Visibility = Visibility.Collapsed;
            SixTeenthNote.Visibility = Visibility.Collapsed;
        }

        private void NoteTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ComboBox comboBox)) return;
            var selectedItem = comboBox.SelectedItem as ComboBoxItem;
            if (selectedItem == null) return;
            changeMusicNote(selectedItem.Content.ToString());
        }

        private void ButtonPlusBpm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Bpm_TextBox.Text, out int bpm))
            {
                bpm = Math.Min(bpm + 1, 999); 
                Bpm_TextBox.Text = bpm.ToString();
            }
        }

        private void ButtonMinusBpm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Bpm_TextBox.Text, out int bpm))
            {
                bpm = Math.Max(bpm - 1, 1); 
                Bpm_TextBox.Text = bpm.ToString();
            }
        }

        private void ButtonPlus5Bpm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Bpm_TextBox.Text, out int bpm))
            {
                bpm = Math.Min(bpm + 1, 999);
                for (int i = 0; i <= 5; i++)
                {
                    Bpm_TextBox.Text = (bpm+4).ToString();
                }
            }
        }

        private void ButtonMinus5Bpm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Bpm_TextBox.Text, out int bpm))
            {
                bpm = Math.Max(bpm - 1, 1);
                for (int i = 0; i <= 5; i++)
                {
                    Bpm_TextBox.Text = (bpm-4).ToString();
                }
            }
        }
    }
}
