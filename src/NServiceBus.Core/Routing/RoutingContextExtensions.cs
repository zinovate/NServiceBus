﻿namespace NServiceBus
{
    using System;
    using System.Collections.Generic;
    using NServiceBus.ConsistencyGuarantees;
    using NServiceBus.DeliveryConstraints;
    using NServiceBus.Extensibility;
    using NServiceBus.Pipeline;
    using NServiceBus.Pipeline.Contexts;
    using NServiceBus.Transports;

    /// <summary>
    /// 
    /// </summary>
    public static class RoutingContextExtensions
    {
        /// <summary>
        /// Tells if this operation is a reply
        /// </summary>
        /// <param name="context">Context beeing extended</param>
        /// <returns>True if the operation is a reply</returns>
        public static bool IsReply(this OutgoingContext context)
        {
            return context.Get<ExtendableOptions>() is ReplyOptions;
        }

        /// <summary>
        /// Tells if this operation is a publish
        /// </summary>
        /// <param name="context">Context beeing extended</param>
        /// <returns>True if the operation is a publish</returns>
        public static bool IsPublish(this OutgoingContext context)
        {
            return context.Get<ExtendableOptions>() is PublishOptions;
        }

        /// <summary>
        /// Tells if this operation is a publish
        /// </summary>
        /// <param name="context">Context beeing extended</param>
        /// <returns>True if the operation is a publish</returns>
        public static bool IsPublish(this PhysicalOutgoingContextStageBehavior.Context context)
        {
            return context.Get<ExtendableOptions>() is PublishOptions;
        }

        /// <summary>
        /// Tells if this operation is a send
        /// </summary>
        /// <param name="context">Context beeing extended</param>
        /// <returns>True if the operation is a publish</returns>
        public static bool IsSend(this OutgoingContext context)
        {
            return context.Get<ExtendableOptions>() is SendOptions || context.Get<ExtendableOptions>() is SendLocalOptions;
        }
        /// <summary>
        /// Tells if this operation is a reply
        /// </summary>
        /// <param name="context">Context beeing extended</param>
        /// <returns>True if the operation is a reply</returns>
        public static bool IsReply(this PhysicalOutgoingContextStageBehavior.Context context)
        {
            return context.Get<ExtendableOptions>() is ReplyOptions;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public static class RoutingOptionExtensions
    {
        /// <summary>
        /// Allows a specific physical address to be used to route this message
        /// </summary>
        /// <param name="option">Option beeing extended</param>
        /// <param name="destination">The destination address</param>
        public static void SetDestination(this SendOptions option,string destination)
        {
            Guard.AgainstNullAndEmpty(destination,"destination");

            option.Extensions.GetOrCreate<DetermineRoutingForMessageBehavior.State>()
                .ExplicitDestination = destination;
        }

        /// <summary>
        /// Allows the target endpoint instance for this reply to set. If not used the reply will be sent to the `ReplyToAddress` of the incoming message
        /// </summary>
        /// <param name="option">Option beeing extended</param>
        /// <param name="destination">The new target address</param>
        public static void OverrideReplyToAddressOfIncomingMessage(this ReplyOptions option, string destination)
        {
            Guard.AgainstNullAndEmpty(destination, "destination");

            option.Extensions.GetOrCreate<DetermineRoutingForMessageBehavior.State>()
                .ExplicitDestination = destination;
        }

        /// <summary>
        /// Routes this message to the local endpoint instance
        /// </summary>
        /// <param name="option">Context beeing extended</param>
        public static void RouteToLocalEndpointInstance(this SendOptions option)
        {
            option.Extensions.GetOrCreate<DetermineRoutingForMessageBehavior.State>()
                .RouteToLocalInstance = true;
        }
    }

    abstract class RoutingStrategy
    {
        public void Deserialize(Dictionary<string, string> options)
        {
            

        }

        public abstract void Dispatch(OutgoingMessage message,
            ConsistencyGuarantee minimumConsistencyGuarantee,
            IEnumerable<DeliveryConstraint> constraints,
            BehaviorContext currentContext);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DelayedDelivery : DeliveryConstraint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay"></param>
        public DelayedDelivery(TimeSpan delay)
        {
            DelayDeliveryWith = delay;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doNotDeliverBefore"></param>
        public DelayedDelivery(DateTime doNotDeliverBefore)
        {
            DoNotDeliverBefore = doNotDeliverBefore;
        }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan? DelayDeliveryWith { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime? DoNotDeliverBefore { get; private set; }

        internal override bool Deserialize(Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }

        internal override void Serialize(Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NonDurableDelivery : DeliveryConstraint
    {
        internal override bool Deserialize(Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }

        internal override void Serialize(Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }
    }

    class RoutingStrategyFactory
    {
        public RoutingStrategy Create(Dictionary<string, string> options)
        {
            return null;
        }
    }
}