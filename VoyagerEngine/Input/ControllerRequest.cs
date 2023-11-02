using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Input
{
    internal interface IControllerRequest
    {
        internal Type ControllerType { get; }
        internal EntityId EntityId { get; }
    }
    internal struct ControllerRequest<T> : IControllerRequest where T : Controller
    {
        Type IControllerRequest.ControllerType => typeof(T);
        EntityId IControllerRequest.EntityId => EntityId;

        internal EntityId EntityId;
    }
}
