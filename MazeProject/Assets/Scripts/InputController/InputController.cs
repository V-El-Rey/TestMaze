using Base;
using UnityEngine;

public class InputController : BaseController
{
    private Vector3 _startTouch;
    private Vector3 _endTouch;

    private Camera _camera;

    private Vector3 _touchDelta;

    public InputController(Camera camera)
    {
        _camera = camera;
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
            _camera.transform.position += _touchDelta * 0.1f;
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
                //Shoot endOfPath coordinates here
            }
        }
    }
}