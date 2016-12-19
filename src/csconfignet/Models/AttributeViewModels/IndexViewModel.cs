using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Web.Models.AttributeViewModels
{
    public class IndexViewModel
    {
        public List<AttributeType> AttributeTypes { get; set; }
        public List<AttributeCategory> AttributeCategories { get; set; }
        public List<Attribute> Attributes { get; set; }

        public Attribute NewAttribute { get; set; }

        public IndexViewModel()
        {
            NewAttribute = new Attribute();
        }
    }
}
