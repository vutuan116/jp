namespace JpT.Entity
{
    public class DataEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Level { get; set; }
        public string Lesson { get; set; }
        public string Kanji { get; set; }
        public string Hiragana { get; set; }
        public string CnVi { get; set; }
        public string Mean { get; set; }
        public string Hard { get; set; }
        public string Lock { get; set; }
        public string LastLearn { get; set; }

        public bool isEmpty()
        {
            return string.IsNullOrEmpty(Lesson + Kanji + Hiragana + Mean);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            return this.Id == ((DataEntity)obj).Id;
        }
    }
}
