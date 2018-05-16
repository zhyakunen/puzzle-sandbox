using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonRandomTable
{
    public int tableStack, index, max, size;
    int[] table;
    public CommonRandomTable(int stack, int randomMax)
    {
        tableStack = stack;
        max = randomMax;
        GenTable();
        Shuffle();
    }

    public void GenTable()
    {
        size = tableStack * max;
        table = new int[size];
        for (int i = 0; i < size; i++)
        {
            table[i] = i % max;
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < size; i++)
        {
            int t = table[i];
            int r = Random.Range(0, size);
            table[i] = table[r];
            table[r] = t;
        }
        index = 0;
    }

    public int Get()
    {
        int r = table[index];
        index++;
        if (index >= size) Shuffle();
        return r;
    }

}
