namespace WS_Appointment.Feature.dto
{
    public class AppointmentRequest
    {
        public long customer_id { get; set; }
        public DateTime appointment_date { get; set; }
        public string status { get; set; }
        public string serviceType { get; set; }
    }

    public class AppointmentCountMax
    {
        public int totalAppToday { get; set; }
        public int totalMax { get; set; }
    }

    public class AppointmentHolidayDate
    {
        public DateTime holiday_date { get; set; }
    }

    public class AppointmentUpdateRequest
    {
        public long id { get; set; }
        public long customer_id { get; set; }
        public DateTime appointment_date { get; set; }
        public string status { get; set; }
        public string serviceType { get; set; }
    }
}
