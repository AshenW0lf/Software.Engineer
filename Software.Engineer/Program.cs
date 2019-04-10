using Microsoft.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Software.Engineer
{
    class Program
    {
        #region Properties
        private static bool ValidationFailed { get; set; } = false;
        private static StringBuilder ErrorMessage { get; } = new StringBuilder("ERROR!");
        #endregion Properties

        #region Fields
        private static float[] _array;
        private static int _c;
        private static int _n;
        #endregion Fields

        #region Entry
        static void Main(string[] args)
        {
            InitiliseAruments(args);
            if (ValidationFailed)
                Console.Write(ErrorMessage);
            else
            {
                var calculator = new EquationCalculation(_c, _array, _n);
                var matrix = new float[_c + 1, _c + 1];
                for (int k = 0; k <= _c; k++)
                {
                    for (int j = 0; j <= _c; j++)
                    {
                        matrix[k, j] = calculator.GetResult(k, j);
                    }
                }

                DisplayMatrix(matrix);
            }
            Console.Read();
        }
        #endregion Entry

        #region Methods
        private static void DisplayMatrix(float[,] matrix)
        {
            for (int k = 0; k <= _c; k++)
            {
                Console.Write("| ");
                for (int j = 0; j <= _c; j++)
                {
                    Console.Write(matrix[k, j]);
                    Console.Write("\t");
                }
                Console.Write("\t|");
                Console.WriteLine();
            }
        }

        private static void InitiliseAruments(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "Software.Engineer",
                Description = ".NET Core console app with argument parsing for the Software Engineer Test."
            };

            app.HelpOption("-?|-h|--help");

            var fileOption = app.Option("-f|--file <filePath>",
                    "Path to the PRN file",
                    CommandOptionType.SingleValue);
            
            var cOption = app.Option("-c <c>",
                    "Value of c",
                    CommandOptionType.SingleValue);

            var nOption = app.Option("-N <N>",
                    "Value of N",
                    CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                if (fileOption.HasValue())
                {
                    ValidateFilePath(fileOption.Value());
                }
                else
                {
                    app.ShowHint();
                }

                if (cOption.HasValue())
                {
                    Validate(ref _c, cOption);
                }
                else
                {
                    app.ShowHint();
                }

                if (nOption.HasValue())
                {
                    Validate(ref _n, nOption);
                }
                else
                {
                    app.ShowHint();
                }

                return 0;
            });
            
            app.Execute(args);
        }

        private static void Validate(ref int i, CommandOption option)
        {
            if (int.TryParse(option.Value(), out i))
                return;
            else
            {
                ValidationFailed = true;
                ErrorMessage.AppendLine();
                ErrorMessage.Append(option.ValueName);
                ErrorMessage.Append(" is not a valid int.");
            }
        }

        public static void ValidateFilePath(string path)
        {
            if(File.Exists(path) && path.ToLower().EndsWith(".prn"))
            {
                ReadFromFile(path);
            }
            else
            {
                ValidationFailed = true;
                ErrorMessage.AppendLine();
                ErrorMessage.Append(path);
                ErrorMessage.Append(" is not a valid file name.");
            }
        }

        public static void ReadFromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            try
            {
                _array = lines.Select(x => float.Parse(x.Trim())).ToArray();
            }
            catch(Exception ex)
            {
                ValidationFailed = true;
                ErrorMessage.AppendLine();
                ErrorMessage.Append("File contains an invalid value.");
            }
        }
        #endregion Methods
    }
}
