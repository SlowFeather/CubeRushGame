using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace HotUpdateScripts
{
    public class MainView: JBehaviour
    {
        public Button stopBtn;
        public Button continueBtn;

        public Button backBtn;

        public Button leftBtn;
        public Button rightBtn;


        public override void Init()
        {
            stopBtn.onClick.AddListener(StopBtnFunction);
            backBtn.onClick.AddListener(BackBtnFunction);
            leftBtn.onClick.AddListener(LeftBtnFunction);
            rightBtn.onClick.AddListener(RightBtnFunction);
            continueBtn.onClick.AddListener(ContinueBtnFunction);
        }

        private void RightBtnFunction()
        {
            ModuleManager.Instance.GetModule<MainModule>().OnRightBtnFunction();

        }

        private void LeftBtnFunction()
        {
            ModuleManager.Instance.GetModule<MainModule>().OnLeftBtnFunction();

        }

        private void BackBtnFunction()
        {
            ModuleManager.Instance.GetModule<MainModule>().OnBackBtnFunction();

        }

        private void StopBtnFunction()
        {
            ModuleManager.Instance.GetModule<MainModule>().OnStopBtnFunction();

        }
        /// <summary>
        /// 点击了继续按钮
        /// </summary>
        private void ContinueBtnFunction()
        {
            ModuleManager.Instance.GetModule<MainModule>().OnContinueBtnFunction();

        }
        public override void Run()
        {
            
        }

        public override void End()
        {
            
        }
    }
}
