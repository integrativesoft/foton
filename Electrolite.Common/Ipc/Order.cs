/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

namespace Electrolite.Core.Ipc
{
    public class Order
    {
        public bool Success { get; set; }
        public string UserErrorMessage { get; set; }
    }

    public class Order<TResult> : Order
    {
        public TResult Value { get; set; }
    }
}
