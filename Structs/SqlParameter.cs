namespace employee_raffles.Structs;

public class SqlParameter
{
    public string Name { get; set; }
    public object Value { get; set; }
    public SqlParameter(string name)
    {
        this.Name = name;
    }
    public SqlParameter(string name, object value)
    {
        this.Name = name;
        this.Value = value;
    }
}
