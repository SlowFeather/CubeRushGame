using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace HotUpdateScripts
{
    public class StartView: JBehaviour
    {
        public Button buyBtn;
        public Button enterBtn;

        public Button leftBtn;
        public Button rightBtn;


        public override void Init()
        {
            buyBtn.onClick.AddListener(BuyBtnFunction);
            enterBtn.onClick.AddListener(EnterBtnFunction);
            leftBtn.onClick.AddListener(LeftBtnFunction);
            rightBtn.onClick.AddListener(RightBtnFunction);
        }
        public override void Run()
        {
            //这里看当前模块状态是buy还是start
            bool b = ModuleManager.Instance.GetModule<StartModule>().NeedBuy(0);
            if (b)
            {
                buyBtn.gameObject.SetActive(true);
                enterBtn.gameObject.SetActive(false);
            }
            else
            {
                buyBtn.gameObject.SetActive(false);
                enterBtn.gameObject.SetActive(true);
            }
        }
        private void RightBtnFunction()
        {
            ModuleManager.Instance.GetModule<StartModule>().OnRightBtnFunction();

        }

        private void LeftBtnFunction()
        {
            ModuleManager.Instance.GetModule<StartModule>().OnLeftBtnFunction();
        }

        private void EnterBtnFunction()
        {
            ModuleManager.Instance.GetModule<StartModule>().OnEnterBtnFunction();

        }

        private void BuyBtnFunction()
        {
            ModuleManager.Instance.GetModule<StartModule>().OnBuyBtnFunction();
        }

        public override void End()
        {
            buyBtn.onClick.RemoveListener(BuyBtnFunction);
            enterBtn.onClick.RemoveListener(EnterBtnFunction);
            leftBtn.onClick.RemoveListener(LeftBtnFunction);
            rightBtn.onClick.RemoveListener(RightBtnFunction);
        }

    }
}
