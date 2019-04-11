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
            if (!InitialiseAruments(args))
                Console.Write(ErrorMessage);
            else
            {
                IEquationCalculation calculator = new EquationCalculation(_c, _array, _n);
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
        /// <summary>
        /// Displays a matrix on the console window
        /// </summary>
        /// <param name="matrix">Matrix to display</param>
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

        /// <summary>
        /// Initialises and validates arguments passed to the application
        /// </summary>
        /// <param name="args">String array of args</param>
        /// <returns>true if all args are valid</returns>
        private static bool InitialiseAruments(string[] args)
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
            bool initalised = true;
            app.OnExecute(() =>
            {
                if (fileOption.HasValue())
                {
                    initalised = ValidateFilePathExtractData(fileOption.Value());
                }

                if (cOption.HasValue() && initalised)
                {
                    initalised = Validate(ref _c, cOption);
                }

                if (nOption.HasValue() && initalised)
                { 
                    initalised = Validate(ref _n, nOption);
                }

                return initalised ? 0 : -1;
            });
            
            return app.Execute(args) == 0;
        }

        /// <summary>
        /// Validates option is an integer
        /// </summary>
        /// <param name="i">reference integer</param>
        /// <param name="option">Option to validate</param>
        /// <returns>true if the option is valid</returns>
        private static bool Validate(ref int i, CommandOption option)
        {
            if (int.TryParse(option.Value(), out i))
                return true;

            ErrorMessage.AppendLine();
            ErrorMessage.Append(option.ValueName);
            ErrorMessage.Append(" is not a valid int.");
            return false;
        }

        /// <summary>
        /// Validates and extracts data from a file path
        /// </summary>
        /// <param name="path">path to the file</param>
        /// <returns>true if file is valid</returns>
        public static bool ValidateFilePathExtractData(string path)
        {
            if(File.Exists(path) && path.ToLower().EndsWith(".prn"))
            {
                return ReadFromFile(path);
            }

            ErrorMessage.AppendLine();
            ErrorMessage.Append(path);
            ErrorMessage.Append(" is not a valid file name.");

            return false;
        }

        /// <summary>
        /// Read all line from the file
        /// </summary>
        /// <param name="path">path to the file</param>
        /// <returns>true if the data is valid</returns>
        public static bool ReadFromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            try
            {
                _array = lines.Select(x => float.Parse(x.Trim())).ToArray();
                return true;
            }
            catch(IOException)
            {
                ErrorMessage.AppendLine();
                ErrorMessage.Append("File contains an invalid value.");
                return false;
            }
            catch(Exception ex)
            {
                ErrorMessage.AppendLine();
                ErrorMessage.Append($"An Exception has occurred '{ex.Message}'");
                return false;
            }
        }
        #endregion Methods
    }
}
