using System.Xml.Serialization;
using static QuizMaker.QnAClass;

namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueStoringQuestion = true;
            bool continueAnsweringQuestion = true;
            int totalWinCounter = 0;
            int totalAnswerCounter = 0;
            int questionInputCounter = 0;

            UIMethod.QuizWelcomeMessage();
            UIMethod.WriteEmptyLine();

            var QuestionList = new List<QuestionandAnswer>();

            continueStoringQuestion = UIMethod.StoreQuestion();
            UIMethod.WriteEmptyLine();

            if (!continueStoringQuestion)
            {
                UIMethod.DisplayQuitMessage();
            }
            else
            {
                UIMethod.NumberOfQuestionMessage();
                int numberOfQuizzerQuestions = int.Parse(Console.ReadLine());

                for (int quizzerReplyIndex = 0; quizzerReplyIndex < numberOfQuizzerQuestions; quizzerReplyIndex++)
                {
                    questionInputCounter++;
                    var EachQuestionInput = new QuestionandAnswer();
                    EachQuestionInput.QuestionText = UIMethod.DisplayQuizzerInstruction(questionInputCounter);
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

                using (FileStream file = File.OpenRead(path))
                {
                    QuestionList = writer.Deserialize(file) as List<QuestionandAnswer>;
                }

                bool isValid = true;
                if (isValid)
                {
                    while (continueAnsweringQuestion)
                    {
                        var randomQuestion = new Random();
                        int indexOfRandomQuestion = randomQuestion.Next(QuestionList.Count);
                        QuestionandAnswer randomlySelectedQuestion = QuestionList[indexOfRandomQuestion];
                        
                        UIMethod.DisplayUserInstruction();
                        Console.WriteLine($"Question: {randomlySelectedQuestion.QuestionText}"); //create a method
                        UIMethod.WriteEmptyLine();
                        int optionCounter = Constant.COUNT_OPTION;
                        foreach (var option in randomlySelectedQuestion.ListofQuestionandAnswers)
                        {
                            Console.WriteLine($"Option {optionCounter}: {option}"); //create a method
                            optionCounter++;
                        }
                        UIMethod.WriteEmptyLine();
                        Console.WriteLine("Type your answer below"); //create a method
                        string userAnswer = UIMethod.TakeUserAnswer();
                        totalAnswerCounter++;
                        UIMethod.WriteEmptyLine();

                        if (userAnswer != randomlySelectedQuestion.CorrectAnswerText)
                        {
                            UIMethod.LossMessage();
                        }
                        else
                        {
                            UIMethod.WinMessage();
                            totalWinCounter++;
                        }
                        QuestionList.Remove(randomlySelectedQuestion);

                        if (numberOfQuizzerQuestions < Constant.MINIMUM_NUMBER_OF_QUESTION || QuestionList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                        {
                            UIMethod.NoMoreQuestion();
                            UIMethod.CalculateWinningScore(totalWinCounter, totalAnswerCounter);
                            break;
                        }
                        else
                        {
                            continueAnsweringQuestion = UIMethod.AnswerAnotherQuestion();
                            if (continueAnsweringQuestion)
                            {
                                isValid = true;
                            }
                            else
                            {
                                continueAnsweringQuestion = false;
                                UIMethod.CalculateWinningScore(totalWinCounter, totalAnswerCounter);
                                Environment.Exit(0);
                            }
                        }
                    }
                }
            }
        }
    }
}





