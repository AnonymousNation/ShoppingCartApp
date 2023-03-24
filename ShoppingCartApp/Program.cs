

using ShoppingCartApp.Services;

namespace ShoppingCartApp
{
    static class Program
    {
        static void Main()
        {
            TerminalService terminalService = new TerminalService();
            var input = "ABCDABAA";

            terminalService.Scan(input);
            Console.WriteLine($"Total Amount for '{input}':\t {{0:C}}", terminalService.Total());

            terminalService.Clear();
            input = "CCCCCCC";
            terminalService.Scan(input);
            Console.WriteLine($"Total Amount for '{input}':\t {{0:C}}", terminalService.Total());

            terminalService.Clear();
            input = "ABCD";
            terminalService.Scan(input);
            Console.WriteLine($"Total Amount for '{input}':\t {{0:C}}", terminalService.Total());
        }
    }

}