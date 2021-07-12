//
// Program.cs
//
// Author:
//       JasonXuDeveloper（傑） <jasonxudeveloper@gmail.com>
//
// Copyright (c) 2020 JEngine
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using JEngine.Core;
using JEngine.AntiCheat;
using JEngine.Examples;
using JEngine.Net;
using UnityEngine;
using UnityEngine.UI;
using JEngine.Event;
using JEngine.UI.UIKit;
using JEngine.UI;
using System.Diagnostics;

namespace HotUpdateScripts
{
    public class Program
    {
        public void RunGame()
        {
            Log.Print("Hello JEngine 如果使用Win把ctrl+s的习惯改为ctrl+b");
            InitGame();
            InitModule();
            InitUI();
            InitConfig();
            ModuleManager.Instance.ShowModule(ModuleDef.StartModule);
        }
        /// <summary>
        /// 初始化静态游戏组件
        /// </summary>
        private void InitGame()
        {
            Game.uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
            Game.mainCamera = Camera.main;
            Game.mainLight = GameObject.Find("Directional Light").GetComponent<Light>();
        }
        /// <summary>
        /// 初始化模块
        /// </summary>
        private void InitModule()
        {
            ModuleManager.Instance.Init("HotUpdateScripts");
            ModuleManager.Instance.CreateModule(ModuleDef.StartModule);
            ModuleManager.Instance.CreateModule(ModuleDef.MainModule);
        }
        /// <summary>
        /// 初始化UI
        /// </summary>
        private void InitUI()
        {
            GameObject uiroot = GameObject.Find("UIManager");
            UITools.uiRoot = uiroot;
        }
        /// <summary>
        /// 初始化系统设置
        /// </summary>
        private void InitConfig()
        {
            Application.targetFrameRate = 60;
        }

    }
}
