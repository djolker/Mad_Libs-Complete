using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MadLibs
{
    class Program
    {
       public static MLE game = new MLE();

        /// <summary>
        /// The "Multiple Endings Story" choice is statically hardcoded into the game.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Main");
                Console.WriteLine("0. MadLib");
                Console.WriteLine("1. Create Story");
                Console.WriteLine("2. Multiple Ending Story");
                Console.WriteLine("3. End Program");

                string answer = Console.ReadLine();

                if (answer == "0")
                {
                    gameStart();
                }
                else if (answer == "1")
                {
                    createStory();
                }
                else if(answer == "2")
                {
                    multipleEndingStart();
                }
                else if (answer == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine(answer + " was not an option, press any key to continue...");
                }
            }
        }

        public static void createStory()
        {
            game = new MLE();
            game.userStoryCreator();
            Console.WriteLine("Please name your story...");
            game.buildUserStory(Console.ReadLine());
        }

        public static void gameStart()
        {
            while (true)
            {
                game = new MLE();
                Console.Clear();
                int count = 0;

                foreach (string line in game.xmlFiles)
                {
                    Console.WriteLine(count.ToString() + ". " + line);
                    count++;
                }

                Console.WriteLine("");
                Console.WriteLine("Pick a story");
                
                game.loadStory(Convert.ToInt32(Console.ReadLine()));
                Console.Clear();
                game.runPrompts();
                Console.Clear();
                Console.WriteLine(game.makeFinalStory());

                Console.WriteLine("Would you like to play again? (Y/N)");
                if (Console.ReadKey().KeyChar == 'n')
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Loops through the mad lib
        /// </summary>
        public static void multipleEndingStart()
        {
            game = new MLE();
            Console.Clear();
            Console.WriteLine(game.multipleEndings());

            Console.WriteLine("Press any key to continue...");

            Console.ReadKey();

            Console.Clear();
        }
    }
}
