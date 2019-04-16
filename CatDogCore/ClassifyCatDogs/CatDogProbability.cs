using System;
using System.Collections.Generic;
using System.Text;

namespace ClassifyCatDogs
{
    public class CatDogProbability
    {
        private double _catProbabilitySum = 0;

        private int _catProbabilityCount = 0;

        private double _dogProbabilitySum = 0;

        private int _dogProbabilityCount = 0;

        public void AddCatValue(double probability)
        {
            _catProbabilitySum += probability;
            _catProbabilityCount++;
        }

        public void AddDogValue(double probability)
        {
            _dogProbabilitySum += probability;
            _dogProbabilityCount++;
        }

        public double GetCatValue()
        {
            if (_catProbabilityCount == 0)
                return 0;
            return _catProbabilitySum / _catProbabilityCount;
        }

        public double GetDogValue()
        {
            if (_dogProbabilityCount == 0)
                return 0;
            return _dogProbabilitySum / _dogProbabilityCount;
        }
    }
}
