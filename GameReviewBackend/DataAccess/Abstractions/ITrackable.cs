using System.ComponentModel.DataAnnotations;

namespace DataAccess.Abstractions
{
    public interface ITrackable
    {
        public int Id { get; set; }

        [StringLength(60)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set;}
    }
}