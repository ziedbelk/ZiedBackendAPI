using ZiedBackendAPI.Entities;

namespace ZiedBackendAPI.Entities
{
    public class PersonSector
    {
        public int SectorId { get; set; }
        public int PersonId { get; set; }
        public Sector Sector { get; set; }
        public Person Person { get; set; }
    }
}
