using System;
using System.Collections.Generic;
using System.Text;

namespace Concordance
{
    public class Concordance
    {
        public string Word { get; set; }

        public int Frequency { get; set; }

        public List<int> IndexList { get; set; }
    }
}
