using System;
using CommandLine;


namespace n2n
{
    class Program
    {
        static void Main (string[] args)
        {
            try
            {
                Console.WriteLine();

                Parser
                    .Default
                    .ParseArguments<CmdLine.OptsConvert>(args)
                    .MapResult
                    (
                        (CmdLine.OptsConvert opts) =>
                        {
                            DoConvert.DoDir(opts.dirsrc, opts.dirdst);
                            return 0;
                        },
                        errs =>
                        {
                            return 0;
                        }
                   );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "\n" +
                    "Oops!\n" +
                    "\n" +
                    ex.Message
                );

                return;
            }
        }
    }
}