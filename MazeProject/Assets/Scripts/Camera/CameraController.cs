using Base;
using UnityEngine;

namespace CameraControl
{
    public class CameraController : BaseController
    {
        private CameraView _cameraView;
        
        public CameraController(CameraView cameraView)
        {
            _cameraView = cameraView;
        }

        public override void UpdateExecute()
        {
            base.UpdateExecute();
        }
    }
}
