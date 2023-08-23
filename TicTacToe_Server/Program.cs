namespace TicTacToe_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();
                      

            Console.ReadKey();
        }
    }
}