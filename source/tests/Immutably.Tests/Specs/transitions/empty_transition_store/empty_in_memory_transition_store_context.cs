﻿using System;
using System.Collections.Generic;
using System.Linq;
using Immutably.Messages;
using Immutably.Transitions;
using Machine.Specifications;

namespace Immutably.Tests.Specs.transitions.empty_transition_store
{
    public class empty_in_memory_transition_store_context
    {
        public static SimpleEvent evnt;
        public static InMemoryTransitionStore store;
        public static List<ITransition> transitions;

        Establish context = () =>
        {
            evnt = new SimpleEvent()
            {
                Id = Guid.NewGuid().ToString(),
                Year = 54545,
                Name = "Lenin"
            };

            store = new InMemoryTransitionStore();
        };

        public static List<ITransition> LoadAllStreamTransitions()
        {
            using (var reader = store.CreateStreamReader(evnt.Id))
            {
                return reader.ReadAll().ToList();
            }
        }

        public static void WriteTransition(String streamId, Int32 streamSequence, IEvent newEvent)
        {
            using (var writer = store.CreateStreamWriter(streamId))
            {
                writer.Write(streamSequence, builder => builder
                    .AddEvent(newEvent)
                );
            }            
        }
    }

    public class SimpleEvent : IEvent
    {
        public String Id { get; set; }
        public Int32 Year { get; set; }
        public String Name { get; set; }
    }
}