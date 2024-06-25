namespace PARENT.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Client client = null;

            if (args.Length > 0)
            {
                if (args[0].All(c => Path.GetInvalidPathChars().Contains(c)))
                {
                    client = new(args[0]);
                }
            }
            client ??= new(null);
        }
    }
}