using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;

namespace TestModel
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new MLContext();

            Console.WriteLine(Directory.GetCurrentDirectory());

            IDataView test_data = context.Data.LoadFromTextFile<DDAData>("test_data.csv", separatorChar: ',', hasHeader: true);

            var pipelineForEnemyDamage = context.Transforms.CopyColumns("Label", "enemyDamageMultiplier")
            .Append(context.Transforms.Concatenate("Features", "passiveScore", "activeScore", "hitScore", "playerHitScore"))
            .Append(context.Regression.Trainers.FastTree())
            .Append(context.Transforms.CopyColumns(outputColumnName: "EnemyDamageMultiplier", inputColumnName: "Score"));

            var pipelineForEnemyDefense = context.Transforms.CopyColumns("Label", "enemyDefenseMultiplier")
            .Append(context.Transforms.Concatenate("Features", "passiveScore", "activeScore", "hitScore", "playerHitScore"))
            .Append(context.Regression.Trainers.FastTree())
            .Append(context.Transforms.CopyColumns(outputColumnName: "EnemyDefenseMultiplier", inputColumnName: "Score"));

            var pipelineForEnemyMaxLife = context.Transforms.CopyColumns("Label", "enemyMaxLifeMultiplier")
            .Append(context.Transforms.Concatenate("Features", "passiveScore", "activeScore", "hitScore", "playerHitScore"))
            .Append(context.Regression.Trainers.FastTree())
            .Append(context.Transforms.CopyColumns(outputColumnName: "EnemyMaxLifeMultiplier", inputColumnName: "Score"));

            var pipelineForPlayerDamage = context.Transforms.CopyColumns("Label", "playerDamageMultiplier")
            .Append(context.Transforms.Concatenate("Features", "passiveScore", "activeScore", "hitScore", "playerHitScore"))
            .Append(context.Regression.Trainers.FastTree())
            .Append(context.Transforms.CopyColumns(outputColumnName: "PlayerDamageMultiplier", inputColumnName: "Score"));

            try
            {
                var model = pipelineForEnemyDamage
                .Append(pipelineForEnemyDefense)
                .Append(pipelineForEnemyMaxLife)
                .Append(pipelineForPlayerDamage)
                .Fit(test_data);

                context.Model.Save(model, test_data.Schema, "model.zip");

                PredictionEngine<DDAData, DDAPrediction> predictionEngine = context.Model.CreatePredictionEngine<DDAData, DDAPrediction>(model);

                DDAData input = new DDAData
                {
                    PassiveScore = 150f,
                    ActiveScore = 167f,
                    HitScore = 472f,
                    PlayerHitScore = 59f
                };

                DDAPrediction predictions = predictionEngine.Predict(input);

                Console.WriteLine($"Enemy Damage: {predictions.EnemyDamageMultiplier}");
                Console.WriteLine($"Enemy Defense: {predictions.EnemyDefenseMultiplier}");
                Console.WriteLine($"Enemy MaxLife: {predictions.EnemyMaxLifeMultiplier}");
                Console.WriteLine($"Player Damage: {predictions.PlayerDamageMultiplier}");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(test_data.Schema.ToString());
            }

            Console.WriteLine("Finished training.");
            Console.ReadKey();
        }
    }
}
