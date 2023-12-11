﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Domain.Abstractions.Messaging
{
    /// <summary>
    /// Represents the query interface.
    /// </summary>
    /// <typeparam name="TResponse">The query response type.</typeparam>
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
