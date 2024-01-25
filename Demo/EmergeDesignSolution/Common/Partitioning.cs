using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Common
{
    class Partitioning<T> : IEnumerable<Partition<T>>
    {
        private IEnumerable<Partition<T>> Partitions { get; }

        public Partitioning(params Partition<T>[] partitions)
            : this((IEnumerable<Partition<T>>)partitions)
        {
        }

        public Partitioning(IEnumerable<Partition<T>> partitions)
        {
            this.Partitions = partitions.ToList();
        }

        public IEnumerator<Partition<T>> GetEnumerator() =>
            this.Partitions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();

        public IEnumerable<Partitioning<T>> Expand()
        {
            (IEnumerable<Partition<T>> prefix, Partition<T> last) = this.Partitions.ExtractLast();
            return this.Expand(prefix.ToList(), last);
        }

        private IEnumerable<Partitioning<T>> Expand(List<Partition<T>> prefix, Partition<T> last) =>
            last.SplitAscending()
                .Where(tuple => tuple.left.Any() && tuple.right.Any())
                .Select(tuple => new[] {tuple.left, tuple.right})
                .Select(expansion => prefix.Concat(expansion))
                .Select(partitions => new Partitioning<T>(partitions));
    }
}