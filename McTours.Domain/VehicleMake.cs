namespace McTours.Domain
{
    public class VehicleMake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<VehicleModel> VehicleModels { get; set; }
    }
}