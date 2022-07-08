using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class GameStarter : MonoBehaviour {
    [SerializeField] private List<GameObject> _toActivate = new List<GameObject>();
    [SerializeField] private Animation _anim;
    [SerializeField] private SingleBell _startBell;

    private AudioSource[] allAudioSources;
    
    private void Start() {
      allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
      foreach (AudioSource audioS in allAudioSources)
        audioS.Stop();

      Time.timeScale = 0f;
      _startBell.SetColor(GameColor.Black);
      foreach (GameObject i in _toActivate)
        i.SetActive(false);
    }
    
    public void StartGame() {
      Time.timeScale = 1f;
      FindObjectOfType<AudioManager>().Play("MainTheme");
      _anim.enabled = true;
      foreach (GameObject i in _toActivate)
        i.SetActive(true);
      StartCoroutine("StartBell");
    }

    IEnumerator StartBell() {
      yield return new WaitForFixedUpdate();
      _startBell.Bonk();
      Destroy(transform.gameObject);
    }
  }
}
