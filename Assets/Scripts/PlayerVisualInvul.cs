using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualInvul : MonoBehaviour
{
  public List<MeshRenderer> _renderList = new List<MeshRenderer>();

  private IEnumerator BecomeTemporarilyInvincible() {
    bool i = true;
    while (true) {
      yield return new WaitForSeconds(.05f);
      if (i) {
        MehsEnable(false);
      } 
      else {
        MehsEnable(true);
      }
      i = !i;
    }
  }

  public void Invincible(bool isInvincible) {
    if (isInvincible)
      StartCoroutine("BecomeTemporarilyInvincible");
    else {
      StopAllCoroutines();
      MehsEnable(true);
    }
  }


  void MehsEnable(bool isEnable) {
    foreach (MeshRenderer i in _renderList) {
      i.enabled = isEnable;
    }
  }
}
