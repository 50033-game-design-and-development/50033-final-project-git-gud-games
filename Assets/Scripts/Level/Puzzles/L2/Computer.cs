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

    [Header("Screens")]
    [SerializeField] private Canvas[] screens;

    [Header("Settings")]
    [SerializeField] private AudioClip loginSuccessAudioClip;
    [SerializeField] private AudioClip loginFailAudioClip;
    [SerializeField] private AudioClip AudioFileClip;
    [SerializeField] private string password;

    [Header("References")]
    [SerializeField] private GameObject interactable;
    [SerializeField] private TMP_InputField loginInputField;
    [SerializeField] private Animator audioWindowAnimator;


    private bool isOn = false;
    private AudioSource ambientAudioSource;
    private AudioSource interactableAudioSource;
    
    private CameraFocusable cameraFocusable;
    private DragDoppable dragDroppable;

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

    public void OnFusePlugged() {
        State = ComputerState.NoBoot;
        dragDroppable.possibleDroppable.Clear();
        dragDroppable.possibleDroppable.Add(InventoryItem.L2_Floppy);
        dragDroppable.UpdateDroppables();
    }
    public void OnFloppyInserted()  => State = ComputerState.Startup;
    public void OnLogin() => State = ComputerState.Desktop;

    public void SetState(ComputerState computerState) {
        State = computerState;
    }

    public void On() {
        isOn = true;
        
        for (int i = 0; i < screens.Length; i++) {
            screens[i].gameObject.SetActive(i == (int) State);
        }
        if (State == ComputerState.Startup) 
            StartCoroutine("LoadStartupScreen");
        
        if (State != ComputerState.Off)
            ambientAudioSource.Play();
        
    }

    public void Off() {
        isOn = false;
        ambientAudioSource.Stop();
        interactableAudioSource.Stop();
        // Set all to off except for the first one
        for (int i = 1; i < screens.Length; i++) {
            screens[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator LoadStartupScreen() {
        yield return new WaitForSeconds(2f);
        State = ComputerState.Login;
    }

    public void OnLoginSubmit() {
    
        string input = loginInputField.text;

        // Trim zero width space characters
        if (input.Trim((char)8203).Equals(password.Trim(), StringComparison.OrdinalIgnoreCase)) {
            Event.L2.loggedIn.Raise();
            ambientAudioSource.PlayOneShot(loginSuccessAudioClip);
        } else {
            loginInputField.text = "";
            ambientAudioSource.PlayOneShot(loginFailAudioClip);
        }
    }

    private void Start() {
        ambientAudioSource = GetComponent<AudioSource>();
        cameraFocusable = interactable.GetComponent<CameraFocusable>();
        dragDroppable = interactable.GetComponent<DragDoppable>();
        interactableAudioSource = interactable.GetComponent<AudioSource>();
        
        Off();
    }

    private void Update() {
        // Off computer when unfocusing, on when focusing
        if (!isOn && cameraFocusable.IsCinemachineInStartState()) {
            On();
        } else if (isOn && !cameraFocusable.IsCinemachineInStartState()) {
            Off();
        }

        // Close inventory when computer is turned on
        if (isOn && GameState.isInventoryOpened && 
            (State == ComputerState.Desktop || State == ComputerState.Login)) {
                GameState.ToggleInventory(false);
                Event.Global.inventoryUpdate.Raise();
        }
    }

    public void OnOpenAudioFile() {
        interactableAudioSource.clip = AudioFileClip;
        interactableAudioSource.Play();
        StartCoroutine("CloseAudioFile");
    }

    private IEnumerator CloseAudioFile() {
        yield return interactableAudioSource.isPlaying;
        yield return new WaitForSeconds(AudioFileClip.length + 1f);
        yield return !interactableAudioSource.isPlaying;
        audioWindowAnimator.SetTrigger("Close");
    }

    
}