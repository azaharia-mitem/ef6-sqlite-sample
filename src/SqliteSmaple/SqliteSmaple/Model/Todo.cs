using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteSmaple.Model
{
    [Table("Todo")]
    public class Todo
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Note { get; set; }

        public bool Done { get; set; }

        public bool Deleted { get; set; }
    }
}
