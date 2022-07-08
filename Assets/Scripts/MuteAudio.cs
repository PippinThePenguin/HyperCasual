using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class MuteAudio : MonoBehaviour {
    private bool isMuted;
    [SerializeField] GameObject _muieUI;

    private void Start() {
      SetAudio(!ProfileScriptable.CurrentProfile.IsMuted);
      isMuted = ProfileScriptable.CurrentProfile.IsMuted;
    }
    public void ToggleAudio() {
      isMuted = !isMuted;
      ProfileScriptable.CurrentProfile.IsMuted = isMuted;
      AudioListener.volume = isMuted ? 0f : 1f;
      _muieUI.SetActive(isMuted);
    }

    public void SetAudio(bool audio) {
      AudioListener.volume = audio ? 1f : 0f;
      _muieUI.SetActive(!audio);
    }
  }
}
