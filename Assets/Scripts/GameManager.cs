using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnStateChanged;
    public event Action OnGamePaused;
    public event Action OnGameUnpaused;


    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying, 
        GameOver
    }


    private State _state;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer = 0f;
    private float _gamePlayingTimerMax = 30f;
    private bool _gamePaused = false;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("GameManager instance already exists. Destroying this one...");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _state = State.WaitingToStart;    
    }


    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractInputAction += GameInput_OnInteractInputAction;
    }


    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:

            break;

            case State.CountdownToStart:
            _countdownToStartTimer -= Time.deltaTime;
            if (_countdownToStartTimer < 0f)
            {
                _state = State.GamePlaying;
                _gamePlayingTimer = _gamePlayingTimerMax;
                OnStateChanged?.Invoke();
            }
            break;

            case State.GamePlaying:
            _gamePlayingTimer -= Time.deltaTime;
            if (_gamePlayingTimer < 0f)
            {
                _state = State.GameOver;
                OnStateChanged?.Invoke();
            }
            break;

            case State.GameOver:

            break;
        }        
    }


    public bool IsGamePlaying()
    {
        return _state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return _state == State.CountdownToStart;
    }


    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }


    public float GetCountdownToStartTimer()
    {
        return _countdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (_gamePlayingTimer / _gamePlayingTimerMax);
    }


    private void GameInput_OnPauseAction()
    {
        TogglePauseGame();
    }


    public void TogglePauseGame()
    {
        _gamePaused = !_gamePaused;

        if (_gamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
        }
    }


    private void GameInput_OnInteractInputAction()
    {
        if (_state == State.WaitingToStart)
        {
            _state = State.CountdownToStart;
            OnStateChanged?.Invoke();
        }
    }
}
