using UnityEngine;
using System.Collections.Generic;

public class CurrentLocation
{
    public GameObject WhiteCell { get; set; }
    public GameObject BlackCell { get; set; }
    public List<GameObject> AdditionalCells { get; set; } = new List<GameObject>();
}