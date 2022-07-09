using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace HyperCasualNamespace {

  public class ChunkSpawner : MonoBehaviour {
    public Pools Pool = Pools.ComponentPool;
    public ScoreController Score;
    [SerializeField] private string _commonFolder;
    [SerializeField] private string _uncommonFolder;
    private List<ChunkScriptable> _commonChunkList = new List<ChunkScriptable>();
    private List<ChunkScriptable> _uncommonChunkList = new List<ChunkScriptable>();
    private List<ChunkScriptable> _gateChunkList = new List<ChunkScriptable>();
    private List<float> _commonLengthList = new List<float>();
    private List<float> _uncommonLengthList = new List<float>();    
    private bool _canSpawn = true;
    public TestChunkSpawn Spawner;
    private int spawnIndex = 0;    
    public float simp = 2;
    private float _currentSpeed;



    void Start() {
      _currentSpeed = SpeedController.MainSpeedController.CurrentSpeed;
      SpeedController.MainSpeedController.OnSpeedChange.AddListener(SpeedUpdate);
      Pool = Pools.ComponentPool;
      Score = FindObjectOfType<ScoreController>();
      spawnIndex = 5;
      PopulateList();
      PopulateList1();
      PopulateList2();
      foreach (float elem in _uncommonLengthList) {
        //Debug.Log(elem);
      }
    }
    void Update() {
      CheckSpawn();
    }

    private void SpeedUpdate(float newSpeed) {
      _currentSpeed = newSpeed;
    }
    private void CheckSpawn() {
      if (_canSpawn) {
        if (spawnIndex > 8) {
          spawnIndex = 0;
          SpawnGates();
          //Debug.Log("Gates");
        } else if (Random.value < 0.1f + (spawnIndex * 0.00f)){
          //Debug.Log("Uncommon" + spawnIndex);
          SpawnUncommon();
        } else {
          SpawnRandom();
        }
        spawnIndex++;
        //Debug.Log(spawnIndex);
      }
    }

    private void SpawnGates() {
      int ind = Random.Range(0, _gateChunkList.Count);
      //Debug.Log("pigc" + ind );
      Spawner.TestChunk = _gateChunkList[ind];
      Spawner.DoIt = true;
      SpawnPause(_gateChunkList[ind].ChunkLength);                      
    }

    
    private void SpawnRandom() {
      int ind = Random.Range(0, _commonChunkList.Count);
      //Debug.Log("pigc" + ind );
      Spawner.TestChunk = _commonChunkList[ind];
      Spawner.DoIt = true;
      SpawnPause(_commonChunkList[ind].ChunkLength);
    }

    private void SpawnUncommon() {
      int ind = Random.Range(0, _uncommonChunkList.Count);
      //Debug.Log("pig" + ind);
      Spawner.TestChunk = _uncommonChunkList[ind];
      Spawner.DoIt = true;
      SpawnPause(_uncommonChunkList[ind].ChunkLength);
    }

    private async void SpawnPause(float length) {
      _canSpawn = false;
      float startTime = Time.time;
      while (Time.time - startTime < (length*Time.fixedDeltaTime*simp)/_currentSpeed) {
        await Task.Yield();
      }
      _canSpawn = true;
    }

    private void PopulateList() {
      Object[] assets = Resources.LoadAll("BasicOnes", typeof(ChunkScriptable));      
      _commonChunkList.Clear();
      foreach (Object obj in assets) {
        _commonChunkList.Add((ChunkScriptable)obj);
      }
    }

    private void PopulateListOld() {
      //string[] assetNames = AssetDatabase.FindAssets(null, new[] { "Assets/Prefabs/BasicOnes" });
      Object[] assets = Resources.LoadAll("BasicOnes", typeof(ChunkScriptable));
      _commonChunkList.Clear();
      foreach (Object obj in assets) {
        //var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
        //var character = AssetDatabase.LoadAssetAtPath<ChunkScriptable>(SOpath);
        _commonChunkList.Add((ChunkScriptable)obj);
      }
      //LengthList();
    }
    private void LengthList() {
      _commonLengthList.Clear();
      foreach (ChunkScriptable scriptable in _commonChunkList) {
        float maxZ = 0;
        foreach (ObjectInfo info in scriptable.MainChunkInfo) {
          if (info.ZPosition > maxZ) {
            maxZ = info.ZPosition;
          }
        }
        _commonLengthList.Add(maxZ * 1.2f + 10f);
      }
    }

    private void PopulateList1() {
      Object[] assets = Resources.LoadAll("UncommonChunks", typeof(ChunkScriptable));
      _uncommonChunkList.Clear();
      foreach (Object obj in assets) {
        _uncommonChunkList.Add((ChunkScriptable)obj);
      }
    }
    
    private void PopulateList2() {
        Object[] assets = Resources.LoadAll("ColorSwapChunks", typeof(ChunkScriptable));
        _gateChunkList.Clear();
        foreach (Object obj in assets) {
        _gateChunkList.Add((ChunkScriptable)obj);
        }      
    }       
  }
}
