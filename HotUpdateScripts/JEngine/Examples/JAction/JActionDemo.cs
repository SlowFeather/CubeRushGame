using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JEngine.Core;
using UnityEngine;

namespace HotUpdateScripts
{
    public class JActionDemo :MonoBehaviour 
    {
        public void Start()
        {
            /*
            * ====================================
            *           JAction EXAMPLE
            * ====================================
            */
            int num = 0;
            int repeatCounts = 3;
            float repeatDuration = 0.5f;
            float timeout = 10f;

            //Simple use
            JAction j = new JAction();
            j.Do(() => Log.Print("[j] Hello from JAction!"))
              .Execute();

            //Until
            JAction j1 = new JAction();
            j1.Until(() => true)
              .Do(() => Log.Print("[j1] until condition has done"))
              .Execute();

            //Repeat
            JAction j2 = new JAction();
            j2.Repeat(() =>
            {
                num++;
                Log.Print($"[j2] num is: {num}");
            }, repeatCounts, repeatDuration)
              .Execute();

            //Repeat when
            JAction j3 = new JAction();
            j3.RepeatWhen(() =>
            {
                Log.Print($"[j3] num is more than 0, num--");
                num--;
            },
                          () => num > 0, repeatDuration, timeout)
              .Execute();

            //Repeat until
            JAction j4 = new JAction();
            j4.RepeatUntil(() =>
            {
                Log.Print($"[j4] num is less than 3, num++");
                num++;
            }, () => num < 3, repeatDuration, timeout)
              .Execute();

            //Delay
            JAction j5 = new JAction();
            j5.Do(() => Log.Print("[j5] JAction will do something else in 3 seconds"))
              .Delay(3.0f)
              .Do(() => Log.Print("[j5] Bye from JAction"))
              .Execute();

            //Execute Async
            JAction j6 = new JAction();
            _ = j6.Do(() => Log.Print("[j6] This is an async JAction"))
              .ExecuteAsync();

            //Execute Async Parallel
            //JAction j7 = new JAction();
            //j7.Do(() => Log.Print("[j7] This is an async JAction but runs parallel, callback will be called after it has done"))
            //  .ExecuteAsync( (a) => Log.Print("[j7] Done"));

            //Cancel a JAction
            JAction j8 = new JAction();
            j8.RepeatWhen(() => Log.Print("[j8] I am repeating!!!"), () => true, 1, timeout)
              .ExecuteAsync();
            //You can either add a cancel callback
            j8.OnCancel(() => Log.Print("[j8] has been cancelled!"));

            JAction j9 = new JAction();
            j9.Delay(5)
              .Do(() =>
              {
                  j8.Cancel();
                  Log.Print("[j9] cancelled j8");
              })
              .Execute();


            //Reset a JAction
            j8.Reset();
        }

    }
}
