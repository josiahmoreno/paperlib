namespace MenuData
{
    public interface IActionMenuData
    {
        string Name { get; }
        IOption[] Options { get; }
    }
}