using System;
using System.Text;

public class User
{
    public delegate void Move(int x);
    event Move move;

    public delegate void Squeeze(int offset);
    event Squeeze squeeze;

    public int Position { get; set; }
    public int Size { get; set; }  

    public User(int position, int size)
    {
        Position = position;
        Size = size;
    }

    public void SubscribeToMove(Move handler) => move += handler;

    public void SubscribeToSqueeze(Squeeze handler) => squeeze += handler;

    public void OnMove(int x)
    {
        Position += x;
        move?.Invoke(x);
    }

    public void OnSqueeze(int offset)
    {
        Size *= offset;
        squeeze?.Invoke(offset);
    }

}

public class StringMethods
{
    public static string RemovePunctuation(string input)
    {
        var result = new StringBuilder();

        foreach (char c in input)
        {
            if (!char.IsPunctuation(c))
            {
                result.Append(c);
            }

        }

        return result.ToString();
    }
    public static string AddSymbol(string input, char symbol)
    {
        return input + symbol;
    }
    public static string ToUpperCase(string input)
    {
        return input.ToUpper();
    }
    public static string RemoveExtraSpaces(string input)
    {
        return input.Replace(" ", "");

    }
    public static string ReplaceSpacesWithUnderscores(string input)
    {
        return input.Replace(' ', '_');
    }
}

namespace OOP8
{
    internal class Program
    {
        static void Main(string[] args)
        {

            User user1 = new User(0, 10);
            User user2 = new User(5, 20);
            User user3 = new User(10, 5);

            user1.SubscribeToMove(x => Console.WriteLine($"User1 moved by {x}, new position: {user1.Position}"));


            user2.SubscribeToMove(x => Console.WriteLine($"User2 moved by {x}, new position: {user2.Position}"));
            user2.SubscribeToSqueeze(offset => Console.WriteLine($"User2 squeezed by {offset}, new size: {user2.Size}"));

            user1.OnMove(3);
            user2.OnMove(2);   
            user2.OnSqueeze(2);

            Console.WriteLine($"User1: Position = {user1.Position}, Size = {user1.Size}");
            Console.WriteLine($"User2: Position = {user2.Position}, Size = {user2.Size}");
            Console.WriteLine($"User3: Position = {user3.Position}, Size = {user3.Size}");


            Console.WriteLine("====================================================");

            string input = " Random string . with spaces and , punctuation ! ";

            Func<string, string> removePunctuation = StringMethods.RemovePunctuation;
            Func<string, string> removeExtraSpaces = StringMethods.RemoveExtraSpaces;
            Func<string, string> toUpperCase = StringMethods.ToUpperCase;
            Func<string, string> replaceSpacesWithUnderscores = StringMethods.ReplaceSpacesWithUnderscores;

            Action<string> addSymbol = (str) => Console.WriteLine(StringMethods.AddSymbol(str, '?'));

            string processedString = input;
            processedString = removePunctuation(processedString);
            processedString = removeExtraSpaces(processedString);
            processedString = toUpperCase(processedString);
            processedString = replaceSpacesWithUnderscores(processedString);

            Console.WriteLine(processedString);

            addSymbol(processedString);



        }
    }
}
