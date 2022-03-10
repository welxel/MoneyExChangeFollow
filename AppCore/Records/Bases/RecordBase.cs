using System.ComponentModel.DataAnnotations;

namespace AppCore.Records.Bases
{
    public class RecordBase
    {
        [Key]
        [Newtonsoft.Json.JsonIgnore]
        public int Id { get; set; }
    }
}
