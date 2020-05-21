using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniTC.Model
{
    class PanelTCModel : PanelTC
    {
        public string[] ListaDyskow => Directory.GetLogicalDrives();
        public string ObecnaSciezka { get; private set; }
        public List<string> Folder { get; private set; }

        public PanelTCModel()
        {}

        public bool Ustaw(string path)
        {
            string[] foldery, pliki;

            try
            {
                foldery = Directory.GetDirectories(path);
                pliki = Directory.GetFiles(path);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }

            Folder = new List<string>();

            if (!ListaDyskow.Contains(Path.GetFullPath(path)))
            {
                Folder.Add("..");
            }

            Folder.AddRange(foldery.Select(x => "<D>" + Path.GetFileName(x)));
            Folder.AddRange(pliki.Select(x => Path.GetFileName(x)));

            ObecnaSciezka = Path.GetFullPath(path);

            return true;
        }

        public bool EnterPlik(string abc)
        {
            if (abc != null && abc.Contains("<D>"))
            {
                var filePath = Path.Combine(ObecnaSciezka, abc.Substring(3));
                return Ustaw(filePath);
            }

            if (abc != null && abc == "..")
            {
                var filePath = Path.Combine(ObecnaSciezka, abc);
                return Ustaw(filePath);
            }

            return true;
        }
    }
}
