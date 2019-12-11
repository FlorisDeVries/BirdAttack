using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WaveSpawner : AwakeSingleton<WaveSpawner>
{
    [SerializeField]
    private GameObject _particleSystem;

    [SerializeField]
    private GameObject _baseEnemy;

    [SerializeField]
    private GameObject _bossEnemy;
    public float spawnRate = 5f;
    private float _spawnTimer = 0f;
    [SerializeField]
    private float _spawnInterval = 1f;
    private bool _spawning = false;
    private bool _spawningOverride = false;

    // Consumption elements
    [SerializeField]
    private float _consumeInterval = 1f;
    private float _consumeSegments = 4f;
    private bool _consuming = false;

    public float waves = 5f;
    private float _waveCounter = 0;
    public float WaveCounter
    {
        get { return _waveCounter; }
    }

    public static UnityEvent OnConsume = new UnityEvent();

    [SerializeField]
    private Text _waveText;

    // Update is called once per frame
    void Update()
    {
        if (_waveCounter < waves)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer > spawnRate && !_spawning)
            {
                SpawnWave();
            }
            if (_spawnTimer > spawnRate - (_consumeSegments - 1 * _consumeInterval) && !_consuming && (!_spawning || _spawningOverride))
            {
                _consuming = true;
                StartCoroutine(ConsumeFood());
            }
        }
        else
        {
            if (!_spawning)
            {
                _spawning = true;
                Instantiate(_bossEnemy, transform.position, transform.rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
            Instantiate(_baseEnemy, transform.position, transform.rotation);
    }

    private void SpawnWave()
    {
        _waveCounter++;
        _waveText.text = _waveCounter.ToString();
        _spawning = true;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator ConsumeFood()
    {
        for (int i = 0; i < _consumeSegments; i++)
        {
            OnConsume.Invoke();
            SpawnParticles();
            yield return new WaitForSeconds(_consumeInterval);
        }
        _consuming = false;
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < _waveCounter + 1; i++)
        {
            Enemy enemy = Instantiate(_baseEnemy, transform.position, transform.rotation).GetComponent<Enemy>();
            enemy.SetHP(enemy.MaxHP * ((int)(_waveCounter / 10) + 1));
            enemy.Reward *= ((int)(_waveCounter / 10) + 1);
            yield return new WaitForSeconds(_spawnInterval);
        }
        yield return new WaitForSeconds(_consumeInterval);
        _spawnTimer = 0;
        OnConsume.Invoke();
        StartCoroutine(ResetSpawning());
    }

    IEnumerator ResetSpawning()
    {
        yield return new WaitForSeconds(_consumeInterval);
        _spawningOverride = true;
        yield return new WaitForSeconds(_consumeInterval * (_consumeSegments - 1));
        _spawning = false;
        _spawningOverride = false;
    }

    private void SpawnParticles()
    {
        Instantiate(_particleSystem, transform.position, transform.rotation);
    }
}