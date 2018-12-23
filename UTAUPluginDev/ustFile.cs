namespace UTAUPluginDev
{
    public class ustFile
    {
        public double Version { get; set; }
        public double Tempo { get; set; }
        public string PjName { get; set; }
        public string VoicePath { get; set; }
        public string OutFile { get; set; }
        public string Cache { get; set; }
        public string Tool1 { get; set; }
        public string Tool2 { get; set; }
        public bool Mode2 { get; set; }
        public bool Autoren { get; set; }
        public bool MapFirst { get; set; }
        public string Flags { get; set; }
        public utaNote[] Notes { get; set; }
    }
}
