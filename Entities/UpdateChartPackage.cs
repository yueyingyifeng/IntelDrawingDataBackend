using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace IntelDrawingDataBackend.Entities
{
    public class UpdateChartPackage
    {
        [Required]
        public List<List<String>> data { get; set; }
        [Required]
        public long fileId { get; set; }
        [Required]
        public string fileName { get; set; }
        [Required]
        public string fileType { get; set; }


        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
