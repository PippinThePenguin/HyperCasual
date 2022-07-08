using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class Mover : MonoBehaviour {
    public float MoveSpeed = 2f;
    public string Tag;
    public Vector3 Speed = new Vector3(0, 0, 1);
    private bool _inited = false;

    private void Start() {
      SpeedController.MainSpeedController.OnSpeedChange.AddListener(SpeedUpdate);
      MoveSpeed = SpeedController.MainSpeedController.CurrentSpeed;
      _inited = true;
    }

    void FixedUpdate() {
      transform.localPosition = transform.localPosition - Speed.normalized*MoveSpeed;
      
      if (transform.position.z < -180f) {        
        gameObject.SetActive(false);
      }
    }

    private void SpeedUpdate(float newSpeed) {
      MoveSpeed = newSpeed;
    }

    
    

    private void OnDisable() {
      Enque();
    }

    public void Enque() {
      if (Tag != null) {
        Pools.ComponentPool.EnqueBlock(Tag, gameObject);
      } else {
        Debug.LogWarning("Tag is null in " + transform.name);
      }
    }
  }
}
