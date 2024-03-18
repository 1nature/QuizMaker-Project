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
                int storePlusAnswerCounter = 0;
                string quizAnswerOptions = "";

                while (keepPlayingQuiz)
                {
                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_STOREONLY) //mode selection
                    {
                        //QuestionandAnswer newQna = UIMethod.GetQuestionandAnswerObjectFromUser();
                        //QuestionList.Add(newQna);

                        numberOfQuizzerQuestions = UIMethod.GetIntFromUser("Input the number of questions you want to store:");
                        int someCountDown = numberOfQuizzerQuestions;
                        for (int quizzerReplyIndex = 0; quizzerReplyIndex < numberOfQuizzerQuestions; quizzerReplyIndex++)
                        {
                            questionInputCounter++;
                            //int someCountDown = numberOfQuizzerQuestions;
                            var EachQuestionInput = new QuestionandAnswer();
                            EachQuestionInput.QuestionText = UIMethod.DisplayQuizzerInstruction(questionInputCounter);
                            UIMethod.WriteEmptyLine();
                            someCountDown--;

                            int maximumOptions = UIMethod.GetIntFromUser("Input the number of options to your question?\n");
                            UIMethod.WriteEmptyLine();
                            UIMethod.ShowOptionsMessage();
                            LogicMethod.TakeAnswerOption(maximumOptions, quizAnswerOptions, EachQuestionInput.ListofQuestionandAnswers);

                            UIMethod.WriteEmptyLine();
                            UIMethod.ShowCorrectAnswerInputMessage();
                            EachQuestionInput.CorrectAnswerText = UIMethod.AddCorrectOption();
                            UIMethod.WriteEmptyLine();
                            QuestionList.Add(EachQuestionInput);

                            if (quizSelection == Constant.QUIZ_TYPE_STOREONLY && someCountDown < Constant.MINIMUM_NUMBER_OF_QUESTION)
                            {
                                Console.WriteLine("You have stored your questions");
                                break;
                                //should have restart opportunity here
                            }
                        }                        
                        LogicMethod.SerializeData(QuestionList);
                    }

                    Console.WriteLine($"Number of questions stored: {QuestionList.Count}"); //for checks

                    bool isValid = true;
                    int totalWinCounter = 0;
                    int totalAnswerCounter = 0;

                    if (isValid)
                    {
                        if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                        {
                            DeserialisedList = LogicMethod.ReadQuizFromXml();
                            UIMethod.DisplayUserInstruction(DeserialisedList.Count);

                            bool answerMoreQuestion = true;
                            while (answerMoreQuestion)
                            {
                                //questionInputCounter++;
                                storePlusAnswerCounter++;
                                if (DeserialisedList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                                {
                                    UIMethod.PrintNoMoreQuestionMessage();
                                }
                                else
                                {
                                    QuestionandAnswer randomlySelectedQuestion = LogicMethod.FetchRandomQuestion(DeserialisedList, randomQuestion);
                                    //UIMethod.PrintQuestionToUser(questionInputCounter, randomlySelectedQuestion.QuestionText);
                                    UIMethod.PrintQuestionToUser(storePlusAnswerCounter, randomlySelectedQuestion.QuestionText);
                                    UIMethod.WriteEmptyLine();
                                    int optionCounter = Constant.COUNT_OPTION;

                                    UIMethod.PrintRandomQuestion(randomlySelectedQuestion.ListofQuestionandAnswers, optionCounter);
                                    UIMethod.WriteEmptyLine();
                                    UIMethod.PrintAnswerInputInstruction();
                                    string userAnswer = UIMethod.TakeUserAnswer();
                                    totalAnswerCounter++;
                                    UIMethod.WriteEmptyLine();
                                    //LogicMethod.CheckCorrectAnswer(userAnswer, randomlySelectedQuestion.CorrectAnswerText, totalWinCounter);
                                    int xtotalWinCounter = LogicMethod.CheckCorrectAnswer(userAnswer, randomlySelectedQuestion.CorrectAnswerText);
                                    //int xtotalWinCounter = 0;
                                    //xtotalWinCounter++;
                                    DeserialisedList.Remove(randomlySelectedQuestion);

                                    if (DeserialisedList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                                    {
                                        int noCount = 0;
                                        questionInputCounter = noCount;
                                        break;
                                    }

                                    bool moreQuestion = UIMethod.AnswerAnotherQuestion();
                                    UIMethod.XCalculateWinningScore(moreQuestion, answerMoreQuestion, xtotalWinCounter, totalAnswerCounter);


                                    //else
                                    //{
                                    //    bool moreQuestion = UIMethod.AnswerAnotherQuestion();
                                    //    UIMethod.XCalculateWinningScore(moreQuestion, answerMoreQuestion, xtotalWinCounter, totalAnswerCounter);
                                    //    //LogicMethod.QuestionAnswerProxy(moreQuestion, answerMoreQuestion, totalWinCounter, totalAnswerCounter);
                                    //}
                                }
                            }
                        }
                    }

                    UIMethod.PrintNoMoreQuestionMessage();
                    keepPlayingQuiz = UIMethod.RestartQuiz();
                    UIMethod.RepeatPlay(keepPlayingQuiz, quizSelection, totalWinCounter, totalAnswerCounter);
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






