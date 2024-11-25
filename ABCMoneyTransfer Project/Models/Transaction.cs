using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABCMoneyTransfer_Project.Models
{
    // ----------------------------------------------------------------------------------------------------------------------------------

    public class Transaction
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(100)]
        public string SenderFirstName { get; set; }

        [StringLength(100)]
        public string SenderMiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string SenderLastName { get; set; }

        [Required]
        [StringLength(200)]
        public string SenderAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string SenderCountry { get; set; }

        [Required]
        [StringLength(100)]
        public string ReceiverFirstName { get; set; }

        [StringLength(100)]
        public string ReceiverMiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string ReceiverLastName { get; set; }

        [Required]
        [StringLength(200)]
        public string ReceiverAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string ReceiverCountry { get; set; }

        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Transfer Amount must be greater then zero.")]
        public decimal TransferAmount { get; set; }

        public decimal ExchangeRate { get; set; }

        public decimal PayoutAmount { get; set; }

        [Required]
        public DateTime TransferDate { get; set; } = DateTime.Now;
    }

    // ----------------------------------------------------------------------------------------------------------------------------------
}
