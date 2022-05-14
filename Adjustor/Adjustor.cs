using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

namespace Cardinal.Adjustor
{
    public class Adjustor : CardinalSingleton<Adjustor>
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #region Messages

        public void Message() { }
        public void Message(ResponseSubject actor, 
            ResponseAction action, ResponseLocation location)
        {
            EventBus.Trigger(EventNames.EngageAdjustor, 
                new ActorActionLocationData(actor,action,location));
        }
        public void Message(ResponseSubject actor, 
            ResponseAction action, ResponseSubject subject)
        {
            EventBus.Trigger(EventNames.EngageAdjustor, new ActorActionSubjectData(actor, action, subject));
        }
        public void Message(ResponseSubject actor, 
            ResponseAction action, ResponseGoal goal)
        {
            EventBus.Trigger(EventNames.EngageAdjustor, new ActorActionGoalData(actor,action,goal));
        }
        public void Message(ResponseSubject actor,
            ResponseAction action,ResponseModifier modifier, 
            ResponseGoal goal)
        {
            EventBus.Trigger(EventNames.EngageAdjustor, new ActorActionModifierGoalData(actor, action,modifier, goal));
        }
        public void Message(ResponseSubject actor, 
            ResponseAction action)
        {
            EventBus.Trigger(EventNames.EngageAdjustor, new ActorActionData(actor,action));
        }
        public void Message(ResponseSubject actor, 
            ResponseValue value)
        {
            EventBus.Trigger(EventNames.EngageAdjustor, new ActorValueData(actor,value));
        }
        public void Message(ResponseSubject actor, 
            ResponseAction action, ResponseSubject subject, 
            ResponseModifier modifier)
        {
            EventBus.Trigger(EventNames.EngageAdjustor, new ActorActionSubjectModifierData(actor,action,subject,modifier));
        }
        public void Message(ResponseSubject actor, 
            ResponseValue value, ResponseAction action)
        {
            EventBus.Trigger(EventNames.EngageAdjustor, new ActorValueActionData(actor,value,action));
        }

