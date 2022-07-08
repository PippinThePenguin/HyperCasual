using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HyperCasualNamespace {
  public class Pools : MonoBehaviour {
    [SerializeField] public static Pools ComponentPool;       
    [System.Serializable]
    public class Pool {
      public string tag;
      public GameObject prefab;
      public int size;
    }
    public Queue<GameObject> ObjectQueue;    
    public List<Pool> listofPools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake() {
      ComponentPool = this;

      poolDictionary = new Dictionary<string, Queue<GameObject>>();

      foreach (Pool pool in listofPools) {
        ObjectQueue = new Queue<GameObject>();
        for (int i = 0; i < pool.size; i++) {
          GameObject obj = Instantiate(pool.prefab);
          Mover objMover = obj.GetComponent<Mover>();
          if (objMover != null) {
            obj.GetComponent<Mover>().Tag = pool.tag;
          } else {
            Debug.LogWarning("No Mover on spawn " + obj.name);            
          }
          ObjectQueue.Enqueue(obj);
          obj.transform.SetParent(transform);                   
          obj.SetActive(false);
        }
        poolDictionary.Add(pool.tag, ObjectQueue);
      }
    }

    public GameObject GetNextBlock(string tag) {
      if (!poolDictionary.ContainsKey(tag)) {
        Debug.LogWarning("No Thing " + tag);
        //Debug.LogWarning(poolDictionary.Keys);
        return null;
      }
      GameObject currentObject = poolDictionary[tag].Dequeue();
      if (poolDictionary[tag].Count < 2) {
        AddToPool(tag);
      }
      return currentObject;
      
    }
    
    public void EnqueBlock(string tag, GameObject gameObject) {
      if (poolDictionary.ContainsKey(tag)){
        poolDictionary[tag].Enqueue(gameObject);
      }     
    }

    private void AddToPool(string tag) {
      foreach (Pool pool in listofPools) {
        if (pool.tag == tag) {
          GameObject obj = Instantiate(pool.prefab);
          Mover objMover = obj.GetComponent<Mover>();
          if (objMover != null) {
            obj.GetComponent<Mover>().Tag = pool.tag;
          } else {
            Debug.LogWarning("No Mover on add " + obj.name);
            ObjectQueue.Enqueue(obj);
          }         
          obj.transform.SetParent(transform);
          obj.SetActive(false);                              
          return;
        }
      }
    }
  }
}
