using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class OwnedItemMapper : Mapper
    {
        int OffsetToItemID;
        int OffsetToUserID;
        int OffsetToDescription;
        int OffsetToEMail;

        public OwnedItemMapper(SqlDataReader reader)
        {
            OffsetToItemID = reader.GetOrdinal("ItemID");
            Assert(0 == OffsetToItemID, $"ItemID offset is {OffsetToItemID} not 0 as expected");
            OffsetToUserID = reader.GetOrdinal("OwnerID");
            Assert(1 == OffsetToUserID, $"ItemID offset is {OffsetToUserID} not 1 as expected");
            OffsetToDescription = reader.GetOrdinal("ItemDescription");
            Assert(2 == OffsetToDescription, $"ItemID offset is {OffsetToDescription} not 2 as expected");
            OffsetToEMail = reader.GetOrdinal("EMail");
            Assert(3 == OffsetToEMail, $"ItemID offset is {OffsetToEMail} not 3 as expected");

        }

        public OwnedItemDAL OwnedItemFromReader(SqlDataReader reader)
        {
            OwnedItemDAL proposedReturnValue = new OwnedItemDAL();
            proposedReturnValue.OwnedItemID = reader.GetInt32(OffsetToItemID);
            proposedReturnValue.OwnerID = GetInt32OrDefault(reader,OffsetToUserID);
            proposedReturnValue.ItemDescription = GetStringOrDefault(reader, OffsetToDescription);
            proposedReturnValue.EMail = GetStringOrDefault(reader, OffsetToEMail);
            return proposedReturnValue;
        }
    }
}
