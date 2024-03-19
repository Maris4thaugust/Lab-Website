using System.ComponentModel.DataAnnotations;

namespace WebSEnR.Models.ProjectsModel
{
    public class UniProject
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Member { get; set; }
        public string? Href { get; set; }
    }
}
