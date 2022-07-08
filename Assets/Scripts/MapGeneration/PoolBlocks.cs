using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBlocks : MonoBehaviour
{
    public static PoolBlocks BlockPool;


    public class Block
    {
        public GameObject obj;
        public IRoadBlocks blockInterface;
        public Transform transform;
        public Block(GameObject o, IRoadBlocks i, Transform t)
        {
            obj = o;
            blockInterface = i;
            transform = t;
            obj.SetActive(false);
        }
        public void Init(Vector3 start, float velocity, BlockSet settings)
        {
            
            obj.SetActive(false);
            transform.position = start;
            
            obj.SetActive(true);            
            blockInterface.OnBlockPlace(settings, velocity);

        }
    }
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public Queue<Block> BlockQueue;    
    [SerializeField] private float speed;
    public List<Pool> listofPools;
    public Dictionary<string, Queue<Block>> poolDictionary;

    private void Awake()
    {
        BlockPool = this;

        poolDictionary = new Dictionary<string, Queue<Block>>();

        foreach (Pool pool in listofPools)
        {
            BlockQueue = new Queue<Block>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(transform);
                obj.GetComponent<IRoadBlocks>().OnStart();
                Block block = new Block(obj, obj.GetComponent<IRoadBlocks>(), obj.transform);
                BlockQueue.Enqueue(block);
            }
            poolDictionary.Add(pool.tag, BlockQueue);
        }
    }
    public void GetNextBlock(Vector3 point, string tag, BlockSet settings, float blockspeed)
    {
        Block currentBlock = poolDictionary[tag].Dequeue();
        currentBlock.Init(point, blockspeed, settings);
        poolDictionary[tag].Enqueue(currentBlock);
    }
}