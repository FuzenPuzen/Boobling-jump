using System.Collections.Generic;
using UnityEngine;

public class ConfigSO : ScriptableObject
{
    [SerializeField] public int[] _difficultyLevels =
                     {
                        6,
                        14,
                        23,
                        30,
                        38,
                        46,
                        54,
                        62,
                        78,
                        85,
                    };
    [SerializeField] public int _changeSpeedScore = 200;
    [SerializeField] private float _minStoolSpawnTime = 1f;
}
