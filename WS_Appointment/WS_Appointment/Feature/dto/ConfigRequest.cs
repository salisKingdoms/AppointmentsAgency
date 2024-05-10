namespace WS_Appointment.Feature.dto
{
    public class ConfigRequest
    {
        public string config_value { get; set; }
        public string description { get; set; }
    }

    public class ConfigUpdateRequest
    {
        public long id { get; set; }
        public string config_value { get; set; }
        public string description { get; set; }
    }
}
