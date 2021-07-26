using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InHouse.Application.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class InHouseNotFoundException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public InHouseNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public InHouseNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="key">The key<see cref="object"/>.</param>
        public InHouseNotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
