using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState
    {
        lobby,
        run,
        over
    }

    private GameState _state;
    public GameState State
    {
        get => _state;
        set
        {
            _state = value;
            OnStageChange?.Invoke(value);
        }
    }
    public event Action<GameState> OnStageChange;

    private int _score;
    private int Score 
    { 
        get => _score; 
        set 
        { 
            _score = value; 
            OnScoreUpdate?.Invoke(value); 
        } 
    }

    public event Action<int> OnScoreUpdate; 

    private static GameController _instance;
    [SerializeField] private PigController _pigController;

    public static GameController Instance { get => _instance; }

    public void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SubscribeEvent();
        State = GameState.run;

        OnStageChange += state =>
        {
            if (state == GameState.run)
            {
                _pigController.Init();
                Score = 0;
            }
        };
    }

    private void InscreaseScore()
    {
        Score += 5;
    }

    private void SubscribeEvent()
    {
        _pigController.OnScore += InscreaseScore;
    }
}
