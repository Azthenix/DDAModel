using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace TestModel
{
    public class DDAData
    {
        [LoadColumn(0)]
        [ColumnName("passiveScore")]
        public float PassiveScore { get; set; }

        [LoadColumn(1)]
        [ColumnName("activeScore")]
        public float ActiveScore { get; set; }

        [LoadColumn(2)]
        [ColumnName("hitScore")]
        public float HitScore { get; set; }

        [LoadColumn(3)]
        [ColumnName("playerHitScore")]
        public float PlayerHitScore { get; set; }


        [LoadColumn(4)]
        [ColumnName("enemyDamageMultiplier")]
        public float EnemyDamageMultiplier { get; set; }

        [LoadColumn(5)]
        [ColumnName("enemyDefenseMultiplier")]
        public float EnemyDefenseMultiplier { get; set; }

        [LoadColumn(6)]
        [ColumnName("enemyMaxLifeMultiplier")]
        public float EnemyMaxLifeMultiplier { get; set; }

        [LoadColumn(7)]
        [ColumnName("playerDamageMultiplier")]
        public float PlayerDamageMultiplier { get; set; }
    }
}
