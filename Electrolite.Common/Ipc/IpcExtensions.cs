/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using JKang.IpcServiceFramework;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Electrolite.Common.Ipc
{
    public static class IpcExtensions
    {
        public static async Task<Order> WrapOrderAsync(Func<Task> action)
        {
            try
            {
                await action();
                return new Order
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new Order
                {
                    Success = false,
                    UserErrorMessage = e.Message
                };
            }
        }

        public static async Task<Order<TResult>> WrapOrderAsync<TResult>(Func<Task<TResult>> action)
        {
            try
            {
                var result = await action();
                return new Order<TResult>
                {
                    Success = true,
                    Value = result
                };
            }
            catch (Exception e)
            {
                return new Order<TResult>
                {
                    Success = false,
                    UserErrorMessage = e.Message
                };
            }
        }

        public static Order WrapOrder(Action action)
        {
            try
            {
                action();
                return new Order
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new Order
                {
                    Success = false,
                    UserErrorMessage = e.Message
                };
            }
        }

        public static Order<TResult> WrapOrder<TResult>(Func<TResult> action)
        {
            try
            {
                var result = action();
                return new Order<TResult>
                {
                    Success = true,
                    Value = result
                };
            }
            catch (Exception e)
            {
                return new Order<TResult>
                {
                    Success = false,
                    UserErrorMessage = e.Message
                };
            }
        }

        public static async Task OrderAsync<TInterface>(
            this IpcServiceClient<TInterface> client,
            Expression<Func<TInterface, Order>> action) where TInterface : class
        {
            var result = await client.InvokeAsync(action);
            if (!result.Success)
            {
                throw new InvalidOperationException(result.UserErrorMessage);
            }
        }

        public static async Task<TResult> OrderAsync<TInterface, TResult>(
            this IpcServiceClient<TInterface> client,
            Expression<Func<TInterface, Order<TResult>>> action) where TInterface : class
        {
            var result = await client.InvokeAsync(action);
            if (result.Success)
            {
                return result.Value;
            }
            else
            {
                throw new InvalidOperationException(result.UserErrorMessage);
            }
        }
    }
}