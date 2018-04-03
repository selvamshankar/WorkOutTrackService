using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkOutDBModel.Model
{
    [Table("workout_active")]
    public class Workout_Active
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", TypeName = "INT")]
        public int Id { get; set; }

        [ForeignKey("WorkOut")]
        [Column("workout_id", TypeName = "INT")]
        public int WorkOutId { get; set; }

        [Column("start_time", TypeName = "VARCHAR")]
        [StringLength(8)]

        public string Start_Time { get; set; }

        [Column("start_date", TypeName = "DATETIME")]
        public DateTime? Start_Date { get; set; }

        [Column("end_time", TypeName = "VARCHAR")]
        [StringLength(8)]
        public string End_time { get; set; }


        [Column("end_date", TypeName = "DATETIME")]
        public DateTime? End_Date { get; set; }

        [Column("comment", TypeName = "VARCHAR")]
        [StringLength(64)]
        public string Comment { get; set; }

        [Column("status", TypeName = "BIT")]
        public bool Status { get; set; }

        public WorkOut WorkOut { get; set; }

    }
}
