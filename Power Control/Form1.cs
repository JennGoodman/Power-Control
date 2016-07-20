﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace Power_Control
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IPowerManager pm = new PowerManager();
            PowerPlan currentPlan = pm.GetCurrentPlan();
            if (currentPlan.name.Contains("Hig"))
            {
                highPerformanceToolStripMenuItem.Checked = true;
            }
            else if (currentPlan.name.Contains("Bal"))
            {
                balancedToolStripMenuItem.Checked = true;
            }
            else if (currentPlan.name.Contains("Pow"))
            {
                powerSaverToolStripMenuItem.Checked = true;
            }
            else
            {
                exitToolStripMenuItem.Checked = true;
            }
        }

        private void highPerformanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo myInfo = new ProcessStartInfo("powercfg");
            myInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myInfo.Arguments = "-setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c";
            Process.Start(myInfo);
            
            highPerformanceToolStripMenuItem.Checked = true;
            balancedToolStripMenuItem.Checked = false;
            powerSaverToolStripMenuItem.Checked = false;
        }

        private void balancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo myInfo = new ProcessStartInfo("powercfg");
            myInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myInfo.Arguments = "-setactive 381b4222-f694-41f0-9685-ff5bb260df2e";
            Process.Start(myInfo);
            
            highPerformanceToolStripMenuItem.Checked = false;
            balancedToolStripMenuItem.Checked = true;
            powerSaverToolStripMenuItem.Checked = false;
        }

        private void powerSaverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo myInfo = new ProcessStartInfo("powercfg");
            myInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myInfo.Arguments = "-setactive a1841308-3541-4fab-bc81-f71556f20b4a";
            Process.Start(myInfo);

            highPerformanceToolStripMenuItem.Checked = false;
            balancedToolStripMenuItem.Checked = false;
            powerSaverToolStripMenuItem.Checked = true;
        }
    }

    public class PowerPlan
    {
        public readonly string name;
        public Guid guid;

        public PowerPlan(string name, Guid guid)
        {
            this.name = name;
            this.guid = guid;
        }
    }

    public interface IPowerManager
    {
        /// <returns>
        /// All supported power plans.
        /// </returns>
        List<PowerPlan> GetPlans();

        bool IsCharging();

        PowerPlan GetCurrentPlan();

        /// <returns>Battery charge value in percent, 
        /// i.e. values in a 0..100 range</returns>
        int GetChargeValue();

        void SetActive(PowerPlan plan);

        /// <summary>
        /// Opens Power Options section of the Control Panel.
        /// </summary>
        void OpenControlPanel();
    }

    public class PowerManagerProvider
    {
        public static IPowerManager CreatePowerManager()
        {
            return new PowerManager();
        }
    }

    class PowerManager : IPowerManager
    {
        /// <summary>
        /// Indicates that almost no power savings measures will be used.
        /// </summary>
        private readonly PowerPlan MaximumPerformance;

        /// <summary>
        /// Indicates that fairly aggressive power savings measures will be used.
        /// </summary>
        private readonly PowerPlan Balanced;

        /// <summary>
        /// Indicates that very aggressive power savings measures will be used to help
        /// stretch battery life.                                                     
        /// </summary>
        private readonly PowerPlan PowerSourceOptimized;


        public PowerManager()
        {

            // See GUID values in WinNT.h.
            MaximumPerformance = NewPlan("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");
            Balanced = NewPlan("381b4222-f694-41f0-9685-ff5bb260df2e");
            PowerSourceOptimized = NewPlan("a1841308-3541-4fab-bc81-f71556f20b4a");

            // Add handler for power mode state changing.
            Microsoft.Win32.SystemEvents.PowerModeChanged += new Microsoft.Win32.PowerModeChangedEventHandler(PowerModeChangedHandler);
        }

        private PowerPlan NewPlan(string guidString)
        {
            Guid guid = new Guid(guidString);
            return new PowerPlan(GetPowerPlanName(guid), guid);
        }

        public void SetActive(PowerPlan plan)
        {
            PowerSetActiveScheme(IntPtr.Zero, ref plan.guid);
        }

        /// <returns>
        /// All supported power plans.
        /// </returns>
        public List<PowerPlan> GetPlans()
        {
            return new List<PowerPlan>(new PowerPlan[] {
                MaximumPerformance,
                Balanced,
                PowerSourceOptimized
            });
        }

        public bool IsCharging()
        {
            PowerStatus pwrStatus = SystemInformation.PowerStatus;
            return pwrStatus.PowerLineStatus == PowerLineStatus.Online;
        }

        public int GetChargeValue()
        {
            PowerStatus pwrStatus = SystemInformation.PowerStatus;
            return (int)(pwrStatus.BatteryLifePercent * 100);
        }

        private Guid GetActiveGuid()
        {
            Guid ActiveScheme = Guid.Empty;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
            if (PowerGetActiveScheme((IntPtr)null, out ptr) == 0)
            {
                ActiveScheme = (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
                if (ptr != null)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
            return ActiveScheme;
        }

        public PowerPlan GetCurrentPlan()
        {
            Guid guid = GetActiveGuid();
            return GetPlans().Find(p => (p.guid == guid));
        }

        private void PowerModeChangedHandler(object sender, EventArgs e)
        {
            if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online)
            {
                SetActive(MaximumPerformance);
            }
            else if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
            {
                SetActive(PowerSourceOptimized);
            }
        }

        private static string GetPowerPlanName(Guid guid)
        {
            string name = string.Empty;
            IntPtr lpszName = (IntPtr)null;
            uint dwSize = 0;

            PowerReadFriendlyName((IntPtr)null, ref guid, (IntPtr)null, (IntPtr)null, lpszName, ref dwSize);
            if (dwSize > 0)
            {
                lpszName = Marshal.AllocHGlobal((int)dwSize);
                if (0 == PowerReadFriendlyName((IntPtr)null, ref guid, (IntPtr)null, (IntPtr)null, lpszName, ref dwSize))
                {
                    name = Marshal.PtrToStringUni(lpszName);
                }
                if (lpszName != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpszName);
            }

            return name;
        }

        /// <summary>
        /// Opens Power Options section of the Control Panel.
        /// </summary>
        public void OpenControlPanel()
        {
            var root = Environment.GetEnvironmentVariable("SystemRoot");
            Process.Start(root + "\\system32\\control.exe", "/name Microsoft.PowerOptions");
        }

        #region DLL imports

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int GetSystemDefaultLCID();

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerSetActiveScheme")]
        public static extern uint PowerSetActiveScheme(IntPtr UserPowerKey, ref Guid ActivePolicyGuid);

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerGetActiveScheme")]
        public static extern uint PowerGetActiveScheme(IntPtr UserPowerKey, out IntPtr ActivePolicyGuid);

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerReadFriendlyName")]
        public static extern uint PowerReadFriendlyName(IntPtr RootPowerKey, ref Guid SchemeGuid, IntPtr SubGroupOfPowerSettingsGuid, IntPtr PowerSettingGuid, IntPtr Buffer, ref uint BufferSize);

        #endregion
    }
}

