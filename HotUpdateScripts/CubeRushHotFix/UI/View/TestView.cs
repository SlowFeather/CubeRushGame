using JEngine.Core;
using JEngine.UI.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace HotUpdateScripts
{
    public class TestView: JBehaviour
    {
        public Text titleText;
        public Button jumpBtn;

        private float timeCount=0;


        public override void Init()
        {
            //Log.Print("Panel Init");
            this.FrameMode = false;
            this.Frequency = 100;
            jumpBtn.onClick.AddListener(OnBtnClick);
        }

        private void OnBtnClick()
        {
            Log.Print("点击了按钮");
            //gameObject.transform.DoMove
            jumpBtn.transform.GetComponent<RectTransform>().DOMoveY(1, 2).OnComplete(()=> {
                Log.Print("Hellop");
            });

        }

        public override void Run()
        {
            Log.Print("Panel Run");


            
        }
        public override void Loop()
        {
            timeCount++;
            titleText.text = timeCount.ToString();

            if (Input.GetKey(KeyCode.Space))
            {
                Log.Print("点击了空格");
            }
        }

        public override void End()
        {
            jumpBtn.onClick.RemoveListener(OnBtnClick);

        }



    }
}
