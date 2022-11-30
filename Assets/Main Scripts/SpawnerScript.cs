using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
  public int poolSize;
  public int normalWoodSpawned;
  private float choiceRangeIncrease = 0f;
  private float choiceIncreaseAmount = 0.1f;

  public GameObject leftBranch;
  public GameObject rightBranch;
  public GameObject wood;
  private List<GameObject> leftBranchPool = new List<GameObject>();
  private List<GameObject> rightBranchPool = new List<GameObject>();
  private List<GameObject> woodPool = new List<GameObject>();

  void Awake()
  {
    CreateObjectPools();
  }

  void Start()
  {
    InvokeRepeating("IncreaseDifficulty", 10f, 20f);
  }

  private void CreateObjectPools()
  {
    GameObject leftBranchClone;
    GameObject rightBranchClone;
    GameObject woodClone;

    for (int i = 0; i < poolSize; i++)
    {
      leftBranchClone = Instantiate(leftBranch);
      rightBranchClone = Instantiate(rightBranch);
      woodClone = Instantiate(wood);

      leftBranchPool.Add(leftBranchClone);
      leftBranchClone.SetActive(false);

      rightBranchPool.Add(rightBranchClone);
      rightBranchClone.SetActive(false);

      woodPool.Add(woodClone);
      woodClone.SetActive(false);
    }
  }

  private GameObject GetPooledLeftBranch()
  {
    foreach (GameObject pooledObject in leftBranchPool)
    {
      if (!pooledObject.activeInHierarchy)
      {
        return pooledObject;
      }
    }

    GameObject newClone = Instantiate(leftBranch);
    leftBranchPool.Add(newClone);
    newClone.SetActive(false);

    return newClone;
  }

  private GameObject GetPooledRightBranch()
  {
    foreach (GameObject pooledObject in rightBranchPool)
    {
      if (!pooledObject.activeInHierarchy)
      {
        return pooledObject;
      }
    }

    GameObject newClone = Instantiate(rightBranch);
    rightBranchPool.Add(newClone);
    newClone.SetActive(false);

    return newClone;
  }

  private GameObject GetPooledWood()
  {
    foreach (GameObject pooledObject in woodPool)
    {
      if (!pooledObject.activeInHierarchy)
      {
        return pooledObject;
      }
    }

    GameObject newClone = Instantiate(wood);
    woodPool.Add(newClone);
    newClone.SetActive(false);

    return newClone;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    GameObject pooledObject;

    if (normalWoodSpawned >= 1)
    {
      float choice = Random.Range(0f, 1f);
      if (choice < 0.2f + choiceRangeIncrease)
      {
        pooledObject = GetPooledLeftBranch();
        normalWoodSpawned = 0;
      }
      else if (choice > 0.8f - choiceRangeIncrease)
      {
        pooledObject = GetPooledRightBranch();
        normalWoodSpawned = 0;
      }
      else
      {
        pooledObject = GetPooledWood();
        normalWoodSpawned += 1;
      }
    }
    else
    {
      pooledObject = GetPooledWood();
      normalWoodSpawned += 1;
    }

    pooledObject.transform.position = transform.position;
    pooledObject.SetActive(true);
  }

  private void IncreaseDifficulty()
  {
    if (choiceRangeIncrease < 0.2f)
    {
      choiceRangeIncrease += choiceIncreaseAmount;
    }
  }
}
