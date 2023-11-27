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
    [SerializeField] private AudioClip audioFileClip;
    [SerializeField] private AudioClip staticNoiseClip;
    [SerializeField] private AudioClip hummingNoiseClip;
    [SerializeField] private AudioClip noBootBeepClip;
    [SerializeField] private AudioClip insertFloppyClip;
    [SerializeField] private string password;

    [Header("References")]
    [SerializeField] private GameObject interactable;
    [SerializeField] private TMP_InputField loginInputField;
    [SerializeField] private Animator audioWindowAnimator;


    private bool isOn;
    private bool watchedRecording;
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
    
    public void OnFloppyInserted() {
        State = ComputerState.Startup;

        ambientAudioSource.Stop();
        ambientAudioSource.clip = hummingNoiseClip;
        ambientAudioSource.Play();
        interactableAudioSource.PlayOneShot(insertFloppyClip);
        Event.Global.showDialogue.Raise(MonologueKey.L2_PC_DISK_INSERTED);
    }

    public void SetState(ComputerState computerState) {
        State = computerState;
    }

    public void On() {
        isOn = true;
        
        for (int i = 0; i < screens.Length; i++) {
            screens[i].gameObject.SetActive(i == (int) State);
        }

        if (State == ComputerState.NoBoot) {
            interactableAudioSource.PlayOneShot(noBootBeepClip);
            ambientAudioSource.clip = staticNoiseClip;
            ambientAudioSource.Play();
        }
        else if (State == ComputerState.Startup) {
            StartCoroutine("LoadStartupScreen");
        }
    }

    public void Off() {
        isOn = false;
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
            ambientAudioSource.PlayOneShot(loginSuccessAudioClip);
            Event.Global.showDialogue.Raise(MonologueKey.L2_PC_LOGIN);
            State = ComputerState.Desktop;
        } else {
            loginInputField.text = "";
            ambientAudioSource.PlayOneShot(loginFailAudioClip);
            Event.Global.showDialogue.Raise(MonologueKey.L2_PC_WRONG_PASSWORD);
        }
    }

    
    public void OnOpenAudioFile() {
        audioWindowAnimator.SetTrigger("Click");
        interactableAudioSource.clip = audioFileClip;
        interactableAudioSource.Play();
        if (!watchedRecording) {
            Event.Global.showDialogue.Raise(MonologueKey.L2_PC_AUDIO_INTERACT);
        }
        
        StartCoroutine("CloseAudioFile");
    }

    private IEnumerator CloseAudioFile() {
        yield return interactableAudioSource.isPlaying;
        yield return new WaitForSeconds(audioFileClip.length + 1f);
        yield return !interactableAudioSource.isPlaying;
        Event.L2.finishRecording.Raise();
        audioWindowAnimator.SetTrigger("Close");
        if (!watchedRecording) {
            watchedRecording = true;
            Event.Global.showDialogue.Raise(MonologueKey.L2_AFTER_AUDIO);
        }
    }

    public void OnForceCloseAudioFile() {
        StopCoroutine("CloseAudioFile");
        interactableAudioSource.Stop();
        audioWindowAnimator.SetTrigger("Close");
    }

    private void Start() {
        ambientAudioSource = GetComponent<AudioSource>();
        cameraFocusable = interactable.GetComponent<CameraFocusable>();
        dragDroppable = interactable.GetComponent<DragDoppable>();
        interactableAudioSource = interactable.GetComponent<AudioSource>();
        
        Off();
    }

    /*
    private void Update() {
        // Off computer when unfocusing, on when focusing
        if (!isOn && cameraFocusable.IsCinemachineInStartState()) {
            On();
        } else if (isOn && !cameraFocusable.IsCinemachineInStartState()) {
            Off();
        }

        // Close inventory when computer is turned on
        if (GameState.isInventoryOpened &&
            (State == ComputerState.Desktop || State == ComputerState.Login)) {
                GameState.ToggleInventory(false);
                Event.Global.inventoryUpdate.Raise();
        }
    }
    */

    
}
