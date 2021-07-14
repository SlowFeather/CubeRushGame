using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts
{
    public class CameraCtrl: JBehaviour
    {
        //1.7 12.4 -6.5
        //0 -0.5 0
        private Vector3 offsetVect=new Vector3(1.7f,11.9f,-6.5f);
        public GameObject roleGameObject;
        public override void Init()
        {
            this.FrameMode = false;
            this.Frequency = 10;
        }
        public void StopFollow()
        {
            this.Pause();
        }

        public void ContinueFollow()
        {
            this.Resume();

        }
        public override void Loop()
        {
            if (roleGameObject == null)
            {
                return;
            }
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, roleGameObject.transform.position + offsetVect, this.LoopDeltaTime * 5);
        }
        public override void End()
        {

        }
    }
}
