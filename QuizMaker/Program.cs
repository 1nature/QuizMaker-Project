namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueStoringQuestion = true;
            bool keepPlayingQuiz = true;
            char quizSelection;
            var randomQuestion = new Random();
            var QuestionList = new List<QuestionandAnswer>();
            var MyList = new List<QuestionandAnswer>();
            var FetchQuestionAndAnswers = new List<QuestionandAnswer>();

            UIMethod.QuizWelcomeMessage();
            continueStoringQuestion = UIMethod.StoreQuestion();
            if (continueStoringQuestion)
            {
                UIMethod.ShowQuizGameInstruction();
                quizSelection = UIMethod.GetQuizLineResponse();
                int numOfStoredQuestion = 0; 
                bool keepStoringQuestion;

                while (keepPlayingQuiz)
                {
                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_STOREONLY)
                    {
                        numOfStoredQuestion = UIMethod.GetUserInputandCreateNewQuestionsandAnswers(QuestionList);
                        keepStoringQuestion = LogicMethod.StopStoringQuestion(quizSelection, QuestionList, keepPlayingQuiz, numOfStoredQuestion);
                        keepPlayingQuiz = keepStoringQuestion;
                        quizSelection = LogicMethod.CheckRestartStoreCondition(keepPlayingQuiz, quizSelection);
                        //0 to quit has no message or nothing. It just quits like that.
                    }

                    //Console.WriteLine($"Number of questions stored: {QuestionList.Count}"); // for checks

                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                    {
                        int numOfAvailableQuestion = UIMethod.FetchQuestionsAndDisplayInstruction(quizSelection); 
                        bool exitAnswerCondition = LogicMethod.PerformAnswerOption(randomQuestion);
                        keepPlayingQuiz = exitAnswerCondition; 
                        quizSelection = LogicMethod.CheckRestartAnsweringCondition(keepPlayingQuiz, quizSelection);
                    }
                }
            }
        }
    }
}






