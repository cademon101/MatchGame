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
    ///🐸
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        // had to add this part in, check back for error💩
        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            //⬇️  List of 8 pairs of Emojis. animalEmoji is this new list of strings 
            List<string> animalEmoji = new List<string>()
            {
                "🙈", "🙈",
                "🙉", "🙉",
                "🙊", "🙊",
                "🐵", "🐵",
                "🦁", "🦁",
                "🐯", "🐯",
                "🦊", "🦊",
                "🐸", "🐸",
            };

            // ⬇️ is a random number gen, not sure how it works but it makes this random method is new random
            Random random = new Random();

            //⬇️ finds every Block of typeblock in our mainGrid and repeats this code
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                //⬇️ stops the timer from being sucked into the fray
                if (textBlock.Name != "timeTextBlock")
                {
                    //⬇️ picks random number between 0 and the emojis left on the list and calls it index
                    int index = random.Next(animalEmoji.Count);
                    //⬇️ uses the random number called index and pulls up that emoji
                    string nextEmoji = animalEmoji[index];
                    //⬇️ update the block with our pulled up emoji
                    textBlock.Text = nextEmoji;
                    //⬇️ removes the used one... rinse and repeat
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        //⬇️ keeps track of whether or not the player just clicked on the first animal in a pair and is trying to find its match
        TextBlock lastTextBlockClicked;
        //⬇️ our bool findingMatch is kept at faulse ⬛️ unchecked
        bool findingMatch = false;
        //⬇️ mousedown method
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            
            /*⬇️ player clicked on first animal, makes that visibily hidden, 
             * keeps track of text block incase it needs to be visable again?
             * this is the first click, so if our switch is still unfliped ⬛️
            ⬇️ */
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            
            /* Match found! it makes the second animal in the pair inviable (and unclickable) 
             * and resets the finding match so its can start on the next first animal. Brings bool back to faulse.
            ⬇️ */
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }

            /* if player clicked on animal that doesnt match, so it makes the first animal clicked visable
             * and resets finding match
            ⬇️ */
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}
