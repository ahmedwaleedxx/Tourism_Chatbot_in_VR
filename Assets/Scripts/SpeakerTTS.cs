using Meta.WitAi.TTS.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SpeakerTTS : MonoBehaviour
{
    [SerializeField] private string _title = "CHARLIE";
    [SerializeField] private TMP_Text _input;
    [SerializeField] private TTSSpeaker _speaker;
   // [SerializeField] private Button _stopButton;
    [SerializeField] private Button _speakButton;
    [SerializeField] private Button _speakQueuedButton;
    [SerializeField] private Button _IPAddresstb;

    [SerializeField] private string _dateId = "[DATE]";
    [SerializeField] private string[] _queuedText;

    // States
    private bool _loading;
    private bool _speaking;

    // Add delegates
    private void OnEnable()
    {
        //RefreshButtons();
       // _stopButton.onClick.AddListener(StopClick);
        _speakButton.onClick.AddListener(SpeakClick);
        //_speakQueuedButton.gameObject.SetActive(true);
        //_speakQueuedButton.onClick.AddListener(SpeakQueuedClick);
        //SendMessage send = new SendMessage();
        //send.connect();
        //_IPAddresstb.gameObject.SetActive(true);
        
    }
    // Stop click
    //private void StopClick() => _speaker.Stop();
    // Speak phrase click
    private void SpeakClick() => _speaker.Speak(FormatText(_input.text));
    // Speak queued phrase click
    private void SpeakQueuedClick()
    {
        //_speakQueuedButton.gameObject.SetActive(false);

        foreach (var text in _queuedText)
        {
            _speaker.SpeakQueued(FormatText(text));
        }
        _speaker.SpeakQueued(FormatText(_input.text));
    }
    // Format text with current datetime
    private string FormatText(string text)
    {
        DateTime now = DateTime.Now;
        string dateString = $"{now.ToLongDateString()} at {now.ToShortTimeString()}";
        return text.Replace(_dateId, dateString);
    }
    // Remove delegates
    private void OnDisable()
    {
       // _stopButton.onClick.RemoveListener(StopClick);
        _speakButton.onClick.RemoveListener(SpeakClick);
        _speakQueuedButton.onClick.RemoveListener(SpeakQueuedClick);
    }

    // Preset text fields
    private void Update()
    {
        // On preset voice id update
        if (!string.Equals(_title, _speaker.presetVoiceID))
        {
            _title = _speaker.presetVoiceID;
            _input.text = GetComponent<Text>().text = $"Write something to say in {_speaker.presetVoiceID}'s voice";
        }
        // On state changes
        if (_loading != _speaker.IsLoading)
        {
            _loading = _speaker.IsLoading;
            //RefreshButtons();
        }
        if (_speaking != _speaker.IsSpeaking)
        {
            _speaking = _speaker.IsSpeaking;
           // RefreshButtons();
        }
    }
    // Refresh interactable based on states
    //private void RefreshButtons()
    //{
    //    _stopButton.interactable = _loading || _speaking;
    //}

}
