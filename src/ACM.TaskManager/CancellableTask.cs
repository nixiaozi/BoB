using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACM.TaskManager
{
    public class CancellableTask<T>
    {
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken canceltoken;
        private Action<T, CancellationToken> action;

        public T TaskDetail;
        public Task TheTask;


        private DateTime createTime;
        public DateTime CreateTime { get { return createTime; } }

        //private DateTime doneTime;
        //public DateTime DoneTime { get { return doneTime; } } // DoneTime 可以以移除的时间来算,毕竟误差不是很大

        public CancellableTask(T t, Action<T, CancellationToken> _action,Action _cancelaction=null)
        {
            cancelTokenSource = new CancellationTokenSource();
            canceltoken = cancelTokenSource.Token; // 初始化token
            action = _action;
            // 初始化创建时间
            createTime = DateTime.Now;


            TaskDetail = t;
            // 定义定义一个取消时回调
            if (_cancelaction != null)
            {
                canceltoken.Register(_cancelaction);
            }

        }

        public void DoTask()
        {
            TheTask = Task.Factory.StartNew(() =>
            {
                action.Invoke(default(T), canceltoken);
            }, canceltoken);

        }

        public void CancelTask()
        {
            cancelTokenSource.CancelAfter(3000);// 3秒后发起 Cancel Token! 
        }


    }
}
