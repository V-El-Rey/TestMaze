using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class PoolManager
{
    private static Transform _gameObjectsParentForPool;

    private static Dictionary<string, LinkedList<GameObject>> _objectsPool;

    public void InitializePool(Transform parent)
    {
        _gameObjectsParentForPool = parent;
        _objectsPool = new Dictionary<string, LinkedList<GameObject>>();
    }

    public static GameObject GetObjectFromPool(GameObject prefab)
    {
        if (!_objectsPool.ContainsKey(prefab.name))
        {
            _objectsPool[prefab.name] = new LinkedList<GameObject>();
        }

        GameObject result;

        if (_objectsPool[prefab.name].Count > 0)
        {
            result = _objectsPool[prefab.name].First.Value;
            _objectsPool[prefab.name].RemoveFirst();
            result.SetActive(true);
            return result;
        }

        result = GameObject.Instantiate(prefab);
        result.name = prefab.name;

        return result;
    }

    public static void ReturnToPool(GameObject target)
    {
        _objectsPool[target.name].AddFirst(target);
        target.transform.parent = _gameObjectsParentForPool;
        target.SetActive(false);
    }
}