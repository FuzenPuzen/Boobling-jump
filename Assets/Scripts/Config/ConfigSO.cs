using UnityEngine;

public class ConfigSO : ScriptableObject
{
    [SerializeField] public readonly int[] _difficultyLevels =
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
    [SerializeField] public readonly int _changeSpeedScore = 200;
    [SerializeField] public readonly float _minStoolSpawnTime = 1f;
}
