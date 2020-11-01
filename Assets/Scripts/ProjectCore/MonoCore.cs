using UnityEngine;

public class MonoCore : MonoBehaviour
{
    private Core _core;

    public void Awake()
    {
        _core = new Core(this);
    }
}