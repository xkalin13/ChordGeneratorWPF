using Sanford.Multimedia.Midi.UI;
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
using System.Windows.Threading;

namespace ChordGeneratorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Generator generator = new Generator();
        Player player = new Player(generator);

        public MainWindow()
        {
            InitializeComponent();

            Note.SetModifiers();
            Note.SetNoteDetails();
            Chord.SetProgressions();
            Chord.SetModes();
            GenerateChords();
        }
        private void GenerateChords_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("key: " + key.SelectedItem.ToString());
                Console.WriteLine("mode: " + mode.SelectedItem.ToString());
                Console.WriteLine("modificator: " + modificator.SelectedItem.ToString());
                Console.WriteLine("mood: " + mood.SelectedItem.ToString());
                GenerateChords();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void Play1_Click(object sender, EventArgs e)
        {
            PlayProgression(Player.PlayType.Alternative, 0);
        }

        private void Play2_Click(object sender, EventArgs e)
        {
            PlayProgression(Player.PlayType.Alternative, 1);
        }

        private void Play3_Click(object sender, EventArgs e)
        {
            PlayProgression(Player.PlayType.Alternative, 2);
        }
        private void PlayProgressionBtn_Click(object sender, EventArgs e)
        {
            PlayProgression(Player.PlayType.Main);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayMainChord(0);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayMainChord(1);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayMainChord(2);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayMainChord(3);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(0, 0);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(1, 0);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(2, 0);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(3, 0);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(0, 1);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(1, 1);

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(2, 1);
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(3, 1);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(0, 2);
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(1, 2);
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(2, 2);
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            // akord|progression
            PlayAlternativeChord(3, 2);
        }

        private void RandomizeBtn_Click(object sender, EventArgs e)
        {
            generator.RandomizeProgression();

            string[] progression = generator.GetProgressionInfo();

            key.SelectedValue = progression[0];
            modificator.SelectedValue = progression[1];
            mode.SelectedValue = progression[2];
            mood.SelectedValue = progression[3];

            Console.WriteLine(key.SelectedValue + "  " + modificator.SelectedValue + "  " + progression[2] + "  " + progression[3]);
        }
        private void Button17_Click(object sender, EventArgs e)
        {
            PlayKeyChord(0);
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            PlayKeyChord(1);
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            PlayKeyChord(2);
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            PlayKeyChord(3);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            PlayKeyChord(4);
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            PlayKeyChord(5);
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            PlayKeyChord(6);
        }
        private void PlayChord(int chordNumber, Player.PlayType playType, int alternativeProgressionId = 0)
        {

            player.PlayChords(playType, chordNumber, alternativeProgressionId);

        }
        private void PlayKeyChord(int chord)
        {
            PlayChord(chord, Player.PlayType.Key);
        }
        private void PlayMainChord(int chord)
        {
            PlayChord(chord, Player.PlayType.Main);
        }
        private void PlayAlternativeChord(int chord, int progression)
        {
            PlayChord(chord, Player.PlayType.Alternative, progression);
        }
        private void PlayProgression(Player.PlayType playType, int alternativeProgressionId = 0)
        {


            for (int i = 0; i < 4; i++)//akordy
            {

                PlayChord(i, playType, alternativeProgressionId);
                Delay(100);
            }
        }
    
        private void GenerateChords()
        {
            generator.GenerateChordProgression(key.Text, modificator.Text, mode.Text, mood.Text);
            SetChordNames();
        }
        private void SetChordNames()
        {
            //main
            button1.Content = generator.GetChordName(Player.PlayType.Main, 0);
            button2.Content = generator.GetChordName(Player.PlayType.Main, 1);
            button3.Content = generator.GetChordName(Player.PlayType.Main, 2);
            button4.Content = generator.GetChordName(Player.PlayType.Main, 3);

            //ALTERNATIVE

            //chord 1
            button5.Content = generator.GetChordName(Player.PlayType.Alternative, 0, 0);
            button6.Content = generator.GetChordName(Player.PlayType.Alternative, 1, 0);
            button7.Content = generator.GetChordName(Player.PlayType.Alternative, 2, 0);
            button8.Content = generator.GetChordName(Player.PlayType.Alternative, 3, 0);
            //chord 2
            button9.Content = generator.GetChordName(Player.PlayType.Alternative, 0, 1);
            button10.Content = generator.GetChordName(Player.PlayType.Alternative, 1, 1);
            button11.Content = generator.GetChordName(Player.PlayType.Alternative, 2, 1);
            button12.Content = generator.GetChordName(Player.PlayType.Alternative, 3, 1);
            //chord4
            button13.Content = generator.GetChordName(Player.PlayType.Alternative, 0, 2);
            button14.Content = generator.GetChordName(Player.PlayType.Alternative, 1, 2);
            button15.Content = generator.GetChordName(Player.PlayType.Alternative, 2, 2);
            button16.Content = generator.GetChordName(Player.PlayType.Alternative, 3, 2);


            //key chords

            button17.Content = generator.GetChordName(Player.PlayType.Key, 0);
            button18.Content = generator.GetChordName(Player.PlayType.Key, 1);
            button19.Content = generator.GetChordName(Player.PlayType.Key, 2);
            button20.Content = generator.GetChordName(Player.PlayType.Key, 3);
            button21.Content = generator.GetChordName(Player.PlayType.Key, 4);
            button22.Content = generator.GetChordName(Player.PlayType.Key, 5);
            button23.Content = generator.GetChordName(Player.PlayType.Key, 6);
        }

        private void PianoControl1_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            player.PianoControl1_PianoKeyDown(sender, e);
        }
        private void PianoControl1_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {
            player.PianoControl1_PianoKeyUp(sender, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public async void Delay(int delay)
        {
            await Task.Delay(delay);
        }

        private void Play1_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
