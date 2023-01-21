namespace PeymanAkhtari.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int EnglishName { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string   img1Class { get; set; }
        public string   Img2Class { get; set; }
        public int Sequence { get; set; }
        public int ProjectId { get; set; }
        public Project project { get; set; }
    }
}
