namespace QuizMaker
{
    public static class UIMethod
    {
        public static int DisplayWinOrLossMessageAndExit(bool theCondition)
        {
            int winCounter = 0;
            if (theCondition)
            {
                winCounter++;
                Console.WriteLine("Correct answer. You have a score\n");
            }
            else
            {
                Console.WriteLine("Incorrect answer. You have no score\n");
            }
            return winCounter;
        }

        public static void ShowQuizGameInstruction()
        {
            Console.WriteLine("**************QUIZ INSTRUCTION*****************\n");
            Console.WriteLine("If you want to store and answer questions, enter 'A'");
            Console.WriteLine("If you want to store questions only, enter 'B'");
            Console.WriteLine("If you want to answer questions only, enter 'C'\n");
        }

        public static void QuizWelcomeMessage()
        {
            Console.WriteLine("Welcome!\n");
            Console.WriteLine("You can store your own questions to this quiz game");
            Console.WriteLine("Also, you can answer stored questions in the quiz game\n");
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
        public static bool RestartQuizMaker()
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine("Enter '1' to restart the quizmaker, or '0' to quit.");
            int newQuestion;
            do
            {
                newQuestion = int.Parse(Console.ReadLine());
                if (newQuestion != Constant.QUIZ_DECISION_YES && newQuestion != Constant.QUIZ_DECISION_NO)
                    Console.WriteLine("Not a valid input. Please try again");

            } while (newQuestion != Constant.QUIZ_DECISION_YES && newQuestion != Constant.QUIZ_DECISION_NO);
            return (newQuestion == Constant.QUIZ_DECISION_YES);
        }

        public static bool ExitAnswerOption(int exitVariable)
        {
            bool exitCondition = true;
            if (exitVariable < Constant.MINIMUM_NUMBER_OF_QUESTION)
            {
                exitCondition = false;
            }
            return exitCondition;
        }

        public static bool ShowScoreForRestartOrQuit(int exitVariable, int currentScore, int maxScore) 
        {
            bool exitCondition = true;
            if (exitVariable < Constant.MINIMUM_NUMBER_OF_QUESTION)
            {
                bool restartVariable = RestartQuizMaker();
                if (restartVariable)
                {
                    exitCondition = restartVariable;
                    Console.WriteLine("***********YOUR SCORE**************");
                    decimal theResult = (decimal)currentScore / maxScore * 100; //Use a method here cos of duplication
                    Console.WriteLine($"You scored {currentScore} question(s) out of {maxScore} OR {theResult}%\n");
                    UIMethod.ShowQuizGameInstruction();
                }
                else
                {
                    DisplayQuitMessage();
                    Console.WriteLine("***********YOUR SCORE**************");
                    decimal theResult = (decimal)currentScore / maxScore * 100;
                    Console.WriteLine($"You scored {currentScore} question(s) out of {maxScore} OR {theResult}%\n");
                    exitCondition = false;
                }
            }
            return exitCondition;
        }

        public static bool GetUserPlayOrQuitInput()
        {
            int numberInput;
            bool checkInput = true;
            bool isInteger;

            while (checkInput)
            {
                Console.WriteLine("Enter '1' to store and/or answer question(s), or '0' to quit.\n");
                do
                {
                    isInteger = int.TryParse(Console.ReadLine(), out numberInput);
                    if (!isInteger)
                    {
                        Console.WriteLine("Wrong input. Please try again");
                    }
                } while (!isInteger);

                if (numberInput == Constant.QUIZ_DECISION_YES || numberInput == Constant.QUIZ_DECISION_NO)
                {
                    break;
                }
            }
            return checkInput;
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

        public static QuestionandAnswer GetQuestionandAnswerObjectFromUser()
        {
            var EachQuestionInput = new QuestionandAnswer(); //
            string quizAnswerOptions = "";

            EachQuestionInput.QuestionText = TakeUserQuestion();
            int maximumOptions = GetIntFromUser("Input the number of options to your question?\n");
            LogicMethod.TakeAnswerOption(maximumOptions, quizAnswerOptions, EachQuestionInput.ListofQuestionandAnswers);
            EachQuestionInput.CorrectAnswerText = AddCorrectOption();
            return EachQuestionInput;
        }

        public static int FetchQuestionsAndDisplayInstruction(char answerOption)
        {
            var getQuestions = new List<QuestionandAnswer>();
            if (answerOption == Constant.QUIZ_TYPE_ANSWERONLY || answerOption == Constant.QUIZ_TYPE_STOREANDANSWER)
            {
                getQuestions = LogicMethod.ReadQuizFromXml();
                DisplayUserInstruction(getQuestions.Count);
            }
            return getQuestions.Count;
        }

        public static int GetUserInputandCreateNewQuestionsandAnswers(List<QuestionandAnswer> storageList)
        {
            int numberOfQuizzerQuestions = 0;
            numberOfQuizzerQuestions = GetIntFromUser("Input the number of questions you want to store: \n");
            int questionDecrement = numberOfQuizzerQuestions;
            for (int quizzerReplyIndex = 0; quizzerReplyIndex < numberOfQuizzerQuestions; quizzerReplyIndex++)
            {
                QuestionandAnswer newQna = GetQuestionandAnswerObjectFromUser();
                storageList.Add(newQna);
                questionDecrement--;
            }
            LogicMethod.SerializeData(storageList);
            return numberOfQuizzerQuestions;
        }

        public static QuestionandAnswer DisplayQuestionAndAnswersToUser(QuestionandAnswer randomlySelectedQuestion, int countAnswer)
        {
            PrintQuestionToUser(countAnswer, randomlySelectedQuestion.QuestionText);
            PrintRandomQuestion(randomlySelectedQuestion.ListofQuestionandAnswers);
            PrintAnswerInputInstruction();
            return randomlySelectedQuestion;
        }

        public static void DisplayQuitMessage()
        {
            Console.WriteLine("You have decided to quit the quizmaker.\n");
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

        public static void PrintNoMoreQuestionMessage()
        {
            Console.WriteLine("There are no more questions left to answer\n");
        }

        public static string TakeUserQuestion()
        {
            Console.WriteLine("Input your question\n");
            string quizzerReply = Console.ReadLine().ToLower() + "\n";
            return quizzerReply;
        }

        public static void ShowOptionsMessage()
        {
            Console.WriteLine("Input your options one after the other. One of them must be the correct answer\n");
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
    }
}
