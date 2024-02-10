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

        public static bool StoreQuestion()
        {
            Console.WriteLine("Enter '1' to store question(s), or '0' to quit.\n");
            int makeQuizDecision = int.Parse(Console.ReadLine());
            return (makeQuizDecision == Constant.QUIZ_DECISION_YES);
        }

        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }

        public static void NumberOfQuestionMessage()
        {
            Console.WriteLine("How many questions would you like to store?\n");
        }

        public static void WinMessage()
        {
            Console.WriteLine("Correct answer. You have a score\n");
        }

        public static void LossMessage()
        {
            Console.WriteLine("Incorrect answer. You have no score\n");
        }

        public static void NoMoreQuestion()
        {
            Console.WriteLine("There are no more questions left to answer. Thank you for your effort\n");
        }
        public static string DisplayQuizzerInstruction(int questionInputTracker)
        {
            Console.WriteLine($"Input the question number: {questionInputTracker}\n");
            //Console.WriteLine("Input the question you would like to ask\n");
            string quizzerReply = Console.ReadLine();
            return quizzerReply;
        }

        public static int GiveNumberOfOptions()
        {
            Console.WriteLine("Input the number of options to your question?\n");

            int numberOfOptions = int.Parse(Console.ReadLine());
            return numberOfOptions;
        }

        public static void ShowOptionsMessage()
        {
            Console.WriteLine("Input your options one after the other. One of them must be the correct answer\n");
        }
        public static bool AnswerAnotherQuestion()
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine("Enter '1' to answer another question, or '0' to quit the game.");
            int newQuestion = int.Parse(Console.ReadLine());
            UIMethod.WriteEmptyLine();
            return (newQuestion == Constant.QUIZ_DECISION_YES);
        }

        public static void CalculateWinningScore(int currentScore, int maxScore)
        {
            Console.WriteLine("***********YOUR FINAL SCORE**************");
            int result = (currentScore / maxScore) * 100;
            Console.WriteLine($"You scored {currentScore} question(s) out of {maxScore} OR {result}%");
        }

        public static string AddTheOption()
        {
            string theOption = Console.ReadLine();
            return theOption;
        }

        public static string AddCorrectOption()
        {
            string correctOption = Console.ReadLine();
            return correctOption;
        }

        public static void ShowCorrectAnswerInputMessage()
        {
            Console.WriteLine("Input the correct answer to your question. It will not be shown to the user\n");
        }


        public static void DisplayUserInstruction()
        {
            Console.WriteLine("Please select from the options shown to answer the question");
            Console.WriteLine("You should type in your answer\n");
        }


        public static string TakeUserAnswer()
        {
            UIMethod.WriteEmptyLine();
            string theUserAnswer = Console.ReadLine();
            return theUserAnswer;
        }

    }
}
