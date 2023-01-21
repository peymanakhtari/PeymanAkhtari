using System.ComponentModel.DataAnnotations;

namespace PeymanAkhtari.Models
{
    public class Content
    {
       
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string lang { get; set; }
    }
}
