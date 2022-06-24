namespace Mgame.Persistence.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string NickName { get; set; }
        public string Host { get; set; }
        public string DeviceName { get; set; }
    }
}
