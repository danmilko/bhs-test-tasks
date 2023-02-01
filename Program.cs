using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace task1
{
    public class MyList<T> : IEnumerable<T>
    {
        private T[] buf;
        private int capacity;
        private int size;

        private const int minCap = 4;
        public int Count => size;

        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEnum<T>(buf, size);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }

        public MyList()
        {
            capacity = minCap;
            buf = new T[capacity];
            size = 0;
        }

        public MyList(params T[] input)
        {
            for (int i = 0; i < input.Length; i++) 
            {
                Add(input[i]);
            }
        }

        private void CheckForResize()
        {
            if (size <= (capacity << 1) / 5 && capacity > minCap)
            {
                Resize(capacity >> 1);
            }
            else if (size + 1 == capacity)
            {
                Resize(capacity << 1);
            }
        }

        private void Resize(int newCap)
        {
            while (size > newCap)
            {
                newCap = newCap << 1;
            }
            T[] newBuf = new T[newCap];
            for (int i = 0; i < size; i++)
            {
                newBuf[i] = buf[i];
            }
            capacity = newCap;
            buf = newBuf;
        }

        public void Add(T item)
        {
            CheckForResize();
            buf[size] = item;
            size++;
        }

        public void Clear()
        {
            buf = new T[minCap];
            capacity = minCap;
            size = 0;
        }

        public void RemoveAt(int pos)
        {
            if (pos < 0 || pos >= size)
            {
                throw new ArgumentOutOfRangeException($"Argument out of bounds: arg: {pos}, size: {size}");
            }
            for (int i = pos; i < size; i++)
            {
                buf[i] = buf[i + 1];
            }
            size -= 1;
            CheckForResize();
        }

        public void Insert(int pos, T item)
        {
            CheckForResize();
            for (int i = size - 1; i > pos; i--)
            {
                buf[i] = buf[i - 1];
            }
            buf[pos] = item;
            size++;
        }

        public T this[int pos]
        {
            get { return (pos < 0 || pos >= size) ? throw new ArgumentOutOfRangeException($"Argument out of bounds: arg: {pos}, size: {size}") : buf[pos]; }
            set 
            { 
                if (pos < 0 || pos >= size) 
                { 
                    throw new ArgumentOutOfRangeException(); 
                } 
                else 
                { 
                    buf[pos] = value; 
                } 
            }
        }

        public MyList<T> Where(Func<T, bool> func)
        {
            MyList<T> resultList = new MyList<T>();
            for (int i = 0; i < size; i++) 
            {
                if (func(buf[i])) 
                {
                    resultList.Add(buf[i]);
                }
            }
            return resultList;
        }

        public MyList<TResult> Select<TResult>(Func<T, TResult> func)
        {
            MyList<TResult> resultList = new MyList<TResult>();
            for (int i = 0; i < size; i++)
            {
                resultList.Add(func(buf[i]));
            }
            return resultList;
        }

        public List<T> ToList()
        {
            List<T> resultList = new List<T>();
            for (int i = 0; i < size; i++)
            {
                resultList.Add(buf[i]);
            }
            return resultList;
        }
    }
    class MyListEnum<T> : IEnumerator<T>
    {
        private T[] buf;
        private int size;
        private int pos; // position

        public MyListEnum(T[] _buf, int _size)
        {
            buf = _buf;
            size = _size;
            pos = -1; //starts with -1, because foreach calls MoveNext first
        }

        public T Current => (pos == -1 || pos >= size) ? throw new ArgumentOutOfRangeException() : buf[pos];

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {

        }

        public bool MoveNext() => ++pos >= size ? false : true;

        public void Reset() => pos = -1;
    }

    class Student
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Group { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var students = new MyList<Student>
            {
                new Student { FirstName="Илья", LastName="Петров", Group="БИТ201" },
                new Student { FirstName="Пётр", LastName="Ильин", Group="БИВ192" },
                new Student { FirstName="Софья", LastName="Козлова", Group="БПМ191"},
                new Student { FirstName="Мария", LastName="Степанова", Group="БПИ194"},
                new Student { FirstName="Николай", LastName="Кошкин", Group="БИБ202"},
                new Student { FirstName="Владимир", LastName="Крутой", Group="БИТ201"},
                new Student { FirstName="Александра",LastName="Милая", Group="БИВ192" }
            };
            var result = students.
            Where(z => z.Group == "БИВ192").
            Select(z => z.FirstName).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i]);
            }
            Console.ReadKey();
        }
    }

}


