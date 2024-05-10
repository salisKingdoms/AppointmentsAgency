using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS_Appointment.EFCore
{
    [Table("configuration")]
    public class configuration
    {
        [Key,Required]
        public long id { get; set; }
        [Required]
        public string config_value { get; set; }
        public string? description { get; set; }
        public DateTime? created_on { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string? updated_by { get; set; }
    }
}
