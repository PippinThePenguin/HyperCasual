using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellPart : MonoBehaviour
{
    [SerializeField] private ParticleSystem system1;

    public void PartBonk(Transform bellTrans)
    {
        system1.transform.parent.position = bellTrans.position;
        system1.Play();
    }
}
