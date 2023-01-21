namespace PeymanAkhtari.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Html{ get; set; }
        public string LinkText { get; set; }
        public string linkHref { get; set; }
        public  string img { get; set; }
        public int Sequence { get; set; }
        public int Language { get; set; }
        public ICollection<Feature> Feactures { get; set; }
    }
}
