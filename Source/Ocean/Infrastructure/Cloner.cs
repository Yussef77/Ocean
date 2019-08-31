﻿namespace Oceanware.Ocean.Infrastructure {

    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Class Cloner.
    /// </summary>
    public static class Cloner {

        /// <summary>
        /// Initializes the <see cref="Cloner"/> class.
        /// </summary>
        static Cloner() { }

        /// <summary>
        /// Deep copy serializable object.
        /// </summary>
        /// <typeparam name="T">Type to create deep copy of.</typeparam>
        /// <param name="toClone">The object instance to clone.</param>
        /// <returns>Deep copy instance of <typeparamref name="T"/>.</returns>
        public static T DeepCopy<T>(Object toClone) {
            if (toClone == null) {
                return default;
            }
            using (var ms = new MemoryStream()) {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, toClone);
                ms.Position = 0;
                return (T)bf.Deserialize(ms);
            }
        }
    }
}
