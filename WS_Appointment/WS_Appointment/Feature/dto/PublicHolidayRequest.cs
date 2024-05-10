namespace WS_Appointment.Feature.dto
{
    public class PublicHolidayRequest
    {
        public DateTime holiday_date { get; set; }
        public string description { get; set; }
    }

    public class PublicHolidayUpdateRequest
    {
        public long id { get; set; }
        public DateTime holiday_date { get; set; }
        public string description { get; set; }
    }
}
