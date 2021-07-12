using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JEngine.Core;
namespace HotUpdateScripts
{
    public class JBTest:JBehaviour
    {
        public override void Init()
        {
            Log.Print("JHHHHHH");
        }

        public override void Loop()
        {
            //Log.Print("time:"+this.FrameMode);
        }
        
    }
}
