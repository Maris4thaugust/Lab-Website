using System.ComponentModel.DataAnnotations;
using WebSEnR.Data.Enum;

namespace WebSEnR.Models.AboutLabModel
{
    public class Equipments
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ProductId { get; set; }
        public EquipmentCategory EquipmentCategory { get; set; }
        public string? Description { get; set; }
        //public string? Detail { get; set; }
        public string Image { get; set; }

    }
}
