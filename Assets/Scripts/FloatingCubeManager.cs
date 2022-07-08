using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatingCube
{
    public Rigidbody selfRB;
    public GameObject selfGO;
    public GameObject mainRenderer;
    public ParticleSystem mainSplash;

    //public GameObject BlackRender, WhiteRender;
    //public ParticleSystem blackSplash, whiteSplash;
    public float cubeScale;
    public Transform initPoint;

    public void Init(GameObject selfBody, float cubeScaleI)
    {
        selfGO = selfBody;
        selfRB = selfGO.transform.GetComponent<Rigidbody>();
        cubeScale = cubeScaleI;
        selfGO.transform.localScale = Vector3.one * 0.2f * cubeScale;
        mainRenderer = selfGO.transform.GetChild(0).gameObject;

    }
    public void SpawnCube(ElGeneriko.Color myColor)
    {
        /*
        if (myColor == ElGeneriko.Color.black)
        {
            blackSplash.Play();
            WhiteRender.SetActive(false);
        }
        else
        {
            whiteSplash.Play();
            BlackRender.SetActive(false);
        }
        */
    }

    
    public void EnableRenderer()//ElGeneriko.Color myColor)
    {
        mainRenderer.SetActive(true);
        /*
        if (myColor == ElGeneriko.Color.black)
            BlackRender.SetActive(true);
        else
            WhiteRender.SetActive(true);
            */
    }

    public void DisableRenderer()
    {
        mainRenderer.SetActive(false);
        //BlackRender.SetActive(false);
        //WhiteRender.SetActive(false);
    }
}

[System.Serializable]
public class initianPoints
{
    public Transform cubeCenter;
    public List<Transform> initPointsAvalible = new List<Transform>();


    public void SetCubeOnPoints(FloatingCube newCube)
    {
        Transform newPoint = initPointsAvalible[Random.Range(0, initPointsAvalible.Count)];
        newCube.initPoint = newPoint;
        ConstantForce cf = newCube.selfGO.AddComponent(typeof(ConstantForce)) as ConstantForce;
        cf.force = addConstanForce(newPoint)*15f;
        initPointsAvalible.Remove(newPoint);
        newCube.selfGO.transform.position = newPoint.position;// * .5f;
    }


    Vector3 addConstanForce(Transform pointToSpawn)
    {
        Vector3 posA = pointToSpawn.position;
        Vector3 posB = cubeCenter.position;
        Vector3 dir = (posA - posB).normalized;
        return dir;
    }

    public void cubeDeleted(FloatingCube cube)
    {
        if (cube.initPoint != null)
        {
            initPointsAvalible.Add(cube.initPoint);
        }
    }
}

public class FloatingCubeManager : MonoBehaviour
{
    [SerializeField] private SingleColorManager blackManager, whiteManager;
    private SingleColorManager currColorManager;
    private ElGeneriko.Color currColor;
    public int currentCubes;

    public void InitiManager(ElGeneriko.Color swithTo)
    {
        currColor = swithTo;
        currColorManager = swithTo == ElGeneriko.Color.white ? whiteManager : blackManager;
        currentCubes = currColorManager.currCubes;
    }

    public void SwapColors(ElGeneriko.Color swithTo)
    {
        if (currColor != swithTo)
        {
            currColor = swithTo;
            currColorManager.DisableCubesVisuals();
            currColorManager = currColorManager == blackManager ? whiteManager : blackManager;
            currColorManager.EnableCubesVisual();
            currentCubes = currColorManager.currCubes;
        }
    }

    public void ChangeCubesNumber(ElGeneriko.Cubes changes)
    {
        int changesNumber = changes.number - 1;
        currentCubes = currColorManager.currCubes;
        if (changesNumber > currentCubes)
        {
            currColorManager.AddCubes(changesNumber - currentCubes);
        }
        else
        {
            currColorManager.RemoveCubes(currentCubes - changesNumber);
        }
        currentCubes = changesNumber;
    }

}

