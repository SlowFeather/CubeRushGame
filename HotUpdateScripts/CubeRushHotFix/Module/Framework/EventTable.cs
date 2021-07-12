using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace HotUpdateScripts
{
    // Unity自带Event事件系统，可以对UnityEvent做一层封装，便于自己使用，使得自己的架构和UnityEvent之间是分开的
    //如果将来将此框架移植到非Unity项目中也是可用的，业务代码不需要改变，因为我们是自己的ModuleEvent，并非UnityEvent

    #region 将Unity自带的事件隔离封装自己的事件机制类
    //object其实就是事件的反生者，作为模块事件的参数
    //public class ModuleEvent : UnityEvent<object>
    //{

    //}
    public class ModuleEvent
    {

    }
    //为了更加灵活，提供一个泛型的模块事件，因为这个参数并不一定是模块事件的发生者，也可以是其他类型的参数
    //public class ModuleEvent<T> : UnityEvent<T>
    //{
        
    //}
    public class ModuleEvent<T>
    {

    }
    #endregion

    #region 制作事件库 类

    /// <summary>
    /// 用于管理所有事件的表容器
    /// </summary>
    public class EventTable
    {
        private Dictionary<string, ModuleEvent> m_mapEvents=new Dictionary<string, ModuleEvent>();


        /// <summary>
        /// 获取Type所指定的ModuleEvent（它其实是一个EventTable）
        /// 如果不存在，则实例化一个
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ModuleEvent GetEvent(string type)
        {
            //每一个事件就是一个实例，所以用type来表示这个事件的发生者
            //if (m_mapEvents == null)
            //{
            //    m_mapEvents = new Dictionary<string, ModuleEvent>();
            //}
            if (!m_mapEvents.ContainsKey(type))
            {
                //如果模块事件列表中没有这个事件记录，就为他创建一个模块事件
                m_mapEvents.Add(type, new ModuleEvent());
            }
            //返回这个木模块对应的模块事件
            return m_mapEvents[type];
        }

        /// <summary>
        /// 用于清除额外的事件表
        /// </summary>
        public void Clear()
        {
            if (m_mapEvents != null)
            {
                //@event 只是一个变量，为了区分系统的event，只是@开头的变量而已
                foreach (var @event in m_mapEvents)
                {
                    //@event.Value.RemoveAllListeners();
                    m_mapEvents.Remove(@event.Key);
                }
                m_mapEvents.Clear();
            }
        }
    }
    #endregion
}
