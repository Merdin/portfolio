using MuzikantenApi.Models;
using System.Linq;

namespace MuzikantenApi.Data
{
    public static class SeedData
    {
        public static void Initialize(DefaultContext context)
        {
            context.Database.EnsureCreated();

            AddMuzikant(context, new Muzikant("B.B.", "King", 1949, 2015));
            AddMuzikant(context, new Muzikant("Lang", "Lang", 1999, null));
            AddMuzikant(context, new Muzikant("Louis", "Armstrong", 1919, 1971));
            AddMuzikant(context, new Muzikant("Anne-Sophie", "Mutter", 1977, null));
            AddMuzikant(context, new Muzikant("Jimi", "Hendrix", 1966, 1970));

            context.SaveChanges();

            AddInstrument(context, new Instrument("Gibson", "ES 355 without F-Holes", "Lucille", 22000, context.Muzikant.FirstOrDefault(m => m.Voornaam.Equals("B.B.") && m.Achternaam.Equals("King")).MuzikantId));
            AddInstrument(context, new Instrument("Steingraeber & Söhne", "Vleugel B192", "Elegance", 43000, context.Muzikant.FirstOrDefault(m => m.Voornaam.Equals("Lang") && m.Achternaam.Equals("Lang")).MuzikantId));
            AddInstrument(context, new Instrument("Selmer", "Model 19", "Satchmo", 70000, context.Muzikant.FirstOrDefault(m => m.Voornaam.Equals("Louis") && m.Achternaam.Equals("Armstrong")).MuzikantId));
            AddInstrument(context, new Instrument("Stradivarius", "Emiliani 1703", "Emiliani", 12000000, context.Muzikant.FirstOrDefault(m => m.Voornaam.Equals("Anne-Sophie") && m.Achternaam.Equals("Mutter")).MuzikantId));
            AddInstrument(context, new Instrument("Monterey Fender", "Stratocaster", "Izabella", 2000000, context.Muzikant.FirstOrDefault(m => m.Voornaam.Equals("Jimi") && m.Achternaam.Equals("Hendrix")).MuzikantId));

            context.SaveChanges();
        }

        private static void AddMuzikant(DefaultContext context, Muzikant muzikant)
        {
            var existing = context.Muzikant.FirstOrDefault(v =>
                    v.Voornaam.ToLower().Equals(muzikant.Voornaam.ToLower())
                    && v.Achternaam.ToLower().Equals(muzikant.Achternaam.ToLower()));

            if (existing == null)
            {
                var record = context.Muzikant.Add(muzikant);
            }
        }

        private static void AddInstrument(DefaultContext context, Instrument instrument)
        {
            var existing = context.Instrument.FirstOrDefault(vo =>
                    vo.Type.ToLower().Equals(instrument.Type.ToLower())
                    && vo.Bijnaam == instrument.Bijnaam);

            if (existing == null)
            {
                context.Instrument.Add(instrument);
            }
        }
    }
}
