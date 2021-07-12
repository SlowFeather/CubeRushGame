using JEngine.Core;

namespace HotUpdateScripts
{
    public abstract class Module
    {
        /// <summary>
        /// 调用它以释放模块
        /// </summary>
        public virtual void Release()
        {
            Log.Print("Release");
        }
    }
}
