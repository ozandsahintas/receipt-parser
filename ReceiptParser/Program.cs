using System.IO;

namespace ReceiptParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReceiptParser parser = new ReceiptParser();

            parser.ParseReceipt("file.json", "receipt.txt");
        }

    }
}
