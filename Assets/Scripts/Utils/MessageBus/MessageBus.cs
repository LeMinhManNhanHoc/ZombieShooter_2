using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessageBusSystem
{
    public class MessageBus
    {
        private static bool isRunning = false;
        private static Dictionary<MessageType, Action<object>> messageDict = new Dictionary<MessageType, Action<object>>();
        private static Queue<MessageType> messageQueue = new Queue<MessageType>();

        public static void Subsribe(MessageType type, Action<object> callback)
        {
            if (messageDict.ContainsKey(type))
            {
                messageDict[type] += callback;
            }
            else
            {
                messageDict.Add(type, callback);
            }
        }

        public static void Unsubscribe(MessageType type, Action<object> callback)
        {
            if (messageDict.ContainsKey(type))
            {
                messageDict[type] -= callback;
            }
        }

        public static void Announce(MessageType type, object data)
        {
            messageQueue.Enqueue(type);

            if (isRunning) return;

            while (messageQueue.Count > 0)
            {
                isRunning = true;

                MessageType mesToBroadCast = messageQueue.Dequeue();

                if (messageDict.ContainsKey(mesToBroadCast))
                {
                    messageDict[mesToBroadCast]?.Invoke(data);
                }
            }

            isRunning = false;
        }
    }
}