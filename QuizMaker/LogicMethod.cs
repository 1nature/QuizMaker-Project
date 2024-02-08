namespace QuizMaker
{
    public static class LogicMethod
    {
        
        public static void UserQuestion(string newQuestion)
        {
            Console.WriteLine(newQuestion);
            //UIMethod.WriteEmptyLine();
        }

        public static void UserAnswerOptions(List<string> answerVariables)
        {

            foreach (string variable in answerVariables)
            {
                Console.WriteLine(variable);
            }
        }

        public static void CombineQuestionAndAnswer (string mainQuestion, List<string>answerVariables)
        {
            //answerVariables.Add(mainQuestion);
            answerVariables.Insert(0, mainQuestion + "\n");
            foreach (string variable in answerVariables)
            {
                Console.WriteLine(variable);
            }
            
            //Console.WriteLine (answerVariables);
            //may it should be a return instead of void?
            //var answer = answerVariables.ToString();
            //List<string> men = new List<string>();
            //men = answerVariables;
            //return men;
            //return answer.ToString();
        }

        public static List<string> CombineQnAnswer(string mainQuestion, List<string> answerVariables)
        {
            answerVariables.Insert(0, mainQuestion + "\n");
            return answerVariables;
        }

        //public static int CountCombinedAnswer(List<string> theCounter)
        //{
        //    int countQnA = 0;
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Stack<T> CreateShuffledDeck<T>(IEnumerable<T> values)
        {
            var rand = new Random();
            var list = new List<T>(values);
            var stack = new Stack<T>();

            while (list.Count > 0)
            {
                //Getting the next item at random
                var index = rand.Next(0, list.Count);
                var item = list[index];

                //Removing item from list & pushing it to the top of the deck
                list.RemoveAt(index);
                stack.Push(item);
            }

            return stack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrayItems"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string[] RandomArrayEntries(object[] arrayItems, int count)
        {
            //var listToReturn = new List<string[]>();
            var listToReturn = new List<string>();

            if (arrayItems.Length != count)
            {
                var deck = CreateShuffledDeck(arrayItems);

                for (int i = 0; i < count; i++)
                {
                    object xxx = arrayItems[i];
                    xxx = deck.Pop();
                    listToReturn.Add((string)xxx);
                }
                return listToReturn.ToArray();
            }
            return ((string[])arrayItems);
        }

    }
}
