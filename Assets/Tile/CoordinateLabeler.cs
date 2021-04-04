using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
  [SerializeField] Color defaultColor = Color.white;
  [SerializeField] Color blockedColor = Color.gray;
  [SerializeField] Color exploredColor = Color.yellow;
  [SerializeField] Color pathColor = Color.red;
  [Tooltip("Unity Snap Settings")]

  TextMeshPro label;
  Vector2Int coordinates = new Vector2Int();
  GridManager gridManager;

  void Awake()
  {
    gridManager = FindObjectOfType<GridManager>();
    label = GetComponent<TextMeshPro>();
    label.enabled = false;

    handleDisplayCoordinates();
  }

  void Update()
  {
    if (!Application.isPlaying)
    {
      handleDisplayCoordinates();
      handleUpdateObjectName();
    }

    handleToggleLabels();
    SetLabelColor();
  }

  void SetLabelColor()
  {
    if (gridManager == null) return;

    Node node = gridManager.GetNode(coordinates);

    if (node == null) return;

    if (!node.isWalkable)
    {
      label.color = blockedColor;
      return;
    }

    if (node.isPath)
    {
      label.color = pathColor;
      return;
    }

    if (node.isExplored)
    {
      label.color = exploredColor;
      return;
    }

    label.color = defaultColor;
  }

  void handleToggleLabels()
  {
    if (Input.GetKeyDown(KeyCode.C))
    {
      label.enabled = !label.enabled;
    }
  }

  void handleDisplayCoordinates()
  {
    if (gridManager == null) return;

    coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
    coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
    label.text = $"{coordinates.x},{coordinates.y}";
  }

  void handleUpdateObjectName()
  {
    transform.parent.name = coordinates.ToString();
  }
}
