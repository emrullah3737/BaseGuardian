using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
  [SerializeField] List<Waypoint> path = new List<Waypoint>();
  [SerializeField] [Range(0f, 5f)] float speed = 1f;

  Enemy enemy;

  void Start()
  {
    enemy = FindObjectOfType<Enemy>();
  }

  void OnEnable()
  {
    FindPath();
    ReturnToStart();
    StartCoroutine(nameof(handleMove));
  }

  void FindPath()
  {
    path.Clear();

    GameObject[] tiles = GameObject.FindGameObjectsWithTag("Path");

    foreach (GameObject tile in tiles)
    {
      Waypoint waypoint = tile.GetComponent<Waypoint>();
      if (waypoint)
      {
        path.Add(waypoint);
      }

    }
  }

  void ReturnToStart()
  {
    transform.position = path[0].transform.position;
  }

  IEnumerator handleMove()
  {
    foreach (Waypoint waypoint in path)
    {
      Vector3 startPosition = transform.position;
      Vector3 endPosition = waypoint.transform.position;
      float travelPercent = 0f;

      transform.LookAt(endPosition);

      while (travelPercent < 1)
      {
        travelPercent += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
        yield return new WaitForEndOfFrame();
      }

    }

    PathFinish();
  }

  void PathFinish()
  {
    gameObject.SetActive(false);
    enemy.StealGold();
  }
}
