using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class SyncContext : MonoBehaviour
{
    public static TaskScheduler UnityTaskScheduler;
    public static int UnityThread;
    public static SynchronizationContext UnitySynchronizationContext;
    static public Queue<Action> RunInUpdate = new Queue<Action>();


    public void Awake()
    {
        UnitySynchronizationContext = SynchronizationContext.Current;
        UnityThread = Thread.CurrentThread.ManagedThreadId;
        UnityTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
    }

    public static bool isOnUnityThread
    {
        get { return UnityThread == Thread.CurrentThread.ManagedThreadId; }
    }


    public static void RunOnUnityThread(Action action)
    {
        if (UnityThread == Thread.CurrentThread.ManagedThreadId)
        {
            action();
        }
        else
        {
            lock (RunInUpdate)
            {
                RunInUpdate.Enqueue(action);
            }
        }
    }


    private void Update()
    {
        while (RunInUpdate.Count > 0)
        {
            Action action = null;
            lock (RunInUpdate)
            {
                if (RunInUpdate.Count > 0)
                    action = RunInUpdate.Dequeue();
            }

            if (action != null) action.Invoke();
        }
    }
}