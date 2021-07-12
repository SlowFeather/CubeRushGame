using JEngine.Core;

namespace HotUpdateScripts
{
    //public abstract class ServiceModule<T> : System.IDisposable where T : class, new()
    //基层服务模块也要受模块统一基类管理
    public abstract class ServiceModule<T> : Module, System.IDisposable where T : class, new()
    {
        #region 通用泛型C#单例
        private static T instance = default(T);

        /// <summary>
        /// 用于实现单例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }

        /// <summary>
        /// 供子类调用它来检查它是否以单例形式创建   【如果子类再次new 那么instance就是null】
        /// </summary>
        /// <param name="args"></param>
        protected void CheckSingleton()
        {
            if (instance == null)
            {
                throw new System.Exception("ServiceModule<" + typeof(T).Name + "> 无法直接实例化，因为它是一个单例!");
            }
        }
        public virtual void Dispose()
        {
            Log.Print(typeof(T).Name+":Disposed");
        }
        #endregion
    }
}
