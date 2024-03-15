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
            var randomQ = new Random();

            UIMethod.QuizWelcomeMessage();
            UIMethod.WriteEmptyLine();

            var QuestionList = new List<QuestionandAnswer>();
            

            continueStoringQuestion = UIMethod.StoreQuestion();
            if (continueStoringQuestion)
            {
                UIMethod.WriteEmptyLine();
                UIMethod.ShowQuizGameInstruction();
                quizSelection = UIMethod.GetQuizLineResponse();
                UIMethod.WriteEmptyLine();
                int questionInputCounter = 0;
                string quizAnswerOptions = "";

                while (keepPlayingQuiz)
                {
                    if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_STOREONLY) //mode selection
                    {
                        //QuestionandAnswer newQna = UIMethod.GetQuestionandAnswerObjectFromUser();
                        //QuestionList.Add(newQna);

                        numberOfQuizzerQuestions = UIMethod.GetIntFromUser("Number of question please:");
                        //numberOfQuizzerQuestions = UIMethod.GetIntFromUser("Number of question please:"); //SOMETHING CHANGED HERE
                        for (int quizzerReplyIndex = 0; quizzerReplyIndex < numberOfQuizzerQuestions; quizzerReplyIndex++)
                        {
                            questionInputCounter++;
                            var EachQuestionInput = new QuestionandAnswer();
                            EachQuestionInput.QuestionText = UIMethod.DisplayQuizzerInstruction(questionInputCounter);
                            UIMethod.WriteEmptyLine();

                            int maximumOptions = UIMethod.GetIntFromUser("Input the number of options to your question?\n"); //UIMethod.GiveNumberOfOptions();
                            UIMethod.WriteEmptyLine();
                            UIMethod.ShowOptionsMessage();
                            LogicMethod.TakeAnswerOption(maximumOptions, quizAnswerOptions, EachQuestionInput.ListofQuestionandAnswers);

                            UIMethod.WriteEmptyLine();
                            UIMethod.ShowCorrectAnswerInputMessage();
                            EachQuestionInput.CorrectAnswerText = UIMethod.AddCorrectOption();
                            UIMethod.WriteEmptyLine();
                            QuestionList.Add(EachQuestionInput);

                            if (quizSelection == Constant.QUIZ_TYPE_STOREONLY && numberOfQuizzerQuestions < Constant.MINIMUM_NUMBER_OF_QUESTION)
                            {
                                break;
                            }
                        }

                        LogicMethod.SerializeData(QuestionList);
                        //LogicMethod.SerializeData(path, writer, QuestionList);
                    }

                    Console.WriteLine($"Number of questions stored: {QuestionList.Count}"); //for checks

                    bool isValid = true;
                    int totalWinCounter = 0;
                    int totalAnswerCounter = 0;

                    if (isValid)
                    {
                        if (quizSelection == Constant.QUIZ_TYPE_STOREANDANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                        {
                            LogicMethod.ReadQuizFromXml(QuestionList);
                            UIMethod.DisplayUserInstruction(QuestionList.Count);

                            bool answerMoreQuestion = true;
                            while (answerMoreQuestion)
                            {
                                questionInputCounter++;
                                if (QuestionList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                                {
                                    UIMethod.PrintNoMoreQuestionMessage();
                                }
                                else
                                {
                                    //Random rnd = new Random();
                                    //
                                    var randomQuestion = new Random();//OUTSIDE THE LOOP
                                    int indexOfRandomQuestion = randomQuestion.Next(QuestionList.Count);
                                    QuestionandAnswer randomlySelectedQuestion = QuestionList[indexOfRandomQuestion];
                                    //TOP OBJECT

                                    //QuestionandAnswer randomlySelectedQuestion = LogicMethod.FetchRandomQuestion(randomQuestion, QuestionList);
                                    //QuestionandAnswer xRandy;
                                    //QuestionandAnswer randomlySelectedQuestionx = LogicMethod.FetchRandomQuestion(QuestionList, rnd);

                                    //Do I still need to declare random in the main code?


                                    UIMethod.PrintQuestionToUser(questionInputCounter, randomlySelectedQuestion.QuestionText);
                                    UIMethod.WriteEmptyLine();
                                    int optionCounter = Constant.COUNT_OPTION;

                                    UIMethod.PrintRandomQuestion(randomlySelectedQuestion.ListofQuestionandAnswers, optionCounter);
                                    UIMethod.WriteEmptyLine();
                                    UIMethod.PrintAnswerInputInstruction();
                                    string userAnswer = UIMethod.TakeUserAnswer();
                                    totalAnswerCounter++;
                                    UIMethod.WriteEmptyLine();
                                    LogicMethod.CheckCorrectAnswer(userAnswer, randomlySelectedQuestion.CorrectAnswerText, totalWinCounter);
                                    QuestionList.Remove(randomlySelectedQuestion);

                                    if (QuestionList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                                    {
                                        int noCount = 0;
                                        questionInputCounter = noCount;
                                        break;
                                    }
                                    else
                                    {
                                        bool moreQuestion = UIMethod.AnswerAnotherQuestion();
                                        LogicMethod.QuestionAnswerProxy(moreQuestion, answerMoreQuestion, totalWinCounter, totalAnswerCounter);
                                    }
                                }
                            }
                        }
                    }

                    UIMethod.PrintNoMoreQuestionMessage();
                    keepPlayingQuiz = UIMethod.RestartQuiz();
                    LogicMethod.RepeatPlay(keepPlayingQuiz, quizSelection, totalWinCounter, totalAnswerCounter);
                }
            }
            else
            {
                continueStoringQuestion = false;
                UIMethod.DisplayQuitMessage();
                Environment.Exit(0);
            }
        }
    }
}






