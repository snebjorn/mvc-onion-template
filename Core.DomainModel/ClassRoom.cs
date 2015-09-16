namespace Core.DomainModel
{
    public class ClassRoom : IEntity
    {
        public int Id { get; set; }
        public int BuildingNumber { get; set; }
        public int Floor { get; set; }
        public virtual Course Course { get; set; }
    }
}
