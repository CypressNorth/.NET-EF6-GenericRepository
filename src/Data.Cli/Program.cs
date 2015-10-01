namespace Data.Cli
{
    using Data.Repository;
    using System;
    using System.Threading.Tasks;

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var repo = new BaseRepository<SampleEntity>())
            {
                // Adding //
                repo.AddAllAsync(new[]
                {
                    new SampleEntity { Name = "Turtle" },
                    new SampleEntity { Name = "Fox" },
                    new SampleEntity { Name = "Cat" },
                    new SampleEntity { Name = "Ant" },
                    new SampleEntity { Name = "Lion" },
                }).GetAwaiter().GetResult();

                Console.WriteLine("# of records : {0}\n", repo.Count());

                MainAsync(repo).GetAwaiter().GetResult();

                Console.ReadKey();
            }
        }

        static async Task MainAsync(IRepository<SampleEntity> repo)
        {
            foreach (var s in await repo.GetAllAsync())
            {
                Console.WriteLine("{0} | {1}", s.ID, s.Name);
            }

            // Paged Set //
            Console.WriteLine("\nPage = 2 - Page Size = 2 :");
            var some = await repo.GetAsync(2, 2, s => s.ID);
            foreach (var s in some)
            {
                Console.WriteLine("{0} | {1}", s.ID, s.Name);
            }
                
            // Updating //
            var fox = await repo.FindAsync(e => e.Name == "Fox");
            fox.Name = "Dog";

            repo.Update(fox, fox.ID);

            // Deleting //
            Console.WriteLine("\n " + await repo.DeleteAsync(repo.Get(5)) + "\n");

            foreach (var e in repo.GetAll())
                Console.WriteLine(e.ID + " | " + e.Name);
        }
    }
}