namespace GraphBase.IO
{

    public class Output
    {
        private bool _needFileOutput;
        private string _path = "";

        public Output(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-o")
                {
                    Console.WriteLine("Введите ключ формата данных : ");
                    SwitchToFileOutput(args[i + 1]);
                }
            }
            _needFileOutput = false;
        }

        public void WriteLine(string line)
        {
            if (_needFileOutput) FileWrite(line);
            else Console.WriteLine(line);
        }

        private void FileWrite(string line)
        {
            using var writer = new StreamWriter(_path, true);
            writer.WriteLine(line);
        }

        private void SwitchToFileOutput(string fileName)
        {
            _path = $"Files\\{fileName}";
            _needFileOutput = true;
        }
    }
}
