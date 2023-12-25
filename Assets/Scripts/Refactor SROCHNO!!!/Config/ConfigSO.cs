using UnityEngine;

public class ConfigSO : ScriptableObject
{
    [SerializeField] public readonly int[] _difficultyLevels =
                     {
                        2000,
                        200014,
                        200023,
                        200030,
                        200038,
                        200046,
                        200054,
                        200062,
                        200078,
                        200085,
                    };
    [SerializeField] public readonly int _changeSpeedScore = 200;
    [SerializeField] public readonly float _minStoolSpawnTime = 1f;
}
