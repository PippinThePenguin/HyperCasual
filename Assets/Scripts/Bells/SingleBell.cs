using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


namespace HyperCasualNamespace {
  public class SingleBell : MonoBehaviour {
    [SerializeField] private Texture _whiteMap, _blackMap, _pinkMap, _redMap, _greenMap, _blueMap;
    [SerializeField] private Material _bellMaterial;
    //public ElGeneriko.Color currCol;
    [SerializeField] private Rigidbody rb;
    private Transform blackEnd, whiteEnd;
    [SerializeField] private Transform anchorPoint;
    private Vector3 positionDef = new Vector3();
    private Transform bellRBObj;
    // Start is called before the first frame update

    void Awake() {
      positionDef = transform.GetChild(0).localPosition;
      bellRBObj = transform.GetChild(0);
    }
    public void Bonk() {
      rb.isKinematic = false;
      CameraShaker.Instance.ShakeOnce(2.7f, 2.7f, .1f, 1f);
      FindObjectOfType<AudioManager>().Play("BellRing");
      FindObjectOfType<BellPart>().PartBonk(transform);
      rb.AddForce(transform.forward * 3000f);
    }
    public void Reset() {
      Rigidbody rb = bellRBObj.GetComponent<Rigidbody>();
      rb.isKinematic = true;
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      bellRBObj.localPosition = positionDef;
      bellRBObj.localRotation = Quaternion.identity;
    }

    public void SetColor(GameColor swithTo) {
      Texture bellText;
      if (swithTo == GameColor.Black) {
        bellText = _blackMap;
      } else if (swithTo == GameColor.Red) {
        bellText = _redMap;
      } else if (swithTo == GameColor.Pink) {
        bellText = _pinkMap;
      } else if (swithTo == GameColor.Green) {
        bellText = _greenMap;
      } else if (swithTo == GameColor.Blue) {
        bellText = _blueMap;
      } else {
        bellText = _whiteMap;
      }
      _bellMaterial.SetTexture("_BaseMap", bellText);
    }
  }
}
