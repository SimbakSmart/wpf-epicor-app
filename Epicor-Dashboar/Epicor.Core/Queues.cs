namespace Epicor.Core
{


    public class Queues
    {
        public string Name { get; set; }
        public int Total { get; set; }
        public string Urgency { get; set; }
        public string Priority{ get; set; }

        private Queues(QueuesBuilder builder)
        {
            Name = builder.Name;
            Total = builder.Total;
            Urgency = builder.Urgency;
            Priority = builder.Priority;
        }

        public class QueuesBuilder
        {
            public string Name { get; private set; }
            public int Total { get; private set; }
            public string Urgency { get; private set; }
            public string Priority { get; set; }

            public QueuesBuilder WithName(string name)
            {
                Name = name;
                return this;
            }

            public QueuesBuilder WithTotal(int total)
            {
                Total = total;
                return this;
            }

            public QueuesBuilder WithUrgency(string urgency)
            {
                Urgency = urgency;
                return this;
            }

            public QueuesBuilder WithPriority(string priority)
            {
                Priority = priority;
                return this;
            }

            public Queues Build()
            {
                return new Queues(this);
            }
        }
    }

}
