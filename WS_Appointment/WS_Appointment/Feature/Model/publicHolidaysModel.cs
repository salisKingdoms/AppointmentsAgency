namespace WS_Appointment.Feature.Model
{
    public class publicHolidaysModel
    {
        public long id { get; set; }
        public DateTime holiday_date { get; set; }
        public string description { get; set; }
        public DateTime? created_on { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
