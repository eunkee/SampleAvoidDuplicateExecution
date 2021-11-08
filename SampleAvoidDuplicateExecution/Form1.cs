using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace SampleAvoidDuplicateExecution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string productName = fvi.ProductName;
            //중복 실행 방지
            if (CheckAlreadyRunThisProcess(productName))
            {
                MessageBox.Show($"동일한 이름의 프로그램 {productName}이\r\n실행 되어 프로그램이 종료됩니다.");
                Close();
                return;
            }
        }

        //core
        private static bool CheckAlreadyRunThisProcess(string processName)
        {
            bool rslt = false;
            int processcount = 0;
            Process[] procs = Process.GetProcesses();
            foreach (Process aProc in procs)
            {
                if (aProc.ProcessName.ToString().Equals(processName))
                {
                    processcount++;
                    if (processcount > 1)
                    {
                        rslt = true;
                        break;
                    }
                }
            }
            return rslt;
        }
    }
}
