using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ColorFlip : MonoBehaviour
{
    
    
    public Material MaterialBase, MaterialOutline;
    public Material MaterialBaseBack, MaterialOutlineBack;
    public Material partMaterial;
    public Texture blackTexture, whiteTexture;

    private void Start()
    {
        Debug.Log(gameObject.name);
    }
    public void SwapMaterials()
    {
        //Material tempMat = new MaterialBase;
        /*
        Color tempol = MaterialBase.color;
        MaterialBase.color = MaterialOutline.color;
        Camera.main.backgroundColor = MaterialOutline.color;
        MaterialOutline.color = tempol;
        */
        if (MaterialBase.color == Color.black)
            ChangeColors(ElGeneriko.Color.white);
        else
            ChangeColors(ElGeneriko.Color.black);

    }



    public void ChangeColors(ElGeneriko.Color swithTo)
    {
        //FindObjectOfType<AudioManager>().Play("kek");
        //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);

        if (swithTo == ElGeneriko.Color.black)
        {
            MaterialBase.color = Color.black;
            MaterialOutline.color = Color.white;

            MaterialBaseBack.color = Color.black;
            MaterialOutlineBack.color = Color.white;

           partMaterial.SetTexture("_BaseMap", blackTexture);
        }
        else
        {
            MaterialBase.color = Color.white;
            MaterialOutline.color = Color.black;

            MaterialBaseBack.color = Color.white;
            MaterialOutlineBack.color = Color.black;

            partMaterial.SetTexture("_BaseMap", whiteTexture);
        }
    }
}
