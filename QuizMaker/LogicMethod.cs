using System.IO;
using System.Xml.Serialization;
using static QuizMaker.QnAClass;

namespace QuizMaker
{
    public static class LogicMethod
    {
        
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

        public static void TakeAnswerOption(int optionTotal, string theOption, List<string>optionInput)
        {
            for (int Index = 0; Index < optionTotal; Index++)
            {
                theOption = UIMethod.AddTheOption();
                optionInput.Add(theOption);
            }
        }

        public static void SerializeData(string path, XmlSerializer xxxxxx, List<QuestionandAnswer> serialList)
        {
            using (FileStream file = File.Create(path))
            {
                xxxxxx.Serialize(file, serialList);
            }
        }

        //public static void SerializeData(List<QuestionandAnswer> questionList, string path)
        //{

        //}

        //public static void SerializeNow()
        //{
        //    using (FileStream file = File.Create(path))
        //    {
        //        writer.Serialize(file, QuestionList);
        //    }
        //}
    }
}
