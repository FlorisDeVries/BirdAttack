using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }

    [HideInInspector]
    public FloatEvent OnHit = new FloatEvent();
    [HideInInspector]
    public UnityEvent OnGameOver = new UnityEvent();
    private bool _gameOver = false;
    public bool GameOver
    {
        get { return _gameOver; }
    }

    [SerializeField]
    private float _lives = 10f;
    public float Lives
    {
        get { return _lives; }
    }

    [SerializeField]
    private bool _cheats = true;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        OnHit.AddListener(Hit);

        OnGameOver.AddListener(() => _gameOver = true);
    }

    public void Hit(float livesLost)
    {
        _lives -= livesLost;
        CheckGameOver();
    }

    private bool CheckGameOver()
    {
        if (_lives <= 0)
        {
            // Gameover
            OnGameOver.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_cheats)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _lives++;
            OnHit.Invoke(0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
            OnHit.Invoke(1);

        if (Input.GetKeyDown(KeyCode.E))
            OnHit.Invoke(_lives);
    }
}