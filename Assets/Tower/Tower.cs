using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  [SerializeField] int towerAmount = 75;

  public bool CreateTower(Vector3 position)
  {
    Bank bank = FindObjectOfType<Bank>();

    if (!bank) return false;

    if (bank.CurrencyBalance < towerAmount) return false;

    bank.Withdraw(towerAmount);
    Instantiate(gameObject, position, Quaternion.identity);
    return true;
  }

}
