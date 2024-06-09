using System.ComponentModel.DataAnnotations;

namespace DataAccess.Abstractions
{
    public interface ITrackable
    {
        public int Id { get; set; }

        [StringLength(25)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set;}
    }
}