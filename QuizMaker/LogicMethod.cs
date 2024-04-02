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

        public static void UserQuestion(string newQuestion)
        {
            Console.WriteLine(newQuestion);
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

        public static int CheckCorrectCount(int holdCount, int winCount)
        {
            if (holdCount == Constant.QUIZ_DECISION_YES)
            {
                winCount++;
            }
            return winCount;
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
