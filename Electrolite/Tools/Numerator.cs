/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

namespace Electrolite.Tools
{
    sealed class Numerator
    {
        readonly object _mylock;
        int _counter;

        public Numerator()
        {
            _mylock = new object();
            _counter = 0;
        }

        public int Numerate()
        {
            lock (_mylock)
            {
                _counter++;
                return _counter;
            }
        }
    }
}
