using CommandLine;


namespace n2n.CmdLine
{
    /// <summary>
    ///
    /// CommandLineParser: Configuration via Declarations
    ///
    /// </summary>

    [Verb("c", HelpText = "Convert all DATs to CSVs")]
    public class OptsConvert
    {
        [Option('i', "srcdir", Required = true, HelpText = "Source directory where all DAT files reside")]
        public string dirsrc { get; set; }

        [Option('o', "dstdir", Required = true, HelpText = "Destination directory")]
        public string dirdst { get; set; }
    }
}