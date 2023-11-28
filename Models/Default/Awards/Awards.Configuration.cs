using Microsoft.EntityFrameworkCore;

namespace employee_raffles.Models.Default;

public class AwardsConfiguration
{
    public static void Configure(ModelBuilder mb)
    {
        mb.Entity<Awards>(opt => {
            opt.ToTable("Awards");
            opt.HasKey(x => x.ID);
            opt.Property(x => x.Amount)
              .HasMaxLength(20);

            #region Constranints
            opt.HasIndex(x => new { x.Sequence, x.Amount })
              .HasDatabaseName("UQ_Awards")
              .IsUnique();
            opt.HasCheckConstraint("CHK_Awards_Sequence", "Sequence <> 0");
            opt.HasCheckConstraint("CHK_Awards_Amount", "Amount <> ''");
            #endregion
        });
    }
}
