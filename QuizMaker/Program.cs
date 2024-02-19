using System.Xml.Serialization;
using static QuizMaker.QnAClass;

namespace QuizMaker
{
    internal class Program
    {
        //Two modes, do you want to play or do you want to add questions?
        //if you want to play, it will load the existing questions
        //if you want to add questions, it will allow addition of questions
        //if you want to do both, you add questions first, then play next, or vice versa - this comes first!
        static void Main(string[] args)
        {
            bool continueStoringQuestion = true;
            bool continueAnsweringQuestion = true;
            bool addnAnswer = true;
            bool addOnly = true;
            bool answerOnly = true;
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
                UIMethod.ShowQuizGameInstruction();
                char quizSelection = UIMethod.GetQuizLineResponse();
                UIMethod.WriteEmptyLine();

                if (quizSelection == Constant.QUIZ_TYPE_STOREnANSWER || quizSelection == Constant.QUIZ_TYPE_STOREONLY)
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

                        if (quizSelection == Constant.QUIZ_TYPE_STOREONLY && numberOfQuizzerQuestions < Constant.MINIMUM_NUMBER_OF_QUESTION)
                        {
                            break;
                        }
                    }
                }

                XmlSerializer writer = new XmlSerializer(typeof(List<QuestionandAnswer>));
                var path = @"QuestionList.xml";
                using (FileStream file = File.Create(path))
                {
                    writer.Serialize(file, QuestionList);
                }
                //should be able to stop here if I am adding questions only
                //should be able to start here if I am answering questions only
                Console.WriteLine("Number of questions stored");
                Console.WriteLine(QuestionList.Count);


                using (FileStream file = File.OpenRead(path))
                {
                    QuestionList = writer.Deserialize(file) as List<QuestionandAnswer>;
                }

                if (quizSelection == Constant.QUIZ_TYPE_STOREnANSWER || quizSelection == Constant.QUIZ_TYPE_ANSWERONLY)
                {
                    //bool isValid = true;
                    //if (isValid)
                    //{
                    //while (continueAnsweringQuestion)
                    //{

                    if (QuestionList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                    {
                        Console.WriteLine("You are out of questions");
                        Environment.Exit(0);
                    }
                    
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




                    //We get here later!
                    //if (numberOfQuizzerQuestions < Constant.MINIMUM_NUMBER_OF_QUESTION || QuestionList.Count < Constant.MINIMUM_NUMBER_OF_QUESTION)
                    //{
                    //    UIMethod.NoMoreQuestion();
                    //    UIMethod.CalculateWinningScore(totalWinCounter, totalAnswerCounter);
                    //    break;
                    //}
                    //else
                    //{
                    //    continueAnsweringQuestion = UIMethod.AnswerAnotherQuestion();
                    //    if (continueAnsweringQuestion)
                    //    {
                    //        isValid = true;
                    //    }
                    //    else
                    //    {
                    //        continueAnsweringQuestion = false;
                    //        UIMethod.CalculateWinningScore(totalWinCounter, totalAnswerCounter);
                    //        Environment.Exit(0);
                    //    }
                    //}
                    // }
                    //}
                    //}
                }
            }
        }
    }
}





