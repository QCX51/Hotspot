using System;
using TaskScheduler;

namespace Hotspot
{
    class Taskschd
    {
        /// <summary>
        /// Task Scheduler by QCX51
        // <href= https://msdn.microsoft.com/en-us/library/windows/desktop/aa383606(v=vs.85).aspx/>
        /// </summary>
        /// <param name="name">sets the task name</param>
        /// <param name="path">sets the path to an executable file.</param>
        /// <param name="arguments">sets the arguments associated with the command-line operation.</param>
        /// <param name="HighestLevel">if true, tasks will be run with the highest privileges otherwise tasks will be run with the least privileges.</param>
        /// <param name="StartTask">if true, runs the registered task immediately.</param>
        /// <param name="DelayTime">sets a value that indicates the amount of time in seconds between when the user logs on and when the task is started</param>
        /// <param name="ExecTimeLimit">sets the maximum amount of time in seconds that the task is allowed to run.</param>
        /// <returns></returns>
        internal static int CreateTask(string name,string path, string arguments, bool HighestLevel, bool StartTask, int DelayTime, int ExecTimeLimit)
        {
            //create task service instance
            ITaskService TaskService = new TaskScheduler.TaskScheduler();
            TaskService.Connect();
            foreach (IRegisteredTask Task in TaskService.GetFolder(@"\").GetTasks((int)_TASK_ENUM_FLAGS.TASK_ENUM_HIDDEN))
            {
                if (name == Task.Name && !Task.Enabled) { Task.Enabled = true; }
                if (name == Task.Name && StartTask && Task.State == _TASK_STATE.TASK_STATE_RUNNING) { return 2; }
                else if (name == Task.Name && StartTask) { Task.Run(null); return 1; }
            }
            ITaskDefinition TaskDefinition = TaskService.NewTask(0);
            TaskDefinition.Settings.Enabled = true;
            TaskDefinition.Settings.AllowDemandStart = true;
            TaskDefinition.Settings.Hidden = true;
            TaskDefinition.Settings.StopIfGoingOnBatteries = false;
            TaskDefinition.Settings.RunOnlyIfNetworkAvailable = false;
            TaskDefinition.Settings.RunOnlyIfIdle = false;
            TaskDefinition.Settings.AllowHardTerminate = true;
            TaskDefinition.Settings.DisallowStartIfOnBatteries = false;
            TaskDefinition.Settings.MultipleInstances = _TASK_INSTANCES_POLICY.TASK_INSTANCES_IGNORE_NEW;
            TaskDefinition.Settings.StartWhenAvailable = true;
            TaskDefinition.Settings.WakeToRun = true;
            TaskDefinition.Principal.RunLevel = HighestLevel ? _TASK_RUNLEVEL.TASK_RUNLEVEL_HIGHEST : _TASK_RUNLEVEL.TASK_RUNLEVEL_LUA;
            TaskDefinition.Settings.Compatibility = _TASK_COMPATIBILITY.TASK_COMPATIBILITY_V2_1;

            //create trigger for task creation.
            ITriggerCollection TriggerCollection = TaskDefinition.Triggers;
            ILogonTrigger LogonTrigger = (ILogonTrigger)TriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON);
            LogonTrigger.Repetition.StopAtDurationEnd = false;
            if (DelayTime > 0) LogonTrigger.Delay = "PT" + DelayTime + "S";
            //_trigger.StartBoundary = DateTime.Now.AddSeconds(15).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            //_trigger.EndBoundary = DateTime.Now.AddMinutes(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            if (ExecTimeLimit > 0) LogonTrigger.ExecutionTimeLimit = "PT" + ExecTimeLimit + "S";
            LogonTrigger.Enabled = true;

            ///get actions.
            IActionCollection ActionCollection = TaskDefinition.Actions;
            _TASK_ACTION_TYPE TaskActionType = _TASK_ACTION_TYPE.TASK_ACTION_EXEC;

            //create new action
            IAction Action = ActionCollection.Create(TaskActionType);
            IExecAction ExecAction = Action as IExecAction;
            ExecAction.WorkingDirectory = Environment.CurrentDirectory;
            ExecAction.Arguments = arguments;
            ExecAction.Path = path;
            ITaskFolder TaskFolder = TaskService.GetFolder(@"\");
            IRegisteredTask RegisteredTask = TaskFolder.RegisterTaskDefinition(name, TaskDefinition, (int)_TASK_CREATION.TASK_CREATE_OR_UPDATE, null, null, _TASK_LOGON_TYPE.TASK_LOGON_NONE, null);
            if (StartTask) { RegisteredTask.Run(null); }
            return 0;
        }
    }
}
