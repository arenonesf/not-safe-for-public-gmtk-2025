using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Code.Core;

public class AudioEvents : PersistentSingleton<AudioEvents>
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
    [SerializeField] private EventReference _beginRoundMusic;
    [SerializeField] private EventReference _backgroundMusic;
    [SerializeField] private EventReference _gameOverMusic;
    private EventInstance _tapeLoopInstance;
    private EventInstance _musicInstance;

    public void PlayCensorItemSound(CensorItemType type) 
    {
        switch (type)
        {
            case CensorItemType.Pen:
                PlayApplyPen();
                break;
            case CensorItemType.Stamp:
                PlayApplyStamp();
                break;
            case CensorItemType.Scissors:
                PlayApplyScissors();
                break;
            default:
                Debug.LogWarning($"No sound defined for {type}");
                break;
        }
    }

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

    public void PlayError()
    {
        FMOD.Studio.EventInstance errorInstance = RuntimeManager.CreateInstance(_errorEvent);
        errorInstance.start();
        errorInstance.release();
    }

    public void PlayOpenCloseNotebook()
    {
        FMOD.Studio.EventInstance openCloseNotebookInstance = RuntimeManager.CreateInstance(_openCloseNotebookEvent);
        openCloseNotebookInstance.start();
        openCloseNotebookInstance.release();
    }

    public void PlayStartRound()
    {
        FMOD.Studio.EventInstance startRoundInstance = RuntimeManager.CreateInstance(_startRoundEvent);
        startRoundInstance.start();
        startRoundInstance.release();
    }

    public void PlaySwitchOnLamp()
    {
        FMOD.Studio.EventInstance switchOnLampInstance = RuntimeManager.CreateInstance(_switchOnLamp);
        switchOnLampInstance.start();
        switchOnLampInstance.release();
    }

    public void PlayStartTape()
    {
        _tapeLoopInstance = RuntimeManager.CreateInstance(_tapeLoop);
        _tapeLoopInstance.start();
    }

    public void PlayStopTape()
    {
        FMOD.Studio.EventInstance stopTapeInstance = RuntimeManager.CreateInstance(_stopTape);
        stopTapeInstance.start();
        stopTapeInstance.release();
        _tapeLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _tapeLoopInstance.release();
    }

    public void PlayBeginRoundMusic() 
    {
        _musicInstance = RuntimeManager.CreateInstance(_beginRoundMusic);
        _musicInstance.start();
    }

    public void PlayBackgroundMusic() 
    {
        _musicInstance = RuntimeManager.CreateInstance(_backgroundMusic);
        _musicInstance.start();
    }

    public void PlayGameOverMusic() 
    {
        _musicInstance = RuntimeManager.CreateInstance(_gameOverMusic);
        _musicInstance.start();
    }

    public void StopMusic() 
    {
        _musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _musicInstance.release();
    }

    public void StopAllAudioEvents() 
    {
        _tapeLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _tapeLoopInstance.release();
        StopMusic();        
    }
}
