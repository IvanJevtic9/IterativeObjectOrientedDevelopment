using System.Collections.Generic;
using System.Linq;

namespace Demo.Common
{
    static class Partitionings
    {
        public static Partitionings<T> Of<T>(IEnumerable<T> sequence) =>
            new Partitionings<T>(sequence);
    }

    class Partitionings<T>
    {
        private IEnumerable<T> EntireSequence { get; }

        public Partitionings(IEnumerable<T> data)
        {
            this.EntireSequence = data.ToList();
        }

        public IEnumerable<Partitioning<T>> All() =>
            new Partitioning<T>(this.EntireSequence.AsPartition())
                .ExpandEndlessly(partitioning => partitioning.Expand());
    }
}