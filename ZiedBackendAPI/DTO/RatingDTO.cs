using System.ComponentModel.DataAnnotations;

namespace ZiedBackendAPI.DTO
{
    public class RatingDTO
    {
        [Range(1, 5)]
        public int Rate { get; set; }
        public int PersonId { get; set; }
    }
}
