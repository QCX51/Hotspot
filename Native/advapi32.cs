using System;
using System.Runtime.InteropServices;

namespace Native
{
    internal class Advapi32
    {
        private const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        private const uint SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
        private const uint SE_PRIVILEGE_REMOVED = 0x00000004;
        private const uint SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;

        private struct SE_PRIVILEGE_NAME
        {
            internal const string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";
            internal const string SE_AUDIT_NAME = "SeAuditPrivilege";
            internal const string SE_BACKUP_NAME = "SeBackupPrivilege";
            internal const string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";
            internal const string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";
            internal const string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";
            internal const string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";
            internal const string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";
            internal const string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";
            internal const string SE_DEBUG_NAME = "SeDebugPrivilege";
            internal const string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";
            internal const string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";
            internal const string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";
            internal const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
            internal const string SE_INC_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";
            internal const string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";
            internal const string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";
            internal const string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";
            internal const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";
            internal const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
            internal const string SE_RELABEL_NAME = "SeRelabelPrivilege";
            internal const string SE_REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";
            internal const string SE_RESTORE_NAME = "SeRestorePrivilege";
            internal const string SE_SECURITY_NAME = "SeSecurityPrivilege";
            internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
            internal const string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";
            internal const string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";
            internal const string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";
            internal const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";
            internal const string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";
            internal const string SE_TCB_NAME = "SeTcbPrivilege";
            internal const string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";
            internal const string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";
            internal const string SE_UNDOCK_NAME = "SeUndockPrivilege";
            internal const string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";

        }
        [Flags]
        public enum SECURITY_INFORMATION : uint
        {
            OWNER_SECURITY_INFORMATION = 0x00000001,
            GROUP_SECURITY_INFORMATION = 0x00000002,
            DACL_SECURITY_INFORMATION = 0x00000004,
            SACL_SECURITY_INFORMATION = 0x00000008,
            UNPROTECTED_SACL_SECURITY_INFORMATION = 0x10000000,
            UNPROTECTED_DACL_SECURITY_INFORMATION = 0x20000000,
            PROTECTED_SACL_SECURITY_INFORMATION = 0x40000000,
            PROTECTED_DACL_SECURITY_INFORMATION = 0x80000000
        }
        private struct ACCESS_MASK
        {
            internal const long TOKEN_ASSIGN_PRIMARY = 0x0001;
            internal const long TOKEN_DUPLICATE = 0x0002;
            internal const long TOKEN_IMPERSONATE = 0x0004;
            internal const long TOKEN_QUERY = 0x0008;
            internal const long TOKEN_QUERY_SOURCE = 0x0010;
            internal const long TOKEN_ADJUST_PRIVILEGES = 0x0020;
            internal const long TOKEN_ADJUST_GROUPS = 0x0040;
            internal const long TOKEN_ADJUST_DEFAULT = 0x0080;
            internal const long TOKEN_ADJUST_SESSIONID = 0x0100;
            internal const long TOKEN_EXECUTE =
                STANDARD_RIGHTS_EXECUTE |
                TOKEN_IMPERSONATE;
            internal const long TOKEN_READ =
                STANDARD_RIGHTS_READ |
                TOKEN_QUERY;
            internal const long TOKEN_WRITE =
                STANDARD_RIGHTS_WRITE |
                TOKEN_ADJUST_PRIVILEGES |
                TOKEN_ADJUST_GROUPS |
                TOKEN_ADJUST_DEFAULT;
            internal const long TOKEN_ALL_ACCESS =
                STANDARD_RIGHTS_REQUIRED |
                TOKEN_ASSIGN_PRIMARY |
                TOKEN_DUPLICATE |
                TOKEN_IMPERSONATE |
                TOKEN_QUERY |
                TOKEN_QUERY_SOURCE |
                TOKEN_ADJUST_PRIVILEGES |
                TOKEN_ADJUST_GROUPS |
                TOKEN_ADJUST_DEFAULT |
                TOKEN_ADJUST_SESSIONID |
                TOKEN_EXECUTE |
                TOKEN_READ |
                TOKEN_WRITE;
            //internal const long TOKEN_ALL_ACCESS = 0xF003F;

            internal const long STANDARD_RIGHTS_REQUIRED = 0x000F0000L;
            internal const long STANDARD_RIGHTS_READ = READ_CONTROL;
            internal const long STANDARD_RIGHTS_WRITE = READ_CONTROL;
            internal const long STANDARD_RIGHTS_EXECUTE = READ_CONTROL;
            internal const long STANDARD_RIGHTS_ALL = 0x001F0000L;
            internal const long SPECIFIC_RIGHTS_ALL = 0x0000FFFFL;


            internal const long DELETE = 0x00010000L;
            internal const long READ_CONTROL = 0x00020000L;
            internal const long WRITE_DAC = 0x00040000L;
            internal const long WRITE_OWNER = 0x00080000L;
            internal const long SYNCHRONIZE = 0x00100000L;
        }
        //###########################################################################
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);
        //###########################################################################
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentProcess();
        //###########################################################################
        [DllImport("kernel32.dll", SetLastError = false)]
        public static extern int GetCurrentThreadId();
        //###########################################################################
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool RevertToSelf();
        //###########################################################################
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetKernelObjectSecurity(IntPtr Handle, SECURITY_INFORMATION RequestedInformation, IntPtr pSecurityDescriptor, UInt32 nLength, out UInt32 lpnLengthNeeded);
        //###########################################################################
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetKernelObjectSecurity(IntPtr Handle, SECURITY_INFORMATION SecurityInformation, IntPtr SecurityDescriptor);
        //###########################################################################
        [DllImport("advapi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, long DesiredAccess, out IntPtr TokenHandle);
        //###########################################################################
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState, uint BufferLength, UIntPtr PreviousState, UIntPtr ReturnLength);
        //###########################################################################
        [DllImport("advapi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);
        //###########################################################################

        private struct LUID
        {
            internal uint LowPart;
            internal int HighPart;
        }
        private struct LUID_AND_ATTRIBUTES
        {
            internal LUID Luid;
            internal uint Attributes;
        }
        private struct TOKEN_PRIVILEGES
        {
            internal uint PrivilegeCount;
            internal LUID_AND_ATTRIBUTES Privileges;
        }
        public static bool GetPrivileges()
        {
            TOKEN_PRIVILEGES tkp;
            tkp.PrivilegeCount = 1;
            tkp.Privileges.Attributes = SE_PRIVILEGE_ENABLED;
            OpenProcessToken(GetCurrentProcess(), ACCESS_MASK.TOKEN_ALL_ACCESS, out IntPtr hToken);
            LookupPrivilegeValue(string.Empty, SE_PRIVILEGE_NAME.SE_DEBUG_NAME, out tkp.Privileges.Luid);
            return (AdjustTokenPrivileges(hToken, false, ref tkp, 0U, UIntPtr.Zero, UIntPtr.Zero) && CloseHandle(hToken));
        }
    }
}
