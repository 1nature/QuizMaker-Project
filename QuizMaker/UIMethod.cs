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
            Console.WriteLine($"If you want to store and answer questions, enter: {Constant.QUIZ_TYPE_STOREANDANSWER}");
            Console.WriteLine($"If you want to store questions only, enter: {Constant.QUIZ_TYPE_STOREONLY}");
            Console.WriteLine($"If you want to answer questions only, enter: {Constant.QUIZ_TYPE_ANSWERONLY}\n");
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

        public static bool StopStoringQuestion(char userAnswer, List<QuestionandAnswer> questionStorage, bool restartStoring, int totalQuestion)
        {
            if (userAnswer == Constant.QUIZ_TYPE_STOREONLY)
            {
                if (questionStorage.Count == totalQuestion)
                {
                    UIMethod.ShowCompletedQuestionMessage();
                    restartStoring = UIMethod.RestartQuiz();
                    if (!restartStoring)
                    {
                        UIMethod.DisplayQuitMessage();
                        restartStoring = false;
                    }
                    else
                    {
                        UIMethod.ShowQuizGameInstruction();
                    }
                }
            }
            return restartStoring;
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

        public static QuestionandAnswer GetQuestionandAnswerObjectFromUser()
        {
            var EachQuestionInput = new QuestionandAnswer(); 
            EachQuestionInput.QuestionText = TakeUserQuestion();
            Console.WriteLine("Input the number of options to your question\n");
            int maximumOptions = GetIntegerFromUser();
            TakeAnswerOption(maximumOptions, EachQuestionInput.ListofQuestionandAnswers);
            EachQuestionInput.CorrectAnswerText = AddCorrectOption();
            return EachQuestionInput;
        }

        public static void TakeAnswerOption(int optionTotal, List<string> optionInput)
        {
            ShowOptionsMessage();
            for (int Index = 0; Index < optionTotal; Index++)
            {
                optionInput.Add(AddTheOption());

            }
        }

        public static void DisplayUserInstruction(int availableQuestions)
        {
            if (availableQuestions >= Constant.MINIMUM_QUIZ_SCORE)
            {
                Console.WriteLine($"There are {availableQuestions} questions available for you to answer\n");
                Console.WriteLine("You would see the questions and their answer options below");
                Console.WriteLine("Please select from the options shown to answer the question");
                Console.WriteLine("You should type in your answer\n");
            }
        }

        public static int GetUserInputandCreateNewQuestionsandAnswers(List<QuestionandAnswer> storageList)
        {
            int numberOfQuizzerQuestions;
            Console.WriteLine("Input the number of questions you want to store: \n");
            numberOfQuizzerQuestions = GetIntegerFromUser();
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

        public static void PrintIntegerInputInstructionToUser()
        {
            Console.WriteLine("Enter '1' to store and/or answer question(s), or '0' to quit. \n");
        }

        public static void PrintQuestionToUser(int questionPosition, string mainQuestion)
        {
            Console.WriteLine($"Question {questionPosition}: {mainQuestion}");
        }

        public static int GetIntegerFromUser()
        {
            bool valid = false;
            int number = 0;
            while (!valid)
            {
                string theInput = Console.ReadLine() +"\n";
                if (int.TryParse(theInput, out number))
                {
                    valid = true;
                } else { Console.WriteLine("Not a valid input, Please try again"); }
            }
            return number;
        }

        public static void DisplayQuizLogic(bool playOn, char quizChoice)
        {
            int storedQuestions;
            var ListOfQuestions = new List<QuestionandAnswer>();
            var theRandomQuestion = new Random();

            while (playOn) 
            {
                if (quizChoice == Constant.QUIZ_TYPE_STOREANDANSWER || quizChoice == Constant.QUIZ_TYPE_STOREONLY)
                {
                    storedQuestions = UIMethod.GetUserInputandCreateNewQuestionsandAnswers(ListOfQuestions);
                    playOn = UIMethod.StopStoringQuestion(quizChoice, ListOfQuestions, playOn, storedQuestions);
                    quizChoice = LogicMethod.CheckRestartStoreCondition(playOn, quizChoice);
                }

                if (quizChoice == Constant.QUIZ_TYPE_STOREANDANSWER || quizChoice == Constant.QUIZ_TYPE_ANSWERONLY)
                {
                    int numOfAvailableQuestions = LogicMethod.FetchQuestionsAndDisplayInstruction(quizChoice);
                    UIMethod.DisplayUserInstruction(numOfAvailableQuestions);
                    playOn = LogicMethod.PerformAnswerOption(theRandomQuestion);
                    quizChoice = LogicMethod.CheckRestartAnsweringCondition(playOn, quizChoice);
                }
            }
        }
    }
}
