using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlientRakendus.Data
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public bool Completed { get; set; }
    }
}
