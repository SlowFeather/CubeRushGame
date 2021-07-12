using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts
{
    public static class UITools
    {
        /// <summary>
        /// ui的父物体
        /// </summary>
        public static GameObject uiRoot;

        /// <summary>
        /// 所有UI的字典
        /// </summary>
        public static Dictionary<string, JBehaviour> uiDic = new Dictionary<string, JBehaviour>();

        /// <summary>
        /// 创建一个UI,如果UI已存在则直接打开
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uiName"></param>
        public static void CreateUI<T>(string uiName) where T:JBehaviour
        {
            //首先检查列表中有米有这个UI,如果有则显示，如果没有则创建
            if (uiDic.ContainsKey(uiName))
            {
                Log.PrintWarning("已经有这个UI了,现在把他显示出来");
                uiDic[uiName].Show();
                uiDic[uiName].Resume();
                return;
            }
            JPrefab jPrefab = new JPrefab(UIDef.uiNametoPath[uiName], (b, jprefab) => {
                if (b)
                {
                    GameObject go = jprefab.Instantiate(uiRoot.transform.transform);
                    JBehaviour jBehaviour = go.GetJBehaviour<T>();
                    //已经手动挂载ClassBind不需要CreateOn  JBehaviour jBehaviour = JBehaviour.CreateOn<T>(go,false);
                    //设置为100毫秒更新一次，不用那么频繁
                    jBehaviour.FrameMode = false;
                    jBehaviour.Frequency = 100;
                    jBehaviour.Activate();
                    //添加到字典中
                    uiDic.Add(uiName, jBehaviour);
                }
                else
                {
                    Log.PrintError("Not Found UI : " + uiName);
                }
            });
        }

        public static GameObject GetUI(string uiName)
        {
            JBehaviour jBehaviour;
            if (uiDic.TryGetValue(uiName,out jBehaviour))
            {
                return jBehaviour.gameObject;
            }
            else
            {
                Log.PrintError("Not Find " + uiName);
                return null;
            }
        }

        public static void CloseUI(string uiName)
        {
            JBehaviour jBehaviour;
            if (uiDic.TryGetValue(uiName, out jBehaviour))
            {
                jBehaviour.Hide();
                jBehaviour.Pause();
                return;
            }
            else
            {
                Log.PrintError("Not Find " + uiName);
            }
        }

    }
}
