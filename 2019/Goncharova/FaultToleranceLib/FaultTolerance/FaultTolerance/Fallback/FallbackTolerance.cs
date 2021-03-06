﻿using System;
using System.Threading;

namespace FaultTolerance.Fallback
{
    public class FallbackTolerance : Tolerance
    {
        private Action fallbackAction;
        private readonly ToleranceExceptions configuredExceptions;

        public FallbackTolerance(ToleranceBuilder ToleranceBuilder, Action fallbackAction)
        {
            configuredExceptions = ToleranceBuilder.configuredExceptions;
            FallbackAction = fallbackAction;
        }

        private Action FallbackAction
        {
            get => fallbackAction;
            set
            {
                fallbackAction = value ?? throw new ArgumentNullException("Fallback action can't be null");
            }
        }

        public override void Execute(Action<CancellationToken> action)
            => FallbackProcessor.Execute(_ => action(_), 
                                         configuredExceptions, 
                                         () => FallbackAction());
    }
}