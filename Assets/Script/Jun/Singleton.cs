using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new() // ���� ����, �����ڿ� �Ű������� ���� Ÿ������ ������ �Ǵ�. 
{
    public static T Instance // �������� �� �ڽ�.
    {
        get;
        private set;
    }

    static Singleton()
    {
        if (Instance == null) // ������ �� �������� ������ Instance ������ ���� üũ.
        {
            Instance = new T(); // ������ ���� -> ���������� �ϳ��ۿ� �������� ����. 
        }
    }

    public virtual void Clear()
    {
        Instance = null;
        Instance = new T();
    }


}
