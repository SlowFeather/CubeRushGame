using JEngine.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts
{
    public class RoleCtrl: JBehaviour
    {
        /// <summary>
        /// 角色移动控制脚本
        /// </summary>
		RoleMonoCtrl roleMonoCtrl;
        /// <summary>
        /// 滑动屏幕处理
        /// </summary>
        SliderTouchMonoCtrl sliderTouchMonoCtrl;

        public override void Init()
        {

        }
        public override void Run()
        {
			//挂载Mono脚本来辅助运行
			roleMonoCtrl=this.gameObject.AddComponent<RoleMonoCtrl>();
            sliderTouchMonoCtrl = this.gameObject.AddComponent<SliderTouchMonoCtrl>();

            roleMonoCtrl.roleCtrl = this;

            sliderTouchMonoCtrl.OnSlideLeft += OnSlideLeft;
            sliderTouchMonoCtrl.OnSlideRight += OnSlideRight;
            sliderTouchMonoCtrl.OnSlideUp += OnSlideUp;
            sliderTouchMonoCtrl.OnSlideDown += OnSlideDown;

        }

        public override void Loop()
        {
            

        }

        public void OnSlideLeft()
        {
            Log.Print("OnSlideLeft");
            roleMonoCtrl.TurnLeftOrRight(true);

        }
        public void OnSlideRight()
        {
            Log.Print("OnSlideRight");
            roleMonoCtrl.TurnLeftOrRight(false);

        }
        public void OnSlideUp()
        {
            Log.Print("OnSlideUp");

        }
        public void OnSlideDown()
        {
            Log.Print("OnSlideDown");

        }

        public void OnTriggerEnter(Collider collider)
        {
            Log.Print("TriggerEnter:"+collider.name);
            switch (collider.name)
            {
                case "createnext":
                    ModuleManager.Instance.GetModule<MainModule>().CreateNewGround();
                    break;
                case "destroyme":
                    ModuleManager.Instance.GetModule<MainModule>().DestroyGround();

                    break;
                default:
                    break;
            }
        }

        public void OnCollisionEnter(Collision collider)
        {
            Log.Print("CollisionEnter:" + collider.collider.name);

        }

        public override void End()
        {
            sliderTouchMonoCtrl.OnSlideLeft -= OnSlideLeft;
            sliderTouchMonoCtrl.OnSlideRight -= OnSlideRight;
            sliderTouchMonoCtrl.OnSlideUp -= OnSlideUp;
            sliderTouchMonoCtrl.OnSlideDown -= OnSlideDown;

            GameObject.Destroy(roleMonoCtrl);
			GameObject.Destroy(sliderTouchMonoCtrl);

        }

    }
}
