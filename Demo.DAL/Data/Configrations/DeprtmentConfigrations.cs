

using Demo.DAL.Models.DepartmentModels;

namespace Demo.DAL.Data.Configrations
{
    class DeprtmentConfigrations : BaseEntityConfigration<Department>,  IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("nvarchar(20)");
            builder.Property(D => D.Code).HasColumnType("nvarchar(20)");
            builder.HasMany(d=> d.Employees)
                .WithOne(e=> e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            base.Configure(builder);
        }
    }
}
