using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioEvents : MonoBehaviour
{
    [SerializeField] private EventReference _applyPenEvent;
    [SerializeField] private EventReference _applyStampEvent;
    [SerializeField] private EventReference _applyScissorsEvent;
    [SerializeField] private EventReference _errorEvent;
    [SerializeField] private EventReference _openCloseNotebookEvent;
    [SerializeField] private EventReference _startRoundEvent;
    [SerializeField] private EventReference _stopTape;
    [SerializeField] private EventReference _switchOnLamp;
    [SerializeField] private EventReference _tapeLoop;
    private EventInstance _tapeLoopInstance;

    private void PlayApplyPen() 
    {
        EventInstance applyPenInstance = RuntimeManager.CreateInstance(_applyPenEvent);
        applyPenInstance.start();
        applyPenInstance.release();
    }
    
    private void PlayApplyStamp() 
    {
        EventInstance applyStampInstance = RuntimeManager.CreateInstance(_applyStampEvent);
        applyStampInstance.start();
        applyStampInstance.release();
    }

    private void PlayApplyScissors()
    {
        FMOD.Studio.EventInstance applyScissorsInstance = RuntimeManager.CreateInstance(_applyScissorsEvent);
        applyScissorsInstance.start();
        applyScissorsInstance.release();
    }

    private void PlayError()
    {
        FMOD.Studio.EventInstance errorInstance = RuntimeManager.CreateInstance(_errorEvent);
        errorInstance.start();
        errorInstance.release();
    }

    private void PlayOpenCloseNotebook()
    {
        FMOD.Studio.EventInstance openCloseNotebookInstance = RuntimeManager.CreateInstance(_openCloseNotebookEvent);
        openCloseNotebookInstance.start();
        openCloseNotebookInstance.release();
    }

    private void PlayStartRound()
    {
        FMOD.Studio.EventInstance startRoundInstance = RuntimeManager.CreateInstance(_startRoundEvent);
        startRoundInstance.start();
        startRoundInstance.release();
    }

    private void PlaySwitchOnLamp()
    {
        FMOD.Studio.EventInstance switchOnLampInstance = RuntimeManager.CreateInstance(_switchOnLamp);
        switchOnLampInstance.start();
        switchOnLampInstance.release();
    }

    private void PlayStartTape()
    {
        _tapeLoopInstance = RuntimeManager.CreateInstance(_tapeLoop);
        _tapeLoopInstance.start();
    }

    private void PlayStopTape()
    {
        FMOD.Studio.EventInstance stopTapeInstance = RuntimeManager.CreateInstance(_stopTape);
        stopTapeInstance.start();
        stopTapeInstance.release();
        _tapeLoopInstance.release();
    }
}
