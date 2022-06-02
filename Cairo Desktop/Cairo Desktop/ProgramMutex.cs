using System;
using System.Diagnostics;
using System.Threading;

namespace CairoDesktop
{
    public class ProgramMutex : IDisposable
    {
        private const string MutexName = "CairoShell";
        private const int MutexAttempts = 3;

        public static ProgramMutex Aquire()
        {
            //for (int i = 0; i < MutexAttempts; ++i)
            var attempt = 1;
            do
            {
                var mutex = AquireOwned();
                if (mutex != null) return new ProgramMutex(mutex);

                if (attempt > MutexAttempts) break;

                Debug.WriteLine("Failed to aquire program mutex. Sleeping before trying again...");
                ++attempt;
                Thread.Sleep(TimeSpan.FromSeconds(1));
            } while (true);

            return null;
        }

        private static Mutex AquireOwned()
        {
            var mutex = new Mutex(initiallyOwned: true, MutexName, out bool isOwned);
            if (isOwned) return mutex;

            mutex.Dispose();
            return null;
        }

        #region class

        private readonly Mutex _cairoMutex;

        private ProgramMutex(Mutex mutex)
        {
            _cairoMutex = mutex;
        }

        public void Dispose()
        {
            _cairoMutex.Dispose();
        }

        #endregion class
    }
}
