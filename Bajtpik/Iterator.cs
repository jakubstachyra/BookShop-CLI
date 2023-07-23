using System.Collections;

namespace Bajtpik
{
    public interface ICollections<T>
    {
        public abstract class Iterator : IEnumerator<T>
        {
            T IEnumerator<T>.Current => Current();
            object IEnumerator.Current => Current();

            public abstract T Current();
            public abstract bool MoveNext();
            public abstract void Reset();
            public void Dispose() { }
        }
        Iterator ForwardIterator();
        Iterator ReverseIterator();
    }

    public class DoublyLinkedList<T> : ICollections<T>
    {
        private class Node
        {
            public T data;
            public Node next;
            public Node prev;

            public Node(T data, Node next = null, Node prev = null)
            {
                this.data = data;
                this.next = next;
                this.prev = prev;
            }
        }

        private Node head;
        private Node tail;


        public DoublyLinkedList()
        {
            head = null;
            tail = null;
        }

        public void Add(T val)
        {
            Node newNode = new Node(val, null, tail);

            if (tail != null)
            {
                tail.next = newNode;
            }

            tail = newNode;

            head ??= tail;
        }

        public void Remove(IEnumerator<T> val)
        {
            Node current = (Node)val;
            if (current.prev == null)
            {
                head = current.next;
                if (head != null)
                {
                    head.prev = null;

                }
                else
                {
                    tail = null;

                }
            }
            else if (current.next == null)
            {
                tail = current.prev;
                tail.next = null;

            }
            else
            {
                current.prev.next = current.next;
                current.next.prev = current.prev;

            }
        }
        class DoublyLinkedListForwardIterator : ICollections<T>.Iterator
        {
            public Node current;
            private DoublyLinkedList<T> list;

            public DoublyLinkedListForwardIterator(DoublyLinkedList<T> list)
            {
                this.list = list;
                current = null;
            }

            public override T Current() => current.data;

            public override bool MoveNext()
            {
                if (current == null)
                {
                    if (list.head == null) return false;
                    current = list.head;
                    return true;

                }

                if (current.next == null) return false;
                current = current.next;
                return true;
            }

            public override void Reset()
            {
                current = null;
            }
        }
        public ICollections<T>.Iterator ForwardIterator() =>
             new DoublyLinkedListForwardIterator(this);

        class DoublyLinkedListReverseIterator : ICollections<T>.Iterator
        {
            public Node current;
            private DoublyLinkedList<T> list;

            public DoublyLinkedListReverseIterator(DoublyLinkedList<T> list)
            {
                this.list = list;
                current = null;
            }

            public override T Current() => current.data;

            public override bool MoveNext()
            {
                if (current == null)
                {
                    if (list.tail == null) return false;
                    current = list.tail;
                    return true;

                }

                if (current.prev == null) return false;
                current = current.prev;
                return true;
            }

            public override void Reset()
            {
                current = null;
            }

        }

        public ICollections<T>.Iterator ReverseIterator() =>
            new DoublyLinkedListReverseIterator(this);
    }

    public class Vector<T> : ICollections<T>
    {
        private T[] items;
        private int count;



        public Vector()
        {
            items = new T[4];
            count = 0;
        }

        public void Add(T item)
        {
            if (count == items.Length)
            {
                Array.Resize(ref items, items.Length * 2);
            }

            items[count] = item;
            count++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);

            if (index < 0) return false;
            RemoveAt(index);
            return true;

        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            count--;
            items[count] = default(T);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }
        public class VectorForwardIterator : ICollections<T>.Iterator
        {
            private int current;
            private Vector<T> list;

            public VectorForwardIterator(Vector<T> list)
            {
                this.list = list;
                current = -1;
            }

            public override T Current() => list.items[current];

            public override bool MoveNext()
            {
                if (current == -1)
                {
                    if (list.count <= 0) return false;
                    current = 0;
                    return true;

                }

                if (current >= list.count - 1) return false;
                current++;
                return true;
            }

            public override void Reset()
            {
                current = -1;
            }
        }
        public ICollections<T>.Iterator ForwardIterator() =>
             new VectorForwardIterator(this);
        public class VectorReverseIterator : ICollections<T>.Iterator
        {
            private int current;
            private Vector<T> list;

            public VectorReverseIterator(Vector<T> list)
            {
                this.list = list;
                current = list.count;
            }

            public override T Current() => list.items[current];

            public override bool MoveNext()
            {
                if (current == list.count)
                {
                    if (list.count <= 0) return false;
                    current = list.count - 1;
                    return true;

                }

                if (current <= 0) return false;
                current--;
                return true;
            }

            public override void Reset()
            {
                current = list.count;
            }
        }

        public ICollections<T>.Iterator ReverseIterator() =>
            new VectorReverseIterator(this);
    }

    public static class Algorithms
    {
        public static object Find<T>(ICollections<T> collection, Func<T, bool> predicate, bool direction)
        {
            IEnumerator<T> iterator = direction ? collection.ForwardIterator() : collection.ReverseIterator();

            while (iterator.MoveNext())
            {
                if (predicate(iterator.Current)) return iterator.Current;
            }
            return null;
        }

        public static void Print<T>(ICollections<T> collection, Func<T, bool> predicate, bool direction)
        {
            IEnumerator<T> iterator = direction ? collection.ForwardIterator() : collection.ReverseIterator();

            while (iterator.MoveNext())
            {
                if (predicate(iterator.Current)) Console.WriteLine(iterator.Current);
            }
        }
    }
}
