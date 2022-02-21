using System;

namespace TrainTicketSystem
{
    internal class Program
    {
        static Train train = new Train();
        static void Main(string[] args)
        {
            StartTicketProgram();
            Console.Read();
        }

        static public void StartTicketProgram()
        {
            train.PrintSeatList();
        }
    }
}
