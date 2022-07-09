using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
  public Slider Slider;
  // Start is called before the first frame update
   void Start()
   {
    LoadLvL();
   }
  void LoadLvL() {
    StartCoroutine(AsynchronousLoad("EgorTestBuild"));
  }
  IEnumerator AsynchronousLoad(string scene) {
    yield return null;
    AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
    while (!ao.isDone) {
      float progress = Mathf.Clamp01(ao.progress / 0.9f);
      // Loading completed
      Slider.value = progress;
      /*
      if (ao.progress == 0.9f) {
        Debug.Log("Press a key to start");
        if (Input.AnyKey())
          ao.allowSceneActivation = true;
      } */
      yield return null;
    }
  }
}
