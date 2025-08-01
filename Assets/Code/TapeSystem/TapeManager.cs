using UnityEngine;
namespace Code.Core.UnityUtils.Persistence 
{
    public class TapeManager : PersistentSingleton<TapeManager>
    {
        [SerializeField] private Tape _currentTape;
        public Tape CurrentTape => _currentTape;
    }
}

