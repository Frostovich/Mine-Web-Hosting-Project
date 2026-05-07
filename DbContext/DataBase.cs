namespace Full_proj.DbContext;
using Full_proj.Domain_Models;
using Microsoft.EntityFrameworkCore;

public class DataBase : DbContext
{
    DbSet<Contacts> Contacts { get; set; }
    public DbSet<User> Users { get; set; }
    public DataBase(DbContextOptions<DataBase> options) : base(options)
    {
    }

    public DbSet<Messages> Message { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //UsernameLenght
        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasMaxLength(128)
            .IsRequired();
        //EmailLenght
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(128)
            .IsRequired();
        //passwordLenght
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .HasMaxLength(128)
            .IsRequired();
        //OneUserHasManyMessages
        modelBuilder.Entity<User>()
            .HasMany(u => u.Messages)
            .WithOne(m => m.User)
            .OnDelete(DeleteBehavior.Cascade);
        //ContactColumn
        modelBuilder.Entity<Contacts>(entity =>
        {
            entity.HasOne(c => c.Owner)
                .WithMany(u => u.OwnedContacts)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Contact)
                .WithMany(u => u.ContactOfOthers)
                .HasForeignKey(c => c.ContactId)
                .OnDelete(DeleteBehavior.Restrict);
            
        });
        modelBuilder.Entity<Messages>((options) =>
        {
            options.HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            options.HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        });

    }
}