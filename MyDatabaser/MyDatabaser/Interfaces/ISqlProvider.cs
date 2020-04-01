using System;

namespace MyDatabaser.Interfaces
{
    public interface ISqlProvider
    {
        void Execute(string sql, Action<string> trace, Action<string> traceError);
    }
}
