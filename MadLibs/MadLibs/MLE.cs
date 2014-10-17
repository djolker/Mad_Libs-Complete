using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace MadLibs
{
    class MLE
    {
        /// <summary>
        /// This is a list of all parts of the mad lib.  In the end all these segments are compiled together
        /// to create the final story piece.
        /// </summary>
        private List<Pair> segments = new List<Pair>();

        /// <summary>
        /// List of files available to read from
        /// </summary>
        public List<string> xmlFiles = new List<string>();

        /// <summary>
        /// Used in the creation of new stories
        /// </summary>
        public List<Pair> createNewStorySegments = new List<Pair>();

        public MLE()
        {
            lookForXMLStories();
        }

        /// <summary>
        /// Searches the current directory for XML files that can be used to 
        /// tell a mad lib
        /// </summary>
        private void lookForXMLStories()
        {
            DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());

            foreach (var file in d.GetFiles())
            {
                if (file.Name.Contains(".xml"))
                {
                    xmlFiles.Add(file.Name);
                }
            }
        }

        public void loadStory(int storyID)
        {
            using (XmlReader read = XmlReader.Create(xmlFiles[storyID]))
            {
                int pairCount = 0;
                while (read.Read())
                {
                    if ((read.NodeType == XmlNodeType.Element) && (read.Name == "piece"))
                    { 
                        Pair thisP = new Pair();
                        pairCount++;

                        thisP.setPiece(read["story"]);
                        segments.Add(thisP);
                    }

                    if ((read.NodeType == XmlNodeType.Element) && (read.Name == "gap"))
                    {
                        segments[pairCount - 1].setGap(read["text"]);
                    }
                }
            }
        }

        public void runPrompts()
        {
            foreach (Pair pair in segments)
            {
                if (pair.gap != "")
                {
                    Console.WriteLine("Please provide a " + pair.gap + ".");
                    pair.setGap(Console.ReadLine());
                }
            }
        }

        /// <summary>
        /// Returns the full story
        /// </summary>
        /// <returns></returns>
        public string makeFinalStory()
        {
            string fullStory = "";
            
            StringBuilder sb = new StringBuilder(fullStory);
            
            foreach (Pair pair in segments)
            {
                sb.Append(pair.piece);
                sb.Append(pair.gap);
            }

            return sb.ToString();
        }

        public void userStoryCreator()
        {
            Console.WriteLine("Instructions: When asked for a story piece, write up until the first gap. Then you will fill in");
            Console.WriteLine(" the text the user will be prompted for.");
            Console.WriteLine("If you don't put anything in the gap portion of entry, it will be assumed that");
            Console.WriteLine("the end of your story has been found, and the editor will close, returning");
            Console.WriteLine("you back to the menu.");

            Console.WriteLine("Press anything to continue...");
            Console.ReadKey();

            int count = 0;
            while (true)
            {
                Pair newPair = new Pair();

                Console.WriteLine("Number of sets: " + count.ToString());

                Console.WriteLine("Story:");
                newPair.setPiece(Console.ReadLine());
                Console.WriteLine("Gap:");
                newPair.setGap(Console.ReadLine());

                createNewStorySegments.Add(newPair);
                count++;
                if (newPair.gap == "")
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This method has a hard coded story to use as a
        /// multiple ending mad lib
        /// -----
        /// This story is hard coded in rather than using XML
        /// because the way the XML files were formatted, it would
        /// be difficult to rebuild for multiple endings
        /// </summary>
        public string multipleEndings()
        {
            List<string> answers = new List<string>();
            List<string> prompts = new List<string> {"noun", "noun", "place", "noun", "verb", "Exclamation", "place", "adverb", "place"};

            foreach (string prompt in prompts)
            {
                Console.WriteLine("Please provide a " + prompt);
                answers.Add(Console.ReadLine());
            }

            return buildMultipleEndings(answers);
        }

        /// <summary>
        /// Compiles strings into a final story. The multiple stories boolean is dependent on the length of the first answer.
        /// If there are more than 5 characters in the first answer, the ending switches to endingB
        /// </summary>
        /// <param name="answers"></param>
        /// <returns>final story</returns>
        public string buildMultipleEndings(List<string> answers)
        {
            bool endingB = false;

            string answer = "";
            while (answer != "a" && answer != "b")
            {
                if (answer == "a")
                {
                    endingB = false;
                }
                else if (answer == "b")
                {
                    endingB = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please choose: A. Depressing Ending, B. Odd Ending");
                    answer = Console.ReadLine();
                }
            }

            StringBuilder sb = new StringBuilder();
            List<string> storyPieces = new List<string> {"You wake up suddenly in a ",
                                                         " with all of your ",
                                                         " around you. You get up and decide to walk to the ",
                                                         "As you were walking you tripped over a ",
                                                         ". You hit the ground and scrape your knee. You ",
                                                         " your phone in the process. \"",
                                                         "\" you yell, realizing you've just ruined a $400 device. \r\n Now I'll have to go to the ",
                                                         " and get a new one. You ",
                                                         "walk to the ",
                                                         " and ask for a new phone. "};

            for (int i = 0; i < storyPieces.Count; i++)
            {
                sb.Append(storyPieces[i]);
                try
                {
                    sb.Append(answers[i - 1]);
                }
                catch
                {
                    
                }
            }

            if (endingB)
            {
                List<string> answersB = new List<string>();

                List<string> endingBprompts = new List<string>
                {
                    "verb", "Something you say", "verb", "verb", "noun"
                };

                List<string> storyB = new List<string>
                {
                    "You walk into the store and ",
                    " one of the salesmen. You yell \"",
                    "\" and in shock he takes a few steps back. \"Sir! That tone is NOT necessary!\" You acquire your new phone and leave, promptly ",
                    " and dropping it on the ground. Your new phone has scratches. \"This is my life now\" you quitely say to yourself. You ",
                    " back to your apartment. Climb into ",
                    " Hopeing for a better tomorrow."
                };

                //ask for answers
                for (int i = 0; i < endingBprompts.Count; i++)
                {
                    Console.WriteLine("Please provide a " + endingBprompts[i]);
                    answersB.Add(Console.ReadLine());
                }

                for (int i = 0; i < 5; i++)
                {
                    sb.Append(storyB[i]);
                    try
                    {
                        sb.Append(answersB[i]);
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                List<string> answersA = new List<string>();
                List<string> promptsA = new List<string>
                {
                    "Question", "An Apology", "verb", "Yell Something", "noun"
                };
                List<string> storyA = new List<string>
                {
                    " The associate you speak to looks tired and bleak \"",
                    " \" you ask. \"I don't have time for questions! I've been up all night writing MadLibs!\" \r\n \"",
                    "\" You respond. Still needing a phone you ",
                    " the first one you find and dash out yelling \"",
                    "\" on your way out. You make your way home, almost tripping over a ",
                    " on your way back to bed."
                };

                for(int i = 0; i< promptsA.Count; i++)
                {
                    Console.WriteLine("Please provide a " + promptsA[i]);
                    answersA.Add(Console.ReadLine());
                }

                for (int i = 0; i < 5; i++)
                {
                    sb.Append(storyA[i]);
                    try
                    {
                        sb.Append(answersA[i]);
                    }
                    catch
                    {

                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Compiles story pieces and writes a file name
        /// </summary>
        /// <param name="storyName"></param>
        public void buildUserStory(string storyName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<story>");
            foreach (Pair pair in createNewStorySegments)
            {
                sb.Append("<piece story=\"" + pair.piece + "\" />");

                if(pair.gap != "")
                sb.Append("<gap text=\"" + pair.gap + "\" />");
            }
            sb.Append("</story>");

            using(StreamWriter file = new StreamWriter(storyName + ".xml"))
            {
                file.WriteLine(sb.ToString());
            }
        }
    }
}
