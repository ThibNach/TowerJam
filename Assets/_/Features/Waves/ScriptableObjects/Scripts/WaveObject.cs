using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "NewEnemyWave1",menuName ="EnemyTypeWave1")]
    public class WaveObject : ScriptableObject
    {
        public EnemyBehavior m_enemyType;
        public int m_nbSpawn;
        public float m_cdSpawn;
    }
