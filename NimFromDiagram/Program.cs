namespace NimFromDiagram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartNim();
        }

        #region View

        /// <summary>
        /// Promt user for numeric input between 1 and 2 or 1 and 1, depending on how many matches that are left.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="matches"></param>
        public static void PromptForInput(bool player, int matches)
        {
            int take;
            if (matches > 3)
            {
                take = 2;
            }
            else
            {
                take = matches - 1;
            }

            if (player)
            {
                Console.WriteLine("Please Take 1 to " + take + "matches!");
            }
            else
            {
                Console.WriteLine("Computers Turn...");
            }
        }

        /// <summary>
        /// Recieve user input and forward it to the controller.
        /// </summary>
        /// <param name="matchesLeft"></param>
        /// <returns>Int between 1 and 2</returns>
        public static int SetUserInput(int matchesLeft)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number)) // is this a number?
                {
                    if (number < 3 && number > 0) // is this number between 1 and 2.
                    {
                        if (number < matchesLeft) // is this number less than matches left.
                        {
                            return number; // then return the number, else prompt for new input.
                        }
                        else
                        {
                            RepeatInput(matchesLeft - 1);
                        }
                    }
                    else
                    {
                        RepeatInput(2);
                    }
                }
                else
                {
                    RepeatInput(0);
                }
            }
        }

        /// <summary>
        /// Write the number of matches that are left
        /// </summary>
        /// <param name="matches"></param>
        public static void RemainingMatches(int matches)
        {
            Console.WriteLine("There's " + matches + " left.");
        }

        /// <summary>
        /// Prompt the user to repeat the input
        /// </summary>
        /// <param name="number"></param>
        private static void RepeatInput(int number)
        {
            if (number == 0)
            {
                Console.WriteLine("Input must be a number!");
            }
            else
            {
                Console.WriteLine("Input must be a number between 1 and " + number + "!");
            }
        }

        /// <summary>
        /// Write who win the game
        /// </summary>
        /// <param name="pt"></param>
        private static void WriteWinner(bool pt)
        {
            if (pt)
            {
                Console.WriteLine("You Loose!");
            }
            else
            {
                Console.WriteLine("You Win!");
            }
        }


        #endregion

        #region Model
        public static int matches;
        public static bool playersTurn;

        /// <summary>
        /// Set the matches variable.</br>
        /// if change is 0, matches are set to 7.
        /// </summary>
        /// <param name="change"></param>
        public static void SetMatches(int change = 0)
        {
            if (change == 0)
            {
                matches = 7;
            }
            else
            {
                matches -= change;
            }
        }

        /// <summary>
        /// Set the turn.
        /// </summary>
        /// <param name="start"></param>
        public static void SetTurn(bool start)
        {
            if (start)
            {
                playersTurn = true;
            }
            else
            {
                playersTurn = !playersTurn;
            }
        }

        /// <summary>
        /// Set the matches on the computers turn.
        /// </summary>
        public static void ComputerRemoveMatches()
        {
            Random rnd = new Random();
            matches -= rnd.Next(1, 3);
        }
        #endregion

        #region Controller

        /// <summary>
        /// Start the game.
        /// </summary>
        public static void StartNim()
        {
            //Start Nim
            SetMatches();
            SetTurn(true);

            while (matches > 0)
            {
                RemainingMatches(matches);
                if (playersTurn && matches <= 1)
                {
                    WriteWinner(playersTurn);
                    break;
                }
                else
                {
                    PromptForInput(playersTurn, matches);
                    if (!playersTurn)
                    {
                        ComputerRemoveMatches();
                    }
                    else
                    {
                        SetMatches(SetUserInput(matches));
                    }
                    SetTurn(false);
                }
            }

        }
        #endregion
    }
}