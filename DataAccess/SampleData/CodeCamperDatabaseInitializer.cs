using System.Data.Entity;

namespace DataAccess.SampleData
{
    public class CodeCamperDatabaseInitializer :
        //CreateDatabaseIfNotExists<CodeCamperDbContext>      // when model is stable
        //DropCreateDatabaseAlways<CodeCamperDbContext>       // when...
        DropCreateDatabaseIfModelChanges<CodeCamperDbContext> // when iterating
    {
        protected override void Seed(CodeCamperDbContext context)
        {
            new Lookups(context).Run();
            new PersonsGenerator(context).Run(100);
            new SessionsGenerator(context).Run(1000, 100);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
