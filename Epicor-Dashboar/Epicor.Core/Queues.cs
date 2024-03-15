namespace Epicor.Core
{
    public class Queues
    {
       

        public string Name { get; set; }
        public int Total { get; set; }

        public Queues(string name, int total)
        {
            Name = name;
            Total = total;
        }


    }
}
