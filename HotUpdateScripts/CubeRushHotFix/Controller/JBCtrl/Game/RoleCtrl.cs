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
        /// 角色碰撞脚本
        /// </summary>
		RoleMonoCtrl roleMonoCtrl;

        /// <summary>
        /// 角色移动控制脚本
        /// </summary>
        RoleMove roleMove;

        /// <summary>
        /// 滑动屏幕处理
        /// </summary>
        SliderTouchMonoCtrl sliderTouchMonoCtrl;

        public override void Init()
        {
            roleMove = this.gameObject.CreateJBehaviour<RoleMove>();
            //挂载Mono脚本来辅助运行
            sliderTouchMonoCtrl = this.gameObject.AddComponent<SliderTouchMonoCtrl>();
            roleMonoCtrl = this.gameObject.AddComponent<RoleMonoCtrl>();
            roleMonoCtrl.roleCtrl = this;

            sliderTouchMonoCtrl.OnSlideLeft += OnSlideLeft;
            sliderTouchMonoCtrl.OnSlideRight += OnSlideRight;
            sliderTouchMonoCtrl.OnSlideUp += OnSlideUp;
            sliderTouchMonoCtrl.OnSlideDown += OnSlideDown;
        }
        public override void Run()
        {


        }

        public override void Loop()
        {
            
        }

        /// <summary>
        /// 停止角色一切动作
        /// </summary>
        public void OnRoleStop()
        {
            roleMove.Pause();
        }
        /// <summary>
        /// 继续角色动作
        /// </summary>
        public void OnRoleContinue()
        {
            roleMove.Resume();
        }

        public void OnSlideLeft()
        {
            Log.Print("OnSlideLeft");
            //roleMonoCtrl.TurnLeftOrRight(true);
            roleMove.TurnLeftOrRight(true);
        }
        public void OnSlideRight()
        {
            Log.Print("OnSlideRight");
            //roleMonoCtrl.TurnLeftOrRight(false);
            roleMove.TurnLeftOrRight(false);

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
            //gameObject.RemoveJBehaviour<RoleMove>();
            //GameObject.Destroy(sliderTouchMonoCtrl);

        }

    }
}
