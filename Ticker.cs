using System;
using System.Collections.Generic;
using UnityEngine;

namespace MKUtil
{

    public class TickHandle
    {

        public Action Callback;
        public bool IsPaused { get; set; }
        public readonly float TickRate;

        public TickHandle(float rate, Action callback)
        {
            this.TickRate = rate;
            this.IsPaused = false;
            this.Callback = callback;
        }

        public void Fire()
        {
            if (!IsPaused)
            {
                Callback?.Invoke();
            }
        }
    }

    public class Ticker
    {
        public List<TickHandle> handles = new List<TickHandle>();
        public float Accumulator { get; private set; }
        public float TickRate { get; }
        public bool IsReady => Accumulator >= TickRate;
        public int Count => handles.Count;

        public Ticker(float tickRate)
        {
            TickRate = tickRate;
            Accumulator = 0.0f;
        }
        public void Tick(float time)
        {
            Accumulator += time;

            while (IsReady)
            {
                Accumulator -= TickRate;
                for (int i = handles.Count - 1; i >= 0; i--)
                {
                    handles[i].Fire();
                }
            }
        }
        public TickHandle Subscribe(Action callback)
        {
            var handle = new TickHandle(TickRate, callback);
            handles.Add(handle);
            return handle;
        }
        public void Pause(TickHandle handle, bool pause = true)
        {
            handle.IsPaused = pause;
        }
        public void Unsubscribe(Action callback)
        {
            // Find the action
            for (int i = handles.Count - 1; i >= 0; i--)
            {
                if (handles[i].Callback == callback)
                {
                    handles.RemoveAt(i);
                    break;
                }
            }
        }
        public void Unsubscribe(TickHandle handle)
        {
            handles.Remove(handle);
        }
    }

    public class TickManager
    {
        public List<Ticker> tickers = new List<Ticker>();
        public TickHandle Subscribe(float rate, Action callback)
        {
            // Find a NewTicker with the correct rate.
            Ticker ticker = null;
            for (int i = 0; i < tickers.Count; i++)
            {
                if (Mathf.Approximately(tickers[i].TickRate, rate))
                {
                    ticker = tickers[i];
                    break;
                }
            }
            if (ticker == null)
            {
                ticker = new Ticker(rate);
                tickers.Add(ticker);
            }
            return ticker.Subscribe(callback);
        }
        public void Unsubscribe(float rate, Action callback)
        {
            for (int i = 0; i < tickers.Count; i++)
            {
                var tempTicker = tickers[i];
                if (tempTicker.TickRate == rate)
                {
                    tempTicker.Unsubscribe(callback);
                    break;
                }
            }
        }
        public void Update()
        {
            float dt = Time.deltaTime;
            int count = tickers.Count;

            for (int i = 0; i < count; i++)
            {
                tickers[i].Tick(dt);
            }
        }
    }

}