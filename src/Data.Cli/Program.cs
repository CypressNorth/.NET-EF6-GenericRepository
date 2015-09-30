namespace Data.Cli
{
    using Data.Repository;
    using System;

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
                    new SampleEntity { Name = "Cat" }
                }).GetAwaiter().GetResult();

                Console.WriteLine("# of records : {0}\n", repo.Count());

                foreach (var s in repo.GetAllAsync().GetAwaiter().GetResult())
                {
                    Console.WriteLine("{0} | {1}", s.ID, s.Name);
                }

                // Updating //
                var fox = repo.FindAsync(e => e.Name == "Fox").GetAwaiter().GetResult();
                fox.Name = "Dog";

                repo.Update(fox, fox.ID);

                // Deleting //
                Console.WriteLine(repo.DeleteAsync(repo.Get(3)).GetAwaiter().GetResult() + "\n");

                foreach (var e in repo.GetAll())
                    Console.WriteLine(e.ID + " | " + e.Name);

                Console.ReadKey();
            }
        }
    }
}