﻿namespace Shared.Base
{
    public interface IParameters
    {
        public string ModuleName { get; init; }

        public bool WriteToDisk { get; init; }
    }
}