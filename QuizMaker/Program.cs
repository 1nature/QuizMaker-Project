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
            int totalWinCounter = 0;
            int totalAnswerCounter = 0;
            var QuestionList = new List<QuestionandAnswer>();
            var MyList = new List<QuestionandAnswer>();
            var FetchQuestionAndAnswers = new List<QuestionandAnswer>();

            UIMethod.QuizWelcomeMessage();
            continueStoringQuestion = UIMethod.StoreQuestion();
            if (continueStoringQuestion)
            {
                UIMethod.ShowQuizGameInstruction();
                quizSelection = UIMethod.GetQuizLineResponse();
                int questionInputCounter = 0;
                int countAnsweredQuestions = 0;
                string quizAnswerOptions = "";

                while (keepPlayingQuiz)
                {
                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_STOREONLY)
                    {
                        int theDecrement = UIMethod.GetUserInputandCreateNewQuestionsandAnswers(QuestionList);

                        if (quizSelection == Constant.QUIZ_TYPE_STOREONLY && theDecrement < Constant.MINIMUM_NUMBER_OF_QUESTION)
                        {
                            UIMethod.ShowCompletedQuestionMessage();
                            keepPlayingQuiz = UIMethod.RestartQuiz(); //
                            if (!keepPlayingQuiz)
                            {
                                break;
                            }
                            else
                            {
                                UIMethod.ShowQuizGameInstruction();
                                quizSelection = UIMethod.GetQuizLineResponse();
                                if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                                {
                                    continue;
                                }
                            }
                        }

                    }
                    Console.WriteLine($"Number of questions stored: {QuestionList.Count}"); //for checks

                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                    {
                        FetchQuestionAndAnswers = LogicMethod.ReadQuizFromXml();
                        UIMethod.DisplayUserInstruction(FetchQuestionAndAnswers.Count);

                        bool answerMoreQuestion = true;
                        while (answerMoreQuestion)
                        {
                            countAnsweredQuestions++;
                            if (FetchQuestionAndAnswers.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                            {
                                UIMethod.PrintNoMoreQuestionMessage();
                                break;
                            }

                            else
                            {
                                QuestionandAnswer theSelected = LogicMethod.FetchRandomQuestion(FetchQuestionAndAnswers, randomQuestion);
                                QuestionandAnswer retrievedText = UIMethod.DisplayQuestionAndAnswersToUser(theSelected, countAnsweredQuestions);
                                string userAnswer = UIMethod.TakeUserAnswer();
                                totalAnswerCounter++;                                
                                bool holdTheCondition = LogicMethod.RemoveText(userAnswer, retrievedText, FetchQuestionAndAnswers);
                                
                                if (holdTheCondition)
                                {
                                    UIMethod.WinMessage();
                                    totalWinCounter++;
                                }
                                else { UIMethod.LossMessage(); }
                                
                                if (FetchQuestionAndAnswers.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                                {
                                    int noCount = Constant.QUIZ_DECISION_NO;
                                    questionInputCounter = noCount;
                                    break;
                                }

                                bool moreQuestion = UIMethod.AnswerAnotherQuestion();
                                if (!moreQuestion)
                                {
                                    break;
                                }
                                else
                                {
                                    UIMethod.CalculateWinningScore(moreQuestion, answerMoreQuestion, totalWinCounter, totalAnswerCounter);
                                }

                            }
                        }
                    }

                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                    {
                        keepPlayingQuiz = LogicMethod.CheckQuizConditionThree(quizSelection, totalWinCounter, totalAnswerCounter);
                        countAnsweredQuestions = LogicMethod.RestartQuestionNumber(keepPlayingQuiz);
                    }

                } 
            }
            else
            {
                continueStoringQuestion = false;
                while (!continueStoringQuestion)
                {
                    UIMethod.DisplayQuitMessage();
                    break; 
                }
            }
        }
    }
}






