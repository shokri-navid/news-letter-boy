using System;
using System.Collections.Generic;

namespace NewsLetterBoy.Model
{
    public class BaseModel
    {
        protected BaseModel()
        {
        }

        public int Id { get; init; }
        public DateTime CreationDate  { get; private set; } = DateTime.Now;
        public DateTime? ModifyDate { get; protected set; } = null;
        public bool IsDeleted { get; protected set; } = false;

        protected void SetAsDeleted()
        {
            IsDeleted = true;
            ModifyDate = DateTime.Now;
        }

    }
}