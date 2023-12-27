using Metronomer.Utils;
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
        static bool isPlaying = false;
        private MetronomeEngine _metronomeEngine = new MetronomeEngine();

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
                    UpdateNoteDivision(1);
                    break;
                case "1/8th Notes":
                    EighthNote.Visibility = Visibility.Visible;
                    UpdateNoteDivision(2);
                    break;
                case "1/16th Notes":
                    SixTeenthNote.Visibility = Visibility.Visible;
                    UpdateNoteDivision(4);
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
                bpm = Math.Min(bpm + 1, 999); //If smaller then 999 (Limit)
                Bpm_TextBox.Text = bpm.ToString(); //Change the text in the bpm box
            }
            changeBpm(int.Parse(Bpm_TextBox.Text)); //Then change in the actual bpm veriable
        }

        private void ButtonMinusBpm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Bpm_TextBox.Text, out int bpm))
            {
                bpm = Math.Max(bpm - 1, 1); 
                Bpm_TextBox.Text = bpm.ToString();
            }
            changeBpm(int.Parse(Bpm_TextBox.Text));
        }

        private void ButtonPlus5Bpm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Bpm_TextBox.Text, out int bpm))
            {
                bpm = Math.Min(bpm + 5, 999);
                Bpm_TextBox.Text = (bpm).ToString();
                changeBpm(int.Parse(Bpm_TextBox.Text));
            }
        }

        private void ButtonMinus5Bpm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Bpm_TextBox.Text, out int bpm))
            {
                bpm = Math.Max(bpm - 5, 1);
                Bpm_TextBox.Text = (bpm).ToString();
                changeBpm(int.Parse(Bpm_TextBox.Text));
            }

        }

        private void changeBpm(int newBpm)
        {
            _metronomeEngine.SetBpm(newBpm);
        }
        private void UpdateNoteDivision(int division)
        {
            _metronomeEngine.SetNoteDivision(division);
        }
        private void ToggleImage_OnClick(object sender, MouseButtonEventArgs e)
        {
            // Toggle the state
            isPlaying = !isPlaying;

            // Determine the new image path
            string imagePath = isPlaying ? "Images/Buttons/Pause.png" : "Images/Buttons/Resume.png";

            // Update the image source
            toggleImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            //Update the metronome
            if (isPlaying)
            {
                _metronomeEngine.Start();
            }
            else
            {
                _metronomeEngine.Stop();
            }
        }
    }
}
