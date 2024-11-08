using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Computer
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public DateTime UpgradeDate { get; set; }

        public bool NeedsReplacement()
        {
            return (DateTime.Now - UpgradeDate).TotalDays > 5 * 365 + 1;
        }
    }
}
