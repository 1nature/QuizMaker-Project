﻿using System.Xml.Serialization;

namespace QuizMaker
{
    public static class LogicMethod
    {
        public static char CheckRestartStoreCondition(bool condition, char theInput)
        {
            if (condition && theInput == Constant.QUIZ_TYPE_STOREONLY)
            {
                theInput = UIMethod.GetQuizLineResponse();
            }
            return theInput;
        }

        public static char CheckRestartAnsweringCondition(bool restartVariable, char userReply)
        {
            if (restartVariable)
            {
                userReply = UIMethod.GetQuizLineResponse();
            }
            return userReply;
        }

        public static bool PerformAnswerOption(Random randQuestion)
        {
            bool answerMoreQuestions = true;
            bool showScoreCondition = true;
            int numOfDisplayedQuestions = 0;
            int numOfAnsweredQuestions = 0;
            List<QuestionandAnswer> fetchStoredQuestions = ReadQuizFromXml();
            int remainderQuestion = fetchStoredQuestions.Count;
            int incrementOperator = 0;

            while (answerMoreQuestions)
            {
                numOfDisplayedQuestions++;
                if (fetchStoredQuestions.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                {
                    break;
                }
                else
                {
                    QuestionandAnswer chosenQuestion = FetchRandomQuestion(fetchStoredQuestions, randQuestion);
                    QuestionandAnswer retrievedText = UIMethod.DisplayQuestionAndAnswersToUser(chosenQuestion, numOfDisplayedQuestions);
                    string userResponse = UIMethod.TakeUserAnswer();
                    numOfAnsweredQuestions++;
                    bool holdTheCondition = RemoveQuestionFromList(userResponse, retrievedText, fetchStoredQuestions);
                    remainderQuestion--;
                    int correctAnswerCounter = UIMethod.DisplayWinOrLossMessageAndExit(holdTheCondition);
                    incrementOperator += correctAnswerCounter;
                    showScoreCondition = UIMethod.ShowScoreForRestartOrQuit(remainderQuestion, incrementOperator, numOfAnsweredQuestions);
                }
            }
            return showScoreCondition;
        }

        public static bool RemoveQuestionFromList(string getAnswer, QuestionandAnswer removeText, List<QuestionandAnswer> removeFetched)
        {
            bool compareAnswer = CheckCorrectAnswer(getAnswer, removeText.CorrectAnswerText);
            removeFetched.Remove(removeText); 
            return compareAnswer;
        }

        public static bool CheckCorrectAnswer(string theAnswer, string correctAnswer)
        {
            bool forChecksValue = true;
            if (theAnswer != correctAnswer)
            {
                forChecksValue = false;
            }
            else
            {
                forChecksValue = true;
            }
            return forChecksValue;
        }

        public static bool ExitAnswerOption(int exitVariable)
        {
            bool exitCondition = true;
            if (exitVariable < Constant.MINIMUM_NUMBER_OF_QUESTION)
            {
                exitCondition = false;
            }
            return exitCondition;
        }

        public static QuestionandAnswer FetchRandomQuestion(List<QuestionandAnswer> randomQuestionList, Random randomQuest)
        {
            int indexOfRandomQuest = randomQuest.Next(randomQuestionList.Count);
            QuestionandAnswer returnedQuestion = randomQuestionList[indexOfRandomQuest];
            return returnedQuestion;
        }

        public static int FetchQuestionsAndDisplayInstruction(char answerOption)
        {
            var getQuestions = new List<QuestionandAnswer>();
            if (answerOption == Constant.QUIZ_TYPE_ANSWERONLY || answerOption == Constant.QUIZ_TYPE_STOREANDANSWER)
            {
                getQuestions = LogicMethod.ReadQuizFromXml();
            }
            return getQuestions.Count;
        }

        public static void SerializeData(List<QuestionandAnswer> serialList)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<QuestionandAnswer>));

            using (FileStream file = File.Create(Constant.THE_XML_PATH))
            {
                writer.Serialize(file, serialList);
            }
        }

        public static List<QuestionandAnswer> ReadQuizFromXml()
        {
            XmlSerializer theVariable = new XmlSerializer(typeof(List<QuestionandAnswer>));
            List<QuestionandAnswer> storedList = null;

            if (File.Exists(Constant.THE_XML_PATH))
            {
                using (FileStream file = File.OpenRead(Constant.THE_XML_PATH))
                {
                    storedList = theVariable.Deserialize(file) as List<QuestionandAnswer>;
                }
            }
            return storedList;
        }

        public static bool ContinueOrQuitQuiz(int returnValue)
        {
            bool continueOrQuitVariable;
            if (returnValue == Constant.QUIZ_DECISION_YES)
            {
                continueOrQuitVariable = true;
            }
            else { continueOrQuitVariable = false; }
            return continueOrQuitVariable;
        }
    }
}
