namespace BlissApp.Utility
{
    public static class UnixTime
    {
        public static int GetCurrentTime()
        {
            DateTime dateTime = new(1970, 1, 1);
            return (int)((DateTime.Now.ToUniversalTime() - dateTime).TotalSeconds + 0.5);
        }
    }
}
