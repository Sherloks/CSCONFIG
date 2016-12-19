namespace Data.Models
{
    public class ConfigurationAttribute
    {
        public int ConfigurationAttributeId { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }


        public int ConfigurationId { get; set; }
        public Configuration Configuration { get; set; }
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
    }
}
