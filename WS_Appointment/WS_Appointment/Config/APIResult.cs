namespace WS_Appointment.Config
{
    public class APIResult
    {
        public bool is_ok { get; set; }
        public string message { get; set; }
    }

    public class APIResult<T> : APIResult
    {
        public T data { get; set; }
    }

    public class APIResultList<T> : APIResult
    {
        public T data { get; set; }
        public int? totalRow { get; set; }
    }
}
