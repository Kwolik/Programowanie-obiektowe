using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    public interface PanelTC 
    {
        string ObecnaSciezka { get; }
        string[] ListaDyskow { get; }
        List<string> Folder { get; }

        bool Ustaw(string path); //Ustawienie folderów i plików na wskazany folder
        bool EnterPlik(string nazwa);
    }
}
