using System.ComponentModel.DataAnnotations;

namespace PeymanAkhtari.Models
{
    public class SiteSetting
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
