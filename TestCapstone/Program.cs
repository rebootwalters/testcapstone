using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace TestCapstone
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ContextDAL ctx = new ContextDAL())
            {
                ctx.ConnectionString = 
    $@"Data Source=.\sqlexpress;Initial Catalog=testcapstone;Integrated Security=True";

                Console.WriteLine(ctx.ObtainRoleCount());
                Console.WriteLine(ctx.FindRoleByID(5));
                Console.WriteLine(ctx.FindRoleByID(1));

                List<UserDAL> answer = ctx.GetUsers(0, 100);
                Console.WriteLine("***********");
                foreach (var x in answer)
                {
                    Console.WriteLine(x);
                }
            }
        }
    }
}
