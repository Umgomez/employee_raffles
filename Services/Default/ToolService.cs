using System.Globalization;
using System.Text.RegularExpressions;

namespace employee_raffles.Services;

public interface IToolService
{
    bool ToBool(Dictionary<string, object> Entity, string Key);
    bool? ToNullBool(Dictionary<string, object> Entity, string Key);
    int ToInt(Dictionary<string, object> Entity, string Key);
    int? ToNullInt(Dictionary<string, object> Entity, string Key);
    decimal ToDecimal(Dictionary<string, object> Entity, string Key);
    string GetFormatFileName(string fileName);
    string[] Arr(params string[] list);
    Int16[] HexToRgb(string hex);
    Int16[] HexToRgba(string hex);
    Int16[] HexToArgb(string hex);
    string Format(string str, string format);
    string Format(DateTime? date, string format);
    string Format<T>(T str);
    string Repeat(string str, int count);
}
public class ToolService: BaseService, IToolService
{
    #region bool,bool?
    public bool ToBool(Dictionary<string, object> Entity, string Key)
    {
        if (Entity.ContainsKey(Key))
        {
            if (Entity.GetValueOrDefault(Key) is int && (int?)Entity.GetValueOrDefault(Key) == 1)
                return true;
            if (Entity.GetValueOrDefault(Key) is int && (int?)Entity.GetValueOrDefault(Key) == 0)
                return false;
            return bool.TryParse(Entity.GetValueOrDefault(Key) + "", out bool result) ? result : false;
        }
        return false;
    }
    public bool? ToNullBool(Dictionary<string, object> Entity, string Key)
    {
        if (Entity.ContainsKey(Key))
        {
            if (Entity.GetValueOrDefault(Key) is int && (int?)Entity.GetValueOrDefault(Key) == 1)
                return true;
            if (Entity.GetValueOrDefault(Key) is int && (int?)Entity.GetValueOrDefault(Key) == 0)
                return false;
            return bool.TryParse(Entity.GetValueOrDefault(Key) + "", out bool result) ? result : (bool?)null;
        }
        return null;
    }
    #endregion

    #region int,int?
    public int ToInt(Dictionary<string, object> Entity, string Key)
    {
        if (Entity.ContainsKey(Key))
        {
            if (Entity.GetValueOrDefault(Key) is bool && (bool?)Entity.GetValueOrDefault(Key) == true)
                return 1;
            if (Entity.GetValueOrDefault(Key) is bool && (bool?)Entity.GetValueOrDefault(Key) == false)
                return 0;
            return int.TryParse(Entity.GetValueOrDefault(Key) + "", out int result) ? result : 0;
        }
        return 0;
    }
    public int? ToNullInt(Dictionary<string, object> Entity, string Key)
    {
        if (Entity.ContainsKey(Key))
        {
            if (Entity.GetValueOrDefault(Key) is bool && (bool?)Entity.GetValueOrDefault(Key) == true)
                return 1;
            if (Entity.GetValueOrDefault(Key) is bool && (bool?)Entity.GetValueOrDefault(Key) == false)
                return 0;
            return int.TryParse(Entity.GetValueOrDefault(Key) + "", out int result) ? result : (int?)null;
        }
        return null;
    }
    #endregion

    #region Decimal
    public decimal ToDecimal(Dictionary<string, object> Entity, string Key)
    {
        if (Entity.ContainsKey(Key))
        {
            if (Entity[Key] == null)
                return 0;
            if (decimal.TryParse(Entity[Key] + "", out decimal result))
                return result;
        }
        return 0;
    }
    #endregion

    public string GetFormatFileName(string fileName)
    {
        var tempFileName = Regex.Replace(fileName, "[ ]", "-");
        tempFileName = Regex.Replace(tempFileName, "[^A-z0-9\\.-_@]", "");
        return $"{tempFileName}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";
    }
    public string[] Arr(params string[] list)
    {
        return list;
    }
    public Int16[] HexToRgb(string hex)
    {
        Int16[] rgb = new Int16[] { 0, 0, 0 };
        hex = hex.Replace("#", "");
        rgb[0] = Int16.Parse(hex[..2], NumberStyles.AllowHexSpecifier);
        rgb[1] = Int16.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
        rgb[2] = Int16.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);
        return rgb;
    }
    public Int16[] HexToRgba(string hex)
    {
        Int16[] rgb = new Int16[] { 0, 0, 0, 0 };
        hex = hex.Replace("#", "");
        rgb[0] = Int16.Parse(hex[..2], NumberStyles.AllowHexSpecifier);
        rgb[1] = Int16.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
        rgb[2] = Int16.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);
        rgb[3] = Int16.Parse(hex.Substring(6, 2), NumberStyles.AllowHexSpecifier);
        return rgb;
    }
    public Int16[] HexToArgb(string hex)
    {
        Int16[] rgb = new Int16[] { 0, 0, 0, 0 };
        hex = hex.Replace("#", "");
        rgb[1] = Int16.Parse(hex[..2], NumberStyles.AllowHexSpecifier);
        rgb[2] = Int16.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
        rgb[3] = Int16.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);
        rgb[0] = Int16.Parse(hex.Substring(6, 2), NumberStyles.AllowHexSpecifier);
        return rgb;
    }
    public string Format(string str, string format)
    {
        str ??= "";
        if (decimal.TryParse(str, out _))
        {
            if (Regex.IsMatch(format.ToUpper(), @"^D"))
                return int.Parse(str).ToString(format);
            else if (Regex.IsMatch(format.ToUpper(), @"^N"))
                return decimal.Parse(str).ToString(format);
        }
        else if (DateTime.TryParse(str, out _))
        {
            return DateTime.Parse(str).ToString(format);
        }
        return "";
    }
    public string Format(DateTime? date, string format)
    {
        if (date == null)
            return "";
        return string.Format("{0:" + format + "}", (date ?? new DateTime()));
    }
    public string Format<T>(T str)
    {
        if (str == null)
            return "";
        return str.ToString();
    }

    public string Repeat(string str, int count)
    {
        return string.Concat(Enumerable.Repeat(str, count));
    }
    public string ReplaceLastOccurrence(string Source, string Find, string Replace)
    {
        int place = Source.LastIndexOf(Find);

        if (place == -1)
            return Source;

        string result = Source.Remove(place, Find.Length).Insert(place, Replace);
        return result;
    }
}
