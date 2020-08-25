using System;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete
{
    public class ATM : IATM
    {
        private ICard _insertedCard = null;

        public ATMState State;

        public ATM()
        {
            // Transition to Ready state.
            this.State = ATMState.Ready;
        }
        
        public void InsertCard(ICard card)
        {
            this._insertedCard = card;

            // Transition to WaitingForPin state.
            this.State = ATMState.WaitingForPin;
            string pinCode = this.WaitForPinInput();
            
            
            
            
        }

        private void SpitOutCard()
        {
            this._insertedCard = null;
            this.State = ATMState.Ready;
        }

        public bool IsCardInserted()
        {
            return _insertedCard != null;
        }

        public string WaitForPinInput()
        {
            Console.WriteLine("Please enter pin-code for the Card.");
            return Console.ReadLine();
        }

        public decimal RequestMoney(decimal money)
        {
            throw new System.NotImplementedException();
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