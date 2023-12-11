using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Domain.Abstractions.Messaging
{
    /// <summary>
    /// Represents the command interface.
    /// </summary>
    /// <typeparam name="TResponse">The command response type.</typeparam>
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
