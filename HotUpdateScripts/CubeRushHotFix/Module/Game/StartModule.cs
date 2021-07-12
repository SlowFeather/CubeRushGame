using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts
{
    public class StartModule:BusinessModule
    {

        /// <summary>
        /// 角色列表，用于切换按钮
        /// </summary>
        public List<int> hasRoleList = new List<int>();
        /// <summary>
        /// 目前的这个角色
        /// </summary>
        public GameObject roleGameObject;
        /// <summary>
        /// 角色下面的地板
        /// </summary>
        public GameObject roleGroundGameObject;

        /// <summary>
        /// 检查是否需要显示Buy按钮
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool NeedBuy(int number)
        {
            for (int i = 0; i < hasRoleList.Count; i++)
            {
                if (hasRoleList[i]==number)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 创建所有角色，目前先创建一个
        /// </summary>
        public void CreateAllRole()
        {
            JPrefab jPrefab = new JPrefab("Game/Start/dog",(b,pre)=> {
                roleGameObject = pre.Instantiate();
            });
            JPrefab jPrefab2 = new JPrefab("Game/Start/floor01", (b, pre) => {
                roleGroundGameObject = pre.Instantiate();
            });
        }
        /// <summary>
        /// 修改相机位置
        /// </summary>
        private void ChangeCamera()
        {
            Game.mainCamera.transform.position = Vector3.zero;
            Game.mainCamera.transform.rotation = Quaternion.identity;
            Game.mainCamera.depth = 1;
            Game.uiCamera.depth = 0;
        }

        private void ChangeLight()
        {
            Game.mainLight.transform.rotation = Quaternion.Euler(34f, -30.5f, -13f);
        }
        public void ChangeQuality()
        {
            QualitySettings.shadowDistance = 8.5f;
        }
        public override void Create(object args = null)
        {
            //加载之前购买了哪些个人物,开始角色是默认拥有的
            hasRoleList.Add(0);
        }

        protected override void Show(object arg)
        {
            UITools.CreateUI<StartView>(UIDef.StartView);
            CreateAllRole();
            ChangeCamera();
            ChangeLight();
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

        public void OnBuyBtnFunction()
        {
            Log.Print("点击了购买按钮，获得人物");

        }

        public void OnEnterBtnFunction()
        {
            Log.Print("点击了进入游戏界面");
            //关闭当前页面。移除展示人物
            
            GameObject.Destroy(roleGameObject);
            GameObject.Destroy(roleGroundGameObject);

            ModuleManager.Instance.ShowModule(ModuleDef.MainModule);
            UITools.CloseUI(UIDef.StartView);
        }

        public override void Release()
        {
            base.Release();

        }
    }
}
