using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts
{
    public class MainModule:BusinessModule
    {

        /// <summary>
        /// 目前的这个角色
        /// </summary>
        public GameObject roleGameObject;

        /// <summary>
        /// 初始地形
        /// </summary>
        public GameObject groundGameObject;

        /// <summary>
        /// 初始地形位置
        /// </summary>
        private Vector3 groundInitPos = new Vector3(0.5f, -1, 10);

        /// <summary>
        /// 地板列表
        /// </summary>
        private List<GameObject> groundList = new List<GameObject>();

        /// <summary>
        /// 创建所有角色，目前先创建一个
        /// </summary>
        public void CreateRole()
        {
            JPrefab jPrefab = new JPrefab("Game/Game/Role/role0",(b,pre)=> {
                roleGameObject = pre.Instantiate();
                roleGameObject.transform.position = new Vector3(0, 0f, 0);
                Log.PrintError("生成"+roleGameObject.name);
                //添加玩家控制脚本
                RoleCtrl roleCtrl=roleGameObject.CreateJBehaviour<RoleCtrl>();
                //添加玩家移动脚本
                RoleMove roleMove = roleGameObject.CreateJBehaviour<RoleMove>();
                //添加触摸屏幕脚本
                SliderTouchMonoCtrl sliderTouchMonoCtrl = roleGameObject.AddComponent<SliderTouchMonoCtrl>();
                //添加碰撞脚本
                RoleMonoCtrl roleMonoCtrl=roleGameObject.AddComponent<RoleMonoCtrl>();

                //给脚本赋值
                roleMonoCtrl.roleCtrl = roleCtrl;
                sliderTouchMonoCtrl.OnSlideLeft += roleCtrl.OnSlideLeft;
                sliderTouchMonoCtrl.OnSlideRight += roleCtrl.OnSlideRight;
                sliderTouchMonoCtrl.OnSlideUp += roleCtrl.OnSlideUp;
                sliderTouchMonoCtrl.OnSlideDown += roleCtrl.OnSlideDown;




                //添加相机跟随脚本
                CameraCtrl cameraCtrl = Game.mainCamera.transform.gameObject.CreateJBehaviour<CameraCtrl>(false);
                cameraCtrl.roleGameObject = roleGameObject;
                cameraCtrl.Activate();
                Log.Print("添加CameraCtrl到Camera");
            });
        }


        /// <summary>
        /// 创建地形
        /// </summary>
        public void CreateGround(bool first=false)
        {

            JPrefab jPrefab = new JPrefab("Game/Game/Ground/floor01", (b, pre) => {
                groundGameObject = pre.Instantiate();
                if (first)
                {
                    groundGameObject.transform.position = groundInitPos;
                    groundList = new List<GameObject>();
                }
                else
                {
                    groundGameObject.transform.position = groundList[groundList.Count - 1].transform.position + new Vector3(0, 0, 40); ;
                }
                groundList.Add(groundGameObject);
            });
        }
        /// <summary>
        /// 更改光照方向
        /// </summary>
        private void ChangeLight()
        {
            Game.mainLight.transform.rotation = Quaternion.Euler(63.5f, -111.3f, -13f);
        }
        /// <summary>
        /// 删除一个地面
        /// </summary>
        public void DestroyGround()
        {
            GameObject.Destroy(groundList[0]);
            groundList.RemoveAt(0);
        }

        /// <summary>
        /// 创建一个新的地形
        /// </summary>
        public void CreateNewGround()
        {
            Log.Print("创建一个新的地面");
            CreateGround();
        }
        /// <summary>
        /// 修改相机位置
        /// </summary>
        private void ChangeCamera()
        {
            Game.mainCamera.transform.position = new Vector3(1.72f, 12.39f, -6.44f);
            Game.mainCamera.transform.rotation = Quaternion.Euler(60.564f, -8.878f, -4.277f);
            Game.mainCamera.depth = 0;
            Game.uiCamera.depth = 1;
            Game.mainCamera.clearFlags = CameraClearFlags.Skybox;
        }

        public void ChangeQuality()
        {
            QualitySettings.shadowDistance = 18f;
        }

        public override void Create(object args = null)
        {

        }

        protected override void Show(object arg)
        {
            UITools.CreateUI<MainView>(UIDef.MainView);
            ChangeLight();
            CreateRole();
            CreateGround(true);
            ChangeCamera();
            ChangeQuality();
        }

        public void OnLeftBtnFunction()
        {
            Log.Print("点击了左按钮");
        }

        public void OnRightBtnFunction()
        {
            Log.Print("点击了右按钮");
        }

        public void OnStopBtnFunction()
        {
            Log.Print("点击了停止");
            roleGameObject.GetJBehaviour<RoleCtrl>().OnRoleStop();
            Game.mainCamera.transform.gameObject.GetJBehaviour<CameraCtrl>().StopFollow();
        }

        public void OnContinueBtnFunction()
        {
            Log.Print("点击了继续");
            roleGameObject.GetJBehaviour<RoleCtrl>().OnRoleContinue();
            Game.mainCamera.transform.gameObject.GetJBehaviour<CameraCtrl>().ContinueFollow();

        }

        public void OnBackBtnFunction()
        {
            Log.Print("点击了返回");
            RemoveSceneChange();
            ModuleManager.Instance.ShowModule(ModuleDef.StartModule);
            UITools.CloseUI(UIDef.MainView);

        }
        /// <summary>
        /// 移除场景变动
        /// </summary>
        private void RemoveSceneChange()
        {
            Log.Print("移除相机脚本");
            //移除相机脚本
            Game.mainCamera.gameObject.RemoveJBehaviour<CameraCtrl>();
            Game.mainCamera.clearFlags = CameraClearFlags.Depth;
            //移除角色脚本
            //roleGameObject.RemoveJBehaviour<RoleCtrl>();

            //删除场景中的东西
            GameObject.Destroy(roleGameObject);
            //GameObject.Destroy(groundGameObject);
            foreach (GameObject item in groundList)
            {
                GameObject.Destroy(item);
            }

            groundList.Clear();
        }

        public override void Release()
        {
            base.Release();

        }
    }
}
