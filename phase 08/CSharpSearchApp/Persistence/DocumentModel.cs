using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence
{
    [Index(nameof(Name), IsUnique = true)]
    class DocumentModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public List<TokenDocumentModel> TokenDocumentModels { get; set; }
    }
}
