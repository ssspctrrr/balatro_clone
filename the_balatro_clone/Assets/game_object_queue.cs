using System;
using UnityEngine;
using System.Collections.Generic;

public class GameObjectQueue<T>
{
    private List<T> list = new List<T>();

    public void enqueue(T item)
    {
        list.Add(item);
    }

    public T dequeue()
    {
        T item_dequeued = list[0];
        list.RemoveAt(0);
        return item_dequeued;
    }

    public T peek()
    {
        return list[0];
    }
    
    public int size()
    {
        return list.Count;
    }

    public bool is_empty()
    {
        if (list.Count == 0)
            return true;
        return false;
    }
}