namespace QuizMaker
{
    internal class QuizMakerProgram
    {
        static void Main(string[] args)
        {
            bool continuePlaying = true;
            int overallAnswerCounter = 0;

            QuizMakerUIMethods.GameWelcomeMessage();

            QuizQuestionandAnswer firstQnA = new QuizQuestionandAnswer();
            firstQnA.QuestionA = "What is the capital of the United Kingdom?";
            firstQnA.OptionA = "Berlin";
            firstQnA.OptionB = "Ottawa";
            firstQnA.OptionC = "London";
            firstQnA.OptionD = "Abuja";
            firstQnA.SetTheAnswer(firstQnA.OptionC);

            QuizQuestionandAnswer secondQnA = new QuizQuestionandAnswer();
            secondQnA.QuestionA = "What is the my favourite sports team?";
            secondQnA.OptionA = "Arsenal";
            secondQnA.OptionB = "Bayern Munich";
            secondQnA.OptionC = "Enyimba";
            secondQnA.OptionD = "Rapid Vienna";
            secondQnA.SetTheAnswer(secondQnA.OptionA);

            QuizQuestionandAnswer thirdQnA = new QuizQuestionandAnswer();
            secondQnA.QuestionA = "Which country was Elon Musk born?";
            secondQnA.OptionA = "Nigeria";
            secondQnA.OptionB = "United States of America";
            secondQnA.OptionC = "Canada";
            secondQnA.OptionD = "South Africa";
            secondQnA.SetTheAnswer(thirdQnA.OptionD);

            QuizQuestionandAnswer fourthQnA = new QuizQuestionandAnswer();
            secondQnA.QuestionA = "Which planet in the Milky Way is the hottest?";
            secondQnA.OptionA = "Venus";
            secondQnA.OptionB = "Earth";
            secondQnA.OptionC = "Uranus";
            secondQnA.OptionD = "Saturn";
            secondQnA.SetTheAnswer(fourthQnA.OptionA);

            var random = new Random();

            QuizMakerUIMethods.GameInstruction();

            continuePlaying = QuizMakerUIMethods.PlayQuizDecision();

            if (!continuePlaying)
            {
                QuizMakerUIMethods.GameQuitMessage();
            }


            while (continuePlaying)
            {
                QuizMakerUIMethods.WriteEmptyLine();
                var arrayOfQuizQuestions = new QuizQuestionandAnswer[] { firstQnA, secondQnA, thirdQnA, fourthQnA };
                int indexOfQuizQuestions = random.Next(arrayOfQuizQuestions.Length);
                var randomlySelectedQuizQuestion = arrayOfQuizQuestions[indexOfQuizQuestions];

                if (randomlySelectedQuizQuestion != null && randomlySelectedQuizQuestion == arrayOfQuizQuestions[0])
                {
                    QuizMakerUIMethods.WriteEmptyLine();
                    Console.WriteLine(randomlySelectedQuizQuestion.QuestionA);
                    QuizMakerUIMethods.WriteEmptyLine();

                    string userAnswer = (Console.ReadLine()).ToLower();
                    QuizMakerUIMethods.WriteEmptyLine();
                    string correctAnswer = firstQnA.SetTheAnswer(firstQnA.OptionC).ToLower();

                    int correctAnswerCounter = QuizMakerUIMethods.AnswerCounter(userAnswer, correctAnswer);
                    QuizMakerUIMethods.WriteEmptyLine();

                    if (correctAnswerCounter >= QuizMakerConstants.MINIMUM_QUIZ_SCORE)
                    {
                        overallAnswerCounter += correctAnswerCounter;
                    }
                    Console.WriteLine(correctAnswerCounter); //for checks
                    QuizMakerUIMethods.WriteEmptyLine();

                }
                else if (randomlySelectedQuizQuestion != null && randomlySelectedQuizQuestion == arrayOfQuizQuestions[1])
                {
                    QuizMakerUIMethods.WriteEmptyLine();
                    Console.WriteLine(randomlySelectedQuizQuestion.QuestionA);
                    QuizMakerUIMethods.WriteEmptyLine();

                    string userAnswer = (Console.ReadLine()).ToLower();
                    QuizMakerUIMethods.WriteEmptyLine();
                    string correctAnswer = secondQnA.SetTheAnswer(secondQnA.OptionA).ToLower();

                    int correctAnswerCounter = QuizMakerUIMethods.AnswerCounter(userAnswer, correctAnswer);
                    QuizMakerUIMethods.WriteEmptyLine();

                    if (correctAnswerCounter >= QuizMakerConstants.MINIMUM_QUIZ_SCORE)
                    {
                        overallAnswerCounter += correctAnswerCounter;
                    }
                    Console.WriteLine(correctAnswerCounter); //for checks
                    QuizMakerUIMethods.WriteEmptyLine();

                }
                else if (randomlySelectedQuizQuestion != null && randomlySelectedQuizQuestion == arrayOfQuizQuestions[2])
                {
                    QuizMakerUIMethods.WriteEmptyLine();
                    Console.WriteLine(randomlySelectedQuizQuestion.QuestionA);
                    QuizMakerUIMethods.WriteEmptyLine();

                    string userAnswer = (Console.ReadLine()).ToLower();
                    QuizMakerUIMethods.WriteEmptyLine();
                    string correctAnswer = thirdQnA.SetTheAnswer(thirdQnA.OptionD).ToLower();

                    int correctAnswerCounter = QuizMakerUIMethods.AnswerCounter(userAnswer, correctAnswer);
                    QuizMakerUIMethods.WriteEmptyLine();

                    if (correctAnswerCounter >= QuizMakerConstants.MINIMUM_QUIZ_SCORE)
                    {
                        overallAnswerCounter += correctAnswerCounter;
                    }
                    Console.WriteLine(correctAnswerCounter); //for checks
                    QuizMakerUIMethods.WriteEmptyLine();

                }
                else if (randomlySelectedQuizQuestion != null && randomlySelectedQuizQuestion == arrayOfQuizQuestions[3])
                {
                    QuizMakerUIMethods.WriteEmptyLine();
                    Console.WriteLine(randomlySelectedQuizQuestion.QuestionA);
                    QuizMakerUIMethods.WriteEmptyLine();

                    string userAnswer = (Console.ReadLine()).ToLower();
                    QuizMakerUIMethods.WriteEmptyLine();
                    string correctAnswer = fourthQnA.SetTheAnswer(fourthQnA.OptionA).ToLower();

                    int correctAnswerCounter = QuizMakerUIMethods.AnswerCounter(userAnswer, correctAnswer);
                    QuizMakerUIMethods.WriteEmptyLine();

                    if (correctAnswerCounter >= QuizMakerConstants.MINIMUM_QUIZ_SCORE)
                    {
                        overallAnswerCounter += correctAnswerCounter;
                    }
                    Console.WriteLine(correctAnswerCounter); //for check
                    QuizMakerUIMethods.WriteEmptyLine();
                }
                break;
            }
        }
    }
}

public class QuizQuestionandAnswer
{

    public string QuestionA;
    public string OptionA;
    public string OptionB;
    public string OptionC;
    public string OptionD;

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




