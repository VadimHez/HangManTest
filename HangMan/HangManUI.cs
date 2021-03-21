using System;
using HangManServices;

namespace HangMan
{
    public class HangManUI : IHangManUI
    {
        private IHangManService _service;
        public HangManUI(IHangManService service)
        {
            _service = service;
        }
        public void StartApplication()
        {
            bool startNewGame = true;
            while (startNewGame)
            {
                Console.Clear();
                Console.WriteLine("Enter secret phrase to start game: ");
                var secret = Console.ReadLine();
                string entered = null;

                _service.SetSecret(secret);
                while (!_service.IsGameFinished())
                {
                    Console.Clear();
                    var model = _service.NextStep(String.IsNullOrEmpty(entered) ? null : entered[0]); 
                    ConsoleDrawMan(model.HangManStatus);
                    ConsoleDrawData(model);
                    ConsoleDrawResult(model);
                    entered = Console.ReadLine();
                    if (IsSomeErrors(model))
                        break;
                }

                Console.WriteLine("Do you want to start over? Y/N _");
                var result = Console.ReadLine();
                startNewGame = result?.ToUpper() == "Y";
            }
        }

        private void ConsoleDrawMan(HangManStatus status)
        {
            switch (status)
            {
                case HangManStatus.Clear:
                    Console.WriteLine(" _________________");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|_________________|");
                    break;
                case HangManStatus.Stick:
                    Console.WriteLine(" _________________");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|       _______   |");
                    Console.WriteLine("|       |      |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|   ___________|  |");
                    Console.WriteLine("|_________________|");
                    break;
                case HangManStatus.Head:
                    Console.WriteLine(" _________________");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|       _______   |");
                    Console.WriteLine("|       |      |  |");
                    Console.WriteLine("|       O      |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|   ___________|  |");
                    Console.WriteLine("|_________________|");
                    break;
                case HangManStatus.Body:
                    Console.WriteLine(" _________________");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|       _______   |");
                    Console.WriteLine("|       |      |  |");
                    Console.WriteLine("|       O      |  |");
                    Console.WriteLine("|       |      |  |");
                    Console.WriteLine("|       |      |  |");
                    Console.WriteLine("|       |      |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|   ___________|  |");
                    Console.WriteLine("|_________________|");
                    break;
                case HangManStatus.OneHand:
                    Console.WriteLine(" _________________");
                    Console.WriteLine("|                 |");
                    Console.WriteLine("|       _______   |");
                    Console.WriteLine("|       |      |  |");
                    Console.WriteLine("|       O      |  |");
                    Console.WriteLine("|      /|      |  |");
                    Console.WriteLine("|     / |      |  |");
                    Console.WriteLine("|       |      |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|              |  |");
                    Console.WriteLine("|   ___________|  |");
                    Console.WriteLine("|_________________|");
                    break;
                case HangManStatus.TwoHands:
                    Console.WriteLine(@" _________________");
                    Console.WriteLine(@"|                 |");
                    Console.WriteLine(@"|       _______   |");
                    Console.WriteLine(@"|       |      |  |");
                    Console.WriteLine(@"|       O      |  |");
                    Console.WriteLine(@"|      /|\     |  |");
                    Console.WriteLine(@"|     / | \    |  |");
                    Console.WriteLine(@"|       |      |  |");
                    Console.WriteLine(@"|              |  |");
                    Console.WriteLine(@"|              |  |");
                    Console.WriteLine(@"|              |  |");
                    Console.WriteLine(@"|   ___________|  |");
                    Console.WriteLine(@"|_________________|");
                    break;
                case HangManStatus.OneLeg:
                    Console.WriteLine(@" _________________");
                    Console.WriteLine(@"|                 |");
                    Console.WriteLine(@"|       _______   |");
                    Console.WriteLine(@"|       |      |  |");
                    Console.WriteLine(@"|       O      |  |");
                    Console.WriteLine(@"|      /|\     |  |");
                    Console.WriteLine(@"|     / | \    |  |");
                    Console.WriteLine(@"|       |      |  |");
                    Console.WriteLine(@"|      /       |  |");
                    Console.WriteLine(@"|     /        |  |");
                    Console.WriteLine(@"|              |  |");
                    Console.WriteLine(@"|   ___________|  |");
                    Console.WriteLine(@"|_________________|");
                    break;
                case HangManStatus.TwoLegs:
                    Console.WriteLine(@" _________________");
                    Console.WriteLine(@"|                 |");
                    Console.WriteLine(@"|       _______   |");
                    Console.WriteLine(@"|       |      |  |");
                    Console.WriteLine(@"|       O      |  |");
                    Console.WriteLine(@"|      /|\     |  |");
                    Console.WriteLine(@"|     / | \    |  |");
                    Console.WriteLine(@"|       |      |  |");
                    Console.WriteLine(@"|      / \     |  |");
                    Console.WriteLine(@"|     /   \    |  |");
                    Console.WriteLine(@"|              |  |");
                    Console.WriteLine(@"|   ___________|  |");
                    Console.WriteLine(@"|_________________|");
                    break;
            }
            
        }

        private void ConsoleDrawData(HangManModel model)
        {
            Console.WriteLine($"Secret Phrase: {string.Join(" ", model.AnswearChars)}");
            Console.WriteLine($"Your letters : {string.Join(" ", model.EnteredChars)}");
        }

        private void ConsoleDrawResult(HangManModel model)
        {
            switch (model.PlayerStatus)
            {
                case PlayerStatus.Lose:
                    Console.WriteLine("YOU LOSE");
                    break;
                case PlayerStatus.Win:
                    Console.WriteLine("YOU WIN");
                    break;
                default:
                    break;
            }
        }

        private bool IsSomeErrors(HangManModel model)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(model.Errors))
            {
                Console.WriteLine($"Something went wrong... : {model.Errors}");
                result = true;
            }
            return result;
        }
    }
}
