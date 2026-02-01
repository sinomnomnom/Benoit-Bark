using OVR.Components;
using OVR.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public enum GameState { WALKAROUND, BATTLE}
public class GameController : MonoBehaviour
{
    public Material BackgroundMaterial;
    public GameObject dog;
    public GameObject detective;
    public GameObject Camera;
    public CameraController CameraController;
    public DogController DogController;
    public DetectiveController DetectiveController;
    public AssemblyDefinitionAsset OVRPlugin;
    public ScentDatabase ScentDatabase;

    public bool dogActive = true;


    public GameState state { get; private set; } = GameState.WALKAROUND;
    private GameState nextState = GameState.WALKAROUND;

    private Event gamemodeChanged;
    [SerializeField]
    private DialogueRunner dialogueRunner;
    public OlfactoryEpithelium olfactoryEpithelium;

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
        Services.ScentManager = new ScentManager(OVRPlugin, olfactoryEpithelium);
        Services.ScentDatabase = ScentDatabase;
        /*
        Services.Prefabs = GameObject.FindObjectOfType<Prefabs>();
        Services.YogaClass = new YogaClass();
        Services.DataLoader = new DataLoader();
        */
    }

    public void SetTheme(ScentDatabase.Scents scent)
    {
        //scent
        Services.ScentManager.SetActiveScent(scent);

        //bg
    }

    public bool switchingCam = false;
    public async void SwitchActiveCharacter()
    {
        if (switchingCam) { return; }
        switchingCam = true;
       
        if (dogActive)
        {
            dogActive = false;
            CameraController.SpinCameraAround(dog.transform.position);
            await Task.Delay(3500);
            DogController.active = false;
            DetectiveController.active = true;
            Vector3 temp = dog.transform.position;
            dog.transform.position = detective.transform.position;
            detective.transform.position = temp;
        }
        else
        {
            dogActive = true;
            CameraController.SpinCameraAround(detective.transform.position);
            await Task.Delay(3500);
            DogController.active = true;
            DetectiveController.active = false;
            Vector3 temp = dog.transform.position;
            dog.transform.position = detective.transform.position;
            detective.transform.position = temp;
        }
        switchingCam = false;
    }


    public void SwitchGameState(GameState newState)
    {
        state = newState;
        Services.EventSystem.TriggerEvent(gamemodeChanged);
        print("game state now: " + newState);
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
