using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectPool
{
    class Program
    {
        static void Main(string[] args)
        {
            var components = CreateComponents(3);

            TestObjectPool1(components);

            Console.WriteLine("");
            Console.WriteLine("");

            TestObjectPool2(components);

            Console.ReadKey();
        }

        static void TestObjectPool1(List<TestComponent> components)
        {
            Console.WriteLine("Testing ObjectPool1...");

            var pool = new ObjectPool1<TestComponent>();
            for(int i = 0; i < components.Count; i++)
            {
                var component = components[i];
                pool.Add(component);
            }

            Console.WriteLine(string.Format("Starting pool size is {0}", pool.GetCount()));

            for (int i = 0; i < pool.GetCount() * 2; i++)
            {
                if(pool.TryGetNext(out TestComponent customComponent))
                {
                    Console.WriteLine(string.Format("Next element is \"{0}\", ID={1}", customComponent.Name, customComponent.ID));
                }

                Console.WriteLine(string.Format("Current pool size is {0}", pool.GetCount()));
            }

            for (int i = 0; i < pool.GetCount() * 2; i++)
            {
                if (pool.TryGetPrevious(out TestComponent customComponent))
                {
                    Console.WriteLine(string.Format("Previous element is \"{0}\", ID={1}", customComponent.Name, customComponent.ID));
                }

                Console.WriteLine(string.Format("Current pool size is {0}", pool.GetCount()));
            }

            Console.WriteLine(string.Format("Ending pool size is {0}", pool.GetCount()));

            pool.Dispose();

            Console.WriteLine("End Testing ObjectPool1...");
        }

        static void TestObjectPool2(List<TestComponent> components)
        {
            Console.WriteLine("Testing ObjectPool2...");

            var pool = new ObjectPool2<TestComponent>();
            for (int i = 0; i < components.Count; i++)
            {
                var component = components[i];
                pool.Add(component);
            }

            Console.WriteLine(string.Format("Starting pool size is {0}", pool.GetCount()));

            for (int i = 0; i < 5; i++)
            {
                if (pool.TryTakeObject((uint)i, out TestComponent customComponent))
                {
                    Console.WriteLine(string.Format("Taking element \"{0}\", ID={1}", customComponent.Name, customComponent.ID));
                    Console.WriteLine(string.Format("Current pool size is {0}", pool.GetCount()));
                }
                else
                {
                    //Fail because object is not in the pool
                }
            }

            Console.WriteLine(string.Format("Ending pool size is {0}", pool.GetCount()));

            pool.Dispose();

            Console.WriteLine("End Testing ObjectPool2...");
        }

        private static List<TestComponent> CreateComponents(int quantity)
        {
            List<TestComponent> list = new List<TestComponent>();

            for(int i = 0; i < quantity; i++)
            {
                TestComponent cc = new TestComponent();
                cc.ID = Convert.ToUInt32(i);
                cc.Name = Guid.NewGuid().ToString();
                cc.IsEnabled = true;
                cc.UniqueData = Encoding.ASCII.GetBytes(cc.Name);

                list.Add(cc);
            }

            return list;
        }
    }
}
