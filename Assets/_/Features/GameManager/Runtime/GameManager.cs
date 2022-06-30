using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private List<EnemySpawn> _enemySpawns = new List<EnemySpawn>();
    [SerializeField]
    private List<WaveObject> _firstWaveSetUp = new List<WaveObject>();
    [SerializeField]
    private static float _waveModifier;
    [SerializeField]
    private float _breakTimeBetweenWaves;

    #endregion


    #region Unity API

    private void Awake()
    {
        _timers = new float[_firstWaveSetUp.Count];
        _nbSpawned = new int[_firstWaveSetUp.Count];
        InitArrays();
    }
    private void Update()
    {
        if (_isInBreakPhase)
            _breakPhaseTimer += Time.deltaTime;
        else
            _waveTimer += Time.deltaTime;
        IncrementArrayWithTime(_timers);
        HandleEnemySpawn();
        CheckForEndOfWave();
        CheckEndOfBreak();
    }

    #endregion


    #region Main
    private void StartNewWave()
    {
        InitArrays();
        m_waveNumber++;
        _waveTimer = 0;
        _isInBreakPhase = false;
        
    }
    private void CheckEndOfBreak()
    {
        if(_breakPhaseTimer > _breakTimeBetweenWaves)
        {
            StartNewWave();
            
        }
    }
    private void CheckForEndOfWave()
    {
        if (_waveTimer > 15f && _livingEnemies.Count >0)
        {
            StartNewBreakPhase();
        }
    }
    private void StartNewBreakPhase() 
    {
        _breakPhaseTimer = 0;
        _isInBreakPhase = true;
    }
    private void InitArrays()
    {        
        ClearFloatArray(_timers);       
        ClearIntArray(_nbSpawned);
    }
    private void HandleEnemySpawn()
    {
        for (int i = 0; i < _firstWaveSetUp.Count; i++)
        {
            if (_firstWaveSetUp[i].m_enemyType != null && _firstWaveSetUp[i].m_nbSpawn > _nbSpawned[i]*GetWaveModifier(m_waveNumber) && _firstWaveSetUp[i].m_cdSpawn < _timers[i])
            {
               if (GetWaveNumber() > _enemySpawns.Count) _enemySpawns[Random.Range(0, _enemySpawns.Count)].SpawnEnemy(_firstWaveSetUp[i].m_enemyType);
               else _enemySpawns[Random.Range(0, GetWaveNumber())].SpawnEnemy(_firstWaveSetUp[i].m_enemyType);
                _timers[i] = 0;
                _nbSpawned[i]++;
            }
        }
    }

    #endregion


    #region Utils

    private void ClearFloatArray(float[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = 0;
        }
    }
    private void ClearIntArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = 0;
        }
    }
    private void IncrementArrayWithTime(float[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] += Time.deltaTime;
        }
    }
    public static void AddEnemyToList(EnemyBehavior enemy) => _livingEnemies.Add(enemy);  
    public static void RemoveEnemyToList(EnemyBehavior enemy) => _livingEnemies.Remove(enemy);
    private static float GetWaveModifier(int nbwave)
    {
        return nbwave*_waveModifier;
    }
    private static int GetWaveNumber()
    {
        return m_waveNumber;
    }
    #endregion


    #region Private

    private float[] _timers;
    private int[] _nbSpawned;
    public static List<EnemyBehavior> _livingEnemies = new List<EnemyBehavior>();
    [HideInInspector]
    public static int m_waveNumber;
    private float _waveTimer;
    private float _breakPhaseTimer;
    private bool _isInBreakPhase;

    #endregion
}
