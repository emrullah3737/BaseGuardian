using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
  [SerializeField] int startingBalance = 150;
  [SerializeField] TMP_Text goldLabel;
  [SerializeField] int currencyBalance;

  public int CurrencyBalance { get { return currencyBalance; } }

  private void Awake()
  {
    currencyBalance = startingBalance;
    UpdateGoldLabel();
  }

  public void Deposit(int amount)
  {
    currencyBalance += Mathf.Abs(amount);
    UpdateGoldLabel();
  }

  public void Withdraw(int amount)
  {
    currencyBalance -= Mathf.Abs(amount);
    UpdateGoldLabel();

    if (currencyBalance < 0)
    {
      GameOver();
    }
  }

  public void UpdateGoldLabel()
  {
    goldLabel.text = $"Gold:{currencyBalance}";
  }

  void GameOver()
  {
    Scene currentScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(currentScene.buildIndex);
  }
}
