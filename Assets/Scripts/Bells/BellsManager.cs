using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> bellList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBell(GameObject bell)
    {
        Rigidbody rb = bell.transform.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        bell.transform.localPosition = Vector3.zero;
        bell.transform.localRotation = Quaternion.identity;
    }

    public void ResetBells()
    {
        foreach (GameObject bell in bellList)
        {
            ResetBell(bell);
        }
    }
}
