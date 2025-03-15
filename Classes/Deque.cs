using System;
using System.Collections;
using System.Collections.Generic;

public class Deque<T> : IEnumerable<T> where T : class  // T 限定为引用类型
{
    private LinkedList<T> list = new LinkedList<T>();

    // 从队尾添加元素
    public void AddLast(T item)
    {
        list.AddLast(item);
    }

    // 从队头添加元素
    public void AddFirst(T item)
    {
        list.AddFirst(item);
    }

    // 从队头移除元素，返回 null 如果为空
    public T RemoveFirst()
    {
        if (list.Count == 0)
            return null;  // 空时返回 null
        T value = list.First.Value;
        list.RemoveFirst();
        return value;
    }

    // 从队尾移除元素，返回 null 如果为空
    public T RemoveLast()
    {
        if (list.Count == 0)
            return null;  // 空时返回 null
        T value = list.Last.Value;
        list.RemoveLast();
        return value;
    }

    // 查看队头元素，返回 null 如果为空
    public T PeekFirst()
    {
        if (list.Count == 0)
            return null;  // 空时返回 null
        return list.First.Value;
    }

    // 查看队尾元素，返回 null 如果为空
    public T PeekLast()
    {
        if (list.Count == 0)
            return null;  // 空时返回 null
        return list.Last.Value;
    }

    // 队列是否为空
    public bool IsEmpty()
    {
        return list.Count == 0;
    }

    // 队列中元素个数
    public int Count()
    {
        return list.Count;
    }

    // 实现 IEnumerable<T> 接口的 GetEnumerator 方法
    public IEnumerator<T> GetEnumerator()
    {
        return list.GetEnumerator();  // 返回 LinkedList 的枚举器
    }

    // 非泛型版本的 GetEnumerator (为了支持老式的 foreach)
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
