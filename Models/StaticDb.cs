namespace Scarpe___Co.Models
{
    public static class StaticDb
    {
        private static int _maxId = 3;
        private static List<Articolo> _articoli = [
            new Articolo() { Id = 1, Nome = "Diadora S.Challenge 5 SL Clay", Prezzo = 60, Descrizione = "Scarpe da Tennis Unisex-Adulto. Rivestimento in suprelltech e mesh", ImgCopertina = "https://m.media-amazon.com/images/I/51oYUf6FxkL._AC_SY625_.jpg", ImgAggiuntive = "https://m.media-amazon.com/images/I/619R5-oSl2L._AC_SY625_.jpg" },
            new Articolo() { Id = 2, Nome = "Adidas Courtflash Speed", Prezzo = 77, Descrizione = "Tennis Shoes, Scarpe Uomo. Modello leggero per il massimo della velocità, realizzato in parte con materiali riciclati.", ImgCopertina = "https://m.media-amazon.com/images/I/61WJv7DOTgL._AC_SY625_.jpg", ImgAggiuntive = "https://m.media-amazon.com/images/I/71LRynvWNuL._AC_SY625_.jpg" },
            new Articolo() { Id = 3, Nome = "Mizuno Breakshot 3 CC", Prezzo = 82, Descrizione = "Scarpe da Tennis Unisex Adulto. Suola altamente flessibile che si piega facilmente anche con bassa forza", ImgCopertina = "https://m.media-amazon.com/images/I/71NNRanPmuL._AC_SX625_.jpg", ImgAggiuntive = "https://m.media-amazon.com/images/I/71aVdLYTSJL._AC_SX625_.jpg" }

        ];


        public static List<Articolo> GetAll()
        {
            List<Articolo> notDeletedArticoli = [];
            foreach (var articolo in _articoli)
            {
                if (articolo.DeletedAt is null)
                {
                    notDeletedArticoli.Add(articolo);
                }
            }
            return notDeletedArticoli;
        }

        public static List<Articolo> GetAllDeleted()
        {
            List<Articolo> deletedArticoli = [];
            foreach (var articolo in _articoli)
            {
                if (articolo.DeletedAt is not null)
                {
                    deletedArticoli.Add(articolo);
                }
            }
            return deletedArticoli;
        }

        public static Articolo? Recover(int idToRecover)
        {
            int? index = findArticoloIndex(idToRecover);

            if (index is not null)
            {
                var articoloRecovered = _articoli[(int)index];
                articoloRecovered.DeletedAt = null;
                return articoloRecovered;
            }
            return null;
        }

        public static Articolo? GetById(int? id)
        {
            if (id is null) return null;
            for (int i = 0; i < _articoli.Count; i++)
            {
                Articolo articolo = _articoli[i];
                if (articolo.Id == id)
                {
                    return articolo;
                }
            }
            return null;
        }

        public static Articolo Add(string nome, double prezzo, string descrizione, string imgCopertina, string imgAggiuntive)
        {
            _maxId++;
            var articolo = new Articolo() { Id = _maxId, Nome = nome, Prezzo = prezzo, Descrizione = descrizione, ImgCopertina = imgCopertina, ImgAggiuntive = imgAggiuntive};
            _articoli.Add(articolo);
            return articolo;
        }

        public static Articolo? Modify(Articolo articolo)
        {
            foreach(var articoloInList in _articoli)
            {
                if(articoloInList.Id == articolo.Id)
                {
                    articoloInList.Nome = articolo.Nome;
                    articoloInList.Prezzo = articolo.Prezzo;
                    articoloInList.Descrizione = articolo.Descrizione;
                    articoloInList.ImgCopertina = articolo.ImgCopertina;
                    articoloInList.ImgAggiuntive = articolo.ImgAggiuntive;
                    return articoloInList;
                }
            }
            return null;
        }

        public static Articolo? SoftDelete(int idToDelete)
        {
            int? deletedIndex = findArticoloIndex(idToDelete);

            if(deletedIndex is not null)
            {
                var articoloDeleted = _articoli[(int)deletedIndex];
                articoloDeleted.DeletedAt = DateTime.UtcNow;
                return articoloDeleted;
            }
            return null;
        }

        public static Articolo? HardDelete(int idToDelete)
        {
            int? deletedIndex = findArticoloIndex(idToDelete);

            if(deletedIndex is not null)
            {
                var articoloDeleted = _articoli[(int)(deletedIndex)];
                _articoli.RemoveAt((int)deletedIndex);
                return articoloDeleted;
            }
            return null;
        }

        private static int? findArticoloIndex(int idToDelete)
        {
            int i;
            bool articoloFound = false;
            for (i=0; i<_articoli.Count; i++)
            {
                if (_articoli[i].Id == idToDelete)
                {
                    articoloFound = true;
                    break;
                }
            }

            if (articoloFound) return i;
            return null;
        }
    }
}
