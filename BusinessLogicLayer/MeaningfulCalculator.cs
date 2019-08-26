using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IDateTimeProvider
    {
        DateTime GetDateTime();
    }

    public class DefaultDateProvider
    {
        static IDateTimeProvider _defaultProvider = new NowDateTimeProvider();
        public static IDateTimeProvider DefaultProvider 
        
        {
            get {return _defaultProvider;}
            set {_defaultProvider = value;}
        }
           
    }

    public class NowDateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }
    }

    public class ThisDateTimeProvider : IDateTimeProvider
    {
        DateTime _thisTime;

        public ThisDateTimeProvider(DateTime thisTime)
        {
            _thisTime = thisTime;
        }
        public DateTime GetDateTime()
        {
            return _thisTime;
        }
    }

    public class ItemStats
    {
        
        public string EMail { get; set; }
        public int Count { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        
    }

    public class MeaningfulCalculator
    {
        IDateTimeProvider _dateProvider;
        public MeaningfulCalculator()
        {
            _dateProvider = DefaultDateProvider.DefaultProvider;
        }

        public MeaningfulCalculator(IDateTimeProvider DateProvider)
        {
            _dateProvider = DateProvider;
        }

        public double AverageAge(List<UserBLL>Users)
        {
            return Users.Average(u => u.Age);
        }

        public List<ItemStats> ComputeStats(List<OwnedItemBLL> Items)
        {
            var Q1 = from itm in Items
                     group itm by itm.EMail into g
                     select new ItemStats() { EMail = g.Key, Count=g.Count(), AveragePrice=g.Average(i=>i.ItemPrice), MinPrice=g.Min(i=>i.ItemPrice), MaxPrice=g.Max(i=>i.ItemPrice) };
            return Q1.ToList();
        }

 
    }
}
