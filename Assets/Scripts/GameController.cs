using OVR.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public enum GameState { WALKAROUND, BATTLE}
public class GameController : MonoBehaviour
{
    public CameraController CameraController;
    public DogController DogController;
    public AssemblyDefinitionAsset OVRPlugin;

    public bool dogActive = true;


    public GameState state { get; private set; } = GameState.WALKAROUND;
    private GameState nextState = GameState.WALKAROUND;

    private Event gamemodeChanged;
    [SerializeField]
    private DialogueRunner dialogueRunner;

    void Awake()
    {
        InitializeServices();
        gamemodeChanged = Services.EventSystem.AddEvent("GamemodeChanged");
    }

    void InitializeServices()
    {
        Services.GameController = this;
        Services.EventSystem = new EventSystem();
        Services.DialogueRunner = dialogueRunner;
        Services.ScentManager = new ScentManager(OVRPlugin);
        /*
        Services.Prefabs = GameObject.FindObjectOfType<Prefabs>();
        Services.YogaClass = new YogaClass();
        Services.DataLoader = new DataLoader();
        */
    }

    public void SetTheme(OdorAsset scent)
    {
        
    }

    public void SwitchActiveCharacter()
    {
        
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

        Services.ScentManager.Update();
    }
}
