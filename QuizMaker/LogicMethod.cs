using System.Xml.Serialization;
using static QuizMaker.QnAClass;

namespace QuizMaker
{
    public static class LogicMethod
    {
        public static XmlSerializer DeclareXmlProperty()
        {
            XmlSerializer theWriter = new XmlSerializer(typeof(List<QuestionandAnswer>));
            //(XmlSerializer writer = new XmlSerializer(typeof(List)) ;)
            return theWriter;
        }

        public static void UserQuestion(string newQuestion)
        {
            Console.WriteLine(newQuestion);
        }

        public static void QuestionAnswerProxy(bool questionProxy, bool answerProxy, int winProxyCounter, int answerProxyCounter)
        {
            if (questionProxy)
            {
                answerProxy = true;
            }
            else
            {
                answerProxy = false;
                UIMethod.CalculateWinningScore(winProxyCounter, answerProxyCounter);
            }
        }

        public static void RepeatPlay(bool keepPlayingProxy, char quizSelectionProxy, int winProxy, int answerProxy)
        {
            if (keepPlayingProxy)
            {
                UIMethod.ShowQuizGameInstruction();
                quizSelectionProxy = UIMethod.GetQuizLineResponse();
                UIMethod.WriteEmptyLine();
            }
            else
            {
                keepPlayingProxy = false;
                UIMethod.DisplayQuitMessage();
                UIMethod.CalculateWinningScore(winProxy, answerProxy);
            }
        }

        public static void CheckCorrectAnswer(string theAnswer, string correctAnswer, int totalCounter)
        {
            if (theAnswer != correctAnswer)
            {
                UIMethod.LossMessage();
            }
            else
            {
                UIMethod.WinMessage();
                totalCounter++;
            }
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
            //XmlSerializer writer = new XmlSerializer(typeof(List<QuestionandAnswer>));
            //string path = Constant.THE_XML_PATH;
            using (FileStream file = File.Create(Constant.THE_XML_PATH))
            {
                writer.Serialize(file, serialList);
            }
        }

        //public static void SerializeData(string path, XmlSerializer variable, List<QuestionandAnswer> serialList)
        //{
        //    XmlSerializer writer = new XmlSerializer(typeof(List<QuestionandAnswer>));
        //    path = @"QuestionList.xml";

        //    using (FileStream file = File.Create(path))
        //    {
        //        variable.Serialize(file, serialList);
        //    }
        //}

        public static void ReadQuizFromXml(List<QuestionandAnswer> storedList)
        {
            XmlSerializer theVariable = DeclareXmlProperty();
            //XmlSerializer theVariable = new XmlSerializer(typeof(List<QuestionandAnswer>));
            using (FileStream file = File.OpenRead(Constant.THE_XML_PATH))
            {
                storedList = theVariable.Deserialize(file) as List<QuestionandAnswer>;
            }

            //using (FileStream file = File.OpenRead(thePath))
            //{
            //    storedList = theVariable.Deserialize(file) as List<QuestionandAnswer>;
            //}
        }

        //public static string FetchRandomQuestion(object randex, string xQuestion)//List<QuestionandAnswer>theList)
        //{
        //    var theRandomQuestion = new Random();
        //    int indexOfRandomQuestion = theRandomQuestion.Next(theList.Count);
        //   //QuestionandAnswer randomlySelectedQuestion = QuestionList[indexOfRandomQuestion];
        //    //QuestionandAnswer theRandomlySelected = theList[indexOfRandomQuestion];
        //    //return theRandomlySelected;
        //}
    }
}
