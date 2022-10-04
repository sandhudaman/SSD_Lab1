///I, <Damanpreet_Singh>, student number <000741359>, certify that all code submitted is my own work;
///that I have not copied it from any other source. 
///I also certify that I have not allowed my work to be copied by others.

using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace Lab1.Models
{
    /// <summary>
    /// Team Modal class to collect teams data
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Team Id and Establised Date are auto generated with whenever a new team is created
        /// </summary>
        public Team()
        {
            Id = Guid.NewGuid();

            CreationTime = DateTime.Now;

        }
        /// <summary>
        /// Team Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// String For Name of Each Team
        /// </summary>
        [Required, StringLength(128)]
        public string TeamName { get; set; }

        /// <summary>
        /// String variable for email address of a team
        /// </summary>
        [Required]
        public string Email { get; set; }
        
        /// <summary>
        /// Creation time is used to get current time and then later converted to a string
        /// </summary>
        public DateTime CreationTime
        {
            get; set;
        }
        
        /// <summary>
        /// Create Date is converted into a string and Date value is parsed to Established Date
        /// </summary>
        [Display(Name = "Established Date")]
        public String EstablishedDate {

            get => this.CreationTime.ToString("D");
            set => this.CreationTime = DateTime.Parse(value);
        }
    }
}
