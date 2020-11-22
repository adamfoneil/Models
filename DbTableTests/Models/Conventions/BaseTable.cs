using AO.Models.Enums;
using AO.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DbTableTests.Models.Conventions
{
    public abstract class BaseTable
    {        
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [MaxLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? DateModified { get; set; }

        [MaxLength(50)]
        public string ModifiedBy { get; set; }

        public static void Stamp(BaseTable model, SaveAction saveAction, IUserBase user)
        {
            switch (saveAction)
            {
                case SaveAction.Insert:
                    model.CreatedBy = user.Name;
                    model.DateCreated = user.LocalTime;
                    break;

                case SaveAction.Update:
                    model.ModifiedBy = user.Name;
                    model.DateModified = user.LocalTime;
                    break;
            }
        }
    }
}
