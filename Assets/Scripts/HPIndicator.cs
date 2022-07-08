using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIndicator : MonoBehaviour
{
  [SerializeField] private List<GameObject> _hpList = new List<GameObject>();
  [SerializeField] private ParticleSystem _part;
  private int currHealth = 4;
  public bool keke;
  // Start is called before the first frame update
    void Start()
    {
      ResetHP();
    }

    // Update is called once per frame
    void Update()
    {
      if (keke) {
        keke = false;
        LooseHP();
      }
    }
  public void LooseHP() {
    currHealth -= 1;
    _hpList[currHealth].SetActive(false);
    _part.transform.position = Camera.main.ScreenToWorldPoint(_hpList[currHealth].transform.position + new Vector3 (0f, 0f, 40f));
    _part.Play();
  }
  public void ResetHP() {
    currHealth = 4;
    foreach(GameObject i in _hpList)
      i.SetActive(true);
  }
}
