using static QuizMaker.QnAClass;

namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueStoringQuestion = true;
            bool keepPlayingQuiz = true;
            int numberOfQuizzerQuestions = 0;
            char quizSelection;
            char newQuizSelection;
            var randomQuestion = new Random();

            UIMethod.QuizWelcomeMessage();
            UIMethod.WriteEmptyLine();

            var QuestionList = new List<QuestionandAnswer>();
            var DeserialisedList = new List<QuestionandAnswer>();


            continueStoringQuestion = UIMethod.StoreQuestion();
            if (continueStoringQuestion)
            {
                UIMethod.WriteEmptyLine();
                UIMethod.ShowQuizGameInstruction();
                quizSelection = UIMethod.GetQuizLineResponse();
                UIMethod.WriteEmptyLine();
                int questionInputCounter = 0;
                int countAnsweredQuestions = 0;
                string quizAnswerOptions = "";

                while (keepPlayingQuiz)
                {
                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_STOREONLY) 
                    {
                        //QuestionandAnswer newQna = UIMethod.GetQuestionandAnswerObjectFromUser();
                        //QuestionList.Add(newQna);

                        numberOfQuizzerQuestions = UIMethod.GetIntFromUser("Input the number of questions you want to store:");
                        int questionDecrement = numberOfQuizzerQuestions;
                        for (int quizzerReplyIndex = 0; quizzerReplyIndex < numberOfQuizzerQuestions; quizzerReplyIndex++)
                        {
                            questionInputCounter++;
                            var EachQuestionInput = new QuestionandAnswer();
                            EachQuestionInput.QuestionText = UIMethod.TakeUserQuestion(questionInputCounter);
                            UIMethod.WriteEmptyLine();
                            questionDecrement--;

                            int maximumOptions = UIMethod.GetIntFromUser("Input the number of options to your question?\n");
                            UIMethod.WriteEmptyLine();
                            UIMethod.ShowOptionsMessage();
                            LogicMethod.TakeAnswerOption(maximumOptions, quizAnswerOptions, EachQuestionInput.ListofQuestionandAnswers);

                            UIMethod.WriteEmptyLine();
                            UIMethod.ShowCorrectAnswerInputMessage();
                            EachQuestionInput.CorrectAnswerText = UIMethod.AddCorrectOption();
                            UIMethod.WriteEmptyLine();
                            QuestionList.Add(EachQuestionInput);
                        }
                        LogicMethod.SerializeData(QuestionList);

                        if (quizSelection == Constant.QUIZ_TYPE_STOREONLY && questionDecrement < Constant.MINIMUM_NUMBER_OF_QUESTION)
                        {
                            UIMethod.ShowCompletedQuestionMessage();
                            keepPlayingQuiz = UIMethod.RestartQuiz();
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

                    Console.WriteLine($"Number of questions stored: {QuestionList.Count}"); //for check

                    int totalWinCounter = 0;
                    int totalAnswerCounter = 0;

                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                    {
                        DeserialisedList = LogicMethod.ReadQuizFromXml();
                        UIMethod.DisplayUserInstruction(DeserialisedList.Count);

                        bool answerMoreQuestion = true;
                        while (answerMoreQuestion)
                        {
                            countAnsweredQuestions++;
                            if (DeserialisedList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                            {
                                UIMethod.PrintNoMoreQuestionMessage();
                            }
                            else
                            {
                                QuestionandAnswer randomlySelectedQuestion = LogicMethod.FetchRandomQuestion(DeserialisedList, randomQuestion);
                                UIMethod.PrintQuestionToUser(countAnsweredQuestions, randomlySelectedQuestion.QuestionText);
                                UIMethod.WriteEmptyLine();
                                int optionCounter = Constant.COUNT_OPTION;

                                UIMethod.PrintRandomQuestion(randomlySelectedQuestion.ListofQuestionandAnswers, optionCounter);
                                UIMethod.WriteEmptyLine();
                                UIMethod.PrintAnswerInputInstruction();
                                string userAnswer = UIMethod.TakeUserAnswer();
                                totalAnswerCounter++;
                                UIMethod.WriteEmptyLine();
                                int holdTheCounter = LogicMethod.CheckCorrectAnswer(userAnswer, randomlySelectedQuestion.CorrectAnswerText);
                                totalWinCounter = LogicMethod.CheckCorrectCount(holdTheCounter, totalWinCounter);
                                DeserialisedList.Remove(randomlySelectedQuestion);

                                if (DeserialisedList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
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
                                    //need to debug this!
                                    UIMethod.CalculateWinningScore(moreQuestion, answerMoreQuestion, totalWinCounter, totalAnswerCounter);
                                }
                            }
                        }
                    }

                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                    {
                        int parseCounter = totalWinCounter;
                        UIMethod.PrintNoMoreQuestionMessage();
                        UIMethod.ShowWinningScore(totalWinCounter, totalAnswerCounter);
                        keepPlayingQuiz = UIMethod.RestartQuiz();
                        newQuizSelection = UIMethod.RepeatPlay(keepPlayingQuiz, quizSelection, parseCounter, totalAnswerCounter);
                        quizSelection = newQuizSelection;
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






