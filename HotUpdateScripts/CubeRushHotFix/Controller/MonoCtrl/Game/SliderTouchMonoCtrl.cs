using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts
{
    public class SliderTouchMonoCtrl : MonoBehaviour
    {
        //private float fingerActionSensitivity = Screen.width * 0.05f; //手指动作的敏感度，这里设定为 二十分之一的屏幕宽度.
        private float fingerActionSensitivity = Screen.width * 0.5f; //手指动作的敏感度，这里设定为 二十分之一的屏幕宽度.

        //
        private float fingerBeginX;
        private float fingerBeginY;
        private float fingerCurrentX;
        private float fingerCurrentY;
        private float fingerSegmentX;
        private float fingerSegmentY;
        //
        private int fingerTouchState;
        //
        private int FINGER_STATE_NULL = 0;
        private int FINGER_STATE_TOUCH = 1;
        private int FINGER_STATE_ADD = 2;


        public Action OnSlideLeft;
        public Action OnSlideUp;
        public Action OnSlideDown;
        public Action OnSlideRight;

        void Start()
        {
            fingerActionSensitivity = Screen.width * 0.17f;

            fingerBeginX = 0;
            fingerBeginY = 0;
            fingerCurrentX = 0;
            fingerCurrentY = 0;
            fingerSegmentX = 0;
            fingerSegmentY = 0;

            fingerTouchState = FINGER_STATE_NULL;
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)||Input.GetMouseButtonDown(0))
            {

                if (fingerTouchState == FINGER_STATE_NULL)
                {
                    fingerTouchState = FINGER_STATE_TOUCH;
                    fingerBeginX = Input.mousePosition.x;
                    fingerBeginY = Input.mousePosition.y;
                }

            }
            if (fingerTouchState == FINGER_STATE_TOUCH)
            {
                fingerCurrentX = Input.mousePosition.x;
                fingerCurrentY = Input.mousePosition.y;
                fingerSegmentX = fingerCurrentX - fingerBeginX;
                fingerSegmentY = fingerCurrentY - fingerBeginY;

            }

            if (fingerTouchState == FINGER_STATE_TOUCH)
            {
                float fingerDistance = fingerSegmentX * fingerSegmentX + fingerSegmentY * fingerSegmentY;

                if (fingerDistance > (fingerActionSensitivity * fingerActionSensitivity))
                {
                    toAddFingerAction();
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetMouseButtonUp(0))
            {
                fingerTouchState = FINGER_STATE_NULL;
            }

        }
        private void toAddFingerAction()
        {

            fingerTouchState = FINGER_STATE_ADD;

            if (Mathf.Abs(fingerSegmentX) > Mathf.Abs(fingerSegmentY))
            {
                fingerSegmentY = 0;
            }
            else
            {
                fingerSegmentX = 0;
            }

            if (fingerSegmentX == 0)
            {
                if (fingerSegmentY > 0)
                {
                    OnSlideUp?.Invoke();
                }
                else
                {
                    OnSlideDown?.Invoke();
                }
            }
            else if (fingerSegmentY == 0)
            {
                if (fingerSegmentX > 0)
                {
                    OnSlideRight?.Invoke();
                }
                else
                {
                    OnSlideLeft?.Invoke();
                }
            }
        }
    }
}
