
namespace Demo.DAL.Data.Contexts
{
    internal class DepartmentConfigrations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(P => P.Id).UseIdentityColumn(10, 10);
            builder.Property(P => P.Name).HasColumnType("nvarchar(20)");
            builder.Property(P => P.Code).HasColumnType("nvarchar(20)");
            builder.Property(P => P.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(P => P.LastModifiedOn).HasComputedColumnSql("GetDate()");
        }
    }
}