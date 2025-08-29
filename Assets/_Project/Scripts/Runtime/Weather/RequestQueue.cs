using System;
using System.Collections.Concurrent;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Test.Core
{
    public sealed class RequestQueue : IDisposable
    {
        private readonly ConcurrentQueue<Func<CancellationToken, UniTask>> _queue = new();
        private readonly CancellationTokenSource _cts = new();
        private bool _isRunning;

        public void Enqueue(Func<CancellationToken, UniTask> request)
        {
            _queue.Enqueue(request);
            if (!_isRunning)
                RunQueueAsync(_cts.Token).Forget();
        }

        public void ClearQueue()
        {
            while (_queue.TryDequeue(out _)) { }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        private async UniTaskVoid RunQueueAsync(CancellationToken token)
        {
            _isRunning = true;
            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (!_queue.TryDequeue(out var request))
                    {
                        await UniTask.Yield(); 
                        continue;
                    }

                    try
                    {
                        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token);
                        await request(linkedCts.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.Log("Request cancelled");
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Request error: {ex}");
                    }
                }
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}