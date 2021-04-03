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

  TextMeshPro label;
  Vector2Int coordinates = new Vector2Int();
  Waypoint waypoint;

  void Awake()
  {
    label = GetComponent<TextMeshPro>();
    label.enabled = false;

    waypoint = GetComponentInParent<Waypoint>();
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
    if (waypoint.IsPlaceable)
    {
      label.color = defaultColor;
    }
    else
    {
      label.color = blockedColor;
    }
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
    coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
    coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
    label.text = $"{coordinates.x},{coordinates.y}";
  }

  void handleUpdateObjectName()
  {
    transform.parent.name = coordinates.ToString();
  }
}
