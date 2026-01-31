using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { WALKAROUND, BATTLE}
public class GameController : MonoBehaviour
{
    public GameState state { get; private set; } = GameState.WALKAROUND;
    private GameState nextState = GameState.WALKAROUND;

    private Event gamemodeChanged;

    void Awake()
    {
        InitializeServices();
        gamemodeChanged = Services.EventSystem.AddEvent("GamemodeChanged");
    }

    void InitializeServices()
    {
        Services.GameController = this;
        Services.EventSystem = new EventSystem();
        /*
        Services.Prefabs = GameObject.FindObjectOfType<Prefabs>();
        
        Services.YogaClass = new YogaClass();
        Services.DataLoader = new DataLoader();
        */
    }

    public void SwitchGameState(GameState newState)
    {
        state = newState;
        Services.EventSystem.TriggerEvent(gamemodeChanged);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        switch (state)
        {
            case GameState.WALKAROUND:

                break;
            case GameState.BATTLE:

                break;
        }
    }
}
