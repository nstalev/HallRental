
namespace HallRental.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Contract
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateSubmitted { get; set; }

        [Required]
        [MaxLength(DataConstants.ContractSubmissionFileLength)]
        public byte[] ContractSubmission { get; set; }

    }
}
