using Base;
using Data;
using Struct;
using UnityEngine;
using UnityEngine.Events;

public class InputController : BaseController
{
    private Vector3 _startTouch;
    private Vector3 _endTouch;

    private float _dragThreshold;
    private float _sensitivity;

    private Camera _camera;

    private Vector3 _touchDelta;

    private CellCoordinate _targetPosition;

    public UnityAction<CellCoordinate> SendTargetPositionToPlayer;

    public InputController(Camera camera, GameData data)
    {
        _camera = camera;
        _dragThreshold = data.dragThreshold;
        _sensitivity = data.dragSensitivity;
    }

    public override void UpdateExecute()
    {
        base.UpdateExecute();
        if (Input.GetMouseButtonDown(0))
        {
          HandleCellTouch();  
          _startTouch = _camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            _touchDelta = _startTouch - _camera.ScreenToViewportPoint(Input.mousePosition);
            _touchDelta.Normalize();
            if (_touchDelta.sqrMagnitude > _dragThreshold * _dragThreshold)
            {
                _camera.transform.position += _touchDelta * _sensitivity;
            }
        }
    }

    private void HandleCellTouch()
    {
        Ray raycast = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycasthit;
        if (Physics.Raycast(raycast, out raycasthit))
        {
            if (raycasthit.collider.CompareTag("Cell"))
            {
                var position = raycasthit.collider.transform.position;
                _targetPosition.xCoordinate = (int)position.x;
                _targetPosition.yCoordinate = (int)position.y;
                SendTargetPositionToPlayer?.Invoke(_targetPosition);
            }
        }
    }
}