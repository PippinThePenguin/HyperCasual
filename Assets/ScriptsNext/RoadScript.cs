using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HyperCasualNamespace {
  public class RoadScript : MonoBehaviour {
    private List<float> _trackPositions;
    [SerializeField] private Transform _road;
    private float _roadXScale;
    // Start is called before the first frame update
    void Start() {
      CheckTracks();
    }

    // Update is called once per frame
    void Update() {

    }

    public void CheckTracks() {
      if (_trackPositions == null) {
        Debug.Log("need set");
      }
      SetTracks();
    }

    private void SetTracks() {
      _roadXScale = _road.localScale.x;
      _trackPositions = new List<float>();
      
      for (float n = -(_roadXScale/2); n <= (_roadXScale/2); n += (_roadXScale/6)) {
        _trackPositions.Add(n);
        Debug.Log(n);
      }
    }
  }
}

