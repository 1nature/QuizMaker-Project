namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char quizSelection;

            UIMethod.QuizWelcomeMessage();
            UIMethod.PrintIntegerInputInstructionToUser();
            int inputNum = UIMethod.GetIntegerFromUser();
            bool continueStoringQuestion = LogicMethod.ContinueOrQuitQuiz(inputNum);
            if (continueStoringQuestion)
            {
                UIMethod.ShowQuizGameInstruction();
                quizSelection = UIMethod.GetQuizLineResponse();
                bool keepPlayingQuiz = true;

                UIMethod.DisplayQuizLogic(keepPlayingQuiz, quizSelection);
            }
        }
    }
}






