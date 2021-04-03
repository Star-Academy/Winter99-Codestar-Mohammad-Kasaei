using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Persistence
{
    [Index(nameof(Value), IsUnique = true)]
    class TokenModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        public List<TokenDocumentModel> TokenDocumentModels { get; set; }
    }
}
