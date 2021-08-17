using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbSchema.Tests.Models
{
    public class AtypicalIdentity
    {
        [Key]        
        public int RoleId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// so that we can toggle in UI
        /// </summary>
        [NotMapped]
        public bool IsEnabled { get; set; }

    }
}
