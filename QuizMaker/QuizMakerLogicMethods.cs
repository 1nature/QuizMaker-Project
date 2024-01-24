namespace QuizMaker
{
    public static class QuizMakerLogicMethods
    {
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
