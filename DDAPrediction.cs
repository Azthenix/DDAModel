using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModel
{
    public class DDAPrediction
    {
        [ColumnName("EnemyDamageMultiplier")]
        public float EnemyDamageMultiplier { get; set; }

        [ColumnName("EnemyDefenseMultiplier")]
        public float EnemyDefenseMultiplier { get; set; }

        [ColumnName("EnemyMaxLifeMultiplier")]
        public float EnemyMaxLifeMultiplier { get; set; }

        [ColumnName("PlayerDamageMultiplier")]
        public float PlayerDamageMultiplier { get; set; }
    }
}
