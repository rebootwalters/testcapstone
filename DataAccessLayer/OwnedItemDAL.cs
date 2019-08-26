using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OwnedItemDAL
    {
        public int OwnedItemID { get; set; }
        public int OwnerID { get; set; }
        public string ItemDescription { get; set; }

        public decimal ItemPrice { get; set; }

        public string  EMail { get; set; }
    }
}
