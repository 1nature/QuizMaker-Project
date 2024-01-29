using Microsoft.VisualBasic;

namespace QuizMaker
{
    public static class UIMethods
    {
        public static void GameWelcomeMessage()
        {
            Console.WriteLine("Welcome!");
        }

        public static void GameQuitMessage()
        {
            Console.WriteLine("You have decided not to play the quiz game.");
        }

        public static void GameInstruction()
        {
            Console.WriteLine("There are four questions in this game.Please enter 1 to answer a question, or 0 to exit\n");
        }
        public static bool PlayQuizDecision()
        {
            Console.WriteLine("Enter '1' to make a bet, or '0' to quit.");
            int makeQuizDecision = int.Parse(Console.ReadLine());
            return (makeQuizDecision == Constants.QUIZ_DECISION_YES);
        }

        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }

        public static void WinMessage()
        {
            Console.WriteLine("Correct answer");
        }

        public static void LossMessage()
        {
            Console.WriteLine("Incorrect answer");
        }


        public static int AnswerCounter(string firstVariable, string secondVariable)
        {
            int singleQuestionCounter = 0;
            if (firstVariable != secondVariable)
            {
                LossMessage();
            }
            else
            {
                singleQuestionCounter++;
                WinMessage();
            }
            return singleQuestionCounter;
        }
    }
}
