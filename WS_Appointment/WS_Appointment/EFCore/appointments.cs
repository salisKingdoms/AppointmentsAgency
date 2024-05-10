using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS_Appointment.EFCore
{
    [Table("appointments")]
    public class appointments
    {
        [Key,Required]
        public long id { get; set; }
        [Required]
        public long customer_id { get; set; }
        [Required]
        public DateTime appointment_date { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public string? serviceType { get; set; }
        public string? token { get; set; }
        public string? appointmentNo { get; set; }
        public DateTime? created_on { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string? updated_by { get; set; }
    }
}
