using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMVCUitgaven.Domain;

namespace WebMVCUitgaven.Database
{
    public interface IUitgaveDatabase
    {
        Uitgave GetUitgave(int id);
        IEnumerable<Uitgave> GetUitgaven();
        Uitgave Insert(Uitgave uitgave);
        void Delete(int id);
        void Update(int id, Uitgave uitgave);
    }
    public class UitgaveDatabase : IUitgaveDatabase
    {
        private readonly UitgaveDbContext uitgaveDbContext;
        public UitgaveDatabase(UitgaveDbContext uitgaveDbContext)
        {
            this.uitgaveDbContext = uitgaveDbContext;
        }
        public Uitgave GetUitgave(int id)
        {
            return uitgaveDbContext.Uitgaven.SingleOrDefault(mbox => mbox.Id == id);
        }
        public IEnumerable<Uitgave> GetUitgaven()
        {
            return uitgaveDbContext.Uitgaven;
               
        }
        public Uitgave Insert(Uitgave uitgave)
        {
            uitgaveDbContext.Uitgaven.Add(uitgave);
            uitgaveDbContext.SaveChanges();
            return uitgave;
        }
        public void Delete(int id)
        {
            var uitgave = uitgaveDbContext.Uitgaven.SingleOrDefault(mbox => mbox.Id == id);
            if (uitgave != null)
            {
                uitgaveDbContext.Uitgaven.Remove(uitgave);
                uitgaveDbContext.SaveChanges();
            }
        }
        public void Update(int id, Uitgave updateUitgave)
        {
            var uitgave = uitgaveDbContext.Uitgaven.SingleOrDefault(m => m.Id == id);
            if (uitgave != null)
            {
                uitgave.Beschrijving = updateUitgave.Beschrijving;
                uitgave.Datum = updateUitgave.Datum;
                uitgave.Bedrag = updateUitgave.Bedrag;
                uitgave.Koper = updateUitgave.Koper;
                uitgave.PhotoUrl = updateUitgave.PhotoUrl;
                uitgave.Soort = updateUitgave.Soort;
                uitgave.PhotoUrl2 = updateUitgave.PhotoUrl2;
            }
        }
    }
}
