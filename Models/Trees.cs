using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Trees
    {
        public int Id { get; set; }
        public string authName { get; set; }
        public string Url { get; set; }

        public List<Trees> children { get; set; }
    }
}
