using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    interface IDateTimeProvider
    {
        DateTime GetDateTime();
    }
    class MeaningfulCalculator
    {
        IDateTimeProvider _dateProvider;
        public MeaningfulCalculator(IDateTimeProvider DateProvider)
        {
            _dateProvider = DateProvider;
        }
    }
}
