using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class PoolManager
{
    private static Transform _gameObjectsParentForPool;
    private static GameObject _cellPrefab;

    private static List<CellView> _mazeCellsPool;

    public void InitializePool(Transform parent, GameObject cellPrefab)
    {
        _gameObjectsParentForPool = parent;
        _cellPrefab = cellPrefab;
        _mazeCellsPool = new List<CellView>();
    }

    public static CellView GetCellFromPool(Vector3 position)
    {
        CellView result;
        if (_mazeCellsPool.Count == 0)
        {
            var cellPrefab = GameObject.Instantiate(_cellPrefab, _gameObjectsParentForPool);
            cellPrefab.TryGetComponent<CellView>(out result);
            return result;
        }

        result = _mazeCellsPool.First();
        _mazeCellsPool.Remove(result);
        return result;
    }

    public static void ReturnToPool(CellView cellView)
    {
        _mazeCellsPool.Add(cellView);
        cellView.gameObject.transform.parent = _gameObjectsParentForPool;
        cellView.gameObject.SetActive(false);
    }
}