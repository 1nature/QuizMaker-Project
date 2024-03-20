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
                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_STOREONLY) //mode selection
                    {
                        //QuestionandAnswer newQna = UIMethod.GetQuestionandAnswerObjectFromUser();
                        //QuestionList.Add(newQna);

                        numberOfQuizzerQuestions = UIMethod.GetIntFromUser("Input the number of questions you want to store:");
                        int questionDecrement = numberOfQuizzerQuestions;
                        for (int quizzerReplyIndex = 0; quizzerReplyIndex < numberOfQuizzerQuestions; quizzerReplyIndex++)
                        {
                            questionInputCounter++;
                            var EachQuestionInput = new QuestionandAnswer();
                            EachQuestionInput.QuestionText = UIMethod.DisplayQuizzerInstruction(questionInputCounter);
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
                            Console.WriteLine("You have exhausted the number of questions you chose to store");
                            keepPlayingQuiz = UIMethod.RestartQuiz();
                            if (!keepPlayingQuiz)
                            {
                                break;
                            }
                        }
                    }

                    Console.WriteLine($"Number of questions stored: {QuestionList.Count}"); //for checks

                    bool isValid = true;
                    //int totalWinCounter = 0; //2 is for test
                    int totalWinCounter = 0;//2 is for test
                    int totalAnswerCounter = 0;

                    //if (isValid)
                    //{
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
                                //LogicMethod.CheckCorrectAnswer(userAnswer, randomlySelectedQuestion.CorrectAnswerText, totalWinCounter);
                                //totalWinCounter = LogicMethod.CheckCorrectAnswer(userAnswer, randomlySelectedQuestion.CorrectAnswerText);
                                int holdTheCounter = LogicMethod.CheckCorrectAnswer(userAnswer, randomlySelectedQuestion.CorrectAnswerText);
                                totalWinCounter++;
                                //int xtotalWinCounter = 0;
                                //totalWinCounter++; //something wrong with my counter
                                //counter counts even when answer is wrong!
                                DeserialisedList.Remove(randomlySelectedQuestion);
                                //int xy = UIMethod.CalculateWinningScoreOnly(totalWinCounter, totalAnswerCounter);

                                if (DeserialisedList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                                {
                                    int noCount = 0;
                                    questionInputCounter = noCount;
                                    break;
                                }

                                bool moreQuestion = UIMethod.AnswerAnotherQuestion();
                                //moreQuestion = keepPlayingQuiz;
                                if (!moreQuestion)
                                {
                                    //keepPlayingQuiz = false;
                                    //UIMethod.CalculateWinningScore(moreQuestion, answerMoreQuestion, totalWinCounter, totalAnswerCounter);
                                    break;
                                }
                                else
                                {
                                    //need to debug this!
                                    UIMethod.CalculateWinningScore(moreQuestion, answerMoreQuestion, totalWinCounter, totalAnswerCounter);
                                }
                                //UIMethod.XCalculateWinningScore(moreQuestion, answerMoreQuestion, xtotalWinCounter, totalAnswerCounter);
                                //above code not yet working
                                

                                //else
                                //{
                                //    bool moreQuestion = UIMethod.AnswerAnotherQuestion();
                                //    UIMethod.XCalculateWinningScore(moreQuestion, answerMoreQuestion, xtotalWinCounter, totalAnswerCounter);
                                //    //LogicMethod.QuestionAnswerProxy(moreQuestion, answerMoreQuestion, totalWinCounter, totalAnswerCounter);
                                //}
                            }
                        }
                    }
                    //totalWinCounter += totalWinCounter;
                    int parseCounter = totalWinCounter;
                    UIMethod.PrintNoMoreQuestionMessage();
                    UIMethod.CalculateWinningScoreOnly(totalWinCounter, totalAnswerCounter);
                    keepPlayingQuiz = UIMethod.RestartQuiz();
                    // UIMethod.RepeatPlay(keepPlayingQuiz, quizSelection, totalWinCounter, totalAnswerCounter);
                    UIMethod.RepeatPlay(keepPlayingQuiz, quizSelection, parseCounter, totalAnswerCounter);
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






