namespace QuizMaker
{
    //[Serializable]
    public static class QnAClass
    {
        public class QuestionandAnswer
        {
            public string QuestionText;
            public string OptionText;
            public List<string> ListofQuestionandAnswers = new List<string>();
            public string CorrectAnswerText;



            public void CheckCorrectAnswer()
            {
                foreach (string answer in ListofQuestionandAnswers)
                {
                    if (answer != CorrectAnswerText)
                    {
                        
                        UIMethod.LossMessage();
                        break;
                    }
                    else {UIMethod.WinMessage(); }
                }
            }

            public void ShowQuestionandOptionToUser(string theQuestion, List<string>questionOptions)
            {
                QuestionText = theQuestion;
                ListofQuestionandAnswers = questionOptions;
                Console.WriteLine($"The question is: {QuestionText}");
                Console.WriteLine($"The options are: {ListofQuestionandAnswers}");
            }

            //public List<string>sssss CreateNewList()
            //{
            //    string thatQuestion = QuestionText;
            //    AnswerOption.Add(thatQuestion);
            //    return AnswerOption;
            //}


            public void ShowQuestion()
            {
                UIMethod.DisplayQuizzerInstruction();
                string userAnswer = Console.ReadLine();
                userAnswer = QuestionText;
                //Console.WriteLine(QuestionText);
            }

            public void ShowOption()
            {
                int maxQuestionNumber = UIMethod.GiveNumberOfOptions();
                UIMethod.ShowOptionsMessage();

                for (int i = 0; i < maxQuestionNumber; i++)
                {
                    string answerOption = Console.ReadLine();
                    ListofQuestionandAnswers.Add(answerOption);
                }
            }

            public void ShowCorrect()
            {
                UIMethod.DisplayQuizzerInstruction();
                string userAnswer = Console.ReadLine();

                if (userAnswer != null)
                {
                    ListofQuestionandAnswers.Add(userAnswer);
                }
            }

            //public string OptionA;

            //string QuestionText, string OptionA, string OptionB, string OptionC, string OptionD
            public void SetQuestionAndAnswer()
            {
                UIMethod.DisplayQuizzerInstruction();
                string userAnswer = Console.ReadLine();
                //Console.WriteLine(userAnswer);

                Console.WriteLine("Add at least four options");

                for (int i = 0; i <= 4; i++)
                {
                    string theOption = Console.ReadLine();
                    //Console.WriteLine(theOption);
                }
            }

            public void XXXXXXX(string yyyyyA)
            {

            }

            public void SetTheMainQuestion()
            {
                UIMethod.DisplayQuizzerInstruction();
                string userAnswer = Console.ReadLine();
                Console.WriteLine(userAnswer);
            }

            //There may be a need to separate input from display

            public void InputTheCorrectOption()
            {
                string theCorrectOption = Console.ReadLine();
                Console.WriteLine(theCorrectOption);
            }

            public void InputTheOtherOptions()
            {
                Console.WriteLine("Add the options");

                for (int i = 0; i <= 4; i++)
                {
                    string theOption = Console.ReadLine();
                    Console.WriteLine(theOption);
                }
            }

            public string StateFirstOption()
            {
                string firstOption = Console.ReadLine();
                return firstOption;
            }

            public string StateSecondOption()
            {
                string secondOption = Console.ReadLine();
                return secondOption;
            }


            public string SetTheCorrectAnswer(string correctAnswer)
            {
                string trueAnswer = correctAnswer;
                return trueAnswer;
            }

            public string CheckTheCorrectAnswer()
            {
                string xxxx = "";
                return xxxx;
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

    }
}
