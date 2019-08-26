using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer
{
    public class UserBLL
    {
        IDateTimeProvider _DateProvider;
        public UserBLL()
        {
            _DateProvider = DefaultDateProvider.DefaultProvider;
        }
        public UserBLL(IDateTimeProvider DateTimeProvider)
        {
            _DateProvider = DateTimeProvider;
        }

        public UserBLL(UserDAL dal, IDateTimeProvider DateTimeProvider) :this(dal)
        {
            _DateProvider = DateTimeProvider;
        }
        public UserBLL(UserDAL dal)
        {
            _DateProvider = DefaultDateProvider.DefaultProvider;
            // this.Age = dal.age;  // this.age is BLL only
            this.DateOfBirth = dal.DateOfBirth;
            this.EMail = dal.EMail;
            this.Hash = dal.Hash;
            //this.isAbleToPurchaseCigarettes = dal.isAbleToPurchaseCigarettes
            // this.isAbleToPurchaseCigarettes is BLL only
            this.RoleID = dal.RoleID;
            this.RoleName = dal.RoleName;
            this.Salt = dal.Salt;
            this.UserID = dal.UserID;

        }
        #region Direct properties
        
        [System.Web.Mvc.HiddenInput(DisplayValue =false)]
        
        public int UserID { get; set; }
        public string EMail { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int RoleID { get; set; }
        #endregion
        #region Indirect Properties
        public string RoleName { get; set; }
        #endregion Indirect

        #region Business Domain Properties
        public int Age
        {
            get
            { return (int)((_DateProvider.GetDateTime() - DateOfBirth).Days/365.25); }
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
