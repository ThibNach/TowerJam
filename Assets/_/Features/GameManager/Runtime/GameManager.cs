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
    [Space(5)]

    [Header("Wave Values")]
    [SerializeField]
    private float _breakTimeBetweenWaves;
    [SerializeField]
    private float _waveModifier;
    public float m_enemyHPAddByWave;
    

    #endregion


    #region Unity API

    private void Awake()
    {
        m_waveNumber = 1;
        _timers = new float[_firstWaveSetUp.Count];
        _nbSpawned = new int[_firstWaveSetUp.Count];
        InitArrays();
    }
    private void Update()
    {
        if (_isInBreakPhase)
        {
            _breakPhaseTimer += Time.deltaTime;
            CheckEndOfBreak();
        }

        else
        {
            _waveTimer += Time.deltaTime;
            IncrementArrayWithTime(_timers);
            HandleEnemySpawn();
            CheckForEndOfWave();
        }         
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
        if (_waveTimer > 15f && _livingEnemies.Count == 0)
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
            if (_firstWaveSetUp[i].m_enemyType != null && _firstWaveSetUp[i].m_nbSpawn*GetWaveModifier() > _nbSpawned[i] && _firstWaveSetUp[i].m_cdSpawn < _timers[i])
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
    public void AddEnemyToList(EnemyBehavior enemy) => _livingEnemies.Add(enemy);  
    public void RemoveEnemyToList(EnemyBehavior enemy) => _livingEnemies.Remove(enemy);
    public float GetWaveModifier()
    {
        return m_waveNumber * _waveModifier;
    }
    private  int GetWaveNumber()
    {
        return m_waveNumber;
    }
    #endregion


    #region Private

    private float[] _timers;
    private int[] _nbSpawned;
    public List<EnemyBehavior> _livingEnemies = new List<EnemyBehavior>();
    [HideInInspector]
    public  int m_waveNumber;
    private float _waveTimer;
    private float _breakPhaseTimer;
    private bool _isInBreakPhase;

    #endregion
}
