using System;

namespace NewsLetterBoy.Model.NewsLetter
{
    public class NewsLetter: BaseModel
    {
        protected NewsLetter()
        {
        }

        public NewsLetter(string title, string description ="")
        {
            if (string.IsNullOrWhiteSpace(title)) throw new DomainException("Name should not be empty or white space", 400); 
            Title = title;
            Description = description; 
        }

        public void Modify(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new DomainException("Name should not be empty or white space", 400); 
            Title = title;
            Description = description;
            ModifyDate = DateTime.Now;
        }

        public  void SetAsRemoved()
        {
            base.SetAsDeleted();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        
    }
}