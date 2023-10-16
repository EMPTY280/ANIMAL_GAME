using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new() // 참조 형식, 생성자에 매개변수가 없는 타입으로 제약을 건다. 
{
    public static T Instance // 정적으로 나 자신.
    {
        get;
        private set;
    }

    static Singleton()
    {
        if (Instance == null) // 생성할 시 정적으로 생성된 Instance 변수가 유무 체크.
        {
            Instance = new T(); // 없으면 생성 -> 정적변수는 하나밖에 존재하지 못함. 
        }
    }

    public virtual void Clear()
    {
        Instance = null;
        Instance = new T();
    }


}
