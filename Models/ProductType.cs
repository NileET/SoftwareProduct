namespace Software.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            SoftwareProducts = new HashSet<SoftwareProduct>();
        }

        public string TypeId { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<SoftwareProduct> SoftwareProducts { get; set; }
    }
}
