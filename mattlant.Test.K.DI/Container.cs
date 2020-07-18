// (c) 2020 mattlant
// See LICENSE file for license information

using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace mattlant.Test.K.DI
{
    /// <summary>
    /// An interface for creating super simple DI containers!
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Adds a new <see cref="object"/> instance to the container..
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to associate with the passed in object.</param>
        /// <param name="item">The <see cref="object"/> to add to the container.</param>
        void Set(Type type, object item);        
        
        /// <summary>
        /// Adds a new instance of type <c>T</c> to the container..
        /// </summary>
        /// <param name="item">The instance to add to the container.</param>
        void Set<T>(T item);


        /// <summary>
        /// Gets an instance from the container.
        /// </summary>
        /// <returns>Returns an instance of type <c>T</c>.</returns>
        T? Get<T>() where T : class;

        /// <summary>
        /// Gets an <see cref="object"/> instance from the container.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> associated with an instance retrieve from the container.</param>
        /// <returns>Returns an <see cref="object"/> instance.</returns>
        object? Get(Type type);

        /// <summary>
        /// Gets the total number of items in this <see cref="IContainer"/> instance.
        /// </summary>
        int ItemCount { get; }
    }

    /// <summary>
    /// A super simple DI container.
    /// </summary>
    public class Container : IContainer
    {
        private static IContainer? _current;

        /// <summary>
        /// Gets the singleton Container for this AppDomain.
        /// </summary>
        public static IContainer Current => _current ??= new Container();

        private readonly Dictionary<Type, object?> _typeKeyDictionary = new Dictionary<Type, object?>();

        /// <summary>
        /// Allows for injecting a different implementation of the container.
        /// </summary>
        /// <param name="container"></param>
        /// <exception cref="ArgumentNullException">
        /// An <see cref="ArgumentNullException"/> is thrown if <paramref name="container"/> is a
        /// null reference.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// An <see cref="InvalidOperationException"/> is thrown if there is already an existing
        /// container that is not empty.
        /// </exception>
        public static void SetSingletonContainer(IContainer container)
        {
            if(!(_current is null) && _current.ItemCount != 0)
                throw new InvalidOperationException(
                    "Cannot assign a new container while the existing container has items in it.");

            _current = (Container) container ??
                       throw new ArgumentNullException(nameof(container), 
                        $"An instance of {nameof(IContainer)} instance is required.");
        }

        public void Set(Type type, object item) 
            => _typeKeyDictionary[type] = item;

        public void Set<T>(T item)
        {
            _typeKeyDictionary[typeof(T)] = item;
        }

        public T? Get<T>() where T : class 
            => Get(typeof(T)) as T;

        public object? Get(Type type) 
            => _typeKeyDictionary.ContainsKey(type) ? _typeKeyDictionary[type] : null;

        public int ItemCount => _typeKeyDictionary.Count;
    }
}
