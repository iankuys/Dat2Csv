using System;
using System.IO;
using System.Globalization;
using CsvHelper;
using System.Collections.Generic;


namespace n2n
{
    /// <summary>
    ///
    /// https://joshclose.github.io/CsvHelper/
    ///
    /// </summary>

    internal class DoConvert
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="DirIn"></param>
        /// <param name="DirOut"></param>

        public static void DoDir (string DirIn, string DirOut)
        {
            // Preparation.

            if ( !Directory.Exists(DirIn) )
                Directory.CreateDirectory(DirIn);

            // For every DAT file found...

            var Errs = new List<string>();

            foreach (var ThisFile in Directory.GetFiles(DirIn, "*.dat"))
            {
                try
                {
                    Console.Write($"{Path.GetFileName(ThisFile)}... ");

                    DoFile(ThisFile, DirOut);

                    Console.WriteLine("OK");
                }
                catch (Exception ex)
                {
                    Errs.Add(ThisFile);
                    Errs.Add($"  {ex.Message}");

                    if ( ex.InnerException != null )
                        Errs.Add($"    {ex.InnerException.Message}");

                    Console.WriteLine("KO");
                }
            }

            // Log fail conversion to txt.

            var FqnErr = Path.Combine(
                DirOut, "ErrorList.txt"
            );

            File.Delete(FqnErr);

            if ( Errs.Count > 0 )
            {
                File.AppendAllLines(
                    FqnErr,
                    new string[] {
                        $"Created on {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                         ""
                    }
                );

                File.AppendAllLines(
                    FqnErr, Errs
                );
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="FileIn"></param>
        /// <param name="DirOut"></param>

        static void DoFile (string FileIn, string DirOut)
        {
            // Prepare CSV reader.

            using var rdr_strm = new StreamReader(FileIn);
            using var rdr_csv  = new CsvReader(rdr_strm, CultureInfo.InvariantCulture);

            rdr_csv.Configuration.HasHeaderRecord = false;
            rdr_csv.Configuration.Delimiter       = "|";

            // Prepare CSV writer.

            var FileOut = Path.Combine(
                DirOut,
                $"{Path.GetFileNameWithoutExtension(FileIn)}.csv"
            );

            using var wrt_strm = new StreamWriter(FileOut);
            using var wrt_csv  = new CsvWriter(wrt_strm, CultureInfo.InvariantCulture);

            // Copy records.

            wrt_csv.WriteRecords(
                rdr_csv.GetRecords<dynamic>()
            );
        }
    }
}