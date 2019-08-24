using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class OwnedItemBLL
    {
        public OwnedItemBLL()
        {

        }
        public OwnedItemBLL(OwnedItemDAL dal)
        {
            this.OwnedItemID = dal.OwnedItemID;
            this.OwnerID = dal.OwnerID;
            this.ItemDescription = dal.ItemDescription;
            this.EMail = dal.EMail;

        }
        #region Direct properties

       // [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int OwnedItemID { get; set; }

      //  [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int OwnerID { get; set; }
 
        public string ItemDescription { get; set; }
        #endregion
        #region Indirect Properties
       
        public string EMail { get; set; }
        #endregion
        #region Business Domain Properties
        // none specified 
        #endregion Business

        public override string ToString()
        {
            return $"OwnedItemID: {OwnedItemID} OwnerID:{OwnerID} ItemDescription:{ItemDescription} Email:{EMail}";
        }



    }
}
