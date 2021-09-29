using MyDatabaser.Interfaces;
using MyDatabaser.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyDatabaser.Services
{
    public class StageStorage : IStageStorage
    {
        private readonly IStorage _storage;
        public StageStorage(IStorage storage)
        {
            _storage = storage;
        }

        public void AddStage(SqlConnectionData stage)
        {
            var stageNames = GetStageNames();
            var newStageNames = stageNames.Union(new[] { stage.Name }).ToList();

            SetStageNames(newStageNames);
            SetStage(stage);
        }

        public List<string> GetStageNames()
        {
            var existStagesStr = _storage.Get("Stages");
            var stages = (existStagesStr ?? "").Split('&');

            return stages.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        private void SetStageNames(IEnumerable<string> stageNames)
        {
            var stagesStr = string.Join("&", stageNames.Where(x => !string.IsNullOrEmpty(x)));
            _storage.Set("Stages", stagesStr);
        }

        public SqlConnectionData GetStage(string stageName)
        {
            return new SqlConnectionData
            {
                Name = stageName,
                Database = _storage.Get($"Stages.{stageName}.Database"),
                Host = _storage.Get($"Stages.{stageName}.Host"),
                UserName = _storage.Get($"Stages.{stageName}.UserName"),
                Password = _storage.Get($"Stages.{stageName}.Password"),
            };
        }

        private void SetStage(SqlConnectionData stage)
        {
            _storage.Set($"Stages.{stage.Name}.Database", stage.Database);
            _storage.Set($"Stages.{stage.Name}.Host", stage.Host);
            _storage.Set($"Stages.{stage.Name}.UserName", stage.UserName);
            _storage.Set($"Stages.{stage.Name}.Password", stage.Password);
        }

        public List<SqlConnectionData> GetStages()
        {
            var stageNames = GetStageNames();

            var response = new List<SqlConnectionData>();
            foreach (var stageName in stageNames)
            {
                var stage = GetStage(stageName);
                response.Add(stage);
            }

            return response;
        }
    }
}
