using System.Threading;
Thread thread1 = new Thread(()=>{
    for (int i = 1; i <= 10; i++)
    {
        Console.WriteLine("Thread 1 is running ... " + i);
        Thread.Sleep(1000);
    }
});

Thread thread2 = new Thread(() => {
    for (int i = 10; i >= 0; i--)
    {
        Console.WriteLine("Thread 2 is running ... " + i);
        Thread.Sleep(1000);
    }
});
thread1.Start();
thread2.Start();

//normal loops behavior.
loops();

void loops(){
    Thread.Sleep(11000);
    Console.WriteLine("##################################################################################################################");
for (int i = 1; i <= 10; i++)
{
        Console.WriteLine("loop 1 is running ... " + i);
}
for (int i = 10; i >= 0; i--)
    {
        Console.WriteLine("loop 2 is running ... " + i);
    }
}