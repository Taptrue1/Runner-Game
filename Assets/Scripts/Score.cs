using System;

namespace Assets.Scripts
{
    internal class Score
    {
        public Action<int> ScoreChanged;

        public int Value => _value;

        private int _value;

        public void Add(int value)
        {
            if (value < 0) throw new SystemException("Нельзя прибавлять отрицательное число");

            _value += value;

            ScoreChanged?.Invoke(_value);
        }
    }
}
