using System;

public class Money
{
    public int Value => _value;
    public Action<int> MoneyChanged;

    private int _value;

    public Money(int startValue)
    {
        _value = startValue;
    }

    public void Increase()
    {
        _value++;
        MoneyChanged?.Invoke(_value);
    }
    public void Add(int value)
    {
        if (value < 0) 
            throw new SystemException("Нельзя прибавлять отрицательное число!");

        _value += value;
        MoneyChanged?.Invoke(_value);
    }
    public bool TrySubtract(int value)
    {
        if (value < 0)
            throw new SystemException("Нельзя вычитать отрицательное число!");

        if (value > _value) 
            return false;
        else
        {
            _value -= value;
            MoneyChanged?.Invoke(_value);
            return true;
        }
    }
}
