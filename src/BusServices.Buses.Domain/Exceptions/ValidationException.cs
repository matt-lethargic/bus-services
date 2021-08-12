using System;
using System.Collections.Generic;
using System.Text;

namespace BusServices.Buses.Domain.Exceptions
{
    public class DomainValidationException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public DomainValidationException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
