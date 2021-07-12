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
        public Button backBtn;

        public Button leftBtn;
        public Button rightBtn;
        public override void Init()
        {
            stopBtn.onClick.AddListener(StopBtnFunction);
            backBtn.onClick.AddListener(BackBtnFunction);
            leftBtn.onClick.AddListener(LeftBtnFunction);
            rightBtn.onClick.AddListener(RightBtnFunction);


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

        public override void Run()
        {
            
        }

        public override void End()
        {
            
        }
    }
}
