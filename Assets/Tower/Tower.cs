using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  [SerializeField] int towerAmount = 75;
  [SerializeField] int buildDelay = 1;

  void Start()
  {
    StartCoroutine(Build());
  }

  public bool CreateTower(Vector3 position)
  {
    Bank bank = FindObjectOfType<Bank>();

    if (!bank) return false;

    if (bank.CurrencyBalance < towerAmount) return false;

    bank.Withdraw(towerAmount);
    Instantiate(gameObject, position, Quaternion.identity);
    return true;
  }

  IEnumerator Build()
  {
    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(false);

      foreach (Transform grandChild in child)
      {
        grandChild.gameObject.SetActive(false);
      }
    }

    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(true);

      yield return new WaitForSeconds(buildDelay);
      foreach (Transform grandChild in child)
      {
        grandChild.gameObject.SetActive(true);
      }
    }
  }
}
