using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleColorManager : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private int maxCubes;
    [SerializeField] private GameObject cubePref;       
    [SerializeField] private initianPoints points;
    public int currCubes, pointCounter;      
    private List<FloatingCube> cubeList = new List<FloatingCube>();
    // Start is called before the first frame update

    void Awake()
    {
        pointCounter = points.initPointsAvalible.Count;
    }


    public void AddCubes(int howMany)
    {
        if (currCubes + howMany > maxCubes)
            howMany = maxCubes - currCubes;
        for (int i = 0; i < howMany; i++)
        {
            AddOneCube();
        }
    }

    void AddOneCube()
    {
        Vector3 whereToInst = Vector3.zero;
        GameObject newCube = Instantiate(cubePref, whereToInst, Quaternion.identity);
        FloatingCube cube = new FloatingCube();

        float cubeScale = Mathf.Clamp(maxCubes / (maxCubes + /*12f*/ 3f * (Mathf.Floor(currCubes / 5f))), .1f, 1f);
        currCubes += 1;
        cube.Init(newCube, cubeScale);

        LinkCubes(cube);
        cubeList.Add(cube);
    }

    public void RemoveCubes(int howMany)
    {
        for (int i = 0; i < howMany; i++)
        {
            RemoveOneCube();
        }
    }

    public void DisableCubesVisuals()
    {
        foreach(FloatingCube cube in cubeList)
        {
            cube.DisableRenderer();
        }
    }
    public void EnableCubesVisual()
    {
        foreach (FloatingCube cube in cubeList)
        {
            cube.EnableRenderer();
        }
    }


    void RemoveOneCube()
    {
        FloatingCube toDelete = cubeList[(cubeList.Count - 1)];
        if (toDelete.initPoint != null)
            points.cubeDeleted(toDelete);
        Destroy(toDelete.selfGO);
        cubeList.RemoveAt(cubeList.Count - 1);
        currCubes -= 1;
    }

    void CalculateNextPos(FloatingCube cube, Transform parentPos)
    {
        Vector3 ouput = parentPos.position + new Vector3(0f, 0f, -1f) * cube.cubeScale;
        cube.selfGO.transform.position = ouput;
    }

    void LinkCubes(FloatingCube newCube)
    {
        if (currCubes < pointCounter + 1)
        {
            points.SetCubeOnPoints(newCube);
            AddSpring(newCube, newCube.initPoint.GetComponent<Rigidbody>(), 0.5f);
        }
        else
        {
            Rigidbody parentRB = cubeList[cubeList.Count - pointCounter].selfRB;
            CalculateNextPos(newCube, cubeList[cubeList.Count - pointCounter].selfGO.transform);
            AddSpring(newCube, parentRB, 0.5f);
        }
    }

    void AddSpring(FloatingCube newCube, Rigidbody parentCubeRB, float linkLength)
    {
        FixedJoint joint = newCube.selfGO.AddComponent<FixedJoint>();
        joint.connectedBody = parentCubeRB;


        /*SpringJoint joint = newCube.selfGO.AddComponent<SpringJoint>();
        joint.connectedBody = parentCubeRB;
        joint.autoConfigureConnectedAnchor = false;
        if (newCube.initPoint != null)
            joint.connectedAnchor = newCube.initPoint.position;
        else
            joint.connectedAnchor = Vector3.zero;
        joint.enableCollision = true;
        float distanceFromPoint = linkLength * newCube.cubeScale * newCube.cubeScale*0.4f;

        joint.maxDistance = distanceFromPoint * 1.2f;
        joint.minDistance = distanceFromPoint * 0.7f;

        joint.spring = 65f;
        joint.damper = 7f;
        //joint.massScale = 3f;
        */
    }
}
