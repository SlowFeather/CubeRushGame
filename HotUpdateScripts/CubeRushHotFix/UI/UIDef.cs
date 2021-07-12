using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotUpdateScripts
{
    public static class UIDef
    {
        /// <summary>
        /// UI名称对应路径字典
        /// </summary>
        public static Dictionary<string, string> uiNametoPath = new Dictionary<string, string>() 
        {
            { "TestView", "UI/uiview_testview" },
            { "StartView", "UI/uiview_startview" },
            { "MainView", "UI/uiview_mainview" },
            //{ "TestView", "UI/uiview_testview.prefab" },
            //{ "StartView", "UI/uiview_startview.prefab" },
            //{ "MainView", "UI/uiview_mainview.prefab" },

        };

        //UI名称字符串
        public static string TestView= "TestView";
        public static string StartView = "StartView";
        public static string MainView = "MainView";



    }
}
