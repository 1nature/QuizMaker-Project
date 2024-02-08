using System.Xml.Serialization;
using static QuizMaker.QnAClass;

namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continuePlaying = true;
            int overallAnswerCounter = 0;

            UIMethod.QuizWelcomeMessage();
            UIMethod.WriteEmptyLine();

            var QuestionList = new List<QuestionandAnswer>();

            continuePlaying = UIMethod.QuizzerDecision();
            UIMethod.WriteEmptyLine();

            if (!continuePlaying)
            {
                UIMethod.DisplayQuitMessage();
            }
            else
            {
                UIMethod.NumberOfQuestionMessage();
                int numberOfQuizzerQuestions = int.Parse(Console.ReadLine());

                int questionPosition = 0; int answerPosition = 0;
                for (int quizzerReplyIndex = 0; quizzerReplyIndex < numberOfQuizzerQuestions; quizzerReplyIndex++)
                {
                    var EachQuestionInput = new QuestionandAnswer();
                    EachQuestionInput.QuestionText = UIMethod.DisplayQuizzerInstruction();
                    UIMethod.WriteEmptyLine();

                    int numberOfOptions = UIMethod.GiveNumberOfOptions();
                    UIMethod.WriteEmptyLine();
                    UIMethod.ShowOptionsMessage();

                    for (int optionIndex = 0; optionIndex < numberOfOptions; optionIndex++)
                    {
                        string quizAnswerOptions = UIMethod.AddTheOption();
                        EachQuestionInput.ListofQuestionandAnswers.Add(quizAnswerOptions);
                    }

                    UIMethod.WriteEmptyLine();
                    UIMethod.ShowCorrectAnswerInputMessage();
                    EachQuestionInput.CorrectAnswerText = UIMethod.AddCorrectOption();
                    UIMethod.WriteEmptyLine();

                    QuestionList.Add(EachQuestionInput);
                }

                XmlSerializer writer = new XmlSerializer(typeof(List<QuestionandAnswer>));
                var path = @"QuestionList.xml";
                using (FileStream file = File.Create(path))
                {
                    writer.Serialize(file, QuestionList);
                }
                //To the user
                Console.WriteLine($"There are {numberOfQuizzerQuestions} questions available");

                using (FileStream file = File.OpenRead(path))
                {
                    QuestionList = writer.Deserialize(file) as List<QuestionandAnswer>;
                }

                Console.WriteLine(QuestionList.Count);
                Console.WriteLine(QuestionList.ToString);
                
                //var EachQuestionOutput = new QuestionandAnswer();
            }




            //start again
            UIMethod.DisplayUserInstruction();
            //LogicMethod.UserQuestion(TheQuiz.QuestionText);
            UIMethod.WriteEmptyLine();
            //LogicMethod.UserAnswerOptions(TheQuiz.ListofQuestionandAnswers);
            string userAnswer = UIMethod.TakeUserAnswer();
            UIMethod.WriteEmptyLine();

            //if (userAnswer != TheQuiz.CorrectAnswer)
            //{
            //    UIMethod.LossMessage();
            //}
            //else
            //{
            //    UIMethod.WinMessage();
            //}




        }
    }
}





