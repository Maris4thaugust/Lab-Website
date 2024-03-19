using WebSEnR.Data.Enum;

namespace WebSEnR.ViewModel.EquipmentViewModel
{
    public class EditEquipmentViewModel
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string? ProductId { get; set; }
        public EquipmentCategory EquipmentCategory { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }

        public string? URL { get; set; }
    }
}
