using System;
using System.Linq;
using Library.Models;

namespace Backend.Data
{
    public class SeedData
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            AddModule(context, new Module(
                "IT Infrastructure Building Blocks ", 
                "Every enterprise is dependent on Information Technology. IT infrastructure acts as the" +
                " foundation under information systems and business processes. IT infrastructure is usually invisible," +
                " but nevertheless very essential: an unavailable infrastructure means unavailable business processes.", 
                new DateTime(2020, 06, 30), visible: true, status: ModuleStatus.Checked));
            
            AddModule(context, new Module(
                "IT Infrastructure Design", 
                "In realizing IT infrastructure the motto is: \"think first, then act.\" An IT infrastructure" +
                " design is similar to the construction drawing of a house: with this document the builders know" +
                " exactly what to do. Designing an IT infrastructure is often a craft: there are few or no known" +
                " methodologies, such as in OO software design. In this semester we get started with architecture" +
                " methods for creating visualizations of the alignment between business and IT. We use those" +
                " visualizations to analyse problems in order to find requirements for new IT infrastructure solutions." +
                " We then use a design method that, in a structured way, leads to an adequate technical design," +
                " by introducing functional infrastructure modeling. In order to test the fulfillment of the posed" +
                " requirements we dive into risk based testing.", 
                new DateTime(2022, 06, 30), visible: true, status: ModuleStatus.Draft));
            
            AddModule(context, new Module(
                "Applied IT Security",
                "It's all in the news: DDoS attacks, stolen credit card information, defaced facebook pages," +
                " identify theft. It is important to protect valuable personal and corporate data. The Applied IT" +
                " Security semester deals with technical protection against cyber-attacks. At each layer of the" +
                " seven-layer OSI network model, attack techniques and countermeasures are discussed, from social" +
                " engineering to lock picking, SQL injection, connection hijacking, cryptography and network" +
                " protection constructions. ", 
                new DateTime(2022, 06, 30), visible: true, status: ModuleStatus.Checked));
            
            AddModule(context, new Module(
                "Web Development",
                "Webtechnologie is niet meer weg te denken uit de huidige maatschappij." +
                " Voor een informaticus is kennis hiervan onmisbaarIn dit semester komen de belangrijkste technieken" +
                " aan bod die ten grondslag liggen aan internettechnologie.", 
                new DateTime(2021, 06, 12), visible: true, status: ModuleStatus.Invalid));

            context.SaveChanges();
        }

        private static void AddModule(DataContext context, Module module)
        {
            var existing = context.Modules.FirstOrDefault(m =>
                m.Name.ToLower().Equals(module.Name.ToLower()));

            if (existing == null)
            {
                var record = context.Modules.Add(module);
            }
        }
    }
}