        #endregion
    }

    #region Visual Graph Node Starters
    [UnitTitle("On Adjustor Request")]
    [UnitCategory("Events\\CardinalEvents")]
    [UnitSubtitle("Actor-Action-Location")]
    public class ActorActionLocationRequest : EventUnit<ActorActionLocationData>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.
        protected override bool register => true;

        // Adding an EventHook with the name of the event to the list of visual scripting events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.EngageAdjustor);
        }
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            result = ValueOutput<ActorActionLocationData>(nameof(result));
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, ActorActionLocationData data)
        {
            flow.SetValue(result, data);
        }
    }

    [UnitTitle("On Adjustor Request")]
    [UnitCategory("Events\\CardinalEvents")]
    [UnitSubtitle("Actor-Action-Subject")]
    public class ActorActionSubjectRequest : EventUnit<ActorActionSubjectData>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.
        protected override bool register => true;

        // Adding an EventHook with the name of the event to the list of visual scripting events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.EngageAdjustor);
        }
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            result = ValueOutput<ActorActionSubjectData>(nameof(result));
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, ActorActionSubjectData data)
        {
            flow.SetValue(result, data);
        }
    }
        
    [UnitTitle("On Adjustor Request")]
    [UnitCategory("Events\\CardinalEvents")]
    [UnitSubtitle("Actor-Action-Goal")]
    public class ActorActionGoalRequest : EventUnit<ActorActionGoalData>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.
        protected override bool register => true;

        // Adding an EventHook with the name of the event to the list of visual scripting events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.EngageAdjustor);
        }
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            result = ValueOutput<ActorActionGoalData>(nameof(result));
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, ActorActionGoalData data)
        {
            flow.SetValue(result, data);
        }
    }

    [UnitTitle("On Adjustor Request")]
    [UnitCategory("Events\\CardinalEvents")]
    [UnitSubtitle("Actor-Action-Modifier-Goal")]
    public class ActorActionModifierGoalRequest : EventUnit<ActorActionModifierGoalData>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.
        protected override bool register => true;

        // Adding an EventHook with the name of the event to the list of visual scripting events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.EngageAdjustor);
        }
        protected override void Definition()
        {
            base.Definition();
            result = ValueOutput<ActorActionModifierGoalData>(nameof(result));
        }
        protected override void AssignArguments(Flow flow, ActorActionModifierGoalData data)
        {
            flow.SetValue(result, data);
        }
    }

    [UnitTitle("On Adjustor Request")]
    [UnitCategory("Events\\CardinalEvents")]
    [UnitSubtitle("Actor-Action")]
    public class ActorActionRequest : EventUnit<ActorActionData>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.
        protected override bool register => true;

        // Adding an EventHook with the name of the event to the list of visual scripting events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.EngageAdjustor);
        }
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            result = ValueOutput<ActorActionData>(nameof(result));
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, ActorActionData data)
        {
            flow.SetValue(result, data);
        }
    }
  
    [UnitTitle("On Adjustor Request")]
    [UnitCategory("Events\\CardinalEvents")]
    [UnitSubtitle("Actor-Value")]
    public class ActorValueRequest : EventUnit<ActorValueData>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.
        protected override bool register => true;

        // Adding an EventHook with the name of the event to the list of visual scripting events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.EngageAdjustor);
        }
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            result = ValueOutput<ActorValueData>(nameof(result));
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, ActorValueData data)
        {
            flow.SetValue(result, data);
        }
    }
       
    [UnitTitle("On Adjustor Request")]
    [UnitCategory("Events\\CardinalEvents")]
    [UnitSubtitle("Actor-Action-Subject-Modifier")]
    public class ActorActionSubjectModifierRequest : EventUnit<ActorActionSubjectModifierData>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.
        protected override bool register => true;

        // Adding an EventHook with the name of the event to the list of visual scripting events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.EngageAdjustor);
        }
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            result = ValueOutput<ActorActionSubjectModifierData>(nameof(result));
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, ActorActionSubjectModifierData data)
        {
            flow.SetValue(result, data);
        }
    }
        
    [UnitTitle("On Adjustor Request")]
    [UnitCategory("Events\\CardinalEvents")]
    [UnitSubtitle("Actor-Value-Action")]
    public class ActorValueActionRequest : EventUnit<ActorValueActionData>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.
        protected override bool register => true;

        // Adding an EventHook with the name of the event to the list of visual scripting events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.EngageAdjustor);
        }
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            result = ValueOutput<ActorValueActionData>(nameof(result));
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, ActorValueActionData data)
        {
            flow.SetValue(result, data);
        }
    }

    #endregion
    #region DataBlobs
    [System.Serializable]
    public class ActorActionLocationData
    {
        public ResponseSubject actor;
        public ResponseAction action;
        public ResponseLocation location;

        public ActorActionLocationData(ResponseSubject actor, ResponseAction action, ResponseLocation location)
        {
            this.actor = actor;
            this.action = action;
            this.location = location;
        }
    }
    [System.Serializable]
    public class ActorActionSubjectData
    {
        public ResponseSubject actor;
        public ResponseAction action;
        public ResponseSubject subject;

        public ActorActionSubjectData(ResponseSubject actor, ResponseAction action, ResponseSubject subject)
        {
            this.actor = actor;
            this.action = action;
            this.subject = subject;
        }
    }
    [System.Serializable]
    public class ActorActionGoalData
    {
        public ResponseSubject actor;
        public ResponseAction action;
        public ResponseGoal Goal;

        public ActorActionGoalData(ResponseSubject actor, ResponseAction action, ResponseGoal goal)
        {
            this.actor = actor;
            this.action = action;
            Goal = goal;
        }
    }
    [System.Serializable]
    public class ActorActionModifierGoalData
    {
        public ResponseSubject actor;
        public ResponseAction action;
        public ResponseModifier modifier;
        public ResponseGoal goal;

        public ActorActionModifierGoalData(ResponseSubject actor, ResponseAction action, ResponseModifier modifier, ResponseGoal goal)
        {
            this.actor = actor;
            this.action = action;
            this.modifier = modifier;
            this.goal = goal;
        }
    }
    [System.Serializable]
    public class ActorActionData
    {
        public ResponseSubject actor;
        public ResponseAction action;

        public ActorActionData(ResponseSubject actor, ResponseAction action)
        {
            this.actor = actor;
            this.action = action;
        }
    }
    [System.Serializable]
    public class ActorValueData
    {
        public ResponseSubject actor;
        public ResponseValue value;

        public ActorValueData(ResponseSubject actor, ResponseValue value)
        {
            this.actor = actor;
            this.value = value;
        }
    }
    [System.Serializable]
    public class ActorActionSubjectModifierData
    {
        public ResponseSubject actor;
        public ResponseAction action;
        public ResponseSubject subject;
        public ResponseModifier modifier;

        public ActorActionSubjectModifierData(ResponseSubject actor, ResponseAction action, ResponseSubject subject, ResponseModifier modifier)
        {
            this.actor = actor;
            this.action = action;
            this.subject = subject;
            this.modifier = modifier;
        }
    }
    [System.Serializable]
    public class ActorValueActionData
    {
        public ResponseSubject actor;
        public ResponseValue value;
        public ResponseAction action;

        public ActorValueActionData(ResponseSubject actor, ResponseValue value, ResponseAction action)
        {
            this.actor = actor;
            this.value = value;
            this.action = action;
        }
    }
    #endregion
}

