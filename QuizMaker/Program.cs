namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            var random = new Random();
            Questions[] array = { new Questions(), new Questions(), new Questions(), new Questions() };
            int index = random.Next(array.Count());
            //Console.WriteLine(index);
        }
    }
}

public class Questions 
{
    public string Question1 = "What is the capital of the UK?";
    
    public string OptionA = "Berlin";
    public string OptionB = "Ottawa";
    public string OptionC = "London";
    public string OptionD = "Abuja";
    public Answer Answer1 = new Answer();
}

public class Answer 
{
    public string correctAnswer;
}

//store in a list
//public List<Answer> Answers = new List<Answer>();