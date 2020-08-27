using System;
using System.IO;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete
{
    public class ATM : IATM
    {
        private ICard _insertedCard = null;

        private TextReader _reader = null;
        private TextWriter _writer = null;

        public ATMState State;

        public ATM(TextReader reader, TextWriter writer)
        {
            this._reader = reader;
            this._writer = writer;

            // Transition to Ready state.
            this.State = ATMState.Ready;
        }
        
        public void InsertCard(ICard card)
        {

            if(this.IsCardInserted())
            {
                throw new Exception("Cannot put in card, there is already a card in the machine.");
            }

            this._insertedCard = card;

            if(this._insertedCard == null)
            {
                return;
            }


            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
            
            
            _writer.WriteLine($"Inserted card:\r\n{card}");


            string pinCodeInput = this.WaitForPinInput();
            
            if(pinCodeInput == card.PinCode)
            {
                _writer.WriteLine("Correct pin entered!");
                _writer.WriteLine($"Current card balance: {card.Account.Balance}$");
                _writer.Write("Please type the amount you would like to withdraw: ");

                bool couldParseInputToDouble = false;
                decimal amountRequested = 0;

                while (!couldParseInputToDouble)
                {
                    string amountRequestedInput = _reader.ReadLine();
                    
                    
                    couldParseInputToDouble = decimal.TryParse(amountRequestedInput, out amountRequested);

                    if(!couldParseInputToDouble)
                        Console.WriteLine("Invalid input, i only support numerical values. American format (, = thousand seperator |||| . = digit seperator");
                }

                decimal withdrawn = RequestMoney(amountRequested);

                if(amountRequested != withdrawn)
                {
                    _writer.WriteLine($"Was not able to withdraw: {amountRequested}$");
                }

                _writer.WriteLine($"Withdrawn money: {withdrawn}$");
                _writer.WriteLine($"New balance: {card.Account.Balance}$");
    
            }else
            {
                _writer.WriteLine("INVALID PIN -- ENTER TO TRY AGAIN");
                _reader.ReadLine();
            }

            _reader.ReadLine();

            this.SpitOutCard();
        }

        private void SpitOutCard()
        {
            this._insertedCard = null;
            _writer.WriteLine("Spat out card.");
            this.State = ATMState.Ready;
        }

        public bool IsCardInserted()
        {
            return _insertedCard != null;
        }

        private string WaitForPinInput()
        {
            // Transition to WaitingForPin state.
            this.State = ATMState.WaitingForPin;
            _writer.Write("Please enter pin-code for the Card: ");
            return _reader.ReadLine();
        }

        public decimal RequestMoney(decimal money)
        {
            _writer.WriteLine($"ATM Attempting to withdraw {money}$ from the connected account...");
            return this._insertedCard.Withdraw(money);
        }
    }

    public enum ATMState
    {
        Disabled,
        Broken,
        Ready,
        WaitingForPin,
        InvalidPinEntered,
        CorrectPinEntered,
        ProcessingTransaction,
        TransactionProcessed
    }
}