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

        //public static void QuestionAnswerProxy(bool questionProxy, bool answerProxy, int winProxyCounter, int answerProxyCounter)
        //{
        //    if (questionProxy)
        //    {
        //        answerProxy = true;
        //    }
        //    else
        //    {
        //        answerProxy = false;
        //        UIMethod.CalculateWinningScore(winProxyCounter, answerProxyCounter);
        //    }
        //}

        public static int CheckCorrectAnswer(string theAnswer, string correctAnswer) //change from int to bool?
        {
            int totalCounter = 0;
            if (theAnswer != correctAnswer)
            {
                UIMethod.LossMessage();
            }
            else
            {
                totalCounter++;
                UIMethod.WinMessage();
            }
            return totalCounter;
        }

        public static void TakeAnswerOption(int optionTotal, string theOption, List<string> optionInput)
        {
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
    }
}
