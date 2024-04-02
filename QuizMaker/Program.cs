﻿using static QuizMaker.QnAClass;
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
            var MyList = new List<QuestionandAnswer>();
            var FetchQuestionAndAnswers = new List<QuestionandAnswer>();


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
                        numberOfQuizzerQuestions = UIMethod.GetIntFromUser("Input the number of questions you want to store: \n");
                        int questionDecrement = numberOfQuizzerQuestions;

                        for (int quizzerReplyIndex = 0; quizzerReplyIndex < numberOfQuizzerQuestions; quizzerReplyIndex++)
                        {
                            QuestionandAnswer newQna = (QuestionandAnswer)UIMethod.GetQuestionandAnswerObjectFromUser();
                            QuestionList.Add(newQna);
                            questionDecrement--;
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
                        FetchQuestionAndAnswers = LogicMethod.ReadQuizFromXml();
                        UIMethod.DisplayUserInstruction(FetchQuestionAndAnswers.Count);

                        bool answerMoreQuestion = true;
                        while (answerMoreQuestion)
                        {
                            countAnsweredQuestions++;
                            if (FetchQuestionAndAnswers.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                            {
                                UIMethod.PrintNoMoreQuestionMessage();
                            }
                            else
                            {
                                QuestionandAnswer randomlySelectedQuestion = LogicMethod.FetchRandomQuestion(FetchQuestionAndAnswers, randomQuestion);
                                UIMethod.PrintQuestionToUser(countAnsweredQuestions, randomlySelectedQuestion.QuestionText);
                                UIMethod.WriteEmptyLine();

                                UIMethod.PrintRandomQuestion(randomlySelectedQuestion.ListofQuestionandAnswers);
                                UIMethod.WriteEmptyLine();
                                UIMethod.PrintAnswerInputInstruction();
                                string userAnswer = UIMethod.TakeUserAnswer();
                                totalAnswerCounter++;
                                UIMethod.WriteEmptyLine();
                                int holdTheCounter = LogicMethod.CheckCorrectAnswer(userAnswer, randomlySelectedQuestion.CorrectAnswerText);
                                totalWinCounter = LogicMethod.CheckCorrectCount(holdTheCounter, totalWinCounter);
                                FetchQuestionAndAnswers.Remove(randomlySelectedQuestion);

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






