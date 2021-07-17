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
		//RoleMonoCtrl roleMonoCtrl;

        /// <summary>
        /// 角色移动控制脚本
        /// </summary>
        RoleMove roleMove;

        /// <summary>
        /// 滑动屏幕处理
        /// </summary>
        //SliderTouchMonoCtrl sliderTouchMonoCtrl;

        public override void Init()
        {

        }
        public override void Run()
        {
            roleMove = this.gameObject.GetJBehaviour<RoleMove>();
            //roleMove.Activate();
            //Log.PrintError("创建RoleMove+" + roleMove.gameObject.name);

            //挂载Mono脚本来辅助运行
            //sliderTouchMonoCtrl = this.gameObject.GetComponent<SliderTouchMonoCtrl>();
            //Log.PrintError("创建SliderTouchMonoCtrl+" + sliderTouchMonoCtrl.gameObject.name);

            //roleMonoCtrl = this.gameObject.GetComponent<RoleMonoCtrl>();
            //Log.PrintError("创建RoleMonoCtrl+" + roleMonoCtrl.gameObject.name);
            

            //sliderTouchMonoCtrl.OnSlideLeft += OnSlideLeft;
            //sliderTouchMonoCtrl.OnSlideRight += OnSlideRight;
            //sliderTouchMonoCtrl.OnSlideUp += OnSlideUp;
            //sliderTouchMonoCtrl.OnSlideDown += OnSlideDown;

        }

        public override void Loop()
        {
            
        }

        /// <summary>
        /// 停止角色一切动作
        /// </summary>
        public void OnRoleStop()
        {
            roleMove = this.gameObject.GetJBehaviour<RoleMove>();
            Log.PrintError(this.gameObject.name);
            roleMove.Pause();
        }
        /// <summary>
        /// 继续角色动作
        /// </summary>
        public void OnRoleContinue()
        {
            roleMove = this.gameObject.GetJBehaviour<RoleMove>();
            roleMove.Resume();
        }

        public void OnSlideLeft()
        {
            Log.Print("OnSlideLeft");
            roleMove.TurnLeftOrRight(true);
        }
        public void OnSlideRight()
        {
            Log.Print("OnSlideRight");
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
            //sliderTouchMonoCtrl.OnSlideLeft -= OnSlideLeft;
            //sliderTouchMonoCtrl.OnSlideRight -= OnSlideRight;
            //sliderTouchMonoCtrl.OnSlideUp -= OnSlideUp;
            //sliderTouchMonoCtrl.OnSlideDown -= OnSlideDown;
            //gameObject.RemoveJBehaviour<RoleMove>();
            //GameObject.Destroy(sliderTouchMonoCtrl);

        }

    }
}
