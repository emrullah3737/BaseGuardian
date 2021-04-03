using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
  [SerializeField] bool isPlaceable = false;
  [SerializeField] Tower tower;
  [SerializeField] int order;

  public bool IsPlaceable { get { return isPlaceable; } }
  public int Order { get { return order; } }

  void OnMouseDown()
  {
    if (isPlaceable)
    {
      handleCreateTower();
    }
  }

  void handleCreateTower()
  {
    bool isTowerCreated = tower.CreateTower(transform.position);
    isPlaceable = !isTowerCreated;
  }
}
