using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace InHouse.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        /// <summary>
        /// The Mapping.
        /// </summary>
        /// <param name="profile">The profile<see cref="Profile"/>.</param>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
