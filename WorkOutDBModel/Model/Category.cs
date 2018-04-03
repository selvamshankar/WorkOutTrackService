using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOutDBModel.Model
{
    public class Category
    {
        public Category() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("category_id", TypeName = "INT")]
        public int CategoryId { get; set; }



        [Column("category_name", TypeName = "VARCHAR")]
        [StringLength(65)]
        public string CategoryName { get; set; }


        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }


        public ICollection<WorkOut> WorkOuts { get; set; }
    }
}
