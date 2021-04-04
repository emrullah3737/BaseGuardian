using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
  [SerializeField] Vector2Int startCoordinates;
  [SerializeField] Vector2Int destinationCoordinates;

  public Vector2Int StartCoordinates { get { return startCoordinates; } }
  public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

  Node startNode;
  Node destinationNode;
  Node currenctSearchNode;

  Queue<Node> frontier = new Queue<Node>();
  Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

  Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
  GridManager gridManager;
  Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

  void Awake()
  {
    gridManager = FindObjectOfType<GridManager>();
    if (gridManager != null)
    {
      grid = gridManager.Grid;
      startNode = gridManager.Grid[startCoordinates];
      destinationNode = gridManager.Grid[destinationCoordinates];
    }
  }

  void Start()
  {
    FindPath();
  }

  public List<Node> FindPath()
  {
    return FindPath(startCoordinates);
  }

  public List<Node> FindPath(Vector2Int coordinates)
  {
    gridManager.ResetNodes();
    BreadthFirstSearch(coordinates);
    return BuildPath();
  }

  void ExpoloreNeighbors()
  {
    List<Node> neighbors = new List<Node>();

    foreach (Vector2Int direction in directions)
    {
      Vector2Int neighborsCoordinate = currenctSearchNode.coordinates + direction;

      if (grid.ContainsKey(neighborsCoordinate))
      {
        neighbors.Add(grid[neighborsCoordinate]);
      }
    }

    foreach (Node neighbor in neighbors)
    {
      if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
      {
        neighbor.connectedTo = currenctSearchNode;
        reached.Add(neighbor.coordinates, neighbor);
        frontier.Enqueue(neighbor);
      }
    }
  }

  void BreadthFirstSearch(Vector2Int coordinates)
  {
    startNode.isWalkable = true;
    destinationNode.isWalkable = true;

    frontier.Clear();
    reached.Clear();

    bool isRunning = true;

    frontier.Enqueue(grid[coordinates]);
    reached.Add(coordinates, grid[coordinates]);

    while (frontier.Count > 0 && isRunning)
    {
      currenctSearchNode = frontier.Dequeue();
      currenctSearchNode.isExplored = true;
      ExpoloreNeighbors();

      if (currenctSearchNode.coordinates == destinationCoordinates)
      {
        isRunning = false;
      }
    }
  }


  List<Node> BuildPath()
  {
    List<Node> path = new List<Node>();
    Node currentNode = destinationNode;

    path.Add(currentNode);
    currentNode.isPath = true;

    while (currentNode.connectedTo != null)
    {
      currentNode = currentNode.connectedTo;
      path.Add(currentNode);
      currentNode.isPath = true;
    }

    path.Reverse();

    return path;
  }

  public bool WillBlockPath(Vector2Int coordinates)
  {
    if (!grid.ContainsKey(coordinates)) return false;

    bool previousState = grid[coordinates].isWalkable;

    grid[coordinates].isWalkable = false;
    List<Node> newPath = FindPath();
    grid[coordinates].isWalkable = previousState;

    if (newPath.Count <= 1)
    {
      FindPath();
      return true;
    }

    return false;
  }

  public void NotifyReceivers()
  {
    BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
  }
}
