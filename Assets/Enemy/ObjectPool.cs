using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  // Start is called before the first frame update

  [SerializeField] [Range(0.1f, 30f)] float createWaitTime = 2f;
  [SerializeField] [Range(0, 50)] int poolSize = 5;
  [SerializeField] GameObject poolObject;

  GameObject[] pool;

  void Awake()
  {
    PopulatePool();
  }

  void Start()
  {
    StartCoroutine(SpawnEnemy());
  }

  void PopulatePool()
  {
    pool = new GameObject[poolSize];

    for (int i = 0; i < pool.Length; i++)
    {
      pool[i] = Instantiate(poolObject, transform);
      pool[i].SetActive(false);
    }
  }

  void ActivePoolObject()
  {
    foreach (GameObject poolItem in pool)
    {
      if (!poolItem.activeInHierarchy)
      {
        poolItem.SetActive(true);
        break;
      }
    }
  }

  IEnumerator SpawnEnemy()
  {
    while (true)
    {
      ActivePoolObject();
      yield return new WaitForSeconds(createWaitTime);
    }
  }
}
