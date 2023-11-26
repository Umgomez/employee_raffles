using System.Text.RegularExpressions;
using System.Text.Json;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Transactions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace employee_raffles.Structs;

public class Sql
{
    DbConnection _conn { get; set; }
    public Sql(DbContext db)
    {
        _conn = db.Database.GetDbConnection();
    }
    public Sql(string connectionString)
    {
        _conn = new SqlConnection(connectionString);
    }
    public async Task<string> QueryFormat(string query, Dictionary<string, string> ps)
    {
        var psx = new Dictionary<string, object>();
        foreach (string key in ps.Keys)
            psx[key] = ps[key];

        return await QueryFormat(query, psx);
    }
    public async Task<string> QueryFormat(string query, params Dictionary<string, object>[] ps)
    {
        var tps = new Dictionary<string, object>();
        for (int i = 0; i < ps.Length; i++)
            tps = tps.Concat(ps[i]).ToDictionary(k => k.Key, v => v.Value);

        return await QueryFormat(query, tps);
    }
    public string ValueToSql(object v)
    {
        string tempv;
        if (v == null)
            tempv = "(NULL)";
        else if (v is int)
            tempv = v + "";
        else if (v is bool)
            tempv = (bool)v ? "'1'" : "'0'";
        else if (v is DateTime)
            tempv = "(DATEFROMPARTS('" + ((DateTime)v).Year + "', '" + ((DateTime)v).Month + "', '" + ((DateTime)v).Day + "', '" + ((DateTime)v).Hour + "', '" + ((DateTime)v).Minute + "', '" + ((DateTime)v).Second + "', 0))";
        else if (v is string && new Regex("[0-9]{4}-[0-1][0-9]-[0-3][0-9] [0-2][0-9]:[0-5][0-9]:[0-5][0-9]").IsMatch(v + ""))
            tempv = "(DATEFROMPARTS('" + v + "'))";
        else if (v is JsonElement && ((JsonElement)v).ValueKind == JsonValueKind.True)
            tempv = "'1'";
        else if (v is JsonElement && ((JsonElement)v).ValueKind == JsonValueKind.False)
            tempv = "'0'";
        else if (v.GetType().IsArray)
        {
            tempv = "";
            for (int i = 0; i < ((Array)v).Length; i++)
            {
                object _ = ((Array)v).GetValue(i);
                tempv += ValueToSql(_);
                if (i < ((Array)v).Length - 1)
                    tempv += ", ";
            }
        }
        else
            tempv = $"'{Regex.Replace(v + "", "'", "''")}'";
        return tempv;
    }
    public async Task<string> QueryFormat(string query, Dictionary<string, object> ps)
    {
        var tempQuery = query;
        foreach (var k in ps.Keys)
        {
            string tempv = ValueToSql(ps[k]);
            var tempk = Regex.Replace(k, "[@:]", "");
            tempQuery = Regex.Replace(tempQuery, $":{tempk}|@{tempk}", tempv);
        }
        string[] tempQueryArr = tempQuery.Split("'");
        tempQuery = "";
        for (int i = 0; i < tempQueryArr.Length; i++)
        {
            if (i % 2 == 0)
                tempQuery += Regex.Replace(tempQueryArr[i], @"(?<!:):[\w]+", "NULL");
            else
                tempQuery += "'" + tempQueryArr[i] + "'";
        }
        return tempQuery;
    }
    public async Task<string> MakeWhere(Dictionary<string, object> filter, bool useWhere = true)
    {
        if (filter.Count == 0)
            return "";
        var whereClause = new List<string>();
        filter ??= new Dictionary<string, object>();
        var keys = new List<string>(filter.Keys);

        foreach (var key in keys)
            if (filter[key] is string)
                filter[key] = Regex.Replace(filter[key].ToString(), "'", "''");

        if (filter.Count > 0)
        {
            foreach (string key in filter.Keys)
            {
                string temp = ":" + key;
                temp = await QueryFormat(temp, new Dictionary<string, object> { { key, filter[key] } });
                if (filter[key].GetType().IsArray && ((Array)filter[key]).Length == 0)
                {
                }
                if (filter[key].GetType().IsArray)
                    whereClause.Add($"{key} IN ({temp})");
                else
                    whereClause.Add($"{key} = {temp}");
            }
        }
        return (useWhere ? "\n WHERE " : "\n AND ") + string.Join(" AND ", whereClause);
    }
    public async Task<List<Dictionary<string, object>>> OneQuery(string query, Dictionary<string, object> ps = null)
    {
        ps ??= new Dictionary<string, object>();
        List<SqlParameter> tempps = new();
        foreach (var k in ps.Keys)
            tempps.Add(new SqlParameter(k, ps[k]));

        return await OneQuery(query, tempps.ToArray());
    }
    public async Task<List<Dictionary<string, object>>> OneQuery(string query, params SqlParameter[] ps)
    {
        var table = new List<Dictionary<string, object>>();
        using (var cmd = _conn.CreateCommand())
        {
            cmd.CommandText = query;
            foreach (var p in ps)
            {
                var qp = cmd.CreateParameter();
                qp.ParameterName = "@" + p.Name.Replace("@", "");
                qp.Value = p.Value;
                cmd.Parameters.Add(qp);
            }

            if (_conn.State != System.Data.ConnectionState.Open)
                _conn.Open();

            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted });

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, object> row = new();
                for (var c = 0; c < reader.FieldCount; c++)
                    if (reader.IsDBNull(c))
                        row.Add(reader.GetName(c), null);
                    else
                        row.Add(reader.GetName(c), reader.GetValue(c));
                table.Add(row);
            }
            scope.Complete();
            _conn.Close();
        }
        return table;
    }
    public async Task<List<List<Dictionary<string, object>>>> MultipleQuery(string query, Dictionary<string, object> ps = null)
    {
        ps ??= new Dictionary<string, object>();
        List<SqlParameter> tempps = new();
        foreach (var k in ps.Keys)
            tempps.Add(new SqlParameter(k, ps[k]));

        return await MultipleQuery(query, tempps.ToArray());
    }
    public async Task<List<List<Dictionary<string, object>>>> MultipleQuery(string query, params SqlParameter[] ps)
    {
        var tables = new List<List<Dictionary<string, object>>>();
        query = query.Replace(":", "@");
        using (var cmd = _conn.CreateCommand())
        {
            cmd.CommandText = query;
            foreach (var p in ps)
            {
                var qp = cmd.CreateParameter();
                qp.ParameterName = p.Name;
                qp.Value = p.Value;
                cmd.Parameters.Add(qp);
            }

            if (_conn.State != System.Data.ConnectionState.Open)
                _conn.Open();

            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted });

            using var reader = cmd.ExecuteReader();
            do
            {
                List<Dictionary<string, object>> table = new();
                while (reader.Read())
                {
                    Dictionary<string, object> row = new();
                    for (var c = 0; c < reader.FieldCount; c++)
                        row.Add(reader.GetName(c), reader.GetValue(c));
                    table.Add(row);
                }
                tables.Add(table);
            } while (reader.NextResult());
            scope.Complete();
            _conn.Close();
        }
        return tables;
    }
    public string GetCatalog(string connectionString)
    {
        if (connectionString == null)
            return null;

        var properties = connectionString.Split(";");
        string catalog = null;

        foreach (var property in properties)
        {
            var lr = property.Trim().Split("=");
            if (lr[0].Trim().ToLower() == "initial catalog")
                catalog = lr[1].Trim();
        }
        return catalog;
    }

    //Para el reporte
    public List<List<Dictionary<string, object>>> MultipleQuery2(string query, Dictionary<string, object> ps = null)
    {
        ps ??= new Dictionary<string, object>();
        List<SqlParameter> tempps = new();
        foreach (var k in ps.Keys)
            tempps.Add(new SqlParameter(k, ps[k]));

        return MultipleQuery2(query, tempps.ToArray());
    }
    public List<List<Dictionary<string, object>>> MultipleQuery2(string query, params SqlParameter[] ps)
    {
        var tables = new List<List<Dictionary<string, object>>>();
        query = query.Replace(":", "@");
        using (var cmd = _conn.CreateCommand())
        {
            cmd.CommandText = query;
            foreach (var p in ps)
            {
                var qp = cmd.CreateParameter();
                qp.ParameterName = p.Name;
                qp.Value = p.Value;
                cmd.Parameters.Add(qp);
            }

            if (_conn.State != System.Data.ConnectionState.Open)
                _conn.Open();

            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted });

            using var reader = cmd.ExecuteReader();
            do
            {
                List<Dictionary<string, object>> table = new();
                while (reader.Read())
                {
                    Dictionary<string, object> row = new();
                    for (var c = 0; c < reader.FieldCount; c++)
                        row.Add(reader.GetName(c), reader.GetValue(c));
                    table.Add(row);
                }
                tables.Add(table);
            } while (reader.NextResult());
            scope.Complete();
            _conn.Close();
        }
        return tables;
    }
}
