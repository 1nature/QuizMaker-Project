using Microsoft.VisualBasic;

namespace QuizMaker
{
    public static class UIMethod
    {
        public static void QuizWelcomeMessage()
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine("You can use this program to ask questions from users\n");
        }

        public static void DisplayQuitMessage()
        {
            Console.WriteLine("You have decided not to play the quiz game.");
        }

        public static void GameInstruction()
        {
            Console.WriteLine("There are four questions in this game.Please enter 1 to answer a question, or 0 to exit\n");
        }
        public static bool QuizzerDecision()
        {
            Console.WriteLine("Enter '1' to ask a question, or '0' to quit.\n");
            int makeQuizDecision = int.Parse(Console.ReadLine());
            return (makeQuizDecision == Constant.QUIZ_DECISION_YES);
        }

        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }

        public static void NumberOfQuestionMessage()
        {
            Console.WriteLine("State the number of questions you would like to ask the user\n");
            Console.WriteLine("How many questions would you like to attempt?\n");
        }

        public static void WinMessage()
        {
            Console.WriteLine("Correct answer\n");
        }

        public static void LossMessage()
        {
            Console.WriteLine("Incorrect answer\n");
        }

        public static string DisplayQuizzerInstruction()
        {
            Console.WriteLine("Input the question you would like to ask\n");
            string quizzerReply = Console.ReadLine();
            return quizzerReply;
        }

        public static int GiveNumberOfOptions()
        {
            Console.WriteLine("Please input the number of options to your question?\n");

            int numberOfOptions = int.Parse(Console.ReadLine());
            return numberOfOptions;
        }

        public static void ShowOptionsMessage()
        {
            Console.WriteLine("Input your options one after the other. One of them must be the correct answer\n");
        }

        public static void ShowCorrectOptionMessage()
        {
            Console.WriteLine("Input the correct option to your question");

        }

        public static string AddTheOption()
        {
            string theOption = Console.ReadLine();
            return theOption;
        }

        public static string AddCorrectOption()
        {
            //UIMethod.WriteEmptyLine();
            string correctOption = Console.ReadLine();
            return correctOption;
        }

        public static void ShowCorrectAnswerInputMessage()
        {
            Console.WriteLine("Input the correct answer\n");
        }


        public static void DisplayUserInstruction()
        {
            Console.WriteLine("Please select from the options shown to answer the question");
            Console.WriteLine("You should type in your answer\n");
            //There are x questions,
            //Select which question to answer from x
        }


        public static string TakeUserAnswer()
        {
            UIMethod.WriteEmptyLine();
            string theUserAnswer = Console.ReadLine();
            return theUserAnswer;
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
