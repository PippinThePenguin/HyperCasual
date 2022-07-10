using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class Collidable : MonoBehaviour {
    [SerializeField] protected bool _playerCollided;
    public ObjectStats Stats;
    public ScoreController ScoreController;
    public Mover _selfMover;

    protected virtual void OnEnable() {
      if (ScoreController == null) {
        ScoreController = ScoreController.Controller;
      }      
      if (_selfMover == null) {
        _selfMover = transform.GetComponent<Mover>();
      }    
      _playerCollided = false;
      //ScoreController = GameObject.Find("TESTOBJECT").GetComponent<ScoreController>();
    }    
    private void OnTriggerEnter(Collider collider) {
      ProcessCollider(collider);
    }

    private void ProcessCollider(Collider collider) {
      string tag = collider.tag;
      if (tag == "Disable") {
        gameObject.SetActive(false);
      }
      if (!_playerCollided && tag == "Player") {
        PlayerCollided();
      }
    }
    protected virtual void PlayerCollided() {
      _playerCollided = true;
      ScoreController.ProcessInteraction(Stats);
      //if (_selfMover != null) {
        //_selfMover.Enque();
      //} else {
        //Debug.LogWarning("No Mover Found!! " + transform.name );
      //}
      
      gameObject.SetActive(false);
    }
  }


}
