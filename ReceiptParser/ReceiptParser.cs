using Newtonsoft.Json;

namespace ReceiptParser
{
    public class ReceiptParser
    {
        public void ParseReceipt(string inputName = "file.json", string outputName = "receipt.txt")
        {
            var dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (!string.Equals(".json", Path.GetExtension(inputName)))
            {
                Console.WriteLine($"{inputName} is not a .json file");
                return;
            }

            var items = ReadReceiptData(dir, inputName);
            var dt = items.OrderBy(a => a.BoundingPoly.Vertices[0].Y).ToList(); // Order items horizontally
            WriteReceiptData(dir, outputName, dt); // ...and then vertically.

            #region Coordinates
            //using (StreamWriter sw = new StreamWriter("coords.txt"))
            //{
            //    for (int i = 1; i < dt.Count; i++)
            //    {
            //        sw.WriteLine($"({dt[i].BoundingPoly.Vertices[0].X}, {dt[i].BoundingPoly.Vertices[0].Y}) \t {dt[i].Description}");
            //    }
            //}
            #endregion
        }
        List<ReceiptData> ReadReceiptData(string dir, string inputName)
        {
            using (StreamReader r = new StreamReader(dir + "/" + inputName))
            {
                string json = r.ReadToEnd();

                if (String.IsNullOrEmpty(json))
                {
                    Console.WriteLine($"{inputName} is empty");
                    return [];
                }

                return JsonConvert.DeserializeObject<List<ReceiptData>>(json) ?? [];
            }
        }

        bool WriteReceiptData(string dir, string outputName, List<ReceiptData> dt)
        {

            using (StreamWriter sw = new StreamWriter(dir + "/" + outputName))
            {
                string line = string.Empty;

                for (int i = 1; i < dt.Count; i++)
                {
                    if (i + 1 == dt.Count)
                    {
                        line += dt[i].Description;
                    }
                    else
                    {
                        var currentx = dt[i].BoundingPoly.Vertices[0].X;
                        var nextx = dt[i + 1].BoundingPoly.Vertices[0].X;

                        if (currentx < nextx)
                            line += dt[i].Description + " "; // Same line.
                        else
                            line += dt[i].Description + "\n"; // Next line.
                    }
                }

                Console.WriteLine($"{outputName} created.");

                sw.WriteLine(line);
                return true;
            }
        }

    }
}
