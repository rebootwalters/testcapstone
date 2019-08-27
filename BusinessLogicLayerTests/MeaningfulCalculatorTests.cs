using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tests
{


    [TestClass()]
    public class MeaningfulCalculatorTests
    {
        List<UserBLL> MakeSampleUsers(int count)
        {
            List<UserBLL> proposedReturnValue = new List<UserBLL>();
            for (int i = 0; i < count; i++)
            {
                UserBLL u = new UserBLL();
                u.UserID = i;
                u.EMail = $"User{i}@Email.com";
                u.Hash = $"HASHUSER{i}";
                u.Salt = $"SALT{i}";
                u.RoleID = i % 3;
                u.DateOfBirth = new DateTime(1900 + i * 5, 1, 1);
                proposedReturnValue.Add(u);
            }
            return proposedReturnValue;

        }


        [TestMethod()]
        public void When_NoUsers_Expect_AverageToBeZero()
        {
            // arrange
            ThisDateTimeProvider now = new ThisDateTimeProvider(1999, 1, 1);
            MeaningfulCalculator mc = new MeaningfulCalculator(now);
            var Users = MakeSampleUsers(0);
            double expected = 0;
            // act
            double actual = mc.AverageAge(Users);
            // assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void When_UsersIsNull_Expect_AverageToBeZero()
        {
            // arrange
            ThisDateTimeProvider now = new ThisDateTimeProvider(1999, 1, 1);
            MeaningfulCalculator mc = new MeaningfulCalculator(now);
            
            double expected = 0;
            // act
            double actual = mc.AverageAge(null);
            // assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void When_UsersIsNonNull_Expect_AverageToBeNonZero()
        {
            // arrange
            ThisDateTimeProvider now = new ThisDateTimeProvider(1999, 1, 1);
            MeaningfulCalculator mc = new MeaningfulCalculator(now);
            List<UserBLL> Users = MakeSampleUsers(4);

            double expected = 0;
            // act
            double actual = mc.AverageAge(Users);
            // assert
            Assert.AreNotEqual(expected, actual);
        }
    }
}