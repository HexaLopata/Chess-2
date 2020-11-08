using UnityEngine;
using System.Collections.Generic;

public class CurrentLocation
{
    public GameObject MainFieldWhiteCell { get; set; }
    public GameObject MainFieldBlackCell { get; set; }
    public GameObject BattleFieldWhiteCell { get; set; }
    public GameObject BattleFieldBlackCell { get; set; }
    public List<GameObject> AdditionalCells { get; set; } = new List<GameObject>();
}