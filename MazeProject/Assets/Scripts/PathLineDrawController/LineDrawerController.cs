using System.Collections.Generic;
using Base;
using Struct;
using UnityEngine;

namespace PathLineDrawController
{
    public class LineDrawerController : BaseController
    {
        private LineDrawerView _lineDrawer;
        private List<Vector3> _vector3CoordinatesList = new List<Vector3>();
        
        public LineDrawerController(LineDrawerView lineDrawerView)
        {
            _lineDrawer = lineDrawerView;
            _lineDrawer.gameObject.SetActive(false);
        }

        public void SetOff() => _lineDrawer.gameObject.SetActive(false);

        public void DrawLine(Stack<CellCoordinate> path)
        {
            _lineDrawer.gameObject.SetActive(true);
            _lineDrawer.LineRenderer.positionCount = path.Count;
            _vector3CoordinatesList.Clear();
            foreach (var cellCoordinate in path)
            {
                _vector3CoordinatesList.Add(new Vector3(cellCoordinate.xCoordinate,cellCoordinate.yCoordinate,-0.7f));
            }
            _lineDrawer.LineRenderer.SetPositions(_vector3CoordinatesList.ToArray());
        }
    }
}