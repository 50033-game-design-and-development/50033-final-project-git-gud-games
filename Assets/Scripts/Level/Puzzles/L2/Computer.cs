using System;
using System.Collections;
using TMPro;
using UnityEngine;

[Serializable]
public enum ComputerState {
    Off = 0,
    NoBoot = 1,
    Startup = 2,
    Login = 3,
    Desktop = 4
}

public class Computer : MonoBehaviour {

    [SerializeField] private Canvas offCanvas;
    [SerializeField] private Canvas noBootCanvas;
    [SerializeField] private Canvas startupOSCanvas;
    [SerializeField] private Canvas loginCanvas;
    [SerializeField] private Canvas desktopCanvas;
    [SerializeField] private TextMeshProUGUI loginInputField;
    [SerializeField] private string password;

    [SerializeField] private CameraFocusable cameraFocusable;

    private Canvas[] canvases;
    private bool isOn = false;
    private AudioSource audioSource;

    private ComputerState _state;
    private ComputerState State {
        get {
            return _state;
        }
        set {
            _state = value;
            if (isOn) On(); // Update screen if on
        }
    }

    public void OnFusePlugged() => State = ComputerState.NoBoot;
    public void OnFloppyInserted()  => State = ComputerState.Startup;
    public void OnLogin() => State = ComputerState.Desktop;

    public void SetState(ComputerState computerState) {
        State = computerState;
    }

    public void On() {
        isOn = true;
        
        for (int i = 0; i < canvases.Length; i++) {
            canvases[i].gameObject.SetActive(i == (int) State);
        }
        if (State == ComputerState.Startup) 
            StartCoroutine("LoadStartupScreen");
        
        if (State != ComputerState.Off)
            audioSource.Play();
        
    }

    public void Off() {
        isOn = false;
        audioSource.Stop();
        // Set all to off except for the first one
        for (int i = 1; i < canvases.Length; i++) {
            canvases[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator LoadStartupScreen() {
        yield return new WaitForSeconds(2f);
        State = ComputerState.Login;
    }

    public void OnLoginSubmit() {
        if (loginInputField.text.ToLower() == password.ToLower()) {
            Event.L2.LoggedIn.Raise();
        } else {
            loginInputField.text = "";
            // Play audioclip for wrong password
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        canvases = new Canvas[] {
            offCanvas,
            noBootCanvas,
            startupOSCanvas,
            loginCanvas,
            desktopCanvas
        };
        
        Off();
    }

    private void Update() {
        if (!isOn && cameraFocusable.IsCinemachineInStartState()) {
            On();
        } else if (isOn && !cameraFocusable.IsCinemachineInStartState()) {
            Off();
        }
    }

    
}