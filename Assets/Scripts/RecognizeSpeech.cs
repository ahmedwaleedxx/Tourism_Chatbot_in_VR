using UnityEngine;
using UnityEngine.UI;
using Oculus.Voice;
using Meta.WitAi.Json;
using Meta.WitAi;
using TMPro;

public class RecognizeSpeech : MonoBehaviour
{
    [Header("Default States"), Multiline]
    [SerializeField] private string freshStateText = "Press on the button to start";

    [Header("UI")]
    [SerializeField] private TMP_Text textArea;
    [SerializeField] private bool showJson;

    [Header("Voice")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;

    // Whether voice is activated
    public bool IsActive => _active;
    private bool _active = false;

    private void OnEnable()
    {
        textArea.text = freshStateText;
        appVoiceExperience.VoiceEvents.OnRequestCreated.AddListener(OnRequestStarted);
        appVoiceExperience.VoiceEvents.OnPartialTranscription.AddListener(OnRequestTranscript);
        appVoiceExperience.VoiceEvents.OnFullTranscription.AddListener(OnRequestTranscript);
        appVoiceExperience.VoiceEvents.OnStartListening.AddListener(OnListenStart);
        appVoiceExperience.VoiceEvents.OnStoppedListening.AddListener(OnListenStop);
        appVoiceExperience.VoiceEvents.OnStoppedListeningDueToDeactivation.AddListener(OnListenForcedStop);
        appVoiceExperience.VoiceEvents.OnStoppedListeningDueToInactivity.AddListener(OnListenForcedStop);
        appVoiceExperience.VoiceEvents.OnResponse.AddListener(OnRequestResponse);
        appVoiceExperience.VoiceEvents.OnError.AddListener(OnRequestError);
    }
    // Remove delegates
    private void OnDisable()
    {
        appVoiceExperience.VoiceEvents.OnRequestCreated.RemoveListener(OnRequestStarted);
        appVoiceExperience.VoiceEvents.OnPartialTranscription.RemoveListener(OnRequestTranscript);
        appVoiceExperience.VoiceEvents.OnFullTranscription.RemoveListener(OnRequestTranscript);
        appVoiceExperience.VoiceEvents.OnStartListening.RemoveListener(OnListenStart);
        appVoiceExperience.VoiceEvents.OnStoppedListening.RemoveListener(OnListenStop);
        appVoiceExperience.VoiceEvents.OnStoppedListeningDueToDeactivation.RemoveListener(OnListenForcedStop);
        appVoiceExperience.VoiceEvents.OnStoppedListeningDueToInactivity.RemoveListener(OnListenForcedStop);
        appVoiceExperience.VoiceEvents.OnResponse.RemoveListener(OnRequestResponse);
        appVoiceExperience.VoiceEvents.OnError.RemoveListener(OnRequestError);
    }

    // Request began
    private void OnRequestStarted(WitRequest r)
    {
        // Store json on completion
        if (showJson) r.onRawResponse = (response) => textArea.text = response;
        // Begin
        _active = true;
    }
    // Request transcript
    private void OnRequestTranscript(string transcript)
    {
        textArea.text = transcript;
    }
    // Listen start
    private void OnListenStart()
    {
        textArea.text = "Listening...";
    }
    // Listen stop
    private void OnListenStop()
    {
        textArea.text = "Processing...";
    }
    // Listen stop
    private void OnListenForcedStop()
    {
        if (!showJson)
        {
            textArea.text = freshStateText;
        }
        OnRequestComplete();
    }
    // Request response
    private void OnRequestResponse(WitResponseNode response)
    {
        if (!showJson)
        {
            if (!string.IsNullOrEmpty(response["text"]))
            {
                textArea.text = response["text"];

            }
            else
            {
                textArea.text = freshStateText;
            }
        }
        OnRequestComplete();
    }
    // Request error
    private void OnRequestError(string error, string message)
    {
        if (!showJson)
        {
            textArea.text = $"<color=\"red\">Error: {error}\n\n{message}</color>";
        }
        OnRequestComplete();
    }
    // Deactivate
    private void OnRequestComplete()
    {
        _active = false;
    }

    // Toggle activation
    public void ToggleActivation()
    {
        SetActivation(!_active);
    }
    // Set activation
    public void SetActivation(bool toActivated)
    {
        if (_active != toActivated)
        {
            _active = toActivated;
            if (_active)
            {
                appVoiceExperience.Activate();
            }
            else
            {
                appVoiceExperience.Deactivate();
            }
        }
    }

}
