using AO.Models.Extensions;
using AO.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AO.Models.Models
{
    /// <summary>
    /// use as basis of your own user profile table
    /// </summary>
    [Table("AspNetUsers")]
    [Identity(nameof(UserId))]
    public class UserProfileBase : IUserBase
    {
        [Key]
        [MaxLength(450)]
        public string Id { get; set; }
        
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Name => UserName;

        [MaxLength(50)]
        public string TimezoneId { get; set; }

        public DateTime LocalTime => Timestamp.Local(TimezoneId);
    }
}
