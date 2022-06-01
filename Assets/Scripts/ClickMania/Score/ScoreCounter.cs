using System;
using UnityEngine;

namespace ClickMania.Score
{
    public class ScoreCounter : IScore
    {
        private readonly IMultiplier _scoreMultiplier;
        private readonly int _blockCost;

        public int Value { get; private set; }

        public ScoreCounter(IMultiplier scoreMultiplier, int blockCost)
        {
            _scoreMultiplier = scoreMultiplier;
            _blockCost = blockCost;
        }

        public void Add(int blockCount)
        {
            var score = _blockCost * blockCount;
            Value += score * _scoreMultiplier.Value;
        }

        public void Reset()
        {
            Value = 0;
        }
    }
}