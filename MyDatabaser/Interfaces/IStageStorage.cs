using MyDatabaser.Models;
using System.Collections.Generic;

namespace MyDatabaser.Interfaces
{
    public interface IStageStorage
    {
        List<SqlConnectionData> GetStages();
        List<string> GetStageNames();
        void AddStage(SqlConnectionData stage);
        SqlConnectionData GetStage(string stageName);
    }
}
