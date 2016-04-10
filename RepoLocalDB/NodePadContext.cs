using System.Data.Entity;
using TT;

namespace RepoLocalDB
{
    public class Tc
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }


    public class MatrixInfo
    {
        public int ID { get; set; }
        public Data.ValBnd ValBnd { get; set; }
        public Data.MatrixShape MatrixShape { get; set; }
        public string Descr { get; set; }
        public string Shape { get; set; }
        public string Values { get; set; }
    }


    public class NodePadContext : DbContext
    {
        public NodePadContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NodePadContext, Migrations.Configuration>());

        }

        public DbSet<Tc> Tcs { get; set; }


        public DbSet<MatrixInfo> MatrixInfos { get; set; }

    }
}
