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

namespace MatchGame
{
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondEslapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpgame();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondEslapsed++;
            timeTextBlock.Text = (tenthsOfSecondEslapsed/10F).ToString("0.0s");
            if (matchesFound == 8) 
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play Again?";
            }
        }

        private void SetUpgame()
        {
            matchesFound = 0;
            List<string> animalEmoji = new List<string>()
            {
                "🐰","🐹","🐭","🐗","🐷","🐮","🦝","🦊",
                "🐰","🐹","🐭","🐗","🐷","🐮","🦝","🦊"
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != timeTextBlock.Name)
                {
                    int index = random.Next(0, animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                    textBlock.Visibility = Visibility.Visible;
                }
            }

            timer.Start();
            tenthsOfSecondEslapsed = 0;
            matchesFound = 0;
        }

        public bool findingMatch;
        TextBlock lastClickedTextBlock;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastClickedTextBlock = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastClickedTextBlock.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastClickedTextBlock.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpgame();
            }
        }
    }
}
