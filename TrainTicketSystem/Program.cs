using System;

namespace TrainTicketSystem
{
    internal class Program
    {
        static Train train = new Train(60, 15, 3);
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
