using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class MuteAudio : MonoBehaviour {
    private bool isMuted;
    [SerializeField] private GameObject _sound, _noSound;

    private void Start() {
      SetAudio(!ProfileScriptable.CurrentProfile.IsMuted);
      isMuted = ProfileScriptable.CurrentProfile.IsMuted;
    }
    public void ToggleAudio() {
      isMuted = !isMuted;
      ProfileScriptable.CurrentProfile.IsMuted = isMuted;
      AudioListener.volume = isMuted ? 0f : 1f;
      _sound.SetActive(!isMuted);
      _noSound.SetActive(isMuted);
    }

    public void SetAudio(bool audio) {
      AudioListener.volume = audio ? 1f : 0f;
      _sound.SetActive(audio);
      _noSound.SetActive(!audio);
    }
  }
}
