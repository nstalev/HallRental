
namespace HallRental.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RentalContract
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateSubmitted { get; set; }

        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.ContractSubmissionFileLength)]
        public byte[] ContractSubmission { get; set; }

    }
}
