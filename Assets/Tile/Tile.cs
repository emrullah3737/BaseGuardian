using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  [SerializeField] bool isPlaceable = false;
  [SerializeField] Tower tower;

  public bool IsPlaceable { get { return isPlaceable; } }

  GridManager gridManager;
  PathFinder pathFinder;
  Vector2Int coordinates;

  void Awake()
  {
    gridManager = FindObjectOfType<GridManager>();
    pathFinder = FindObjectOfType<PathFinder>();
  }

  void Start()
  {
    BlockTile();
  }

  private void BlockTile()
  {
    if (gridManager == null) return;

    coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

    if (!isPlaceable)
    {
      gridManager.BlockNode(coordinates);
    }
  }

  void OnMouseDown()
  {
    if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
    {
      handleCreateTower();
    }
  }

  void handleCreateTower()
  {
    bool isTowerCreated = tower.CreateTower(transform.position);
    if (isTowerCreated)
    {
      gridManager.BlockNode(coordinates);
      pathFinder.NotifyReceivers();
    }
  }
}
