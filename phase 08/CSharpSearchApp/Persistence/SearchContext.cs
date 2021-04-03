using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Persistence
{
    class SearchContext : DbContext
    {
        public SearchContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<TokenModel> Tokens { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<TokenDocumentModel> TokenDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenDocumentModel>()
                .HasKey(tokenDocument =>
                    new { tokenDocument.TokenModelId, tokenDocument.DocumentModelId }
                );

            modelBuilder.Entity<TokenDocumentModel>()
                .HasOne(tokenDoc => tokenDoc.TokenModel)
                .WithMany(token => token.TokenDocumentModels)
                .HasForeignKey(tokenDoc => tokenDoc.TokenModelId);

            modelBuilder.Entity<TokenDocumentModel>()
                .HasOne(tokenDoc => tokenDoc.DocumentModel)
                .WithMany(doc => doc.TokenDocumentModels)
                .HasForeignKey(tokenDoc => tokenDoc.DocumentModelId);
        }
    }
}
