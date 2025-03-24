namespace Demo.presentation.ViewModels.DepartmentViewModel
{
    public class DepartmentEditViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public string? Descreption { get; set; }

        public DateOnly DateOfCreation { get; set; }
    }
}
