namespace Data.Models
{
    public class Attribute
    {
        public int AttributeId { get; set; }
        public string Name { get; set; }
        public string CVar { get; set; }
        public string Tooltip { get; set; }
        public string View { get; set; }
        public string ViewConfig { get; set; }

        public int AttributeTypeId { get; set; }
        public AttributeType AttributeType { get; set; }

        public int AttributeCategoryId { get; set; }
        public AttributeCategory AttributeCategory { get; set; }
    }
}
