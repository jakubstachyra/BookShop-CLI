namespace Bajtpik
{
    public class Deque<T> : ICollections<T>
    {
        private class Node
        {
            public List<T> Vector { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }
            public Node(List<T> vector, Node next = null, Node prev = null)
            {
                Vector = vector;
                Next = next;
                Prev = prev;
            }
            public Node()
            {
                Vector = new List<T>(10);
                Next = null;
                Prev = null;
            }
        }

        private Node Head;
        private Node Tail;

        public Deque()
        {
            Head = null;
            Tail = null;
        }
        public void FrontInsertion(T item)
        {
            if (Head == null)
            {
                Head = new Node();
                Tail = Head;
            }
            if (Head.Vector.Count < 10)
            {
                Head.Vector.Insert(0, item);
            }
            else
            {
                Node NewNode = new Node();
                NewNode.Vector.Insert(0, item);
                NewNode.Next = Head;
                Head.Prev = NewNode;
                Head = NewNode;
            }
        }
        public void BackInsertion(T item)
        {
            if (Head == null)
            {
                Head = new Node();
                Tail = Head;
            }
            if (Tail.Vector.Count < 10)
            {
                Tail.Vector.Add(item);
            }
            else
            {
                Node NewNode = new Node();
                NewNode.Vector.Add(item);
                NewNode.Prev = Tail;
                Tail.Next = NewNode;
                Tail = NewNode;
            }
        }
        public void FrontDelection()
        {
            if (Head.Vector.Count > 0) Head.Vector.RemoveAt(0);
        }
        public void BackDelection()
        {
            if (Tail.Vector.Count > 0) Tail.Vector.RemoveAt(Tail.Vector.Count - 1);
        }

        class DequeForwardIterator : ICollections<T>.Iterator
        {
            public int i = 0;
            public Node current;
            public Deque<T> Deque;

            public DequeForwardIterator(Deque<T> Deque)
            {
                current = null;
                this.Deque = Deque;
            }

            public override void Reset()
            {
                current = null;
                i = 0;
            }
            public override T Current() => current.Vector[i];

            public override bool MoveNext()
            {
                if (current == null)
                {
                    if (Deque.Head == null) return false;
                    current = Deque.Head;
                    return true;
                }
                if (++i < current.Vector.Count)
                {
                    return true;
                }
                else
                {
                    if (current.Next == null) return false;
                    i = 0;
                    current = current.Next;
                    return true;
                }
            }

        }
        public ICollections<T>.Iterator ForwardIterator() => new DequeForwardIterator(this);

        class DequeReverseIterator : ICollections<T>.Iterator
        {
            public int i;
            public Node current;
            public Deque<T> Deque;

            public DequeReverseIterator(Deque<T> Deque)
            {
                current = null;
                this.Deque = Deque;
            }

            public override void Reset()
            {
                current = null;
                i = 0;
            }
            public override T Current() => current.Vector[i];

            public override bool MoveNext()
            {
                if (current == null)
                {
                    if (Deque.Head == null) return false;
                    current = Deque.Tail;
                    i = current.Vector.Count - 1;
                    return true;
                }
                if (i-- > 0)
                {
                    return true;
                }
                else
                {
                    if (current.Prev == null) return false;
                    current = current.Prev;
                    i = current.Vector.Count - 1;
                    return true;
                }
            }
        }

        public ICollections<T>.Iterator ReverseIterator() => new DequeReverseIterator(this);

        public static object Find<T>(ICollections<T>.Iterator it, Predicate<T> predicate)
        {
            while (it.MoveNext())
            {
                if (predicate(it.Current())) return it.Current();
            }
            return null;
        }
        public static void ForEach<T>(ICollections<T>.Iterator it, Action<T> action)
        {
            while (it.MoveNext())
            {
                action(it.Current());
            }
        }
        public static int CountIf(ICollections<T>.Iterator it, Predicate<T> predicate)
        {
            int i = 0;
            while (it.MoveNext())
            {
                if (predicate(it.Current())) i++;
            }
            return i;
        }

    }

}
