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
        int OffsetToItemPrice;
        int OffsetToEMail;

        public OwnedItemMapper(SqlDataReader reader)
        {
            OffsetToItemID = reader.GetOrdinal("ItemID");
            Assert(0 == OffsetToItemID, $"ItemID offset is {OffsetToItemID} not 0 as expected");
            OffsetToUserID = reader.GetOrdinal("OwnerID");
            Assert(1 == OffsetToUserID, $"ItemID offset is {OffsetToUserID} not 1 as expected");
            OffsetToDescription = reader.GetOrdinal("ItemDescription");
            Assert(2 == OffsetToDescription, $"ItemID offset is {OffsetToDescription} not 2 as expected");
            OffsetToItemPrice = reader.GetOrdinal("ItemPrice");
            Assert(3 == OffsetToItemPrice, $"ItemID offset is {OffsetToItemPrice} not 3 as expected");
            OffsetToEMail = reader.GetOrdinal("EMail");
            Assert(4 == OffsetToEMail, $"ItemID offset is {OffsetToEMail} not 3 as expected");

        }

        public OwnedItemDAL OwnedItemFromReader(SqlDataReader reader)
        {
            OwnedItemDAL proposedReturnValue = new OwnedItemDAL();
            proposedReturnValue.OwnedItemID = reader.GetInt32(OffsetToItemID);
            proposedReturnValue.OwnerID = GetInt32OrDefault(reader,OffsetToUserID);
            proposedReturnValue.ItemDescription = GetStringOrDefault(reader, OffsetToDescription);
            proposedReturnValue.ItemPrice = GetDecimalOrDefault(reader, OffsetToItemPrice);
            proposedReturnValue.EMail = GetStringOrDefault(reader, OffsetToEMail);
            return proposedReturnValue;
        }
    }
}
