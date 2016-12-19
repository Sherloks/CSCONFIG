using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.AttributeViewModels
{
    public class EditViewModel
    {
        public Data.Models.Attribute Attribute { get; set; }

        public string MinNumber { get; set; }
        public string MaxNumber { get; set; }
        public string DefaultNumber { get; set; }
        public string StepNumber { get; set; }

        public string DefaultBoolean { get; set; }

        public string ValuesSelection { get; set; }
        public string DefaultSelection { get; set; }

        public EditViewModel()
        {
            Attribute = new Data.Models.Attribute();
        }
    }
}
