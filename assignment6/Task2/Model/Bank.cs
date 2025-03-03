using System;
using System.Collections.Generic;

public class BankQueue
{
    private Queue<int> tokenQueue;
    private int tokenNumber;

    public BankQueue()
    {
        tokenQueue = new Queue<int>();
        tokenNumber = 1;
    }

    public int GetNewToken()
    {
        tokenQueue.Enqueue(tokenNumber);
        return tokenNumber++;
    }

    public int? ServeCustomer()
    {
        if (tokenQueue.Count == 0)
            return null;

        return tokenQueue.Dequeue();
    }

    public int? CheckNextCustomer()
    {
        if (tokenQueue.Count == 0)
            return null;

        return tokenQueue.Peek();
    }

    public bool IsQueueEmpty()
    {
        return tokenQueue.Count == 0;
    }
}
