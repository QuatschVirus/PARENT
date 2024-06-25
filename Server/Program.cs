namespace PARENT.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = null;

            if (args.Length > 0)
            {
                if (args[0].All(c => Path.GetInvalidPathChars().Contains(c))) {
                    server = new(args[0]);
                }
            }
            server ??= new(null);
        }
    }
}
