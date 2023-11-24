using System.Collections.Generic;

namespace employee_raffles.Structs;

public class Return
{
    public string Success { get; set; }
    public dynamic Error { get; set; }
    public dynamic Data { get; set; }
    public Return(string Success = null)
    {
        this.Success = Success;
    }
    public Return(dynamic Error)
    {
        this.Error = Error;
        this.Data = new List<object>();
    }
    public Return SetSuccess(string Success)
    {
        this.Success = Success;
        return this;
    }
    public Return SetError(dynamic Error)
    {
        this.Error = Error;
        return this;
    }
    public Return SetData(dynamic Data)
    {
        this.Data = Data;
        return this;
    }
    public T GetInError<T>(string Key)
    {
        System.Type type = this.Error.GetType();
        T Value = (T)type.GetProperty(Key).GetValue(this.Error, null);
        return Value;
    }
    public T GetInData<T>(string Key)
    {
        System.Type type = this.Data.GetType();
        T Value = (T)type.GetProperty(Key).GetValue(this.Data, null);
        return Value;
    }
}
