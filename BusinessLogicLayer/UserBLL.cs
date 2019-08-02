using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserBLL
    {

        public UserBLL(UserDAL dal)
        {
            // this.Age = dal.age;  // this.age is BLL only
            this.DateOfBirth = dal.DateOfBirth;
            this.EMail = dal.EMail;
            this.Hash = dal.Hash;
            //this.isAbleToPurchaseCigarettes = dal.isAbleToPurchaseCigarettes
            // this.isAbleToPurchaseCiggarettes is BLL only
            this.RoleID = dal.RoleID;
            this.RoleName = dal.RoleName;
            this.Salt = dal.Salt;
            this.UserID = dal.UserID;

        }
        #region Direct properties
        public int UserID { get; set; }
        public string EMail { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleID { get; set; }
        #endregion
        #region Indirect Properties
        public string RoleName { get; set; }
        #endregion Indirect

        #region Business Domain Properties
        public int Age
        {
            get
            { return (int)((DateOfBirth - DateTime.Now).Days/365.25); }
        }

        public bool isAbleToPurchaseCigarettes
        {
            get
            {
                if (17 < Age) return true;
                else return false;
            }
        }
        #endregion Business

        public override string ToString()
        {
            return $"UserID:{UserID} EMail:{EMail} DOB:{DateOfBirth} RoleID:{RoleID} RoleName:{RoleName} Age:{Age} Cigs: {isAbleToPurchaseCigarettes} Hash:{Hash} Salt:{Salt}";
        }



    }
}
