﻿namespace NServiceBus.DelayedDelivery
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represent a constraint that the message can't be delivered before the specified delay has elapsed
    /// </summary>
    public class DelayDeliveryWith : DelayedDeliveryConstraint
    {
        /// <summary>
        /// Initializes the constraint
        /// </summary>
        /// <param name="delay">How long to delay the delivery of the message</param>
        public DelayDeliveryWith(TimeSpan delay)
        {
            Delay = delay;
        }

        /// <summary>
        /// The requested delay
        /// </summary>
        public TimeSpan Delay { get; private set; }


        internal override bool Deserialize(Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }

        internal override void Serialize(Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }
    }
}