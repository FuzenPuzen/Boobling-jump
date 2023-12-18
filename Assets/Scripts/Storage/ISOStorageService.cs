
using System.Collections.Generic;
using UnityEngine;

public interface ISOStorageService 
{
    public ScriptableObject GetSOByType<T>();
    public List<ScriptableObject> GetSOsByType<T>();
}
