using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOutDBModel.Model
{
    

        [Table("workout_collection")]
        public class WorkOut
        {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Column("workout_id", TypeName = "INT")]
            public int WorkOutId { get; set; }

            [Column("workout_title", TypeName = "VARCHAR")]
            [StringLength(128)]
            public string WorkOutTitle { get; set; }

            [Column("workout_note", TypeName = "VARCHAR")]
            [StringLength(256)]
            public string WorkOutNote { get; set; }

            [Column("calories_burn_per_min", TypeName = "INT")]

            public int CaloriesBurnPerMin { get; set; }

            public DateTime? Created { get; set; }

            public DateTime? Updated { get; set; }

            [ForeignKey("Category")]
            public int CategoryId { get; set; }

            public Category Category { get; set; }
        }

        public class WorkOut_VM
        {
            public int Id { get; set; }
            public int WorkOutId { get; set; }

            public string WorkOutTitle { get; set; }

            public string WorkOutComment { get; set; }

            public string StartDate { get; set; }
            public string StartTime { get; set; }
            public string EndDate { get; set; }
            public string EndTime { get; set; }

        }

    }
