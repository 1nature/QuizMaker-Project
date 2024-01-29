namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continuePlaying = true;
            int overallAnswerCounter = 0;

            UIMethods.GameWelcomeMessage();

            //QuizQuestionandAnswer firstQnA = new QuizQuestionandAnswer();
            //firstQnA.QuestionText = "What is the capital of the United Kingdom?";
            //firstQnA.OptionA = "Berlin";
            //firstQnA.OptionB = "Ottawa";
            //firstQnA.OptionC = "London";
            //firstQnA.OptionD = "Abuja";
            //firstQnA.SetTheAnswer(firstQnA.OptionC);

            //QuizQuestionandAnswer secondQnA = new QuizQuestionandAnswer();
            //secondQnA.QuestionText = "What is the my favourite sports team?";
            //secondQnA.OptionA = "Arsenal";
            //secondQnA.OptionB = "Bayern Munich";
            //secondQnA.OptionC = "Enyimba";
            //secondQnA.OptionD = "Rapid Vienna";
            //secondQnA.SetTheAnswer(secondQnA.OptionA);

            //QuizQuestionandAnswer thirdQnA = new QuizQuestionandAnswer();
            //secondQnA.QuestionText = "Which country was Elon Musk born?";
            //secondQnA.OptionA = "Nigeria";
            //secondQnA.OptionB = "United States of America";
            //secondQnA.OptionC = "Canada";
            //secondQnA.OptionD = "South Africa";
            //secondQnA.SetTheAnswer(thirdQnA.OptionD);

            //QuizQuestionandAnswer fourthQnA = new QuizQuestionandAnswer();
            //secondQnA.QuestionText = "Which planet in the Milky Way is the hottest?";
            //secondQnA.OptionA = "Venus";
            //secondQnA.OptionB = "Earth";
            //secondQnA.OptionC = "Uranus";
            //secondQnA.OptionD = "Saturn";
            //secondQnA.SetTheAnswer(fourthQnA.OptionA);

            var random = new Random();

            UIMethods.GameInstruction();

            continuePlaying = UIMethods.PlayQuizDecision();

            if (!continuePlaying)
            {
                UIMethods.GameQuitMessage();
            }


            while (continuePlaying)
            {
                UIMethods.WriteEmptyLine();

                List<QuizQuestionandAnswer> listOfQuizzes = new List<QuizQuestionandAnswer>();
                listOfQuizzes.Add(new QuizQuestionandAnswer());


                listOfQuizzes.Add(listOfQuizzes.First());
                Console.WriteLine(listOfQuizzes.First());

                //List<object> listOfQuizzes = new List<object>();
                //listOfQuizzes.Add();


                foreach (var quiz in listOfQuizzes)
                {
                    Console.WriteLine(quiz);
                    //Console.WriteLine($"quiz: {0}", quiz.QuestionText, quiz.OptionA, quiz.OptionB, quiz.OptionC, quiz.OptionD);
                }

                //var arrayOfQuizQuestions = new QuizQuestionandAnswer[] { firstQnA, secondQnA, thirdQnA, fourthQnA };
                //int indexOfQuizQuestions = random.Next(arrayOfQuizQuestions.Length);
                //var randomlySelectedQuizQuestion = arrayOfQuizQuestions[indexOfQuizQuestions];


                //LC Example
                //List < QuizQuestionandAnswer > qnaList = new();

                //var item = qnaList[indexOfQuizQuestions];

                ////gameplay 

                //qnaList.Remove(item);



                //if (randomlySelectedQuizQuestion != null && randomlySelectedQuizQuestion == arrayOfQuizQuestions[0])
                //{
                //    UIMethods.WriteEmptyLine();
                //    Console.WriteLine(randomlySelectedQuizQuestion.QuestionText);
                //    UIMethods.WriteEmptyLine();

                //    string userAnswer = (Console.ReadLine()).ToLower();
                //    UIMethods.WriteEmptyLine();
                //    string correctAnswer = firstQnA.SetTheAnswer(firstQnA.OptionC).ToLower();

                //    int correctAnswerCounter = UIMethods.AnswerCounter(userAnswer, correctAnswer);
                //    UIMethods.WriteEmptyLine();

                //    if (correctAnswerCounter >= Constants.MINIMUM_QUIZ_SCORE)
                //    {
                //        overallAnswerCounter += correctAnswerCounter;
                //    }
                //    Console.WriteLine(correctAnswerCounter); //for checks
                //    UIMethods.WriteEmptyLine();

                //}


                break;
            }
        }
    }
}

public class QuizQuestionandAnswer
{

    public string QuestionText;
    public string OptionA;
    public string OptionB;
    public string OptionC;
    public string OptionD;


    public string SetTheCorrectAnswer(string correctAnswer)
    {
        string trueAnswer = correctAnswer;
        return trueAnswer;
    }

    public string SetTheAnswer(string answerOption)
    {
        string answer = answerOption;
        return answer;
    }


    public string[] GetOption()
    {
        string[] options = { "Berlin", "Ottawa", "London", "Abuja" };
        //Index of? Check the link on my Uni computer
        return options;
    }
}




