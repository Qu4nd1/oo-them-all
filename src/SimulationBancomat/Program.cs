using System;

namespace SimulationBancomat
{
    class Program
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Presentation();
            Console.ReadLine();
        }

        static void Presentation()
        {
            Console.Write("\t@@@@@@@@@@@         @@@@@        @@@@     @@@@@@@@@@    @@@@@@@@@@    @@@@@@@@@@    @@@@      @@@@@@@@@    @@@@@@@@@@    @@@@@      @@@\n" +
                          "\t@@@    @@@@@        @@@@@        @@@@     @@@@ @@@@@    @@@@ @@@@     @@@@ @@@@     @@@@     @@@@    @     @@@@@@@@@@     @@@@@     @@@\n" +
                          "\t@@@      @@@@      @@@@@@@       @@@@     @@@@          @@@@          @@@@          @@@@    @@@@           @@@@           @@@@@@@   @@@\n" +
                          "\t@@@      @@@@      @@@ @@@@      @@@@     @@@@          @@@@          @@@@          @@@@    @@@@@          @@@@           @@@@@@@@  @@@\n" +
                          "\t@@@   @@@@@@      @@@  @@@@      @@@@     @@@@@@@@@     @@@@@@@@@     @@@@@@@@@     @@@@     @@@@@@@       @@@@@@@@@      @@@  @@@@ @@@\n" +
                          "\t@@@  @@@@        @@@@   @@@@     @@@@     @@@@@@@@@     @@@@@@@@@     @@@@@@@@      @@@@        @@@@@@@    @@@@@@@@@      @@@   @@@@@@@\n" +
                          "\t@@@   @@@@       @@@     @@@     @@@@     @@@@          @@@@          @@@@          @@@@           @@@@    @@@@           @@@    @@@@@@\n" +
                          "\t@@@    @@@@     @@@@@@@@@@@@@    @@@@     @@@@          @@@@          @@@@          @@@@           @@@@    @@@@           @@@     @@@@@\n" +
                          "\t@@@    @@@@@    @@@      @@@@@   @@@@     @@@@          @@@@          @@@@ @@@@@    @@@@     @@   @@@@@    @@@@ @@@@@@    @@@      @@@@\n" +
                          "\t@@@    @@@@@  @@@@       @@@@   @@@@     @@@@          @@@@          @@@@@@@@@@    @@@@    @@@@@@@@@@     @@@@@@@@@@     @@@        @@\n");      


        }

        static void MenuChoice()
        {
            Console.Write("1: Transactions\n" +
                          "2: Information Bénéficiaire\n" +
                          "3: ");
        }

        static void Menu()
        {

        }

        static void VerifyCode()
        {

        }
        
        static void GetOutMoney()
        {

        }

        static void SeeAmount()
        {

        }

        static void PrintReceipt()
        {

        }
    }
}