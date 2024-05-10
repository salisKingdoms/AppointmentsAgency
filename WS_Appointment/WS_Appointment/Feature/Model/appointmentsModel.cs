

namespace WS_Appointment.Feature.Model
{
    public class appointmentsModel
    {
        public long id { get; set; }
        public long customer_id { get; set; }
        public DateTime appointment_date { get; set; }
        public string status { get; set; }
        public string serviceType { get; set; }
        public string token { get; set; }
        public string appointmentNo { get; set; }
        public DateTime? created_on { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
