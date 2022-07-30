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
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
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
    }
}
