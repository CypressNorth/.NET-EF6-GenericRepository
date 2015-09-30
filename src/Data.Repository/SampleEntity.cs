using System.ComponentModel.DataAnnotations;

namespace Data.Repository
{
    /// <summary>
    /// Sample model class .. 
    /// </summary>
    public class SampleEntity
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}