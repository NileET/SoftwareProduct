namespace Software.Models
{
    public partial class SoftwareProduct
    {
        public SoftwareProduct()
        {
            DevelopmentPlayers = new HashSet<DevelopmentPlayer>();
        }

        public uint ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? TypeId { get; set; }
        public string Version { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        public uint? ContractId { get; set; }

        public virtual Contract? Contract { get; set; }
        public virtual ProductType? Type { get; set; }
        public virtual ICollection<DevelopmentPlayer> DevelopmentPlayers { get; set; }
    }
}
