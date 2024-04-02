using static QuizMaker.QnAClass;

namespace QuizMaker
{
    public static class UIMethod
    {
        public static Object GetQuestionandAnswerObjectFromUser()
        {
            var EachQuestionInput = new QuestionandAnswer();
            string quizAnswerOptions = "";
            var QuestionList = new List<QuestionandAnswer>();
                        
            EachQuestionInput.QuestionText = TakeUserQuestion();
            WriteEmptyLine();
            int maximumOptions = GetIntFromUser("Input the number of options to your question?\n");
            WriteEmptyLine();
            LogicMethod.TakeAnswerOption(maximumOptions, quizAnswerOptions, EachQuestionInput.ListofQuestionandAnswers);
            WriteEmptyLine();
            EachQuestionInput.CorrectAnswerText = AddCorrectOption();
            WriteEmptyLine();
            return EachQuestionInput;
        }

        public static void QuizWelcomeMessage()
        {
            Console.WriteLine("Welcome!\n");
            Console.WriteLine("You can store your own questions to this quiz game");
            Console.WriteLine("Also, you can answer stored questions in the quiz game\n");
        }

        public static void DisplayQuitMessage()
        {
            Console.WriteLine("You have decided not to play the quiz game.");
        }

        public static bool RestartQuiz()
        {
            Console.WriteLine("Enter '1' to restart the quiz, or '0' to quit.\n");
            int restart;
            do
            {
                restart = int.Parse(Console.ReadLine());
                if (restart != Constant.QUIZ_DECISION_YES && restart != Constant.QUIZ_DECISION_NO)
                    Console.WriteLine("Not a valid input. Please try again");
            } while (restart != 1 && restart != 0);
            return (restart == Constant.QUIZ_DECISION_YES);
        }

        public static void ShowQuizGameInstruction()
        {
            Console.WriteLine("**************QUIZ INSTRUCTION*****************\n");
            Console.WriteLine("If you want to store and answer questions, enter 'A'");
            Console.WriteLine("If you want to store questions only, enter 'B'");
            Console.WriteLine("If you want to answer questions only, enter 'C'\n");
        }

        public static bool StoreQuestion()
        {
            Console.WriteLine("Enter '1' to store and/or answer question(s), or '0' to quit.\n");
            int makeQuizDecision;
            do
            {
                makeQuizDecision = int.Parse(Console.ReadLine());
                if (makeQuizDecision != Constant.QUIZ_DECISION_YES && makeQuizDecision != Constant.QUIZ_DECISION_NO)
                    Console.WriteLine("Not a valid input. Please try again");
            } while (makeQuizDecision != Constant.QUIZ_DECISION_YES && makeQuizDecision != Constant.QUIZ_DECISION_NO);
            return (makeQuizDecision == Constant.QUIZ_DECISION_YES);
        }

        public static int GetIntFromUser(string promt)
        {
            bool valid = false;
            int number = 0;
            while (!valid)
            {
                Console.WriteLine(promt);
                string theInput = Console.ReadLine() + "\n";

                if (int.TryParse(theInput, out number))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Not a valid input, please try again");
                }
            }
            return number;
        }

        public static char GetQuizLineResponse()
        {
            char quizLineChoice;
            do
            {
                quizLineChoice = char.Parse(Console.ReadLine());
                quizLineChoice = char.ToUpper(quizLineChoice);

                if (quizLineChoice != Constant.QUIZ_TYPE_STOREANDANSWER && quizLineChoice != Constant.QUIZ_TYPE_STOREONLY && quizLineChoice != Constant.QUIZ_TYPE_ANSWERONLY)
                    Console.WriteLine("Not a valid input. Please try again");

            } while (quizLineChoice != Constant.QUIZ_TYPE_STOREANDANSWER && quizLineChoice != Constant.QUIZ_TYPE_STOREONLY && quizLineChoice != Constant.QUIZ_TYPE_ANSWERONLY);
            return quizLineChoice;
        }

        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }

        public static void WinMessage()
        {
            Console.WriteLine("Correct answer. You have a score\n");
        }

        public static void LossMessage()
        {
            Console.WriteLine("Incorrect answer. You have no score\n");
        }

        public static void PrintNoMoreQuestionMessage()
        {
            Console.WriteLine("There are no more questions left to answer\n");
        }

        public static string TakeUserQuestion()
        {
            Console.WriteLine("Input your question\n"); // number: {questionInputTracker}\n");
            string quizzerReply = Console.ReadLine().ToLower() + "\n";
            return quizzerReply;
        }

        public static void ShowOptionsMessage()
        {
            Console.WriteLine("Input your options one after the other. One of them must be the correct answer\n");
        }
        public static bool AnswerAnotherQuestion()
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine("Enter '1' to answer another question, or '0' to quit the game.");
            int newQuestion = int.Parse(Console.ReadLine());
            WriteEmptyLine();
            return (newQuestion == Constant.QUIZ_DECISION_YES);
        }

        public static void ShowWinningScore(int currentScore, int maxScore)
        {
            Console.WriteLine("***********YOUR SCORE**************");
            decimal theResult = (decimal)currentScore / maxScore * 100;
            Console.WriteLine($"You scored {currentScore} question(s) out of {maxScore} OR {theResult}%\n");
        }

        public static void CalculateWinningScore(bool questionProxy, bool answerProxy, int currentScore, int maxScore)
        {
            int result = 0;

            if (questionProxy)
            {
                answerProxy = true;
            }
            else
            {
                answerProxy = false;
                ShowWinningScore(currentScore, maxScore);
            }
        }

        public static string AddTheOption()
        {
            string theOption = Console.ReadLine().ToLower();
            return theOption;
        }

        public static string AddCorrectOption()
        {
            ShowCorrectAnswerInputMessage();
            string correctOption = Console.ReadLine().ToLower();
            return correctOption;
        }

        public static void ShowCorrectAnswerInputMessage()
        {
            Console.WriteLine("Input the correct answer to your question. It will not be shown to the user\n");
        }

        public static void ShowCompletedQuestionMessage()
        {
            Console.WriteLine("You have exhausted the number of questions you chose to store");
        }


        public static void DisplayUserInstruction(int availableQuestions)
        {
            Console.WriteLine($"There are {availableQuestions} questions available for you to answer\n");
            Console.WriteLine("You would see the questions and their answer options below");
            Console.WriteLine("Please select from the options shown to answer the question");
            Console.WriteLine("You should type in your answer\n");
        }

        public static string TakeUserAnswer()
        {
            WriteEmptyLine();
            string theUserAnswer = Console.ReadLine().ToLower();
            return theUserAnswer;
        }

        public static void PrintRandomQuestion(List<string> listOfQuestions)
        {
            int optionCounter = Constant.COUNT_OPTION;
            foreach (var option in listOfQuestions)
            {
                Console.WriteLine($"Option {optionCounter}: {option}");
                optionCounter++;
            }
        }

        public static void PrintAnswerInputInstruction()
        {
            Console.WriteLine("Type your answer below");
        }

        public static void PrintQuestionToUser(int questionPosition, string mainQuestion)
        {
            Console.WriteLine($"Question {questionPosition}: {mainQuestion}");
        }

        public static char RepeatPlay(bool keepPlayingProxy, char quizSelectionProxy, int winProxy, int answerProxy)
        {
            if (keepPlayingProxy)
            {
                ShowQuizGameInstruction();
                quizSelectionProxy = GetQuizLineResponse();
                WriteEmptyLine();
            }
            else
            {
                keepPlayingProxy = false;
                DisplayQuitMessage();
            }
            return quizSelectionProxy;
        }

    }
}
