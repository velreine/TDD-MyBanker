namespace MyBanker_Library.Interfaces
{
    public interface IATM // Automated Teller Machine
    {
        public void InsertCard(ICard card);
        //public void WaitForPinInput();
        public decimal RequestMoney(decimal money);

    }
}