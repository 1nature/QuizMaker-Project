using System.Xml.Serialization;
using static QuizMaker.QnAClass;

namespace QuizMaker
{
    public static class LogicMethod
    {
        public static XmlSerializer DeclareXmlProperty()
        {
            XmlSerializer theWriter = new XmlSerializer(typeof(List<QuestionandAnswer>));
            return theWriter;
        }

        public static QuestionandAnswer FetchRandomQuestion(List<QuestionandAnswer> randomQuestionList, Random randomQuest)
        {
            int indexOfRandomQuest = randomQuest.Next(randomQuestionList.Count);
            QuestionandAnswer answerX = randomQuestionList[indexOfRandomQuest];
            return answerX;
        }

        public static int RemoveText(string getAnswer, QuestionandAnswer removeText, List<QuestionandAnswer> removeFetched)
        {
            int holdTheCounter = LogicMethod.CheckCorrectAnswer(getAnswer, removeText.CorrectAnswerText);
            removeFetched.Remove(removeText);
            return holdTheCounter;
        }

        public static int CheckCorrectAnswer(string theAnswer, string correctAnswer)
        {
            int totalCounter = 0;
            if (theAnswer != correctAnswer)
            {
                totalCounter = Constant.QUIZ_DECISION_NO;
                UIMethod.LossMessage();
            }
            else
            {
                totalCounter = Constant.QUIZ_DECISION_YES;
                UIMethod.WinMessage();
            }
            return totalCounter;
        }

        public static void TakeAnswerOption(int optionTotal, string theOption, List<string> optionInput)
        {
            UIMethod.ShowOptionsMessage();
            for (int Index = 0; Index < optionTotal; Index++)
            {
                theOption = UIMethod.AddTheOption();
                optionInput.Add(theOption);
            }
        }

        public static void SerializeData(List<QuestionandAnswer> serialList)
        {
            XmlSerializer writer = DeclareXmlProperty();
            using (FileStream file = File.Create(Constant.THE_XML_PATH))
            {
                writer.Serialize(file, serialList);
            }
        }

        public static List<QuestionandAnswer> ReadQuizFromXml()
        {
            XmlSerializer theVariable = DeclareXmlProperty();
            List<QuestionandAnswer> storedList;

            using (FileStream file = File.OpenRead(Constant.THE_XML_PATH))
            {
                storedList = theVariable.Deserialize(file) as List<QuestionandAnswer>;
            }
            return storedList;
        }

        public static int CheckConditionOne(List<QuestionandAnswer> storageList)
        {
            int numberOfQuizzerQuestions = UIMethod.GetIntFromUser("Input the number of questions you want to store: \n");
            int questionDecrement = numberOfQuizzerQuestions;

            for (int quizzerReplyIndex = 0; quizzerReplyIndex < numberOfQuizzerQuestions; quizzerReplyIndex++)
            {
                QuestionandAnswer newQna = (QuestionandAnswer)UIMethod.GetQuestionandAnswerObjectFromUser();
                storageList.Add(newQna);
                questionDecrement--;
            }
            LogicMethod.SerializeData(storageList);
            return questionDecrement;
        }

        public static QuestionandAnswer RetrieveQuestion(List<QuestionandAnswer> fetch, Random theRand, int countAnswer)
        {
            QuestionandAnswer randomlySelectedQuestion = LogicMethod.FetchRandomQuestion(fetch, theRand);
            UIMethod.PrintQuestionToUser(countAnswer, randomlySelectedQuestion.QuestionText);
            UIMethod.PrintRandomQuestion(randomlySelectedQuestion.ListofQuestionandAnswers);
            UIMethod.PrintAnswerInputInstruction();
            return randomlySelectedQuestion;
        }

        public static bool CheckQuizConditionThree(char select, int winKounter, int maxCounter)
        {
            int parseCounter = winKounter;
            UIMethod.PrintNoMoreQuestionMessage();
            UIMethod.ShowWinningScore(winKounter, maxCounter);
            bool keepPlay = UIMethod.RestartQuiz();
            char newSelect = UIMethod.RepeatPlay(keepPlay, select, parseCounter, maxCounter);
            select = newSelect;
            return keepPlay;
        }

        public static int RestartQuestionNumber(bool restartCondition)
        {
            int answerCount = 0;
            if (restartCondition)
            {
                answerCount = 0;
            }
            return answerCount;
        }
    }
}
