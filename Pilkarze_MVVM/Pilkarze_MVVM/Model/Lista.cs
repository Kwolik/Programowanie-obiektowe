using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Pilkarze_MVVM.Model
{
    class Lista
    {
        private List<Pilkarz> ListaPilkarzy = Load(@"data.json");

        public void Add(Pilkarz p) => ListaPilkarzy.Add(p);
        public void Remove(int n) => ListaPilkarzy.RemoveAt(n);
        public void Modify(Pilkarz p, int n) => ListaPilkarzy[n] = p;

        public int Count()
        {
            var i = ListaPilkarzy.Count;
            return i;
        }

        public List<Pilkarz> GetPlayers { get => ListaPilkarzy; }

        private static List<Pilkarz> Load(string plik)
        {
            List<Pilkarz> lista = new List<Pilkarz>();
            if (File.Exists(plik))
                lista = JsonConvert.DeserializeObject<List<Pilkarz>>(File.ReadAllText(plik));
            return lista;
        }

        public void Save(string plik)
        {
            string s = JsonConvert.SerializeObject(ListaPilkarzy);
            File.WriteAllText(plik, s);
        }
    }
}
