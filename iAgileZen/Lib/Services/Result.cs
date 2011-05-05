using System;

namespace AgileZen.Lib
{
	
	/// <summary>
	/// Rest service (stjålet villt fra  Jonas Follesøs Flytider-kodebase 
	/// https://github.com/follesoe/FlightsNorway.git
	/// </summary>
    public class Result<T>
    {
        public Exception Error { get; private set; }
        public T Value { get; private set;  }

        public Result(T value)
        {
            Value = value;
        }

        public Result(Exception error)
        {
            Error = error;
        }

        public bool HasError()
        {
            return Error != null;
        }
    }
}