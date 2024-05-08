namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueStoringQuestion = true;
            char quizSelection;
            var randomQuestion = new Random();

            UIMethod.QuizWelcomeMessage();
            continueStoringQuestion = UIMethod.GetUserPlayOrQuitInput();
            if (continueStoringQuestion)
            {
                UIMethod.ShowQuizGameInstruction();
                quizSelection = UIMethod.GetQuizLineResponse();
                int numOfStoredQuestion = 0; 
                var QuestionList = new List<QuestionandAnswer>();
                bool keepPlayingQuiz = true;


                while (keepPlayingQuiz)
                {
                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_STOREONLY)
                    {
                        numOfStoredQuestion = UIMethod.GetUserInputandCreateNewQuestionsandAnswers(QuestionList);
                        keepPlayingQuiz = LogicMethod.StopStoringQuestion(quizSelection, QuestionList, keepPlayingQuiz, numOfStoredQuestion);
                        quizSelection = LogicMethod.CheckRestartStoreCondition(keepPlayingQuiz, quizSelection);
                        //0 to quit has no message or nothing. It just quits like that.
                    }

                    //Console.WriteLine($"Number of questions stored: {QuestionList.Count}"); // for checks
                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                    {
                        int numOfAvailableQuestions = LogicMethod.FetchQuestionsAndDisplayInstruction(quizSelection);
                        UIMethod.DisplayUserInstruction(numOfAvailableQuestions);
                        bool exitAnswerCondition = LogicMethod.PerformAnswerOption(randomQuestion);
                        keepPlayingQuiz = exitAnswerCondition; //some repetition here
                        quizSelection = LogicMethod.CheckRestartAnsweringCondition(keepPlayingQuiz, quizSelection);
                    }
                }
            }
        }
    }
}






