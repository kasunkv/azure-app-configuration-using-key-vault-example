namespace MusicStore.Shared
{
    public class AppSettings
    {
        public Discount Discount { get; set; }
        public Identity Identity { get; set; }
    }

    public class Discount
    {
        public double Amount { get; set; }
    }

    public class Identity
    {
        public string Secret { get; set; }
    }

}
