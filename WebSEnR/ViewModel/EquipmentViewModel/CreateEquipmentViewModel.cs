using WebSEnR.Data.Enum;

namespace WebSEnR.ViewModel.EquipmentViewModel
{
    public class CreateEquipmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ProductId { get; set; }
        public EquipmentCategory EquipmentCategory { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
