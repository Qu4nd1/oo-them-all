using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using SimulationBancomat.Display;
using SimulationBancomat.Features;
using static SimulationBancomat.Display.SuperConsole;
using static SimulationBancomat.Features.Withdrawal;
using static SimulationBancomat.Features.Verifications;

namespace SimulationBancomat
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]

        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public struct PasswordData
        {
            public bool passwordValidity;
            public int password;
            public int tempPassword;
            public const int PASSWORD_SCREEN_X = 40;
            public const int PASSWORD_SCREEN_Y = 16;
            public const int PASSWORD_LENGTH = 6;

            // Constructeur
            public PasswordData(bool validity, int pwd, int tempPwd)
            {
                passwordValidity = validity;
                password = pwd;
                tempPassword = tempPwd;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            PasswordData data = new PasswordData(false, 123456, 0);

            do
            {
                SuperConsole.Presentation();
                SuperConsole.PasswordVisualSuppression();
                SuperConsole.DrawNumPad(emptyButton, Verifications.keyChar);

                data.passwordValidity = Verifications.Code(ref data, emptyButton, Verifications.keyChar);

                if (data.passwordValidity == true)
                {
                    SuperConsole.Presentation();
                    Menu.MenuChoice(Menu.bancomatOptions, ref bankMoneyAmount, ref withdrawalMoneyLogs, withdrawalMoneyOptions);
                }
            } while (data.passwordValidity != true);

                Console.ReadLine();
        }
    }
}