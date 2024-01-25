using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Common
{
    class Partition<T> : IEnumerable<T>
    {
        private IEnumerable<T> Content { get; }

        public Partition(params T[] content)
            : this((IEnumerable<T>)content) { }

        public Partition(IEnumerable<T> content)
        {
            this.Content = content.ToList();
        }

        private static Partition<T> Empty => new Partition<T>(Enumerable.Empty<T>());

        public IEnumerable<(Partition<T> left, Partition<T> right)> Split() =>
            this.Split(this.Content);

        public IEnumerable<(Partition<T> left, Partition<T> right)> SplitAscending() =>
            this.Content.IsEmpty() ? new[] {(Partition<T>.Empty, Partition<T>.Empty)}
            : this.SplitAndPrependLeft(this.Content.First(), this.Content.Skip(1));

        private IEnumerable<(Partition<T> left, Partition<T> right)> Split(IEnumerable<T> sequence) =>
            sequence.IsEmpty() ? new[] {(Partition<T>.Empty, Partition<T>.Empty)}
            : this.Split(sequence.First(), sequence.Skip(1));

        private (Partition<T> left, Partition<T> right) Prepend(
            T leftHead, Partition<T> left, Partition<T> right) =>
            (new Partition<T>(new[] {leftHead}.Concat(left)), right);

        private IEnumerable<(Partition<T> left, Partition<T> right)> SplitAndPrependLeft(
            T leftHead, IEnumerable<T> toSplit) =>
            this.Split(toSplit)
                .Select(split => this.Prepend(leftHead, split.left, split.right));

        private IEnumerable<(Partition<T> left, Partition<T> right)> Split(
            T head, IEnumerable<T> tail) =>
            this.Combine(this.TrivialSplit(head), this.Split(tail).ToList());

        private IEnumerable<(Partition<T> left, Partition<T> right)> TrivialSplit(T obj) =>
            new[]
            {
                (new Partition<T>(obj), Partition<T>.Empty),
                (Partition<T>.Empty, new Partition<T>(obj))
            };

        private IEnumerable<(Partition<T> left, Partition<T> right)> Combine(
            IEnumerable<(Partition<T> left, Partition<T> right)> head,
            IEnumerable<(Partition<T> left, Partition<T> right)> tail) =>
            head.SelectMany(split => tail.Select(
                tuple => (split.left.Concat(tuple.left), split.right.Concat(tuple.right))));

        private Partition<T> Concat(Partition<T> other) =>
            new Partition<T>(this.Content.Concat(other.Content));

        public IEnumerator<T> GetEnumerator() =>
            this.Content.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}