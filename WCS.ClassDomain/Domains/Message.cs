

namespace WCS.ClassDomain.Domains
{
    public class Message
    {
        public int Message_ID { get; set; }
        public string Message_FullName { get; set; }
        public string Message_Mobile { get; set; }
        public string Message_Text { get; set; }
        public int Message_TenantID { get; set; }
        public DateTime? Message_DateSend { get; set; }
    }
}
