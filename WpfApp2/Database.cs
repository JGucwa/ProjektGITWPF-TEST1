using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Database
    {
        readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "baza.json");

        public List<Zadanie> WszystkieZadania()
        {
            if (JsonConvert.DeserializeObject<List<Zadanie>>(FilePath) != null)
            {
                string tekst = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Zadanie>>(tekst);
            }
            else
            {
                return null;
            }
            
        }
        public void DodajZadanie(Zadanie zadanie)
        {
            List<Zadanie> tmp = WszystkieZadania();

            if(tmp != null)
            {
                if(tmp.Count == 0) zadanie.Id = 0; else zadanie.Id = tmp[tmp.Count-1].Id + 1;
                tmp.Add(zadanie);

                string tekst = JsonConvert.SerializeObject(tmp);

                File.WriteAllText(FilePath, tekst);
            }
        }
        public void EdytujZadanie(Zadanie zadanie)
        {
            List<Zadanie> tmp = WszystkieZadania();

            if (tmp != null)
            {
                for(int i = 0; i < tmp.Count; i++)
                {
                    if(zadanie.Id == tmp[i].Id)
                    {
                        tmp[i] = zadanie;
                    }
                }

                string tekst = JsonConvert.SerializeObject(tmp);

                File.WriteAllText(FilePath, tekst);
            }
        }
    }
}
