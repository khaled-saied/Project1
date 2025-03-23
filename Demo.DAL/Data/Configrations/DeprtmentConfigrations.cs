

namespace Demo.DAL.Data.Configrations
{
    class DeprtmentConfigrations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("nvarchar(20)");
            builder.Property(D => D.Code).HasColumnType("nvarchar(20)");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("getdate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("getdate()");
        }
    }
}
